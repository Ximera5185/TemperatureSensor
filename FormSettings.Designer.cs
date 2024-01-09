namespace TemperatureSensor
{
    partial class FormSettings
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
            this.trackBarTemperature = new System.Windows.Forms.TrackBar();
            this.labelSettingsShowTemperature = new System.Windows.Forms.Label();
            this.panelSettungs = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTemperature)).BeginInit();
            this.panelSettungs.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarTemperature
            // 
            this.trackBarTemperature.Location = new System.Drawing.Point(178, 12);
            this.trackBarTemperature.Maximum = 1000;
            this.trackBarTemperature.Name = "trackBarTemperature";
            this.trackBarTemperature.Size = new System.Drawing.Size(620, 45);
            this.trackBarTemperature.TabIndex = 0;
            this.trackBarTemperature.TickFrequency = 10;
            this.trackBarTemperature.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarTemperature.Scroll += new System.EventHandler(this.trackBarTemperature_Scroll);
            // 
            // labelSettingsShowTemperature
            // 
            this.labelSettingsShowTemperature.AutoSize = true;
            this.labelSettingsShowTemperature.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettingsShowTemperature.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.labelSettingsShowTemperature.Location = new System.Drawing.Point(136, 28);
            this.labelSettingsShowTemperature.MaximumSize = new System.Drawing.Size(50, 50);
            this.labelSettingsShowTemperature.Name = "labelSettingsShowTemperature";
            this.labelSettingsShowTemperature.Size = new System.Drawing.Size(36, 17);
            this.labelSettingsShowTemperature.TabIndex = 1;
            this.labelSettingsShowTemperature.Text = "1000";
            // 
            // panelSettungs
            // 
            this.panelSettungs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panelSettungs.Controls.Add(this.label2);
            this.panelSettungs.Controls.Add(this.label1);
            this.panelSettungs.Controls.Add(this.labelSettingsShowTemperature);
            this.panelSettungs.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSettungs.Location = new System.Drawing.Point(0, 0);
            this.panelSettungs.Name = "panelSettungs";
            this.panelSettungs.Size = new System.Drawing.Size(172, 450);
            this.panelSettungs.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.label1.Location = new System.Drawing.Point(-3, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Порог температуры";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.label2.Location = new System.Drawing.Point(44, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Настройки";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelSettungs);
            this.Controls.Add(this.trackBarTemperature);
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSettings";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTemperature)).EndInit();
            this.panelSettungs.ResumeLayout(false);
            this.panelSettungs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarTemperature;
        private System.Windows.Forms.Label labelSettingsShowTemperature;
        private System.Windows.Forms.Panel panelSettungs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}