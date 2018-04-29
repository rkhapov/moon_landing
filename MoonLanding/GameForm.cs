﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using Core.Controls;
using Core.Game;
using Core.Objects;

namespace MoonLanding
{
    public class GameForm : Form
    {
        private Game game;
        private const int TimerInterval = 100 / 6;
        private Timer timer;
        private Image image;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
        }

        public GameForm()
        {
            SetUpTimer();
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
        }

        private void OnGameTimerTick()
        {
            game.Controller.ProvideTick(1000.0 / TimerInterval);
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
        
        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Bisque, ClientRectangle);
            DrawTo(Graphics.FromImage(image));
            e.Graphics.DrawImage(image, (ClientRectangle.Width - image.Width)/2, (ClientRectangle.Height - image.Height) / 2);
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
            graphics.FillRectangle(Brushes.Red, (float) ship.Cords.X, (float) ship.Cords.Y, ship.Size.Width,
                ship.Size.Height);
        }

        private void DrawInfo(Graphics graphics)
        {
            graphics.DrawString($"Fuel: {game.Level.Ship.Fuel}",
                new Font(FontFamily.GenericSerif, 10),
                Brushes.AliceBlue,
                0, 0);
        }
    }
}