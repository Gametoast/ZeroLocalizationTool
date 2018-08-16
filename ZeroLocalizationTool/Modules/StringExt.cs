using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ZeroLocalizationTool.Modules
{
	public static class StringExt
	{
		/// <summary>
		/// Takes the substring of a string based on a start and end position instead of length.
		/// </summary>
		/// <param name="value">String to take the substring from.</param>
		/// <param name="startIndex">Index of the character in the string where the substring should start.</param>
		/// <param name="endIndex">Index of the character in the string where the substring should end.</param>
		/// <returns>Substring of the specified string.</returns>
		public static string SubstringIdx(this string value, int startIndex, int endIndex)
		{
			if (value == null) throw new ArgumentNullException();
			if (endIndex > value.Length) throw new IndexOutOfRangeException("End index must be less than or equal to the length of the string.");
			if (startIndex < 0 || startIndex > value.Length + 1) throw new IndexOutOfRangeException("Start index must be between zero and the length of the string minus one");
			if (startIndex >= endIndex) throw new ArgumentOutOfRangeException("Start index must be less than end index");

			var length = endIndex - startIndex;
			return value.Substring(startIndex, length);
		}

		/// <summary>
		/// Reverses a string.
		/// Ex: "abcd" would become "dcba".
		/// </summary>
		/// <param name="s">String to reverse.</param>
		/// <returns>Reversed string.</returns>
		public static string Reverse(string s)
		{
			char[] charArray = s.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}

		/// <summary>
		/// Converts a list of binary strings (each item being 64 unicode characters, or 16 string characters when parsed) to a single, readable string.
		/// </summary>
		/// <param name="s">List of binary strings to convert.</param>
		/// <returns>Readable string.</returns>
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

		public static List<string> ConvertStringToUnicodeList(string s)
		{
			List<string> list = new List<string>();
			char[] chars = s.ToCharArray();

			return list;
		}

		/// <summary>
		/// Splits the specified string into chunks of the specified size. 
		/// </summary>
		/// <param name="str">String to split into chunks.</param>
		/// <param name="maxChunkSize">Max number of characters per split string.</param>
		/// <returns>IEnumerable collection of the split string.</returns>
		public static IEnumerable<string> Split(string str, int maxChunkSize)
		{
			for (int i = 0; i < str.Length; i += maxChunkSize)
				yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
		}
	}
}
