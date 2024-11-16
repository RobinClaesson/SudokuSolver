namespace Sudoku.Kudos
{
    partial class MainForm
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
            sudokuPanel = new Panel();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            gridToolStripMenuItem = new ToolStripMenuItem();
            fetchNewToolStripMenuItem = new ToolStripMenuItem();
            resetToolStripMenuItem = new ToolStripMenuItem();
            validateToolStripMenuItem = new ToolStripMenuItem();
            solveToolStripMenuItem = new ToolStripMenuItem();
            creditsToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // sudokuPanel
            // 
            sudokuPanel.BackColor = SystemColors.ControlLightLight;
            sudokuPanel.Location = new Point(12, 27);
            sudokuPanel.Name = "sudokuPanel";
            sudokuPanel.Size = new Size(500, 500);
            sudokuPanel.TabIndex = 0;
            sudokuPanel.Paint += sudokuPanel_Paint;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, gridToolStripMenuItem, solveToolStripMenuItem, creditsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(521, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "&Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // gridToolStripMenuItem
            // 
            gridToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fetchNewToolStripMenuItem, resetToolStripMenuItem, validateToolStripMenuItem });
            gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            gridToolStripMenuItem.Size = new Size(41, 20);
            gridToolStripMenuItem.Text = "&Grid";
            // 
            // fetchNewToolStripMenuItem
            // 
            fetchNewToolStripMenuItem.Name = "fetchNewToolStripMenuItem";
            fetchNewToolStripMenuItem.Size = new Size(180, 22);
            fetchNewToolStripMenuItem.Text = "&Fetch new";
            fetchNewToolStripMenuItem.Click += fetchButton_Click;
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resetToolStripMenuItem.Size = new Size(180, 22);
            resetToolStripMenuItem.Text = "&Reset";
            resetToolStripMenuItem.Click += resetButton_Click;
            // 
            // validateToolStripMenuItem
            // 
            validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            validateToolStripMenuItem.Size = new Size(180, 22);
            validateToolStripMenuItem.Text = "&Validate";
            validateToolStripMenuItem.Click += validateButton_Click;
            // 
            // solveToolStripMenuItem
            // 
            solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            solveToolStripMenuItem.Size = new Size(50, 20);
            solveToolStripMenuItem.Text = "&Solve!";
            solveToolStripMenuItem.Click += solveButton_Click;
            // 
            // creditsToolStripMenuItem
            // 
            creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            creditsToolStripMenuItem.Size = new Size(56, 20);
            creditsToolStripMenuItem.Text = "&Credits";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(521, 540);
            Controls.Add(sudokuPanel);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Sudoku";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel sudokuPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem gridToolStripMenuItem;
        private ToolStripMenuItem fetchNewToolStripMenuItem;
        private ToolStripMenuItem resetToolStripMenuItem;
        private ToolStripMenuItem validateToolStripMenuItem;
        private ToolStripMenuItem solveToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem creditsToolStripMenuItem;
    }
}
