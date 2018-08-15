using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ZeroLocalizationTool.Modules
{
	public static class StringExt
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
}
