namespace SG_CG_P4_EN_Demo
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.Title_Panal = new System.Windows.Forms.Panel();
            this.lbl_Min = new System.Windows.Forms.Label();
            this.lbl_Close = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsm_Set = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_IPconfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Langua = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_CN = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_EN = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Manual = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_Product = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_API = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_About = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_Soft = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_DisConn = new Sunny.UI.UIButton();
            this.btn_Conn = new Sunny.UI.UIButton();
            this.txt_Port = new Sunny.UI.UITextBox();
            this.txt_IP = new Sunny.UI.UITextBox();
            this.led_HeartBeat = new Sunny.UI.UILedBulb();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pal_View = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pal_Tab = new System.Windows.Forms.Panel();
            this.lbl_Back = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lbl_Test = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_RF3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_RF2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_RF1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_RF0 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_GPIO = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Title_Panal.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pal_Tab.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // Title_Panal
            // 
            this.Title_Panal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            this.Title_Panal.Controls.Add(this.lbl_Min);
            this.Title_Panal.Controls.Add(this.lbl_Close);
            this.Title_Panal.Controls.Add(this.label1);
            resources.ApplyResources(this.Title_Panal, "Title_Panal");
            this.Title_Panal.Name = "Title_Panal";
            this.Title_Panal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.Title_Panal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            // 
            // lbl_Min
            // 
            resources.ApplyResources(this.lbl_Min, "lbl_Min");
            this.lbl_Min.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_Min.ForeColor = System.Drawing.Color.White;
            this.lbl_Min.Image = global::SG_CG_P4_EN_Demo.Properties.Resources.icons8_minus_26px;
            this.lbl_Min.Name = "lbl_Min";
            this.lbl_Min.Click += new System.EventHandler(this.lbl_Min_Click);
            // 
            // lbl_Close
            // 
            resources.ApplyResources(this.lbl_Close, "lbl_Close");
            this.lbl_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_Close.ForeColor = System.Drawing.Color.White;
            this.lbl_Close.Image = global::SG_CG_P4_EN_Demo.Properties.Resources.icons8_shutdown_26px;
            this.lbl_Close.Name = "lbl_Close";
            this.lbl_Close.Click += new System.EventHandler(this.lbl_Close_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::SG_CG_P4_EN_Demo.Properties.Resources.icons8_rfid_sensor_26px;
            this.label1.Name = "label1";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Set,
            this.tsm_Langua,
            this.tsm_Manual,
            this.tsm_Help});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // tsm_Set
            // 
            resources.ApplyResources(this.tsm_Set, "tsm_Set");
            this.tsm_Set.BackColor = System.Drawing.Color.LightGray;
            this.tsm_Set.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_IPconfig});
            this.tsm_Set.Image = global::SG_CG_P4_EN_Demo.Properties.Resources.icons8_settings_26px;
            this.tsm_Set.Name = "tsm_Set";
            // 
            // tls_IPconfig
            // 
            resources.ApplyResources(this.tls_IPconfig, "tls_IPconfig");
            this.tls_IPconfig.Name = "tls_IPconfig";
            this.tls_IPconfig.Click += new System.EventHandler(this.tls_IPconfig_Click);
            // 
            // tsm_Langua
            // 
            resources.ApplyResources(this.tsm_Langua, "tsm_Langua");
            this.tsm_Langua.BackColor = System.Drawing.Color.LightGray;
            this.tsm_Langua.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_CN,
            this.tls_EN});
            this.tsm_Langua.Image = global::SG_CG_P4_EN_Demo.Properties.Resources.icons8_language_26px;
            this.tsm_Langua.Name = "tsm_Langua";
            // 
            // tls_CN
            // 
            resources.ApplyResources(this.tls_CN, "tls_CN");
            this.tls_CN.Name = "tls_CN";
            this.tls_CN.Click += new System.EventHandler(this.tls_CN_Click);
            // 
            // tls_EN
            // 
            resources.ApplyResources(this.tls_EN, "tls_EN");
            this.tls_EN.Name = "tls_EN";
            this.tls_EN.Click += new System.EventHandler(this.tls_EN_Click);
            // 
            // tsm_Manual
            // 
            resources.ApplyResources(this.tsm_Manual, "tsm_Manual");
            this.tsm_Manual.BackColor = System.Drawing.Color.LightGray;
            this.tsm_Manual.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_Product,
            this.tls_API});
            this.tsm_Manual.Name = "tsm_Manual";
            // 
            // tls_Product
            // 
            resources.ApplyResources(this.tls_Product, "tls_Product");
            this.tls_Product.Name = "tls_Product";
            this.tls_Product.Click += new System.EventHandler(this.tls_Product_Click);
            // 
            // tls_API
            // 
            resources.ApplyResources(this.tls_API, "tls_API");
            this.tls_API.Name = "tls_API";
            this.tls_API.Click += new System.EventHandler(this.tls_API_Click);
            // 
            // tsm_Help
            // 
            resources.ApplyResources(this.tsm_Help, "tsm_Help");
            this.tsm_Help.BackColor = System.Drawing.Color.LightGray;
            this.tsm_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_About,
            this.tls_Soft});
            this.tsm_Help.Name = "tsm_Help";
            // 
            // tls_About
            // 
            resources.ApplyResources(this.tls_About, "tls_About");
            this.tls_About.Name = "tls_About";
            this.tls_About.Click += new System.EventHandler(this.tls_About_Click);
            // 
            // tls_Soft
            // 
            resources.ApplyResources(this.tls_Soft, "tls_Soft");
            this.tls_Soft.Name = "tls_Soft";
            this.tls_Soft.Click += new System.EventHandler(this.tls_Soft_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btn_DisConn);
            this.panel2.Controls.Add(this.btn_Conn);
            this.panel2.Controls.Add(this.txt_Port);
            this.panel2.Controls.Add(this.txt_IP);
            this.panel2.Controls.Add(this.led_HeartBeat);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label9);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // btn_DisConn
            // 
            this.btn_DisConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DisConn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.btn_DisConn, "btn_DisConn");
            this.btn_DisConn.Name = "btn_DisConn";
            this.btn_DisConn.Style = Sunny.UI.UIStyle.Custom;
            this.btn_DisConn.Click += new System.EventHandler(this.btn_DisConn_Click);
            // 
            // btn_Conn
            // 
            this.btn_Conn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Conn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.btn_Conn, "btn_Conn");
            this.btn_Conn.Name = "btn_Conn";
            this.btn_Conn.Style = Sunny.UI.UIStyle.Custom;
            this.btn_Conn.Click += new System.EventHandler(this.btn_Conn_Click);
            // 
            // txt_Port
            // 
            this.txt_Port.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Port.DoubleValue = 502D;
            this.txt_Port.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_Port, "txt_Port");
            this.txt_Port.IntValue = 502;
            this.txt_Port.Maximum = 2147483647D;
            this.txt_Port.Minimum = -2147483648D;
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_IP
            // 
            this.txt_IP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_IP.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_IP, "txt_IP");
            this.txt_IP.Maximum = 2147483647D;
            this.txt_IP.Minimum = -2147483648D;
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // led_HeartBeat
            // 
            resources.ApplyResources(this.led_HeartBeat, "led_HeartBeat");
            this.led_HeartBeat.Name = "led_HeartBeat";
            this.led_HeartBeat.On = false;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pal_View);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.pal_Tab);
            this.panel3.Controls.Add(this.panel4);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // pal_View
            // 
            this.pal_View.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pal_View, "pal_View");
            this.pal_View.Name = "pal_View";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // pal_Tab
            // 
            this.pal_Tab.BackColor = System.Drawing.Color.White;
            this.pal_Tab.Controls.Add(this.lbl_Back);
            this.pal_Tab.Controls.Add(this.label18);
            this.pal_Tab.Controls.Add(this.lbl_Test);
            this.pal_Tab.Controls.Add(this.label12);
            this.pal_Tab.Controls.Add(this.lbl_RF3);
            this.pal_Tab.Controls.Add(this.label10);
            this.pal_Tab.Controls.Add(this.lbl_RF2);
            this.pal_Tab.Controls.Add(this.label8);
            this.pal_Tab.Controls.Add(this.lbl_RF1);
            this.pal_Tab.Controls.Add(this.label6);
            this.pal_Tab.Controls.Add(this.lbl_RF0);
            this.pal_Tab.Controls.Add(this.label5);
            this.pal_Tab.Controls.Add(this.lbl_GPIO);
            resources.ApplyResources(this.pal_Tab, "pal_Tab");
            this.pal_Tab.Name = "pal_Tab";
            // 
            // lbl_Back
            // 
            this.lbl_Back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.lbl_Back, "lbl_Back");
            this.lbl_Back.ForeColor = System.Drawing.Color.White;
            this.lbl_Back.Name = "lbl_Back";
            this.lbl_Back.Click += new System.EventHandler(this.lbl_Back_Click);
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label18, "label18");
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label18.Name = "label18";
            // 
            // lbl_Test
            // 
            this.lbl_Test.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.lbl_Test, "lbl_Test");
            this.lbl_Test.ForeColor = System.Drawing.Color.White;
            this.lbl_Test.Name = "lbl_Test";
            this.lbl_Test.Click += new System.EventHandler(this.lbl_Test_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label12, "label12");
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label12.Name = "label12";
            // 
            // lbl_RF3
            // 
            this.lbl_RF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.lbl_RF3, "lbl_RF3");
            this.lbl_RF3.ForeColor = System.Drawing.Color.White;
            this.lbl_RF3.Name = "lbl_RF3";
            this.lbl_RF3.Click += new System.EventHandler(this.lbl_RF3_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label10, "label10");
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label10.Name = "label10";
            // 
            // lbl_RF2
            // 
            this.lbl_RF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.lbl_RF2, "lbl_RF2");
            this.lbl_RF2.ForeColor = System.Drawing.Color.White;
            this.lbl_RF2.Name = "lbl_RF2";
            this.lbl_RF2.Click += new System.EventHandler(this.lbl_RF2_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label8, "label8");
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label8.Name = "label8";
            // 
            // lbl_RF1
            // 
            this.lbl_RF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.lbl_RF1, "lbl_RF1");
            this.lbl_RF1.ForeColor = System.Drawing.Color.White;
            this.lbl_RF1.Name = "lbl_RF1";
            this.lbl_RF1.Click += new System.EventHandler(this.lbl_RF1_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label6, "label6");
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Name = "label6";
            // 
            // lbl_RF0
            // 
            this.lbl_RF0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.lbl_RF0, "lbl_RF0");
            this.lbl_RF0.ForeColor = System.Drawing.Color.White;
            this.lbl_RF0.Name = "lbl_RF0";
            this.lbl_RF0.Click += new System.EventHandler(this.lbl_RF0_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label5, "label5");
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label5.Name = "label5";
            // 
            // lbl_GPIO
            // 
            this.lbl_GPIO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            resources.ApplyResources(this.lbl_GPIO, "lbl_GPIO");
            this.lbl_GPIO.ForeColor = System.Drawing.Color.White;
            this.lbl_GPIO.Name = "lbl_GPIO";
            this.lbl_GPIO.Click += new System.EventHandler(this.lbl_GPIO_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(166)))));
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.lbl_Time);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.label3);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Name = "label7";
            // 
            // lbl_Time
            // 
            resources.ApplyResources(this.lbl_Time, "lbl_Time");
            this.lbl_Time.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_Time.ForeColor = System.Drawing.Color.White;
            this.lbl_Time.Name = "lbl_Time";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Title_Panal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Title_Panal.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pal_Tab.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Title_Panal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsm_Langua;
        private System.Windows.Forms.ToolStripMenuItem tsm_Manual;
        private System.Windows.Forms.ToolStripMenuItem tsm_Help;
        private System.Windows.Forms.ToolStripMenuItem tls_CN;
        private System.Windows.Forms.ToolStripMenuItem tsm_Set;
        private System.Windows.Forms.ToolStripMenuItem tls_IPconfig;
        private System.Windows.Forms.ToolStripMenuItem tls_EN;
        private System.Windows.Forms.ToolStripMenuItem tls_Product;
        private System.Windows.Forms.ToolStripMenuItem tls_About;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pal_View;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel pal_Tab;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_GPIO;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_RF3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_RF2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_RF1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_RF0;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lbl_Test;
        private Sunny.UI.UILedBulb led_HeartBeat;
        private Sunny.UI.UITextBox txt_Port;
        private Sunny.UI.UITextBox txt_IP;
        private Sunny.UI.UIButton btn_DisConn;
        private Sunny.UI.UIButton btn_Conn;
        private System.Windows.Forms.Label lbl_Close;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Min;
        private System.Windows.Forms.Label lbl_Back;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem tls_API;
        private System.Windows.Forms.ToolStripMenuItem tls_Soft;
    }
}