using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Ini配置文件操作类
    /// </summary>
    public  class IniConfigHelper
    {

        #region kernel32.Dll - API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,string val, string filePath);
        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,string def, StringBuilder retVal, int size, string filePath);

        #endregion

        /// <summary>
        /// 读Ini文件
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">键</param>
        /// <param name="NoText">可用""表示无字符</param>
        /// <param name="iniFilePath">路径</param>
        /// <returns>返回结果 字符串</returns>
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 写Ini文件
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">键</param>
        /// <param name="Value">可用""表示无字符</param>
        /// <param name="iniFilePath">路径</param>
        /// <returns>返回结果 成功:true</returns>
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }


    }
}
   