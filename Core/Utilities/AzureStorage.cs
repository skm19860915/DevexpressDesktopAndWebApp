using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BlitzerCore.Utilities
{
    public class AzureStorage
    {
        string accessKey = string.Empty;
        const string PHOTO_BLOB_LOCATION = "images";
        const string VIDEO_BLOB_LOCATION = "videos";
        IDbContext DbContext { get; set; }
        public AzureStorage(string aAzureConnectionString, IDbContext aContext)
        {
            accessKey = aAzureConnectionString;
            DbContext = aContext;
        }

        private Graphic Save(string aPath, MediaFormats aDimensions)
        {
            using (var stream = new MemoryStream())
            {
                var lPath = aPath;
                Graphic lGraphic = null;
                if (aDimensions == MediaFormats.MPEG)
                    lGraphic = new Video() { MediaFormat = aDimensions, Location = lPath };
                else
                    lGraphic = new Photo() { MediaFormat = aDimensions, Location = lPath };

                var lMediaDA = new MediaDataAccess(DbContext);
                lMediaDA.Save(lGraphic);

                return lGraphic;
            }
        }

        private int Save(string aPath, Video aVideo)
        {
            var lMDA = new MediaDataAccess(DbContext);
            aVideo.Location = aPath;
            return lMDA.Save(aVideo);
        }

        public string UploadFileToBlob(string strFileName, byte[] fileData)
        {
            try
            {

                var _task = System.Threading.Tasks.Task.Run(() => this.UploadFileToBlobAsync(strFileName, fileData, DataHelper.MimeType(strFileName)));
                _task.Wait();
                string fileUrl = _task.Result;
                return fileUrl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public Graphic UploadVideoToBlob(string strFileName, Content aContent, MediaFormats aDimensions)
        {
            return UploadMediaToBlob(strFileName, aContent, aDimensions, VIDEO_BLOB_LOCATION);
        }

        public Graphic UploadVideoToBlob(string strFileName, Media aMedia, MediaFormats aDimensions)
        {
            return UploadMediaToBlob(strFileName, aMedia, aDimensions, VIDEO_BLOB_LOCATION);
        }

        public Graphic UploadPhotoToBlob(string strFileName, Media aMedia, MediaFormats aDimensions)
        {
            return UploadMediaToBlob(strFileName, aMedia, aDimensions, PHOTO_BLOB_LOCATION);
        }

        private Graphic UploadMediaToBlob(string strFileName, Content aContent, MediaFormats aDimensions, string aLocation)
        {
            try
            {

                var _task = System.Threading.Tasks.Task.Run(() => this.UploadMediaToBlobAsync(GenerateFileName(strFileName, aContent, aDimensions), System.IO.File.ReadAllBytes(strFileName), DataHelper.MimeType(strFileName), aLocation));
                _task.Wait();
                string fileUrl = _task.Result;
                return Save(fileUrl, aDimensions);
            }
            catch (Exception ex)
            {
                Logger.LogException("Failed to Upload File to Azure and Store", ex);
                throw (ex);
            }
        }

        private Graphic UploadMediaToBlob(string strFileName, Media aMedia, MediaFormats aDimensions, string aLocation)
        {
            try
            {

                var _task = System.Threading.Tasks.Task.Run(() => this.UploadMediaToBlobAsync(GenerateFileName(strFileName, aMedia, aDimensions), System.IO.File.ReadAllBytes(strFileName), DataHelper.MimeType(strFileName), aLocation));
                _task.Wait();
                string fileUrl = _task.Result;
                return Save(fileUrl, aDimensions);
            }
            catch (Exception ex)
            {
                Logger.LogException("Failed to Upload File to Azure and Store", ex);
                throw (ex);
            }
        }
        //public async void DeleteBlobData(string fileUrl)
        //{
        //    Uri uriObj = new Uri(fileUrl);
        //    string BlobName = Path.GetFileName(uriObj.LocalPath);

        //    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
        //    CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        //    string strContainerName = "uploads";
        //    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);

        //    string pathPrefix = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/";
        //    CloudBlobDirectory blobDirectory = cloudBlobContainer.GetDirectoryReference(pathPrefix);
        //    // get block blob refarence    
        //    CloudBlockBlob blockBlob = blobDirectory.GetBlockBlobReference(BlobName);

        //    // delete blob from container        
        //    await blockBlob.DeleteAsync();
        //}

        private string GenerateFileName(string fileName, Media aMedia, MediaFormats aDim)
        {
            string strFileName = string.Empty;
            string lPrefix = "";
//#if DEBUG
//            lPrefix = "TESTING_";
//#endif
            string strName = "";
            strName += aDim.ToString().Split('_')[1] + "/";
            strName += lPrefix + aMedia.Id.ToString() + "." + fileName.Split('.')[1];
            return strName;
        }

        private string GenerateFileName(string fileName, Content aContent, MediaFormats aDim)
        {
            string strFileName = string.Empty;
            string lPrefix = "";
#if DEBUG
            lPrefix = "TESTING_";
#endif
            string strName = lPrefix + Path.DirectorySeparatorChar + "C_" + aContent.Id.ToString() + "." + fileName.Split('.')[1];
            return strName;
        }

        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }

        private async Task<string> UploadMediaToBlobAsync(string strFileName, byte[] fileData, string fileMimeType, string aLocation)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(aLocation);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            if (strFileName != null && fileData != null)
            {
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(strFileName);
                cloudBlockBlob.Properties.ContentType = fileMimeType;
                await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                return cloudBlockBlob.Uri.AbsoluteUri;
            }
            return "";
        }


        private async Task<string> UploadFileToBlobAsync(string strFileName, byte[] fileData, string fileMimeType)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = "uploads";
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
            string fileName = this.GenerateFileName(strFileName);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            if (fileName != null && fileData != null)
            {
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                cloudBlockBlob.Properties.ContentType = fileMimeType;
                await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                return cloudBlockBlob.Uri.AbsoluteUri;
            }
            return "";
        }

    }
}
