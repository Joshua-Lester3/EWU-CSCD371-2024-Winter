using System;

namespace CanHazFunny
{
    public class Jester
    {
        public IOutputable outputable {  get; set; }
        public IJokeable jokeable { get; set; }
        public Jester(IOutputable outputable, IJokeable jokeable)
        {
            ArgumentNullException.ThrowIfNull(outputable);
            ArgumentNullException.ThrowIfNull(jokeable);
            this.outputable = outputable;
            this.jokeable = jokeable;
        }

        public void TellJoke()
        {
            string joke;
            do
            {
                joke = jokeable.GetJoke();
            } while (joke.ToLower().Contains("chuck norris"));
            outputable.Output(joke);
        }
    }
}
