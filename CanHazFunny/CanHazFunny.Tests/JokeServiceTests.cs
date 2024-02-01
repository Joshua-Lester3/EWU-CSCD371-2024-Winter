using System.Net.Http;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        Assert.IsTrue(JokeService!.GetJoke().Length > 0);
    }

    [TestMethod]
    public void GetJoke_AssignedToIJokeable_ReturnsNonEmptyString()
    {
        // Arrange

        // Act
        string joke = JokeService!.GetJoke();

        // Assert
        Assert.IsTrue(joke.Length > 0);
    }

    public void GetJoke_InitializedJokeService_ReturnsJoke()
    {
        JokeService jokeService = new();
        Assert.IsNotNull(jokeService.GetJoke());
    }


}

