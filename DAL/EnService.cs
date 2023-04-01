using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 实时数据返回委托
    /// </summary>
    /// <param name="ReaderInfos"></param>
    /// <param name="IOInfos"></param>
    public delegate void DeviceMonitor(Dictionary<Reader, ReaderMonitor> ReaderInfos, GPIO IOInfos);

    /// <summary>
    /// EN_GateWay对象
    /// </summary>
    public class EnService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EnService()
        {
            //this.timer = new System.Threading.Timer(CheckConnectStatus, null, 0, 1000);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">Port</param>
        public EnService(string ip, string port)
        {
            //this.timer = new System.Threading.Timer(CheckConnectStatus, null, 0, 1000);
            this.Ip = ip;
            this.Port = port;
        }

        #region Private Member
        private ModTcpHelper ModTcp = null;
        private CancellationTokenSource cts = null;
        //private System.Threading.Timer timer = null;
        private bool IsConnected { get; set; } = false;
        private bool _AutoConnect { get; set; } = false;
        //private static bool _ConnectDispose = false;
        #endregion Private Member

        #region DataCache
        private byte[] ClearCache = new byte[130];
        private byte[] RcvArrayCache = new byte[130];
        private byte GP_Cmd = 0;
        private byte LastByteCache0 = 0;
        private byte LastByteCache1 = 0;
        private byte LastByteCache2 = 0;
        private byte LastByteCache3 = 0;
        private GPIO GPIN = new GPIO();
        private byte CmdCache0 = 0;
        private byte CmdCache1 = 0;
        private byte CmdCache2 = 0;
        private byte CmdCache3 = 0;
        private bool ReaderBusy0 = false;
        private bool ReaderBusy1 = false;
        private bool ReaderBusy2 = false;
        private bool ReaderBusy3 = false;
        private ReaderMonitor reader0 = new ReaderMonitor();
        private ReaderMonitor reader1 = new ReaderMonitor();
        private ReaderMonitor reader2 = new ReaderMonitor();
        private ReaderMonitor reader3 = new ReaderMonitor();
        private Dictionary<Reader, ReaderMonitor> Readers = new Dictionary<Reader, ReaderMonitor>();
        #endregion DataCache

        #region Publish Member

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public string Port { get; set; }

        ///// <summary>
        ///// 自动连接
        ///// </summary>
        //public bool AutoConnectMode { get; set; } = false;

        /// <summary>
        /// 实时数据返回事件
        /// </summary>
        public event DeviceMonitor DeviceMonitor;

        #endregion Publish Member


        #region Publish Method

        /// <summary>
        /// 设备状态
        /// </summary>
        /// <returns>已连接:true/未连接:false</returns>
        public bool IsConnect()
        {
            return this.IsConnected;
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns>成功:true/失败:false</returns>
        public bool Connect()
        {
            //_DisConnect();
            if (ModTcp == null)
            {
                ModTcp = new ModTcpHelper();
            }
            if (ModTcp.Connect(this.Ip, this.Port))
            {
                this.IsConnected = true;
                this.StartPolling();
                return true;
            }
            else
            {
                if (ModTcp != null)
                {
                    ModTcp.Dispose();
                    ModTcp = null;
                }
                this.StopPolling();
                this.IsConnected = false;
                return false;
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            _DisConnect();
            _AutoConnect = false;
        }
        private void _DisConnect()
        {
            this.StopPolling();
            Thread.Sleep(50);
            if (ModTcp != null)
            {
                ModTcp.Dispose();
                ModTcp = null;
            }
            this.ResetStatus();
            this.IsConnected = false;
            
        }

        /// <summary>
        /// 设置GPO输出
        /// </summary>
        /// <param name="GPO">GPO集合(GPO通道号,设置值)</param>
        /// <returns>成功:true/失败:false</returns>
        public bool SetOutBit(Dictionary<GPOS, bool> GPO)
        {
            if (!this.IsConnect())
            {
                return false;
            }
            if (GPO.Count <= 0)
            {
                return false;
            }
            bool result = false;
            foreach (var item in GPO.Keys)
            {
                switch (item)
                {
                    case GPOS.GPO_1:
                        this.GP_Cmd = DataTran.SetBitValue(this.GP_Cmd, 1, GPO[item]);
                        break;

                    case GPOS.GPO_2:
                        this.GP_Cmd = DataTran.SetBitValue(this.GP_Cmd, 2, GPO[item]);
                        break;

                    case GPOS.GPO_3:
                        this.GP_Cmd = DataTran.SetBitValue(this.GP_Cmd, 3, GPO[item]);
                        break;

                    case GPOS.GPO_4:
                        this.GP_Cmd = DataTran.SetBitValue(this.GP_Cmd, 4, GPO[item]);
                        break;

                    default:
                        break;
                }
            }
            result = ModTcp.PreSetMultiByteSwap(0, new byte[2] { this.GP_Cmd, this.CmdCache0 });
            return result;
        }

        /// <summary>
        /// 获取GPI输入
        /// </summary>
        /// <returns>GPO集合(GPO通道号,状态值)</returns>
        public Dictionary<GPIS, bool> GetInPutBit()
        {
            if (!this.IsConnect())
            {
                return null;
            }
            byte[] Value = new byte[2];
            Value = ModTcp.ReadKeepRegSwap(0, 1);
            Dictionary<GPIS, bool> temp = new Dictionary<GPIS, bool>();
            if (Value != null && Value.Length > 0)
            {
                temp.Add(GPIS.GPI_1, DataTran.GetBitValue(Value[0], 1));
                temp.Add(GPIS.GPI_2, DataTran.GetBitValue(Value[0], 2));
                temp.Add(GPIS.GPI_3, DataTran.GetBitValue(Value[0], 3));
                temp.Add(GPIS.GPI_4, DataTran.GetBitValue(Value[0], 4));
                return temp;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 连接/断开
        /// </summary>
        /// <param name="Reader">读写器通道</param>
        /// <param name="Value">设置值</param>
        /// <returns>成功:true/失败:false</returns>
        public bool EnableReader(Reader Reader, bool Value)
        {
            bool result_W = false;
            if (ModTcp != null)
            {
                if (Value)
                {
                    switch (Reader)
                    {
                        case Reader.RF0:
                            this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 1, true);
                            this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 5, true);   //rssi强度检测
                            result_W = ModTcp.PreSetMultiByteSwap(0, new byte[2] { GP_Cmd, this.CmdCache0 });
                            break;

                        case Reader.RF1:
                            this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 1, true);
                            this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 5, true);   //rssi强度检测
                            result_W = ModTcp.PreSetMultiByteSwap(32, new byte[2] { LastByteCache0, this.CmdCache1 });
                            break;

                        case Reader.RF2:
                            this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 1, true);
                            this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 5, true);   //rssi强度检测
                            result_W = ModTcp.PreSetMultiByteSwap(64, new byte[2] { LastByteCache1, this.CmdCache2 });
                            break;

                        case Reader.RF3:
                            this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 1, true);
                            this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 5, true);   //rssi强度检测
                            result_W = ModTcp.PreSetMultiByteSwap(96, new byte[2] { LastByteCache2, this.CmdCache3 });
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    switch (Reader)
                    {
                        case Reader.RF0:
                            this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 1, false);
                            result_W = ModTcp.PreSetMultiByteSwap(0, new byte[2] { GP_Cmd, this.CmdCache0 });
                            break;

                        case Reader.RF1:
                            this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 1, false);
                            result_W = ModTcp.PreSetMultiByteSwap(32, new byte[2] { LastByteCache0, this.CmdCache1 });
                            break;

                        case Reader.RF2:
                            this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 1, false);
                            result_W = ModTcp.PreSetMultiByteSwap(64, new byte[2] { LastByteCache1, this.CmdCache2 });
                            break;

                        case Reader.RF3:
                            this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 1, false);
                            result_W = ModTcp.PreSetMultiByteSwap(96, new byte[2] { LastByteCache2, this.CmdCache3 });
                            break;

                        default:
                            break;
                    }
                }
            }
            return result_W;
        }

        /// <summary>
        /// 读取标签User数据
        /// </summary>
        /// <param name="Reader">读写器通道</param>
        /// <param name="Addr">起始地址</param>
        /// <param name="Count">字节数</param>
        /// <returns>读取结果对象</returns>
        public ReadResult ReadTagUsers(Reader Reader, int Addr, int Count)
        {
            ReadResult result = new ReadResult();

            if (ModTcp == null)
            {
                result.sataus = ExecutSataus.DisConnected;
                return result;
            }

            if (Addr < 0 || Count < 0 || Count > 240)
            {
                result.sataus = ExecutSataus.InParam_Err;
                return result;
            }

            byte AddrL;
            byte AddrH;
            bool res = false;

            byte ErrCodeTemp = 0;

            ListByteArray TagUserTemp = new ListByteArray();
            ExecutSataus satausTemp = new ExecutSataus();

            AddrL = (byte)(Addr & 0x00ff);
            AddrH = (byte)(Addr >> 8);

            res = newReadTagUser(Reader, AddrH, AddrL, (byte)Count, ref ErrCodeTemp, ref TagUserTemp, ref satausTemp);
            result.sataus = satausTemp;
            if (res && satausTemp == ExecutSataus.Cmd_Succeed && TagUserTemp != null)
            {
                result.TagUser.Add(TagUserTemp.ByteArray);
            }
            else
            {
                result.ErrCode = (byte)(ErrCodeTemp > 0 ? ErrCodeTemp : 0);
                return result;
            }

            return result;
        }

        /// <summary>
        /// 写入标签User数据
        /// </summary>
        /// <param name="Reader">读写器通道</param>
        /// <param name="Addr">起始地址</param>
        /// <param name="Count">字节数</param>
        /// <param name="data">写入数据</param>
        /// <returns>写入结果对象</returns>
        public WriteResult WriteTagUsers(Reader Reader, int Addr, int Count, byte[] data)
        {
            WriteResult result = new WriteResult();

            if (ModTcp == null)
            {
                result.sataus = ExecutSataus.DisConnected;
                return result;
            }

            if ((Addr < 0 || Count < 0 || Count > 240) || (data.Length != Count || data == null))
            {
                result.sataus = ExecutSataus.InParam_Err;
                return result;
            }


            byte AddrH;
            byte AddrL;
            byte Num = (byte)Count;
            bool res;
            byte ErrCodeTemp = 0;
            ExecutSataus satausTemp = new ExecutSataus();

            AddrL = (byte)(Addr & 0x00ff);
            AddrH = (byte)(Addr >> 8);

            res = newWriteTagUser(Reader, AddrH, AddrL, Num, data, ref ErrCodeTemp, ref satausTemp);
            result.sataus = satausTemp;
            if (!res)
            {
                result.ErrCode = (byte)(ErrCodeTemp > 0 ? ErrCodeTemp : 0);
                return result;
            }
            return result;
        }

        #endregion Publish Method

        #region Private Method

        /// <summary>
        /// 读取USER（单次）
        /// </summary>
        /// <param name="Reader">读写器通道</param>
        /// <param name="AddrH">读取地址高八位</param>
        /// <param name="AddrL">读取地址低八位</param>
        /// <param name="num">读取字节数</param>
        /// <param name="ErrCode">错误码</param>
        /// <param name="TagUser">USER数据</param>
        /// <param name="sataus">执行状态</param>
        /// <returns>成功;true/失败:false</returns>
        private bool ReadTagUser(Reader Reader, byte AddrH, byte AddrL, byte num, ref byte ErrCode, ref ListByteArray TagUser, ref ExecutSataus sataus)
        {
            switch (Reader)
            {
                case Reader.RF0:
                    if (ReaderBusy0)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy0 = true;
                    break;

                case Reader.RF1:
                    if (ReaderBusy1)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy1 = true;
                    break;

                case Reader.RF2:
                    if (ReaderBusy2)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy2 = true;
                    break;

                case Reader.RF3:
                    if (ReaderBusy3)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy3 = true;
                    break;

                default:
                    break;
            }

            bool done = false;
            bool done1 = false;
            bool err = false;
            int CycleCount = 0;
            int CycleCount1 = 0;
            ListByteArray ReadByte = new ListByteArray();
            bool res = true;

            //Debug.WriteLine("----------------------读取命令触发时间：" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss:FFF"));
            switch (Reader)
            {
                case Reader.RF0:
                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, true);
                    res = ModTcp.PreSetMultiByteSwap(0, new byte[8] { GP_Cmd, CmdCache0, 0, 0x11, AddrH, AddrL, num, 0 });
                    break;

                case Reader.RF1:
                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, true);
                    res = ModTcp.PreSetMultiByteSwap(32, new byte[8] { LastByteCache0, CmdCache1, 0, 0x11, AddrH, AddrL, num, 0 });
                    break;

                case Reader.RF2:
                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, true);
                    res = ModTcp.PreSetMultiByteSwap(64, new byte[8] { LastByteCache1, CmdCache2, 0, 0x11, AddrH, AddrL, num, 0 });
                    break;

                case Reader.RF3:
                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, true);
                    res = ModTcp.PreSetMultiByteSwap(96, new byte[8] { LastByteCache2, CmdCache3, 0, 0x11, AddrH, AddrL, num, 0 });
                    break;

                default:
                    break;
            }
            //Debug.WriteLine("----------------------读取命令触发结束时间：" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss:FFF"));

            //Debug.WriteLine("----------------------读取命令等待完成时间：" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss:FFF"));
            while (!done && res)
            {
                switch (Reader)
                {
                    case Reader.RF0:
                        done = done1 = this.reader0.Done;
                        err = this.reader0.Err;
                        break;

                    case Reader.RF1:
                        done = done1 = this.reader1.Done;
                        err = this.reader1.Err;
                        break;

                    case Reader.RF2:
                        done = done1 = this.reader2.Done;
                        err = this.reader2.Err;
                        break;

                    case Reader.RF3:
                        done = done1 = this.reader3.Done;
                        err = this.reader3.Err;
                        break;

                    default:
                        break;
                }
                CycleCount++;
                if (CycleCount >= 60) break;
                Thread.Sleep(3);
            }
            //Debug.WriteLine("----------------------读取命令等待完成结束时间：" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss:FFF"));

            switch (Reader)
            {
                case Reader.RF0:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 0, 32));
                    break;

                case Reader.RF1:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 32, 32));
                    break;

                case Reader.RF2:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 64, 32));
                    break;

                case Reader.RF3:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 96, 32));
                    break;

                default:
                    break;
            }

            //Debug.WriteLine("----------------------读取命令复位触发时间：" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss:FFF"));
            switch (Reader)
            {
                case Reader.RF0:
                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(0, new byte[2] { GP_Cmd, this.CmdCache0 });
                    break;

                case Reader.RF1:
                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(32, new byte[2] { LastByteCache0, this.CmdCache1 });
                    break;

                case Reader.RF2:
                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(64, new byte[2] { LastByteCache1, this.CmdCache2 });
                    break;

                case Reader.RF3:
                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(96, new byte[2] { LastByteCache2, this.CmdCache3 });
                    break;

                default:
                    break;
            }
            //Debug.WriteLine("----------------------读取命令复位触发结束时间：" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss:FFF"));

            //Debug.WriteLine("----------------------读取命令复位完成等待时间：" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss:FFF"));
            while (done1 && res)
            {
                switch (Reader)
                {
                    case Reader.RF0:
                        done1 = this.reader0.Done;
                        break;

                    case Reader.RF1:
                        done1 = this.reader1.Done;
                        break;

                    case Reader.RF2:
                        done1 = this.reader2.Done;
                        break;

                    case Reader.RF3:
                        done1 = this.reader3.Done;
                        break;

                    default:
                        break;
                }
                CycleCount1++;
                if (CycleCount1 >= 60) break;
                Thread.Sleep(3);
            }
            //Debug.WriteLine("----------------------读取命令复位完成结束时间：" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss:FFF"));
            //Debug.WriteLine("-----------------------------------------------------------------------------------------------");
            if (done && !done1 && !err)
            {
                TagUser.Add(DataTran.GetByteFromArray(ReadByte.ByteArray, 5, ReadByte.ByteArray[4]));
                sataus = ExecutSataus.Cmd_Succeed;
                switch (Reader)
                {
                    case Reader.RF0:
                        this.ReaderBusy0 = false;
                        break;

                    case Reader.RF1:
                        this.ReaderBusy1 = false;
                        break;

                    case Reader.RF2:
                        this.ReaderBusy2 = false;
                        break;

                    case Reader.RF3:
                        this.ReaderBusy3 = false;
                        break;

                    default:
                        break;
                }
                return true;
            }
            else
            {
                if (err)
                {
                    ErrCode = ReadByte.ByteArray[3];
                    sataus = ExecutSataus.Cmd_Fail;
                }
                else
                {
                    sataus = ExecutSataus.Cmd_OverTime;
                }
                switch (Reader)
                {
                    case Reader.RF0:
                        this.ReaderBusy0 = false;
                        break;

                    case Reader.RF1:
                        this.ReaderBusy1 = false;
                        break;

                    case Reader.RF2:
                        this.ReaderBusy2 = false;
                        break;

                    case Reader.RF3:
                        this.ReaderBusy3 = false;
                        break;

                    default:
                        break;
                }
                return false;
            }
        }
        /// <summary>
        /// 读取USER（单次）
        /// </summary>
        /// <param name="Reader">读写器通道</param>
        /// <param name="AddrH">读取地址高八位</param>
        /// <param name="AddrL">读取地址低八位</param>
        /// <param name="num">读取字节数</param>
        /// <param name="ErrCode">错误码</param>
        /// <param name="TagUser">USER数据</param>
        /// <param name="sataus">执行状态</param>
        /// <returns>成功;true/失败:false</returns>
        private bool newReadTagUser(Reader Reader, byte AddrH, byte AddrL, byte num, ref byte ErrCode, ref ListByteArray TagUser, ref ExecutSataus sataus)
        {
            switch (Reader)
            {
                case Reader.RF0:
                    if (ReaderBusy0)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy0 = true;
                    break;

                case Reader.RF1:
                    if (ReaderBusy1)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy1 = true;
                    break;

                case Reader.RF2:
                    if (ReaderBusy2)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy2 = true;
                    break;

                case Reader.RF3:
                    if (ReaderBusy3)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy3 = true;
                    break;

                default:
                    break;
            }

            bool done = false;
            bool done1 = false;
            bool err = false;
            int CycleCount = 0;
            int CycleCount1 = 0;
            int CycleCount2 = 0;

            int cmd_timeout = 100;
            int reset_timeout = 100;
            int toggle_timeout = 100;

            ListByteArray ReadByte = new ListByteArray();
            bool res = true;

            switch (Reader)
            {
                case Reader.RF0:
                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, true);
                    res = ModTcp.PreSetMultiByteSwap(0, new byte[8] { GP_Cmd, CmdCache0, 0, 0x11, AddrH, AddrL, num, 0 });
                    break;

                case Reader.RF1:
                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, true);
                    res = ModTcp.PreSetMultiByteSwap(32, new byte[8] { LastByteCache0, CmdCache1, 0, 0x11, AddrH, AddrL, num, 0 });
                    break;

                case Reader.RF2:
                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, true);
                    res = ModTcp.PreSetMultiByteSwap(64, new byte[8] { LastByteCache1, CmdCache2, 0, 0x11, AddrH, AddrL, num, 0 });
                    break;

                case Reader.RF3:
                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, true);
                    res = ModTcp.PreSetMultiByteSwap(96, new byte[8] { LastByteCache2, CmdCache3, 0, 0x11, AddrH, AddrL, num, 0 });
                    break;

                default:
                    break;
            }

            int remain_len = 0;
            bool pdi_toggle = false;
            bool pdi_toggle_old = false;
            bool pdo_toggle = false;
            while (!done && res)
            {
                switch (Reader)
                {
                    case Reader.RF0:
                        done = done1 = this.reader0.Done;
                        err = this.reader0.Err;
                        remain_len = reader0.UserDataLen;
                        pdi_toggle = reader0.Toggle;
                        pdi_toggle_old = pdi_toggle;
                        break;

                    case Reader.RF1:
                        done = done1 = this.reader1.Done;
                        err = this.reader1.Err;
                        remain_len = reader1.UserDataLen;
                        pdi_toggle = reader1.Toggle;
                        pdi_toggle_old = pdi_toggle;
                        break;

                    case Reader.RF2:
                        done = done1 = this.reader2.Done;
                        err = this.reader2.Err;
                        remain_len = reader2.UserDataLen;
                        pdi_toggle = reader2.Toggle;
                        pdi_toggle_old = pdi_toggle;
                        break;

                    case Reader.RF3:
                        done = done1 = this.reader3.Done;
                        err = this.reader3.Err;
                        remain_len = reader3.UserDataLen;
                        pdi_toggle = reader3.Toggle;
                        pdi_toggle_old = pdi_toggle;
                        break;

                    default:
                        break;
                }
                CycleCount++;
                if (CycleCount >= cmd_timeout) break;
                Thread.Sleep(5);
            }

            switch (Reader)
            {
                case Reader.RF0:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 1, 32));
                    break;

                case Reader.RF1:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 33, 32));
                    break;

                case Reader.RF2:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 65, 32));
                    break;

                case Reader.RF3:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 97, 32));
                    break;

                default:
                    break;
            }

            if (err)
            {
                //return error code
                ErrCode = ReadByte.ByteArray[2];
            }
            else if (CycleCount >= cmd_timeout)
            {
                //do nothing
            }
            else    //succeeded
            {
                //第一次取数据
                if (remain_len <= 28)
                {
                    TagUser.Add(DataTran.GetByteFromArray(ReadByte.ByteArray, 4, remain_len));
                    sataus = ExecutSataus.Cmd_Succeed;
                    switch (Reader)
                    {
                        case Reader.RF0:
                            this.ReaderBusy0 = false;
                            break;

                        case Reader.RF1:
                            this.ReaderBusy1 = false;
                            break;

                        case Reader.RF2:
                            this.ReaderBusy2 = false;
                            break;

                        case Reader.RF3:
                            this.ReaderBusy3 = false;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    TagUser.Add(DataTran.GetByteFromArray(ReadByte.ByteArray, 4, 28));
                    remain_len -= 28;

                    //循环取数据，直到全部数据取出
                    while (remain_len >= 30)
                    {
                        switch (Reader)
                        {
                            case Reader.RF0:
                                //取pdo的toggle当前值
                                pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                                //取反
                                pdo_toggle = !pdo_toggle;
                                if (pdo_toggle == true)
                                {
                                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 2, true);
                                }
                                else
                                {
                                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 2, false);
                                }
                                res = ModTcp.PreSetMultiByteSwap(0, new byte[8] { GP_Cmd, CmdCache0, 0, 0x11, AddrH, AddrL, num, 0 });
                                break;

                            case Reader.RF1:
                                //取pdo的toggle当前值
                                pdo_toggle = ((CmdCache1 >> 1) & 0x01) == 0x01;
                                //取反
                                pdo_toggle = !pdo_toggle;
                                if (pdo_toggle == true)
                                {
                                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 2, true);
                                }
                                else
                                {
                                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 2, false);
                                }
                                res = ModTcp.PreSetMultiByteSwap(32, new byte[8] { LastByteCache0, CmdCache1, 0, 0x11, AddrH, AddrL, num, 0 });
                                break;

                            case Reader.RF2:
                                //取pdo的toggle当前值
                                pdo_toggle = ((CmdCache2 >> 1) & 0x01) == 0x01;
                                //取反
                                pdo_toggle = !pdo_toggle;
                                if (pdo_toggle == true)
                                {
                                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 2, true);
                                }
                                else
                                {
                                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 2, false);
                                }
                                res = ModTcp.PreSetMultiByteSwap(64, new byte[8] { LastByteCache1, CmdCache2, 0, 0x11, AddrH, AddrL, num, 0 });
                                break;

                            case Reader.RF3:
                                //取pdo的toggle当前值
                                pdo_toggle = ((CmdCache3 >> 1) & 0x01) == 0x01;
                                //取反
                                pdo_toggle = !pdo_toggle;
                                if (pdo_toggle == true)
                                {
                                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 2, true);
                                }
                                else
                                {
                                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 2, false);
                                }
                                res = ModTcp.PreSetMultiByteSwap(96, new byte[8] { LastByteCache2, CmdCache3, 0, 0x11, AddrH, AddrL, num, 0 });
                                break;

                            default:
                                break;
                        }

                        CycleCount2 = 0;
                        while (true)
                        {
                            switch (Reader)
                            {
                                case Reader.RF0:
                                    pdi_toggle = reader0.Toggle;
                                    break;

                                case Reader.RF1:
                                    pdi_toggle = reader1.Toggle;
                                    break;

                                case Reader.RF2:
                                    pdi_toggle = reader2.Toggle;
                                    break;

                                case Reader.RF3:
                                    pdi_toggle = reader3.Toggle;
                                    break;

                                default:
                                    break;
                            }
                            if (pdi_toggle_old != pdi_toggle)
                            {
                                pdi_toggle_old = pdi_toggle;
                                remain_len -= 30;

                                ReadByte.Clear();
                                switch (Reader)
                                {
                                    case Reader.RF0:
                                        ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 1, 32));
                                        break;

                                    case Reader.RF1:
                                        ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 33, 32));
                                        break;

                                    case Reader.RF2:
                                        ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 65, 32));
                                        break;

                                    case Reader.RF3:
                                        ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 97, 32));
                                        break;

                                    default:
                                        break;
                                }

                                TagUser.Add(DataTran.GetByteFromArray(ReadByte.ByteArray, 2, 30));

                                break;
                            }
                            CycleCount2++;
                            if (CycleCount2 >= toggle_timeout) break;
                            Thread.Sleep(5);
                        }
                    }
                    //最后一包数据
                    switch (Reader)
                    {
                        case Reader.RF0:
                            //取pdo的toggle当前值
                            pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                            //取反
                            pdo_toggle = !pdo_toggle;
                            if (pdo_toggle == true)
                            {
                                this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 2, true);
                            }
                            else
                            {
                                this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 2, false);
                            }
                            res = ModTcp.PreSetMultiByteSwap(0, new byte[8] { GP_Cmd, CmdCache0, 0, 0x11, AddrH, AddrL, num, 0 });
                            break;

                        case Reader.RF1:
                            //取pdo的toggle当前值
                            pdo_toggle = ((CmdCache1 >> 1) & 0x01) == 0x01;
                            //取反
                            pdo_toggle = !pdo_toggle;
                            if (pdo_toggle == true)
                            {
                                this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 2, true);
                            }
                            else
                            {
                                this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 2, false);
                            }
                            res = ModTcp.PreSetMultiByteSwap(32, new byte[8] { LastByteCache0, CmdCache1, 0, 0x11, AddrH, AddrL, num, 0 });
                            break;

                        case Reader.RF2:
                            //取pdo的toggle当前值
                            pdo_toggle = ((CmdCache2 >> 1) & 0x01) == 0x01;
                            //取反
                            pdo_toggle = !pdo_toggle;
                            if (pdo_toggle == true)
                            {
                                this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 2, true);
                            }
                            else
                            {
                                this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 2, false);
                            }
                            res = ModTcp.PreSetMultiByteSwap(64, new byte[8] { LastByteCache1, CmdCache2, 0, 0x11, AddrH, AddrL, num, 0 });
                            break;

                        case Reader.RF3:
                            //取pdo的toggle当前值
                            pdo_toggle = ((CmdCache3 >> 1) & 0x01) == 0x01;
                            //取反
                            pdo_toggle = !pdo_toggle;
                            if (pdo_toggle == true)
                            {
                                this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 2, true);
                            }
                            else
                            {
                                this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 2, false);
                            }
                            res = ModTcp.PreSetMultiByteSwap(96, new byte[8] { LastByteCache2, CmdCache3, 0, 0x11, AddrH, AddrL, num, 0 });
                            break;

                        default:
                            break;
                    }
                    CycleCount2 = 0;
                    while (true)
                    {
                        switch (Reader)
                        {
                            case Reader.RF0:
                                pdi_toggle = reader0.Toggle;
                                break;

                            case Reader.RF1:
                                pdi_toggle = reader1.Toggle;
                                break;

                            case Reader.RF2:
                                pdi_toggle = reader2.Toggle;
                                break;

                            case Reader.RF3:
                                pdi_toggle = reader3.Toggle;
                                break;

                            default:
                                break;
                        }
                        if (pdi_toggle_old != pdi_toggle)
                        {
                            ReadByte.Clear();
                            switch (Reader)
                            {
                                case Reader.RF0:
                                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 1, 32));
                                    break;

                                case Reader.RF1:
                                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 33, 32));
                                    break;

                                case Reader.RF2:
                                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 65, 32));
                                    break;

                                case Reader.RF3:
                                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 97, 32));
                                    break;

                                default:
                                    break;
                            }

                            TagUser.Add(DataTran.GetByteFromArray(ReadByte.ByteArray, 2, remain_len));
                            break;
                        }
                        CycleCount2++;
                        if (CycleCount2 >= toggle_timeout) break;
                        Thread.Sleep(5);
                    }
                }


            }

            //指令复位（reset Newjob）
            switch (Reader)
            {
                case Reader.RF0:
                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(0, new byte[2] { GP_Cmd, this.CmdCache0 });
                    break;

                case Reader.RF1:
                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(32, new byte[2] { LastByteCache0, this.CmdCache1 });
                    break;

                case Reader.RF2:
                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(64, new byte[2] { LastByteCache1, this.CmdCache2 });
                    break;

                case Reader.RF3:
                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(96, new byte[2] { LastByteCache2, this.CmdCache3 });
                    break;

                default:
                    break;
            }

            while (done1 && res)
            {
                switch (Reader)
                {
                    case Reader.RF0:
                        done1 = this.reader0.Done;
                        break;

                    case Reader.RF1:
                        done1 = this.reader1.Done;
                        break;

                    case Reader.RF2:
                        done1 = this.reader2.Done;
                        break;

                    case Reader.RF3:
                        done1 = this.reader3.Done;
                        break;

                    default:
                        break;
                }
                CycleCount1++;
                if (CycleCount1 >= reset_timeout) break;
                Thread.Sleep(5);
            }

            switch (Reader)
            {
                case Reader.RF0:
                    this.ReaderBusy0 = false;
                    break;

                case Reader.RF1:
                    this.ReaderBusy1 = false;
                    break;

                case Reader.RF2:
                    this.ReaderBusy2 = false;
                    break;

                case Reader.RF3:
                    this.ReaderBusy3 = false;
                    break;

                default:
                    break;
            }

            if (err)
            {
                sataus = ExecutSataus.Cmd_Fail;
                return false;
            }
            else if (done && !done1)
            {
                sataus = ExecutSataus.Cmd_Succeed;
                return true;
            }
            else if (CycleCount2 >= toggle_timeout || CycleCount1 >= reset_timeout || CycleCount >= cmd_timeout)
            {
                sataus = ExecutSataus.Cmd_OverTime;
                return false;
            }
            else
            {
                sataus = ExecutSataus.Cmd_OverTime;
                return false;
            }
        }
        /// <summary>
        /// 写入USER（单次）
        /// </summary>
        /// <param name="Reader">读写器通道</param>
        /// <param name="AddrH">写入地址高八位</param>
        /// <param name="AddrL">写入地址低八位</param>
        /// <param name="num">写入字节数</param>
        /// <param name="data">写入数据</param>
        /// <param name="ErrCode">错误码</param>
        /// <param name="sataus">执行状态</param>
        /// <returns>成功;true/失败:false</returns>
        private bool WriteTagUser(Reader Reader, byte AddrH, byte AddrL, byte num, byte[] data, ref byte ErrCode, ref ExecutSataus sataus)
        {
            bool done = false;
            bool err = false;
            bool done1 = false;
            int CycleCount = 0;
            int CycleCount1 = 0;
            ListByteArray WriteByte = new ListByteArray();
            ListByteArray ReadByte = new ListByteArray();
            bool res = true;

            switch (Reader)
            {
                case Reader.RF0:
                    if (ReaderBusy0)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy0 = true;
                    break;

                case Reader.RF1:
                    if (ReaderBusy1)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy1 = true;
                    break;

                case Reader.RF2:
                    if (ReaderBusy2)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy2 = true;
                    break;

                case Reader.RF3:
                    if (ReaderBusy3)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy3 = true;
                    break;

                default:
                    break;
            }

            switch (Reader)
            {
                case Reader.RF0:
                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, true);
                    WriteByte.Add(new byte[] { this.GP_Cmd, this.CmdCache0, 0, 0x12, AddrH, AddrL, num });
                    break;

                case Reader.RF1:
                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, true);
                    WriteByte.Add(new byte[] { 0, this.CmdCache1, 0, 0x12, AddrH, AddrL, num });
                    break;

                case Reader.RF2:
                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, true);
                    WriteByte.Add(new byte[] { 0, this.CmdCache2, 0, 0x12, AddrH, AddrL, num });
                    break;

                case Reader.RF3:
                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, true);
                    WriteByte.Add(new byte[] { 0, this.CmdCache3, 0, 0x12, AddrH, AddrL, num });
                    break;

                default:
                    break;
            }

            WriteByte.Add(data);
            if (WriteByte.ByteArray.Length % 2 != 0)
            {
                WriteByte.Add(0);
            }

            switch (Reader)
            {
                case Reader.RF0:
                    res = ModTcp.PreSetMultiByteSwap(0, WriteByte.ByteArray);
                    break;

                case Reader.RF1:
                    res = ModTcp.PreSetMultiByteSwap(32, WriteByte.ByteArray);
                    break;

                case Reader.RF2:
                    res = ModTcp.PreSetMultiByteSwap(64, WriteByte.ByteArray);
                    break;

                case Reader.RF3:
                    res = ModTcp.PreSetMultiByteSwap(96, WriteByte.ByteArray);
                    break;

                default:
                    break;
            }

            while (!done && res)
            {
                switch (Reader)
                {
                    case Reader.RF0:
                        done = done1 = this.reader0.Done;
                        err = this.reader0.Err;
                        break;

                    case Reader.RF1:
                        done = done1 = this.reader1.Done;
                        err = this.reader1.Err;
                        break;

                    case Reader.RF2:
                        done = done1 = this.reader2.Done;
                        err = this.reader2.Err;
                        break;

                    case Reader.RF3:
                        done = done1 = this.reader3.Done;
                        err = this.reader3.Err;
                        break;

                    default:
                        break;
                }
                CycleCount++;
                if (CycleCount >= 60) break;
                Thread.Sleep(3);
            }

            switch (Reader)
            {
                case Reader.RF0:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 0, 6));
                    break;

                case Reader.RF1:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 32, 6));
                    break;

                case Reader.RF2:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 64, 6));
                    break;

                case Reader.RF3:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 96, 6));
                    break;

                default:
                    break;
            }

            switch (Reader)
            {
                case Reader.RF0:
                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(0, new byte[2] { GP_Cmd, this.CmdCache0 });
                    break;

                case Reader.RF1:
                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(32, new byte[2] { LastByteCache0, this.CmdCache1 });
                    break;

                case Reader.RF2:
                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(64, new byte[2] { LastByteCache1, this.CmdCache2 });
                    break;

                case Reader.RF3:
                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(96, new byte[2] { LastByteCache2, this.CmdCache3 });
                    break;

                default:
                    break;
            }

            while (done1 && res)
            {
                switch (Reader)
                {
                    case Reader.RF0:
                        done1 = this.reader0.Done;
                        break;

                    case Reader.RF1:
                        done1 = this.reader1.Done;
                        break;

                    case Reader.RF2:
                        done1 = this.reader2.Done;
                        break;

                    case Reader.RF3:
                        done1 = this.reader3.Done;
                        break;

                    default:
                        break;
                }
                CycleCount1++;
                if (CycleCount1 >= 60) break;
                Thread.Sleep(3);
            }

            if (done && !done1 && !err)
            {
                sataus = ExecutSataus.Cmd_Succeed;
                switch (Reader)
                {
                    case Reader.RF0:
                        this.ReaderBusy0 = false;
                        break;

                    case Reader.RF1:
                        this.ReaderBusy1 = false;
                        break;

                    case Reader.RF2:
                        this.ReaderBusy2 = false;
                        break;

                    case Reader.RF3:
                        this.ReaderBusy3 = false;
                        break;

                    default:
                        break;
                }
                return true;
            }
            else
            {
                if (err)
                {
                    ErrCode = ReadByte.ByteArray[3];
                    sataus = ExecutSataus.Cmd_Fail;
                }
                else
                {
                    sataus = ExecutSataus.Cmd_OverTime;
                }
                switch (Reader)
                {
                    case Reader.RF0:
                        this.ReaderBusy0 = false;
                        break;

                    case Reader.RF1:
                        this.ReaderBusy1 = false;
                        break;

                    case Reader.RF2:
                        this.ReaderBusy2 = false;
                        break;

                    case Reader.RF3:
                        this.ReaderBusy3 = false;
                        break;

                    default:
                        break;
                }
                return false;
            }
        }

        /// <summary>
        /// 写入USER（单次）
        /// </summary>
        /// <param name="Reader">读写器通道</param>
        /// <param name="AddrH">写入地址高八位</param>
        /// <param name="AddrL">写入地址低八位</param>
        /// <param name="num">写入字节数</param>
        /// <param name="data">写入数据</param>
        /// <param name="ErrCode">错误码</param>
        /// <param name="sataus">执行状态</param>
        /// <returns>成功;true/失败:false</returns>
        private bool newWriteTagUser(Reader Reader, byte AddrH, byte AddrL, byte num, byte[] data, ref byte ErrCode, ref ExecutSataus sataus)
        {
            bool done = false;
            bool err = false;
            bool done1 = false;
            int CycleCount = 0;
            int CycleCount1 = 0;
            int CycleCount2 = 0;
            int cmd_timeout = 100;
            int reset_timeout = 100;
            int toggle_timeout = 100;

            ListByteArray WriteByte = new ListByteArray();
            ListByteArray ReadByte = new ListByteArray();
            bool res = true;

            bool pdi_toggle = false;
            bool pdi_toggle_old = false;
            bool pdo_toggle = false;

            switch (Reader)
            {
                case Reader.RF0:
                    if (ReaderBusy0)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy0 = true;
                    break;

                case Reader.RF1:
                    if (ReaderBusy1)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy1 = true;
                    break;

                case Reader.RF2:
                    if (ReaderBusy2)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy2 = true;
                    break;

                case Reader.RF3:
                    if (ReaderBusy3)
                    {
                        sataus = ExecutSataus.Cmd_InExecut;
                        return false;
                    }
                    ReaderBusy3 = true;
                    break;

                default:
                    break;
            }

            int remain_len = num;
            int index = 0;
            byte[] tmp_datas;

            //判断长度
            if (remain_len <= 26)   //一包写完
            {
                WriteByte.Clear();
                switch (Reader)
                {
                    case Reader.RF0:
                        this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, true);
                        WriteByte.Add(new byte[] { this.GP_Cmd, this.CmdCache0, 0, 0x12, AddrH, AddrL, num });
                        WriteByte.Add(data);
                        if (num == 26)
                        {
                            WriteByte.Add(CmdCache1);
                        }
                        break;

                    case Reader.RF1:
                        this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache0, this.CmdCache1, 0, 0x12, AddrH, AddrL, num });
                        WriteByte.Add(data);
                        if (num == 26)
                        {
                            WriteByte.Add(CmdCache2);
                        }
                        break;

                    case Reader.RF2:
                        this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache1, this.CmdCache2, 0, 0x12, AddrH, AddrL, num });
                        WriteByte.Add(data);
                        if (num == 26)
                        {
                            WriteByte.Add(CmdCache3);
                        }
                        break;

                    case Reader.RF3:
                        this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache2, this.CmdCache3, 0, 0x12, AddrH, AddrL, num });
                        WriteByte.Add(data);
                        if (num == 26)
                        {
                            WriteByte.Add(0);
                        }
                        break;

                    default:
                        break;
                }
                if (WriteByte.ByteArray.Length % 2 != 0)
                {
                    WriteByte.Add(0);
                }

                switch (Reader)
                {
                    case Reader.RF0:
                        res = ModTcp.PreSetMultiByteSwap(0, WriteByte.ByteArray);
                        break;

                    case Reader.RF1:
                        res = ModTcp.PreSetMultiByteSwap(32, WriteByte.ByteArray);
                        break;

                    case Reader.RF2:
                        res = ModTcp.PreSetMultiByteSwap(64, WriteByte.ByteArray);
                        break;

                    case Reader.RF3:
                        res = ModTcp.PreSetMultiByteSwap(96, WriteByte.ByteArray);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                //记录初始的toggle状态
                switch (Reader)
                {
                    case Reader.RF0:
                        pdi_toggle = reader0.Toggle;
                        break;

                    case Reader.RF1:
                        pdi_toggle = reader1.Toggle;
                        break;

                    case Reader.RF2:
                        pdi_toggle = reader2.Toggle;
                        break;

                    case Reader.RF3:
                        pdi_toggle = reader3.Toggle;
                        break;

                    default:
                        break;
                }
                pdi_toggle_old = pdi_toggle;

                
                //第一包数据
                WriteByte.Clear();
                tmp_datas = DataTran.GetByteFromArray(data, 0, 26);
                switch (Reader)
                {
                    case Reader.RF0:
                        this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, true);
                        WriteByte.Add(new byte[] { this.GP_Cmd, this.CmdCache0, 0, 0x12, AddrH, AddrL, num });
                        WriteByte.Add(tmp_datas);
                        WriteByte.Add(CmdCache1);
                        break;

                    case Reader.RF1:
                        this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache0, this.CmdCache1, 0, 0x12, AddrH, AddrL, num });
                        WriteByte.Add(tmp_datas);
                        WriteByte.Add(CmdCache2);
                        break;

                    case Reader.RF2:
                        this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache1, this.CmdCache2, 0, 0x12, AddrH, AddrL, num });
                        WriteByte.Add(tmp_datas);
                        WriteByte.Add(CmdCache3);
                        break;

                    case Reader.RF3:
                        this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache2, this.CmdCache3, 0, 0x12, AddrH, AddrL, num });
                        WriteByte.Add(tmp_datas);
                        WriteByte.Add(0);
                        break;

                    default:
                        break;
                }
                if (WriteByte.ByteArray.Length % 2 != 0)
                {
                    WriteByte.Add(0);
                }

                switch (Reader)
                {
                    case Reader.RF0:
                        res = ModTcp.PreSetMultiByteSwap(0, WriteByte.ByteArray);
                        break;

                    case Reader.RF1:
                        res = ModTcp.PreSetMultiByteSwap(32, WriteByte.ByteArray);
                        break;

                    case Reader.RF2:
                        res = ModTcp.PreSetMultiByteSwap(64, WriteByte.ByteArray);
                        break;

                    case Reader.RF3:
                        res = ModTcp.PreSetMultiByteSwap(96, WriteByte.ByteArray);
                        break;

                    default:
                        break;
                }
                //等待翻转
                CycleCount2 = 0;
                while (true)
                {
                    switch (Reader)
                    {
                        case Reader.RF0:
                            pdi_toggle = reader0.Toggle;
                            break;

                        case Reader.RF1:
                            pdi_toggle = reader1.Toggle;
                            break;

                        case Reader.RF2:
                            pdi_toggle = reader2.Toggle;
                            break;

                        case Reader.RF3:
                            pdi_toggle = reader3.Toggle;
                            break;

                        default:
                            break;
                    }
                    if (pdi_toggle_old != pdi_toggle)
                    {
                        pdi_toggle_old = pdi_toggle;
                        remain_len -= 26;
                        index += 26;
                        ReadByte.Clear();
                        switch (Reader)
                        {
                            case Reader.RF0:
                                ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 1, 32));
                                break;

                            case Reader.RF1:
                                ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 33, 32));
                                break;

                            case Reader.RF2:
                                ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 65, 32));
                                break;

                            case Reader.RF3:
                                ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 97, 32));
                                break;

                            default:
                                break;
                        }
                        break;
                    }
                    CycleCount2++;
                    if (CycleCount2 >= toggle_timeout) break;
                    Thread.Sleep(5);
                }

                //循环填充数据
                while (remain_len > 30)
                {
                    WriteByte.Clear();
                    tmp_datas = DataTran.GetByteFromArray(data, index, 30);
                    switch (Reader)
                    {
                        case Reader.RF0:
                            //取pdo的toggle当前值
                            pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                            //取反
                            pdo_toggle = !pdo_toggle;
                            if (pdo_toggle == true)
                            {
                                this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 2, true);
                            }
                            else
                            {
                                this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 2, false);
                            }
                            this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, true);
                            WriteByte.Add(new byte[] { this.GP_Cmd, this.CmdCache0, 0 });
                            WriteByte.Add(tmp_datas);
                            WriteByte.Add(CmdCache1);
                            break;

                        case Reader.RF1:
                            //取pdo的toggle当前值
                            pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                            //取反
                            pdo_toggle = !pdo_toggle;
                            if (pdo_toggle == true)
                            {
                                this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 2, true);
                            }
                            else
                            {
                                this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 2, false);
                            }
                            this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, true);
                            WriteByte.Add(new byte[] { LastByteCache0, this.CmdCache1, 0 });
                            WriteByte.Add(tmp_datas);
                            WriteByte.Add(CmdCache2);
                            break;

                        case Reader.RF2:
                            //取pdo的toggle当前值
                            pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                            //取反
                            pdo_toggle = !pdo_toggle;
                            if (pdo_toggle == true)
                            {
                                this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 2, true);
                            }
                            else
                            {
                                this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 2, false);
                            }
                            this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, true);
                            WriteByte.Add(new byte[] { LastByteCache1, this.CmdCache2, 0 });
                            WriteByte.Add(tmp_datas);
                            WriteByte.Add(CmdCache3);
                            break;

                        case Reader.RF3:
                            //取pdo的toggle当前值
                            pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                            //取反
                            pdo_toggle = !pdo_toggle;
                            if (pdo_toggle == true)
                            {
                                this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 2, true);
                            }
                            else
                            {
                                this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 2, false);
                            }
                            this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, true);
                            WriteByte.Add(new byte[] { LastByteCache2, this.CmdCache3, 0 });
                            WriteByte.Add(tmp_datas);
                            WriteByte.Add(0);
                            break;

                        default:
                            break;
                    }
                    if (WriteByte.ByteArray.Length % 2 != 0)
                    {
                        WriteByte.Add(0);
                    }

                    switch (Reader)
                    {
                        case Reader.RF0:
                            res = ModTcp.PreSetMultiByteSwap(0, WriteByte.ByteArray);
                            break;

                        case Reader.RF1:
                            res = ModTcp.PreSetMultiByteSwap(32, WriteByte.ByteArray);
                            break;

                        case Reader.RF2:
                            res = ModTcp.PreSetMultiByteSwap(64, WriteByte.ByteArray);
                            break;

                        case Reader.RF3:
                            res = ModTcp.PreSetMultiByteSwap(96, WriteByte.ByteArray);
                            break;

                        default:
                            break;
                    }
                    //等待翻转
                    CycleCount2 = 0;
                    while (true)
                    {
                        switch (Reader)
                        {
                            case Reader.RF0:
                                pdi_toggle = reader0.Toggle;
                                break;

                            case Reader.RF1:
                                pdi_toggle = reader1.Toggle;
                                break;

                            case Reader.RF2:
                                pdi_toggle = reader2.Toggle;
                                break;

                            case Reader.RF3:
                                pdi_toggle = reader3.Toggle;
                                break;

                            default:
                                break;
                        }
                        if (pdi_toggle_old != pdi_toggle)
                        {
                            pdi_toggle_old = pdi_toggle;
                            remain_len -= 30;
                            index += 30;
                            ReadByte.Clear();
                            switch (Reader)
                            {
                                case Reader.RF0:
                                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 1, 32));
                                    break;

                                case Reader.RF1:
                                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 33, 32));
                                    break;

                                case Reader.RF2:
                                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 65, 32));
                                    break;

                                case Reader.RF3:
                                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 97, 32));
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }
                        CycleCount2++;
                        if (CycleCount2 >= toggle_timeout) break;
                        Thread.Sleep(5);
                    }
                }

                //最后一包数据
                WriteByte.Clear();
                tmp_datas = DataTran.GetByteFromArray(data, index, remain_len);
                switch (Reader)
                {
                    case Reader.RF0:
                        //取pdo的toggle当前值
                        pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                        //取反
                        pdo_toggle = !pdo_toggle;
                        if (pdo_toggle == true)
                        {
                            this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 2, true);
                        }
                        else
                        {
                            this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 2, false);
                        }
                        this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, true);
                        WriteByte.Add(new byte[] { this.GP_Cmd, this.CmdCache0, 0 });
                        WriteByte.Add(tmp_datas);
                        WriteByte.Add(CmdCache1);
                        break;

                    case Reader.RF1:
                        //取pdo的toggle当前值
                        pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                        //取反
                        pdo_toggle = !pdo_toggle;
                        if (pdo_toggle == true)
                        {
                            this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 2, true);
                        }
                        else
                        {
                            this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 2, false);
                        }
                        this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache0, this.CmdCache1, 0 });
                        WriteByte.Add(tmp_datas);
                        WriteByte.Add(CmdCache2);
                        break;

                    case Reader.RF2:
                        //取pdo的toggle当前值
                        pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                        //取反
                        pdo_toggle = !pdo_toggle;
                        if (pdo_toggle == true)
                        {
                            this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 2, true);
                        }
                        else
                        {
                            this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 2, false);
                        }
                        this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache1, this.CmdCache2, 0 });
                        WriteByte.Add(tmp_datas);
                        WriteByte.Add(CmdCache3);
                        break;

                    case Reader.RF3:
                        //取pdo的toggle当前值
                        pdo_toggle = ((CmdCache0 >> 1) & 0x01) == 0x01;
                        //取反
                        pdo_toggle = !pdo_toggle;
                        if (pdo_toggle == true)
                        {
                            this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 2, true);
                        }
                        else
                        {
                            this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 2, false);
                        }
                        this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, true);
                        WriteByte.Add(new byte[] { LastByteCache2, this.CmdCache3, 0 });
                        WriteByte.Add(tmp_datas);
                        WriteByte.Add(0);
                        break;

                    default:
                        break;
                }
                if (WriteByte.ByteArray.Length % 2 != 0)
                {
                    WriteByte.Add(0);
                }

                switch (Reader)
                {
                    case Reader.RF0:
                        res = ModTcp.PreSetMultiByteSwap(0, WriteByte.ByteArray);
                        break;

                    case Reader.RF1:
                        res = ModTcp.PreSetMultiByteSwap(32, WriteByte.ByteArray);
                        break;

                    case Reader.RF2:
                        res = ModTcp.PreSetMultiByteSwap(64, WriteByte.ByteArray);
                        break;

                    case Reader.RF3:
                        res = ModTcp.PreSetMultiByteSwap(96, WriteByte.ByteArray);
                        break;

                    default:
                        break;
                }
            }


            while (!done && res)
            {
                switch (Reader)
                {
                    case Reader.RF0:
                        done = done1 = this.reader0.Done;
                        err = this.reader0.Err;
                        break;

                    case Reader.RF1:
                        done = done1 = this.reader1.Done;
                        err = this.reader1.Err;
                        break;

                    case Reader.RF2:
                        done = done1 = this.reader2.Done;
                        err = this.reader2.Err;
                        break;

                    case Reader.RF3:
                        done = done1 = this.reader3.Done;
                        err = this.reader3.Err;
                        break;

                    default:
                        break;
                }
                CycleCount++;
                if (CycleCount >= cmd_timeout) break;
                Thread.Sleep(5);
            }

            switch (Reader)
            {
                case Reader.RF0:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 0, 6));
                    break;

                case Reader.RF1:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 32, 6));
                    break;

                case Reader.RF2:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 64, 6));
                    break;

                case Reader.RF3:
                    ReadByte.Add(DataTran.GetByteFromArray(RcvArrayCache, 96, 6));
                    break;

                default:
                    break;
            }

            switch (Reader)
            {
                case Reader.RF0:
                    this.CmdCache0 = DataTran.SetBitValue(this.CmdCache0, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(0, new byte[2] { GP_Cmd, this.CmdCache0 });
                    break;

                case Reader.RF1:
                    this.CmdCache1 = DataTran.SetBitValue(this.CmdCache1, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(32, new byte[2] { LastByteCache0, this.CmdCache1 });
                    break;

                case Reader.RF2:
                    this.CmdCache2 = DataTran.SetBitValue(this.CmdCache2, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(64, new byte[2] { LastByteCache1, this.CmdCache2 });
                    break;

                case Reader.RF3:
                    this.CmdCache3 = DataTran.SetBitValue(this.CmdCache3, 3, false);
                    res = ModTcp.PreSetMultiByteSwap(96, new byte[2] { LastByteCache2, this.CmdCache3 });
                    break;

                default:
                    break;
            }

            while (done1 && res)
            {
                switch (Reader)
                {
                    case Reader.RF0:
                        done1 = this.reader0.Done;
                        break;

                    case Reader.RF1:
                        done1 = this.reader1.Done;
                        break;

                    case Reader.RF2:
                        done1 = this.reader2.Done;
                        break;

                    case Reader.RF3:
                        done1 = this.reader3.Done;
                        break;

                    default:
                        break;
                }
                CycleCount1++;
                if (CycleCount1 >= reset_timeout) break;
                Thread.Sleep(5);
            }

            if (done && !done1 && !err)
            {
                sataus = ExecutSataus.Cmd_Succeed;
                switch (Reader)
                {
                    case Reader.RF0:
                        this.ReaderBusy0 = false;
                        break;

                    case Reader.RF1:
                        this.ReaderBusy1 = false;
                        break;

                    case Reader.RF2:
                        this.ReaderBusy2 = false;
                        break;

                    case Reader.RF3:
                        this.ReaderBusy3 = false;
                        break;

                    default:
                        break;
                }
                return true;
            }
            else
            {
                if (err)
                {
                    ErrCode = ReadByte.ByteArray[3];
                    sataus = ExecutSataus.Cmd_Fail;
                }
                else
                {
                    sataus = ExecutSataus.Cmd_OverTime;
                }
                switch (Reader)
                {
                    case Reader.RF0:
                        this.ReaderBusy0 = false;
                        break;

                    case Reader.RF1:
                        this.ReaderBusy1 = false;
                        break;

                    case Reader.RF2:
                        this.ReaderBusy2 = false;
                        break;

                    case Reader.RF3:
                        this.ReaderBusy3 = false;
                        break;

                    default:
                        break;
                }
                return false;
            }
        }

        /// <summary>
        /// 复位信号标志
        /// </summary>
        private void ResetStatus()
        {
            //this.GP_Cmd = 0;
            //this.CmdCache0 = 0;
            //this.CmdCache1 = 0;
            //this.CmdCache2 = 0;
            //this.CmdCache3 = 0;
            this.ReaderBusy0 = false;
            this.ReaderBusy1 = false;
            this.ReaderBusy2 = false;
            this.ReaderBusy3 = false;
        }

        private int disconnect_count = 0;
        /// <summary>
        /// 轮询线程
        /// </summary>
        private void Monitoring()
        {
            while (!cts.IsCancellationRequested)
            {
                byte[] tmp_recv;
                if (IsConnected)
                {
                    tmp_recv = DataTran.GetSwapByteArray(ModTcp.ReadKeepReg(0, 65), DataFormat.CDAB);
                    if (tmp_recv != null)
                    {
                        disconnect_count = 0;
                        this.RcvArrayCache = tmp_recv;
                        if (this.RcvArrayCache.Length > 0 && this.RcvArrayCache.Length == 130)
                        {
                            this.DataReport(this.RcvArrayCache);
                        }
                    }
                    else
                    {
                        disconnect_count++;
                        if (disconnect_count >= 10)    //连续10次没有收到数据
                        {
                            _DisConnect();
                            disconnect_count = 0;
                        }
                    }
                }
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// 数据上报事件方法
        /// </summary>
        /// <param name="data">传入数据</param>
        private void DataReport(byte[] data)
        {
            #region CPIO

            this.GPIN.GPI_1 = DataTran.GetBitValue(data[0], 1);
            this.GPIN.GPI_2 = DataTran.GetBitValue(data[0], 2);
            this.GPIN.GPI_3 = DataTran.GetBitValue(data[0], 3);
            this.GPIN.GPI_4 = DataTran.GetBitValue(data[0], 4);

            #endregion CPIO

            #region RF

            this.reader0.Conn = DataTran.GetBitValue(data[1], 1);
            this.reader0.TagPresent = DataTran.GetBitValue(data[1], 2);
            this.reader0.Busy = DataTran.GetBitValue(data[1], 3);
            this.reader0.Done = DataTran.GetBitValue(data[1], 4);
            this.reader0.Err = DataTran.GetBitValue(data[1], 5);
            this.reader0.Toggle = DataTran.GetBitValue(data[1], 6);
            this.reader0.UserDataLen = data[4];
            if (DataTran.GetBitValue(data[1], 2))
            {
                this.reader0.RSSI = (DataTran.GetBitValue(data[2], 2) ? 1 : 0) + (DataTran.GetBitValue(data[2], 3) ? 2 : 0);
                if (data[1] == 3 && data[4] == 8)
                {
                    this.reader0.UidData.Clear();
                    this.reader0.DataLen = data[4];
                    this.reader0.UidData.Add(DataTran.GetByteFromArray(data, 5, data[4]));
                }
            }
            else
            {
                this.reader0.RSSI = 0;
                this.reader0.DataLen = 0;
                this.reader0.UidData.Clear();
            }

            this.reader1.Conn = DataTran.GetBitValue(data[33], 1);
            this.reader1.TagPresent = DataTran.GetBitValue(data[33], 2);
            this.reader1.Busy = DataTran.GetBitValue(data[33], 3);
            this.reader1.Done = DataTran.GetBitValue(data[33], 4);
            this.reader1.Err = DataTran.GetBitValue(data[33], 5);
            this.reader1.Toggle = DataTran.GetBitValue(data[33], 6);
            this.reader1.UserDataLen = data[36];
            if (DataTran.GetBitValue(data[33], 2))
            {
                this.reader1.RSSI = (DataTran.GetBitValue(data[34], 2) ? 1 : 0) + (DataTran.GetBitValue(data[34], 3) ? 2 : 0);
                if (data[33] == 3 && data[36] == 8)
                {
                    this.reader1.UidData.Clear();
                    this.reader1.DataLen = data[36];
                    this.reader1.UidData.Add(DataTran.GetByteFromArray(data, 37, data[36]));
                }
            }
            else
            {
                this.reader1.RSSI = 0;
                this.reader1.DataLen = 0;
                this.reader1.UidData.Clear();
            }

            this.reader2.Conn = DataTran.GetBitValue(data[65], 1);
            this.reader2.TagPresent = DataTran.GetBitValue(data[65], 2);
            this.reader2.Busy = DataTran.GetBitValue(data[65], 3);
            this.reader2.Done = DataTran.GetBitValue(data[65], 4);
            this.reader2.Err = DataTran.GetBitValue(data[65], 5);
            this.reader2.Toggle = DataTran.GetBitValue(data[65], 6);
            this.reader2.UserDataLen = data[68];
            if (DataTran.GetBitValue(data[65], 2))
            {
                this.reader2.RSSI = (DataTran.GetBitValue(data[66], 2) ? 1 : 0) + (DataTran.GetBitValue(data[66], 3) ? 2 : 0);
                if (data[65] == 3 && data[68] == 8)
                {
                    this.reader2.UidData.Clear();
                    this.reader2.DataLen = data[68];
                    this.reader2.UidData.Add(DataTran.GetByteFromArray(data, 69, data[68]));
                }
            }
            else
            {
                this.reader2.RSSI = 0;
                this.reader2.DataLen = 0;
                this.reader2.UidData.Clear();
            }

            this.reader3.Conn = DataTran.GetBitValue(data[97], 1);
            this.reader3.TagPresent = DataTran.GetBitValue(data[97], 2);
            this.reader3.Busy = DataTran.GetBitValue(data[97], 3);
            this.reader3.Done = DataTran.GetBitValue(data[97], 4);
            this.reader3.Err = DataTran.GetBitValue(data[97], 5);
            this.reader3.Toggle = DataTran.GetBitValue(data[97], 6);
            this.reader3.UserDataLen = data[100];
            if (DataTran.GetBitValue(data[97], 2))
            {
                this.reader3.RSSI = (DataTran.GetBitValue(data[98], 2) ? 1 : 0) + (DataTran.GetBitValue(data[98], 3) ? 2 : 0);
                if (data[97] == 3 && data[100] == 8)
                {
                    this.reader3.UidData.Clear();
                    this.reader3.DataLen = data[100];
                    this.reader3.UidData.Add(DataTran.GetByteFromArray(data, 101, data[100]));
                }
            }
            else
            {
                this.reader3.RSSI = 0;
                this.reader3.DataLen = 0;
                this.reader3.UidData.Clear();
            }

            if (this.Readers == null)
            {
                this.Readers.Clear();
                this.Readers.Add(Reader.RF0, this.reader0);
                this.Readers.Add(Reader.RF1, this.reader1);
                this.Readers.Add(Reader.RF2, this.reader2);
                this.Readers.Add(Reader.RF3, this.reader3);
            }
            else
            {
                this.Readers[Reader.RF0] = this.reader0;
                this.Readers[Reader.RF1] = this.reader1;
                this.Readers[Reader.RF2] = this.reader2;
                this.Readers[Reader.RF3] = this.reader3;
            }

            #endregion RF

            DeviceMonitor?.BeginInvoke(this.Readers, this.GPIN, null, null);
        }

        /// <summary>
        /// 通讯检测
        /// </summary>
        /// <param name="state"></param>
        private void CheckConnectStatus(object state)
        {
            //if (this.IsConnected)
            //{
            //    byte[] result = ModTcp.ReadKeepReg(0, 2);
            //    if (result == null)
            //    {
            //        //_DisConnect();
            //    }
            //}
        }

        /// <summary>
        /// 开启监控
        /// </summary>
        private void StartPolling()
        {
            cts = new CancellationTokenSource();
            Task.Run(new Action(() =>
            {
                Monitoring();
            }), cts.Token);
        }

        /// <summary>
        /// 关闭监控
        /// </summary>
        private void StopPolling()
        {
            cts?.Cancel();
        }

        #endregion Private Method
    }

    /// <summary>
    /// 命令执行状态
    /// </summary>
    public enum ExecutSataus : byte
    {
        /// <summary>
        /// 网关通讯未连接
        /// </summary>
        DisConnected = 0x01,

        /// <summary>
        /// 正在执行命令
        /// </summary>
        Cmd_InExecut = 0x02,

        /// <summary>
        /// 命令执行超时
        /// </summary>
        Cmd_OverTime = 0x03,

        /// <summary>
        /// 输入参数错误
        /// </summary>
        InParam_Err = 0x04,

        /// <summary>
        /// 命令执行成功
        /// </summary>
        Cmd_Succeed = 0x05,

        /// <summary>
        /// 命令执行失败
        /// </summary>
        Cmd_Fail = 0x06,

        /// <summary>
        /// 读写器未使能工作
        /// </summary>
        Cmd_ReaderDisEnable = 07,

        /// <summary>
        /// 无标签到位
        /// </summary>
        Cmd_NoTag = 08
    }

    /// <summary>
    /// Reader
    /// </summary>
    public enum Reader : byte
    {
        /// <summary>
        /// 通道1
        /// </summary>
        RF0 = 0x01,

        /// <summary>
        /// 通道2
        /// </summary>
        RF1 = 0x02,

        /// <summary>
        /// 通道3
        /// </summary>
        RF2 = 0X03,

        /// <summary>
        /// 通道4
        /// </summary>
        RF3 = 0X04
    }

    /// <summary>
    /// GPO
    /// </summary>
    public enum GPOS : byte
    {
        /// <summary>
        /// 输出1
        /// </summary>
        GPO_1 = 0x01,

        /// <summary>
        /// 输出2
        /// </summary>
        GPO_2 = 0x02,

        /// <summary>
        /// 输出3
        /// </summary>
        GPO_3 = 0X03,

        /// <summary>
        /// 输出4
        /// </summary>
        GPO_4 = 0X04
    }

    /// <summary>
    /// GPI
    /// </summary>
    public enum GPIS : byte
    {
        /// <summary>
        /// 输入1
        /// </summary>
        GPI_1 = 0x01,

        /// <summary>
        /// 输入2
        /// </summary>
        GPI_2 = 0x02,

        /// <summary>
        /// 输入3
        /// </summary>
        GPI_3 = 0X03,

        /// <summary>
        /// 输入4
        /// </summary>
        GPI_4 = 0X04
    }

    /// <summary>
    /// 字符串类型
    /// </summary>
    public enum CharType : int
    {
        /// <summary>
        /// Dec
        /// </summary>
        DEC = 0x00,

        /// <summary>
        /// ASCII
        /// </summary>
        ASCII = 0X01,

        /// <summary>
        /// Hex
        /// </summary>
        HEX = 0X02
    }

    /// <summary>
    /// 读取User结果对象
    /// </summary>
    public class ReadResult
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public byte ErrCode { get; set; }

        /// <summary>
        /// 命令状态
        /// </summary>
        public ExecutSataus sataus { get; set; }

        /// <summary>
        /// 标签Tag数据
        /// </summary>
        public ListByteArray TagUser { get; set; } = new ListByteArray();
    }

    /// <summary>
    /// 写入User结果对象
    /// </summary>
    public class WriteResult
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public byte ErrCode { get; set; }

        /// <summary>
        /// 命令状态
        /// </summary>
        public ExecutSataus sataus { get; set; }
    }
}