using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    /// <summary>
    /// 语言切换
    /// </summary>
    public class MultiLanguage
    {
        //默认语言
        public static string DefaultLanguage = "zh-CN";

        /// <summary>
        /// 设置默认语言
        /// </summary>
        /// <param name="language">待设置默认语言</param>
        public static void SetDefaultLanguage(string language)
        {
            if (language == "zh-CN" || language == "en-US") 
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
                DefaultLanguage = language;
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(DefaultLanguage);
            }
        }

        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="form">加载语言的窗口</param>
        /// <param name="formType">窗口的类型</param>
        public static void LoadLanguage(Form form, Type formType)
        {
            if (form != null)
            {
                ComponentResourceManager resources = new ComponentResourceManager(formType);
                resources.ApplyResources(form, "$this");
                Loading(form, resources);
            }
        }

        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="resources">语言资源</param>
        private static void Loading(Control control, ComponentResourceManager resources)
        {
            if (control is MenuStrip)
            {
                resources.ApplyResources(control, control.Name);
                MenuStrip ms = (MenuStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (ToolStripMenuItem c in ms.Items)
                    {
                        Loading(c, resources);
                    }
                }
            }

            if (control is StatusStrip)
            {
                resources.ApplyResources(control, control.Name);
                StatusStrip ts = (StatusStrip)control;
                foreach (ToolStripItem c in ts.Items)
                {
                    resources.ApplyResources(c, c.Name);
                }
            }

            //需要完善
            foreach (Control c in control.Controls)
            {
                resources.ApplyResources(c, c.Name);
                Loading(c, resources);
            }
        }

        /// <summary>
        /// 遍历菜单
        /// </summary>
        /// <param name="item">菜单项</param>
        /// <param name="resources">语言资源</param>
        private static void Loading(ToolStripMenuItem item, ComponentResourceManager resources)
        {
            if (item is ToolStripMenuItem)
            {
                resources.ApplyResources(item, item.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)item;
                if (tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in tsmi.DropDownItems)
                    {
                        Loading(c, resources);
                    }
                }
            }
        }
    }
}
