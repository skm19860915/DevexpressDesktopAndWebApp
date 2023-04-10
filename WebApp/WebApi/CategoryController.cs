using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using BlitzerCore.DataAccess;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataServices;
using BlitzerCore.Models;

namespace WebApp.WebApi
{
    [Route("api/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private RepositoryContext _context;

        public CategoryController(RepositoryContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var Category = _context.Category.Select(i => new
            {
                i.Id,
                i.Name,
                i.Description
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            var lResults = await DataSourceLoader.LoadAsync(Category, loadOptions);
            return Json(lResults);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Category();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Category.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Category.FirstOrDefaultAsync(item => item.Id == key);
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
        public async System.Threading.Tasks.Task Delete(int key) {
            var model = await _context.Category.FirstOrDefaultAsync(item => item.Id == key);

            _context.Category.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Category model, IDictionary values) {
            string ID = nameof(Category.Id);
            string NAME = nameof(Category.Name);
            string DESCRIPTION = nameof(Category.Description);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
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