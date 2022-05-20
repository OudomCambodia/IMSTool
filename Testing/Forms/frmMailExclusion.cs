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
    public partial class frmMailExclusion : Form
    {


        CRUD crud = new CRUD();
        public string exclutype = string.Empty;
        public bool isExclusion = true; //false => Document

        public frmMailExclusion()
        {
            InitializeComponent();
        }

        private void frmMailExclusion_Load(object sender, EventArgs e)
        {
            if (!isExclusion)
            {
                lbTitle.Text = "Mail Document";
                this.Text = "Mail Document";
            }

            requeryDGV();
        }

        private void requeryDGV()
        {
            dgvExclu.DataSource = null;
            dgvExclu.Columns.Clear();

            if (isExclusion)
            {
                DataTable dtExclu = crud.ExecQuery("SELECT EXCL_CODE,EXCLUSION FROM USER_CLAIM_EMAIL_EXCLUS WHERE PRODUCT like '%" + exclutype + "%'");
                if (dtExclu.Rows.Count > 0)
                {
                    DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                    //CheckBox chk = new CheckBox();
                    dgvExclu.Columns.Add(CheckboxColumn);

                    dgvExclu.DataSource = dtExclu;
                    dgvExclu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DataGridViewColumn column = dgvExclu.Columns[0];
                    column.Width = 35;
                    dgvExclu.Columns[0].Resizable = DataGridViewTriState.False;
                    dgvExclu.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    dgvExclu.Columns["EXCL_CODE"].Width = 80;


                    for (int i = 1; i < dgvExclu.Columns.Count; i++)
                    {
                        dgvExclu.Columns[i].ReadOnly = true;
                    }
                }
            }
            else
            {
                DataTable dtDoc = crud.ExecQuery("SELECT DOC_CODE,DOC_TYPE,DOC_CONTENT FROM USER_CLAIM_EMAIL_DOC WHERE PRODUCT like '%" + exclutype + "%' or PRODUCT = 'ALL'");
                if (dtDoc.Rows.Count > 0)
                {
                    DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                    //CheckBox chk = new CheckBox();
                    dgvExclu.Columns.Add(CheckboxColumn);

                    dgvExclu.DataSource = dtDoc;
                    dgvExclu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DataGridViewColumn column = dgvExclu.Columns[0];
                    column.Width = 35;
                    dgvExclu.Columns[0].Resizable = DataGridViewTriState.False;
                    dgvExclu.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    dgvExclu.Columns["DOC_CODE"].Width = 80;


                    for (int i = 1; i < dgvExclu.Columns.Count; i++)
                    {
                        dgvExclu.Columns[i].ReadOnly = true;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvExclu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex == -1)
                return;

            if (dgvExclu.SelectedCells[0].ColumnIndex == 0)
            {
                foreach (DataGridViewCell dgvc in dgvExclu.SelectedCells)
                {
                    dgvExclu[0, dgvc.RowIndex].Value = true;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isExclusion)
            {
                frmAddExclusion frm = new frmAddExclusion();
                frm.exclutype = exclutype;
                frm.AddExclu = true;
                frm.EditExclu = false;
                frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                frm.ShowDialog();
            }
            else 
            {
                frmDocumentDetail frm = new frmDocumentDetail();
                frm.isAdd = true;
                frm.doctype = exclutype;
                frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                frm.ShowDialog();
            }
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            requeryDGV();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string status = "", selectedExcluCode = "", selectedDocCode = "";
            bool hasSelected = false;

            if (isExclusion)
            {
                foreach (DataGridViewRow row in dgvExclu.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        status = row.Cells[0].Value.ToString();
                        if (status == "True")
                        {
                            selectedExcluCode = row.Cells["EXCL_CODE"].Value.ToString();
                            hasSelected = true;
                            break;
                        }
                    }
                }
                if (!hasSelected)
                {
                    Msgbox.Show("No selected exclusion.");
                    return;
                }
                frmAddExclusion frm = new frmAddExclusion();
                frm.exclutype = exclutype;
                frm.AddExclu = false;
                frm.EditExclu = true;
                frm.EditExcluCode = selectedExcluCode;
                frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                frm.ShowDialog();
            }
            else
            {

                foreach (DataGridViewRow row in dgvExclu.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        status = row.Cells[0].Value.ToString();
                        if (status == "True")
                        {
                            selectedDocCode = row.Cells["DOC_CODE"].Value.ToString();
                            hasSelected = true;
                            break;
                        }
                    }
                }
                if (!hasSelected)
                {
                    Msgbox.Show("No selected document.");
                    return;
                }
                frmDocumentDetail frm = new frmDocumentDetail();
                frm.doctype  = exclutype;
                frm.isAdd = false;
                frm.EditDocCode = selectedDocCode;
                frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                frm.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string status = "";
            var dt = new DataTable();
            dt.Columns.Add("CODE");

            if (isExclusion)
            {
                foreach (DataGridViewRow row in dgvExclu.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        status = row.Cells[0].Value.ToString();
                        if (status == "True")
                        {
                            dt.Rows.Add(row.Cells["EXCL_CODE"].Value.ToString());
                        }
                    }
                }
                if (dt.Rows.Count <= 0)
                {
                    Msgbox.Show("No selected exclusion.");
                    return;
                }

                DialogResult res = Msgbox.Show("Are you sure you want to delete " + dt.Rows.Count + " selected exclusion?", "Confirmation");
                if (res == System.Windows.Forms.DialogResult.No)
                    return;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    crud.ExecNonQuery("DELETE FROM USER_CLAIM_EMAIL_EXCLUS WHERE EXCL_CODE = '" + dt.Rows[i]["CODE"].ToString() + "'");
                }
                Msgbox.Show(dt.Rows.Count + " selected exclusion deleted!");
            }

            else
            {
                foreach (DataGridViewRow row in dgvExclu.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        status = row.Cells[0].Value.ToString();
                        if (status == "True")
                        {
                            dt.Rows.Add(row.Cells["DOC_CODE"].Value.ToString());
                        }
                    }
                }
                if (dt.Rows.Count <= 0)
                {
                    Msgbox.Show("No selected document.");
                    return;
                }

                DialogResult res = Msgbox.Show("Are you sure you want to delete " + dt.Rows.Count + " selected document?", "Confirmation");
                if (res == System.Windows.Forms.DialogResult.No)
                    return;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    crud.ExecNonQuery("DELETE FROM USER_CLAIM_EMAIL_DOC WHERE DOC_CODE = '" + dt.Rows[i]["CODE"].ToString() + "'");
                }
                Msgbox.Show(dt.Rows.Count + " selected document deleted!");
            }
            requeryDGV();
        }
    }
}
