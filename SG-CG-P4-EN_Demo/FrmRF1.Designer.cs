namespace SG_CG_P4_EN_Demo
{
    partial class FrmRF1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRF1));
            this.label16 = new System.Windows.Forms.Label();
            this.txt_UserData = new Sunny.UI.UITextBox();
            this.txt_WriteData = new Sunny.UI.UITextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Num = new Sunny.UI.UITextBox();
            this.txt_Time = new Sunny.UI.UITextBox();
            this.txt_Code = new Sunny.UI.UITextBox();
            this.txt_UidLen = new Sunny.UI.UITextBox();
            this.txt_Status = new Sunny.UI.UITextBox();
            this.txt_UID = new Sunny.UI.UITextBox();
            this.txt_addr = new Sunny.UI.UITextBox();
            this.txt_RSSI = new Sunny.UI.UITextBox();
            this.rdb_HEX = new Sunny.UI.UIRadioButton();
            this.rdb_ASCII = new Sunny.UI.UIRadioButton();
            this.rdb_Dec = new Sunny.UI.UIRadioButton();
            this.btn_Write = new Sunny.UI.UIButton();
            this.btn_Read = new Sunny.UI.UIButton();
            this.btn_Enab = new Sunny.UI.UISwitch();
            this.led_TagPre = new Sunny.UI.UILight();
            this.label5 = new System.Windows.Forms.Label();
            this.led_Conn = new Sunny.UI.UILight();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.BackColor = System.Drawing.Color.Gainsboro;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            this.label16.Name = "label16";
            // 
            // txt_UserData
            // 
            resources.ApplyResources(this.txt_UserData, "txt_UserData");
            this.txt_UserData.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_UserData.FillColor = System.Drawing.Color.White;
            this.txt_UserData.Maximum = 2147483647D;
            this.txt_UserData.Minimum = -2147483648D;
            this.txt_UserData.Multiline = true;
            this.txt_UserData.Name = "txt_UserData";
            this.txt_UserData.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txt_WriteData
            // 
            resources.ApplyResources(this.txt_WriteData, "txt_WriteData");
            this.txt_WriteData.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_WriteData.FillColor = System.Drawing.Color.White;
            this.txt_WriteData.Maximum = 2147483647D;
            this.txt_WriteData.Minimum = -2147483648D;
            this.txt_WriteData.Multiline = true;
            this.txt_WriteData.Name = "txt_WriteData";
            this.txt_WriteData.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txt_Num
            // 
            this.txt_Num.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Num.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_Num, "txt_Num");
            this.txt_Num.Maximum = 2147483647D;
            this.txt_Num.Minimum = -2147483648D;
            this.txt_Num.Name = "txt_Num";
            this.txt_Num.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Time
            // 
            this.txt_Time.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Time.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_Time, "txt_Time");
            this.txt_Time.Maximum = 2147483647D;
            this.txt_Time.Minimum = -2147483648D;
            this.txt_Time.Name = "txt_Time";
            this.txt_Time.ReadOnly = true;
            this.txt_Time.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Code
            // 
            this.txt_Code.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Code.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_Code, "txt_Code");
            this.txt_Code.Maximum = 2147483647D;
            this.txt_Code.Minimum = -2147483648D;
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.ReadOnly = true;
            this.txt_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_UidLen
            // 
            this.txt_UidLen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_UidLen.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_UidLen, "txt_UidLen");
            this.txt_UidLen.Maximum = 2147483647D;
            this.txt_UidLen.Minimum = -2147483648D;
            this.txt_UidLen.Name = "txt_UidLen";
            this.txt_UidLen.ReadOnly = true;
            this.txt_UidLen.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Status
            // 
            this.txt_Status.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Status.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_Status, "txt_Status");
            this.txt_Status.Maximum = 2147483647D;
            this.txt_Status.Minimum = -2147483648D;
            this.txt_Status.Name = "txt_Status";
            this.txt_Status.ReadOnly = true;
            this.txt_Status.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_UID
            // 
            this.txt_UID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_UID.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_UID, "txt_UID");
            this.txt_UID.Maximum = 2147483647D;
            this.txt_UID.Minimum = -2147483648D;
            this.txt_UID.Name = "txt_UID";
            this.txt_UID.ReadOnly = true;
            this.txt_UID.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_addr
            // 
            this.txt_addr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_addr.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_addr, "txt_addr");
            this.txt_addr.Maximum = 2147483647D;
            this.txt_addr.Minimum = -2147483648D;
            this.txt_addr.Name = "txt_addr";
            this.txt_addr.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_RSSI
            // 
            this.txt_RSSI.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_RSSI.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_RSSI, "txt_RSSI");
            this.txt_RSSI.Maximum = 2147483647D;
            this.txt_RSSI.Minimum = -2147483648D;
            this.txt_RSSI.Name = "txt_RSSI";
            this.txt_RSSI.ReadOnly = true;
            this.txt_RSSI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdb_HEX
            // 
            this.rdb_HEX.Checked = true;
            this.rdb_HEX.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.rdb_HEX, "rdb_HEX");
            this.rdb_HEX.Name = "rdb_HEX";
            // 
            // rdb_ASCII
            // 
            this.rdb_ASCII.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.rdb_ASCII, "rdb_ASCII");
            this.rdb_ASCII.Name = "rdb_ASCII";
            // 
            // rdb_Dec
            // 
            this.rdb_Dec.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.rdb_Dec, "rdb_Dec");
            this.rdb_Dec.Name = "rdb_Dec";
            // 
            // btn_Write
            // 
            this.btn_Write.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btn_Write, "btn_Write");
            this.btn_Write.Name = "btn_Write";
            this.btn_Write.Click += new System.EventHandler(this.btn_Write_Click);
            // 
            // btn_Read
            // 
            this.btn_Read.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btn_Read, "btn_Read");
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
            // 
            // btn_Enab
            // 
            this.btn_Enab.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.btn_Enab, "btn_Enab");
            this.btn_Enab.Name = "btn_Enab";
            this.btn_Enab.Style = Sunny.UI.UIStyle.Custom;
            this.btn_Enab.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.btn_Enab_ValueChanged);
            // 
            // led_TagPre
            // 
            this.led_TagPre.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            resources.ApplyResources(this.led_TagPre, "led_TagPre");
            this.led_TagPre.Name = "led_TagPre";
            this.led_TagPre.Radius = 35;
            this.led_TagPre.State = Sunny.UI.UILightState.Off;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // led_Conn
            // 
            this.led_Conn.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            resources.ApplyResources(this.led_Conn, "led_Conn");
            this.led_Conn.Name = "led_Conn";
            this.led_Conn.Radius = 35;
            this.led_Conn.State = Sunny.UI.UILightState.Off;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.Name = "label49";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FrmRF1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txt_UserData);
            this.Controls.Add(this.txt_WriteData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Num);
            this.Controls.Add(this.txt_Time);
            this.Controls.Add(this.txt_Code);
            this.Controls.Add(this.txt_UidLen);
            this.Controls.Add(this.txt_Status);
            this.Controls.Add(this.txt_UID);
            this.Controls.Add(this.txt_addr);
            this.Controls.Add(this.txt_RSSI);
            this.Controls.Add(this.rdb_HEX);
            this.Controls.Add(this.rdb_ASCII);
            this.Controls.Add(this.rdb_Dec);
            this.Controls.Add(this.btn_Write);
            this.Controls.Add(this.btn_Read);
            this.Controls.Add(this.btn_Enab);
            this.Controls.Add(this.led_TagPre);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.led_Conn);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRF1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRF1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label16;
        private Sunny.UI.UITextBox txt_UserData;
        private Sunny.UI.UITextBox txt_WriteData;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UITextBox txt_Num;
        private Sunny.UI.UITextBox txt_Time;
        private Sunny.UI.UITextBox txt_Code;
        private Sunny.UI.UITextBox txt_UidLen;
        private Sunny.UI.UITextBox txt_Status;
        private Sunny.UI.UITextBox txt_UID;
        private Sunny.UI.UITextBox txt_addr;
        private Sunny.UI.UITextBox txt_RSSI;
        private Sunny.UI.UIRadioButton rdb_HEX;
        private Sunny.UI.UIRadioButton rdb_ASCII;
        private Sunny.UI.UIRadioButton rdb_Dec;
        private Sunny.UI.UIButton btn_Write;
        private Sunny.UI.UIButton btn_Read;
        private Sunny.UI.UISwitch btn_Enab;
        private Sunny.UI.UILight led_TagPre;
        private System.Windows.Forms.Label label5;
        private Sunny.UI.UILight led_Conn;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label1;
    }
}