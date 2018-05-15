using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Core.Controls;
using Core.Game;
using Core.Objects;
using Core.Physics;
using Core.Tools;
using Size = System.Drawing.Size;

namespace MoonLanding.Forms
{
    public class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            SetupUi();
        }

        private void SetupUi()
        {
            var table = CreateLayoutTable();

            table.Controls.Add(CreateStartButton(), 0, 0);
            table.Controls.Add(CreateHelpButton(), 0, 1);
            table.Controls.Add(CreateExitButton(), 0, 2);

            Controls.Add(table);
        }

        private TableLayoutPanel CreateLayoutTable()
        {
            var table = new TableLayoutPanel();

            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            table.Dock = DockStyle.Fill;

            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            return table;
        }

        private Button CreateStartButton()
        {
            var start = new Button
            {
                Text = "Insert coin",
                Dock = DockStyle.Fill
            };

            start.Click += (s, o) => RunGame();

            return start;
        }

        private Button CreateHelpButton()
        {
            var helpButton = new Button
            {
                Text = "Help",
                Dock = DockStyle.Fill
            };

            helpButton.Click += (s, o) =>
            {
                MessageBox.Show(this,
                    @"
This is the lunar landing game
Use left and right arrows to rotate ship
Use up arrow to accelerate ship",
                    "Help",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };

            return helpButton;
        }

        private Button CreateExitButton()
        {
            var exitButton = new Button
            {
                Text = "Exit",
                Dock = DockStyle.Fill
            };
            exitButton.Click += (s, e) => Close();

            return exitButton;
        }

        private void RunGame()
        {
//            try
//            {
                var game = InitializeGame();

                var gameForm = new LunarLandingForm();

                gameForm.SetGame(game);
                gameForm.Closed += (s, o) => Show();
                gameForm.Show();

                Hide();
//            }
//            catch
//            {
//                // no file inputed or other error
//            }
        }

        private Game InitializeGame()
        {
            return new Game(GetLevel(), new Controller());
        }

        private static Level GetLevel()
        {
            var levelInfo = LevelInfo.CreateFromFile(GetLevelFile());

            return levelInfo.BuildLevel();
        }

        private static string GetLevelFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = ".",
                Filter = "Level File(*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;

            throw new FormatException();
        }
    }
}