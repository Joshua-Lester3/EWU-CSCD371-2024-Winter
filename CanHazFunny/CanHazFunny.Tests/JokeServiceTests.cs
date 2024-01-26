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
        public void GetJoke_NormalConditions_ReturnsNonEmptyString()
        {
            Assert.IsTrue(_JokeService!.GetJoke().Length > 0);
        }

        [TestMethod]
        public void GetJoke_AssignedToIJokeable_ReturnsNonEmptyString()
        {
            // Arrange
            IJokeable jokeable = new JokeService();

            // Act
            string joke = jokeable.GetJoke();

            // Assert
            Assert.IsTrue(joke.Length > 0);
        }
    }
}
