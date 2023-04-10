using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using System.Linq;
using System.Data;

namespace BlitzerCore.Business
{
    public class PresentationBusiness
    {
        private IDbContext mContext;
        public IConfiguration Configuration { get; }

        public PresentationBusiness(IDbContext mContext, IConfiguration aConfiguration)
        {
            this.mContext = mContext;
            Configuration = aConfiguration;
        }

        public PresentationBusiness(IDbContext mContext)
        {
            this.mContext = mContext;
        }

        /// <summary>
        /// This method is called when an agent first decides to create a presenation
        /// </summary>
        /// <param name="aClientName"></param>
        /// <returns></returns>
        public Guid StartQueue(string aClientName)
        {
            //new 
            return Guid.NewGuid();
        }

        /// <summary>
        /// Called when the agent wants to add a website to the presentation
        /// </summary>
        /// <param name="aGuid"></param>
        /// <param name="aURL"></param>
        public PresentationQueueItem AddToQueue(Presentation aPresenation, WebPage aWebPage)
        {
            int lNewLocation = 0;
            if (aPresenation == null)
                return null;

            var lQueue = aPresenation.Queue;
            if (lQueue != null)
                lNewLocation = lQueue.Count() - 1;

            PresentationQueueItem lPresentationItem = new PresentationQueueItem() { PresentationId = aPresenation.Id, Location = lNewLocation, WebPageId = aWebPage.Id };
            aPresenation.Queue.Add(lPresentationItem);
            Save(aPresenation);
            return lPresentationItem;
        }

        /// <summary>
        /// Called when the agent wants to add a website to the presentation
        /// </summary>
        /// <param name="aGuid"></param>
        /// <param name="aURL"></param>
        public bool RemoveFromQueue(Presentation aPresenation, int Id)
        {

            if (aPresenation == null)
                return false;

            var obj = aPresenation.Queue.Where(m => m.Id == Id).FirstOrDefault();

            aPresenation.Queue.Remove(obj);
            Save(aPresenation);

            return true;
        }

        /// <summary>
        /// This is called when the agent has completed putting together the preseantion with all the webpages
        /// </summary>
        /// <param name="aGuid"></param>
        public void StartTour(Presentation aPresentation)
        {
            aPresentation.Status = Presentation.Statuses.Ready;
            Save(aPresentation);
        }

        /// <summary>
        /// Called to change the order of the websites in the presenation
        /// Implement this last!
        /// </summary>
        /// <param name="aGuid"></param>
        /// <param name="aURL"></param>
        /// <param name="aNewLocation"></param>
        /// <param name="aOldLocation"></param>
        public void MoveUrlinQueue(Presentation aPresentation, List<WebPage> webPages)
        {
            aPresentation.Queue.Clear();
            Save(aPresentation);

            int lNewLocation = 0;

            foreach (var item in webPages)
            {
                PresentationQueueItem lPresentationItem = new PresentationQueueItem() { PresentationId = aPresentation.Id, Location = lNewLocation, WebPageId = item.Id };
                aPresentation.Queue.Add(lPresentationItem);
                lNewLocation++;
            }

            Save(aPresentation);
        }


        public void Save(Presentation aPresentation)
        {
            new PresentationDataAccess(mContext).Save(aPresentation);
        }

        /// <summary>
        /// The website calls this method to find the next website page to display to a client
        /// </summary>
        /// <param name="aGuid">If null, return first website in active presentation</param>
        /// <returns></returns>
        public PresentationQueueItem GetPresentationItem(string aGuid)
        {
            // For testing, create presenation
            Presentation lPresentation = null;
            if (aGuid == null)
            {
                var lPresenations = new PresentationDataAccess(mContext).Get().Where(x => x.Status == Presentation.Statuses.Ready).ToList();
                if (lPresenations == null || lPresenations.Count == 0)
                    return null;

                var lPresenation = lPresenations.First();
                if (lPresenation == null || lPresenation.Queue.Count() == 0)
                    return null;

                return lPresenation.Queue.First();
            }
            else
            {
                // Guid is invalid
                lPresentation = new PresentationDataAccess(mContext).Get(aGuid);
            }

            if (lPresentation == null)
                return null;

            // Guid is valid.  Grap the first item
            var lQueue = lPresentation.Queue as List<PresentationQueueItem>;
            if (lQueue == null)
                return null;

            var lItem = lQueue.First();
            if (lItem != null)
                lQueue.Remove(lItem);

            // After we remove the last webpage, market presentation as completed
            if (lQueue.Count == 0)
                lPresentation.Status = Presentation.Statuses.Completed;

            Save(lPresentation);
            lItem.Presentation = lPresentation;
            return lItem;
        }

        public Presentation CreatePresenation(string aUserName)
        {
            var lPresenationDA = new PresentationDataAccess(mContext);
            var lPresentation = new Presentation()
            {
                Id = 0,
                ClientName = aUserName,
                Created = DateTime.Now,
                Guid = Guid.NewGuid().ToString(),
                Status = Presentation.Statuses.NotReady
            };

            Save(lPresentation);
            return lPresentation;
        }

        public List<WebPage> WebPages()
        {
            var lPresenationDA = new PresentationDataAccess(mContext);
            return lPresenationDA.GetWebPages();
        }

        public List<Presentation> Get()
        {
            var lPresenationDA = new PresentationDataAccess(mContext);
            return lPresenationDA.Get();
        }

        public Presentation GetPresentation(int id)
        {
            var lPresenationDA = new PresentationDataAccess(mContext);
            return lPresenationDA.Get(id);
        }
    }
}
