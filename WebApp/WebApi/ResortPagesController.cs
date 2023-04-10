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

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ResortPagesController : Controller
    {
        private RepositoryContext _context;

        public ResortPagesController(RepositoryContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var pages = _context.Pages.Select(i => new
            {
                i.Id,
                i.Title,
                i.PageTitle,
                i.PageCaption,
                i.HeaderImageID,
                i.Published,
                i.PublishedOn,
                i.AuthorID,
                i.MyUrl
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(pages, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Page();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Pages.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Pages.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Pages.FirstOrDefaultAsync(item => item.Id == key);

            _context.Pages.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> BlockLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Block
                         orderby i.Title
                         select new {
                             Value = i.Id,
                             Text = i.Title
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Page model, IDictionary values) {
            string ID = nameof(Page.Id);
            string TITLE = nameof(Page.Title);
            string PAGE_TITLE = nameof(Page.PageTitle);
            string PAGE_CAPTION = nameof(Page.PageCaption);
            string HEADER_IMAGE_ID = nameof(Page.HeaderImageID);
            string PUBLISHED = nameof(Page.Published);
            string PUBLISHED_ON = nameof(Page.PublishedOn);
            string AUTHOR_ID = nameof(Page.AuthorID);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(TITLE)) {
                model.Title = Convert.ToString(values[TITLE]);
            }

            if(values.Contains(PAGE_TITLE)) {
                model.PageTitle = Convert.ToString(values[PAGE_TITLE]);
            }

            if(values.Contains(PAGE_CAPTION)) {
                model.PageCaption = Convert.ToString(values[PAGE_CAPTION]);
            }

            if(values.Contains(HEADER_IMAGE_ID)) {
                model.HeaderImageID = values[HEADER_IMAGE_ID] != null ? Convert.ToInt32(values[HEADER_IMAGE_ID]) : (int?)null;
            }

            if(values.Contains(PUBLISHED)) {
                model.Published = Convert.ToBoolean(values[PUBLISHED]);
            }

            if(values.Contains(PUBLISHED_ON)) {
                model.PublishedOn = values[PUBLISHED_ON] != null ? Convert.ToDateTime(values[PUBLISHED_ON]) : (DateTime?)null;
            }

            if(values.Contains(AUTHOR_ID)) {
                model.AuthorID = Convert.ToString(values[AUTHOR_ID]);
            }
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