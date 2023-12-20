using System.Runtime.InteropServices;
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

        [TestCase("kiranK", "1234", "1234")]
        [TestCase("sa", "12", "12" )]
        [TestCase("BobDeBouwster", "1234", "1234")]
        [TestCase("LeroyRodmond", "ICT1379!", "ICT1379!")]
        [Test]
        public void CheckRegister_Fine_RetrunsTrue(string username, string password, string password2)
        {
            //Arrange
            var model = new SkeptaModel();

            //Act
            var (registerBool, registerString) = model.CheckRegister(username, password, password2);

            //Assert
            Assert.Multiple(() =>
                {
                    Assert.IsTrue(registerBool);
                    Assert.That(registerString.Length, Is.GreaterThan(10));
                });
        }

        [TestCase("kiranKkiranKkiranKkiranK", "1234", "1234")]
        [TestCase("", "12", "12")]
        [TestCase("BobDeBouwer", "1234", "1234")]
        [TestCase("LeroyRodmond", "ICT1379!", "")]
        [TestCase("LeroyRodmond", "", "ICT1379!")]
        [Test]
        public void CheckRegister_notFine_RetrunsFalse(string username, string password, string password2)
        {
            //Arrange
            var model = new SkeptaModel();

            //Act
            var (registerBool, registerString) = model.CheckRegister(username, password, password2);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(registerBool);
                Assert.That(registerString.Length, Is.GreaterThan(10));
            });
        }
        [Test]
        public void ObtainRandomText_Text_ReturnTextFromDatabase()
        {
            //Arrange 
            var model = new SkeptaModel();

            //Act 
            var randomText = model.ObtainRandomText();

            //Assert
            Assert.That(randomText.Length, Is.GreaterThan(15));
        }
    }
}