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
            newJoke = Jokeable.GetJoke();
        } while (newJoke.ToLower().Contains("chuck norris"));
        Outputable.Output(newJoke);
    }


    
}
