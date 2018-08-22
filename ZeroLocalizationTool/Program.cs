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
			Dictionary<string, string[]> parsedArgs = new Dictionary<string, string[]>();

			// Parse command line arguments
			for (int i = 0; i < args.Length; i++)
			{
				switch (args[i])
				{
					case "-f":
					case "--file":
						parsedArgs.Add("file", new string[] { args[i + 1] });
						break;

					case "-sv":
					case "--set-value":
						parsedArgs.Add("set-value", new string[] { args[i + 1], args[i + 2] });
						break;

					case "-gv":
					case "--get-value":
						parsedArgs.Add("get-value", new string[] { args[i + 1] });
						break;
				}
			}

			//parsedArgs["file"][0] = @"J:\BF2_ModTools\data_LCT\Common\Localize\english.cfg";
			//parsedArgs["file"][0] = @"J:\BF2_ModTools\data_TCW\data_TCW\Common\Localize\english.cfg";
			//parsedArgs["file"][0] = @"J:\BF2_ModTools\data_LCT\Common\Localize\english.cfg";

			if (parsedArgs.ContainsKey("file"))
			{
				DataBase db = new DataBase();
				db = LocalizationParser.ParseDataBase(parsedArgs["file"][0]);


				if (parsedArgs.ContainsKey("set-value"))
				{
					Key key = db.GetKey(parsedArgs["set-value"][0]);
					key.SetValue(parsedArgs["set-value"][1]);

					db.WriteToFile(parsedArgs["file"][0]);
				}


				if (parsedArgs.ContainsKey("get-value"))
				{
					Key key = db.GetKey(parsedArgs["get-value"][0]);

					Console.WriteLine(string.Format("Value of key at path '{0}': \n{1}", parsedArgs["get-value"][0], key.Value));

					// Exit the application
					Console.WriteLine("\n" + "Press any key to exit.");
					Console.ReadKey();
				}



				//Key testrootkey = db.GetKey("testrootkey");
				//testrootkey.SetValue("new test value");

				//db.WriteToFile(parsedArgs["file"][0]);
				//Console.WriteLine(StringExt.ConvertUnicodeListToString(db.GetKey("testrootkey").BinaryValues));

				//string testString = "\u0002";
				//Console.WriteLine(testString);

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
			}
		}
	}
}
