using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JesterTests
    {
        private Mock<IOutputable> _Outputable {  get; set; }
        private Mock<IJokeable> _Jokeable { get; set; }
        private TextWriter? _OldOut;
        private StringWriter? _NewOut;
        private OutputService? _Service;

        public JesterTests()
        {
            _Outputable = new Mock<IOutputable>();
            _Jokeable = new Mock<IJokeable>();
        }

        [TestMethod]
        public void JesterConstructor_NonNullParams_DoesNotThrowException()
        {
            try
            {
                Jester jester = new(_Outputable.Object, _Jokeable.Object);
            } catch (ArgumentNullException ex)
            {
                Assert.Fail(ex.Message);
            }
            return;
        }

        [TestMethod]
        public void JesterConstructor_NullIOutputable_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { Jester jester = new(null!, _Jokeable.Object); });
        }

        [TestMethod]
        public void JesterConstructor_NullIJokeable_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { Jester jester = new(_Outputable.Object, null!); });
        }

        //[TestMethod]
        //public void TellJoke_DoesNotContainChuckNorris_PrintsJoke()
        //{
        //    _Jokeable
        //        .Setup(x => x.GetJoke())
        //        .Returns("haha funny joke");

        //    Jester jester = new Jester(_Outputable.Object, _Jokeable.Object);
        //    Assert.IsTrue("haha funny joke", )
        //}
    }
}
