using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EconBot.IndicatorAPI
{
    
    public class WorldBankCoverter
    {
        private Dictionary<string, string> countryMapping = new Dictionary<string, string> 
            (StringComparer.InvariantCultureIgnoreCase)
        {
            {"New Zealand", "nz"},
            {"nz", "nz"},
            {"United States", "us"},
            {"United States of America", "us"},
            {"us", "us"},
            {"usa", "us"},
            {"India", "ind"}
        };
        

        private Dictionary<string, string> indicatorIntentMapping = new Dictionary<string, string> {
            {"GDPTotal", "NY.GDP.MKTP.CD"}, // Total GDP (USD)

        };

        
        private string IndicatorCode;
        private string CountryCode;
        private int Date = -1; // only one year given
        private int Date2 = -1; // second date - range given
        
        // Luis Entities for Indicators
        private const string EntityGeographyCountry = "builtin.geography.country";
        private const string EntityCountry_ShortName = "shortName_Country";
        private const string EntityDateRange = "builtin.datetimeV2.daterange";
        private const int CurrentYear = 2017; // need a better way to specify current year.


        public WorldBankCoverter(LuisResult result)
        {
            // set the indicator by getting the top scoring intent.
            IndicatorCode = indicatorIntentMapping[result.TopScoringIntent.Intent];

            // set the countryEntity if the full name is given or if the short name is given.
            var countryEntity = result.Entities.FirstOrDefault(e => e.Type == EntityGeographyCountry);
            var countryEntity_ShortName = result.Entities.FirstOrDefault(e => e.Type == EntityCountry_ShortName);
            // if no country is given
            if (countryEntity==null && countryEntity_ShortName == null)
            {
                Console.WriteLine("Invalid Country");
            } else if (countryEntity != null) // if full country name is given
            {
                CountryCode = countryMapping[countryEntity.Entity];
            } else
            {   // short name for country given : e.g. NZ for new zealand.
                CountryCode = countryMapping[countryEntity_ShortName.Entity];
            }

            // setDate
            var dateEntity = result.Entities.Where(e => e.Type == EntityDateRange);
            // no date given
            if (dateEntity == null || dateEntity.Count()==0)
            {
                Console.WriteLine("Handle no date provided.");
            } // if a specific year's GDP is required
            else if (dateEntity.Count() == 1)
            {
                Date = VerifyandSetDate(dateEntity.SingleOrDefault().Entity);
            } else if (dateEntity.Count() == 2) // if a range of GDP's are requested
            {
                string date1 = dateEntity.ToList()[0].Entity;
                string date2 = dateEntity.ToList()[1].Entity;

                Date = VerifyandSetDate(date1);
                Date2 = VerifyandSetDate(date2);
            } else
            {
                Console.WriteLine("Invalid set of Date(s) provided.");
            }
        }
        /// <summary>
        /// generate a query based on LuisResult which is a url that is used by an HTTPClient to query the 
        /// WorldBank databse.
        /// </summary>
        public string generateQuery()
        {
            if (Date == -1) // no year
            {
                // note that date = 2016 may not be available so will need better logic here.
                return "http://api.worldbank.org/countries/"+CountryCode+"/indicators/"+IndicatorCode+"?format=json&date=2016";
            } else if (Date2 == -1) // one specific year
            {
                return "http://api.worldbank.org/countries/" + CountryCode + "/indicators/" + 
                    IndicatorCode + "?format=json&date=" + Date.ToString();
            } else // range of years
            {
                return "http://api.worldbank.org/countries/" + CountryCode + "/indicators/" + 
                    IndicatorCode + "?format=json&date=" + Date.ToString() + ":" + Date2.ToString();
            }
        }

        private void FindKey(string key)
        {
            if (countryMapping.ContainsKey(key))
            {
                string code = countryMapping[key];
            } // else throw exception or error
        }
        /// <summary>
        /// Method checks a specified input date string and parses it into a given year. It handles invalid and valid dates
        /// and returns a integer which represents the date after parsing. Also checks that date is not too recent. 
        /// </summary>
        /// <param name="DateString">The String containing the year which is requested</param>
        /// <returns>An integer that represents the year for which we want the indicator for
        /// If the integer returned is -1, then the input year was invalid in some way.</returns>
        private int VerifyandSetDate(string DateString)
        {
            int returnDate = -1;
            if (!int.TryParse(DateString, out returnDate))
            {
                // invalid date
                Console.WriteLine("Invalid Date");
                returnDate = -1;
            }
            // check that date is not more recent than current year of less than 1960 since no data is available prior to 1960
            if (returnDate > CurrentYear || returnDate < 1960)
            {
                Console.WriteLine("Date Out of Bounds"); // may not be true for all countries
                // also handle when date < 1960 by saying no data is available for those years.
                returnDate = -1;
            }
            return returnDate;

        }

    }
}