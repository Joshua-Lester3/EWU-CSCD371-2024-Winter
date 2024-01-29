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
        private static int _Counter = 0;

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

       [TestMethod]
        public void TellJoke_DoesNotContainChuckNorris_PrintsJoke()
        {
            SetOut();
            _Jokeable
                .Setup(x => x.GetJoke())
                .Returns("haha funny joke");

            Jester jester = new Jester(new OutputService(), _Jokeable.Object);
            jester.TellJoke();
            Assert.AreEqual<string>("haha funny joke", _NewOut!.ToString());
            ResetOut();
        }

        [TestMethod]
        public void TellJoke_ContainsChuckNorris_PrintsJoke()
        {
            SetOut();
            _Jokeable
                .Setup(x => x.GetJoke())
                .Returns(() =>
                {
                    if (JesterTests._Counter == 0)
                    {
                        JesterTests._Counter++;
                        return "haha funny joke about Chuck Norris";
                    } else
                    {
                        return "haha funny joke";
                    }
                });
            _Outputable
                .Setup(x => x.Output(It.IsAny<string>()))
                .Returns((string s) =>
                {
                    Console.Write(s);
                    return true;
                });

            Jester jester = new Jester(_Outputable.Object, _Jokeable.Object);
            jester.TellJoke();
            Assert.AreEqual<string>("haha funny joke", _NewOut!.ToString());
            ResetOut();
        }

        private void SetOut()
        {
            _OldOut = Console.Out;
            _NewOut = new StringWriter();
            Console.SetOut(_NewOut);
            Console.SetError(_NewOut);
        }

        private void ResetOut()
        {
            Console.SetOut(_OldOut!);
            Console.SetError(_OldOut!);
        }
    }
}
