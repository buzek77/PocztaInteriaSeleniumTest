using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    public class VField : IEquatable<VField>
    {
        public string Label { get; set; }

        public string Value { get; set; }

        private string CleanFromInvisibleChars(string value)
        {
            string[] chars = {"\n","\r"};
            return chars.Aggregate(value, (current, c) => current.Replace(c, "")).Trim();
        }

        public bool Equals(VField other)
        {

            if (CleanFromInvisibleChars(other.Label) == CleanFromInvisibleChars(Label) && CleanFromInvisibleChars(other.Value) == CleanFromInvisibleChars(Value))
                return true;
            return false;
        }

        public override string ToString()
        {
            return CleanFromInvisibleChars(Label) + " " + CleanFromInvisibleChars(Value);
        }
    }
}
