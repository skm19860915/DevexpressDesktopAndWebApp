using BlitzerCore.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using BlitzerCore.DataAccess;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BlitzerCore.Business
{
    public class MediaBusiness
    {
        private IDbContext mContext;
        private readonly IConfiguration mConfig;

        public MediaBusiness(IDbContext mContext, IConfiguration aConfig)
        {
            this.mContext = mContext;
            mConfig = aConfig;
        }

        public int Save(Media aMedia)
        {
            return new MediaDataAccess(mContext).Save(aMedia);
        }
    }
}