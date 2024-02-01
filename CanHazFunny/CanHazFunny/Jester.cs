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
    }
}
