using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Core.Game;
using Core.Objects;

namespace MoonLanding.Forms
{
    public class ScreenForm : Form
    {
        private Game game;
        private const int TimerInterval = 100 / 3; // 30 times per second
        private Image image;
        private Timer updateTimer;
        
        private readonly Bitmap disableEngineShipImage;
        private readonly Bitmap enableEngineShipImage;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
        }
        
        public ScreenForm(Game game)
        {
            this.game = game;
            Size = new Size(this.game.Level.Landscape.Size.Width, this.game.Level.Landscape.Size.Height);
            image = new Bitmap(this.game.Level.Landscape.Size.Width, this.game.Level.Landscape.Size.Height);
            
            SetupTimer();
            
            disableEngineShipImage = new Bitmap("../resources/ship_disabled.png");
            enableEngineShipImage = new Bitmap("../resources/ship_enabled.png");
        }

        private void ScreenUpdate()
        {
            if (game == null)
                return;
            
            Invalidate();
            Update();
        }
        
        private void SetupTimer()
        {
            updateTimer = new Timer() {Interval = TimerInterval};
            updateTimer.Tick += (sender, obj) => ScreenUpdate();
        }
        
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Brushes.Bisque, ClientRectangle);
            DrawTo(Graphics.FromImage(image));
            pevent.Graphics.DrawImage(image, 0, 0);
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
                return "You crashed";
                break;
            case GameState.Success:
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
            var angle = (float) (ship.Direction.Angle * 180 / Math.PI + 90);
            
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