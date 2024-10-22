using Arkanoid.GameMaster.Properties;
using Arkanoid.Models;

namespace Arkanoid
{
    /// <summary>
    /// Отвечает за отрисовку игровых объектов на форме.
    /// </summary>
    public class GameDrawer
    {
        /// <summary>
        /// Количество блоков в строке.
        /// </summary>
        public int BricksInRow { get; }

        /// <summary>
        /// Экземпляр объекта <see cref="Models.Brick"/>
        /// </summary>
        public Brick Brick { get; set; }

        /// <summary>
        /// Экземпляр объекта <see cref="Models.Ball"/>.
        /// </summary>
        public Ball Ball { get; set; }

        private const int Rows = 5;
        private int defaultHeartCount;
        private readonly Control heartsControl;
        private readonly Control mainControl;
        private readonly Bitmap[] bricksBitmap = { Resources.BlueBrick, Resources.PurpleBrick, Resources.GreenBrick, Resources.RedBrick, Resources.OrangeBrick };

        public GameDrawer(Control mainControl, Control heartsControl)
        {
            Brick = new(80, 30);
            BricksInRow = 8;

            this.mainControl = mainControl;
            this.heartsControl = heartsControl;

            InitBallInstance();
        }

        /// <summary>
        /// Объект <see cref="PictureBox"/> для <see cref="Models.Ball"/>
        /// </summary>
        public PictureBox BallPictureBox { get; set; }

        /// <summary>
        /// Объект <see cref="PictureBox"/> для <see cref="Models.Platform"/>
        /// </summary>
        public PictureBox PlatformPictureBox { get; set; }

        /// <summary>
        /// Список объектов <see cref="PictureBox"/> по <see cref="Models.Brick"/>.
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
                        Width = Brick.Width,
                        Height = Brick.Height,
                        Image = bricksBitmap[row % bricksBitmap.Length],
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Left = col * Brick.Width,
                        Top = row * Brick.Height,
                    };

                    Bricks.Add(block);
                    mainControl.Controls.Add(block);
                }
            }
        }

        /// <summary>
        /// Добавляет <see cref="Models.Ball"/> на панель игры.
        /// </summary>
        public void InitializePictureBoxBall()
        {
            InitBallInstance();

            BallPictureBox = new PictureBox()
            {
                Height = Ball.Size,
                Width = Ball.Size,
                Image = Resources.Ball,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = Ball.Location,
            };

            mainControl.Controls.Add(BallPictureBox);
        }

        private void InitBallInstance()
        {
            var ballPoint = new Point(mainControl.Width / 2, mainControl.Height / 2);
            Ball = new(25, -2, -2, ballPoint);
        }

        /// <summary>
        /// Добавляет <see cref="Models.Platform"/> на панель игры.
        /// </summary>
        public void InitializePictureBoxPlatform()
        {
            var platformPoint = new Point(mainControl.Width / 2, mainControl.Height - 100);
            Platform platform = new(150, 35, platformPoint);

            PlatformPictureBox = new PictureBox()
            {
                Width = platform.Width,
                Height = platform.Height,
                Image = Resources.Platform,
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
            var groupBoxWidth = heartsControl.Width;

            var heartWidth = groupBoxWidth / defaultHeartCount;

            for (var i = 0; i < defaultHeartCount; i++)
            {
                var heart = new PictureBox
                {
                    Image = Resources.Heart,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = heartWidth,
                    Location = new Point(i * heartWidth, 0)
                };

                heartsControl.Controls.Add(heart);
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
