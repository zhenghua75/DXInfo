using System;
using System.IO;
using System.Xml;

namespace AMSApp.zhenghua.Common
{
	/// <summary>
	/// 配置文件适配器类.
	/// fightop@create 2006.6.21
	/// </summary>
	public class ConfigAdapter
	{
        /// <summary>
        /// 配置文件名称
        /// </summary>
        private const string  CONFIG_FILE = "Web.Config";

        private static string strWebConfigPath = "";

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ConfigAdapter()
        {
            // 设置配置文件路径
            strWebConfigPath = AppDomain.CurrentDomain.BaseDirectory + CONFIG_FILE;
        }

		/// <summary>
        /// 取配置项
        /// </summary>
        /// <param name="strNoteName">配置项名称</param>
        /// <returns>配置项值</returns>
        public static string GetConfigNote(string strNoteName)
        {
            XmlDocument xmldoc= new XmlDocument();
            string strNodeValue = "";
 

            xmldoc.Load(strWebConfigPath);

            //在配置文件中找appSettings
            XmlNodeList _node = null;
            foreach(XmlNode ne in xmldoc.DocumentElement.ChildNodes)
            {
                if ( ne.NodeType == XmlNodeType.Element )
                {
                    if(ne.Name.ToLower() == "appsettings")
                    {
                        _node=ne.ChildNodes; //取的设置结点集
                        break;
                    }
                }
            }


            foreach(XmlNode ne in _node)
            {
                if (ne.NodeType == XmlNodeType.Element )
                { //只对Element操作
                    if(ne.Attributes["key"].Value.ToLower()==strNoteName.ToLower())
                    {
                        strNodeValue = ne.Attributes["value"].Value; // 取节点值
                        break;
                    }
                }
            }

            return strNodeValue;
        }

        /// <summary>
        /// 设置配置项
        /// </summary>
        /// <param name="strNoteName">配置项名称</param>
        /// <param name="strNoteValue">配置项值</param>
        public static void SetConfigNote(string strNoteName,string strNoteValue)
        {
            XmlDocument xmldoc= new XmlDocument();
			xmldoc.PreserveWhitespace = true;

            xmldoc.Load(strWebConfigPath);

            //在配置文件中找appSettings
            XmlNodeList _node = null;
            foreach(XmlNode ne in xmldoc.DocumentElement.ChildNodes)
            {
                if ( ne.NodeType == XmlNodeType.Element )
                {
                    if(ne.Name.ToLower() == "appsettings")
                    {
                        _node=ne.ChildNodes; //取的设置结点集
                        break;
                    }
                }
            }


            foreach(XmlNode ne in _node)
            {
                if (ne.NodeType == XmlNodeType.Element )
                { //只对Element操作
                    if(ne.Attributes["key"].Value.ToLower()==strNoteName.ToLower())
                    {
                        //保存设置项并加密
                        ne.Attributes["value"].Value=strNoteValue; // 设置节点值
                        break;
                    }
                }
            }

            xmldoc.Save(strWebConfigPath);
        }

	}
}
