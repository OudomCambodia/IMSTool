using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmViewAttachments : Form
    {

        string TempFolder = @"C:\\IMSFileManagement\\Tempt\\";
        public string DocCode = "1"; //default
        DBS11SqlCrud crud = new DBS11SqlCrud();

        public frmViewAttachments()
        {
            InitializeComponent();
        }

        private void frmViewAttachments_Load(object sender, EventArgs e)
        {
            Requery();
        }

        void Requery()
        {
            DataTable dtTemp = crud.LoadData("SELECT CREATE_DATE,DOC_TYPE,PRODUCT_TYPE,CUS_NAME FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE = " + DocCode).Tables[0];
            tbDocID.Text = DocCode;
            tbCreateDate.Text = dtTemp.Rows[0]["CREATE_DATE"].ToString();
            tbDocType.Text = dtTemp.Rows[0]["DOC_TYPE"].ToString();
            tbProType.Text = dtTemp.Rows[0]["PRODUCT_TYPE"].ToString();
            tbCusName.Text = dtTemp.Rows[0]["CUS_NAME"].ToString();

            dtTemp = crud.LoadData("SELECT FILENAME,PATH FROM dbo.tbAttachment WHERE DOC_CODE = " + DocCode).Tables[0];
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                    dgvFile.Rows.Add((i + 1).ToString(), dtTemp.Rows[i][0], dtTemp.Rows[i][1]);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    if (!File.Exists(path))
                    {
                        if (path.Contains("192.168.110.228"))
                            path = path.Replace("192.168.110.228", "AD02");
                        else if (path.Contains("AD02"))
                            path = path.Replace("AD02", "192.168.110.228");
                    }
                    File.Copy(path, TempPath, true);
                    var attributes = File.GetAttributes(TempPath);
                    File.SetAttributes(TempPath, attributes | FileAttributes.ReadOnly);
                    System.Diagnostics.Process.Start(TempPath);
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
                    Cursor.Current = Cursors.WaitCursor;
                    int RowIndex = dgvFile.SelectedRows[0].Index;
                    string path = dgvFile.Rows[RowIndex].Cells[2].Value.ToString();
                    if (!File.Exists(path))
                    {
                        if (path.Contains("192.168.110.228"))
                            path = path.Replace("192.168.110.228", "AD02");
                        else if (path.Contains("AD02"))
                            path = path.Replace("AD02", "192.168.110.228");
                    }
                    File.Copy(path, fbdDownload.SelectedPath + @"\\" + dgvFile.Rows[RowIndex].Cells[1].Value.ToString(), true);

                    Msgbox.Show("File downloaded successfully!");
                    Cursor.Current = Cursors.AppStarting;
                }
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofdUpload = new OpenFileDialog();
            ofdUpload.Filter = "Common Files(*.JPEG;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF)|*.BMP;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF|All files (*.*)|*.*";
            ofdUpload.Multiselect = true;
            if (ofdUpload.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string drivePath = frmAddDocument1.drivePath;
                string DocCodeFolder = DocCode + "\\";

                if (!Directory.Exists(drivePath))
                {
                    if (drivePath.Contains("192.168.110.228"))
                        drivePath = drivePath.Replace("192.168.110.228", "AD02");
                    else if (drivePath.Contains("AD02"))
                        drivePath = drivePath.Replace("AD02", "192.168.110.228");
                }

                if (!Directory.Exists(drivePath + DocCodeFolder))
                    Directory.CreateDirectory(drivePath + DocCodeFolder);

                foreach (string path in ofdUpload.FileNames)
                {
                    string filename = Path.GetFileName(path);

                    for (int i = 0; i < dgvFile.Rows.Count; i++)
                    {
                        if (dgvFile.Rows[i].Cells[1].Value.ToString() == filename)
                        {
                            Msgbox.Show(filename + " already exists in the list. Please check the file again.");
                            return;
                        }
                    }
                    string fullPath = drivePath + DocCodeFolder + filename;
                    File.Copy(path, fullPath, true);
                    //string sql = @"INSERT INTO dbo.tbAttachment (DOC_CODE,PATH,FILENAME,ADD_DATE) VALUES (" + DocCode + ",N'" + fullPath + "',N'" + filename + "','" + DateTime.Now + "')";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandText = "INSERT INTO dbo.tbAttachment (DOC_CODE,PATH,FILENAME,ADD_DATE) VALUES (" + DocCode + ",@fullpath,@filename,'" + DateTime.Now + "')";
                    System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter();
                    parameter.ParameterName = "@fullpath";
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Value = fullPath;
                    System.Data.SqlClient.SqlParameter parameter1 = new System.Data.SqlClient.SqlParameter();
                    parameter1.ParameterName = "@filename";
                    parameter1.SqlDbType = SqlDbType.NVarChar;
                    parameter1.Direction = ParameterDirection.Input;
                    parameter1.Value = filename;
                    cmd.Parameters.Add(parameter);
                    cmd.Parameters.Add(parameter1);

                    crud.Executing(cmd);

                }
                Cursor.Current = Cursors.AppStarting;
                Requery();
            }
        }
    }
}
