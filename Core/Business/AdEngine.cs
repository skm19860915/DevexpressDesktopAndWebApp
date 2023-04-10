using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.Business
{
    public class AdEngine
    {
        protected IDbContext mContext = null;

        public AdEngine()
        {
        }

        public AdEngine(IDbContext aContext)
        {
            mContext = aContext;
        }

        public void GetInitialRange(out int? aStart, out int? aEnd, int aIndex, int aLength)
        {
            int lSize = 10;

            // Advance last seen index by 1
            aStart = aIndex + 1;
            aEnd = 0;

            if (aLength < 30)
                lSize = 5;

            // Check for a short list
            if (lSize > aLength)
                lSize = aLength;

            // Bad index, make them start at the beginning
            if (aStart >= aLength)
            {
                aStart = 0;
                aEnd = lSize - 1;
            }
            // The List will need to be broken into to parks
            else if ((aStart + lSize ) > aLength )
            {
                aStart = aIndex + 1;
                aEnd = aLength - 1;
            }
            // Normal situation and the results can be returned
            // in one continuous list
            else
            {
                aStart = aIndex + 1;
                aEnd = aStart + lSize - 1;
            }
        }
        public void GetEndRange(out int? aStart, out int? aEnd, int aIndex, int aLength)
        {
            int lSize = 10;

            var lNewStartIndex = aIndex + 1;
 
            if (aLength < 30)
                lSize = 5;

            if (lNewStartIndex == aLength )
            {
                aStart = null;
                aEnd = null;
            }
            else if ((lNewStartIndex + lSize) > (aLength))
            {
                // It wrapped around so must set start to initial index
                aStart = 0;
                if (lSize > aLength)
                    aEnd = aLength - (aLength - lNewStartIndex) - 1;
                else
                    aEnd = lSize - (aLength - lNewStartIndex) - 1;
            }
            else
            {
                aStart = null;
                aEnd = null;
            }
        }

        //public virtual Ad.AdTypes AdType { get { return Ad.AdTypes.Destination; } }

        protected List<Ad> DetermineResultsStream(int aAdID, List<Ad> lResults)
        {
            var lIndex = lResults.FindIndex(c => c.AdID == aAdID);
            List<Ad> lFinalResults = new List<Ad>();

            int? lStart = 0;
            int? lEnd = 0;
            GetInitialRange(out lStart, out lEnd, lIndex, lResults.Count());
            for (int i = lStart.Value; i <= lEnd.Value; i++)
                lFinalResults.AddRange(lResults.Where(x => x.AdID == lResults[i].AdID));

            GetEndRange(out lStart, out lEnd, lIndex, lResults.Count());

            if (lStart != null && lEnd != null)
                for (int i = lStart.Value; i <= lEnd.Value; i++)
                    lFinalResults.AddRange(lResults.Where(x => x.AdID == lResults[i].AdID));

            return lFinalResults;
        }
    }
}
