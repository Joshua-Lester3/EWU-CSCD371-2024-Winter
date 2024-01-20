using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger.Tests;

[TestClass]
public class LogFactoryTests
{
    [TestMethod]
    public void CreateLogger_NonNullInputString_ReturnsBaseLogger()
    {
        LogFactory logFactory = new LogFactory();
        BaseLogger logger = logFactory.CreateLogger("Jeff");
        Assert.IsTrue(logger is BaseLogger);
    }
}
