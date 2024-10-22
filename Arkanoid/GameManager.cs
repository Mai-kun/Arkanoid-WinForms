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

        public GameManager(GameForm form)
        {
            this.form = form;
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
            form.Timer1.Start();
            return true;
        }

        private void UpdateScoreLabel()
        {
            form.ScoreLabel.Text = $"Счёт: {score}";
        }
    }
}
