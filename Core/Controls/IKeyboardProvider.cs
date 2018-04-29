using System.Windows.Forms;

namespace Core.Controls
{
    public interface IKeyboardProvider
    {
        void ProvideKeyDown(Keys key);
        void ProvideKeyUp(Keys key);
    }
}