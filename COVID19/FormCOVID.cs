using System;
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

        private void FormCOVID_Load(object sender, EventArgs e)
        {
            using (WebClient webClient = new WebClient())
            {
                CovidJSONRawData = webClient.DownloadString(CovidJSONURL);
            }
        }
    }
}
