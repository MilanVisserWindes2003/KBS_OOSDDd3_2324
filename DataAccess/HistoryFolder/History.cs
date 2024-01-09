using System.Data;

namespace Skepta.DataAcces.HistoryFolder
{
    //Class to represent a history row page on the 'geschiedenis page'
    public class History
    {
        public string TypeSpeed { get; set; }
        public char WorstMistake { get; set; }

        public bool IsSpoken { get; set; }

        public string TextId { get; set; }

        public string Username { get; set; }
       public DataTable HistoryTable { get; set; }
    }
}
