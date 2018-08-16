using System;
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
			string path = @"J:\BF2_ModTools\data_LCT\Common\Localize\english.cfg";
			string path2 = @"J:\BF2_ModTools\data_TCW\data_TCW\Common\Localize\english.cfg";

			DataBase db = new DataBase();
			db = LocalizationParser.ParseDataBase(path2);
			db.WriteToFile(@"J:\BF2_ModTools\data_LCT\Common\english3.cfg");

			//string testString = "\0\0TCW_Dev_Build_30813/01\r\nPROPERTY OF FRAYED WIRES STUDIOS\r\nDO NOT DISTRIBUTE";
			//List<string> testBinaryList = StringExt.ConvertStringToUnicodeList(testString);
			//foreach (string s in testBinaryList)
			//{
			//	Console.WriteLine(s);
			//}
			//Console.WriteLine("\n\nSize: " + testString.Length * 2);
			//Console.WriteLine("\n\n" + StringExt.ConvertUnicodeListToString(testBinaryList));

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
}
