using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19
{
    class Country
    {
        private string countryCode, countryName, geoId;
        public string CountryCode
        {
            get => countryCode;
            set => countryCode = value;
        }
        public string Name
        {
            get => countryName;
            set => countryName = value;
        }
        public string GeoID
        {
            get => geoId;
            set => geoId = value;
        }
        private long population;
        public long Population
        {
            get => population;
            set => population = value;
        }
        private List<CountryRecord> countryRecords = new List<CountryRecord>();
        public List<CountryRecord> CountryRecords
        {
            get => countryRecords;
            set => countryRecords = value;
        }
    }
}
