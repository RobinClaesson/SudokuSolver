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
            sourceLabel = new Label();
            apiLabel = new Label();
            sourceLink = new LinkLabel();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            SuspendLayout();
            // 
            // creatorLabel
            // 
            creatorLabel.AutoSize = true;
            creatorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            creatorLabel.Location = new Point(12, 52);
            creatorLabel.Name = "creatorLabel";
            creatorLabel.Size = new Size(52, 15);
            creatorLabel.TabIndex = 1;
            creatorLabel.Text = "Creator:";
            // 
            // sourceLabel
            // 
            sourceLabel.AutoSize = true;
            sourceLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sourceLabel.Location = new Point(12, 9);
            sourceLabel.Name = "sourceLabel";
            sourceLabel.Size = new Size(49, 15);
            sourceLabel.TabIndex = 1;
            sourceLabel.Text = "Source:";
            // 
            // apiLabel
            // 
            apiLabel.AutoSize = true;
            apiLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            apiLabel.Location = new Point(12, 113);
            apiLabel.Name = "apiLabel";
            apiLabel.Size = new Size(74, 15);
            apiLabel.TabIndex = 1;
            apiLabel.Text = "Dosuku API:";
            // 
            // sourceLink
            // 
            sourceLink.AutoSize = true;
            sourceLink.Location = new Point(12, 24);
            sourceLink.Name = "sourceLink";
            sourceLink.Size = new Size(269, 15);
            sourceLink.TabIndex = 0;
            sourceLink.TabStop = true;
            sourceLink.Text = "https://github.com/RobinClaesson/SudokuSolver";
            sourceLink.LinkClicked += LinkLabels_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(12, 67);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(192, 15);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://github.com/RobinClaesson";
            linkLabel1.LinkClicked += LinkLabels_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(12, 85);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(201, 15);
            linkLabel2.TabIndex = 2;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "https://portfolio.robinclaesson.com/";
            linkLabel2.LinkClicked += LinkLabels_LinkClicked;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(12, 128);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(169, 15);
            linkLabel3.TabIndex = 3;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "https://sudoku-api.vercel.app/";
            linkLabel3.LinkClicked += LinkLabels_LinkClicked;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.Location = new Point(12, 145);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(177, 15);
            linkLabel4.TabIndex = 4;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "https://github.com/Marcus0086";
            linkLabel4.LinkClicked += LinkLabels_LinkClicked;
            // 
            // CreditsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(291, 187);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel3);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(sourceLink);
            Controls.Add(sourceLabel);
            Controls.Add(apiLabel);
            Controls.Add(creatorLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreditsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sudoku - Credits";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label creatorLabel;
        private Label sourceLabel;
        private Label apiLabel;
        private LinkLabel sourceLink;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel4;
    }
}