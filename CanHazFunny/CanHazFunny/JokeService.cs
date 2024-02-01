using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace CanHazFunny;
public class JokeService : IJokeable
{
    private HttpClient HttpClient { get; } = new();

    public string GetJoke()
    {
        string joke = HttpClient.GetStringAsync("https://geek-jokes.sameerkumar.website/api?format=json").Result;
        JSONRoot? root = JsonConvert.DeserializeObject<JSONRoot>(joke);
        return root?.Joke ?? throw new ArgumentNullException(nameof(root));
    }
}
