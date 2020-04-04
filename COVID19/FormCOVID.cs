using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace COVID19
{
    public partial class FormCOVID : Form
    {
        public FormCOVID()
        {
            InitializeComponent();
        }
        private string CovidJSONURL = "https://opendata.ecdc.europa.eu/covid19/casedistribution/json/";
        private string CovidJSONRawData = "";
        private List<Country> countries = new List<Country>();
        private void LoadJSON ()
        {
            this.Cursor = Cursors.WaitCursor;
            using (WebClient webClient = new WebClient())
            {
                CovidJSONRawData = webClient.DownloadString(CovidJSONURL);
            }
            this.Cursor = Cursors.Default;
        }
        private void ParseJSONIntoObjects()
        {
            this.Cursor = Cursors.WaitCursor;
            JObject json = JObject.Parse(CovidJSONRawData);
            List<JToken> data = json.Children().ToList();

            foreach (JProperty item in data)
            {
                item.CreateReader();
                switch (item.Name)
                {
                    case "records":
                        CountryRecord newRecord = new CountryRecord();
                        List<JToken> issues = item.Children().Children().ToList();
                        foreach (JObject issue in issues)
                        {
                            issue.CreateReader();
                            List<JToken> values = issue.Children().ToList();

                            string countryName = "";
                            string geoId = "";
                            long population = -1;
                            foreach (JProperty value in values)
                            {
                                value.CreateReader();
                                switch (value.Name)
                                {
                                    case "countryterritoryCode":
                                        newRecord.CountryCode = value.Value.ToString();
                                        break;
                                    case "day":
                                        newRecord.Day = (int)value.Value;
                                        break;
                                    case "month":
                                        newRecord.Month = (int)value.Value;
                                        break;
                                    case "year":
                                        newRecord.Year = (int)value.Value;
                                        break;
                                    case "cases":
                                        newRecord.Cases = (int)value.Value;
                                        break;
                                    case "deaths":
                                        newRecord.Deaths = (int)value.Value;
                                        break;
                                    case "countriesAndTerritories":
                                        countryName = value.Value.ToString();
                                        break;
                                    case "geoId":
                                        geoId = value.Value.ToString();
                                        break;
                                    case "popData2018":
                                        if (value.Value.ToString() != "")
                                            population = (long)value.Value;
                                        break;
                                }
                            }
                            Country tempCountry;

                            int index = countries.FindIndex(x => x.CountryCode == newRecord.CountryCode);

                            if (index >= 0)
                            {
                                tempCountry = countries.ElementAt(index);
                                tempCountry.CountryRecords.Add(newRecord);
                            }
                            else
                            {
                                tempCountry = new Country
                                {
                                    CountryCode = newRecord.CountryCode,
                                    Name = countryName,
                                    GeoID = geoId,
                                    Population = population
                                };
                                tempCountry.CountryRecords.Add(newRecord);
                                countries.Add(tempCountry);
                            }
                        }
                        break;
                }
            }
            this.Cursor = Cursors.Default;
        }
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadJSON();
            ParseJSONIntoObjects();
            this.Text = countries.Count + " Countries";
            ClbCountries.Items.Clear();
            CboCountries.Items.Clear();
            ClbCountries.Items.Add("All Countries");
            for (int index = 0; index < countries.Count - 1; index++)
            {
                ClbCountries.Items.Add(countries[index].Name);
                CboCountries.Items.Add(countries[index].Name);
            }
        }

        private void CboCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country country = countries[CboCountries.SelectedIndex];
            TxtCountryName.Text = country.Name;
            TxtCountryCode.Text = country.CountryCode;
            TxtGeoID.Text = country.GeoID;
            TxtPopulation.Text = country.Population.ToString();
        }
    }
}
