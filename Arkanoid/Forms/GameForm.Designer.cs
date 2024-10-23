
namespace Arkanoid
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gamePanel = new Panel();
            infoPanel = new Panel();
            scoreLabel = new Label();
            heartsPanel = new Panel();
            infoPanel.SuspendLayout();
            SuspendLayout();
            // 
            // gamePanel
            // 
            gamePanel.BorderStyle = BorderStyle.FixedSingle;
            gamePanel.Dock = DockStyle.Left;
            gamePanel.Location = new Point(0, 0);
            gamePanel.Name = "gamePanel";
            gamePanel.Padding = new Padding(10);
            gamePanel.Size = new Size(512, 686);
            gamePanel.TabIndex = 1;
            // 
            // infoPanel
            // 
            infoPanel.Controls.Add(scoreLabel);
            infoPanel.Dock = DockStyle.Bottom;
            infoPanel.Location = new Point(512, 80);
            infoPanel.Name = "infoPanel";
            infoPanel.Size = new Size(201, 606);
            infoPanel.TabIndex = 2;
            // 
            // scoreLabel
            // 
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            scoreLabel.Location = new Point(15, 20);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(92, 32);
            scoreLabel.TabIndex = 0;
            scoreLabel.Text = "Счёт: 0";
            // 
            // heartsPanel
            // 
            heartsPanel.Dock = DockStyle.Top;
            heartsPanel.Location = new Point(512, 0);
            heartsPanel.Name = "heartsPanel";
            heartsPanel.Size = new Size(201, 74);
            heartsPanel.TabIndex = 3;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(713, 686);
            Controls.Add(heartsPanel);
            Controls.Add(infoPanel);
            Controls.Add(gamePanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "GameForm";
            Text = "Arkanoid";
            KeyDown += GameForm_KeyDown;
            infoPanel.ResumeLayout(false);
            infoPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel gamePanel;
        private Panel heartsPanel;
        private Panel infoPanel;
        private Label scoreLabel;
    }
}
