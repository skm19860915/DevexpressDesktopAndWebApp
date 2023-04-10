using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.WebApi
{
    [Route("api/[controller]/[action]")]
    public class FileUploaderAzureAccessApiController : Controller
    {
        const long MaxBlobSize = 1048576;
        const string AzureBlobAccountName = "blitzerblobs";
        const string AzureBlobKey = "3hJlPM3Uv2FEdNW2+G63nlbrw+p0O8dOsowxGhCntw6mB3dVQzjJCl8vXyoLHzTp1HWhNvnKNoWUCZhUYmGNAA==";
        const string AzureBlobContainer = "maincontainer";
        CloudBlobClient _client;
        CloudBlobClient Client
        {
            get
            {
                if (this._client == null)
                {
                    //AzureStorageAccount accountModel = AzureStorageAccount.FileUploader;
                    var credentials = new StorageCredentials(AzureBlobAccountName, AzureBlobKey);
                    var account = new CloudStorageAccount(credentials, true);
                    this._client = account.CreateCloudBlobClient();
                }
                return this._client;
            }
        }

        CloudBlobContainer _container;
        CloudBlobContainer Container
        {
            get
            {
                if (this._container == null)
                {
                    this._container = Client.GetContainerReference("documents");
                }
                return this._container;
            }
        }
        [Route("api/file-uploader-azure-access", Name = "FileUploaderAzureAccessApi")]
        public object Process(string command, string blobName)
        {
            try
            {
                return UploadBlob(blobName);
            }
            catch
            {
                return CreateErrorResult();
            }
        }

        object UploadBlob(string blobName)
        {
            if (blobName.Contains("/"))
                return CreateErrorResult("Invalid blob name.");

            string prefix = Guid.NewGuid().ToString("N");
            string fullBlobName = $"{prefix}_{blobName}";
            CloudBlockBlob blob = Container.GetBlockBlobReference(fullBlobName);
            bool lExists = blob.Exists();

            if (blob.Exists() && blob.Properties.Length > MaxBlobSize)
            {
                blob.Delete();
                return CreateErrorResult();
            }

            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(1),
                Permissions = SharedAccessBlobPermissions.Write
            };
            string url = blob.Uri + blob.GetSharedAccessSignature(policy, null, null, SharedAccessProtocol.HttpsOnly, null);

            return CreateSuccessResult(url);
        }

        object CreateSuccessResult(string url, string url2 = null)
        {
            return new
            {
                success = true,
                accessUrl = url,
                accessUrl2 = url2
            };
        }

        object CreateErrorResult(string error = null)
        {
            if (string.IsNullOrEmpty(error))
                error = "Unspecified error.";

            return new
            {
                success = false,
                error = error
            };
        }
    }
}
