using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;
using Core.Game;
using Core.Objects;

namespace MoonLanding.Forms
{
    public class GameForm : Form
    {
        private Timer worldUpdateTimer;
        private Game game;
        private const int TimerInterval = 100 / 5; // 50 times per second

        private SoundPlayer enginePlayer;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Maximized;
//            FormBorderStyle = FormBorderStyle.FixedDialog;
//            MaximizeBox = false;
//            MinimizeBox = false;
        }

        public GameForm()
        {
            SetupUi();
            SetupSound();
            SetupTimer();
        }

        private void SetupSound()
        {
            enginePlayer = new SoundPlayer("../resources/engine.wav");
        }

        private void SetupUi()
        {
            var startButton = CreateStartButton();
            var loadButton = CreateLoadLevelButton();
            var helpButton = CreateHelpButton();
            var exitButton = CreateExitButton();
            
            var table = CreateLayoutTable();
            
            table.Controls.Add(startButton, 0, 0);
            table.Controls.Add(loadButton, 0, 1);
            table.Controls.Add(helpButton, 0, 2);
            table.Controls.Add(exitButton, 0, 3);

            table.Controls.Add(new Panel(), 0, 4);
            table.Controls.Add(new Panel(), 1, 1);
            table.Controls.Add(new Panel(), 1, 2);

            Controls.Add(table);
        }

        private TableLayoutPanel CreateLayoutTable()
        {
            var table = new TableLayoutPanel();

            const int buttonHeight = 25;
            const int buttonWidth = 70;

            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, buttonHeight));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, buttonHeight));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, buttonHeight));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, buttonHeight));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, buttonWidth));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            table.Dock = DockStyle.Fill;

            return table;
        }

        private Button CreateStartButton()
        {
            return new Button
            {
                Text = "Insert coin",
                Dock = DockStyle.Fill
            };
        }
        
        private Button CreateLoadLevelButton()
        {
            return new Button
            {
                Text = "Load level",
                Dock = DockStyle.Fill
            };
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

//        public void SetGame(Game game)
//        {
//            worldUpdateTimer.Stop();
//            
//            this.game = game;
//            image = new Bitmap(this.game.Level.Landscape.Size.Width, this.game.Level.Landscape.Size.Height);
//            
//            worldUpdateTimer.Start();
//        }
        
        private void SetupTimer()
        {
            worldUpdateTimer = new Timer() {Interval = TimerInterval};
            worldUpdateTimer.Tick += (sender, obj) => OnGameTimerTick();
        }

        private void OnGameTimerTick()
        {
            if (game == null)
                return;
            
            if (game.State != GameState.InProgress)
                return;

            game.Controller.ProvideTick(TimerInterval / 1000.0); // convert milliseconds to seconds
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if (game == null)
                return;
            
            if (game.State != GameState.InProgress)
                return;
            
            if (e.KeyCode == Keys.Up && !game.Level.Ship.EngineEnabled)
                enginePlayer.PlayLooping();
            
            game.Controller.ProvideKeyDown(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if (game == null)
                return;
            
            if (game.State != GameState.InProgress)
                return;
            
            if (e.KeyCode == Keys.Up)
                enginePlayer.Stop();
            
            game.Controller.ProvideKeyUp(e.KeyCode);
        }
    }
}