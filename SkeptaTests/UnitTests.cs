using System.Runtime.InteropServices;
using Skepta.Business;
using Skepta.Business.ResultsLogic;

namespace SkeptaTests
{
    
    public class SkeptaModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("kiran", "1234")]
        [TestCase("s", "1")]
        [TestCase("BobDeBouwer", "1234")]
        [TestCase("LeroyRodermond", "ICT1379!")]
        [Test]
        public void CheckLogin_NotRight_ReturnsTrue(string username, string password)
        {
            //Arrange
            var model = new SkeptaModel();

            //Act
            var login = model.CheckLogin(username, password);
            //Assert
            Assert.IsTrue(login);
        }

        [TestCase("kiran", "123")]
        [TestCase("s", "12")]
        [TestCase("BobDeBouwer", "123411")]
        [TestCase("LeroyRodermond", "ICT1379!2123")]
        [Test]
        public void CheckLogin_NotRight_ReturnsFalse(string username, string password)
        {
            //Arrange
            var model = new SkeptaModel();

            //Act
            var login = model.CheckLogin(username, password);
            //Assert
            Assert.IsFalse(login);
        }

       
    }
    [TestFixture]
    public class ResultsLogicTests
    {
        [SetUp]
        public void Setup() { }


        [TestCase("Ik ben Anna. Ik hou van bloemen.", 7)]
        [TestCase("Mijn naam is Peter. Ik ben 25 jaar oud. Ik werk in een winkel.", 14)]
        [TestCase("De opkomst van automatisering en kunstmatige intelligentie heeft de aard van werk ingrijpend veranderd. Terwijl technologische vooruitgang nieuwe mogelijkheden creëert, brengt het ook uitdagingen met zich mee, zoals werkloosheid en heropleiding van werknemers. Het vormgeven van de toekomst van werk vereist een holistische benadering, waarbij technologische innovatie wordt gecombineerd met beleidsmaatregelen die sociale rechtvaardigheid en inclusiviteit bevorderen.", 57)]
        [Test]
        public void WordCounting_returnsInt(string randomText, int expectedWords)
        {
            //Arrange
            var rsl = new ResultsLogic();

            //Act
            var CountedWords = rsl.WordCounting(randomText);

            //Assert
            Assert.AreEqual(expectedWords, CountedWords);
        }
    }
}