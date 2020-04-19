using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Covid19Web.Pages
{
    public class IndexModel : PageModel
    {
        public COVID19.Country SelectedCountry { get; set; }
        public string SelectedCountryName { get; private set; } = "";
        public int TotalCases { get; set; } = 0;
        public int TotalDeaths { get; set; } = 0;
        public string PopulationLabel { get; private set; }
        public string TotalCasesLabel { get=>string.Format("{0:#,##0}", TotalCases); }
        public string TotalDeathsLabel { get=>string.Format("{0:#,##0}", TotalDeaths); }
        public string DeathRateLabel { get=> string.Format("{0:#0.0%}", (double)TotalDeaths / (double)TotalCases); }
        public string FormatNumber(object number) { return string.Format("{0:#,##0}", number); }
        public string FormatPercentage(int number1, int number2) { return string.Format("{0:#0.0%}", (double)number1/(double)number2); }
        public SelectList Countries { get; set; }
        public void OnGet()
        {
            SharedCode.LoadAndParseJSONIntoObjects();

            Countries = new SelectList(SharedCode.countries, nameof(COVID19.Country.GeoID), nameof(COVID19.Country.Name));
            
            if (!string.IsNullOrEmpty(Request.Query["id"]))
            {
                SelectedCountry = SharedCode.countries.Find(x => x.GeoID == Request.Query["id"]);

                SelectedCountryName = SelectedCountry.Name;
                TotalCases = SelectedCountry.TotalCases();
                TotalDeaths = SelectedCountry.TotalDeaths();
                PopulationLabel = string.Format("{0:#,##0}", SelectedCountry.Population);
            }
        }
        public void BuildChart()
        {
            
        }
    }
}
