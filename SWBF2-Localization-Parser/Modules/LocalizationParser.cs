using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroLocalizationTool.Modules
{
    public static class LocalizationParser
	{
		enum Chunk
		{
			DataBase,
			VarScope,
			VarBinary
		}

		public static DataBase ParseDataBase(string filePath)
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
								curScope.Scopes.Add(scope); //						<--  might be what's causing problems

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
						if (scopes.Count != 0) curScope = scopes.Last();
					}
					else if (curParentChunk == Chunk.VarBinary)
					{
						curKey.Value = StringExt.ConvertUnicodeListToString(curKey.BinaryValues);
					}

					parentChunks.RemoveAt(parentChunks.Count - 1);
					if (parentChunks.Count != 0) curParentChunk = parentChunks.Last();
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

		public static string ParseValue(string line)
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

	public class DataBase
	{
		public List<Scope> Scopes { get; set; }
		public List<Key> Keys { get; set; }

		public DataBase()
		{
			Scopes = new List<Scope>();
			Keys = new List<Key>();
		}
	}

	public class Scope
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

	public class Key
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
