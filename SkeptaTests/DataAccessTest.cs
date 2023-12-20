using DataAccess;
using NUnit.Framework;
using System.Collections.Generic;

namespace SkeptaTests
{
    //Check if I can reach database
    [Explicit]
    public class DataAccessTest
    {
        private IDataAccess dataAccess;

        [SetUp]
        public void Setup()
        {
            dataAccess = new DataAccess.DataAccess();
        }

        [TestCase(2, "a1")]
        [TestCase(1, "a2")]
        [TestCase(3, "b1")]
        [TestCase(4, "b2")]
        [TestCase(4, "c1")]
        [TestCase(5, "c2")]
        public void ObtainTextsTestSucceeds(int length, string level)
        {
            List<string> texts = dataAccess.ObTainTexts(level, length);
            Assert.IsNotNull(texts);
            Assert.True(texts.Count > 0);
        }

        [TestCase(-1, "xx")]
        [TestCase(0, "ee")]
        [TestCase(6, "bs")]
        [TestCase(24, "Invalid")]
        public void ObtainTextsTestFails(int length, string level) 
        {
            List<string> texts = dataAccess.ObTainTexts(level, length);
            Assert.IsNotNull(texts);
            Assert.True(texts.Count == 0);
        }

        [TestCase("s")]
        [TestCase("kiran")]
        public void UserNameExistsSucceeds(string name)
        {
            Assert.True(dataAccess.UsernameExists(name));
        }

        [TestCase("INVALID")]
        [TestCase("NotAUsername")]
        public void UserNameExistsFails(string name)
        {
            Assert.False(dataAccess.UsernameExists(name));
        }

        public void RegisterAccount(string name, string password)
        {

        }
    }
}
