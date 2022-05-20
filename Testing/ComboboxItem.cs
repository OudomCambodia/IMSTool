using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public ComboboxItem(string t, string v)
        {
            Text = t; Value = v;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    class ComboboxMultiVal
    {
        public string Text { get; set; }
        public Dictionary<string,string> Value { get; set; }

        public ComboboxMultiVal(string t, Dictionary<string,string> v)
        {
            Text = t; Value = v;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
