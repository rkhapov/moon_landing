using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;
using Core.Game;
using Core.Objects;

namespace MoonLanding.Forms
{
    public class LunarLandingForm : Form
    {
        private Game game;
        private const int TimerInterval = 100 / 4;
        private Timer timer;
        private Image image;

        private readonly Bitmap disableEngineShipImage;
        private readonly Bitmap enableEngineShipImage;
        private readonly SoundPlayer enginePlayer;
        private readonly SoundPlayer alarmPlayer;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public LunarLandingForm()
        {
            SetUpTimer();
            disableEngineShipImage = new Bitmap("../resources/ship_disabled.png");
            enableEngineShipImage = new Bitmap("../resources/ship_enabled.png");
            enginePlayer = new SoundPlayer("../resources/engine.wav");
            alarmPlayer = new SoundPlayer("../resources/alarm.wav");
            FormClosing += (s, o) => { enginePlayer.Stop(); enginePlayer.Dispose(); };
        }

        public void SetGame(Game game)
        {
            timer.Stop();

            this.game = game;
            image = new Bitmap(this.game.Level.Landscape.Size.Width, this.game.Level.Landscape.Size.Height);

            timer.Start();
        }

        private void ScreenUpdate()
        {
            if (game == null)
                return;

            Invalidate();
            Update();
        }

        private void SetUpTimer()
        {
            timer = new Timer() { Interval = TimerInterval };
            timer.Tick += (sender, obj) => OnGameTimerTick();
            timer.Tick += (sender, obj) => ScreenUpdate();
        }

        private void OnGameTimerTick()
        {
            game.Controller.ProvideTick(TimerInterval / 1000.0);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (game.State != GameState.InProgress)
                return;

            if (e.KeyCode == Keys.Up && !game.Level.Ship.EngineEnabled)
                enginePlayer.PlayLooping();

            game.Controller.ProvideKeyDown(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (game.State != GameState.InProgress)
                return;

            if (e.KeyCode == Keys.Up)
                enginePlayer.Stop();

            game.Controller.ProvideKeyUp(e.KeyCode);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Brushes.Bisque, ClientRectangle);
            DrawTo(Graphics.FromImage(image));
            pevent.Graphics.DrawImage(image, (ClientRectangle.Width - image.Width) / 2,
                (ClientRectangle.Height - image.Height) / 2);
        }

        private void DrawTo(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle(Brushes.Black, ClientRectangle);

            DrawLandscape(graphics);
            DrawShip(graphics);
            DrawInfo(graphics);
            if (game.State != GameState.InProgress)
                DrawStateString(graphics);
        }

        private void DrawStateString(Graphics graphics)
        {
            var x = game.Level.Landscape.Size.Width / 2;
            var y = game.Level.Landscape.Size.Height / 2;

            graphics.DrawString(GetGameStateString(),
                new Font(FontFamily.GenericSerif, 15),
                Brushes.AliceBlue,
                x, y);
        }

        private string GetGameStateString()
        {
            switch (game.State)
            {
                case GameState.Failed:
                    enginePlayer.Stop();
                    return "You crashed";
                case GameState.Success:
                    enginePlayer.Stop();
                    return "You landed successfully!";
                case GameState.InProgress:
                    return "In progress";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawLandscape(Graphics graphics)
        {
            var landscape = game.Level.Landscape;

            for (var i = 0; i < landscape.Size.Height; i++)
            {
                for (var j = 0; j < landscape.Size.Width; j++)
                {
                    if (landscape.GetCell(i, j) == LandscapeCell.Ground)
                        graphics.FillRectangle(Brushes.White, j, i, 1, 1);
                }
            }
        }

        private void DrawShip(Graphics graphics)
        {
            var ship = game.Level.Ship;

            var centerX = (float)(2 * ship.Cords.X + ship.Size.Width) / 2;
            var centerY = (float)(2 * ship.Cords.Y + ship.Size.Height) / 2;
            var angle = (float)(ship.Direction.Angle * 180 / Math.PI + 90);

            graphics.TranslateTransform(centerX, centerY);

            graphics.RotateTransform(angle);

            graphics.DrawImage(ship.EngineEnabled ? enableEngineShipImage : disableEngineShipImage,
                new Rectangle(-ship.Size.Width / 2, -ship.Size.Height / 2,
                    ship.Size.Width, ship.Size.Height));
            graphics.RotateTransform(-angle);

            graphics.TranslateTransform(-centerX, -centerY);
        }

        private void DrawInfo(Graphics graphics)
        {
            var ship = game.Level.Ship;
            var x = (int)ship.Cords.X > game.Level.Landscape.Size.Width / 2 ? 0 : game.Level.Landscape.Size.Width - 150;

            graphics.DrawString($"Fuel: {ship.Fuel:0.00}\n" +
                                $"Cords: {ship.Cords}\n" +
                                $"Velocity: {ship.Velocity}\n" +
                                $"Absolut velocity: {ship.Velocity.Length:0.00}\n" +
                                $"Physics: {game.Level.Physics.Name}\n" +
                                $"Direction: {ship.Direction}",
                new Font(FontFamily.GenericSerif, 10),
                Brushes.AliceBlue,
                x, 0);
        }
    }
}