using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBallGame
{
    internal class Brick
    {
        private readonly int width = 100;
        private readonly int height = 20;
        private readonly int xVelocity = 10;
        public Rectangle DisplayRect;
        private Rectangle Canvas;

        public Brick(Rectangle canvas)
        {
            this.Canvas = canvas;
            int brickX = (canvas.Width / 2) - (int)(width / 2);
            int brickY = canvas.Top + (int)(canvas.Height * 0.1);
            DisplayRect = new Rectangle(brickX, brickY, width, height);
        }
        internal void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Red, DisplayRect);
        }




    }
}
