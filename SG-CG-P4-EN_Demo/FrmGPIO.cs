using DAL;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SG_CG_P4_EN_Demo
{
    public partial class FrmGPIO : Form
    {
        private System.Timers.Timer timer;

        public FrmGPIO()
        {
            InitializeComponent();
            this.Load += FrmGPIO_Load;
        }

        private void FrmGPIO_Load(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer(100);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            this.btn_Out1.Active = GateWayService.GPIN.GPI_1;
            this.btn_Out2.Active = GateWayService.GPIN.GPI_2;
            this.btn_Out3.Active = GateWayService.GPIN.GPI_3;
            this.btn_Out4.Active = GateWayService.GPIN.GPI_4;
        }

        private void FrmGPIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                if (GateWayService.EN.IsConnect())
                {
                    this.led_In1.State = GateWayService.GPIN.GPI_1 ? UILightState.On : UILightState.Off;
                    this.led_In2.State = GateWayService.GPIN.GPI_2 ? UILightState.On : UILightState.Off;
                    this.led_In3.State = GateWayService.GPIN.GPI_3 ? UILightState.On : UILightState.Off;
                    this.led_In4.State = GateWayService.GPIN.GPI_4 ? UILightState.On : UILightState.Off;
                }
            }));
        }

        private void btn_Out1_ValueChanged(object sender, bool value)
        {
            if (GateWayService.EN.IsConnect())
            {
                if (btn_Out1.Active) GateWayService.EN.SetOutBit(new Dictionary<GPOS, bool>() { [GPOS.GPO_1] = true });
                else GateWayService.EN.SetOutBit(new Dictionary<GPOS, bool>() { [GPOS.GPO_1] = false });
            }
            else
            {
                this.txt_Status.Text = GateWayService.IsEN ? "Check Connection !" : "检查连接！";
            }
        }

        private void btn_Out2_ValueChanged(object sender, bool value)
        {
            if (GateWayService.EN.IsConnect())
            {
                if (btn_Out2.Active) GateWayService.EN.SetOutBit(new Dictionary<GPOS, bool>() { [GPOS.GPO_2] = true });
                else GateWayService.EN.SetOutBit(new Dictionary<GPOS, bool>() { [GPOS.GPO_2] = false });
            }
            else
            {
                this.txt_Status.Text = GateWayService.IsEN ? "Check Connection !" : "检查连接！";
            }
        }

        private void btn_Out3_ValueChanged(object sender, bool value)
        {
            if (GateWayService.EN.IsConnect())
            {
                if (btn_Out3.Active) GateWayService.EN.SetOutBit(new Dictionary<GPOS, bool>() { [GPOS.GPO_3] = true });
                else GateWayService.EN.SetOutBit(new Dictionary<GPOS, bool>() { [GPOS.GPO_3] = false });
            }
            else
            {
                this.txt_Status.Text = GateWayService.IsEN ? "Check Connection !" : "检查连接！";
            }
        }

        private void btn_Out4_ValueChanged(object sender, bool value)
        {
            if (GateWayService.EN.IsConnect())
            {
                if (btn_Out4.Active) GateWayService.EN.SetOutBit(new Dictionary<GPOS, bool>() { [GPOS.GPO_4] = true });
                else GateWayService.EN.SetOutBit(new Dictionary<GPOS, bool>() { [GPOS.GPO_4] = false });
            }
            else
            {
                this.txt_Status.Text = GateWayService.IsEN ? "Check Connection !" : "检查连接！";
            }
        }


    }
}