﻿using System.Net.Http;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JokeServiceTests
    {
        private IJokeable? _Jokeable;

        [TestInitialize]
        public void Initialize()
        {
            _Jokeable = new JokeService();
        }

        [TestMethod]
        public void GetJoke_NormalConditions_ReturnsNonEmptyString()
        {
            Assert.IsTrue(_Jokeable!.GetJoke().Length > 0);
        }

        [TestMethod]
        public void GetJoke_AssignedToIJokeable_ReturnsNonEmptyString()
        {
            // Arrange

            // Act
            string joke = _Jokeable!.GetJoke();

            // Assert
            Assert.IsTrue(joke.Length > 0);
            Assert.AreEqual("hi", joke);
        }

        public void GetJoke_InitializedJokeService_ReturnsJoke()
        {
            JokeService jokeService = new();
            Assert.IsNotNull(jokeService.GetJoke());
        }

        
    }
}
