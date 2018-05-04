using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Core.Game;
using Core.Objects;

namespace MoonLanding.Forms
{
    public class GameForm : Form
    {
        private Game game;
        private const int TimerInterval = 100 / 6;
        private Timer timer;
        private Image image;

        private readonly Bitmap disableEngineShipImage;
        private readonly Bitmap enableEngineShipImage;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public GameForm()
        {
            SetUpTimer();
            disableEngineShipImage = new Bitmap("ship_disabled.png");
            enableEngineShipImage = new Bitmap("ship_enabled.png");

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
            timer = new Timer() {Interval = TimerInterval};
            timer.Tick += (sender, obj) => OnGameTimerTick();
            timer.Tick += (sender, obj) => ScreenUpdate();
            timer.Tick += (sender, obj) => GameStateCheck();
        }

        private void GameStateCheck()
        {
            if (game == null)
                return;

            if (game.State != GameState.InProgress)
                Close();
        }

        private void OnGameTimerTick()
        {
            game.Controller.ProvideTick(TimerInterval / 1000.0);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            game.Controller.ProvideKeyDown(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
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

            if (game == null)
                return;

            DrawLandscape(graphics);
            DrawShip(graphics);
            DrawInfo(graphics);
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
            
            var centerX = (float)(2 * ship.Cords.X + ship.Size.Width) / 2.0f;
            var centerY = (float)(2 * ship.Cords.Y + ship.Size.Height) / 2.0f;
            
            graphics.TranslateTransform(centerX, centerY);
            graphics.RotateTransform((float)ship.Direction.Angle);
            
            graphics.DrawImage(ship.EngineEnabled ? enableEngineShipImage : disableEngineShipImage,
                new Rectangle(-ship.Size.Width / 2, -ship.Size.Height / 2,
                    ship.Size.Width, ship.Size.Height));
            graphics.RotateTransform(-(float)ship.Direction.Angle);    
            
            graphics.TranslateTransform(-centerX, -centerY);
        }

        private void DrawInfo(Graphics graphics)
        {
            var ship = game.Level.Ship;
            graphics.DrawString($"Fuel: {ship.Fuel:0.00}\n" +
                                $"Cords: {ship.Cords}\n" +
                                $"Velocity: {ship.Velocity}\n" +
                                $"Absolut velocity: {ship.Velocity.Length:0.00}\n" +
                                $"Physics: {game.Level.Physics.Name}\n" +
                                $"Direction: {ship.Direction}",
                new Font(FontFamily.GenericSerif, 10),
                Brushes.AliceBlue,
                0, 0);
        }
    }
}