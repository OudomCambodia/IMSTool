using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmAddChara : Form
    {
        public frmAddChara()
        {
            InitializeComponent();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
                int row = dataGridView1.CurrentCell.RowIndex;
                int col = dataGridView1.CurrentCell.ColumnIndex;
                foreach (string data in lines)
                {
                    if (data == "")
                        return;

                    if (row == dataGridView1.Rows.Count - 1)
                        dataGridView1.Rows.Add(data);
                    else
                        dataGridView1.Rows[row].Cells[col].Value = data;
                    row++;
                }
            }
        }

        private void cus_button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow vr in dataGridView1.Rows)
            {
                if (vr.Index < dataGridView1.Rows.Count - 1)
                    vr.Cells["Result"].Value = frontText.Text + vr.Cells["Text"].Value.ToString() + backText.Text;
            }
        }
    }
}
