using System;

namespace CanHazFunny;

public class Jester
{
    private IOutputable Outputable { get; set; }
    private IJokeable Jokeable { get; set; }
    public Jester(IOutputable outputable, IJokeable jokeable)
    {
        ArgumentNullException.ThrowIfNull(outputable);
        ArgumentNullException.ThrowIfNull(jokeable);
        Outputable = outputable;
        Jokeable = jokeable;
    }

    public void TellJoke()
    {
        string newJoke;
        do
        {
<<<<<<< HEAD
            string newJoke;
            do
            {
                newJoke = jokeable.GetJoke();
            } while (newJoke.ToLower().Contains("chuck norris"));
            outputable.Output(newJoke);

        }

        public void TellJoke(String key)
        {
            TellJoke();
        }
=======
            newJoke = Jokeable.GetJoke();
        } while (newJoke.Contains("chuck norris", StringComparison.OrdinalIgnoreCase));
        Outputable.Output(newJoke);
>>>>>>> origin/Assignment3
    }
}
