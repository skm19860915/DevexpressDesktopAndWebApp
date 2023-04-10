using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class ResortPageBusiness
    {
        const int RESORT_PAGE_TYPE = 1;
        public IDbContext DbContext { get; set; }

        public ResortPageBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }

        public BlitzerCore.Models.UI.UIResortPage CreateNewPage(Hotel aHotel,  Agent aCurrentUser, bool aCommit = true)
        {
            string lTitle = aHotel.Name;
            BlitzerCore.Models.UI.UIResortPage lResort = new UIResortPage() { Title = lTitle };
            lResort.PageTypeId = RESORT_PAGE_TYPE;
            lResort.HeaderImage = new Block() { Title = "Header for the " + lTitle + " page",  BlockTitle = "Upate the Header", Caption = "Update the caption" };
            lResort.HeaderImage.Media = new Media() { Title = "Empty background for " + lTitle + " page" };
            lResort.HeaderImage.Media.Size1600x1200 = new Photo() { Location = "/images/1600x1200/Empty.Jpg", MediaFormat = MediaFormats.Size_1600x1200 };
            lResort.MainImage = CreateEmptyMainImage(lTitle);

            lResort.CenterContent = new Content() { Caption = "Update the caption", Header = "Update the Header", Summary = "" };
            lResort.CenterContent.Video = new Video();
            lResort.RightPanel = new Panel();
            lResort.LeftPanel = new Panel();
            lResort.RightPanel.Tiles.Add(new Tile() { Title = lTitle + " Tile 1", BlockTitle = "Tile 1" });
            lResort.RightPanel.Tiles.Add(new Tile() { Title = lTitle + " Tile 2", BlockTitle = "Tile 2" });
            lResort.RightPanel.Tiles.Add(new Tile() { Title = lTitle + " Tile 3", BlockTitle = "Tile 3" });
            lResort.RightPanel.Tiles.Add(new Tile() { Title = lTitle + " Tile 4", BlockTitle = "Tile 4" });
            lResort.LeftPanel = new Panel();
            lResort.AuthorID = aCurrentUser.Id;
            new ResortPageDataAccess(DbContext).Save(lResort, aCommit);
            aHotel.PageId = lResort.Id;
            return lResort;

        }

        public List<UIResortPage> GetByCountry(int aCountryID)
        {
            return new PageDataAccess(DbContext).GetResortsByCountry(aCountryID);
        }

        public List<UIResortPage> GetAll()
        {
            return new ResortPageDataAccess(DbContext).GetAll();
        }

        public Block CreateEmptyMainImage(string aTitle)
        {
            var lOutput = new Block() { Title = "Header for the " + aTitle + " page", BlockTitle = "Upate the Header", Caption = "Update the caption" };
            lOutput.Media = new Media() { Title = "Empty background for " + aTitle + " page" };
            lOutput.Media.Size560x460 = new Photo() { Location = "/images/560x460/Empty.jpg", MediaFormat = MediaFormats.Size_1600x1200 };

            return lOutput;
        }

        public List<Media> GetGalleryPhotos(int aResortID, int aCategoryID)
        {
            return new MediaDataAccess(DbContext).GetResortImagesByCategory(aResortID, aCategoryID);
        }

        public string GetGalleryHeader(int aCategoryID)
        {
            return new MediaDataAccess(DbContext).GetCategories().Where(x => x.Id == aCategoryID).FirstOrDefault().Name + " Photo Gallary";
        }

        public PhotoGallery GetIndexPage(int aResortID, int aCategoryID)
        {
            PhotoGallery lGallary = new PhotoGallery();
            lGallary.Photos = GetGalleryPhotos(aResortID, aCategoryID);
            lGallary.Header = GetGalleryHeader(aCategoryID);
            //lGallary.NextResortID = GetCompResortID(aResortID);
            return lGallary;
        }

        private int GetCompResortID(int aResortID)
        {
            var Comps = new ResortPageDataAccess(DbContext).GetComparables();
            if ( Comps.Count == 0)
            {
                Logger.LogError("ResortPageBusiness::GetCompResortID - There were no comps for ResortID = " + aResortID);
                return 0;
            }
            return new ResortPageDataAccess(DbContext).GetComparables().FirstOrDefault(x => x.BaseResortID == aResortID).CompID;
        }

        public UIResortPage Get(Hotel aHotel, Contact aAgent)
        {
            var lHotelBiz = new HotelBusiness(DbContext);
            var lPageDA = new PageDataAccess(DbContext);
            BlitzerCore.Models.UI.UIResortPage lPage = lPageDA.GetResort(aHotel) as UIResortPage;
            if (lPage == null)
                lPage = CreateNewPage(aHotel, aAgent as Agent);

            if (lPage.MainImage == null)
                lPage.MainImage = CreateEmptyMainImage(lPage.Title);

            if (lPage.LeftPanel == null)
            {
                lPage.LeftPanel = new Panel();
                new ResortPageDataAccess(DbContext).Save(lPage);
            }

            if (lPage.RightPanel == null)
            {
                lPage.RightPanel = new Panel();
                lPage.RightPanel.Tiles.Add(new Tile() { Title = lPage.Title + " Tile 1", BlockTitle = "Tile 1" });
                lPage.RightPanel.Tiles.Add(new Tile() { Title = lPage.Title + " Tile 2", BlockTitle = "Tile 2" });
                lPage.RightPanel.Tiles.Add(new Tile() { Title = lPage.Title + " Tile 3", BlockTitle = "Tile 3" });
                lPage.RightPanel.Tiles.Add(new Tile() { Title = lPage.Title + " Tile 4", BlockTitle = "Tile 4" });
                new ResortPageDataAccess(DbContext).Save(lPage);
            }

            foreach (var lTile in lPage.RightPanel.Tiles)
                if (lTile.MediaID != null )
                    lTile.Media = new MediaDataAccess(DbContext).Get(lTile.MediaID.Value);

            foreach (var lComp in lPage.LeftPanel.Comparables)
                if (lComp.CompPage == null)
                    lComp.CompPage = Get( lComp.CompID) as UIResortPage;

            lPage.Categories = new MediaDataAccess(DbContext).GetCategories().Take(4).ToList();

            return lPage;
        }

        public UIResortPage Get(int aPageId)
        {
            var lHotelBiz = new HotelBusiness(DbContext);
            var lPageDA = new PageDataAccess(DbContext);
            var lPage = lPageDA.GetResort(aPageId);

            foreach (var lTile in lPage.RightPanel.Tiles)
                if (lTile.MediaID != null)
                    lTile.Media = new MediaDataAccess(DbContext).Get(lTile.MediaID.Value);

            foreach (var lComp in lPage.LeftPanel.Comparables)
                if (lComp.CompPage == null)
                    lComp.CompPage = lPageDA.Get(lComp.CompID) as UIResortPage;

            lPage.Categories = new MediaDataAccess(DbContext).GetCategories().Take(4).ToList();

            return lPage;
        }

        public AmenityMap GetStagingHotelAmenityMap(Staging.Hotel aResort, string aAmenity)
        {
            var lDA = new AccommodationDataAccess(DbContext);
            AmenityMap lMap = lDA.GetAmenityMap(aResort, aAmenity);
            if (lMap != null)
                return lMap;

            return lDA.AddAmenityMap(aResort, aAmenity);
        }

        public AmenityMap GetStagingHotelAmenityMap(Hotel aResort, string aAmenity)
        {
            var lDA = new AccommodationDataAccess(DbContext);
            AmenityMap lMap = lDA.GetAmenityMap(aResort, aAmenity);
            if (lMap != null)
                return lMap;

            return lDA.AddAmenityMap(aResort, aAmenity);
        }


    }
}
