using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncyBallGame
{
    public partial class BouncyBallGame : Form
    {
        // Properties
        Paddle paddle;
        // Declare the ball object. 
        HashSet<Ball> balls = new HashSet<Ball>();
        int score = 0;
        int lives = 3;
        Brick brick;
        //Ball ball;
        private MciPlayer pop = new MciPlayer("sounds/pop.mp3", "1");
        public BouncyBallGame()
        {
            InitializeComponent();
        }

        private void BouncyBallGame_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //Creating the paddle here
            paddle = new Paddle(this.DisplayRectangle);
            //ball = new Ball(this.DisplayRectangle);
            balls.Add(new Ball(this.DisplayRectangle));
        }

        private void BouncyBallGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            //Paddle paddle = new Paddle(this.DisplayRectangle);
            paddle.Draw(graphics);            

            //ball.Draw(graphics);
            foreach (var ball in balls)
            {
                ball.Draw(graphics);
            }
            // Display number of balls created
            DisplayNumberOfBalls(graphics);
        }

        private void DisplayNumberOfBalls(Graphics graphics)
        {
            string message = $"Balls Number: {balls.Count}";
            Font font = new Font(FontFamily.GenericSansSerif, 25);
            graphics.DrawString(message, font,Brushes.Blue, 20, 20);

        }

        private void BouncyBallGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    paddle.Move(Paddle.Direction.Left);
                    paddle.Move(Paddle.Direction.Left);
                   
                    break;
                case Keys.Right:
                    paddle.Move(Paddle.Direction.Right);
                    paddle.Move(Paddle.Direction.Right);

                    break;
                case Keys.Space:
                    this.animationTimer.Start(); // to start the timer and perform the ball move
                    
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.N:
                    balls.Add(new Ball(this.DisplayRectangle));
                    break;

            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {

            //Removing any balls that have slipped off the main form
            balls.RemoveWhere(BallMissesPaddle);
            foreach (var ball in balls)
            {
                ball.Move();
            }
            if (balls.RemoveWhere(BallMissesPaddle) > 0)
            {
                pop.PlayFromStart();
                lives--;
                label1.Text = "Lives : " + lives;
                if (lives == 0)
                    lbl_lose.Enabled = true;
            }


            //ball.Move();
            //Check for any collison using this mwthod
            foreach (var ball in balls)
            {
                CheckForCollision(ball);

            }
            
            Invalidate(); // Refresh the form

        }
        /// <summary>
        /// The delgate which decide if any ball is to be removed
        /// </summary>
        /// <param name="ball"></param>
        /// <returns></returns>
        private bool BallMissesPaddle(Ball ball)
        {
            return (ball.currentY >= DisplayRectangle.Bottom);
        }

        /// <summary>
        /// The Collision Detection Method
        /// </summary>

        private void CheckForCollision(Ball ball)
        {
            // Check the Top side collision Detection 
            if (ball.currentY <= this.DisplayRectangle.Top)
            {
                ball.FlipY();
            }
            // Check the paddle collision Detection 
            else if (paddle.DisplayRect.IntersectsWith(ball.DisplayArea))
            {
                ball.FlipY();
            }
            foreach(Control control in this.Controls)
            {
                if (ball.DisplayArea.IntersectsWith(control.Bounds))
                {
                    if (!control.Equals(lbl_score) && !control.Equals(menuStrip1) && !control.Equals(lbl_lose) && !control.Equals(lbl_win))
                    {
                        Controls.Remove(control);
                        ball.FlipY();
                        score++;
                    }
                    lbl_score.Text = "Score : " + score;

                    if (score == 24)
                        lbl_win.Enabled = true;

                }
            }
            //To decrease the value of number of balls on the top of the main form of our game
            //else if (ball.currentY >= DisplayRectangle.Bottom)
            //{
            //    balls.Remove(ball);
            //}

            // Check the Left side collision Detection
            if (ball.currentX <= this.DisplayRectangle.Left)
            {
                ball.FlipX();
            }
            // Check the right collision Detection
            else if (ball.currentX >= (this.DisplayRectangle.Right-ball.Size))
            {
                ball.FlipX();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
