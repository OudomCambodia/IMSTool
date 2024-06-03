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

        public static void UploadFiles(string folderType, string folderName, string localFilePath)
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

        public static void OpenFiles(string s3FilePath)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DownloadFiles(string s3FilePath)
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
    }
}
