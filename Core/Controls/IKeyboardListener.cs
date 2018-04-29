using System.Windows.Forms;

namespace Core.Controls
{
    public interface IKeyboardListener
    {
        void HandleKeyDown(Keys key);
        void HandleKeyUp(Keys key);
    }
}