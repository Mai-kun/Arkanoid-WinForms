using Arkanoid.Properties;

namespace Arkanoid
{
    /// <summary>
    /// Отвечает за отрисовку игровых объектов на форме.
    /// </summary>
    internal class GameDrawer
    {
        /// <summary>
        /// Количество блоков в строке.
        /// </summary>
        public const int BlocksInRow = 8;

        private const int Rows = 5;
        private const int DefaultHeartCount = 3;
        private int currentHeartCount;
        private readonly GameForm form;
        private readonly GameManager gameManager;

        public GameDrawer(GameForm form)
        {
            this.form = form;
            gameManager = GameManager.GetInstance(form);
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
        public void InitializeBricks()
        {
            Bricks = new List<PictureBox>();
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < BlocksInRow; col++)
                {
                    var block = new PictureBox
                    {
                        Width = gameManager.Brick.Width,
                        Height = gameManager.Brick.Height,
                        Image = gameManager.Brick.Image[row % gameManager.Brick.Image.Length],
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Left = col * gameManager.Brick.Width,
                        Top = row * gameManager.Brick.Height,
                    };

                    Bricks.Add(block);
                    form.GamePanel.Controls.Add(block);
                }
            }
        }

        /// <summary>
        /// Добавляет <see cref="Models.Ball"/> на панель игры.
        /// </summary>
        public void InitializeBall()
        {
            BallPictureBox = new PictureBox()
            {
                Height = gameManager.Ball.Size,
                Width = gameManager.Ball.Size,
                Image = gameManager.Ball.Image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = gameManager.Ball.Location,
            };

            form.GamePanel.Controls.Add(BallPictureBox);
        }

        /// <summary>
        /// Добавляет <see cref="Models.Platform"/> на панель игры.
        /// </summary>
        public void InitializePlatform()
        {
            PlatformPictureBox = new PictureBox()
            {
                Width = gameManager.Platform.Width,
                Height = gameManager.Platform.Height,
                Image = gameManager.Platform.Image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = gameManager.Platform.Location,
            };

            form.GamePanel.Controls.Add(PlatformPictureBox);
        }

        /// <summary>
        /// Добавляет сердца на панель игры.
        /// </summary>
        public void InitializeHearts()
        {
            currentHeartCount = DefaultHeartCount;
            var groupBoxWidth = form.HeartsPanel.Width;

            var heartWidth = groupBoxWidth / DefaultHeartCount;

            for (var i = 0; i < DefaultHeartCount; i++)
            {
                var heart = new PictureBox
                {
                    Image = Resources.Heart,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = heartWidth,
                    Location = new Point(i * heartWidth, 0)
                };

                form.HeartsPanel.Controls.Add(heart);
            }
        }

        /// <summary>
        /// Уменьшает количество жизней.
        /// </summary>
        public void RemoveOneHeart()
        {
            var heart = form.HeartsPanel.Controls.OfType<PictureBox>().ElementAt(currentHeartCount - 1);
            form.HeartsPanel.Controls.Remove(heart);
            currentHeartCount -= 1;

            if (currentHeartCount == 0 && gameManager.NeedResetGame())
            {
                RedrawElements();
                return;
            }

            form.GamePanel.Controls.Remove(BallPictureBox);
            InitializeBall();
        }

        /// <summary>
        /// Перерисовывает все элементы игры.
        /// </summary>
        public void RedrawElements()
        {
            form.GamePanel.Controls.Clear();
            form.HeartsPanel.Controls.Clear();

            InitializeBricks();
            InitializeBall();
            InitializePlatform();
            InitializeHearts();
        }
    }
}
