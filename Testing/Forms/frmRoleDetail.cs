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
    public partial class frmRoleDetail : Form
    {
        private CRUD crud = new CRUD();
        private string groupCode = frmUserRoleManagement.GroupCode;

        public frmRoleDetail()
        {
            InitializeComponent();
        }

        private void frmRoleDetail_Load(object sender, EventArgs e)
        {
            lblGroupName.Text = string.Concat("User Under '", groupCode, "' Group");

            var userInfos = crud.ExecQuery("select USER_CODE, USER_NAME, USER_CREATE_DATE from USER_PRINT_SYSTEM where TYPE = '" + groupCode + "' order by USER_CREATE_DATE desc");
            var userInfoCount = userInfos.Rows.Count;

            if (userInfos.Rows.Count > 0)
            {
                for (int i = 0; i < userInfos.Rows.Count; i++)
                {
                    var userCode = userInfos.Rows[i]["USER_CODE"].ToString();
                    var userName = userInfos.Rows[i]["USER_NAME"].ToString();
                    var userCreateDate = userInfos.Rows[i]["USER_CREATE_DATE"].ToString().Split(' ')[0].Trim();

                    string[] items = { userCode, userName, userCreateDate };
                    var listViewItem = new ListViewItem(items);
                    lstUserInfo.Items.Add(listViewItem);
                }
                lblTotalUser.Text = string.Concat(userInfoCount.ToString(), " ", userInfoCount > 1 ? "Users" : "User");
            }
            else
                lblTotalUser.Text = "0 User";
        }

        private void lstUserInfo_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                e.Item.Selected = false;
        }
    }
}
