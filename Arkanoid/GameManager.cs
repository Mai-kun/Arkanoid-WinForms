using Arkanoid.Models;
using Arkanoid.Properties;

namespace Arkanoid
{
    /// <summary>
    /// Представляет функционал Управления игровым процессом.
    /// </summary>
    public class GameManager
    {
        private int score = 0;
        private readonly GameForm form;
        private static GameManager? instance;

        private GameManager(GameForm form)
        {
            this.form = form;

            InitializeInstances();
        }

        /// <summary>
        /// <see cref="Models.Ball"/> игры.
        /// </summary>
        public Ball Ball { get; set; }

        /// <summary>
        /// <see cref="Models.Platform"/> игрока.
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// <see cref="Models.Brick"/> игрока.
        /// </summary>
        public Brick Brick { get; set; }

        /// <summary>
        /// Возвращает единственный экземпляр <see cref="GameManager"/>.
        /// </summary>
        public static GameManager GetInstance(GameForm form)
        {
            instance ??= new GameManager(form);

            return instance;
        }

        /// <summary>
        /// Обновляет счёт игры.
        /// </summary>
        public void UpdateScore()
        {
            score += 100;
            UpdateScoreLabel();
        }

        /// <summary>
        /// Спрашивает о необходимости возобновления игры.
        /// </summary>
        public bool NeedResetGame()
        {
            form.Timer1.Stop();

            var dialogResult = MessageBox.Show("Вы проиграли. Хотите сыграть снова?",
                                                "Уведомление",
                                                MessageBoxButtons.OKCancel,
                                                MessageBoxIcon.Question);

            if (dialogResult != DialogResult.OK)
            {
                Application.Exit();
            }

            score = 0;
            UpdateScoreLabel();
            InitializeInstances();
            form.Timer1.Start();
            return true;
        }

        private void UpdateScoreLabel()
        {
            form.ScoreLabel.Text = $"Счёт: {score}";
        }

        private void InitializeInstances()
        {
            var platformPoint = new Point(form.GamePanel.Width / 2, form.GamePanel.Height - 100);
            Platform = new(150, 35, Resources.Platform, platformPoint, 8);

            var ballPoint = new Point(form.GamePanel.Width / 2, form.GamePanel.Height / 2);
            Ball = new(Resources.Ball, 25, -2, -2, ballPoint);

            Bitmap[] bricksBitmap = { Resources.BlueBrick, Resources.PurpleBrick, Resources.GreenBrick, Resources.RedBrick, Resources.OrangeBrick };
            Brick = new(80, 30, bricksBitmap);
        }
    }
}
