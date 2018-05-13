using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Size = System.Drawing.Size;
using System.Drawing;
using System.Threading;

namespace Editor
{
    public class EditorForm : Form
    {
        private readonly Button selectLandscapeButton;
        private readonly TextBox selectedLandscapeFile;
        private readonly TextBox cordsTextBox;
        private readonly TextBox physicsTextBox;
        
        public EditorForm()
        {
            selectLandscapeButton = new Button
            {
                Text = "Select Landscape",
                Location = new Point(0, 25),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            selectLandscapeButton.Click += (s, o) => SelectLandscapeFile();
            Controls.Add(selectLandscapeButton);
            
            selectedLandscapeFile = new TextBox
            {
                Location = new Point(0, 0)
            };
            Controls.Add(selectedLandscapeFile);

            cordsTextBox = new TextBox
            {
                Text = "Enter ship cords",
                Location = new Point(0, 60)
            };
            Controls.Add(cordsTextBox);

            physicsTextBox = new TextBox
            {
                Text = "Enter physics name",
                Location = new Point(0, 100)
            };
            Controls.Add(physicsTextBox);
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void SelectLandscapeFile()
        {
            var fileName = string.Empty;

            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = ".",
                Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                fileName = openFileDialog.FileName;

            if (string.IsNullOrEmpty(fileName)) 
                return;
            
            selectedLandscapeFile.Text = fileName;
//            selectedLandscapeFile.Scale(new SizeF(selectedLandscapeFile.TextLength, 10));
        }
    }
}
