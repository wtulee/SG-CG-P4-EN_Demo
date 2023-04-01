using DAL;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SG_CG_P4_EN_Demo
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
            this.Load += FrmTest_Load;
        }

        private System.Timers.Timer timer;

        private CancellationTokenSource cts;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (GateWayService.EN.IsConnect())
                {
                    Invoke(new Action(() =>
                    {
                        switch (this.cbo_RfCh.SelectedIndex)
                        {
                            case 0:
                                this.sde_RSSI.Value = GateWayService.reader0.RSSI;
                                break;

                            case 1:
                                this.sde_RSSI.Value = GateWayService.reader1.RSSI;
                                break;

                            case 2:
                                this.sde_RSSI.Value = GateWayService.reader2.RSSI;
                                break;

                            case 3:
                                this.sde_RSSI.Value = GateWayService.reader3.RSSI;
                                break;

                            default:
                                break;
                        }
                    }));
                }
            }
            catch (Exception)
            {
            }
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            if (GateWayService.LanguageStr == "zh-CN" || GateWayService.LanguageStr == "en-US")
            {
                MultiLanguage.SetDefaultLanguage(GateWayService.LanguageStr);
                MultiLanguage.LoadLanguage(this, typeof(FrmTest));
            }

            this.btn_Close.Enabled = false;
            this.cbo_RfCh.Items.AddRange(new string[]
            {
                Reader.RF0.ToString(),
                Reader.RF1.ToString(),
                Reader.RF2.ToString(),
                Reader.RF3.ToString()
            });
            this.GetConfigData();
            timer = new System.Timers.Timer(100);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void btn_Open_Click(object sender, EventArgs e)
        {
            GateWayService.TestStatus = true;

            if (this.cbo_RfCh.Text.Trim() == "" || this.cbo_RfCh.Text.Length == 0)
            {
                AddLog(GateWayService.IsEN ? "Check RF-CH !" : "检查通道！");
                return;
            }
            this.EnableReader();
            int MaxCount;
            if (!int.TryParse(this.txt_MaxNum.Text.Trim(), out MaxCount))
            {
                AddLog(GateWayService.IsEN ? "Check MaxCount !" : "检查最大次数！");
                return;
            }
            int cycleTime;
            if (!int.TryParse(this.txt_CycleTime.Text.Trim(), out cycleTime))
            {
                AddLog(GateWayService.IsEN ? "Check CycleTime !" : "检查测试间隔时间！");
                return;
            }
            int Addr;
            if (!int.TryParse(this.txt_addr.Text.Trim(), out Addr))
            {
                AddLog(GateWayService.IsEN ? "Check Address !" : "检查地址！");
                return;
            }
            int Count;
            if ((!int.TryParse(this.txt_Num.Text.Trim(), out Count)) || Count <= 0)
            {
                AddLog(GateWayService.IsEN ? "Check Count !" : "检查长度！");
                return;
            }
            if (this.txt_WriteData.Text.Trim().Length <= 0 && (this.rdb_W.Checked || this.rdb_RW.Checked))
            {
                AddLog(GateWayService.IsEN ? "Check WriteData !" : "检查写入数据！");
                return;
            }
            if (this.txt_WriteData.Text.Trim().Length > 0 && (this.rdb_W.Checked || this.rdb_RW.Checked))
            {
                int DataLen;
                try
                {
                    DataLen = DataTran.GetByteFromHexStr(this.txt_WriteData.Text.Trim()).Length;
                }
                catch (Exception)
                {
                    AddLog(GateWayService.IsEN ? "Check WriteData,Value out of range! " : "检查写入数据，写入值超出范围！");
                    return;
                }
                if (DataLen != Count)
                {
                    AddLog(GateWayService.IsEN ? "Check Count !" : "检查长度！");
                    return;
                }
            }
            DataReportR(0, 0, "0", "0");
            DataReportW(0, 0, "0", "0");
            this.btn_Open.Enabled = false;
            this.btn_Close.Enabled = true;
            this.gbx_Mode.Enabled = false;
            this.gbx_RfCh.Enabled = false;
            this.gbx_RW.Enabled = false;
            int RfCh = this.cbo_RfCh.SelectedIndex;
            int RWMode = 0;
            if (this.rdb_R.Checked)
            {
                RWMode = 1;
            }
            else if (this.rdb_W.Checked)
            {
                RWMode = 2;
            }
            else if (this.rdb_RW.Checked)
            {
                RWMode = 3;
            }
            cts = new CancellationTokenSource();

            AddLog(GateWayService.IsEN ? "Continuat Test......" : "压力测试中......");

            cycleTime = cycleTime + 10;
            Task<bool> t = Task<bool>.Run(new Func<bool>(() =>
            {
                try
                {
                    return Test(MaxCount, cycleTime, Addr, Count, RfCh, RWMode, DataTran.GetByteFromHexStr(this.txt_WriteData.Text.Trim()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(GateWayService.IsEN ? "Communication Failure..." : "异常断开连接...", GateWayService.IsEN ? "Error" : "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return true;
                }
            }), cts.Token);
            bool result = await t;
            this.btn_Open.Enabled = true;
            if (result)
            {
                this.btn_Close_Click(null, null);
            }
            AddLog(GateWayService.IsEN ? "End of Continuat Test!" : "压力测试结束！");
            GateWayService.TestStatus = false;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.gbx_Mode.Enabled = true;
            this.gbx_RfCh.Enabled = true;
            this.gbx_RW.Enabled = true;
            AddLog(GateWayService.IsEN ? "End of Continuat Test!" : "压力测试结束！");
            this.btn_Open.Enabled = true;
            cts?.Cancel();
            GateWayService.TestStatus = false;
        }

        private void FrmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.btn_Close_Click(null, null);
            this.timer.Stop();
            this.SaveConfigData();
        }

        private bool Test(int maxCount, int cycleTime, int addr, int count, int rfCh, int rwMode, byte[] writeData)
        {
            int ctCount = 0;
            Reader reader = Reader.RF0;
            switch (rfCh)
            {
                case 0:
                    reader = Reader.RF0;
                    break;

                case 1:
                    reader = Reader.RF1;
                    break;

                case 2:
                    reader = Reader.RF2;
                    break;

                case 3:
                    reader = Reader.RF3;
                    break;
            }
            Thread.Sleep(200);
            Stopwatch stopwatchR = new Stopwatch();
            Stopwatch stopwatchW = new Stopwatch();
            int ReadTotal = 0;
            int ReadFail = 0;
            int WriteTotal = 0;
            int WriteFail = 0;
            string CurruentR = "";
            string CurruentW = "";
            string ReadRate = "";
            string WriteRate = "";

            while ((!cts.IsCancellationRequested) && ctCount < maxCount && GateWayService.EN.IsConnect())
            {
                ctCount++;
                if (rwMode > 1)
                {
                    stopwatchW.Restart();
                    WriteResult Wres = GateWayService.EN.WriteTagUsers(reader, addr, count, writeData);
                    stopwatchW.Stop();
                    CurruentW = stopwatchW.ElapsedMilliseconds.ToString();
                    if (Wres.sataus == ExecutSataus.Cmd_Succeed)
                    {
                        WriteTotal++;
                    }
                    else
                    {
                        WriteFail++;
                    }
                    double r = Convert.ToDouble(WriteTotal) / Convert.ToDouble(ctCount);
                    WriteRate = r.ToString("0.##%");
                    DataReportW(ctCount, WriteFail, CurruentW, WriteRate);
                }
                Thread.Sleep(cycleTime);
                if (rwMode == 1 || rwMode == 3)
                {
                    stopwatchR.Restart();
                    ReadResult Rres = GateWayService.EN.ReadTagUsers(reader, addr, count);
                    stopwatchR.Stop();
                    CurruentR = stopwatchR.ElapsedMilliseconds.ToString();
                    if (Rres.sataus == ExecutSataus.Cmd_Succeed)
                    {
                        if (rwMode == 3)
                        {
                            if (DataTran.ByteArrayEquals(Rres.TagUser.ByteArray, writeData))
                            {
                                ReadTotal++;
                            }
                        }
                        else
                        {
                            ReadTotal++;
                        }
                    }
                    else
                    {
                        ReadFail++;
                    }
                    double r = Convert.ToDouble(ReadTotal) / Convert.ToDouble(ctCount);
                    ReadRate = r.ToString("0.##%");
                    DataReportR(ctCount, ReadFail, CurruentR, ReadRate);
                }
                Thread.Sleep(cycleTime);
            }
            return true;
        }

        private void DataReportR(int ctCount, int readFail, string curruentR, string readRate)
        {
            BeginInvoke(new Action(() =>
            {
                this.txt_ReadNum.Text = ctCount.ToString();
                this.txt_ReadFailNum.Text = readFail.ToString();
                this.txt_ReadCurrTime.Text = curruentR;
                this.txt_ReadSuccRate.Text = readRate;
            }));
        }

        private void DataReportW(int ctCount, int writeFail, string curruentW, string writeRate)
        {
            BeginInvoke(new Action(() =>
            {
                this.txt_WriteNum.Text = ctCount.ToString();
                this.txt_WriteFailNum.Text = writeFail.ToString();
                this.txt_WriteCurrTime.Text = curruentW;
                this.txt_WriteSuccRate.Text = writeRate;
            }));
        }

        private void AddLog(string v)
        {
            this.txt_Status.Text = v;
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
                IniConfigHelper.WriteIniData(this.Text.Trim(), "MaxCount", this.txt_MaxNum.Text.Trim(), GateWayService.IniFilePath);
                IniConfigHelper.WriteIniData(this.Text.Trim(), "CycleTime", this.txt_CycleTime.Text.Trim(), GateWayService.IniFilePath);
                IniConfigHelper.WriteIniData(this.Text.Trim(), "CH", this.cbo_RfCh.SelectedIndex.ToString(), GateWayService.IniFilePath);
                if (this.rdb_R.Checked)
                {
                    IniConfigHelper.WriteIniData(this.Text.Trim(), "Mode", "1", GateWayService.IniFilePath);
                }
                else if (this.rdb_W.Checked)
                {
                    IniConfigHelper.WriteIniData(this.Text.Trim(), "Mode", "2", GateWayService.IniFilePath);
                }
                else if (this.rdb_RW.Checked)
                {
                    IniConfigHelper.WriteIniData(this.Text.Trim(), "Mode", "3", GateWayService.IniFilePath);
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
                this.txt_WriteData.Text = IniConfigHelper.ReadIniData(this.Text.Trim(), "WriteData", "", GateWayService.IniFilePath);
                this.txt_MaxNum.Text = IniConfigHelper.ReadIniData(this.Text.Trim(), "MaxCount", "", GateWayService.IniFilePath);
                this.txt_CycleTime.Text = IniConfigHelper.ReadIniData(this.Text.Trim(), "CycleTime", "", GateWayService.IniFilePath);
                this.cbo_RfCh.SelectedIndex = Convert.ToInt32(IniConfigHelper.ReadIniData(this.Text.Trim(), "CH", "", GateWayService.IniFilePath));
                int mode = Convert.ToInt32(IniConfigHelper.ReadIniData(this.Text.Trim(), "Mode", "", GateWayService.IniFilePath));
                switch (mode)
                {
                    case 1:
                        this.rdb_R.Checked = true;
                        break;

                    case 2:
                        this.rdb_W.Checked = true;
                        break;

                    case 3:
                        this.rdb_RW.Checked = true;
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

        private void EnableReader()
        {
            if (!GateWayService.EN.IsConnect())
            {
                AddLog(GateWayService.IsEN ? "Check Connection !" : "检查连接！");
                return;
            }
            switch (this.cbo_RfCh.SelectedIndex)
            {
                case 0:
                    GateWayService.EN.EnableReader(Reader.RF0, true);
                    break;

                case 1:
                    GateWayService.EN.EnableReader(Reader.RF1, true);
                    break;

                case 2:
                    GateWayService.EN.EnableReader(Reader.RF2, true);
                    break;

                case 3:
                    GateWayService.EN.EnableReader(Reader.RF3, true);
                    break;

                default:
                    break;
            }
            Thread.Sleep(100);
        }

        private void txt_Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int num = 0;
                StringBuilder str = new StringBuilder();
                if (int.TryParse(this.txt_Num.Text, out num))
                {
                    if (num > 0)
                    {
                        for (int i = 1; i <= num; i++)
                        {
                            str.Append((i.ToString("X").Length == 1 ? "0" + i.ToString("X").ToUpper() : i.ToString("X").ToUpper()) + " ");
                        }
                        this.txt_WriteData.Text = str.ToString();
                    }
                }
            }
        }
    }
}