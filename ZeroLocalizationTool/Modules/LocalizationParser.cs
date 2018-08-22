using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroLocalizationTool;

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

		/// <summary>
		/// Constructs a DataBase from the specified localization file.
		/// </summary>
		/// <param name="filePath">Path of localization file to parse.</param>
		/// <returns>DataBase containing the localization file's parsed contents.</returns>
		/// <exception cref="System.Security.SecurityException"></exception>
		/// <exception cref="NotSupportedException"></exception>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="UnauthorizedAccessException"></exception>
		/// <exception cref="IOException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public static DataBase ParseDataBase(string filePath)
		{
			DataBase db = new DataBase();

			try
			{
				// StreamReader vars
				//StreamReader file = new StreamReader(filePath);
				string[] file = File.ReadAllLines(filePath);

				// Iteration vars
				Chunk curParentChunk = Chunk.DataBase;
				List<Chunk> parentChunks = new List<Chunk>();
				Scope curScope = new Scope();
				List<Scope> scopes = new List<Scope>();
				Key curKey = new Key();
				int curIndex = 0;

				// Go through each line of the file
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
			}
			catch (System.Security.SecurityException ex)
			{
				throw new System.Security.SecurityException(ex.Message, ex);
			}
			catch (NotSupportedException ex)
			{
				throw new NotSupportedException(ex.Message, ex);
			}
			catch (FileNotFoundException ex)
			{
				throw new FileNotFoundException(ex.Message, filePath, ex);
			}
			catch (UnauthorizedAccessException ex)
			{
				throw new UnauthorizedAccessException(ex.Message, ex);
			}
			catch (IOException ex)
			{
				throw new IOException(ex.Message, ex);
			}
			catch (ArgumentNullException ex)
			{
				throw new ArgumentNullException(ex.Message, ex);
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}

			return db;
		}

		/// <summary>
		/// Gets the value between the parantheses and quotation marks in the given string.
		/// Ex: Passing `VarScope("build")` would return `build`.
		/// </summary>
		/// <param name="line">String to parse.</param>
		/// <returns>Parsed value.</returns>
		public static string ParseValue(string line)
		{
			string parsedValue;

			// Replace quotation marks with '|'
			string formattedLine = line.Replace("\"", "|");

			bool useQuotations = false;
			if (line.Contains("|"))
			{
				useQuotations = true;
			}

			// Get the value of the string from within the parantheses/quotation marks
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

			// Remove any quotation marks and such from the final string
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

		/// <summary>
		/// Writes the DataBase to the specified file path.
		/// </summary>
		/// <param name="filePath">Path to write the file.</param>
		/// <exception cref="ObjectDisposedException"></exception>
		/// <exception cref="PathTooLongException"></exception>
		/// <exception cref="IOException"></exception>
		/// <exception cref="UnauthorizedAccessException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public void WriteToFile(string filePath)
		{
			try
			{
				StreamWriter sw = new StreamWriter(filePath, false, Encoding.ASCII);
				int curIndentLevel = 0;

				#region Methods

				/// <summary>
				/// Indents the given string based on the current indentation level.
				/// </summary>
				string Indent(int indentLevel)
				{
					string s = "";

					for (int i = 0; i < indentLevel; i++)
					{
						s += "  ";
					}

					return s;
				}

				/// <summary>
				/// Shortcut method to write the given string to the file with the appropriate level of indentation.
				/// </summary>
				void AddLine(string s)
				{
					sw.WriteLine(Indent(curIndentLevel) + s);
				}

				/// <summary>
				/// Writes the given Scope and its contents to the file.
				/// </summary>
				void ParseScope(Scope scope)
				{
					AddLine(string.Format("VarScope(\"{0}\")", scope.Name));
					AddLine("{");

					curIndentLevel++;

					// Parse any child scopes recursively
					foreach (Scope childScope in scope.Scopes)
					{
						ParseScope(childScope);
					}

					// Parse keys
					foreach (Key key in scope.Keys)
					{
						ParseKey(key);
					}

					curIndentLevel--;

					AddLine("}");
				}

				/// <summary>
				/// Writes the given Key and its contents to the file.
				/// </summary>
				void ParseKey(Key key)
				{
					AddLine(string.Format("VarBinary(\"{0}\")", key.Name));
					AddLine("{");
					curIndentLevel++;

					// Write 'Size' value
					AddLine(string.Format("Size({0});", key.Size));

					// Write each binary 'Value'
					foreach (string value in key.BinaryValues)
					{
						AddLine(string.Format("Value(\"{0}\");", value));
					}

					curIndentLevel--;
					AddLine("}");
				}

				#endregion Methods

				// OPEN DATABASE
				AddLine("DataBase()");
				AddLine("{");
				curIndentLevel++;

				// Write root-level Scopes
				foreach (Scope scope in Scopes)
				{
					ParseScope(scope);
				}

				// Write root-level Keys
				foreach (Key key in Keys)
				{
					ParseKey(key);
				}

				// CLOSE DATABASE
				curIndentLevel--;
				AddLine("}");

				// Done writing to the file
				sw.Close();
			}
			catch (ObjectDisposedException ex)
			{
				Trace.WriteLine(ex.Message);
				throw new ObjectDisposedException(ex.ObjectName, ex.Message);
			}
			catch (PathTooLongException ex)
			{
				Trace.WriteLine(ex.Message);
				throw new PathTooLongException(ex.Message, ex);
			}
			catch (IOException ex)
			{
				Trace.WriteLine(ex.Message);
				throw new IOException(ex.Message, ex);
			}
			catch (UnauthorizedAccessException ex)
			{
				Trace.WriteLine(ex.Message);
				throw new UnauthorizedAccessException(ex.Message, ex);
			}
			catch (System.Security.SecurityException ex)
			{
				Trace.WriteLine(ex.Message);
				throw new System.Security.SecurityException(ex.Message, ex);
			}
			catch (ArgumentNullException ex)
			{
				Trace.WriteLine(ex.Message);
				throw new ArgumentNullException(ex.Message, ex);
			}
			catch (ArgumentException ex)
			{
				Trace.WriteLine(ex.Message);
				throw new ArgumentException(ex.Message, ex);
			}
		}

		/// <summary>
		/// Try to get the Key at the specified localized key path.
		/// </summary>
		/// <param name="keyPath">Localized key path. Should be formatted the same way as in Lua scripts. Ex: "scope1.scope2.key"</param>
		/// <returns>Key at the specified localized key path.</returns>
		/// <exception cref="LocalizedKeyNotFoundException"></exception>
		public Key GetKey(string keyPath)
		{
			Key keyToFind = null;
			string[] splitPath = keyPath.Split('.');
			int level = 0;
			bool foundKey = false;

			#region Methods

			/// <summary>
			/// Checks if the given Scope matches the Scope name at the current level's path element. If not found, looks in any child Scopes.
			/// </summary>
			void GetScope(Scope scope)
			{
				// Does this Scope match the current level's path element?
				if (scope.Name == splitPath[level])
				{
					level++;

					// Are we at the final path element, aka the Key?
					if (level == splitPath.Length - 1)
					{
						// Try to find the Key matching the final path element
						foreach (Key key in scope.Keys)
						{
							if (key.Name == splitPath[level])
							{
								keyToFind = key;
								foundKey = true;
								break;
							}
						}
					}
					// Are there still more Scope levels?
					else if (level < splitPath.Length)
					{
						// Recursively go through any child Scopes
						foreach (Scope childScope in scope.Scopes)
						{
							GetScope(childScope);

							if (foundKey) break;
						}
					}
				}
			}

			#endregion Methods
			
			// Is this a root-level key?
			if (splitPath.Length == 1)
			{
				foreach (Key key in Keys)
				{
					if (key.Name == splitPath[0])
					{
						keyToFind = key;
						foundKey = true;
					}
				}
			}

			if (!foundKey)
			{
				// Start looking through the Scopes
				if (splitPath.Length > 1)
				{
					foreach (Scope scope in Scopes)
					{
						GetScope(scope);

						if (foundKey) break;
					}
				}
			}

			if (!foundKey)
			{
				throw new LocalizedKeyNotFoundException(string.Format("Localized key not found in localization file."), keyPath);
			}

			return keyToFind;
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

		/// <summary>
		/// Gets the value of the key without the two starting unicode characters.
		/// </summary>
		/// <returns>Key value.</returns>
		public string GetValue()
		{
			return Value.Substring(2);
		}

		/// <summary>
		/// Sets a new value for this Key. Automatically handles setting the binary Values and the Size.
		/// </summary>
		/// <param name="str">Readable string to save to the key.</param>
		public void SetValue(string str)
		{
			// TODO: need to figure out the behaviour of the first two unicode characters and add logic to make sure the Value always starts with the right ones
			/* NOTES: 
				 - Closing MLTool, reopening, and modifying the key seems to increment the first digit
				 - Closing MLTool, reopening, and NOT modifying the key (but still saving) does NOT increment the first digit
				 - Creating a new, blank key and saving: 0
				 - Creating a new, blank key, adding a character, and saving: 1
				 - It appears that several chars are cut off from the end of the string when these two unicode chars are left out
			*/

			// Set the readable value
			Value = "\u0000" + "\u0000" + str;

			// Calculate and set the new size
			Size = (Value.Length * 2).ToString();

			// Convert readable string to binary unicode
			BinaryValues.Clear();
			BinaryValues = StringExt.ConvertStringToUnicodeList(Value);
		}
	}

	public class LocalizedKeyNotFoundException: Exception
	{
		/// <summary>
		/// Path of localized key.
		/// </summary>
		public string KeyPath { get; set; }

		/// <summary>
		/// The exception thrown when the localized key is not found in the localization file.
		/// </summary>
		public LocalizedKeyNotFoundException()
		{
		}

		/// <summary>
		/// The exception thrown when the localized key is not found in the localization file.
		/// </summary>
		/// <param name="message">Exception message.</param>
		public LocalizedKeyNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// The exception thrown when the localized key is not found in the localization file.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="inner">Inner exception.</param>
		public LocalizedKeyNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// The exception thrown when the localized key is not found in the localization file.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="keyPath">Path of localized key.</param>
		public LocalizedKeyNotFoundException(string message, string keyPath)
			: base(message)
		{
			KeyPath = keyPath;
		}

		/// <summary>
		/// The exception thrown when the localized key is not found in the localization file.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="keyPath">Path of localized key.</param>
		/// <param name="inner">Inner exception.</param>
		public LocalizedKeyNotFoundException(string message, string keyPath, Exception inner)
			: base(message, inner)
		{
			KeyPath = keyPath;
		}
	}
}
