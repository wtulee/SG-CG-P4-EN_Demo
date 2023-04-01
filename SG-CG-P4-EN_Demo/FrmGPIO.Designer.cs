namespace SG_CG_P4_EN_Demo
{
    partial class FrmGPIO
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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.led_In1 = new Sunny.UI.UILight();
            this.led_In2 = new Sunny.UI.UILight();
            this.led_In3 = new Sunny.UI.UILight();
            this.led_In4 = new Sunny.UI.UILight();
            this.btn_Out1 = new Sunny.UI.UISwitch();
            this.btn_Out2 = new Sunny.UI.UISwitch();
            this.btn_Out3 = new Sunny.UI.UISwitch();
            this.btn_Out4 = new Sunny.UI.UISwitch();
            this.txt_Status = new Sunny.UI.UITextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(261, 237);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 19);
            this.label7.TabIndex = 31;
            this.label7.Text = "GPI_2";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(103, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 19);
            this.label8.TabIndex = 32;
            this.label8.Text = "GPI_1";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(577, 237);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 19);
            this.label12.TabIndex = 33;
            this.label12.Text = "GPI_4";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(419, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 19);
            this.label11.TabIndex = 34;
            this.label11.Text = "GPI_3";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(577, 317);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(54, 19);
            this.label47.TabIndex = 37;
            this.label47.Text = "GPO_4";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(419, 317);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(54, 19);
            this.label46.TabIndex = 38;
            this.label46.Text = "GPO_3";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(261, 317);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 19);
            this.label13.TabIndex = 39;
            this.label13.Text = "GPO_2";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(103, 317);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 19);
            this.label9.TabIndex = 40;
            this.label9.Text = "GPO_1";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Gainsboro;
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            this.label16.Location = new System.Drawing.Point(685, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(128, 56);
            this.label16.TabIndex = 45;
            this.label16.Text = "GPIO";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // led_In1
            // 
            this.led_In1.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            this.led_In1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.led_In1.Location = new System.Drawing.Point(161, 196);
            this.led_In1.MinimumSize = new System.Drawing.Size(1, 1);
            this.led_In1.Name = "led_In1";
            this.led_In1.Radius = 35;
            this.led_In1.Size = new System.Drawing.Size(60, 60);
            this.led_In1.State = Sunny.UI.UILightState.Off;
            this.led_In1.Style = Sunny.UI.UIStyle.Green;
            this.led_In1.TabIndex = 46;
            this.led_In1.Text = "uiLight1";
            // 
            // led_In2
            // 
            this.led_In2.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            this.led_In2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.led_In2.Location = new System.Drawing.Point(320, 196);
            this.led_In2.MinimumSize = new System.Drawing.Size(1, 1);
            this.led_In2.Name = "led_In2";
            this.led_In2.Radius = 35;
            this.led_In2.Size = new System.Drawing.Size(60, 60);
            this.led_In2.State = Sunny.UI.UILightState.Off;
            this.led_In2.Style = Sunny.UI.UIStyle.Green;
            this.led_In2.TabIndex = 46;
            this.led_In2.Text = "uiLight1";
            // 
            // led_In3
            // 
            this.led_In3.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            this.led_In3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.led_In3.Location = new System.Drawing.Point(479, 196);
            this.led_In3.MinimumSize = new System.Drawing.Size(1, 1);
            this.led_In3.Name = "led_In3";
            this.led_In3.Radius = 35;
            this.led_In3.Size = new System.Drawing.Size(60, 60);
            this.led_In3.State = Sunny.UI.UILightState.Off;
            this.led_In3.Style = Sunny.UI.UIStyle.Green;
            this.led_In3.TabIndex = 46;
            this.led_In3.Text = "uiLight1";
            // 
            // led_In4
            // 
            this.led_In4.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            this.led_In4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.led_In4.Location = new System.Drawing.Point(638, 196);
            this.led_In4.MinimumSize = new System.Drawing.Size(1, 1);
            this.led_In4.Name = "led_In4";
            this.led_In4.Radius = 35;
            this.led_In4.Size = new System.Drawing.Size(60, 60);
            this.led_In4.State = Sunny.UI.UILightState.Off;
            this.led_In4.Style = Sunny.UI.UIStyle.Green;
            this.led_In4.TabIndex = 46;
            this.led_In4.Text = "uiLight1";
            // 
            // btn_Out1
            // 
            this.btn_Out1.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_Out1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Out1.Location = new System.Drawing.Point(158, 299);
            this.btn_Out1.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Out1.Name = "btn_Out1";
            this.btn_Out1.Size = new System.Drawing.Size(75, 37);
            this.btn_Out1.Style = Sunny.UI.UIStyle.Custom;
            this.btn_Out1.TabIndex = 47;
            this.btn_Out1.Text = "uiSwitch1";
            this.btn_Out1.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.btn_Out1_ValueChanged);
            // 
            // btn_Out2
            // 
            this.btn_Out2.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_Out2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Out2.Location = new System.Drawing.Point(317, 299);
            this.btn_Out2.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Out2.Name = "btn_Out2";
            this.btn_Out2.Size = new System.Drawing.Size(75, 37);
            this.btn_Out2.Style = Sunny.UI.UIStyle.Custom;
            this.btn_Out2.TabIndex = 47;
            this.btn_Out2.Text = "uiSwitch1";
            this.btn_Out2.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.btn_Out2_ValueChanged);
            // 
            // btn_Out3
            // 
            this.btn_Out3.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_Out3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Out3.Location = new System.Drawing.Point(476, 299);
            this.btn_Out3.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Out3.Name = "btn_Out3";
            this.btn_Out3.Size = new System.Drawing.Size(75, 37);
            this.btn_Out3.Style = Sunny.UI.UIStyle.Custom;
            this.btn_Out3.TabIndex = 47;
            this.btn_Out3.Text = "uiSwitch1";
            this.btn_Out3.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.btn_Out3_ValueChanged);
            // 
            // btn_Out4
            // 
            this.btn_Out4.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_Out4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Out4.Location = new System.Drawing.Point(635, 299);
            this.btn_Out4.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Out4.Name = "btn_Out4";
            this.btn_Out4.Size = new System.Drawing.Size(75, 37);
            this.btn_Out4.Style = Sunny.UI.UIStyle.Custom;
            this.btn_Out4.TabIndex = 47;
            this.btn_Out4.Text = "uiSwitch1";
            this.btn_Out4.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.btn_Out4_ValueChanged);
            // 
            // txt_Status
            // 
            this.txt_Status.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Status.FillColor = System.Drawing.Color.White;
            this.txt_Status.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_Status.Location = new System.Drawing.Point(224, 135);
            this.txt_Status.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Status.Maximum = 2147483647D;
            this.txt_Status.Minimum = -2147483648D;
            this.txt_Status.MinimumSize = new System.Drawing.Size(1, 1);
            this.txt_Status.Name = "txt_Status";
            this.txt_Status.Padding = new System.Windows.Forms.Padding(5);
            this.txt_Status.ReadOnly = true;
            this.txt_Status.Size = new System.Drawing.Size(490, 29);
            this.txt_Status.TabIndex = 78;
            this.txt_Status.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(103, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 22);
            this.label5.TabIndex = 77;
            this.label5.Text = "OperStatus:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmGPIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(813, 583);
            this.Controls.Add(this.txt_Status);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_Out4);
            this.Controls.Add(this.btn_Out3);
            this.Controls.Add(this.btn_Out2);
            this.Controls.Add(this.btn_Out1);
            this.Controls.Add(this.led_In4);
            this.Controls.Add(this.led_In3);
            this.Controls.Add(this.led_In2);
            this.Controls.Add(this.led_In1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Font = new System.Drawing.Font("Consolas", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmGPIO";
            this.Text = "GPIO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGPIO_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private Sunny.UI.UILight led_In1;
        private Sunny.UI.UILight led_In2;
        private Sunny.UI.UILight led_In3;
        private Sunny.UI.UILight led_In4;
        private Sunny.UI.UISwitch btn_Out1;
        private Sunny.UI.UISwitch btn_Out2;
        private Sunny.UI.UISwitch btn_Out3;
        private Sunny.UI.UISwitch btn_Out4;
        private Sunny.UI.UITextBox txt_Status;
        private System.Windows.Forms.Label label5;
    }
}