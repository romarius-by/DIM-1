using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.BusinessModels
{
    public class WordsAmount
    {
        private readonly char[] delimiters = new char[] { ' ', '\r', '\n', ',', '.', ':' };
        private string _val = String.Empty;

        public WordsAmount(string val)
        {
            _val = val;
        }

        public string Value => _val;

        public int GetWordsAmount => _val.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
