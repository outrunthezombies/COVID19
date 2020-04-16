using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static SharedCode;
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
            LoadAndParseJSONIntoObjects();
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
