using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19
{
    public class CountryRecord
    {
        /*
            dateRep	"03/04/2020"                    --> Date
            day	"3"                                 --> Day
            month	"4"                             --> Month
            year	"2020"                          --> Year
            cases	"43"                            --> Cases
            deaths	"0"                             --> Deaths
            countriesAndTerritories	"Afghanistan"   --> CountryName
            geoId	"AF"                            --> GeoID
            countryterritoryCode	"AFG"           --> CountryCode
            popData2018	"37172386"                  --> CountryPopulation
        */
        private string countryCode;
        public string CountryCode
        {
            get => countryCode;
            set => countryCode = value;
        }
        public string Date
        {
            get => year + "/" + month + "/" + day;
        }
        private int day, month, year;
        public int Day
        {
            get => day;
            set => day = value;
        }
        public int Month
        {
            get => month;
            set => month = value;
        }
        public int Year
        {
            get => year;
            set => year = value;
        }
        private int cases, deaths;
        public int Cases
        {
            get => cases;
            set => cases = value;
        }
        public int Deaths
        {
            get => deaths;
            set => deaths = value;
        }
    }
}