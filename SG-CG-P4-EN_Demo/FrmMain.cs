using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_CG_P4_EN_Demo
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;
        }

        private System.Windows.Forms.Timer _1sTimer = new System.Windows.Forms.Timer();

        private void _1sTimer_Tick(object sender, EventArgs e)
        {
            this.lbl_Time.Text = DateTime.Now.ToString();
            if (GateWayService.EN.IsConnect())
            {
                this.led_HeartBeat.On = true;
            }
            else
            {
                GateWayService.TestStatus = false;
                this.led_HeartBeat.On = false;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.GetConfigData();
            _ = GateWayService.IsEN ? this.tls_EN.Image = Properties.Resources.icons8_checkmark_26px : this.tls_CN.Image = Properties.Resources.icons8_checkmark_26px;
            this.lbl_Back_Click(null, null);
            GateWayService.EN.DeviceMonitor += EN_DeviceMonitor;
            this.lbl_Time.Text = DateTime.Now.ToString();
            this._1sTimer.Interval = 100;
            this._1sTimer.Tick += _1sTimer_Tick;
            this._1sTimer.Enabled = true;
            this._1sTimer.Start();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._1sTimer.Stop();
            this.SaveConfigData();
        }

        private void EN_DeviceMonitor(Dictionary<Reader, ReaderMonitor> ReaderInfos, GPIO IOInfos)
        {
            GateWayService.GPIN = IOInfos;
            GateWayService.reader0 = ReaderInfos[Reader.RF0];
            GateWayService.reader1 = ReaderInfos[Reader.RF1];
            GateWayService.reader2 = ReaderInfos[Reader.RF2];
            GateWayService.reader3 = ReaderInfos[Reader.RF3];
        }

        private void lbl_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbl_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = true;
        }

        #region PageTag

        private void lbl_GPIO_Click(object sender, EventArgs e)
        {
            if (GateWayService.TestStatus)
            {
                string info = GateWayService.IsEN ? "Are you Leaving? Please stop the test first!" : "请先关闭测试再离开！";
                string title = GateWayService.IsEN ? "Continuat Test" : "压力测试";
                MessInfo(info, title);
                return;
            }
            FrmGPIO objFrm = new FrmGPIO();
            if (!CloseFrm(objFrm, this.pal_View))
            {
                OpenFrm(objFrm, this.pal_View);
            }
        }

        private void lbl_RF0_Click(object sender, EventArgs e)
        {
            if (GateWayService.TestStatus)
            {
                string info = GateWayService.IsEN ? "Are you Leaving? Please stop the test first!" : "请先关闭测试再离开！";
                string title = GateWayService.IsEN ? "Continuat Test" : "压力测试";
                MessInfo(info, title);
                return;
            }
            FrmRF0 objFrm = new FrmRF0();
            if (!CloseFrm(objFrm, this.pal_View))
            {
                OpenFrm(objFrm, this.pal_View);
            }
        }

        private void lbl_RF1_Click(object sender, EventArgs e)
        {
            if (GateWayService.TestStatus)
            {
                string info = GateWayService.IsEN ? "Are you Leaving? Please stop the test first!" : "请先关闭测试再离开！";
                string title = GateWayService.IsEN ? "Continuat Test" : "压力测试";
                MessInfo(info, title);
                return;
            }
            FrmRF1 objFrm = new FrmRF1();
            if (!CloseFrm(objFrm, this.pal_View))
            {
                OpenFrm(objFrm, this.pal_View);
            }
        }

        private void lbl_RF2_Click(object sender, EventArgs e)
        {
            if (GateWayService.TestStatus)
            {
                string info = GateWayService.IsEN ? "Are you Leaving? Please stop the test first!" : "请先关闭测试再离开！";
                string title = GateWayService.IsEN ? "Continuat Test" : "压力测试";
                MessInfo(info, title);
                return;
            }
            FrmRF2 objFrm = new FrmRF2();
            if (!CloseFrm(objFrm, this.pal_View))
            {
                OpenFrm(objFrm, this.pal_View);
            }
        }

        private void lbl_RF3_Click(object sender, EventArgs e)
        {
            if (GateWayService.TestStatus)
            {
                string info = GateWayService.IsEN ? "Are you Leaving? Please stop the test first!" : "请先关闭测试再离开！";
                string title = GateWayService.IsEN ? "Continuat Test" : "压力测试";
                MessInfo(info, title);
                return;
            }
            FrmRF3 objFrm = new FrmRF3();
            if (!CloseFrm(objFrm, this.pal_View))
            {
                OpenFrm(objFrm, this.pal_View);
            }
        }

        private void lbl_Test_Click(object sender, EventArgs e)
        {
            FrmTest objFrm = new FrmTest();
            if (!CloseFrm(objFrm, this.pal_View))
            {
                OpenFrm(objFrm, this.pal_View);
            }
        }

        private void lbl_Back_Click(object sender, EventArgs e)
        {
            if (GateWayService.TestStatus)
            {
                string info = GateWayService.IsEN ? "Are you Leaving? Please stop the test first!" : "请先关闭测试再离开！";
                string title = GateWayService.IsEN ? "Continuat Test" : "压力测试";
                MessInfo(info, title);
                return;
            }
            FrmBack objFrm = new FrmBack();
            if (!CloseFrm(objFrm, this.pal_View))
            {
                OpenFrm(objFrm, this.pal_View);
            }
        }

        private void tls_IPconfig_Click(object sender, EventArgs e)
        {
            FrmIPConfig frmIPConfig = new FrmIPConfig();
            frmIPConfig.ShowDialog();
        }

        private void tls_Product_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\Manual\\TCP网关技术规格书.pdf");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void tls_About_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("iexplore.exe", "https://www.sygole.com/");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void tls_API_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\Manual\\SG_CG_P4_EN软件操作说明.pdf");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void tls_Soft_Click(object sender, EventArgs e)
        {
        }

        #endregion PageTag

        #region Reader

        private async void btn_Conn_Click(object sender, EventArgs e)
        {
            IPAddress ip;
            int port;
            if (!IPAddress.TryParse(this.txt_IP.Text.Trim(), out ip))
            {
                MessageBox.Show(GateWayService.IsEN ? "Check IPAddress":"检查IP地址", GateWayService.IsEN ? "Input Parameter":"连接参数", MessageBoxButtons.OK);
                return;
            }
            if (!int.TryParse(this.txt_Port.Text.Trim(), out port))
            {
                MessageBox.Show(GateWayService.IsEN ? "Check Port":"检查端口配置", GateWayService.IsEN ? "Input Parameter":"连接参数", MessageBoxButtons.OK);
                return;
            }
            GateWayService.EN.Ip = this.txt_IP.Text.Trim();
            GateWayService.EN.Port = this.txt_Port.Text.Trim();
            //GateWayService.EN.AutoConnectMode = true;
            Task<bool> t = new Task<bool>(new Func<bool>(() =>
            {
                bool result = GateWayService.EN.Connect();
                return result;
            }));
            t.Start();
            bool res = await t;
            if (res)
            {
                this.btn_Conn.Enabled = false;
                this.btn_DisConn.Enabled = true;
            }
            else
            {
                MessageBox.Show(GateWayService.IsEN ? "Connection Fail,Please check the configuration...":"连接失败，请检查配置...", GateWayService.IsEN ? "Connection":"连接状态", MessageBoxButtons.OK);
            }
            this.btn_DisConn.Focus();
        }

        private void btn_DisConn_Click(object sender, EventArgs e)
        {
            GateWayService.EN.DisConnect();
            this.btn_Conn.Enabled = true;
            this.btn_DisConn.Enabled = false;
        }

        #endregion Reader

        #region Language

        private void tls_CN_Click(object sender, EventArgs e)
        {
            if (GateWayService.LanguageStr == "zh-CN")
            {
                return;
            }
            if (!File.Exists(GateWayService.IniFilePath))
            {
                FileStream fs = new FileStream(GateWayService.IniFilePath, FileMode.Create);
                fs.Close();
            }
            try
            {
                IniConfigHelper.WriteIniData("Language", "Switch", "zh-CN", GateWayService.IniFilePath);
                MessageBox.Show("This will take effect after restarting the software！", "Language Switch", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void tls_EN_Click(object sender, EventArgs e)
        {
            if (GateWayService.LanguageStr == "en-US")
            {
                return;
            }
            if (!File.Exists(GateWayService.IniFilePath))
            {
                FileStream fs = new FileStream(GateWayService.IniFilePath, FileMode.Create);
                fs.Close();
            }
            try
            {
                IniConfigHelper.WriteIniData("Language", "Switch", "en-US", GateWayService.IniFilePath);
                MessageBox.Show("重启软件后生效！", "语言切换", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Language

        #region TabControl

        private bool CloseFrm(Form Frm, Panel panel)
        {
            bool res = false;
            foreach (Control item in panel.Controls)
            {
                if (item is Form frm)
                {
                    if (frm.Name == Frm.Name)
                    {
                        res = true;
                        break;
                    }
                    else
                    {
                        frm.Close();
                        frm.Dispose();
                    }
                }
            }
            return res;
        }

        private void OpenFrm(Form Frm, Panel panel)
        {
            Frm.TopLevel = false;
            Frm.FormBorderStyle = FormBorderStyle.None;
            Frm.Dock = DockStyle.Fill;
            Frm.Parent = panel;
            BtnBackColorSet(Frm.Text);
            Frm.Show();
        }

        private void BtnBackColorSet(string FrmTitle)
        {
            this.lbl_GPIO.BackColor = this.lbl_RF0.BackColor = this.lbl_RF1.BackColor = this.lbl_RF2.BackColor = this.lbl_RF3.BackColor = this.lbl_Test.BackColor = this.lbl_Back.BackColor = Color.FromArgb(0, 70, 166);
            switch (FrmTitle)
            {
                case "GPIO":
                    this.lbl_GPIO.BackColor = Color.FromArgb(50, 124, 46);
                    break;

                case "RF0":
                    this.lbl_RF0.BackColor = Color.FromArgb(50, 124, 46);
                    break;

                case "RF1":
                    this.lbl_RF1.BackColor = Color.FromArgb(50, 124, 46);
                    break;

                case "RF2":
                    this.lbl_RF2.BackColor = Color.FromArgb(50, 124, 46);
                    break;

                case "RF3":
                    this.lbl_RF3.BackColor = Color.FromArgb(50, 124, 46);
                    break;

                case "TEST":
                    this.lbl_Test.BackColor = Color.FromArgb(50, 124, 46);
                    break;

                case "Home":
                    this.lbl_Back.BackColor = Color.FromArgb(50, 124, 46);
                    break;

                default:
                    break;
            }
        }

        #endregion TabControl

        #region FrmMove

        private Point mPoint;

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        #endregion FrmMove

        #region Config

        private void SaveConfigData()
        {
            if (!File.Exists(GateWayService.IniFilePath))
            {
                FileStream fs = new FileStream(GateWayService.IniFilePath, FileMode.Create);
                fs.Close();
            }
            try
            {
                IniConfigHelper.WriteIniData("Main", "IP", this.txt_IP.Text.Trim(), GateWayService.IniFilePath);
                IniConfigHelper.WriteIniData("Main", "Port", this.txt_Port.Text.Trim(), GateWayService.IniFilePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GetConfigData()
        {
            try
            {
                GateWayService.LanguageStr = IniConfigHelper.ReadIniData("Language", "Switch", "", GateWayService.IniFilePath);
                if (GateWayService.LanguageStr == "zh-CN" || GateWayService.LanguageStr == "en-US")
                {
                    MultiLanguage.SetDefaultLanguage(GateWayService.LanguageStr);
                    MultiLanguage.LoadLanguage(this, typeof(FrmMain));
                }
                this.txt_IP.Text = IniConfigHelper.ReadIniData("Main", "IP", "", GateWayService.IniFilePath);
                //this.txt_Port.Text = IniConfigHelper.ReadIniData("Main", "Port", "", GateWayService.IniFilePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Config

        private void MessInfo(string info, string title)
        {
            if (info.Length == 0 || title.Length == 0)
            {
                return;
            }
            MessageBox.Show(info, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}