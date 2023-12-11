using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skepta.DataAcces.HistoryFolder
{
    public class History
    {
        public string TypeSpeed { get; set; }
        public string WorstMistake { get; set; }

        public bool IsSpoken { get; set; }

        public string TextId { get; set; }

        public string Username { get; set; }
       public DataTable HistoryTable { get; set; }


        
        

       

    }
}
