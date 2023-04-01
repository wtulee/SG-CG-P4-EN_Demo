using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL
{
    /// <summary>
    /// 数据转换类
    /// </summary>
    public class DataTran
    {
        /// <summary>
        /// 字符串转换bool数组
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>bool数组</returns>
        public static bool[] GetBoolArray(string val)
        {
            List<bool> Result = new List<bool>();
            if (val.Contains(' '))
            {
                string[] str = Regex.Split(val, "\\s+", RegexOptions.IgnoreCase);
                foreach (var item in str)
                {
                    Result.Add(item.ToLower() == "true" || item == "1");
                }
            }

            if (!val.Contains(' '))
            {
                char[] str = val.ToCharArray();
                foreach (var item in str)
                {
                    Result.Add(item.ToString().ToLower() == "true" || item.ToString() == "1");
                }
            }
            return Result.ToArray();
        }

        /// <summary>
        /// 字符串转换byte数组
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteArray(string val)
        {
            List<byte> Result = new List<byte>();
            if (val.Contains(' '))
            {
                string[] str = Regex.Split(val, "\\s+", RegexOptions.IgnoreCase);
                foreach (var item in str)
                {
                    Result.Add(Convert.ToByte(item));
                }
            }
            else
            {
                Result.Add(Convert.ToByte(val));
            }

            return Result.ToArray();
        }

        /// <summary>
        /// 字符串转short数组
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>short数组</returns>
        public static short[] GetShortArray(string val)
        {
            List<short> Result = new List<short>();
            if (val.Contains(' '))
            {
                string[] str = Regex.Split(val, "\\s+", RegexOptions.IgnoreCase);
                foreach (var item in str)
                {
                    Result.Add(Convert.ToInt16(item));
                }
            }
            else
            {
                Result.Add(Convert.ToInt16(val));
            }

            return Result.ToArray();
        }

        /// <summary>
        /// 字符串转ushort数组
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>ushort数组</returns>
        public static ushort[] GetUShortArray(string val)
        {
            List<ushort> Result = new List<ushort>();
            if (val.Contains(' '))
            {
                string[] str = Regex.Split(val, "\\s+", RegexOptions.IgnoreCase);
                foreach (var item in str)
                {
                    Result.Add(Convert.ToUInt16(item));
                }
            }
            else
            {
                Result.Add(Convert.ToUInt16(val));
            }
            return Result.ToArray();
        }

        /// <summary>
        /// 字符串转int数组
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>int数组</returns>
        public static int[] GetIntArray(string val)
        {
            List<int> Result = new List<int>();
            if (val.Contains(' '))
            {
                string[] str = Regex.Split(val, "\\s+", RegexOptions.IgnoreCase);
                foreach (var item in str)
                {
                    Result.Add(Convert.ToInt32(item));
                }
            }
            else
            {
                Result.Add(Convert.ToInt32(val));
            }
            return Result.ToArray();
        }

        /// <summary>
        /// 字符串转uint数组
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>uint数组</returns>
        public static uint[] GetUIntArray(string val)
        {
            List<uint> Result = new List<uint>();
            if (val.Contains(' '))
            {
                string[] str = Regex.Split(val, "\\s+", RegexOptions.IgnoreCase);
                foreach (var item in str)
                {
                    Result.Add(Convert.ToUInt32(item));
                }
            }
            else
            {
                Result.Add(Convert.ToUInt32(val));
            }
            return Result.ToArray();
        }

        /// <summary>
        /// 字符串转float数组
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>float数组</returns>
        public static float[] GetFloatArray(string val)
        {
            List<float> Result = new List<float>();
            if (val.Contains(' '))
            {
                string[] str = Regex.Split(val, "\\s+", RegexOptions.IgnoreCase);
                foreach (var item in str)
                {
                    Result.Add(Convert.ToSingle(item));
                }
            }
            else
            {
                Result.Add(Convert.ToSingle(val));
            }
            return Result.ToArray();
        }

        /// <summary>
        /// byte数组转HEX字符串
        /// </summary>
        /// <param name="val">byte数组</param>
        /// <returns>HEX字符串</returns>
        public static string GetHexArray(byte[] val)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in val)
            {
                str.Append((Convert.ToString(item, 16).Length == 1 ? "0" + Convert.ToString(item, 16).ToUpper() : Convert.ToString(item, 16).ToUpper()) + " ");
            }
            return str.ToString();
        }

        /// <summary>
        /// byte数组转ASCII字符串
        /// </summary>
        /// <param name="val">byte数组</param>
        /// <returns>ASCII字符串</returns>
        public static string GetAsciiArray(byte[] val)
        {
            string tmp_str = System.Text.Encoding.ASCII.GetString(val);
            return tmp_str;
        }

        /// <summary>
        /// HEX字符串转DEC数组
        /// </summary>
        /// <param name="val">HEX字符串</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteFromHexStr(string val)
        {
            List<byte> Result = new List<byte>();
            if (val.Contains(' '))
            {
                string[] str = Regex.Split(val, "\\s+", RegexOptions.IgnoreCase);
                foreach (var item in str)
                {
                    Result.Add(Convert.ToByte(item, 16));
                }
            }
            else
            {
                Result.Add(Convert.ToByte(val, 16));
            }
            return Result.ToArray();
        }

        /// <summary>
        /// ASCII字符串转DEC数组
        /// </summary>
        /// <param name="val">ASCII字符串串</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteFromAsciiStr(string val)
        {
            ListByteArray Result = new ListByteArray();
            if (val.Length > 0)
            {
                for (int i = 0; i < val.Length; i++)
                {
                    //超出ASCII数值范围
                    if ((byte)val[i] < 32 && (byte)val[i] > 126)
                    {
                        return null;
                    }
                    else
                    {
                        Result.Add((byte)val[i]);
                    }
                }
                return Result.ByteArray;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///从byte数组中截取部分数据
        /// </summary>
        /// <param name="dest">被截取数组</param>
        /// <param name="offset">截取起始地址</param>
        /// <param name="count">截取字节个数</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteFromArray(byte[] dest, int offset, int count)
        {
            if (dest != null && dest.Length >= offset + count)
            {
                byte[] resultArray = new byte[count];
                Array.Copy(dest, offset, resultArray, 0, count);
                return resultArray;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 比较两个数组是否相等
        /// </summary>
        /// <param name="array1">数组1</param>
        /// <param name="array2">数组2</param>
        /// <returns>相等:true/不想等:false</returns>
        public static bool ByteArrayEquals(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null) return false;
            if (array1.Length == 0 || array2.Length == 0 || array1.Length != array2.Length) return false;
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// bool数组转byte数组
        /// </summary>
        /// <param name="array">bool数组</param>
        /// <returns>byte数组</returns>
        public static byte[] BoolArrayToByteByte(bool[] array)
        {
            if (array == null || array.Length == 0) return null;
            int ByteLen = (array.Length % 8) == 0 ? array.Length / 8 : array.Length / 8 + 1;
            byte[] result = new byte[ByteLen];
            for (int i = 0; i < ByteLen; i++)
            {
                int total = i == ByteLen - 1 ? array.Length - 8 * i : 8;
                for (int j = 0; j < total; j++)
                {
                    result[i] = SetBitValue(result[i], j + 1, array[8 * i + j]);
                }
            }
            return result;
        }

        /// <summary>
        /// 设置字节某个位的值
        /// </summary>
        /// <param name="data">byte数据</param>
        /// <param name="index">位数</param>
        /// <param name="val">设置值</param>
        /// <returns>byte数据</returns>
        public static byte SetBitValue(byte data, int index, bool val)
        {
            if (index > 8 || index < 1)
            {
                return 0;
            }
            int v = index < 2 ? index : (2 << (index - 2));
            return val ? (byte)(data | v) : (byte)(data & ~v);
        }

        /// <summary>
        /// 获取字节某个位的值
        /// </summary>
        /// <param name="data">byte数据</param>
        /// <param name="index">位数</param>
        /// <returns>true/false</returns>
        public static bool GetBitValue(byte data, int index)
        {
            if (index > 8 || index < 1)
            {
                return false;
            }
            bool result = false;
            switch (index)
            {
                case 1:
                    result = (data & 1) == 1;
                    break;

                case 2:
                    result = (data & 2) == 2;
                    break;

                case 3:
                    result = (data & 4) == 4;
                    break;

                case 4:
                    result = (data & 8) == 8;
                    break;

                case 5:
                    result = (data & 16) == 16;
                    break;

                case 6:
                    result = (data & 32) == 32;
                    break;

                case 7:
                    result = (data & 64) == 64;
                    break;

                case 8:
                    result = (data & 128) == 128;
                    break;

                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// byte数组高低字节交换
        /// </summary>
        /// <param name="array">byte数组</param>
        /// <param name="type">数据格式</param>
        /// <returns>byte数组</returns>
        public static byte[] GetSwapByteArray(byte[] array, DataFormat type)
        {
            if (array != null && array.Length >= 2 && array.Length % 2 == 0)
            {
                byte[] result = new byte[array.Length];
                switch (type)
                {
                    case DataFormat.ABCD:
                        result = array;
                        break;

                    case DataFormat.CDAB:
                        for (int i = 0; i < array.Length; i += 2)
                        {
                            result[i] = array[i + 1];
                            result[i + 1] = array[i];
                        }
                        break;
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 2字节转16位short数据
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="start">转换起始地址</param>
        /// <param name="type">数据格式</param>
        /// <returns>byte数组</returns>
        public static byte[] Get16ByteArray(byte[] array, int start, DataFormat type)
        {
            if (array != null && array.Length >= start + 2)
            {
                byte[] ResTemp = new byte[2];
                byte[] Res = new byte[2];
                for (int i = 0; i < 2; i++)
                {
                    ResTemp[i] = array[start + i];
                }
                switch (type)
                {
                    case DataFormat.ABCD:
                    case DataFormat.CDAB:
                        Res[0] = ResTemp[1];
                        Res[1] = ResTemp[0];
                        break;

                    case DataFormat.DCBA:
                    case DataFormat.BADC:
                        Res = ResTemp;
                        break;
                }
                return Res;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 4字节转32位int数据
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="start">转换起始地址</param>
        /// <param name="type">数据格式</param>
        /// <returns>byte数组</returns>
        public static byte[] Get32ByteArray(byte[] array, int start, DataFormat type)
        {
            if (array != null && array.Length >= start + 4)
            {
                byte[] ResTemp = new byte[4];
                byte[] Res = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    ResTemp[i] = array[start + i];
                }
                switch (type)
                {
                    case DataFormat.ABCD:
                        Res[0] = ResTemp[3];
                        Res[1] = ResTemp[2];
                        Res[2] = ResTemp[1];
                        Res[3] = ResTemp[0];
                        break;

                    case DataFormat.CDAB:
                        Res[0] = ResTemp[1];
                        Res[1] = ResTemp[0];
                        Res[2] = ResTemp[3];
                        Res[3] = ResTemp[2];
                        break;

                    case DataFormat.BADC:
                        Res[0] = ResTemp[2];
                        Res[1] = ResTemp[3];
                        Res[2] = ResTemp[0];
                        Res[3] = ResTemp[1];
                        break;

                    case DataFormat.DCBA:
                        Res = ResTemp;
                        break;
                }
                return Res;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 16位short数据转byte数组
        /// </summary>
        /// <param name="Value">uint数据</param>
        /// <param name="type">数据格式</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteArrayFrom16Bit(short Value, DataFormat type)
        {
            byte[] ResTemp = BitConverter.GetBytes(Value);
            byte[] Res = new byte[2];
            switch (type)
            {
                case DataFormat.ABCD:
                case DataFormat.CDAB:
                    Res[0] = ResTemp[1];
                    Res[1] = ResTemp[0];
                    break;

                case DataFormat.BADC:
                case DataFormat.DCBA:
                    Res = ResTemp;
                    break;
            }
            return Res;
        }

        /// <summary>
        /// 16位ushort数据转byte数组
        /// </summary>
        /// <param name="Value">uint数据</param>
        /// <param name="type">数据格式</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteArrayFrom16Bit(ushort Value, DataFormat type)
        {
            byte[] ResTemp = BitConverter.GetBytes(Value);
            byte[] Res = new byte[2];
            switch (type)
            {
                case DataFormat.ABCD:
                case DataFormat.CDAB:
                    Res[0] = ResTemp[1];
                    Res[1] = ResTemp[0];
                    break;

                case DataFormat.BADC:
                case DataFormat.DCBA:
                    Res = ResTemp;
                    break;
            }
            return Res;
        }

        /// <summary>
        /// 32位float数据转byte数组
        /// </summary>
        /// <param name="Value">uint数据</param>
        /// <param name="type">数据格式</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteArrayFrom32Bit(float Value, DataFormat type)
        {
            byte[] ResTemp = BitConverter.GetBytes(Value);
            byte[] Res = new byte[4];
            switch (type)
            {
                case DataFormat.ABCD:
                    Res[0] = ResTemp[3];
                    Res[1] = ResTemp[2];
                    Res[2] = ResTemp[1];
                    Res[3] = ResTemp[0];
                    break;

                case DataFormat.CDAB:
                    Res[0] = ResTemp[1];
                    Res[1] = ResTemp[0];
                    Res[2] = ResTemp[3];
                    Res[3] = ResTemp[2];
                    break;

                case DataFormat.BADC:
                    Res[0] = ResTemp[2];
                    Res[1] = ResTemp[3];
                    Res[2] = ResTemp[0];
                    Res[3] = ResTemp[1];
                    break;

                case DataFormat.DCBA:
                    Res = ResTemp;
                    break;
            }
            return Res;
        }

        /// <summary>
        /// 32位int数据转byte数组
        /// </summary>
        /// <param name="Value">uint数据</param>
        /// <param name="type">数据格式</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteArrayFrom32Bit(int Value, DataFormat type)
        {
            byte[] ResTemp = BitConverter.GetBytes(Value);
            byte[] Res = new byte[4];
            switch (type)
            {
                case DataFormat.ABCD:
                    Res[0] = ResTemp[3];
                    Res[1] = ResTemp[2];
                    Res[2] = ResTemp[1];
                    Res[3] = ResTemp[0];
                    break;

                case DataFormat.CDAB:
                    Res[0] = ResTemp[1];
                    Res[1] = ResTemp[0];
                    Res[2] = ResTemp[3];
                    Res[3] = ResTemp[2];
                    break;

                case DataFormat.BADC:
                    Res[0] = ResTemp[2];
                    Res[1] = ResTemp[3];
                    Res[2] = ResTemp[0];
                    Res[3] = ResTemp[1];
                    break;

                case DataFormat.DCBA:
                    Res = ResTemp;
                    break;
            }
            return Res;
        }

        /// <summary>
        /// 32位uint数据转byte数组
        /// </summary>
        /// <param name="Value">uint数据</param>
        /// <param name="type">数据格式</param>
        /// <returns>byte数组</returns>
        public static byte[] GetByteArrayFrom32Bit(uint Value, DataFormat type)
        {
            byte[] ResTemp = BitConverter.GetBytes(Value);
            byte[] Res = new byte[4];
            switch (type)
            {
                case DataFormat.ABCD:
                    Res[0] = ResTemp[3];
                    Res[1] = ResTemp[2];
                    Res[2] = ResTemp[1];
                    Res[3] = ResTemp[0];
                    break;

                case DataFormat.CDAB:
                    Res[0] = ResTemp[1];
                    Res[1] = ResTemp[0];
                    Res[2] = ResTemp[3];
                    Res[3] = ResTemp[2];
                    break;

                case DataFormat.BADC:
                    Res[0] = ResTemp[2];
                    Res[1] = ResTemp[3];
                    Res[2] = ResTemp[0];
                    Res[3] = ResTemp[1];
                    break;

                case DataFormat.DCBA:
                    Res = ResTemp;
                    break;
            }
            return Res;
        }
    }
}