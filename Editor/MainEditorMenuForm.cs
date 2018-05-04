using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Size = System.Drawing.Size;
using System.Drawing;

namespace Editor
{
    public class MainEditorMenuForm : Form
    {
        private readonly Button startButton;
        private readonly Button exitButton;

        public MainEditorMenuForm()
        {
            startButton = new Button { Text = "Start edit new level" };
            startButton.Click += (s, o) => RunEditor();
            Controls.Add(startButton);

            exitButton = new Button { Text = "Exit editor" };
            exitButton.Click += (s, o) => Close();
            Controls.Add(exitButton);

            SizeChanged += (s, o) =>
            {
                startButton.Location = new Point(0, 0);
                startButton.Size = new Size(ClientSize.Width, ClientSize.Height / 2);

                exitButton.Location = new Point(0, ClientSize.Height / 2);
                exitButton.Size = new Size(ClientSize.Width, ClientSize.Height / 2);
            };

            Load += (s, o) => OnSizeChanged(EventArgs.Empty);
        }

        private void RunEditor()
        {
            //TODO
        }
    }
}
