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
    public partial class ViewTicketRequest : Form
    {
        CRUD crud = new CRUD();
        DBS11SqlCrud sqlcrud = new DBS11SqlCrud();
        public ViewTicketRequest()
        {
            InitializeComponent();
        }

        private void ViewTicketRequest_Load(object sender, EventArgs e)
        {
            DataTable dt = sqlcrud.LoadData("SELECT TOP 1000 [ticketID] " +
            ",[Requestor] " +
            ",[Subject] " +
            ",[Reason] " +
            ",[Status] " +
            ",[Owner] " +
            ",[Describe] " +
            ",[Filepath] " +
            ",[CreateDate] " +
            ",[CountTicket] " +
            ",[TicketStatus] " +
            "FROM [DocumentControlDB].[dbo].[tbTicketRequests] " +
            "WHERE [TicketStatus] = 'Open' and [CreateDate] = '" + DateTime.Now.ToShortDateString()+ " 00:00:00.000'").Tables[0];
            dataGridView1.DataSource = dt;
                 
        }
    }
}
