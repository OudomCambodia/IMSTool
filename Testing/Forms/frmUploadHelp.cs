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
    public partial class frmUploadHelp : Form
    {
        public frmUploadHelp()
        {
            InitializeComponent();
        }

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable uploadtb = new DataTable();
            DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = "Document Type";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Customer Code";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Product Type";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Agent/Broker/Finance Code";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Priority";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "To Be Finished On";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Print Card";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Submit Via";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Policy No";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Quot No";
            uploadtb.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Commission";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Effective Date";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Other Instruction";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Remark Note";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Remark Rate";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Original Rate";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Group Discount";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Loyalty Discount";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "NCD";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Special Discount";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Fleet/Size Discount";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Discount";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Loading";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Final Premium Per Person";
            uploadtb.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Attachment";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Premium";
            uploadtb.Columns.Add(dataColumn);
            //Update by adding format to Acleda Sale Person - to do project for Acelda Request Brom Vichhay - Updated Southeane 09-01-24
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "PremiumType";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "ClientCatag";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "ClientDetails";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "StaffID";
            uploadtb.Columns.Add(dataColumn);
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "SalePersonName";
            uploadtb.Columns.Add(dataColumn);

            uploadtb.Columns["To Be Finished On"].DataType = typeof(DateTime);

            DataRow dr = uploadtb.NewRow();
            dr["Document Type"] = "P";
            dr["Customer Code"] = "C#########";
            dr["Product Type"] = "GPA";
            dr["Agent/Broker/Finance Code"] = "A####";
            dr["Priority"] = "U";
            dr["To Be Finished On"] = new DateTime(2020,1,1);
            dr["Print Card"] = "Yes";
            dr["Submit Via"] = "HC";
            uploadtb.Rows.Add(dr);

            My_DataTable_Extensions.ExportToExcel(uploadtb, "");
            Cursor.Current = Cursors.AppStarting;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
