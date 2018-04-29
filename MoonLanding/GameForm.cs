using System.Windows.Forms;

namespace MoonLanding
{
    public class GameForm : Form
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            var k = e.KeyCode;
        }
    }
}