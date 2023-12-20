using Skepta.DataAcces.HistoryFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataAccess
    {
        List<string> ObTainTexts(string level, int length);
        bool UsernameExists(string username);
        string GetPassword(string username);
        bool Register(string username, string password);
        string ObtainTextId(string level, string length, string content);
        History ObtainHistory(string username);
        void InsertHistoryData(History history);

    }
}
