using Gurock.SmartInspect;
using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Utilities;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace BlitzerCore.Helpers
{
    public class Employee
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ID")]
        public int ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "FirstName")]
        public string FirstName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "LastName")]
        public string LastName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Title")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Prefix")]
        public string Prefix { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Position")]
        public string Position { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "BirthDate")]
        public DateTime? BirthDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "HireDate")]
        public DateTime? HireDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Notes")]
        public string Notes { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Address")]
        public string Address { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "City")]
        public string City { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Tasks")]
        public int? StateID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "State")]
        public string State { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "HomePhone")]
        public string HomePhone { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Skype")]
        public string Skype { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Picture")]
        public string Picture { get; set; }
    }


    public class DateHelper
    {
        public IEnumerable<Employee> SampleData()
        {
            return new[] {
                new Employee {
                    ID = 1,
                    FirstName = "John",
                    LastName = "Heart",
                    Phone = "(213) 555-9392",
                    Prefix = "Mr.",
                    Position = "CEO",
                    BirthDate = DateTime.Parse("1964/03/16"),
                    HireDate = DateTime.Parse("1995/01/15"),
                    Notes = "John has been in the Audio/Video industry since 1990. He has led DevAv as its CEO since 2003.\r\n\r\nWhen not working hard as the CEO, John loves to golf and bowl. He once bowled a perfect game of 300.",
                    Email = "jheart@dx-email.com",
                    Address = "351 S Hill St.",
                    City = "Los Angeles",
                    State = "California",
                    StateID = 5,
                    HomePhone = "(213) 555-9208",
                    Skype = "jheartDXskype",
                    Picture = "../../images/employees/01.png"
                },
                new Employee {
                    ID = 2,
                    FirstName = "Olivia",
                    LastName = "Peyton",
                    Phone = "(310) 555-2728",
                    Prefix = "Mrs.",
                    Position = "Sales Assistant",
                    BirthDate = DateTime.Parse("1981/06/03"),
                    HireDate = DateTime.Parse("2012/05/14"),
                    Notes = "Olivia loves to sell. She has been selling DevAV products since 2012. \r\n\r\nOlivia was homecoming queen in high school. She is expecting her first child in 6 months. Good Luck Olivia.",
                    Email = "oliviap@dx-email.com",
                    Address = "807 W Paseo Del Mar",
                    City = "Los Angeles",
                    State = "California",
                    StateID = 5,
                    HomePhone = "(310) 555-4547",
                    Skype = "oliviapDXskype",
                    Picture = "../../images/employees/09.png"
                }
            };
        }

        public static DateTime? ConvertDate(string aDate)
        {
            try
            {
                if (aDate == null || aDate.Length == 0 )
                    return null;

                int lFirstDelim = aDate.IndexOf('/');
                // Check if the date is in a different format
                if ( lFirstDelim < 0 )
                {
                    DateTime lOutput;
                    if (DateTime.TryParse(aDate, out lOutput))
                        return lOutput;
                }
                string lMonth = aDate.Substring(0, lFirstDelim);
                int lSecondDelim = aDate.IndexOf('/', lFirstDelim + 1);
                string lDay = aDate.Substring(lFirstDelim + 1, lSecondDelim - lFirstDelim - 1);
                string lsYear = aDate.Substring(lSecondDelim + 1);
                int lYear = 0;

                if (lsYear.Length == 2)
                {
                    if (int.Parse(lsYear) > 50)
                        lYear = 1900 + int.Parse(lsYear);
                    else
                        lYear = 2000 + int.Parse(lsYear);
                }
                else
                    lYear = int.Parse(lsYear);

                return new DateTime(lYear, int.Parse(lMonth), int.Parse(lDay), 0, 0, 0);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to Convert Date string[" + aDate + "]", e);
                throw new InvalidDataException("Failed to Convert Date string[" + aDate + "]");
            }
        }
    }
}
