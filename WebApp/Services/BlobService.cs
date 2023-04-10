using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BlitzerCore.Models.UI;

namespace WebApp.Services
{
    public class BlobStorageService
    {
        string accessKey = string.Empty;

        public BlobStorageService(IConfiguration config)
        {
            this.accessKey = config["AzureStorage:StorageConnectionString"];
        }

    }
}
