using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SmartWater.Processor;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow("tinylist.txt", "ratcatdogcattle", "ratcatdogcat", 4)]
        [DataRow("wordlist.txt", "ethylenediaminetetraacetates", "electroencephalographically", 97107)]
        public async Task Test(
            string fileName, 
            string longestWordExpected, 
            string secondLongestWordExpected, 
            int compositeWordCountExpected)
        {
            List<string> rawList = await ReadRawWordList(fileName);

            var analyzer = new WordAnalyzer(rawList);
            await analyzer.Process();

            var composites = analyzer.AllEntries
                .Where(x => x.IsComposite)
                .OrderByDescending(x => x.Length).ToArray();

            var longestWordActual = composites.Length > 0
                ? composites[0].Value : null;
            Assert.AreEqual(longestWordExpected, longestWordActual, "Longest word detection check");

            var secondLongestWordActual = composites.Length > 1
                ? composites[1].Value : null;
            Assert.AreEqual(secondLongestWordExpected, secondLongestWordActual, "Second longest word detection check");

            Assert.AreEqual(compositeWordCountExpected, composites.Length, "Composite word count check");
        }

        private async Task<List<string>> ReadRawWordList(string fileName)
        {
            var result = new List<string>();
            var path = Path.Combine(Environment.CurrentDirectory, "Data", fileName);

            using (var stream = new FileStream(path, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = await reader.ReadLineAsync();
                        if (!string.IsNullOrWhiteSpace(line))
                            result.Add(line.Trim());
                    }
                }
            }
            return result;
        }
    }
}
