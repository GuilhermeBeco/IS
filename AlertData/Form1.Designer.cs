namespace AlertData
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSensors = new System.Windows.Forms.ComboBox();
            this.comboBoxCampos = new System.Windows.Forms.ComboBox();
            this.comboBoxOperacao = new System.Windows.Forms.ComboBox();
            this.buttonCreateTrigger = new System.Windows.Forms.Button();
            this.buttonSaveAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sensor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Campo: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Operação: ";
            // 
            // comboBoxSensors
            // 
            this.comboBoxSensors.FormattingEnabled = true;
            this.comboBoxSensors.Location = new System.Drawing.Point(273, 32);
            this.comboBoxSensors.Name = "comboBoxSensors";
            this.comboBoxSensors.Size = new System.Drawing.Size(137, 21);
            this.comboBoxSensors.TabIndex = 3;
            // 
            // comboBoxCampos
            // 
            this.comboBoxCampos.FormattingEnabled = true;
            this.comboBoxCampos.Items.AddRange(new object[] {
            "Temperature",
            "Humidity",
            "Battery"});
            this.comboBoxCampos.Location = new System.Drawing.Point(273, 73);
            this.comboBoxCampos.Name = "comboBoxCampos";
            this.comboBoxCampos.Size = new System.Drawing.Size(137, 21);
            this.comboBoxCampos.TabIndex = 4;
            // 
            // comboBoxOperacao
            // 
            this.comboBoxOperacao.FormattingEnabled = true;
            this.comboBoxOperacao.Items.AddRange(new object[] {
            "<",
            ">",
            "="});
            this.comboBoxOperacao.Location = new System.Drawing.Point(273, 115);
            this.comboBoxOperacao.Name = "comboBoxOperacao";
            this.comboBoxOperacao.Size = new System.Drawing.Size(137, 21);
            this.comboBoxOperacao.TabIndex = 5;
            // 
            // buttonCreateTrigger
            // 
            this.buttonCreateTrigger.Location = new System.Drawing.Point(273, 159);
            this.buttonCreateTrigger.Name = "buttonCreateTrigger";
            this.buttonCreateTrigger.Size = new System.Drawing.Size(74, 23);
            this.buttonCreateTrigger.TabIndex = 6;
            this.buttonCreateTrigger.Text = "Criar Trigger";
            this.buttonCreateTrigger.UseVisualStyleBackColor = true;
            // 
            // buttonSaveAll
            // 
            this.buttonSaveAll.Location = new System.Drawing.Point(353, 159);
            this.buttonSaveAll.Name = "buttonSaveAll";
            this.buttonSaveAll.Size = new System.Drawing.Size(57, 23);
            this.buttonSaveAll.TabIndex = 7;
            this.buttonSaveAll.Text = "Save All";
            this.buttonSaveAll.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSaveAll);
            this.Controls.Add(this.buttonCreateTrigger);
            this.Controls.Add(this.comboBoxOperacao);
            this.Controls.Add(this.comboBoxCampos);
            this.Controls.Add(this.comboBoxSensors);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxSensors;
        private System.Windows.Forms.ComboBox comboBoxCampos;
        private System.Windows.Forms.ComboBox comboBoxOperacao;
        private System.Windows.Forms.Button buttonCreateTrigger;
        private System.Windows.Forms.Button buttonSaveAll;
    }
}

