using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JesterTests
    {
        private IOutputable? _Outputable {  get; set; }
        private IJokeable? _Jokeable { get; set; }

        [TestInitialize] public void Init()
        {
            _Outputable = new OutputService();
            _Jokeable = new JokeService();
        }

        [TestMethod]
        public void JesterConstructor_NonNullParams_DoesNotThrowException()
        {
            IOutputable outputable = new OutputService();
            IJokeable jokeable = new JokeService();
            try
            {
                Jester jester = new(outputable, jokeable);
            } catch (ArgumentNullException ex)
            {
                Assert.Fail(ex.Message);
            }
            return;
        }

        [TestMethod]
        public void JesterConstructor_NullIOutputable_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { Jester jester = new(null!, new JokeService()); });
        }

        [TestMethod]
        public void JesterConstructor_NullIJokeable_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { Jester jester = new(new OutputService(), null!); });
        }
    }
}
