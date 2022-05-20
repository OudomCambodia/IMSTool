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
    public partial class frmViewRemark : Form
    {
        public string DocCode = "1"; //default
        DBS11SqlCrud crud = new DBS11SqlCrud();

        public frmViewRemark()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmViewRemark_Load(object sender, EventArgs e)
        {
            DataTable dtTemp = crud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE = " + DocCode).Tables[0];
            tbDocID.Text = DocCode;
            tbCommission.Text = dtTemp.Rows[0]["COMMISSION"].ToString();
            tbEffectiveDate.Text = dtTemp.Rows[0]["EFFECTIVE_DATE"].ToString();
            tbOtherInstruction.Text = dtTemp.Rows[0]["OTHER_INSTRUCTION"].ToString();
            tbRemarkNote.Text = dtTemp.Rows[0]["REMARK_NOTE"].ToString();
            tbRemarkRate.Text = dtTemp.Rows[0]["REMARK_RATE"].ToString();
            tbOriginalRate.Text = dtTemp.Rows[0]["ORIGINAL_RATE"].ToString();
            tbGroupDiscount.Text = dtTemp.Rows[0]["GROUP_DISCOUNT"].ToString();
            tbLoyaltyDiscount.Text = dtTemp.Rows[0]["LOYALTY_DISCOUNT"].ToString();
            tbNCD.Text = dtTemp.Rows[0]["NCD"].ToString();
            tbSpecialDiscount.Text = dtTemp.Rows[0]["SPECIAL_DISCOUNT"].ToString();
            tbFleetSizeDiscount.Text = dtTemp.Rows[0]["FLEET_SIZE_DISCOUNT"].ToString();
            tbDiscount.Text = dtTemp.Rows[0]["DISCOUNT"].ToString();
            tbLoading.Text = dtTemp.Rows[0]["LOADING"].ToString();
            tbFinalPremium.Text = dtTemp.Rows[0]["FINAL_PREMIUM_PER_PERSON"].ToString();
        }
    }
}
