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
        private List<Record> countryRecords = new List<Record>();

        private void FormCOVID_Load(object sender, EventArgs e)
        {

        }
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
            JObject json = JObject.Parse(CovidJSONRawData);
            List<JToken> data = json.Children().ToList();

            foreach (JProperty item in data)
            {
                item.CreateReader();
                switch (item.Name)
                {
                    case "records":
                        Record newRecord = new Record();
                        List<JToken> issues = item.Children().Children().ToList();
                        foreach (JObject issue in issues)
                        {
                            issue.CreateReader();
                            List<JToken> values = issue.Children().ToList();

                            foreach (JProperty value in values)
                            {
                                value.CreateReader();
                                switch (value.Name)
                                {
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
                                        newRecord.Country = value.Value.ToString();
                                        break;
                                    case "geoId":
                                        newRecord.GeoID = value.Value.ToString();
                                        break;
                                    case "countryterritoryCode":
                                        newRecord.CountryCode = value.Value.ToString();
                                        break;
                                    case "popData2018":
                                        if (value.Value.ToString() == "")
                                        {
                                            newRecord.Population = -1;
                                        }
                                        else
                                        {
                                            newRecord.Population = (long)value.Value;
                                        }
                                        break;
                                }
                                countryRecords.Add(newRecord);
                            }
                        }
                        break;
                }
            }
        }

        private void BtnParse_Click(object sender, EventArgs e)
        {
            ParseJSONIntoObjects();
            this.Text = countryRecords.Count + " Records";
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadJSON();
        }
    }
}
