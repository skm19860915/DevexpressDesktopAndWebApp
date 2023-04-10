using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class FileBusiness
    {
        const string ClassName = "FileBusiness::";
        private IDbContext DbContext { get; set; }
        public FileBusiness(IDbContext mContext)
        {
            this.DbContext = mContext;
        }
        public List<FileType> GetFileTypes()
        {
            return new FileDataAccess(DbContext).GetFileTypes();
        }

        public List<File> GetFiles(Trip aTrip)
        {
            return new FileDataAccess(DbContext).GetFiles(aTrip);
        }
        public int Save(File aModel)
        {
            string FuncName = $"{ClassName}Save (File {aModel.Name})";
            Logger.LogMessage(FuncName);
            var NDA = new FileDataAccess(DbContext);
            var ss = NDA.Save(aModel);
            return ss.ID;
        }
    }
}
