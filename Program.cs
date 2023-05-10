using System;
using ClientLib;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!!!");

            string user = "user";
            string url = "http://localhost:3030";
            string context = "add";

            UpgradeClient upgrade = new UpgradeClient(user, url, context);
            var initResponse = await upgrade.Init();
            Console.WriteLine("Init Response on app: {0}", initResponse);
        }
    }
}