using Timer = System.Windows.Forms.Timer;

namespace Arkanoid
{
    /// <summary>
    /// Представляет функционал управления игровым процессом.
    /// </summary>
    public class GameManager
    {
        private int score = 0;
        private readonly Timer gameTimer;
        private readonly Label uiLabel;

        public GameManager(Timer gameTimer, Label uiLabel)
        {
            this.gameTimer = gameTimer;
            this.uiLabel = uiLabel;
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
        /// Спрашивает о необходимости перезапуска игры.
        /// </summary>
        public bool NeedResetGame()
        {
            gameTimer.Stop();

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
            gameTimer.Start();
            return true;
        }

        private void UpdateScoreLabel()
        {
            uiLabel.Text = $"Счёт: {score}";
        }
    }
}
