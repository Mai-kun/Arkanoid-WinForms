using Arkanoid.Contracts.Models;
using Arkanoid.GameMaster;
using Arkanoid.Properties;
using Timer = System.Windows.Forms.Timer;

namespace Arkanoid
{
    public partial class GameForm : Form
    {
        private readonly GameDrawer gameDrawer;
        private readonly GameManager gameManager;

        private int heartCount = 3;
        private Timer timer1;
        private Brick brick;
        private Platform platform;
        private Ball ball;
        private Heart heart;

        public GameForm()
        {
            InitializeComponent();

            InitializeGameObjects();
            gameDrawer = new(gamePanel, heartsPanel, brick, platform, ball, heart);

            gamePanel.Width = brick.Width * gameDrawer.BricksInRow;
            Width = gamePanel.Width + 250;

            gameDrawer.InitializePictureBoxBricks();
            gameDrawer.InitializePictureBoxBall();
            gameDrawer.InitializePictureBoxPlatform();
            gameDrawer.InitializePictureBoxHearts(heartCount);

            InitializeTimer();
            gameManager = new(timer1, scoreLabel);
        }

        private void InitializeGameObjects()
        {
            Bitmap[] bricksBitmap = { Resources.BlueBrick, Resources.PurpleBrick, Resources.GreenBrick, Resources.RedBrick, Resources.OrangeBrick };
            brick = new(80, 30, bricksBitmap);

            var platformPoint = new Point(Width / 2, Height - 100);
            platform = new(150, 35, 8, platformPoint, Resources.Platform);

            heart = new(Resources.Heart, heartsPanel.Width / heartCount, 0);

            var ballPoint = new Point(Width / 2, Height / 2);
            ball = new(25, -2, -2, ballPoint, Resources.Ball);
        }

        private void InitializeTimer()
        {
            timer1 = new Timer
            {
                Interval = 10
            };
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            var ballSpeedX = ball.SpeedX;
            var ballSpeedY = ball.SpeedY;

            var newLocationX = gameDrawer.BallPictureBox.Location.X + ballSpeedX;
            var newLocationY = gameDrawer.BallPictureBox.Location.Y + ballSpeedY;

            gameDrawer.BallPictureBox.Location = new Point(newLocationX, newLocationY);

            CheckGameEnd();
            CheckBallCollisionWithBricks();
            CheckBallCollisionWithBorders();
        }

        private void CheckGameEnd()
        {
            if (gameDrawer.BallPictureBox.Bottom > gamePanel.Bottom)
            {
                gameDrawer.RemoveOneHeart();
                heartCount -= 1;

                if (heartCount == 0 && gameManager.NeedResetGame())
                {
                    gameDrawer.RedrawElements();
                    return;
                }

                return;
            }
        }

        private void CheckBallCollisionWithBorders()
        {
            if (gameDrawer.BallPictureBox.Left < 0 || gameDrawer.BallPictureBox.Right > gamePanel.Width)
            {
                ball.ChangeVelocityX();
            }

            if (gameDrawer.BallPictureBox.Top < 0)
            {
                ball.ChangeVelocityY();
            }

            if (gameDrawer.BallPictureBox.Bounds.IntersectsWith(gameDrawer.PlatformPictureBox.Bounds))
            {
                ball.ChangeVelocityY();
            }
        }

        private void CheckBallCollisionWithBricks()
        {
            foreach (var brick in gameDrawer.Bricks)
            {
                if (gameDrawer.BallPictureBox.Bounds.IntersectsWith(brick.Bounds))
                {
                    gamePanel.Controls.Remove(brick);
                    gameDrawer.Bricks.Remove(brick);

                    gameManager.UpdateScore();

                    ball.ChangeVelocityY();
                    break;
                }
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            var platformLocationX = gameDrawer.PlatformPictureBox.Location.X;

            switch (e.KeyCode)
            {
                case Keys.A:
                    platformLocationX -= platform.Speed;
                    break;

                case Keys.D:
                    platformLocationX += platform.Speed;
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }

            if (IsPlatformTouchEdge(platformLocationX, gameDrawer.PlatformPictureBox.Width))
            {
                return;
            }

            var LocationY = gameDrawer.PlatformPictureBox.Location.Y;
            gameDrawer.PlatformPictureBox.Location = new Point(platformLocationX, LocationY);
        }

        private bool IsPlatformTouchEdge(int platformLocationX, int controlWidth)
        {
            if (platformLocationX < 0 || platformLocationX > gamePanel.Width - controlWidth)
            {
                return true;
            }

            return false;
        }
    }
}
