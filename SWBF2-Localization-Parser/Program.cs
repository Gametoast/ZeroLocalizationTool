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
			string path = @"J:\BF2_ModTools\data_LCT\Common\english2.cfg";
			string path2 = @"J:\BF2_ModTools\data_TCW\data_TCW\Common\Localize\english.cfg";

			DataBase db = new DataBase();
			db = ParseDataBase(path);

			//List<string> testList = new List<string>();
			//testList.Add("00000000450034007500F500440056006700F500240057009600C6004600F500");
			//testList.Add("33000300230013001300F20003001300D000A00005002500F400050054002500");
			//testList.Add("450095000200F400640002006400250014009500540044000200750094002500");
			//testList.Add("54003500020035004500550044009400F4003500D000A0004400F4000200E400");
			//testList.Add("F400450002004400940035004500250094002400550045005400");

			//Console.WriteLine(StringExt.ConvertUnicodeListToString(testList));

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

		static DataBase ParseDataBase(string filePath)
		{
			// StreamReader vars
			//StreamReader file = new StreamReader(filePath);
			string[] file = File.ReadAllLines(filePath);
			DataBase db = new DataBase();

			// Iteration vars
			Chunk curParentChunk = Chunk.DataBase;
			List<Chunk> parentChunks = new List<Chunk>();
			Scope curScope = new Scope();
			List<Scope> scopes = new List<Scope>();
			Key curKey = new Key();
			int curIndex = 0;

			for (int i = 0; i < file.Length; i++)
			{
				string line = file[i];

				// Set the current chunk header
				if (line == "DataBase()")
				{
					curParentChunk = Chunk.DataBase;
				}
				else if (line.Contains("VarScope("))
				{
					curParentChunk = Chunk.VarScope;
				}
				else if (line.Contains("VarBinary("))
				{
					curParentChunk = Chunk.VarBinary;
				}


				// Are we opening a new chunk?
				else if (line.Contains("{"))
				{
					if (curParentChunk == Chunk.VarScope)
					{
						// Initialize the new Scope
						Scope scope = new Scope();
						scope.Name = ParseValue(file[i - 1]);

						// Add the new Scope to the DataBase container
						if (parentChunks.Last() == Chunk.DataBase)
						{
							db.Scopes.Add(scope);

							// Get a ref to the most recent Scope
							curIndex = db.Scopes.Count - 1;
							curScope = db.Scopes[curIndex];
						}
						// Add the new Scope to the Scope container
						else if (parentChunks.Last() == Chunk.VarScope)
						{
							if (curScope != null)
							{
								// Add the new Scope to the DataBase container
								curScope.Scopes.Add(scope);	//						<--  might be what's causing problems

								// Get a ref to the most recent Scope
								curIndex = curScope.Scopes.Count - 1;
								curScope = curScope.Scopes[curIndex];
							}
						}

						scopes.Add(curScope);
						//curLastScope = new Scope();
					}
					else if (curParentChunk == Chunk.VarBinary)
					{
						// Initialize the new Key
						Key key = new Key();
						key.Name = ParseValue(file[i - 1]);

						if (parentChunks.Last() == Chunk.DataBase)
						{
							// Add the new Key to the DataBase container
							db.Keys.Add(key);

							// Get a ref to the most recent Key
							curIndex = db.Keys.Count - 1;
							curKey = db.Keys[curIndex];
						}
						else if (parentChunks.Last() == Chunk.VarScope)
						{
							if (curKey != null)
							{
								// Add the new Key to the Scope container
								curScope.Keys.Add(key);

								// Get a ref to the most recent Key
								curIndex = curScope.Keys.Count - 1;
								curKey = curScope.Keys[curIndex];
							}
						}

						//if (parentChunks.Last() == Chunk.DataBase || parentChunks.Last() == Chunk.VarScope)
						//{
						//	parentChunkParent = parentChunk;
						//}
					}
					parentChunks.Add(curParentChunk);
				}
				// Are we closing the opened chunk?
				else if (line.Contains("}"))
				{
					if (curParentChunk == Chunk.VarScope)
					{
						// Close out of the Scope
						scopes.RemoveAt(scopes.Count - 1);
					}
					else if (curParentChunk == Chunk.VarBinary)
					{
						curKey.Value = StringExt.ConvertUnicodeListToString(curKey.BinaryValues);
					}
					parentChunks.RemoveAt(parentChunks.Count - 1);
				}


				// If we're in a VarBinary chunk, parse the Key properties
				else if (parentChunks.Last() == Chunk.VarBinary)
				{
					if (line.Contains("Size("))
					{
						curKey.Size = ParseValue(line);
					}
					else if (line.Contains("Value("))
					{
						curKey.BinaryValues.Add(ParseValue(line));
					}
					//else if (curLine.Contains("}"))
					//{
					//	curParentChunk = parentChunkParent;
					//}
				}
				else if (parentChunks.Last() == Chunk.VarScope)
				{
					//if (curLine.Contains("}"))
					//{
					//	curParentChunk = parentChunkParent;
					//}
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
			if (startIndex >= endIndex) throw new ArgumentOutOfRangeException("Start index must be less than end index");

			var length = endIndex - startIndex;
			return value.Substring(startIndex, length);
		}

		public static string Reverse(string s)
		{
			char[] charArray = s.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}

		public static string ConvertUnicodeListToString(List<string> s)
		{
			string combinedStr = String.Concat(s);
			string[] valueArray = Split(combinedStr, 4).ToArray();
			string[] reversedValues = new string[valueArray.Length];
			char[] chars = new char[valueArray.Length];

			// Go through the list of values, then reverse them and convert them into their character representations
			for (int i = 0; i < valueArray.Length; i++)
			{
				reversedValues[i] = Reverse(valueArray[i]);
				chars[i] = (char)int.Parse(reversedValues[i], System.Globalization.NumberStyles.HexNumber);
			}

			return new string(chars);
		}

		public static IEnumerable<string> Split(string str, int chunkSize)
		{
			return Enumerable.Range(0, str.Length / chunkSize)
				.Select(i => str.Substring(i * chunkSize, chunkSize));
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
}
