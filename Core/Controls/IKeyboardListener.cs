using System.Windows.Forms;

namespace Core
{
    public interface IKeyboardListener
    {
        void HandleKeyDown(Keys key);
        void HandleKeyUp(Keys key);
    }
}