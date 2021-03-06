﻿using System.Collections.Generic;

namespace COVID19
{
    public class Country
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
        public int TotalCases()
        {
            int totalCases = 0;
            foreach (CountryRecord record in CountryRecords)
            {
                totalCases += record.Cases;
            }
            return totalCases;
        }
        public int TotalDeaths()
        {
            int totalDeaths = 0;
            foreach (CountryRecord record in CountryRecords)
            {
                totalDeaths += record.Deaths;
            }
            return totalDeaths;
        }
    }
}
