using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartWater.Processor
{
    public class WordEntry
    {
        private int _length;
        private string _value;
        

        public WordEntry(string value)
        {
            _value = value;
            _length = _value.Length;
        }

        public string Value => _value;
        public int Length => _length;
        public bool IsComposite { get; set; }
    }
}
