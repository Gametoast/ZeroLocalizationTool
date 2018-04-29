using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWBF2_Localization_Parser
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"J:\BF2_ModTools\data_LCT\Common\Localize\english.cfg";
			string path2 = @"J:\BF2_ModTools\data_TCW\data_TCW\Common\Localize\english.cfg";

			// StreamReader vars
			string curLine;
			StreamReader file = new StreamReader(path2);
			DataBase db = new DataBase();

			// Iteration vars
			Chunk parentChunk = Chunk.DataBase;
			Chunk parentChunkParent = Chunk.DataBase;
			Scope lastScope = new Scope();
			Key lastKey = new Key();
			int curIndex = 0;

			while ((curLine = file.ReadLine()) != null)
			{
				if (curLine == "DataBase()")				// Beginning of DataBase
				{
					parentChunk = Chunk.DataBase;
				}
				else if (curLine.Contains("VarScope("))     // Beginning of VarScope
				{
					// Create the new Scope
					Scope scope = new Scope();
					scope.Name = ParseValue(curLine);

					if (parentChunk == Chunk.DataBase)
					{
						// Add the new Scope to the DataBase container
						db.Scopes.Add(scope);

						// Get a ref to the most recent Scope
						curIndex = db.Scopes.Count - 1;
						lastScope = db.Scopes[curIndex];
					}
					else if (parentChunk == Chunk.VarScope)
					{
						if (lastScope != null)
						{
							// Add the new Scope to the DataBase container
							lastScope.Scopes.Add(scope);

							// Get a ref to the most recent Scope
							curIndex = lastScope.Scopes.Count - 1;
							lastScope = lastScope.Scopes[curIndex];
						}
					}

					if (parentChunk == Chunk.DataBase)
					{
						parentChunkParent = parentChunk;
					}
					parentChunk = Chunk.VarScope;
				}
				else if (curLine.Contains("VarBinary("))	// Beginning of VarBinary
				{
					// Create the new Key
					Key key = new Key();
					key.Name = ParseValue(curLine);

					if (parentChunk == Chunk.DataBase)
					{
						// Add the new Key to the DataBase container
						db.Keys.Add(key);

						// Get a ref to the most recent Key
						curIndex = db.Keys.Count - 1;
						lastKey = db.Keys[curIndex];
					}
					else if (parentChunk == Chunk.VarScope)
					{
						if (lastKey != null)
						{
							// Add the new Key to the Scope container
							lastScope.Keys.Add(key);

							// Get a ref to the most recent Key
							curIndex = lastScope.Keys.Count - 1;
							lastKey = lastScope.Keys[curIndex];
						}
					}

					if (parentChunk == Chunk.DataBase || parentChunk == Chunk.VarScope)
					{
						parentChunkParent = parentChunk;
					}
					parentChunk = Chunk.VarBinary;
				}
				else if (parentChunk == Chunk.VarBinary)
				{
					if (curLine.Contains("Size("))
					{
						lastKey.Size = ParseValue(curLine);
					}
					else if (curLine.Contains("Value("))
					{
						lastKey.BinaryValues.Add(ParseValue(curLine));
					}
					else if (curLine.Contains("}"))
					{
						parentChunk = parentChunkParent;
					}
				}
			}

			//DataBase db = new DataBase();
			//db = ParseDataBase(flattenedLines);

			// Exit the application
			Console.WriteLine("\n" + "Press any key to exit.");
			Console.ReadKey();
		}

		enum Chunk
		{
			DataBase,
			VarScope,
			VarBinary
		};

		struct ParentObj
		{
			Chunk ObjType;
			int Index;
		};

		static DataBase ParseDataBase(string[] lines)
		{
			DataBase db = new DataBase();

			Chunk curChunk;
			bool startOfChunk = false;
			bool endOfChunk = false;
			int depth = 0;
			object chunkObj;

			for (int lineIdx = 0; lineIdx < lines.Length; lineIdx++)
			{
				string curLine = lines[lineIdx];
				string nextLine = "<EOF>";
				if (lineIdx < lines.Length - 1)
				{
					nextLine = lines[lineIdx + 1];
				}

				// Determine which chunk we're in
				if (curLine == "DataBase()")
				{
					curChunk = Chunk.DataBase;
					depth = 0;
				}
				else if (curLine.Contains("VarScope("))
				{
					curChunk = Chunk.VarScope;
					Scope scope = new Scope();
					scope.Name = ParseValue(curLine);
				}
				else if (curLine.Contains("VarBinary("))
				{
					curChunk = Chunk.VarBinary;
				}

				// Adjust the chunk depth
				if (curLine == "{")
				{
					depth++;
				}
				else if (curLine == "}")
				{
					depth--;
				}
			}

			return db;
		}

		static string ParseValue(string line)
		{
			string formattedLine = line.Replace("\"", "|");

			bool useQuotations = false;
			if (line.Contains("|"))
			{
				useQuotations = true;
			}

			char openChar, closeChar;
			int openIdx, closeIdx;
			if (useQuotations)
			{
				openChar = '|';
				closeChar = '|';

				openIdx = formattedLine.IndexOf(openChar) + 1;
				closeIdx = formattedLine.LastIndexOf(closeChar) - 1;
			}
			else
			{
				openChar = '(';
				closeChar = ')';

				openIdx = formattedLine.IndexOf(openChar) + 1;
				closeIdx = formattedLine.LastIndexOf(closeChar);
			}

			string parsedValue;

			if (openIdx != closeIdx)
			{
				parsedValue = StringExt.SubstringIdx(formattedLine, openIdx, closeIdx);
				parsedValue = parsedValue.Replace("|", "");
			}
			else
			{
				parsedValue = formattedLine[openIdx].ToString();
			}

			return parsedValue;
		}
	}

	static class StringExt
	{
		public static string SubstringIdx(this string value, int startIndex, int endIndex)
		{
			if (value == null) throw new ArgumentNullException();
			if (endIndex > value.Length) throw new IndexOutOfRangeException("End index must be less than or equal to the length of the string.");
			if (startIndex < 0 || startIndex > value.Length + 1) throw new IndexOutOfRangeException("Start index must be between zero and the length of the string minus one");
			if (startIndex >= endIndex) throw new ArgumentOutOfRangeException("Start index must be less then end index");

			var length = endIndex - startIndex;
			return value.Substring(startIndex, length);
		}
	}

	class DataBase
	{
		public List<Scope> Scopes { get; set; }
		public List<Key> Keys { get; set; }

		public DataBase()
		{
			Scopes = new List<Scope>();
			Keys = new List<Key>();
		}
	}

	class Scope
	{
		public string Name { get; set; }
		public List<Scope> Scopes { get; set; }
		public List<Key> Keys { get; set; }

		public Scope()
		{
			Scopes = new List<Scope>();
			Keys = new List<Key>();
		}
	}

	class Key
	{
		public string Name { get; set; }
		public string Size { get; set; }
		public List<string> BinaryValues { get; set; }
		public string Value { get; set; }

		public Key()
		{
			BinaryValues = new List<string>();
		}
	}

	class BinDataBase
	{
		public List<BinScope> Scopes { get; set; }

		public BinDataBase(string[] fileLines)
		{

		}
	}

	class BinScope
	{
		public string Name { get; set; }
		public List<BinScope> Scopes { get; set; }
		public List<BinKey> Keys { get; set; }
	}

	class BinKey
	{
		public string Name { get; set; }
		public int Size { get; set; }
		public List<string> Values { get; set; }

		public string ParseValues()
		{
			string parsedValues = "";

			return parsedValues;
		}
	}
}
