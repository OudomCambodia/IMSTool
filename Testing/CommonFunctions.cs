using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Configuration;
using System.Net;
using System.Configuration;
using Microsoft.SharePoint.Client;
using System.IO;

namespace Testing
{
    static class CommonFunctions
    {
        static Dictionary<string, string> KhNumber = new Dictionary<string, string>()
            {
                {"0", "០"},
                {"1", "១"},
                {"2", "២"},
                {"3", "៣"},
                {"4", "៤"},
                {"5", "៥"},
                {"6", "៦"},
                {"7", "៧"},
                {"8", "៨"},
                {"9", "៩"}
            };
        static Dictionary<string, string> KhMonth = new Dictionary<string, string>()
            {
                {"January", "មករា"},
                {"February", "កុម្ភៈ"},
                {"March", "មីនា"},
                {"April", "មេសា"},
                {"May", "ឧសភា"},
                {"June", "មិថុនា"},
                {"July", "កក្កដា"},
                {"August", "សីហា"},
                {"September", "កញ្ញា"},
                {"October", "តុលា"},
                {"November", "វិច្ឆិកា"},
                {"December", "ធ្នូ"}
            };

        public static void GoTo(DataGridView dgv, TextBox tb, int Column, bool FullRowSelection = false)
        {
            dgv.ClearSelection();
            // dtPolicy.ForeColor = Color.Black;
            // dtPolicy.Rows.DefaultCellStyle.BackColor = Color.White;
            string strSearch = tb.Text.ToUpper();
            int iIndex = -1;
            int iFirstFoundRow = -1;
            bool bFound = false;
            if (strSearch != "")
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                /*  Select All Rows Starting With The Search string in row.cells[1] =
                second column. The search string can be 1 letter till a complete line
                If The dataGridView MultiSelect is set to true this will highlight 
                all found rows. If The dataGridView MultiSelect is set to false only 
                the last found row will be highlighted. Or if you jump out of the  
                foreach loop the first found row will be highlighted.*/

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if ((row.Cells[Column].Value.ToString().ToUpper()).IndexOf(strSearch) == 0)
                    {
                        iIndex = row.Index;
                        if (iFirstFoundRow == -1)  // First row index saved in iFirstFoundRow
                        {
                            iFirstFoundRow = iIndex;
                        }
                        dgv.Rows[iIndex].Selected = true; // Found row is selected
                        //dtPolicy.Rows[iIndex].DefaultCellStyle.ForeColor = Color.Yellow;
                        bFound = true; // This is needed to scroll de found rows in display
                        // break; //uncomment this if you only want the first found row.
                    }

                    if (FullRowSelection == false)
                        dgv.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                }

