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
    public partial class Accounting : Form
    {
        public Accounting()
        {
            InitializeComponent();
        }

        private void Accounting_Load(object sender, EventArgs e)
        {
            //Outstanding
            DataTable dt1 = My_DataTable_Extensions.ConvertExcelToDataTable(@"T:\Forte Project\Sources\Oct-Dec 2017\Used Files\Premium Register - Oct 17.xlsx", true);
            DataTable dt2 = My_DataTable_Extensions.ConvertExcelToDataTable(@"T:\Forte Project\Sources\Oct-Dec 2017\Used Files\Premium Register - Nov 17.xlsx", true);
            DataTable dt3 = My_DataTable_Extensions.ConvertExcelToDataTable(@"T:\Forte Project\Sources\Oct-Dec 2017\Used Files\Premium Register - Dec 17.xlsx", true);
            DataTable dt4 = My_DataTable_Extensions.ConvertExcelToDataTable(@"T:\Forte Project\Sources\Oct-Dec 2017\Used Files\OS from Sep17.xlsx", true);
            DataTable resOst = dt1.AsEnumerable().Union(dt2.AsEnumerable()).Union(dt3.AsEnumerable()).Union(dt4.AsEnumerable()).CopyToDataTable();
            dgvOst.DataSource = resOst;
            int Num = 0;
            double Amt = 0;
            if (int.TryParse(dgvOst.Rows.Count.ToString(), out Num) && double.TryParse(resOst.AsEnumerable().Sum(Rec => Rec.Field<double>("Amount")).ToString(), out Amt))
            {
                lbTotRec.Text = string.Format("{0:#,###0}", Num);
                lbTotAmo.Text = string.Format("{0:#,###0.##}", Amt);
            }

            //Collection
            DataTable dtC1 = My_DataTable_Extensions.ConvertExcelToDataTable(@"T:\Forte Project\Sources\Oct-Dec 2017\Used Files\Collection - Oct 17.xlsx", true);
            DataTable dtC2 = My_DataTable_Extensions.ConvertExcelToDataTable(@"T:\Forte Project\Sources\Oct-Dec 2017\Used Files\Collection - Nov 17.xlsx", true);
            DataTable dtC3 = My_DataTable_Extensions.ConvertExcelToDataTable(@"T:\Forte Project\Sources\Oct-Dec 2017\Used Files\Collection - Dec 17.xlsx", true);
            DataTable resCol = dtC1.AsEnumerable().Union(dtC2.AsEnumerable()).Union(dtC3.AsEnumerable()).CopyToDataTable();
            //var resGroup = from row in resCol.AsEnumerable()
            //               group row by new { DN_No = row.Field<string>("Debit Note No"), Deb_Name = row.Field<string>("Debtor Name"), Deb_Code = row.Field<string>("Debtor Code"), Pol_No = row.Field<string>("Policy No") } into grp
            //               select new
            //               {
            //                   Debit_no = grp.Key.DN_No,
            //                   Debtor_Code = grp.Key.Deb_Code,
            //                   Debtor_Name = grp.Key.Deb_Name,
            //                   Policy_No = grp.Key.Pol_No,
            //                   Amount = grp.Sum(r => r.Field<double>("Receipt Amount"))
            //               };
            DataTable resGroup = (resCol.AsEnumerable()
                .GroupBy(r => new { DN_No = r.Field<string>("Debit Note No"), Deb_Name = r.Field<string>("Debtor Name"), Deb_Code = r.Field<string>("Debtor Code"), Pol_No = r.Field<string>("Policy No") })
                .Select(g =>
                {
                    var row = resCol.NewRow();

                    row["Debit Note No"] = g.Key.DN_No;
                    row["Debtor Name"] = g.Key.Deb_Name;
                    row["Debtor Code"] = g.Key.Deb_Code;
                    row["Policy No"] = g.Key.Pol_No;
                    row["Receipt Amount"] = g.Sum(r => r.Field<double>("Receipt Amount"));

                    return row;
                })).CopyToDataTable();
            dgvCol.DataSource = resGroup;
            Num = 0;
            Amt = 0;
            if (int.TryParse(dgvCol.Rows.Count.ToString(), out Num) && double.TryParse(resGroup.AsEnumerable().Sum(Rec => Rec.Field<double>("Receipt Amount")).ToString(), out Amt))
            {
                lbRecCol.Text = string.Format("{0:#,###0}", Num);
                lbAmtCol.Text = string.Format("{0:#,###0.##}", Amt);
            }

            var result = (from t1 in resOst.AsEnumerable()
                          join t2 in resGroup.AsEnumerable()
                              on t1.Field<string>("DN_CN") equals t2.Field<string>("Debit Note No")
                          select new
                          {
                              Debit_No = t1.Field<string>("DN_CN"),
                              Policy_No = t1.Field<string>("POLICYNO_ENDO"),
                              Insured = t1.Field<string>("INSURED"),
                              Customer_Code = t1.Field<string>("CUSTOMER_CODE"),
                              Amount_Col = Math.Round(t2.Field<double>("Receipt Amount"), 2),
                              Amount_Ost = Math.Round(t1.Field<double>("Amount"), 2),
                              Amount = Math.Round(t1.Field<double>("Amount") - t2.Field<double>("Receipt Amount"), 2)
                          }).ToList();
            //var result = (from t1 in resOst.AsEnumerable()
            //              join t2 in resGroup.AsEnumerable()
            //                  on t1.Field<string>("DN_CN") equals t2.Field<string>("Debit Note No") into res
            //              from t3 in res.DefaultIfEmpty()
            //              select new
            //              {
            //                  Debit_No = t1.Field<string>("DN_CN"),
            //                  Policy_No = t1.Field<string>("POLICYNO_ENDO"),
            //                  Insured = t1.Field<string>("INSURED"),
            //                  Customer_Code = t1.Field<string>("CUSTOMER_CODE"),
            //                  Amount_Col = t3 == null? 0 : Math.Round(t3.Field<double>("Receipt Amount"), 2),
            //                  Amount_Ost = Math.Round(t1.Field<double>("Amount"), 2),
            //                  //Amount = Math.Round(t1.Field<double>("Amount") - t3.Field<double>("Receipt Amount"), 2)
            //              }).ToList();
            //var result = (from t1 in resGroup.AsEnumerable()
            //              join t2 in resOst.AsEnumerable()
            //                  on t1.Field<string>("Debit Note No") equals t2.Field<string>("DN_CN") into res
            //              from t3 in res.DefaultIfEmpty()
            //              select new
            //              {
            //                  Debit_No = t1.Field<string>("DN_CN"),
            //                  Policy_No = t1.Field<string>("POLICYNO_ENDO"),
            //                  Insured = t1.Field<string>("INSURED"),
            //                  Customer_Code = t1.Field<string>("CUSTOMER_CODE"),
            //                  Amount_Col = t3 == null ? 0 : Math.Round(t3.Field<double>("Receipt Amount"), 2),
            //                  Amount_Ost = Math.Round(t1.Field<double>("Amount"), 2),
            //                  //Amount = Math.Round(t1.Field<double>("Amount") - t3.Field<double>("Receipt Amount"), 2)
            //              }).ToList();

            DataTable refRes = CommonFunctions.ConvertToDataTable(result.AsEnumerable().Where(r => r.Amount == 0).ToList());//.AsEnumerable().Where(ite => ite.Field<double>("Amount") != 0).CopyToDataTable();
            dgvResult.DataSource = refRes;
            dgvResult.Columns[4].DefaultCellStyle.Format = "#,###0.##";
            Num = 0;
            Amt = 0;
            if (int.TryParse(dgvResult.Rows.Count.ToString(), out Num) && double.TryParse(refRes.AsEnumerable().Sum(Rec => Rec.Field<double>("Amount")).ToString(), out Amt))
            {
                lbTotRes.Text = string.Format("{0:#,###0}", Num);
                lbResAmt.Text = string.Format("{0:#,###0.##}", Amt);
            }
        }

        private void dgvOst_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvOst);
        }

        private void dgvCol_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvCol);
        }

        private void dgvResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dgvResult);
        }
    }
}
