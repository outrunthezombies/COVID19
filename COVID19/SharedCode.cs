using COVID19;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;

public static class SharedCode
{
    public readonly static int[] ChartType = { 13, 7, 28, 2, 20, 10, 18, 27, 6, 1, 33, 31, 3, 17, 0, 32, 26, 34, 25, 21, 23, 24, 29, 4, 14, 22, 15, 16, 8, 9, 11, 12, 5, 19, 30 };
    public readonly static string[] chartTypes = new string[] { "Area", "Bar", "BoxPlot", "Bubble", "Candlestick", "Column", "Doughnut", "ErrorBar", "FastLine", "FastPoint", "Funnel", "Kagi", "Line", "Pie", "Point", "PointAndFigure", "Polar", "Pyramid", "Radar", "Range", "RangeBar", "RangeColumn", "Renko", "Spline", "SplineArea", "SplineRange", "StackedArea", "StackedArea100", "StackedBar", "StackedBar100", "StackedColumn", "StackedColumn100", "StepLine", "Stock", "ThreeLineBreak" };
    public readonly static string CovidJSONURL = "https://opendata.ecdc.europa.eu/covid19/casedistribution/json/";
    public static string CovidJSONRawData = "";
    public readonly static char underscore = '_';
    public readonly static char space = ' ';
    public static List<CountryRecord> countryRecords = new List<CountryRecord>();
    public static List<Country> countries = new List<Country>();
    public enum ChartData
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
    public static void LoadAndParseJSONIntoObjects()
    {
        using (WebClient webClient = new WebClient())
        {
            CovidJSONRawData = webClient.DownloadString(CovidJSONURL);
        }

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
    }
}