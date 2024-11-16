using System.Diagnostics;

namespace Sudoku.Forms;

public partial class CreditsForm : Form
{
    public CreditsForm()
    {
        InitializeComponent();
    }

    private void LinkLabels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if(sender is LinkLabel link)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = link.Text,
                UseShellExecute = true
            });
        }
    }
}
