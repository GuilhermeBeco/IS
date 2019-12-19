namespace ShowData
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.comboBoxSensors = new System.Windows.Forms.ComboBox();
            this.labelTemperature = new System.Windows.Forms.Label();
            this.labelHumidity = new System.Windows.Forms.Label();
            this.labelTemperatureValue = new System.Windows.Forms.Label();
            this.labelHumidityValue = new System.Windows.Forms.Label();
            this.labelBattery = new System.Windows.Forms.Label();
            this.labelBatteryValue = new System.Windows.Forms.Label();
            this.labelData = new System.Windows.Forms.Label();
            this.labelDataValue = new System.Windows.Forms.Label();
            this.richTextBoxData = new System.Windows.Forms.RichTextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelLogin = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxSensors
            // 
            this.comboBoxSensors.FormattingEnabled = true;
            this.comboBoxSensors.Location = new System.Drawing.Point(784, 161);
            this.comboBoxSensors.Name = "comboBoxSensors";
            this.comboBoxSensors.Size = new System.Drawing.Size(136, 21);
            this.comboBoxSensors.TabIndex = 0;
            // 
            // labelTemperature
            // 
            this.labelTemperature.AutoSize = true;
            this.labelTemperature.Location = new System.Drawing.Point(22, 73);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.Size = new System.Drawing.Size(73, 13);
            this.labelTemperature.TabIndex = 1;
            this.labelTemperature.Text = "Temperature: ";
            // 
            // labelHumidity
            // 
            this.labelHumidity.AutoSize = true;
            this.labelHumidity.Location = new System.Drawing.Point(471, 73);
            this.labelHumidity.Name = "labelHumidity";
            this.labelHumidity.Size = new System.Drawing.Size(53, 13);
            this.labelHumidity.TabIndex = 2;
            this.labelHumidity.Text = "Humidity: ";
            // 
            // labelTemperatureValue
            // 
            this.labelTemperatureValue.AutoSize = true;
            this.labelTemperatureValue.Location = new System.Drawing.Point(101, 73);
            this.labelTemperatureValue.Name = "labelTemperatureValue";
            this.labelTemperatureValue.Size = new System.Drawing.Size(13, 13);
            this.labelTemperatureValue.TabIndex = 3;
            this.labelTemperatureValue.Text = "0";
            // 
            // labelHumidityValue
            // 
            this.labelHumidityValue.AutoSize = true;
            this.labelHumidityValue.Location = new System.Drawing.Point(575, 73);
            this.labelHumidityValue.Name = "labelHumidityValue";
            this.labelHumidityValue.Size = new System.Drawing.Size(13, 13);
            this.labelHumidityValue.TabIndex = 4;
            this.labelHumidityValue.Text = "0";
            // 
            // labelBattery
            // 
            this.labelBattery.AutoSize = true;
            this.labelBattery.Location = new System.Drawing.Point(22, 116);
            this.labelBattery.Name = "labelBattery";
            this.labelBattery.Size = new System.Drawing.Size(46, 13);
            this.labelBattery.TabIndex = 5;
            this.labelBattery.Text = "Battery: ";
            // 
            // labelBatteryValue
            // 
            this.labelBatteryValue.AutoSize = true;
            this.labelBatteryValue.Location = new System.Drawing.Point(101, 116);
            this.labelBatteryValue.Name = "labelBatteryValue";
            this.labelBatteryValue.Size = new System.Drawing.Size(13, 13);
            this.labelBatteryValue.TabIndex = 6;
            this.labelBatteryValue.Text = "0";
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Location = new System.Drawing.Point(470, 116);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(80, 13);
            this.labelData.TabIndex = 7;
            this.labelData.Text = "Date and Time:";
            // 
            // labelDataValue
            // 
            this.labelDataValue.AutoSize = true;
            this.labelDataValue.Location = new System.Drawing.Point(575, 116);
            this.labelDataValue.Name = "labelDataValue";
            this.labelDataValue.Size = new System.Drawing.Size(13, 13);
            this.labelDataValue.TabIndex = 8;
            this.labelDataValue.Text = "0";
            // 
            // richTextBoxData
            // 
            this.richTextBoxData.Location = new System.Drawing.Point(16, 148);
            this.richTextBoxData.Name = "richTextBoxData";
            this.richTextBoxData.Size = new System.Drawing.Size(591, 191);
            this.richTextBoxData.TabIndex = 9;
            this.richTextBoxData.Text = "";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(784, 34);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(136, 20);
            this.textBoxUsername.TabIndex = 10;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(784, 73);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(136, 20);
            this.textBoxPassword.TabIndex = 12;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(713, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(713, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Password:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(813, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(830, 125);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(0, 13);
            this.labelLogin.TabIndex = 16;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(-4, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Temperature";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Humidity";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(695, 342);
            this.chart1.TabIndex = 17;
            this.chart1.Text = "chart1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(702, 371);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelHumidityValue);
            this.tabPage1.Controls.Add(this.labelTemperatureValue);
            this.tabPage1.Controls.Add(this.labelBattery);
            this.tabPage1.Controls.Add(this.labelHumidity);
            this.tabPage1.Controls.Add(this.labelTemperature);
            this.tabPage1.Controls.Add(this.labelBatteryValue);
            this.tabPage1.Controls.Add(this.labelData);
            this.tabPage1.Controls.Add(this.labelDataValue);
            this.tabPage1.Controls.Add(this.richTextBoxData);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(694, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data Table";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(694, 345);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Data Chart";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(713, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Sensors:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 405);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxSensors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Name = "Form1";
            this.Text = "Show Data";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSensors;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.Label labelHumidity;
        private System.Windows.Forms.Label labelTemperatureValue;
        private System.Windows.Forms.Label labelHumidityValue;
        private System.Windows.Forms.Label labelBattery;
        private System.Windows.Forms.Label labelBatteryValue;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.Label labelDataValue;
        private System.Windows.Forms.RichTextBox richTextBoxData;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
    }
}

