using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartWater.Processor
{
    public class WordAnalyzer
    {
        private readonly List<string> _words;
        private List<WordEntry> _allEntriesLongToShort;
        private Dictionary<string, WordEntry> _dictionary;

        private int _minWordLength;
        private int _minCompositeWordLength;

        private string[] _knownComposites =
        {
            "catsdogcats",
            "dogcatsdog",
            "ratcatdogcat"
        };

        public WordAnalyzer(List<string> words)
        {
            _words = words;
        }

        public Task Process()
        {
            return Task.Run(()=> {
                _allEntriesLongToShort = new List<WordEntry>();
                _dictionary = new Dictionary<string, WordEntry>();
                foreach(var word in _words.OrderByDescending(x => x.Length))
                {
                    if (string.IsNullOrWhiteSpace(word))
                        continue;

                    var entry = new WordEntry(word);
                    _dictionary.Add(word, new WordEntry(word));
                    _allEntriesLongToShort.Add(entry);
                }

                _minWordLength = _allEntriesLongToShort.Last().Value.Length;
                _minCompositeWordLength = _minWordLength * 2;

                foreach (var entry in _allEntriesLongToShort)
                    entry.IsComposite = IsCompositeWord(entry.Value);
            });
        }

        public List<WordEntry> AllEntries => _allEntriesLongToShort;

        private bool IsCompositeWord(string candidate, bool targetIsFullWord = true)
        {
            var minLength = targetIsFullWord ? _minCompositeWordLength : _minWordLength;
            if (candidate.Length < minLength) return false;

            var maxComponentLength = targetIsFullWord ? candidate.Length - _minWordLength : candidate.Length;

            for(var componentLength = _minWordLength; componentLength <= maxComponentLength; componentLength++)
            {
                var firstComponentCandidate = candidate.Substring(0, componentLength);
                if (_dictionary.ContainsKey(firstComponentCandidate))
                {
                    var remainder = candidate.Substring(firstComponentCandidate.Length);
                    if (remainder == string.Empty || IsCompositeWord(remainder, false))
                        return true;
                }
            }
            return false; 
        }
    }
}
