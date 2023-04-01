using Entity;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DAL
{

    /// <summary>
    /// 数据格式
    /// </summary>
    public enum DataFormat
    {
        ABCD,
        BADC,
        CDAB,
        DCBA
    }

    /// <summary>
    /// 存储区
    /// </summary>
    public enum StoreArea
    {
        输出线圈0X,
        输入线圈1X,
        输入寄存器3X,
        保持寄存器4X
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum VarType
    {
        Bit,
        Byte,
        Short,
        UShort,
        Int,
        UInt,
        Float
    }

    /// <summary>
    /// ModbusTCP类
    /// </summary>
    public class ModTcpHelper : IDisposable
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public ModTcpHelper()
        {
            TcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            simpleHybirdLock = new SimpleHybirdLock();
        }

        #region ModbusTCP Config

        /// <summary>
        /// 单元标识符
        /// </summary>
        public int SlaveAddress { get; set; } = 1;

        /// <summary>
        /// 数据编码格式
        /// </summary>
        public DataFormat dataFormat { get; set; } = DataFormat.ABCD;

        /// <summary>
        /// 接收最大次数
        /// </summary>
        private int MaxCycle_Count { get; set; } = 50;

        /// <summary>
        /// 接收休眠时间
        /// </summary>
        private int checkInterval { get; set; } = 2;

        /// <summary>
        /// 发送数据超时
        /// </summary>
        private int SendTimeOUT { get; set; } = 2000;

        /// <summary>
        /// 接收数据超时
        /// </summary>
        private int ReceiveTimeOUT { get; set; } = 2000;

        #endregion ModbusTCP Config

        #region Object

        /// <summary>
        /// Socket
        /// </summary>
        private Socket TcpClient;

        /// <summary>
        /// SimpleHybirdLock
        /// </summary>
        private SimpleHybirdLock simpleHybirdLock;

        #endregion Object

        #region Connent/Disconnect

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="IP">IP</param>
        /// <param name="Port">PORT</param>
        public bool Connect(string IP, string Port)
        {
            try
            {
                TcpClient.SendTimeout = SendTimeOUT;
                TcpClient.ReceiveTimeout = ReceiveTimeOUT;
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), int.Parse(Port));
                TcpClient.Connect(endPoint);
                return true;
            }
            catch (Exception ex)
            {
                TcpClient?.Close();
                return false;
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (TcpClient != null && TcpClient.Connected)
            {
                TcpClient.Close();
            }
        }

        /// <summary>
        /// ConnectState
        /// </summary>
        /// <returns>连接/未连接</returns>
        public bool ConnectState()
        {
            return TcpClient.Connected;
        }

        #endregion Connent/Disconnect

        #region FunctionCode

        /// <summary>
        /// ReadKeepReg 0x03 
        /// </summary>
        /// <param name="iAddress">起始地址</param>
        /// <param name="iLength">读取点数</param>
        /// <returns>byte数组</returns>
        public byte[] ReadKeepReg(int iAddress, int iLength)
        {
            ListByteArray sendByteArray = new ListByteArray();
            sendByteArray.Add(new byte[] { 0, 0, 0, 0 });
            sendByteArray.Add(new byte[] { 0, 6 });
            sendByteArray.Add((byte)SlaveAddress);
            sendByteArray.Add(0x03);
            sendByteArray.Add(new byte[] { (byte)(iAddress / 256), (byte)(iAddress % 256) });
            sendByteArray.Add(new byte[] { (byte)(iLength / 256), (byte)(iLength % 256) });
            byte[] RcvBuffer = SendAndReceive(sendByteArray.ByteArray);
            if (RcvBuffer != null)
            {
                if (RcvBuffer[7] == 0x03 && RcvBuffer[8] == (iLength * 2) && RcvBuffer.Length == (9 + iLength * 2))
                {
                    return DataTran.GetByteFromArray(RcvBuffer, 9, iLength * 2);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ReadKeepReg 0x03 - swap 
        /// </summary>
        /// <param name="iAddress">起始地址</param>
        /// <param name="iLength">读取点数</param>
        /// <returns>byte数组</returns>
        public byte[] ReadKeepRegSwap(int iAddress, int iLength)
        {
            byte[] RcvBuffer = ReadKeepReg(iAddress, iLength);

            if (RcvBuffer != null)
            {
                return DataTran.GetSwapByteArray(RcvBuffer, DataFormat.CDAB);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// PreSetMulti 0x10 
        /// </summary>
        /// <param name="iAddress">起始地址</param>
        /// <param name="SetValue">写入值</param>
        /// <returns>成功:true/失败:false</returns>
        public bool PreSetMultiByteSwap(int iAddress, byte[] SetValue)
        {
            if (SetValue == null || SetValue.Length == 0 || SetValue.Length % 2 == 1)
            {
                return false;
            }
            SetValue = DataTran.GetSwapByteArray(SetValue, DataFormat.CDAB);
            int byteCount = SetValue.Length + 7;
            int byteLen = SetValue.Length / 2;
            ListByteArray sendByteArray = new ListByteArray();
            sendByteArray.Add(new byte[] { 0, 0, 0, 0 });
            sendByteArray.Add(new byte[] { (byte)(byteCount / 256), (byte)(byteCount % 256) });
            sendByteArray.Add((byte)SlaveAddress);
            sendByteArray.Add(0x10);
            sendByteArray.Add(new byte[] { (byte)(iAddress / 256), (byte)(iAddress % 256) });
            sendByteArray.Add(new byte[] { (byte)(byteLen / 256), (byte)(byteLen % 256) });
            sendByteArray.Add((byte)SetValue.Length);
            sendByteArray.Add(SetValue);
            byte[] RcvBuffer = SendAndReceive(sendByteArray.ByteArray);
            if (RcvBuffer != null)
            {
                byte[] sendTemp = DataTran.GetByteFromArray(sendByteArray.ByteArray, 0, 12);
                sendTemp[4] = 0x00;
                sendTemp[5] = 0x06;
                return DataTran.ByteArrayEquals(sendTemp, RcvBuffer);
            }
            else
            {
                return false;
            }
        }


        #endregion FunctionCode

        #region Read&Write

        /// <summary>
        /// SendAndReceive
        /// </summary>
        /// <param name="SendByte">发送字节数组</param>
        /// <returns></returns>
        private byte[] SendAndReceive(byte[] SendByte)
        {
            simpleHybirdLock.Enter();
            try
            {
                TcpClient.Send(SendByte);
                byte[] result = ReadMessage();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                simpleHybirdLock.Leave();
            }
        }

        /// <summary>
        /// SendAndReceive
        /// </summary>
        /// <returns></returns>
        private byte[] ReadMessage()
        {
            int count = TcpClient.Available;
            int RCV_CycleCount = 0;
            while (count == 0)
            {
                count = TcpClient.Available;
                RCV_CycleCount++; //读取次数
                if (RCV_CycleCount > MaxCycle_Count)
                {
                    break;
                }
                Thread.Sleep(checkInterval);
            }
            if (count <= 0)
            {
                return null;
            }
            else
            {
                byte[] buffer = new byte[count];
                TcpClient.Receive(buffer, count, SocketFlags.None);
                return buffer;
            }
        }

        #endregion Read&Write

        #region 释放资源

        /// <summary>
        /// 释放标记
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 显式调用Dispose方法
        /// </summary>
        ~ModTcpHelper()
        {
            //必须为false
            Dispose(false);
        }

        /// <summary>
        /// 执行与释放或重置非托管资源关联的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收器不再调用终结器
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 非密封类可重写的Dispose方法，方便子类继承时可重写
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            //清理托管资源
            if (disposing)
            {
                TcpClient?.Close();
                TcpClient?.Dispose();
                simpleHybirdLock?.Dispose();
            }
            //清理非托管资源

            //已经被释放
            disposed = true;
        }

        #endregion 释放资源

    }
}
