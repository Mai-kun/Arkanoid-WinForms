using Timer = System.Windows.Forms.Timer;

namespace Arkanoid
{
    public partial class GameForm : Form
    {
        private readonly GameDrawer gameDrawer;
        private readonly GameManager gameManager;

        private const int DefaultHeartCount = 3;
        private int currentHeartCount = DefaultHeartCount;
        private Timer timer1;

        public GameForm()
        {
            InitializeComponent();

            gameDrawer = new(gamePanel, heartsPanel);

            gamePanel.Width = gameDrawer.Brick.Width * gameDrawer.BricksInRow;
            Width = gamePanel.Width + 250;

            gameDrawer.InitializePictureBoxBricks();
            gameDrawer.InitializePictureBoxBall();
            gameDrawer.InitializePictureBoxPlatform();
            gameDrawer.InitializePictureBoxHearts(DefaultHeartCount);

            InitializeTimer();

            gameManager = new(timer1, scoreLabel);
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
            var ballSpeedX = gameDrawer.Ball.SpeedX;
            var ballSpeedY = gameDrawer.Ball.SpeedY;

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
                currentHeartCount -= 1;

                if (currentHeartCount == 0 && gameManager.NeedResetGame())
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
                gameDrawer.Ball.ChangeVelocityX();
            }

            if (gameDrawer.BallPictureBox.Top < 0)
            {
                gameDrawer.Ball.ChangeVelocityY();
            }

            if (gameDrawer.BallPictureBox.Bounds.IntersectsWith(gameDrawer.PlatformPictureBox.Bounds))
            {
                gameDrawer.Ball.ChangeVelocityY();
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

                    gameDrawer.Ball.ChangeVelocityY();
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
                    platformLocationX -= gameDrawer.Platform.Speed;
                    break;

                case Keys.D:
                    platformLocationX += gameDrawer.Platform.Speed;
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
