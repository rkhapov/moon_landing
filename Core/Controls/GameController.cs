using System;
using System.Windows.Forms;

namespace Core
{
    public class GameController : IKeyboardListener, ITimerListener
    {
        public void HandleKeyDown(Keys key)
        {
            Console.WriteLine($"Game event: key down {key.ToString()}");
        }

        public void HandleKeyUp(Keys key)
        {
            Console.WriteLine($"Game event: key up {key.ToString()}");
        }

        public void HandleTick(double dt)
        {
            Console.WriteLine($"Game event: time tick {dt}");
        }
    }
}