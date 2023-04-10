using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlitzerCore.Utilities;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public class FileDataAccess 
    {
        IDbContext DbContext { get; set; }
        public FileDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            try
            {
                return FindById(id) != null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException.InnerException.Message);
            }

        }

        public List<File> GetFiles(Trip aTrip)
        {
            try
            {
                return DbContext.Files.Where(x => x.OpportunityId == aTrip.ID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.InnerException.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public File Delete(int id)
        {
            try
            {
                var lFile = FindById(id);
                if (lFile == null)
                {
                    return null;
                }
                DbContext.Files.Remove(lFile);

                return lFile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.InnerException.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passedFile"></param>
        /// <returns></returns>
        public File Save(File passedFile)
        {
            try
            {
                if (passedFile.ID == 0)
                    DbContext.Files.Add(passedFile);
                else
                    DbContext.Files.Update(passedFile);

                DbContext.SaveChanges();

                return passedFile;
            }
            catch (Exception e1)
            {
                Logger.LogException("File Saved Failed", e1);
                throw new Exception(e1.Message);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<File> GetFiles(int aOrg)
        //{

        //    try
        //    {
        //        return DbContext.Files.Where(x => x.Owner.OrgID == aOrg).OrderByDescending (x => x.Date);
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogException("FileDataAccess.GetFiles(Exception)", e);
        //        throw e;
        //    }
        //}

        /// <summary>
        /// Temporary for migration 
        /// </summary>
        /// <param name="aOrg"></param>
        /// <returns></returns>
        public List<File> GetAllFiles()
        {

            try
            {
                return DbContext.Files.ToList();
            }
            catch (Exception e)
            {
                Logger.LogException("FileDataAccess.GetFiles(Exception)", e);
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public File FindById(int id)
        {

            return DbContext.Files.Where(x => x.ID == id).FirstOrDefault();
        }

        public List<FileType> GetFileTypes()
        {
            return DbContext.FileTypes.ToList();
        }
    }
}
