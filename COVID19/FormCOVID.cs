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
        private readonly string CovidJSONURL = "https://opendata.ecdc.europa.eu/covid19/casedistribution/json/";
        private string CovidJSONRawData = "";
        private List<CountryRecord> countryRecords = new List<CountryRecord>();
        private List<Country> countries = new List<Country>();
        private readonly char underscore = '_';
        private readonly char space = ' ';
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
                        List<JToken> issues = item.Children().Children().ToList();
                        foreach (JObject issue in issues)
                        {
                            CountryRecord newRecord = new CountryRecord();
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
                                    Name = countryName.Replace(underscore, space),
                                    GeoID = geoId,
                                    Population = population
                                };
                                tempCountry.CountryRecords.Add(newRecord);
                                countries.Add(tempCountry);
                            }
                            countryRecords.Add(newRecord);
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
            ClbCountries.Items.Clear();
            CboCountries.Items.Clear();
            ClbCountries.Items.Add("All Countries");
            foreach (Country record in countries)
            {
                ClbCountries.Items.Add(record.Name);
                CboCountries.Items.Add(record.Name);
            }
        }

        private void CboCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            int totalCases = 0;
            int totalDeaths = 0;

            foreach (Country country in countries)
            {
                if (country.Name.Equals(CboCountries.SelectedItem.ToString()))
                {
                    LblCountryName.Text = country.Name + ", " + country.CountryCode + ", " + country.GeoID;
                    LblPopulation.Text = string.Format("{0:#,##0}", double.Parse(country.Population.ToString()));
                    LblTotalCases.Text = string.Format("{0:#,##0}", double.Parse(country.TotalCases().ToString()));
                    LblTotalDeaths.Text = string.Format("{0:#,##0}", double.Parse(country.TotalDeaths().ToString()));
                    DgvCountryRecords.Rows.Clear();
                    foreach (CountryRecord record in country.CountryRecords)
                    {
                        DataGridViewRow dgvr = (DataGridViewRow)DgvCountryRecords.Rows[0].Clone();
                        dgvr.Cells[0].Value = record.Year + "/" + record.Month + "/" + record.Day;
                        dgvr.Cells[1].Value = record.Cases;
                        dgvr.Cells[2].Value = record.Deaths;
                        dgvr.Cells[3].Value = "";
                        dgvr.Cells[4].Value = "";
                        DgvCountryRecords.Rows.Add(dgvr);
                    }
                }
            }
            for (int index = DgvCountryRecords.Rows.Count-2; index >= 0 ; index--)
            {
                totalCases += (int)DgvCountryRecords.Rows[index].Cells[1].Value;
                DgvCountryRecords.Rows[index].Cells[3].Value = totalCases;
                totalDeaths += (int)DgvCountryRecords.Rows[index].Cells[2].Value;
                DgvCountryRecords.Rows[index].Cells[4].Value = totalDeaths;
            }
        }
    }
}
