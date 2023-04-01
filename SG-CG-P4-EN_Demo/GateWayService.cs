using DAL;
using Entity;
using System.Windows.Forms;

namespace SG_CG_P4_EN_Demo
{
    public class GateWayService
    {
        /// <summary>
        /// 网关对象
        /// </summary>
        public static EnService EN = new EnService();

        /// <summary>
        /// GPI
        /// </summary>
        public static GPIO GPIN = new GPIO();

        /// <summary>
        /// RFO
        /// </summary>
        public static ReaderMonitor reader0 = new ReaderMonitor();

        /// <summary>
        /// RF1
        /// </summary>
        public static ReaderMonitor reader1 = new ReaderMonitor();

        /// <summary>
        /// RF2
        /// </summary>
        public static ReaderMonitor reader2 = new ReaderMonitor();

        /// <summary>
        /// RF3
        /// </summary>
        public static ReaderMonitor reader3 = new ReaderMonitor();

        /// <summary>
        /// 测试状态
        /// </summary>
        public static bool TestStatus = false;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string IniFilePath = Application.StartupPath + "\\Config\\Config.ini";
        
        /// <summary>
        /// 语言选择
        /// </summary>
        public static string LanguageStr;
        
        /// <summary>
        /// 中英文标志
        /// </summary>
        public static bool IsEN 
        {
            get 
            {
                if (LanguageStr == "zh-CN") 
                {
                    return false;
                }
                else if (LanguageStr == "en-US")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}