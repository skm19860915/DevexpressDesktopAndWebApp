using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataServices;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Utilities;

namespace WebApp.WebApi
{
    [Route("api/[controller]/[action]")]
    public class MediaController : Controller
    {
        private RepositoryContext _context;

        public MediaController(RepositoryContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            //var medias = _context.Medias.Select(i => new {
            //    i.Id,
            //    i.Title,
            //    i.Description,
            //    i.Size1920x1080ID,
            //    i.Size1600x1200ID,
            //    i.Size1024x640ID,
            //    i.Size560x460ID,
            //    i.MPegID,
            //    i.ThumbNailID,
            //    i.PageID,
            //    i.CategoryID
            //});

            var medias = new MediaDataAccess(_context).GetAll();

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(medias, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            try
            {
                var model = new Media();
                var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
                PopulateModel(model, valuesDict);

                if (!TryValidateModel(model))
                    return BadRequest(GetFullErrorMessage(ModelState));

                var result = _context.Medias.Add(model);
                await _context.SaveChangesAsync();

                return Json(new { result.Entity.Id });
            } catch ( Exception e )
            {
                Logger.LogException("Failed to create Media Item", e);
            }

            throw new InvalidOperationException();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            try
            {
                var model = await _context.Medias.FirstOrDefaultAsync(item => item.Id == key);
                if (model == null)
                    return StatusCode(409, "Object not found");

                var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
                PopulateModel(model, valuesDict);

                if (!TryValidateModel(model))
                    return BadRequest(GetFullErrorMessage(ModelState));

                await _context.SaveChangesAsync();
                return Ok();
            }catch ( Exception e )
            {
                Logger.LogException("Failed to Save Media[" + key + "]", e);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Medias.FirstOrDefaultAsync(item => item.Id == key);

            _context.Medias.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> PhotosLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Photos
                         orderby i.Location
                         select new {
                             Value = i.ID,
                             Text = i.Location
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> VideosLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Videos
                         orderby i.Location
                         select new {
                             Value = i.ID,
                             Text = i.Location
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PagesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Pages
                         orderby i.Title
                         select new {
                             Value = i.Id,
                             Text = i.Title
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> CategoryLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Category
                         orderby i.Name
                         select new {
                             Value = i.Id,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Media model, IDictionary values) {
            string ID = nameof(Media.Id);
            string TITLE = nameof(Media.Title);
            string DESCRIPTION = nameof(Media.Description);
            string SIZE1920X1080 = nameof(Media.Size1920x1080);
            string SIZE1600X1200 = nameof(Media.Size1600x1200);
            string SIZE1024X640 = nameof(Media.Size1024x640);
            string SIZE560X460 = nameof(Media.Size560x460);
            string MPEG = nameof(Media.MPeg);
            string THUMB_NAIL = nameof(Media.ThumbNail);
            string PAGE_ID = nameof(Media.PageID);
            string CATEGORY_ID = nameof(Media.CategoryID);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(TITLE)) {
                model.Title = Convert.ToString(values[TITLE]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(SIZE1920X1080)) {
                if (model.Size1920x1080 == null)
                    model.Size1920x1080 = new Photo() { MediaFormat = MediaFormats.Size_1920x1080 };

                model.Size1920x1080.Location = GetLocation(values, SIZE1920X1080);
            }

            if(values.Contains(SIZE1600X1200)) {
                if (model.Size1600x1200 == null)
                    model.Size1600x1200 = new Photo() { MediaFormat = MediaFormats.Size_1600x1200 };

                model.Size1600x1200.Location = GetLocation(values, SIZE1600X1200);
            }

            if (values.Contains(SIZE1024X640)) {
                if (model.Size1024x640 == null)
                    model.Size1024x640 = new Photo() { MediaFormat = MediaFormats.Size_1024x640 };

                model.Size1024x640.Location = GetLocation(values, SIZE1024X640);
            }

            if (values.Contains(SIZE560X460)) {
                if (model.Size560x460 == null)
                    model.Size560x460 = new Photo() { MediaFormat = MediaFormats.Size_560x460 };

                model.Size560x460.Location = GetLocation(values, SIZE560X460);
            }

            if (values.Contains(MPEG)) {
                if (model.MPeg == null)
                    model.MPeg = new Video() { MediaFormat = MediaFormats.MPEG };

                model.MPeg.Location = GetLocation(values, MPEG);
            }

            if(values.Contains(THUMB_NAIL)) {
                if (model.ThumbNail == null)
                    model.ThumbNail = new Photo() { MediaFormat = MediaFormats.Size_560x460 };

                model.ThumbNail.Location = GetLocation(values, THUMB_NAIL);

            }

            if (values.Contains(PAGE_ID)) {
                model.PageID = values[PAGE_ID] != null ? Convert.ToInt32(values[PAGE_ID]) : (int?)null;
            }

            if(values.Contains(CATEGORY_ID)) {
                model.CategoryID = values[CATEGORY_ID] != null ? Convert.ToInt32(values[CATEGORY_ID]) : (int?)null;
            }
        }

        private string GetLocation(IDictionary aDic, string aSize)
        {
            string LOCATION = nameof(Graphic.Location);

            var lDic = (Convert.ToString(aDic[aSize]));
            var lValues = JsonConvert.DeserializeObject<IDictionary>(lDic);
            return (Convert.ToString(lValues[LOCATION]));
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}