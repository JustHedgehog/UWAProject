using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWA_Projekt
{
    internal class SessionData
    {

        public SessionData(string score, string mode, string date)
        {
            this.Score = score;
            this.Mode = mode;
            this.Date = date;
        }

        public String Score { get; set; }
        public String Mode { get; set; }
        public String Date { get; set; }
    }
}
