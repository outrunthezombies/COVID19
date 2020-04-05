using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace COVID19
{
    public partial class FormCOVID : Form
    {
        public FormCOVID()
        {
            InitializeComponent();
            CboChartType.SelectedIndex = 3;
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
        private void GetCountryData(int countryIndex)
        { 
            int totalCases = 0;
            int totalDeaths = 0;

            LblPopulation.Text = string.Format("{0:#,##0}", double.Parse(countries[countryIndex].Population.ToString()));
            LblTotalCases.Text = string.Format("{0:#,##0}", double.Parse(countries[countryIndex].TotalCases().ToString()));
            LblTotalDeaths.Text = string.Format("{0:#,##0}", double.Parse(countries[countryIndex].TotalDeaths().ToString()));
            DgvCountryRecords.Rows.Clear();
            foreach (CountryRecord record in countries[countryIndex].CountryRecords)
            {
                DataGridViewRow dgvr = (DataGridViewRow)DgvCountryRecords.Rows[0].Clone();
                dgvr.Cells[0].Value = record.Year + "/" + record.Month + "/" + record.Day;
                dgvr.Cells[1].Value = record.Cases;
                dgvr.Cells[2].Value = record.Deaths;
                dgvr.Cells[3].Value = "";
                dgvr.Cells[4].Value = "";
                DgvCountryRecords.Rows.Add(dgvr);
            }
            for (int index = DgvCountryRecords.Rows.Count-2; index >= 0 ; index--)
            {
                totalCases += (int)DgvCountryRecords.Rows[index].Cells[1].Value;
                DgvCountryRecords.Rows[index].Cells[3].Value = totalCases;
                totalDeaths += (int)DgvCountryRecords.Rows[index].Cells[2].Value;
                DgvCountryRecords.Rows[index].Cells[4].Value = totalDeaths;
            }
        }
        private bool IsCountrySelected(int countryIndex)
        {
            bool isSelected = false;
            foreach (Series series in ChtChart.Series)
            {
                if (series.Name == countries[countryIndex].Name)
                    isSelected = true;
            }
            return isSelected;
        }
        private void AddRemoveChartSeries(int countryIndex)
        {
            DataPoint dp;
            if (IsCountrySelected(countryIndex))
            {
                ChtChart.Series.Remove(ChtChart.Series[countries[countryIndex].Name]);
                ClbCountries.SetItemChecked(countryIndex, false);
            }
            else
            {
                ChtChart.Series.Add(countries[countryIndex].Name);
                ChtChart.Series[countries[countryIndex].Name].ChartType = (SeriesChartType)CboChartType.SelectedIndex;
                int totalCases = 0;
                for (int index = countries[countryIndex].CountryRecords.Count - 1; index >= 0; index--)
                {
                    totalCases += countries[countryIndex].CountryRecords[index].Cases;
                    dp = new DataPoint();
                    dp.SetValueXY(countries[countryIndex].CountryRecords[index].Date, (double)totalCases / ((double)countries[countryIndex].Population / 1000000));
                    ChtChart.Series[countries[countryIndex].Name].Points.Add(dp);
                    ChtChart.ChartAreas[0].AxisX.IsReversed = false;
                }
                ClbCountries.SetItemChecked(countryIndex, true);
            }
        }
        private void FormCOVID_Load(object sender, EventArgs e)
        {
            this.Show();
            this.Cursor = Cursors.WaitCursor;
            LoadJSON();
            ParseJSONIntoObjects();
            ClbCountries.Items.Clear();
            CboCountries.Items.Clear();
            foreach (Country record in countries)
            {
                ClbCountries.Items.Add(record.Name);
                CboCountries.Items.Add(record.Name);
            }
            this.Cursor = Cursors.Default;
        }
        private void BtnChartAddRemove_Click(object sender, EventArgs e)
        {
            if (CboCountries.SelectedIndex >= 0)
            {
                AddRemoveChartSeries(CboCountries.SelectedIndex);
            }
        }
        private void BtnResetChart_Click(object sender, EventArgs e)
        {
            ChtChart.Series.Clear();
        }
        private void CboCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCountryData(CboCountries.SelectedIndex);
        }
        private void ClbCountries_ItemCheck(object sender, EventArgs e)
        {
            AddRemoveChartSeries(ClbCountries.SelectedIndex);
        }
        private void CboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Series series in ChtChart.Series)
            {
                series.ChartType = (SeriesChartType)CboChartType.SelectedIndex;
            }
        }
    }
}
