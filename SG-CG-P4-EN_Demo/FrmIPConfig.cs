using Sygole.HFReader;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_CG_P4_EN_Demo
{
    public partial class FrmIPConfig : Form
    {
        public FrmIPConfig()
        {
            InitializeComponent();
        }

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

        private void lbl_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbl_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = true;
        }

        private async void btn_Search_Click(object sender, EventArgs e)
        {
            this.dgv_Data.Rows.Clear();
            set_status(GateWayService.IsEN ? "Searching...":"搜索设备中...");
            this.Refresh();
            try
            {
                Task<CommCfg[]> t = new Task<CommCfg[]>(new Func<CommCfg[]>(() =>
                {
                    CommCfg[] res = UdpReader.SearchReader();
                    return res;
                }));
                t.Start();
                CommCfg[] readers = await t;
                if (readers == null || readers.Length == 0)
                {
                    set_status(GateWayService.IsEN ? "Search None Device...":"没有搜索到设备...");
                    return;
                }
                foreach (CommCfg cfg in readers)
                {
                    DeviceData res = new DeviceData(0, cfg.IPAddr, cfg.Mask, cfg.GateWay, Byte2MAC(cfg.MAC, 0, 6));
                    Add_Dgv(res);
                }
                set_status(GateWayService.IsEN ? "Successful of Search...":"成功搜索设备...");
            }
            catch (Exception ex)
            {
                set_status(ex.Message);
            }
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            CommCfg cfg = new CommCfg();
            cfg.IPAddr = this.txt_IP.Text;
            cfg.Mask = this.txt_Mark.Text;
            cfg.GateWay = this.txt_GateWay.Text;
            this.MAC2Byte(this.txt_Mac.Text, cfg.MAC, 0);
            if (UdpReader.SetCommCfg(cfg.MAC, 3002, cfg) == true)
            {
                DeviceData res = new DeviceData(0, cfg.IPAddr, cfg.Mask, cfg.GateWay, Byte2MAC(cfg.MAC, 0, 6));
                Add_Dgv(res);
                set_status(GateWayService.IsEN ? "Setting: successful, Restart takes effect...":"设置参数成功，重启之后生效...");
            }
            else
            {
                set_status(GateWayService.IsEN ? "Setting: failed...":"设置失败...");
            }
        }

        private void dgv_Data_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (this.dgv_Data.RowCount < e.RowIndex) return;
            this.txt_IP.Text = (string)this.dgv_Data[1, e.RowIndex].Value;
            this.txt_Mark.Text = (string)this.dgv_Data[2, e.RowIndex].Value;
            this.txt_GateWay.Text = (string)this.dgv_Data[3, e.RowIndex].Value;
            this.txt_Mac.Text = (string)this.dgv_Data[4, e.RowIndex].Value;
        }

        private void Add_Dgv(DeviceData deviceData)
        {
            bool res = false;
            if (this.dgv_Data.RowCount > 0)
            {
                for (int i = 0; i < this.dgv_Data.RowCount; i++)
                {
                    string mac_tmp = (string)this.dgv_Data[4, i].Value;
                    if (mac_tmp.CompareTo(deviceData.Mac) == 0)
                    {
                        res = true;
                        break;
                    }
                }
            }
            if (!res)
            {
                this.dgv_Data.Rows.Add(this.dgv_Data.Rows.Count + 1, deviceData.IP, deviceData.Mark, deviceData.GateWay, deviceData.Mac);
            }
        }

        private void set_status(string v)
        {
            this.lbl_Status.Text = "Status: " + v;
        }

        public string Byte2MAC(byte[] by, int start, int cnt)
        {
            string str = "";
            for (int i = start; i < start + cnt; i++)
            {
                str += by[i].ToString("X2");
                if (i + 1 < start + cnt) str += "-";
            }
            return str;
        }

        public bool MAC2Byte(string mac, byte[] by, int start)
        {
            string[] items = mac.Trim().Split('-');
            if (items.Length != 6) return false;

            byte[] bmac = new byte[6];
            byte b = 0x00;
            bool err = false;

            for (int i = 0; i < items.Length; i++)
            {
                if (byte.TryParse(items[i], System.Globalization.NumberStyles.HexNumber, null, out b) == true)
                {
                    bmac[i] = b;
                }
                else
                {
                    err = true;
                }
            }

            if (err == false)
            {
                Array.Copy(bmac, 0, by, start, bmac.Length);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class DeviceData
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string Mark { get; set; }
        public string GateWay { get; set; }
        public string Mac { get; set; }
        private byte[] _raw = new byte[1024];

        public DeviceData(int id, string ip, string mark, string gateway, string mac)
        {
            this.ID = id;
            this.IP = ip;
            this.Mark = mark;
            this.GateWay = gateway;
            this.Mac = mac;
        }
    }
}