using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JokeServiceTests
    {
        private JokeService? _JokeService;

        [TestInitialize]
        public void Initialize()
        {
            _JokeService = new();
        }

        [TestMethod]
        public void GetJoke_NormalConditions_ReturnsNonEmptyArray()
        {
            Assert.AreEqual("hi", _JokeService!.GetJoke());
            Assert.IsTrue(_JokeService!.GetJoke().Length > 0);
        }


    }
}
