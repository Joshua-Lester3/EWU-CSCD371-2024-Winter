using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit.Sdk;
//using Xunit;


namespace CanHazFunny.Tests;

[TestClass]
public class JokeServiceTests
{
    #pragma warning disable CS8618 // We know Joke service will not be null because of [TestInitialize]
    private JokeService JokeService { get; set; }
    #pragma warning restore CS8618 // We know Joke service will not be null because of [TestInitialize]

    [TestInitialize]
    public void Initialize()
    {
        JokeService = new JokeService();
    }

    [TestMethod]
    public void GetJoke_NormalConditions_ReturnsNonEmptyString()
    {
        Assert.IsTrue(JokeService.GetJoke().Length > 0);
    }

    [TestMethod]
    public void GetJoke_ProperlyInitializedJokeService_ReturnsJoke_Successful()
    {
        Assert.IsNotNull(JokeService.GetJoke());
    }

    public JokeService GetJokeService()
    {
        return JokeService;
    }
}
