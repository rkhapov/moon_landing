using System;
using System.Windows.Forms;

namespace Core.Controls
{
    public interface IController : IKeyboardProvider, ITimerProvider
    {
        event Action<double> Tick;
        event Action<Keys> KeyUp;
        event Action<Keys> KeyDown;
    }
}