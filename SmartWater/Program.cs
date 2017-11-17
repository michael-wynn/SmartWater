using Smartwater.Processor;
using SmartWater.Processor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWater
{
    class Program
    {
        static int Main(string[] args)
        {
            int returnCode;
            try
            {
                MainAsync(args).GetAwaiter().GetResult();
                returnCode = 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                returnCode = -1;
            }
            Console.Write("Press any key to finish");
            Console.ReadKey();
            return returnCode;
        }


        static async Task MainAsync(string[] args)
        {
            var fileName = "wordlist.txt";
            var path = Path.Combine(Environment.CurrentDirectory, "Data", fileName);
            List<string> rawList = await ListReader.Read(path);
            var analyzer = new WordAnalyzer(rawList);
            await analyzer.Process();

            var composites = analyzer.AllEntries
                .Where(x => x.IsComposite)
                .OrderByDescending(x => x.Length).ToArray();

            if (composites.Length > 0)
                Console.WriteLine($"The longest word made up of other words is: {composites[0].Value}");


            if (composites.Length > 1)
                Console.WriteLine($"The 2nd longest word made up of other words is: {composites[1].Value}");

            Console.WriteLine($"The file {fileName} contains {composites.Length} such composite words");

        }

    }
}
