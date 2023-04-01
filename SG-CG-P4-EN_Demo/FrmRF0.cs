using DAL;
using Entity;
using Sunny.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SG_CG_P4_EN_Demo
{
    public partial class FrmRF0 : Form
    {
        public FrmRF0()
        {
            InitializeComponent();
            this.Load += FrmRF0_Load;
        }

        private void FrmRF0_Load(object sender, EventArgs e)
        {
            if (GateWayService.LanguageStr == "zh-CN" || GateWayService.LanguageStr == "en-US")
            {
                MultiLanguage.SetDefaultLanguage(GateWayService.LanguageStr);
                MultiLanguage.LoadLanguage(this, typeof(FrmRF0));
            }
            this.btn_Enab.Active = GateWayService.reader0.Conn;
            this.GetConfigData();
            timer = new System.Timers.Timer(100);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private System.Timers.Timer timer;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    if (GateWayService.EN.IsConnect())
                    {
                        this.led_TagPre.State = GateWayService.reader0.TagPresent ? UILightState.On : UILightState.Off;
                        this.led_Conn.State = GateWayService.reader0.Conn ? UILightState.On : UILightState.Off;
                        this.txt_RSSI.Text = GateWayService.reader0.RSSI.ToString();
                        this.txt_UidLen.Text = GateWayService.reader0.DataLen.ToString();
                        this.txt_UID.Text = DataTran.GetHexArray(GateWayService.reader0.UidData.ByteArray);
                    }
                }));

            }
            catch (Exception)
            {
            }
        }

        private void btn_Enab_ValueChanged(object sender, bool value)
        {
            if (!GateWayService.EN.IsConnect())
            {
                AddLog(GateWayService.IsEN ? "Check Connection !" : "检查连接！");
                return;
            }
            if (btn_Enab.Active)
            {
                GateWayService.EN.EnableReader(Reader.RF0, true);
            }
            else
            {
                GateWayService.EN.EnableReader(Reader.RF0, false);
            }
        }


        private void btn_Read_Click(object sender, EventArgs e)
        {
            int Addr;
            if (!int.TryParse(this.txt_addr.Text.Trim(), out Addr))
            {
                AddLog(GateWayService.IsEN ? "Check Address !" : "检查地址！");
                return;
            }
            int Count;
            if (!int.TryParse(this.txt_Num.Text.Trim(), out Count) || Count <= 0)
            {
                AddLog(GateWayService.IsEN ? "Check Count !" : "检查长度！");
                return;
            }
            AddLog(GateWayService.IsEN ? "Reading!" : "读取中！");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ReadResult result = GateWayService.EN.ReadTagUsers(Reader.RF0, Addr, Count);
            stopwatch.Stop();
            this.txt_Time.Text = stopwatch.ElapsedMilliseconds.ToString();
            if (result.sataus == ExecutSataus.Cmd_Succeed)
            {
                string data = "";
                if (this.rdb_ASCII.Checked)
                {
                    data = DataTran.GetAsciiArray(result.TagUser.ByteArray);
                }
                else if (this.rdb_HEX.Checked)
                {
                    data = DataTran.GetHexArray(result.TagUser.ByteArray);
                }
                else if (this.rdb_Dec.Checked)
                {
                    StringBuilder str = new StringBuilder();
                    foreach (var item in result.TagUser.ByteArray)
                    {
                        str.Append(item.ToString() + " ");
                    }
                    data = str.ToString();
                }
                this.txt_UserData.Text = data;
                this.txt_Code.Text = "0";
            }
            else
            {
                this.txt_Code.Text = Convert.ToString(result.ErrCode, 16);
                this.txt_UserData.Text = "";
            }
            AddLog(result.sataus.ToString() + "!");
        }
        private async void btn_Write_Click(object sender, EventArgs e)
        {
            int Addr;
            if (!int.TryParse(this.txt_addr.Text.Trim(), out Addr))
            {
                AddLog(GateWayService.IsEN ? "Check Address !" : "检查地址！");
                return;
            }
            int Count;
            if (!int.TryParse(this.txt_Num.Text.Trim(), out Count) || Count <= 0)
            {
                AddLog(GateWayService.IsEN ? "Check Count !" : "检查长度！");
                return;
            }
            if (!(this.txt_WriteData.Text.Trim().Length > 0))
            {
                AddLog(GateWayService.IsEN ? "Check WriteData !" : "检查写入数据！");
                return;
            }
            AddLog(GateWayService.IsEN ? "Writing !" : "写入中！");

            ListByteArray data = new ListByteArray();
            try
            {
                if (this.rdb_ASCII.Checked)
                {
                    data.Add(DataTran.GetByteFromAsciiStr(this.txt_WriteData.Text.Trim()));
                }
                else if (this.rdb_HEX.Checked)
                {
                    data.Add(DataTran.GetByteFromHexStr(this.txt_WriteData.Text.Trim()));
                }
                else if (this.rdb_Dec.Checked)
                {
                    data.Add(DataTran.GetByteArray(this.txt_WriteData.Text.Trim()));
                }
            }
            catch (Exception ex)
            {
                AddLog(GateWayService.IsEN ? "Check WriteData,Value out of range! " : "检查写入数据，写入值超出范围！");
                return;
            }

            Task<WriteResult> t = new Task<WriteResult>(new Func<WriteResult>(() =>
            {
                WriteResult wres = GateWayService.EN.WriteTagUsers(Reader.RF0, Addr, Count, data.ByteArray);
                return wres;
            }));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            t.Start();
            WriteResult result = await t;
            stopwatch.Stop();
            this.txt_Time.Text = stopwatch.ElapsedMilliseconds.ToString();
            if (result.sataus == ExecutSataus.Cmd_Succeed)
            {
                this.txt_Code.Text = "0";
            }
            else
            {
                this.txt_Code.Text = Convert.ToString(result.ErrCode, 16);
            }
            AddLog(result.sataus.ToString() + " !");
        }

        private void AddLog(string v)
        {
            this.txt_Status.Text = v;
        }

        private void FrmRF0_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer.Stop();
            this.SaveConfigData();
        }

        private void SaveConfigData()
        {
            if (!File.Exists(GateWayService.IniFilePath))
            {
                FileStream fs = new FileStream(GateWayService.IniFilePath, FileMode.Create);
                fs.Close();
            }

            try
            {
                IniConfigHelper.WriteIniData(this.Text.Trim(), "Addr", this.txt_addr.Text.Trim(), GateWayService.IniFilePath);
                IniConfigHelper.WriteIniData(this.Text.Trim(), "Count", this.txt_Num.Text.Trim(), GateWayService.IniFilePath);
                IniConfigHelper.WriteIniData(this.Text.Trim(), "WriteData", this.txt_WriteData.Text.Trim(), GateWayService.IniFilePath);
                if (this.rdb_Dec.Checked)
                {
                    IniConfigHelper.WriteIniData(this.Text.Trim(), "Encoding", "1", GateWayService.IniFilePath);
                }
                else if (this.rdb_ASCII.Checked)
                {
                    IniConfigHelper.WriteIniData(this.Text.Trim(), "Encoding", "2", GateWayService.IniFilePath);
                }
                else if (this.rdb_HEX.Checked)
                {
                    IniConfigHelper.WriteIniData(this.Text.Trim(), "Encoding", "3", GateWayService.IniFilePath);
                }
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
                this.txt_addr.Text = IniConfigHelper.ReadIniData(this.Text.Trim(), "Addr", "", GateWayService.IniFilePath);
                this.txt_Num.Text = IniConfigHelper.ReadIniData(this.Text.Trim(), "Count", "", GateWayService.IniFilePath);
                int mode = Convert.ToInt32(IniConfigHelper.ReadIniData(this.Text.Trim(), "Encoding", "", GateWayService.IniFilePath));
                this.txt_WriteData.Text = IniConfigHelper.ReadIniData(this.Text.Trim(), "WriteData", "", GateWayService.IniFilePath);
                switch (mode)
                {
                    case 1:
                        this.rdb_Dec.Checked = true;
                        break;

                    case 2:
                        this.rdb_ASCII.Checked = true;
                        break;

                    case 3:
                        this.rdb_HEX.Checked = true;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}