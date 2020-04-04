using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19
{
    class CountryRecord
    {
        /*
            dateRep	"03/04/2020"
            day	"3"
            month	"4"
            year	"2020"
            cases	"43"
            deaths	"0"
            countriesAndTerritories	"Afghanistan"
            geoId	"AF"
            countryterritoryCode	"AFG"
            popData2018	"37172386"
        */
        private string countryCode;
        public string CountryCode
        {
            get => countryCode;
            set => countryCode = value;
        }
        public DateTime DateRep
        {
            get => new DateTime(year, month, day);
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