                if (bFound == false)
                {
                    dgv.ClearSelection(); // Nothing found clear all Highlights.
                    //dtPolicy.Rows[iIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    // Scroll found rows in display
                    dgv.FirstDisplayedScrollingRowIndex = iFirstFoundRow;
                }
            }
            else
            {
                dgv.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        public static void HighLightGrid(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (i % 2 == 1)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(230, 235, 255);
                }

                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 35, 35);
            }

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 9, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 9, 47);
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public static DataTable getDifferentRecords(DataTable FirstDataTable, DataTable SecondDataTable)
        {
            //Create Empty Table   
            DataTable ResultDataTable = new DataTable("ResultDataTable");

            //use a Dataset to make use of a DataRelation object   
            using (DataSet ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation   
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation   
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table   
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.   
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.   
                //foreach (DataRow parentrow in ds.Tables[1].Rows)
                //{
                //    DataRow[] childrows = parentrow.GetChildRows(r2);
                //    if (childrows == null || childrows.Length == 0)
                //        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                //}
                ResultDataTable.EndLoadData();
            }

            return ResultDataTable;
        }

        public static string KhDate(DateTime date)
        {
            string d = date.ToString("dd"), m = date.ToString("MMMM"), y = date.ToString("yyyy");
            string khd = "", khm = "", khy = "";
            foreach (char c in d)
            {
                khd += KhNumber[c.ToString()];
            }
            if (khd.Length == 1) khd = "០" + khd;

            khm = KhMonth[m];

            foreach (char c in y)
            {
                khy += KhNumber[c.ToString()];
            }

            return "ថ្ងៃទី" + khd + " ខែ" + khm + " ឆ្នាំ" + khy;
        }

        public static string KhDateNew(DateTime date)
        {
            string d = date.ToString("dd"), m = date.ToString("MMMM"), y = date.ToString("yyyy");
            string khm = "";
            if (d.Length == 1) d = "0" + d;

            khm = KhMonth[m];

            return "ថ្ងៃទី" + d + " ខែ" + khm + " ឆ្នាំ" + y;
        }

        public static string KhNum(double num)
        {
            string n = String.Format("{0:N}",Convert.ToDecimal(num));
            n = n.Replace("$", "");

            string result = "";

            foreach (char c in n)
            {
                if (KhNumber.ContainsKey(c.ToString()))
                    result += KhNumber[c.ToString()];
                else
                    result += c.ToString();
            }

            return result; 
        }

        public static string ToWords(this decimal value)
        {
            string decimals = "";
            string input = Math.Round(value, 2).ToString();

            if (input.Contains("."))
            {
                decimals = input.Substring(input.IndexOf(".") + 1);
                // remove decimal part from input
                input = input.Remove(input.IndexOf("."));
            }

            // strWords to store result           
            string strWords = "";

            if (decimals.Length > 0)   //has decimal
            {
                strWords = GetWords(input);

                //make decimal legnth = 2
                if (decimals.Length == 1)
                    decimals += "0";

                // if there is any decimal part convert it to words and add it to strWords.
                strWords += " AND " + GetWords(decimals) + " CENTS";
            }
            else //no decimal
            {
                if (input.Length <= 2)
                    strWords = GetWords(input);
                else
                {
                    string last2digit = input.Substring(input.Length - 2);
                    input = input.Substring(0, input.Length - 2);
                    input += "00";  //get last2digit and use 00 instead for GetWords()
                    strWords = GetWords(input) + " AND " + GetWords(last2digit);

                    if (last2digit == "00")
                        strWords += "Zero Cents";
                }
            }

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[ ]{2,}", System.Text.RegularExpressions.RegexOptions.None); //regex for replace double space

            return regex.Replace(strWords," ");
        }

        public static void UploadFile(ClientContext context, string uploadFolderUrl, string uploadFilePath)
        {
            var fileCreationInfo = new FileCreationInformation
            {
                Content = System.IO.File.ReadAllBytes(uploadFilePath),
                Overwrite = true,
                Url = Path.GetFileName(uploadFilePath)
            };
            var targetFolder = context.Web.GetFolderByServerRelativeUrl(uploadFolderUrl);
            //var file = context.Web.GetFileByServerRelativeUrl("");
            


            var uploadFile = targetFolder.Files.Add(fileCreationInfo);
            context.Load(uploadFile);
            context.ExecuteQuery();
        }

        private static string GetWords(string input)
        {
            // these are seperators for each 3 digit in numbers. you can add more if you want convert beigger numbers.
            string[] seperators = { "", " THOUSAND ", " MILLION ", " BILLION " };

            // Counter is indexer for seperators. each 3 digit converted this will count.
            int i = 0;

            string strWords = "";

            while (input.Length > 0)
            {
                // get the 3 last numbers from input and store it. if there is not 3 numbers just use take it.
                string _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);
                // remove the 3 last digits from input. if there is not 3 numbers just remove it.
                input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

                int no = int.Parse(_3digits);
                // Convert 3 digit number into words.
                _3digits = GetWord(no);

                // apply the seperator.
                _3digits += seperators[i];
                // since we are getting numbers from right to left then we must append resault to strWords like this.
                strWords = _3digits + strWords;

                // 3 digits converted. count and go for next 3 digits
                i++;
            }
            return strWords;
        }

        // your method just to convert 3digit number into words.
        private static string GetWord(int no)
        {
            string[] Ones =
		{
			"ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN",
			"TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
		};

            string[] Tens = { "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

            string word = "";

            if (no > 99 && no < 1000)
            {
                int i = no / 100;
                word = word + Ones[i - 1] + " HUNDRED ";
                no = no % 100;
            }

            if (no > 19 && no < 100)
            {
                int i = no / 10;
                word = word + Tens[i - 1] + " ";
                no = no % 10;
            }

            if (no > 0 && no < 20)
            {
                word = word + Ones[no - 1];
            }

            return word;
        }

        public static DataTable Provinces()
        {
            List<string> lstProvinces = new List<string>();
            lstProvinces.Add("Banteay Meanchey");
            lstProvinces.Add("Battambang");
            lstProvinces.Add("Kampong Cham");
            lstProvinces.Add("Kampong Chhnang");
            lstProvinces.Add("Kampong Speu");
            lstProvinces.Add("Kampong Thom");
            lstProvinces.Add("Kampot");
            lstProvinces.Add("Kandal");
            lstProvinces.Add("Koh Kong");
            lstProvinces.Add("Kratié");
            lstProvinces.Add("Mondulkiri");
            lstProvinces.Add("Phnom Penh");
            lstProvinces.Add("Prey Veng");
            lstProvinces.Add("Pursat");
            lstProvinces.Add("Ratanakiri");
            lstProvinces.Add("Siem Reap");
            lstProvinces.Add("Preah Sihanouk");
            lstProvinces.Add("Stung Treng");
            lstProvinces.Add("Svay Rieng");
            lstProvinces.Add("Takéo");
            lstProvinces.Add("Oddar Meanchey");
            lstProvinces.Add("Kep");
            lstProvinces.Add("Pailin");
            lstProvinces.Add("Tboung Khmum");
            lstProvinces.Add("Preah Vihear");

            var dtProvinces = new DataTable();
            dtProvinces.Columns.Add("Name", typeof(string));
            dtProvinces.Columns.Add("Value", typeof(string));

            foreach (var province in lstProvinces)
            {
                var drProvince = dtProvinces.NewRow();
                drProvince["Name"] = province;
                drProvince["Value"] = province;
                dtProvinces.Rows.Add(province);
            }

            dtProvinces.DefaultView.Sort = "Name ASC";
            dtProvinces = dtProvinces.DefaultView.ToTable();

            return dtProvinces;
        }

        /// <summary>
        /// This class is an implementation of the 'IComparer' interface.
        /// </summary>
        public class ListViewColumnSorter : IComparer
        {
            /// <summary>
            /// Specifies the column to be sorted
            /// </summary>
            private int ColumnToSort;

            /// <summary>
            /// Specifies the order in which to sort (i.e. 'Ascending').
            /// </summary>
            private SortOrder OrderOfSort;

            /// <summary>
            /// Case insensitive comparer object
            /// </summary>
            private CaseInsensitiveComparer ObjectCompare;

            /// <summary>
            /// Class constructor. Initializes various elements
            /// </summary>
            public ListViewColumnSorter()
            {
                // Initialize the column to '0'
                ColumnToSort = 0;

                // Initialize the sort order to 'none'
                OrderOfSort = SortOrder.None;

                // Initialize the CaseInsensitiveComparer object
                ObjectCompare = new CaseInsensitiveComparer();
            }

            /// <summary>
            /// This method is inherited from the IComparer interface. It compares the two objects passed using a case insensitive comparison.
            /// </summary>
            /// <param name="x">First object to be compared</param>
            /// <param name="y">Second object to be compared</param>
            /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
            public int Compare(object x, object y)
            {
                int compareResult;
                ListViewItem listviewX, listviewY;

                // Cast the objects to be compared to ListViewItem objects
                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                // Compare the two items
                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

                // Calculate correct return value based on object comparison
                if (OrderOfSort == SortOrder.Ascending)
                {
                    // Ascending sort is selected, return normal result of compare operation
                    return compareResult;
                }
                else if (OrderOfSort == SortOrder.Descending)
                {
                    // Descending sort is selected, return negative result of compare operation
                    return (-compareResult);
                }
                else
                {
                    // Return '0' to indicate they are equal
                    return 0;
                }
            }

            /// <summary>
            /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
            /// </summary>
            public int SortColumn
            {
                set
                {
                    ColumnToSort = value;
                }
                get
                {
                    return ColumnToSort;
                }
            }

            /// <summary>
            /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
            /// </summary>
            public SortOrder Order
            {
                set
                {
                    OrderOfSort = value;
                }
                get
                {
                    return OrderOfSort;
                }
            }

        }

        public static string SendEmail(NetworkCredential credential, MailMessage msg, string sectionName = "default")
        {
            string result = "";
            List<string> lstAttPath = new List<string>();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook._MailItem mailItem = (Microsoft.Office.Interop.Outlook._MailItem)outlookApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                mailItem.To = msg.To.ToString().Replace(",", ";");
                mailItem.CC = msg.CC.ToString().Replace(",", ";");
                mailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                mailItem.Subject = msg.Subject;

                foreach (var attachment in msg.Attachments)
                {
                    Stream contentStream = attachment.ContentStream;

                    MemoryStream memoryStream = new MemoryStream();
                    contentStream.CopyTo(memoryStream);

                    // Reset the position of the MemoryStream object to the beginning
                    memoryStream.Position = 0;

                    FileInfo fileInfo = new FileInfo(attachment.Name);

                    // Create a new FileStream object using the FileInfo.FullName property and the FileMode.Create enumeration value
                    using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Create))
                    {
                        // Copy the content of the MemoryStream object to the FileStream object
                        memoryStream.CopyTo(fileStream);
                        mailItem.Attachments.Add(fileStream.Name, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, 1, attachment.Name);
                        lstAttPath.Add(fileStream.Name);
                    }
                }

                string htmlBody = null;
                foreach (AlternateView view in msg.AlternateViews)
                {
                    if (view.ContentType.MediaType == "text/html")
                    {
                        using (var reader = new StreamReader(view.ContentStream, Encoding.UTF8))
                        {
                            htmlBody = reader.ReadToEnd();
                        }
                    }
                }
                mailItem.HTMLBody = htmlBody;

                if (string.IsNullOrEmpty(htmlBody))
                {
                    mailItem.Body = msg.Body;
                }

                string currentDir = Directory.GetCurrentDirectory();
                EmbedImage(mailItem, string.Concat(currentDir, @"\Html\forte-general-logo-red.png"), "forte-general-logo-red");
                EmbedImage(mailItem, string.Concat(currentDir, @"\Html\fb.png"), "FB_logo");
                EmbedImage(mailItem, string.Concat(currentDir, @"\Html\yt.png"), "YT_logo");
                EmbedImage(mailItem, string.Concat(currentDir, @"\Html\mail.png"), "Mail_logo");
                EmbedImage(mailItem, string.Concat(currentDir, @"\Html\linkedin.png"), "linkedin");

                mailItem.Send();

                if (lstAttPath.Count() > 0)
                {
                    foreach (var attPath in lstAttPath)
                    {
                        if (System.IO.File.Exists(attPath))
                            System.IO.File.Delete(attPath);
                    }
                }

                //var smtp = new CustomSmtpClient(credential, sectionName);
                //smtp.Send(msg);
                //msg.Attachments.Dispose();
                //msg.Dispose();
                //result = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Authentication unsuccessful"))
                {
                    string newpw = InputBox.Show("Please input your email password (PC log in password)", "Email Verification");
                    credential.Password = newpw;
                    var smtp = new CustomSmtpClient(credential, sectionName);
                    smtp.Send(msg);
                    msg.Attachments.Dispose();
                    msg.Dispose();
                    result = "";



                    CRUD crud = new CRUD();
                    string encpw = Cipher.Encrypt(newpw, "Forte@2017");
                    crud.ExecNonQuery("UPDATE USER_PRINT_SYSTEM SET EMAIL_PW = '" + encpw + "' WHERE EMAIL = '" + credential.UserName + "'");



                    return result;
                }
                return ex.Message;
            }
            Cursor.Current = Cursors.AppStarting;

            return result;
        }

        public static void EmbedImage(Microsoft.Office.Interop.Outlook._MailItem mailItem, string imgPath, string cid)
        {
            Microsoft.Office.Interop.Outlook.Attachment image = mailItem.Attachments.Add(imgPath, Microsoft.Office.Interop.Outlook.OlAttachmentType.olEmbeddeditem, null, string.Empty);
            image.PropertyAccessor.SetProperty("http://schemas.microsoft.com/mapi/proptag/0x3712001E", cid);
        }

        public class NotiType
        {
            public static string REJECTED = "REJECTED";
            public static string PENDING = "PENDING";
            public static string REVERSED = "REVERSED";
        }
    }

    public class CustomSmtpClient
    {
        private readonly SmtpClient _smtpClient;

        public CustomSmtpClient(NetworkCredential credential, string sectionName = "default")
        {
            SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("mailSettings/" + sectionName);
            _smtpClient = new SmtpClient();
            if (section != null)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                _smtpClient.Port = section.Network.Port;
                _smtpClient.Host = section.Network.Host;
                _smtpClient.EnableSsl = section.Network.EnableSsl;
                _smtpClient.DeliveryMethod = section.DeliveryMethod;
                _smtpClient.UseDefaultCredentials = section.Network.DefaultCredentials;
                _smtpClient.Credentials = credential;
                //_smtpClient.Credentials = new NetworkCredential
                //{
                // UserName = section.Network.UserName,
                // Password = section.Network.Password
                //};
            }
        }

        public void Send(MailMessage message)
        {
            _smtpClient.Send(message);
        }
    }
    
}
