using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class BlockBusiness
    {
        const string ClassName = "BlockBusiness::";
        public IDbContext DbContext { get; set; }

        public BlockBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }

        public Block Create(string lTitle)
        {
            return new Block() { BlockTitle = lTitle + " Block Title", Title = lTitle + " Title" };
        }

        public int Save(Block aBlock)
        {
            string FuncName = ClassName + "Save";
            Logger.EnterFunction(FuncName);
            try
            {
                // Determine if the UI Created a PageMap by Default
                if (aBlock.PageMap != null && aBlock.PageMap.BlockId == 0 && aBlock.PageMap.PageId == 0)
                    aBlock.PageMap = null;
                else
                    if (aBlock.PageMap != null && aBlock.PageMap.Id == 0)
                    aBlock.PageMap.BlockId = aBlock.Id;

                return new BlockDataAccess(DbContext).Save(aBlock);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
    }
}
