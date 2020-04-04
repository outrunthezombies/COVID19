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
            this.TxtCountryName = new System.Windows.Forms.TextBox();
            this.CboCountries = new System.Windows.Forms.ComboBox();
            this.TxtCountryCode = new System.Windows.Forms.TextBox();
            this.TxtGeoID = new System.Windows.Forms.TextBox();
            this.TxtPopulation = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DailyCases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DailyDeaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalDeaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.ClbCountries.Size = new System.Drawing.Size(214, 304);
            this.ClbCountries.TabIndex = 2;
            // 
            // TxtCountryName
            // 
            this.TxtCountryName.Location = new System.Drawing.Point(354, 54);
            this.TxtCountryName.Name = "TxtCountryName";
            this.TxtCountryName.Size = new System.Drawing.Size(138, 20);
            this.TxtCountryName.TabIndex = 3;
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
            // TxtCountryCode
            // 
            this.TxtCountryCode.Location = new System.Drawing.Point(499, 54);
            this.TxtCountryCode.Name = "TxtCountryCode";
            this.TxtCountryCode.Size = new System.Drawing.Size(138, 20);
            this.TxtCountryCode.TabIndex = 5;
            // 
            // TxtGeoID
            // 
            this.TxtGeoID.Location = new System.Drawing.Point(643, 54);
            this.TxtGeoID.Name = "TxtGeoID";
            this.TxtGeoID.Size = new System.Drawing.Size(138, 20);
            this.TxtGeoID.TabIndex = 6;
            // 
            // TxtPopulation
            // 
            this.TxtPopulation.Location = new System.Drawing.Point(787, 54);
            this.TxtPopulation.Name = "TxtPopulation";
            this.TxtPopulation.Size = new System.Drawing.Size(138, 20);
            this.TxtPopulation.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.DailyCases,
            this.DailyDeaths,
            this.TotalCases,
            this.TotalDeaths});
            this.dataGridView1.Location = new System.Drawing.Point(354, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(547, 229);
            this.dataGridView1.TabIndex = 8;
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
            this.ClientSize = new System.Drawing.Size(1005, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.TxtPopulation);
            this.Controls.Add(this.TxtGeoID);
            this.Controls.Add(this.TxtCountryCode);
            this.Controls.Add(this.CboCountries);
            this.Controls.Add(this.TxtCountryName);
            this.Controls.Add(this.ClbCountries);
            this.Controls.Add(this.BtnLoad);
            this.Name = "FormCOVID";
            this.Text = "COVID19 Analysis";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.CheckedListBox ClbCountries;
        private System.Windows.Forms.TextBox TxtCountryName;
        private System.Windows.Forms.ComboBox CboCountries;
        private System.Windows.Forms.TextBox TxtCountryCode;
        private System.Windows.Forms.TextBox TxtGeoID;
        private System.Windows.Forms.TextBox TxtPopulation;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn DailyCases;
        private System.Windows.Forms.DataGridViewTextBoxColumn DailyDeaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCases;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalDeaths;
    }
}

