using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.Data;
using Testing;
using System.Data.SqlClient;

namespace Testing
{
    public static class AWSHelper
    {
        public static NameValueCollection awsConfig = (NameValueCollection)ConfigurationManager.GetSection("awsConfig");
        public static string AccessKey = awsConfig["accessKey"];
        public static string SecretKey = awsConfig["secretKey"];
        public static RegionEndpoint BucketRegion = RegionEndpoint.APSoutheast1;
        public static string BucketName = awsConfig["bucketName"];

        public static AmazonS3Client s3Client = new AmazonS3Client(AccessKey, SecretKey, BucketRegion);

        public static void UploadFiles(string folderType, string folderName, string localFilePath, string productType)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                string key = string.Format("{0}/{1}/{2}", folderType, folderName, Path.GetFileName(localFilePath));

                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = BucketName,
                    FilePath = localFilePath,
                    StorageClass = S3StorageClass.IntelligentTiering,
                    PartSize = 10485760, // 10 MB.  
                    Key = key,
                    CannedACL = S3CannedACL.Private,
                };

                fileTransferUtility.UploadAsync(fileTransferUtilityRequest).GetAwaiter().GetResult();

                InsertLog((folderType.Contains(@"/") ? folderType.Split('/')[0].ToUpper() : folderType.ToUpper()), "UPLOAD", frmLogIn.Usert.ToUpper(), productType, Path.GetFileName(localFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Tuple<string, string>> RetrieveFiles(string folderType, string folderName)
        {
            try
            {
                ListObjectsRequest request = new ListObjectsRequest();
                request.BucketName = BucketName;
                request.Prefix = folderType + "/" + folderName + "/"; ;

                var response = s3Client.ListObjectsAsync(request).GetAwaiter().GetResult();

                List<Tuple<string, string>> objectNames = new List<Tuple<string, string>>();

                foreach (S3Object obj in response.S3Objects)
                {
                    string key = obj.Key;
                    //string[] keyParts = key.Split('/');
                    objectNames.Add(Tuple.Create(Path.GetFileName(key), "https://" + BucketName + ".s3.ap-southeast-1.amazonaws.com/" + obj.Key));
                }

                return objectNames;
            }
            catch (Exception)
            {
                return new List<Tuple<string, string>>();
            }
        }

        public static void OpenFiles(string s3FilePath, string productType)
        {
            try
            {
                string folderType = s3FilePath.Split('/')[3].ToUpper();

                string tempFilePath = Path.GetTempFileName();
                string dirPath = Path.GetDirectoryName(tempFilePath);

                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = BucketName,
                    Key = GetKeyAndFileNameFromS3FilePath(s3FilePath).Item1
                };

                using (GetObjectResponse response = s3Client.GetObject(request))
                using (Stream responseStream = response.ResponseStream)
                using (FileStream fileStream = new FileStream(Path.Combine(dirPath, GetKeyAndFileNameFromS3FilePath(s3FilePath).Item2), FileMode.Create, FileAccess.Write))
                {
                    responseStream.CopyTo(fileStream);

                    Process process = new Process();
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = string.Concat(dirPath, @"\", GetKeyAndFileNameFromS3FilePath(s3FilePath).Item2),
                        UseShellExecute = true
                    };

                    process.EnableRaisingEvents = true;
                    process.Exited += (sender, e) => File.Delete(string.Concat(dirPath, @"\", GetKeyAndFileNameFromS3FilePath(s3FilePath).Item2));

                    process.Start();
                }

                InsertLog(s3FilePath.Split('/')[3].ToUpper(), "RETRIEVE", frmLogIn.Usert.ToUpper(), productType, GetKeyAndFileNameFromS3FilePath(s3FilePath).Item2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DownloadFiles(string s3FilePath, string productType)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = BucketName,
                        Key = GetKeyAndFileNameFromS3FilePath(s3FilePath).Item1
                    };

                    using (GetObjectResponse response = s3Client.GetObject(request))
                    using (Stream responseStream = response.ResponseStream)
                    using (FileStream fileStream = new FileStream(Path.Combine(dialog.SelectedPath, GetKeyAndFileNameFromS3FilePath(s3FilePath).Item2), FileMode.Create, FileAccess.Write))
                    {
                        responseStream.CopyTo(fileStream);
                        InsertLog(s3FilePath.Split('/')[3].ToUpper(), "DOWNLOAD", frmLogIn.Usert.ToUpper(), productType, GetKeyAndFileNameFromS3FilePath(s3FilePath).Item2);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteFiles(string s3FilePath)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = BucketName,
                    Key = GetKeyAndFileNameFromS3FilePath(s3FilePath).Item1
                };

                s3Client.DeleteObjectAsync(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ExtractFileNameFromKey(string key)
        {
            return System.IO.Path.GetFileName(key);
        }

        public static Tuple<string, string> GetKeyAndFileNameFromS3FilePath(string s3FilePath)
        {
            string key = string.Empty;
            string fileName = string.Empty;

            const string s3Prefix = "https://imstools-docs.s3.ap-southeast-1.amazonaws.com/";
            if (!s3FilePath.StartsWith(s3Prefix))
            {
                throw new ArgumentException("The provided URL does not match the expected S3 URL format.");
            }

            key = s3FilePath.Substring(s3Prefix.Length);
            fileName = System.IO.Path.GetFileName(key);

            return Tuple.Create(key, fileName);
        }

        public static void InsertLog(string folderType, string logType, string userName, string productType, string fileName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBS11SQLConnectionString"].ConnectionString;
            string query = "INSERT INTO tbAttachmentLog(FOLDER_TYPE, LOG_TYPE, USER_NAME, PRODUCT_TYPE, FILE_NAME, CREATED_DATE) VALUES(@folder_type, @log_type, @user_name, @product_type, @file_name, @created_date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the SqlCommand and assign the query and connection
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Define and add the parameters with values
                    cmd.Parameters.Add(new SqlParameter("@folder_type", SqlDbType.VarChar)).Value = folderType;
                    cmd.Parameters.Add(new SqlParameter("@log_type", SqlDbType.VarChar)).Value = logType;
                    cmd.Parameters.Add(new SqlParameter("@user_name", SqlDbType.VarChar)).Value = userName;
                    cmd.Parameters.Add(new SqlParameter("@product_type", SqlDbType.VarChar)).Value = productType;
                    cmd.Parameters.Add(new SqlParameter("@file_name", SqlDbType.VarChar)).Value = fileName;
                    cmd.Parameters.Add(new SqlParameter("@created_date", SqlDbType.DateTime)).Value = DateTime.Now;

                    try
                    {
                        // Open the connection
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occur
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }  
        }
    }
}
