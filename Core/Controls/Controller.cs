using System;
using System.Windows.Forms;

namespace Core.Controls
{
    public class Controller : IKeyboardProvider, ITimerProvider
    {
        public event Action<double> Tick;
        public event Action<Keys> KeyUp;
        public event Action<Keys> KeyDown;

        public void ProvideKeyDown(Keys key)
        {
            KeyDown?.Invoke(key);
        }

        public void ProvideKeyUp(Keys key)
        {
            KeyUp?.Invoke(key);
        }

        public void ProvideTick(double dt)
        {
            Tick?.Invoke(dt);
        }
    }
}