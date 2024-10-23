using Arkanoid.Contracts.Models;
using Arkanoid.Models;

namespace Arkanoid.GameMaster
{
    /// <summary>
    /// Отвечает за отрисовку игровых объектов на форме.
    /// </summary>
    public class GameDrawer
    {
        /// <summary>
        /// Количество блоков в строке.
        /// </summary>
        public int BricksInRow { get; } = 8;

        private readonly brick brick;
        private readonly Platform platform;
        private readonly Ball ball;
        private readonly Heart heart;
        private readonly Control heartsControl;
        private readonly Control mainControl;

        private const int Rows = 5;
        private int defaultHeartCount;

        public GameDrawer(Control mainControl, Control heartsControl, brick brick, Platform platform, Ball ball, Heart heart)
        {
            this.brick = brick;
            this.platform = platform;
            this.ball = ball;
            this.heart = heart;

            this.mainControl = mainControl;
            this.heartsControl = heartsControl;
        }

        /// <summary>
        /// Объект <see cref="PictureBox"/> для <see cref="Ball"/>
        /// </summary>
        public PictureBox BallPictureBox { get; set; }

        /// <summary>
        /// Объект <see cref="PictureBox"/> для <see cref="Platform"/>
        /// </summary>
        public PictureBox PlatformPictureBox { get; set; }

        /// <summary>
        /// Список объектов <see cref="PictureBox"/> по <see cref="Models.brick"/>.
        /// </summary>
        public List<PictureBox> Bricks { get; set; }

        /// <summary>
        /// Добавляет блоки на панель игры.
        /// </summary>
        public void InitializePictureBoxBricks()
        {
            Bricks = new List<PictureBox>();
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < BricksInRow; col++)
                {
                    var block = new PictureBox
                    {
                        Width = brick.Width,
                        Height = brick.Height,
                        Image = brick.Images[row % brick.Images.Length],
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Left = col * brick.Width,
                        Top = row * brick.Height,
                    };

                    Bricks.Add(block);
                    mainControl.Controls.Add(block);
                }
            }
        }

        /// <summary>
        /// Добавляет <see cref="Ball"/> на панель игры.
        /// </summary>
        public void InitializePictureBoxBall()
        {
            BallPictureBox = new PictureBox()
            {
                Height = ball.Size,
                Width = ball.Size,
                Image = ball.Image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = ball.Location,
            };

            mainControl.Controls.Add(BallPictureBox);
        }

        /// <summary>
        /// Добавляет <see cref="Platform"/> на панель игры.
        /// </summary>
        public void InitializePictureBoxPlatform()
        {
            PlatformPictureBox = new PictureBox()
            {
                Width = platform.Width,
                Height = platform.Height,
                Image = platform.Image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = platform.Location,
            };

            mainControl.Controls.Add(PlatformPictureBox);
        }

        /// <summary>
        /// Добавляет сердца на панель игры.
        /// </summary>
        public void InitializePictureBoxHearts(int defaultHeartCount)
        {
            this.defaultHeartCount = defaultHeartCount;
            for (var i = 0; i < defaultHeartCount; i++)
            {
                var heartPictureBox = new PictureBox
                {
                    Image = heart.Image,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = heart.Width,
                    Location = new Point(i * heart.Width, 0)
                };

                heartsControl.Controls.Add(heartPictureBox);
            }
        }

        /// <summary>
        /// Уменьшает количество жизней.
        /// </summary>
        public void RemoveOneHeart()
        {
            var heartCount = heartsControl.Controls.OfType<PictureBox>().Count();
            var heart = heartsControl.Controls.OfType<PictureBox>().ElementAt(heartCount - 1);
            heartsControl.Controls.Remove(heart);
            RedrawBall();
        }

        private void RedrawBall()
        {
            mainControl.Controls.Remove(BallPictureBox);
            InitializePictureBoxBall();
        }

        /// <summary>
        /// Перерисовывает все элементы игры.
        /// </summary>
        public void RedrawElements()
        {
            mainControl.Controls.Clear();
            mainControl.Controls.Clear();

            InitializePictureBoxBricks();
            InitializePictureBoxBall();
            InitializePictureBoxPlatform();
            InitializePictureBoxHearts(defaultHeartCount);
        }
    }
}
