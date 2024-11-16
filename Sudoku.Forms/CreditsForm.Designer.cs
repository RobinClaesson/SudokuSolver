namespace Sudoku.Forms
{
    partial class CreditsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            creatorLabel = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // creatorLabel
            // 
            creatorLabel.AutoSize = true;
            creatorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            creatorLabel.Location = new Point(12, 9);
            creatorLabel.Name = "creatorLabel";
            creatorLabel.Size = new Size(52, 15);
            creatorLabel.TabIndex = 0;
            creatorLabel.Text = "Creator:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 27);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(277, 132);
            textBox1.TabIndex = 1;
            textBox1.Text = "Robin Claesson\r\n";
            // 
            // CreditsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(442, 402);
            Controls.Add(textBox1);
            Controls.Add(creatorLabel);
            Name = "CreditsForm";
            Text = "CreditsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label creatorLabel;
        private TextBox textBox1;
    }
}