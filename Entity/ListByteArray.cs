using System.Collections.Generic;

namespace Entity
{
    /// <summary>
    /// 字节集合
    /// </summary>
    public class ListByteArray
    {
        private List<byte> ListArray = new List<byte>();

        /// <summary>
        /// 添加单字节
        /// </summary>
        /// <param name="item">添加值</param>
        public void Add(byte item)
        {
            ListArray.Add(item);
        }
        /// <summary>
        /// 添加字节数组
        /// </summary>
        /// <param name="item">添加值</param>
        public void Add(byte[] item)
        {
            ListArray.AddRange(item);
        }
        /// <summary>
        /// 清空字节集合
        /// </summary>
        public void Clear()
        {
            ListArray.Clear();
        }

        /// <summary>
        /// 返回字节数组形式数据
        /// </summary>
        public byte[] ByteArray
        {
            get { return ListArray.ToArray(); }
        }
    }
}