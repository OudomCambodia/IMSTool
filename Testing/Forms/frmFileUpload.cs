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
    public partial class frmFileUpload : Form
    {
        CRUD crud = new CRUD();
        public frmViewDetailUploadcs vdUpload = new frmViewDetailUploadcs();
        public bool UpdateBtn = false;

        string drivePath = @"\\fipnhdbs11\Infoins_IMS_Upload_doc$\";

        public string upl_id, policy_no, UserName;

        public frmFileUpload()
        {
            InitializeComponent();
        }

        private void frmFileUpload_Load(object sender, EventArgs e)
        {
            ofdUpload.Filter = "Common Files(*.JPEG;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF)|*.BMP;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF|All files (*.*)|*.*";
            DataTable dt = new DataTable();
            dt = crud.ExecQuery("select * from VIEW_POLICY_INFORMATION where POL_POLICY_NO = '" + policy_no + "'");
            txtEndoNo.Text = dt.Rows[0].ItemArray[3].ToString();
            dtpEffFrom.Value = (DateTime)dt.Rows[0].ItemArray[4];
            dtpEffTo.Value = (DateTime)dt.Rows[0].ItemArray[5];

            if (UpdateBtn == true)
                bnSave.Text = "Update";
        }

        private void bnBrowse_Click(object sender, EventArgs e)
        {
            if (ofdUpload.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in ofdUpload.FileNames)
                {
                    string filename = Path.GetFileName(path);

                    for (int i = 0; i < dgvFile.Rows.Count; i++)
                    {
                        if (dgvFile.Rows[i].Cells[0].Value.ToString() == filename)
                        {
                            Msgbox.Show(filename + " already exists in the list. Please check the file again.");
                            return;
                        }
                    }

                    dgvFile.Rows.Add(filename, path);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No record to remove!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvFile.SelectedRows)
                dgvFile.Rows.Remove(dgvr);
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No attached files are found.");
                return;
            }

            DialogResult dr = Msgbox.Show("Do you want to save the uploaded file(s)?", "Confirmation");
            if (dr == System.Windows.Forms.DialogResult.No)
                 return;
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string PolNoFolder = policy_no.Replace("/", "-") + "\\";
                string EndoNoFolder = txtEndoNo.Text.Replace("/", "-") + "\\";
                Directory.CreateDirectory(drivePath + PolNoFolder);
                Directory.CreateDirectory(drivePath + PolNoFolder + EndoNoFolder);

                string sql = "";
                if (UpdateBtn == false)
                {
                    sql = @"insert into USER_UPLOAD_DETAIL (UPLOAD_ID, ENDORSEMENT_NO, EFFECTIVE_DATE, EFFECTIVE_TO, REMARK, ISSUED_BY, ISSUE_DATE)
                            values
                            ('" + upl_id + "', '" + txtEndoNo.Text + "', TO_DATE('" + dtpEffFrom.Value.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), TO_DATE('" + dtpEffTo.Value.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), q'[" + txtRemark.Text.Trim() + "]', '" + UserName + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'))";
                    crud.ExecNonQuery(sql);
                }

                string upl_detail_id = crud.ExecQuery("select UPLOAD_DETAIL_ID from USER_UPLOAD_DETAIL where ENDORSEMENT_NO = '" + txtEndoNo.Text + "'").Rows[0].ItemArray[0].ToString();

                if (UpdateBtn == true)
                {
                    Array.ForEach(Directory.GetFiles(drivePath + PolNoFolder + EndoNoFolder), File.Delete);
                    crud.ExecNonQuery("delete from USER_UPLOAD_FILES where UPLOAD_DETAIL_ID = '" + upl_detail_id + "'");
                    sql = "update USER_UPLOAD_DETAIL set REMARK = '" + txtRemark.Text.Trim() + "', MODIFY_BY = '" + UserName + "', MODIFY_DATE = TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS') where UPLOAD_DETAIL_ID = '" + upl_detail_id + "'";
                    crud.ExecNonQuery(sql);
                }

                foreach (DataGridViewRow dgvr in dgvFile.Rows)
                {
                    string fileName = dgvr.Cells[0].Value.ToString();
                    string fullPath = drivePath + PolNoFolder + EndoNoFolder + fileName;
                    File.Copy(dgvr.Cells[1].Value.ToString(), fullPath, true);
                    sql = @"insert into USER_UPLOAD_FILES (UPLOAD_DETAIL_ID, FILE_PATH, FILE_NAME, ENDORSEMENT_NO)
                        values
                        ('" + upl_detail_id + "', q'[" + fullPath + "]', q'[" + fileName + "]', '" + txtEndoNo.Text + "')";
                    crud.ExecNonQuery(sql);
                }
                
                Cursor.Current = Cursors.AppStarting;

                if (UpdateBtn == false)
                    Msgbox.Show("The record has been saved.");
                else
                    Msgbox.Show("The record has been updated.");

                vdUpload.GetDataGrid();
                this.Close();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            OpenFile();
        }

        private void OpenFile()
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No file to open!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvFile.SelectedRows)
                Process.Start(dgvr.Cells[1].Value.ToString());
        }

        private void dgvFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void dgvFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in FileList)
                dgvFile.Rows.Add(Path.GetFileName(file), file);
        }
    }
}
