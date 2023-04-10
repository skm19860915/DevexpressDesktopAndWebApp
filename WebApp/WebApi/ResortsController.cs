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
using BlitzerCore.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ResortsController : Controller
    {
        private RepositoryContext _context;

        public ResortsController(RepositoryContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var resort = _context.Resort.Select(i => new {
                i.BeachRating,
                i.Id,
                i.Name,
                i.AAPreferredProvider,
                i.CheckIn,
                i.CheckOut,
                i.Bedding,
                i.Inclusions,
                i.Address1,
                i.Address2,
                i.City,
                i.State,
                i.Area,
                i.ZipCode,
                i.Country,
                i.Rating,
                i.FoodRating,
                i.RoomRating,
                i.Description,
                i.QuoteRequestID,
                i.Header,
                i.Summary,
                i.AirPortID
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "AccommodationID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(resort, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Resort();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Resort.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Resort.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> QuoteRequestsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.QuoteRequests
                         orderby i.Notes
                         select new {
                             Value = i.QuoteRequestID,
                             Text = i.Notes
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> AirPortsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.AirPorts
                         orderby i.Code
                         select new {
                             Value = i.AirPortID,
                             Text = i.Code
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Resort model, IDictionary values) {
            string BEACH_RATING = nameof(Resort.BeachRating);
            string ACCOMMODATION_ID = nameof(Resort.Id);
            string NAME = nameof(Resort.Name);
            string AAPREFERRED_PROVIDER = nameof(Resort.AAPreferredProvider);
            string CHECK_IN = nameof(Resort.CheckIn);
            string CHECK_OUT = nameof(Resort.CheckOut);
            string BEDDING = nameof(Resort.Bedding);
            string INCLUSIONS = nameof(Resort.Inclusions);
            string ADDRESS1 = nameof(Resort.Address1);
            string ADDRESS2 = nameof(Resort.Address2);
            string CITY = nameof(Resort.City);
            string STATE = nameof(Resort.State);
            string AREA = nameof(Resort.Area);
            string ZIP_CODE = nameof(Resort.ZipCode);
            string COUNTRYID = nameof(Resort.CountryId);
            string STARS = nameof(Resort.Rating);
            string FOOD_RATING = nameof(Resort.FoodRating);
            string ROOM_RATING = nameof(Resort.RoomRating);
            string DESCRIPTION = nameof(Resort.Description);
            string QUOTE_REQUEST_ID = nameof(Resort.QuoteRequestID);
            string HEADER = nameof(Resort.Header);
            string SUMMARY = nameof(Resort.Summary);
            string AIR_PORT_ID = nameof(Resort.AirPortID);

            if(values.Contains(BEACH_RATING)) {
                model.BeachRating = values[BEACH_RATING] != null ? Convert.ToDouble(values[BEACH_RATING], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(ACCOMMODATION_ID)) {
                model.Id = Convert.ToInt32(values[ACCOMMODATION_ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(AAPREFERRED_PROVIDER)) {
                model.AAPreferredProvider = Convert.ToBoolean(values[AAPREFERRED_PROVIDER]);
            }

            if(values.Contains(CHECK_IN)) {
                model.CheckIn = Convert.ToDateTime(values[CHECK_IN]);
            }

            if(values.Contains(CHECK_OUT)) {
                model.CheckOut = Convert.ToDateTime(values[CHECK_OUT]);
            }

            if(values.Contains(BEDDING)) {
                model.Bedding = Convert.ToString(values[BEDDING]);
            }

            if(values.Contains(INCLUSIONS)) {
                model.Inclusions = Convert.ToString(values[INCLUSIONS]);
            }

            if(values.Contains(ADDRESS1)) {
                model.Address1 = Convert.ToString(values[ADDRESS1]);
            }

            if(values.Contains(ADDRESS2)) {
                model.Address2 = Convert.ToString(values[ADDRESS2]);
            }

            if(values.Contains(CITY)) {
                model.City = Convert.ToString(values[CITY]);
            }

            if(values.Contains(STATE)) {
                model.State = Convert.ToString(values[STATE]);
            }

            if(values.Contains(AREA)) {
                model.Area = Convert.ToString(values[AREA]);
            }

            if(values.Contains(ZIP_CODE)) {
                model.ZipCode = Convert.ToString(values[ZIP_CODE]);
            }

            if(values.Contains(COUNTRYID)) {
                model.CountryId = Convert.ToInt32(values[COUNTRYID]);
            }

            if(values.Contains(STARS)) {
                model.Rating = values[STARS] != null ? Convert.ToDouble(values[STARS], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(FOOD_RATING)) {
                model.FoodRating = values[FOOD_RATING] != null ? Convert.ToDouble(values[FOOD_RATING], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(ROOM_RATING)) {
                model.RoomRating = values[ROOM_RATING] != null ? Convert.ToDouble(values[ROOM_RATING], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(QUOTE_REQUEST_ID)) {
                model.QuoteRequestID = values[QUOTE_REQUEST_ID] != null ? Convert.ToInt32(values[QUOTE_REQUEST_ID]) : (int?)null;
            }

            if(values.Contains(HEADER)) {
                model.Header = Convert.ToString(values[HEADER]);
            }

            if(values.Contains(SUMMARY)) {
                model.Summary = Convert.ToString(values[SUMMARY]);
            }

            if(values.Contains(AIR_PORT_ID)) {
                model.AirPortID = values[AIR_PORT_ID] != null ? Convert.ToInt32(values[AIR_PORT_ID]) : (int?)null;
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