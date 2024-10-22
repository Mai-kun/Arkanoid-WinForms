using Timer = System.Windows.Forms.Timer;

namespace Arkanoid
{
    public partial class GameForm : Form
    {
        private readonly GameDrawer gameDrawer;
        private readonly GameManager gameManager;

        public Timer Timer1 { get; init; }
        public Panel GamePanel => gamePanel;
        public Panel HeartsPanel => heartsPanel;
        public Label ScoreLabel => scoreLabel;

        public GameForm()
        {
            InitializeComponent();

            gameDrawer = new(this);
            gameManager = GameManager.GetInstance(this);

            gamePanel.Width = gameManager.Brick.Width * GameDrawer.BlocksInRow;
            Width = gamePanel.Width + 250;

            gameDrawer.InitializeBricks();
            gameDrawer.InitializeBall();
            gameDrawer.InitializePlatform();
            gameDrawer.InitializeHearts();

            Timer1 = new Timer
            {
                Interval = 10
            };
            Timer1.Tick += Timer1_Tick;
            Timer1.Start();
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            gameManager.Ball.Move();
            gameDrawer.BallPictureBox.Location = gameManager.Ball.Location;

            if (gameDrawer.BallPictureBox.Bottom > gamePanel.Bottom)
            {
                gameDrawer.RemoveOneHeart();
                return;
            }

            if (gameDrawer.BallPictureBox.Left < 0 || gameDrawer.BallPictureBox.Right > gamePanel.Width)
            {
                gameManager.Ball.ChangeVelocityX();
            }

            if (gameDrawer.BallPictureBox.Top < 0)
            {
                gameManager.Ball.ChangeVelocityY();
            }

            if (gameDrawer.BallPictureBox.Bounds.IntersectsWith(gameDrawer.PlatformPictureBox.Bounds))
            {
                gameManager.Ball.ChangeVelocityY();
            }

            foreach (var brick in gameDrawer.Bricks)
            {
                if (gameDrawer.BallPictureBox.Bounds.IntersectsWith(brick.Bounds))
                {
                    gamePanel.Controls.Remove(brick);
                    gameDrawer.Bricks.Remove(brick);

                    gameManager.UpdateScore();

                    gameManager.Ball.ChangeVelocityY();
                    break;
                }
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            var platform = gameManager.Platform;

            switch (e.KeyCode)
            {
                case Keys.A:
                    platform.Move(Direction.Left);
                    break;

                case Keys.D:
                    platform.Move(Direction.Right);
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }

            platform.Location = ClampPlatformPosition(platform.Location, platform.Width);
            gameDrawer.PlatformPictureBox.Location = platform.Location;
        }

        private Point ClampPlatformPosition(Point location, int controlWidth)
        {
            if (location.X < 0)
            {
                return new Point(0, location.Y);
            }
            else if (location.X > gamePanel.Width - controlWidth)
            {
                return new Point(gamePanel.Width - controlWidth, location.Y);
            }
            return location;
        }
    }
}
