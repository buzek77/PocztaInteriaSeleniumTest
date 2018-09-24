using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulcan.Common2015.SeleniumLib.Helpers
{
    public class LogQueue : Queue<string>
    {
        //TODO dorobic pobieranie z konfiga
        private int maxCount = 3;

        /// <summary>
        /// Wszystkie wprowadzone logi, rozdzielone znakiem nowej linii
        /// </summary>
        public string Logs
        {
            get { return string.Join(Environment.NewLine, base.ToArray()); }
        }

        public new void Enqueue(string msg)
        {
            if (base.Count == maxCount)
                base.Dequeue();

            base.Enqueue(msg);
        }

    }
}
