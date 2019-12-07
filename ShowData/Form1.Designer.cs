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
            this.SuspendLayout();
            // 
            // comboBoxSensors
            // 
            this.comboBoxSensors.FormattingEnabled = true;
            this.comboBoxSensors.Location = new System.Drawing.Point(257, 12);
            this.comboBoxSensors.Name = "comboBoxSensors";
            this.comboBoxSensors.Size = new System.Drawing.Size(121, 21);
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
            this.labelHumidityValue.Location = new System.Drawing.Point(547, 73);
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
            this.labelData.Location = new System.Drawing.Point(453, 116);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(71, 13);
            this.labelData.TabIndex = 7;
            this.labelData.Text = "Data e Hora: ";
            // 
            // labelDataValue
            // 
            this.labelDataValue.AutoSize = true;
            this.labelDataValue.Location = new System.Drawing.Point(523, 116);
            this.labelDataValue.Name = "labelDataValue";
            this.labelDataValue.Size = new System.Drawing.Size(13, 13);
            this.labelDataValue.TabIndex = 8;
            this.labelDataValue.Text = "0";
            // 
            // richTextBoxData
            // 
            this.richTextBoxData.Location = new System.Drawing.Point(12, 157);
            this.richTextBoxData.Name = "richTextBoxData";
            this.richTextBoxData.Size = new System.Drawing.Size(591, 226);
            this.richTextBoxData.TabIndex = 9;
            this.richTextBoxData.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 405);
            this.Controls.Add(this.richTextBoxData);
            this.Controls.Add(this.labelDataValue);
            this.Controls.Add(this.labelData);
            this.Controls.Add(this.labelBatteryValue);
            this.Controls.Add(this.labelBattery);
            this.Controls.Add(this.labelHumidityValue);
            this.Controls.Add(this.labelTemperatureValue);
            this.Controls.Add(this.labelHumidity);
            this.Controls.Add(this.labelTemperature);
            this.Controls.Add(this.comboBoxSensors);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

