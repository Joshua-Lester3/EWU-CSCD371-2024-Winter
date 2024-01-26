using System.IO;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class OutputServiceTests
    {
        private TextWriter? _OldOut;
        private StringWriter? _NewOut;
        private OutputService? _Service;
        [TestInitialize] public void Init() 
        {
            _OldOut = Console.Out;
            _NewOut = new StringWriter();
            Console.SetOut(_NewOut);
            Console.SetError(_NewOut);
            _Service = new OutputService();
        }

        [TestCleanup] public void Cleanup()
        {
            Console.SetOut(_OldOut!);
            Console.SetError(_OldOut!);
        }

        [TestMethod]
        public void Output_PassInNonNullString_PrintsStringToConsole()
        {
            // Arrange

            // Act
            _Service!.Output("Hello Jeff!");

            // Assert
            Assert.AreEqual("Hello Jeff!", _NewOut!.ToString());
        }

        [TestMethod]
        public void Output_PassInNullString_ThrowsException()
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => _Service!.Output(null!));
        }
    }
}
