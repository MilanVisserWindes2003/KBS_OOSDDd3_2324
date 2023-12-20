using DataAccess;
using Skepta.DataAcces.HistoryFolder;

namespace SkeptaTests
{
    public class DataAccessStub : IDataAccess
    {
        public List<string> AcceptedUsernames { get;} = new List<string>();
        public string GetPassword(string username)
        {
            throw new NotImplementedException();
        }

        public void InsertHistoryData(History history)
        {
            throw new NotImplementedException();
        }

        public History ObtainHistory(string username)
        {
            throw new NotImplementedException();
        }

        public string ObtainTextId(string level, string length, string content)
        {
            throw new NotImplementedException();
        }

        public List<string> ObTainTexts(string level, int length)
        {
            throw new NotImplementedException();
        }

        public bool Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool UsernameExists(string username)
        {
            return false;
        }
    }
}