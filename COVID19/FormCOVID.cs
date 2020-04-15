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
            CboChartDataSet.SelectedIndex = 0;
            CboChartType.Items.AddRange(chartTypes);
            CboChartType.SelectedIndex = 0; ;
        }
        private readonly int[] ChartType = { 13, 7, 28, 2, 20, 10, 18, 27, 6, 1, 33, 31, 3, 17, 0, 32, 26, 34, 25, 21, 23, 24, 29, 4, 14, 22, 15, 16, 8, 9, 11, 12, 5, 19, 30 };
        private readonly string[] chartTypes = new string[] { "Area", "Bar", "BoxPlot", "Bubble", "Candlestick", "Column", "Doughnut", "ErrorBar", "FastLine", "FastPoint", "Funnel", "Kagi", "Line", "Pie", "Point", "PointAndFigure", "Polar", "Pyramid", "Radar", "Range", "RangeBar", "RangeColumn", "Renko", "Spline", "SplineArea", "SplineRange", "StackedArea", "StackedArea100", "StackedBar", "StackedBar100", "StackedColumn", "StackedColumn100", "StepLine", "Stock", "ThreeLineBreak" };
        private readonly string CovidJSONURL = "https://opendata.ecdc.europa.eu/covid19/casedistribution/json/";
        private string CovidJSONRawData = "";
        private List<CountryRecord> countryRecords = new List<CountryRecord>();
        private List<Country> countries = new List<Country>();
        private readonly char underscore = '_';
        private readonly char space = ' ';
        private enum ChartData
        {
            TotalCasesPerMillionPeople = 0,
            DailyCasesPerMillionPeople = 1,
            TotalDeathsPerMillionPeople = 2,
            DailyDeathsPerMillionPeople = 3,
            TotalCasesByDay = 4,
            DailyCases = 5,
            TotalDeathsByDay = 6,
            DailyDeaths = 7
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
            LblDeathRate.Text = string.Format("{0:#0.0%}", (double)countries[countryIndex].TotalDeaths() / (double)countries[countryIndex].TotalCases());
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
            string countryName = countries[countryIndex].Name;
            DataPoint dp;
            if (IsCountrySelected(countryIndex))
            {
                ChtChart.Series.Remove(ChtChart.Series[countryName]);
                ClbCountries.SetItemChecked(countryIndex, false);
            }
            else
            {
                ChtChart.Series.Add(countryName);
                ChtChart.Series[countryName].ChartType = (SeriesChartType)ChartType[CboChartType.SelectedIndex];
                switch (ChtChart.Series[countryName].ChartType)
                {
                    case SeriesChartType.Line:
                        ChtChart.Series[countryName].IsVisibleInLegend = false;
                        break;
                    default:
                        ChtChart.Series[countryName].IsVisibleInLegend = true;
                        break;
                }

                int totalCases = 0;
                int totalDeaths = 0;
                long population = countries[countryIndex].Population;
                for (int index = countries[countryIndex].CountryRecords.Count - 1; index >= 0; index--)
                {
                    int cases = countries[countryIndex].CountryRecords[index].Cases;
                    int deaths = countries[countryIndex].CountryRecords[index].Deaths;
                    string date = countries[countryIndex].CountryRecords[index].Date;
                    totalCases += cases;
                    totalDeaths += deaths;
                    double yAxisData = 0.0;
                    string dataLabel = "";
                    switch ((ChartData)CboChartDataSet.SelectedIndex)
                    {
                        case ChartData.TotalCasesPerMillionPeople:
                            yAxisData = (double)totalCases / ((double)population / 1000000);
                            dataLabel = "Total Cases Per Million";
                            break;
                        case ChartData.DailyCasesPerMillionPeople:
                            yAxisData = (double)cases / ((double)population / 1000000);
                            dataLabel = "Daily Cases Per Million";
                            break;
                        case ChartData.TotalDeathsPerMillionPeople:
                            yAxisData = (double)totalDeaths / ((double)population / 1000000);
                            dataLabel = "Total Deaths Per Million";
                            break;
                        case ChartData.DailyDeathsPerMillionPeople:
                            yAxisData = (double)deaths / ((double)population / 1000000);
                            dataLabel = "Daily Deaths Per Million";
                            break;
                        case ChartData.TotalCasesByDay:
                            dataLabel = "Total Cases By Day";
                            yAxisData = (double)totalCases;
                            break;
                        case ChartData.DailyCases:
                            dataLabel = "Daily Cases";
                            yAxisData = (double)cases;
                            break;
                        case ChartData.TotalDeathsByDay:
                            yAxisData = (double)totalDeaths;
                            dataLabel = "Total Deaths Per By Day";
                            break;
                        case ChartData.DailyDeaths:
                            yAxisData = (double)deaths;
                            dataLabel = "Daily Deaths";
                            break;
                    }
                    ChtChart.Titles[0].Text = dataLabel;
                    dataLabel += ": " + string.Format("{0:#,0.#}", yAxisData);
                    dp = new DataPoint();
                    dp.SetValueXY(date, yAxisData);
                    dp.ToolTip = "Date: " + date + Environment.NewLine + dataLabel;
                    ChtChart.Series[countryName].ToolTip = "#VALX, #VAL";
                    ChtChart.Series[countryName].Points.Add(dp);
                    ChtChart.ChartAreas[0].AxisX.IsReversed = false;
                    ChtChart.ChartAreas[0].AxisX.Title = "Date";
                    ChtChart.ChartAreas[0].AxisY.Title = "Amount";
                }
                ChtChart.Series[countryName].Points[ChtChart.Series[countryName].Points.Count-1].Label = countryName;
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
            for (int index=0; index<ClbCountries.Items.Count; index++)
                ClbCountries.SetItemChecked(index, false);
        }
        private void CboCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCountryData(CboCountries.SelectedIndex);
        }
        private void ClbCountries_ItemCheck(object sender, EventArgs e)
        {
            CboCountries.SelectedIndex = ClbCountries.SelectedIndex;
            AddRemoveChartSeries(ClbCountries.SelectedIndex);
        }
        private void CboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Series series in ChtChart.Series)
            {
                series.ChartType = (SeriesChartType)ChartType[CboChartType.SelectedIndex];
                switch (series.ChartType)
                {
                    case SeriesChartType.Line:
                        series.IsVisibleInLegend = false;
                        break;
                    default:
                        series.IsVisibleInLegend = true;
                        break;
                }
            }
        }
        private void CboChartDataSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChtChart.Series.Clear();
            for (int index = 0; index < ClbCountries.Items.Count; index++)
            {
                if (ClbCountries.GetItemChecked(index))
                    AddRemoveChartSeries(index);
            }
        }
    }
}
