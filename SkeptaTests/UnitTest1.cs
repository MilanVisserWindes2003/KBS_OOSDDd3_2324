using Skepta.Business;

namespace SkeptaTests
{
    
    public class SkeptaModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("kiran", "123")]
        [TestCase("s", "1")]
        [TestCase("BobDeBouwer", "1234")]
        [TestCase("LeroyRodermond", "ICT1379!")]
        [Test]
        public void CheckLogin_NotRight_RetrunsTrue(string username, string password)
        {
            //Arrange
            var model = new SkeptaModel();

            //Act
            var login = model.CheckLogin(username, password);
            //Assert
            Assert.IsTrue(login);
        }

        [TestCase("kiran", "1234")]
        [TestCase("s", "12")]
        [TestCase("BobDeBouwer", "123411")]
        [TestCase("LeroyRodermond", "ICT1379!2123")]
        [Test]
        public void CheckLogin_NotRight_RetrunsFalse(string username, string password)
        {
            //Arrange
            var model = new SkeptaModel();

            //Act
            var login = model.CheckLogin(username, password);
            //Assert
            Assert.IsFalse(login);
        }
    }
}