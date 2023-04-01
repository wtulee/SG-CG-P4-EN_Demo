namespace Entity
{
    /// <summary>
    ///Reader
    /// </summary>
    public class ReaderMonitor
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Conn { get; set; }
        /// <summary>
        /// 标签信号
        /// </summary>
        public bool TagPresent { get; set; }
        /// <summary>
        /// 命令状态
        /// </summary>
        public bool Done { get; set; }
        /// <summary>
        /// 错位标志
        /// </summary>
        public bool Err { get; set; }
        /// <summary>
        /// 翻转信号
        /// </summary>
        public bool Toggle { get; set; }
        /// <summary>
        /// 忙碌信号
        /// </summary>
        public bool Busy { get; set; }
        /// <summary>
        /// UID数据长度
        /// </summary>
        public byte DataLen { get; set; }
        /// <summary>
        /// user数据长度
        /// </summary>
        public byte UserDataLen { get; set; }
        /// <summary>
        /// 标签感应强度值
        /// </summary>
        public int RSSI { get; set; }
        /// <summary>
        /// 标签UID数据
        /// </summary>
        public ListByteArray UidData { get; set; } = new ListByteArray();
    }
}