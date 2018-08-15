﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZeroLocalizationTool.Modules;

namespace ZeroLocalizationTool
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"J:\BF2_ModTools\data_LCT\Common\english2.cfg";
			string path2 = @"J:\BF2_ModTools\data_TCW\data_TCW\Common\Localize\english.cfg";

			DataBase db = new DataBase();
			db = LocalizationParser.ParseDataBase(path2);

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
	}

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