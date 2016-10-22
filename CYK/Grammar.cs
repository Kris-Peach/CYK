using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYK
{
    class Grammar
    {
        public string Generator
        {
            get;
            set;
        }
        public string Begotten
        {
            get;
            set;
        }
        public Grammar(string Gen, string Begot)
        {
            Generator = Gen;
            Begotten = Begot;
        }
    }
}
