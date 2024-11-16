using Sudoku.Core;
using Sudoku.Core.Dosuku;
using Sudoku.Core.Dosuku.Models;
using Sudoku.Forms;
using System.Net.Http.Json;

namespace Sudoku.Kudos;

public partial class MainForm : Form
{
    private SolvingSudokuGrid _grid = new SolvingSudokuGrid();
    private DosukuResponse? _apiGrid;

    private readonly Pen _thinPen = new Pen(Color.Black, 1);
    private readonly Pen _thickPen = new Pen(Color.Black, 2);

    private Font _valueFont = new Font("Arial", 20);
    private Font _candidatesFont = new Font("Arial", 10);

    private int _gridCellWidth;
    private int _gridCellHeight;

    public MainForm()
    {
        InitializeComponent();
        InitPanel();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void InitPanel()
    {
        sudokuPanel.Width = sudokuPanel.Width - sudokuPanel.Width % 9 + 2;
        sudokuPanel.Height = sudokuPanel.Height - sudokuPanel.Height % 9 + 2;

        _gridCellHeight = sudokuPanel.Height / 9;
        _gridCellWidth = sudokuPanel.Width / 9;

        _valueFont = new Font("Arial", _gridCellHeight / 2, FontStyle.Regular);
        _candidatesFont = new Font("Arial", _gridCellHeight / 4, FontStyle.Regular);
    }

    private void sudokuPanel_Paint(object sender, PaintEventArgs e)
    {
        if (sender is Panel panel)
        {
            if (_grid.IsSolved())
                panel.BackColor = Color.LightGreen;

            var g = e.Graphics;

            for (int x = 0; x <= 9; x++)
            {
                if (x % 3 == 0)
                    g.DrawLine(_thickPen, x * _gridCellWidth + 1, 0, x * _gridCellWidth + 1, panel.Height);
                else
                    g.DrawLine(_thinPen, x * _gridCellWidth + 1, 0, x * _gridCellWidth + 1, panel.Height);
            }

            for (int y = 0; y <= 9; y++)
            {
                if (y % 3 == 0)
                    g.DrawLine(_thickPen, 0, y * _gridCellHeight + 1, panel.Width, y * _gridCellHeight + 1);
                else
                    g.DrawLine(_thinPen, 0, y * _gridCellHeight + 1, panel.Width, y * _gridCellHeight + 1);
            }

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    var cell = _grid[row, column];
                    var font = cell.Value == 0 ? _candidatesFont : _valueFont;
                    var brush = cell.CellType == CellType.Editable ? Brushes.Blue : Brushes.Black;

                    if (cell.Value != 0)
                    {
                        var x = column * _gridCellWidth + _gridCellWidth / 2 - font.Size / 2;
                        var y = row * _gridCellHeight + _gridCellHeight / 2 - font.Height / 2;
                        g.DrawString(cell.Value.ToString(), font, brush, x, y);
                    }
                    else
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (cell.Candidates.Contains(i + 1))
                            {
                                var x = column * _gridCellWidth + (i % 3) * _gridCellWidth / 3 + 2;
                                var y = row * _gridCellHeight + (i / 3) * _gridCellHeight / 3 + 2;
                                g.DrawString((i + 1).ToString(), font, brush, x, y);
                            }
                        }
                    }
                }
            }
        }
    }

    private async void fetchButton_Click(object sender, EventArgs e)
    {
        resetToolStripMenuItem.Enabled = false;
        solveToolStripMenuItem.Enabled = false;
        fetchNewToolStripMenuItem.Enabled = false;
        validateToolStripMenuItem.Enabled = false;

        _apiGrid = await DosukuHandler.FetchNewBoardAsync();

        if (_apiGrid is not null)
            Text = "Sudoku - Difficulty: " + _apiGrid.NewBoard.Grids[0].Difficulty;

        ResetGrid();

        resetToolStripMenuItem.Enabled = true;
        solveToolStripMenuItem.Enabled = true;
        fetchNewToolStripMenuItem.Enabled = true;
        validateToolStripMenuItem.Enabled = true;
    }

    private void solveButton_Click(object sender, EventArgs e)
    {
        _grid.Solve();
    }

    private void OnGridUpdated(object sender, EventArgs e)
    {
        sudokuPanel.Refresh();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
        ResetGrid();
    }

    private void ResetGrid()
    {
        if (_apiGrid is not null)
        {
            _grid = new SolvingSudokuGrid(_apiGrid.NewBoard.Grids[0].Value);
            _grid.GridUpdated += OnGridUpdated;
            sudokuPanel.BackColor = Color.White;
            sudokuPanel.Refresh();
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        _grid.SolveBruteForce();
    }

    private void validateButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show($"The grid is {(_grid.IsValid() ? "valid" : "invalid")} {(_grid.IsSolved() ? "and" : "but not")} solved.");
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var credits = new CreditsForm();
        credits.ShowDialog();
    }
}
