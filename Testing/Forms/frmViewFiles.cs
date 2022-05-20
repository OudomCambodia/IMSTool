 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmViewFiles : Form
    {
        public string upl_detail_id;
        string TempFolder = @"C:\\IMSFileManagement\\Tempt\\";
        public frmViewFiles()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmViewFiles_Load(object sender, EventArgs e)
        {
            CRUD crud = new CRUD();
            DataTable dt = crud.ExecQuery("select FILE_PATH, FILE_NAME from USER_UPLOAD_FILES where UPLOAD_DETAIL_ID = " + upl_detail_id);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvFile.Rows.Add((i + 1).ToString(), dt.Rows[i].ItemArray[1], dt.Rows[i].ItemArray[0]);
            }
        }

        private void dgvFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvFile.Columns["Open"].Index && e.RowIndex >= 0)
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    int RowIndex = dgvFile.SelectedRows[0].Index;
                    string path = dgvFile.Rows[RowIndex].Cells[2].Value.ToString();
                    Directory.CreateDirectory(TempFolder);
                    Array.ForEach(Directory.GetFiles(TempFolder), File.Delete);
                    string TempPath = TempFolder + dgvFile.Rows[RowIndex].Cells[1].Value.ToString();
                    File.Copy(path, TempPath, true);
                    var attributes = File.GetAttributes(TempPath);
                    File.SetAttributes(TempPath, attributes | FileAttributes.ReadOnly);
                    Process.Start(TempPath);
                    File.SetAttributes(TempPath, attributes);
                }
                catch (Exception ex)
                {
                    Msgbox.Show(ex.Message);
                }

                Cursor.Current = Cursors.AppStarting;
            }
            else if (e.ColumnIndex == dgvFile.Columns["Download"].Index && e.RowIndex >= 0)
            {
                if (fbdDownload.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbdDownload.SelectedPath))
                {
                    int RowIndex = dgvFile.SelectedRows[0].Index;
                    string path = dgvFile.Rows[RowIndex].Cells[2].Value.ToString();
                    File.Copy(path, fbdDownload.SelectedPath + @"\\" + dgvFile.Rows[RowIndex].Cells[1].Value.ToString(), true);

                    Msgbox.Show("The file is downloaded successfully!");
                }
            }
        }
    }
}
