using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Utilities;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Primitives;


namespace NUnitTests.Business
{
    //public class WebHostMock : IWebHostEnvironment
    //{
    //    public string WebRootPath { get; set; }
    //    public string ApplicationName { get; set; }
    //    IFileProvider ContentRootFileProvider { get; set; }
    //    public string ContentRootPath { get; set; }
    //    public string EnvironmentName { get; set; }
    //    IFileProvider IWebHostEnvironment.WebRootFileProvider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //    IFileProvider IHostEnvironment.ContentRootFileProvider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //}
}
