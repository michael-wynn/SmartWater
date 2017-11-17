using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Smartwater.Processor
{
    public static class ListReader
    {
        public static async Task<List<string>> Read(string path)
        {
            var result = new List<string>();
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
