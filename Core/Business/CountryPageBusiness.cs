using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class CountryPageBusiness
    {
        const int COUNTRY_PAGE_TYPE = 2;
        public IDbContext DbContext { get; set; }

        public CountryPageBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }

        public BlitzerCore.Models.UI.UICountry CreateNewPage(string aTitle,  string aCurrentUserID)
        {
            BlitzerCore.Models.UI.UICountry lCountry = new UICountry() { Title = aTitle };
            lCountry.PageTypeId = COUNTRY_PAGE_TYPE;
            lCountry.HeaderImage = new Block() { Title = "Header for the " + aTitle + " page",  BlockTitle = "Upate the Header", Caption = "Update the caption" };
            lCountry.HeaderImage.Media = new Media() { Title = "Empty background for " + aTitle + " page" };
            lCountry.HeaderImage.Media.Size1600x1200 = new Photo() { Location = "/images/1600x1200/Empty.Jpg", MediaFormat = MediaFormats.Size_1600x1200 };
            lCountry.MainImage = CreateEmptyMainImage(aTitle);

            lCountry.CenterContent = new Content() { Caption = "Update the caption", Header = "Update the Header", Summary = "Update the Summary" };
            lCountry.CenterContent.Video = new Video();
            lCountry.AuthorID = aCurrentUserID;
            new CountryPageDataAccess(DbContext).Save(lCountry);

            return lCountry;
        }

        public List<UIResortPage> GetByCountry(int aCountryID)
        {
            return new PageDataAccess(DbContext).GetResortsByCountry(aCountryID);
        }

        public Block CreateEmptyMainImage(string aTitle)
        {
            var lOutput = new Block() { Title = "Header for the " + aTitle + " page", BlockTitle = "Upate the Header", Caption = "Update the caption" };
            lOutput.Media = new Media() { Title = "Empty background for " + aTitle + " page" };
            lOutput.Media.Size560x460 = new Photo() { Location = "/images/560x460/Empty.jpg", MediaFormat = MediaFormats.Size_1600x1200 };

            return lOutput;
        }

        public void AddBlock(int id)
        {
            var lUICountry = new CountryPageDataAccess(DbContext).Get(id);
            if (lUICountry.Blocks == null)
                lUICountry.Blocks = new List<PageToBlockMap>();
            var lPToB = new PageToBlockMap() { Block = new Block() { Title = "Admin Title for " + lUICountry.Title, BlockTitle = "Text for " + lUICountry.Title } };
            lUICountry.Blocks.Add(lPToB);
            new CountryPageDataAccess(DbContext).Save(lUICountry);
        }

        public PhotoGallery GetIndexPage(int aResortID, int aCategoryID)
        {
            PhotoGallery lGallary = new PhotoGallery();
            lGallary.Photos = new MediaDataAccess(DbContext).GetResortImagesByCategory(aResortID, aCategoryID);
            lGallary.Header = new MediaDataAccess(DbContext).GetCategories().Where(x => x.Id == aCategoryID).FirstOrDefault().Name + " Photo Gallary";
            lGallary.NextResortID = GetCompResortID(aResortID);
            return lGallary;
        }

        private int GetCompResortID(int aResortID)
        {
            return new ResortPageDataAccess(DbContext).GetComparables().FirstOrDefault(x => x.BaseResortID == aResortID).CompID;
        }

        public Page Get(Country aCountry)
        {
            //var lPageDA = new PageDataAccess(DbContext);
            //BlitzerCore.Models.UI.UIResort lPage = lPageDA.Get(aCountry) as UIResort;
            //if (lPage == null )
            //    return lPage;

            //if (lPage.MainImage == null)
            //    lPage.MainImage = CreateEmptyMainImage(lPage.Title);

            //if (lPage.LeftPanel == null)
            //{
            //    lPage.LeftPanel = new Panel();
            //    new ResortDataAccess(DbContext).Save(lPage);
            //}

            //if (lPage.RightPanel == null)
            //{
            //    lPage.RightPanel = new Panel();
            //    lPage.RightPanel.Tiles.Add(new Tile() { Title = lPage.Title + " Tile 1", BlockTitle = "Tile 1" });
            //    lPage.RightPanel.Tiles.Add(new Tile() { Title = lPage.Title + " Tile 2", BlockTitle = "Tile 2" });
            //    lPage.RightPanel.Tiles.Add(new Tile() { Title = lPage.Title + " Tile 3", BlockTitle = "Tile 3" });
            //    lPage.RightPanel.Tiles.Add(new Tile() { Title = lPage.Title + " Tile 4", BlockTitle = "Tile 4" });
            //    new ResortDataAccess(DbContext).Save(lPage);
            //}

            //foreach (var lTile in lPage.RightPanel.Tiles)
            //    if (lTile.MediaID != null )
            //        lTile.Media = new MediaDataAccess(DbContext).Get(lTile.MediaID.Value);

            //foreach (var lComp in lPage.LeftPanel.Comparables)
            //    if (lComp.CompPage == null)
            //        lComp.CompPage = lPageDA.Get(  lComp.CompID) as UIResort;

            //lPage.Categories = new MediaDataAccess(DbContext).GetCategories().Take(4).ToList();

            //return lPage;
            throw new NotImplementedException();
        }
    }
}
