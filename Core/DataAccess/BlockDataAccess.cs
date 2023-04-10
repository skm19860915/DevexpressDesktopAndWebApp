using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BlitzerCore.Utilities;
using System;

namespace BlitzerCore.DataAccess
{
    public class BlockDataAccess
    {
        const string ClassName = "BlockDataAccess::";
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<Block> Table { get; set; }

        public BlockDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.Blocks;
        }

        public Block Get(int aID)
        {
            try
            {
                int lCnt = Table.Count(x=>x.Id == aID);

                var lOutput = Table
                    //.Include(x => x.Media).ThenInclude(m => m.ThumbNail)
                    //.Include(x => x.Media).ThenInclude(m => m.Size1600x1200)
                    //.Include(x => x.Media).ThenInclude(m => m.Size560x460)
                    .FirstOrDefault(x => x.Id == aID);

                return lOutput;
            } catch (Exception e)
            {
                Logger.LogException("Failed to retrieve block", e);
                return null;
            }
        }

        public List<Block> GetAll()
        {
            string FuncName = ClassName + $"GetAll :";

            try
            {
                var lOutput = Table
                    .Include(x => x.Media).ThenInclude(m => m.Size1600x1200)
                    .Include(x => x.Media).ThenInclude(m => m.ThumbNail)
                    .Include(x => x.Media).ThenInclude(m => m.Size560x460)
                    .Include(x => x.PageMap).ThenInclude(m => m.Page).ThenInclude(y => y.HeaderImage).ThenInclude(z => z.Media).ThenInclude(q => q.Size1600x1200);

                if (lOutput == null)
                    return new List<Block>();

                var lCnt = lOutput.Count();
                if (lCnt == 0)
                    return new List<Block>();

                if (lOutput != null && lOutput.Count() > 0)
                    return lOutput.ToList();
            } catch ( Exception e )
            {
                Logger.LogException($"{FuncName} failed", e);
            }

            return new List<Block>();
        }

        public int Save(Block aBlock )
        {
            string FuncName = ClassName + $"Save (Block={aBlock.Id})";
            if (aBlock.Id > 0)
                mContext.Blocks.Update(aBlock);
            else
                mContext.Blocks.Add(aBlock);

            try
            {
                var lCnt = mContext.SaveChanges();
                Logger.LogInfo(FuncName + $" - Saved  {lCnt} rows");
                return lCnt;
            } catch ( Exception e )
            {
                Logger.LogException(FuncName + " Failed to save block", e);
                return 0;
            }
        }
    }
}

