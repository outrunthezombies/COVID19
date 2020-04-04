namespace COVID19
{
    partial class FormCOVID
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnLoad = new System.Windows.Forms.Button();
            this.ClbCountries = new System.Windows.Forms.CheckedListBox();
            this.CboCountries = new System.Windows.Forms.ComboBox();
            this.DgvCountryRecords = new System.Windows.Forms.DataGridView();
            this.LblCountryLabel = new System.Windows.Forms.Label();
            this.LblCountryName = new System.Windows.Forms.Label();
            this.LblPopulation = new System.Windows.Forms.Label();
            this.LblPopulationLabel = new System.Windows.Forms.Label();
            this.LblTotalCases = new System.Windows.Forms.Label();
            this.LblTotalCasesLabel = new System.Windows.Forms.Label();
            this.LblTotalDeaths = new System.Windows.Forms.Label();
            this.LblTotalDeathsLabel = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DailyCases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DailyDeaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalDeaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCountryRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnLoad
            // 
            this.BtnLoad.Location = new System.Drawing.Point(12, 25);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(75, 23);
            this.BtnLoad.TabIndex = 1;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // ClbCountries
            // 
            this.ClbCountries.FormattingEnabled = true;
            this.ClbCountries.Location = new System.Drawing.Point(93, 55);
            this.ClbCountries.Name = "ClbCountries";
            this.ClbCountries.Size = new System.Drawing.Size(214, 379);
            this.ClbCountries.TabIndex = 2;
            // 
            // CboCountries
            // 
            this.CboCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCountries.FormattingEnabled = true;
            this.CboCountries.Location = new System.Drawing.Point(93, 25);
            this.CboCountries.Name = "CboCountries";
            this.CboCountries.Size = new System.Drawing.Size(214, 21);
            this.CboCountries.TabIndex = 4;
            this.CboCountries.SelectedIndexChanged += new System.EventHandler(this.CboCountries_SelectedIndexChanged);
            // 
            // DgvCountryRecords
            // 
            this.DgvCountryRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCountryRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.DailyCases,
            this.DailyDeaths,
            this.TotalCases,
            this.TotalDeaths});
            this.DgvCountryRecords.Location = new System.Drawing.Point(313, 108);
            this.DgvCountryRecords.Name = "DgvCountryRecords";
            this.DgvCountryRecords.Size = new System.Drawing.Size(680, 326);
            this.DgvCountryRecords.TabIndex = 8;
            // 
            // LblCountryLabel
            // 
            this.LblCountryLabel.AutoSize = true;
            this.LblCountryLabel.Location = new System.Drawing.Point(354, 35);
            this.LblCountryLabel.Name = "LblCountryLabel";
            this.LblCountryLabel.Size = new System.Drawing.Size(46, 13);
            this.LblCountryLabel.TabIndex = 9;
            this.LblCountryLabel.Text = "Country:";
            // 
            // LblCountryName
            // 
            this.LblCountryName.AutoSize = true;
            this.LblCountryName.Location = new System.Drawing.Point(406, 35);
            this.LblCountryName.Name = "LblCountryName";
            this.LblCountryName.Size = new System.Drawing.Size(0, 13);
            this.LblCountryName.TabIndex = 10;
            // 
            // LblPopulation
            // 
            this.LblPopulation.AutoSize = true;
            this.LblPopulation.Location = new System.Drawing.Point(707, 35);
            this.LblPopulation.Name = "LblPopulation";
            this.LblPopulation.Size = new System.Drawing.Size(0, 13);
            this.LblPopulation.TabIndex = 14;
            // 
            // LblPopulationLabel
            // 
            this.LblPopulationLabel.AutoSize = true;
            this.LblPopulationLabel.Location = new System.Drawing.Point(649, 35);
            this.LblPopulationLabel.Name = "LblPopulationLabel";
            this.LblPopulationLabel.Size = new System.Drawing.Size(60, 13);
            this.LblPopulationLabel.TabIndex = 13;
            this.LblPopulationLabel.Text = "Population:";
            // 
            // LblTotalCases
            // 
            this.LblTotalCases.AutoSize = true;
            this.LblTotalCases.Location = new System.Drawing.Point(707, 55);
            this.LblTotalCases.Name = "LblTotalCases";
            this.LblTotalCases.Size = new System.Drawing.Size(0, 13);
            this.LblTotalCases.TabIndex = 16;
            // 
            // LblTotalCasesLabel
            // 
            this.LblTotalCasesLabel.AutoSize = true;
            this.LblTotalCasesLabel.Location = new System.Drawing.Point(643, 55);
            this.LblTotalCasesLabel.Name = "LblTotalCasesLabel";
            this.LblTotalCasesLabel.Size = new System.Drawing.Size(66, 13);
            this.LblTotalCasesLabel.TabIndex = 15;
            this.LblTotalCasesLabel.Text = "Total Cases:";
            // 
            // LblTotalDeaths
            // 
            this.LblTotalDeaths.AutoSize = true;
            this.LblTotalDeaths.Location = new System.Drawing.Point(708, 77);
            this.LblTotalDeaths.Name = "LblTotalDeaths";
            this.LblTotalDeaths.Size = new System.Drawing.Size(0, 13);
            this.LblTotalDeaths.TabIndex = 18;
            // 
            // LblTotalDeathsLabel
            // 
            this.LblTotalDeathsLabel.AutoSize = true;
            this.LblTotalDeathsLabel.Location = new System.Drawing.Point(640, 77);
            this.LblTotalDeathsLabel.Name = "LblTotalDeathsLabel";
            this.LblTotalDeathsLabel.Size = new System.Drawing.Size(71, 13);
            this.LblTotalDeathsLabel.TabIndex = 17;
            this.LblTotalDeathsLabel.Text = "Total Deaths:";
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // DailyCases
            // 
            this.DailyCases.HeaderText = "Daily Cases";
            this.DailyCases.Name = "DailyCases";
            this.DailyCases.ReadOnly = true;
            // 
            // DailyDeaths
            // 
            this.DailyDeaths.HeaderText = "Daily Deaths";
            this.DailyDeaths.Name = "DailyDeaths";
            this.DailyDeaths.ReadOnly = true;
            // 
            // TotalCases
            // 
            this.TotalCases.HeaderText = "Total Cases";
            this.TotalCases.Name = "TotalCases";
            this.TotalCases.ReadOnly = true;
            // 
            // TotalDeaths
            // 
            this.TotalDeaths.HeaderText = "Total Deaths";
            this.TotalDeaths.Name = "TotalDeaths";
            this.TotalDeaths.ReadOnly = true;
            // 
            // FormCOVID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 737);
            this.Controls.Add(this.LblTotalDeaths);
            this.Controls.Add(this.LblTotalDeathsLabel);
            this.Controls.Add(this.LblTotalCases);
            this.Controls.Add(this.LblTotalCasesLabel);
            this.Controls.Add(this.LblPopulation);
            this.Controls.Add(this.LblPopulationLabel);
            this.Controls.Add(this.LblCountryName);
            this.Controls.Add(this.LblCountryLabel);
            this.Controls.Add(this.DgvCountryRecords);
            this.Controls.Add(this.CboCountries);
            this.Controls.Add(this.ClbCountries);
            this.Controls.Add(this.BtnLoad);
            this.Name = "FormCOVID";
            this.Text = "COVID19 Analysis";
            ((System.ComponentModel.ISupportInitialize)(this.DgvCountryRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.CheckedListBox ClbCountries;
        private System.Windows.Forms.ComboBox CboCountries;
        private System.Windows.Forms.DataGridView DgvCountryRecords;
        private System.Windows.Forms.Label LblCountryLabel;
        private System.Windows.Forms.Label LblCountryName;
        private System.Windows.Forms.Label LblPopulation;
        private System.Windows.Forms.Label LblPopulationLabel;
        private System.Windows.Forms.Label LblTotalCases;
        private System.Windows.Forms.Label LblTotalCasesLabel;
        private System.Windows.Forms.Label LblTotalDeaths;
        private System.Windows.Forms.Label LblTotalDeathsLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn DailyCases;
        private System.Windows.Forms.DataGridViewTextBoxColumn DailyDeaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCases;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalDeaths;
    }
}

