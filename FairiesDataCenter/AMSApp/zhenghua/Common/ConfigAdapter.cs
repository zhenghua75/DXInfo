using System;
using System.IO;
using System.Xml;

namespace AMSApp.zhenghua.Common
{
	/// <summary>
	/// �����ļ���������.
	/// fightop@create 2006.6.21
	/// </summary>
	public class ConfigAdapter
	{
        /// <summary>
        /// �����ļ�����
        /// </summary>
        private const string  CONFIG_FILE = "Web.Config";

        private static string strWebConfigPath = "";

        /// <summary>
        /// ��̬������
        /// </summary>
        static ConfigAdapter()
        {
            // ���������ļ�·��
            strWebConfigPath = AppDomain.CurrentDomain.BaseDirectory + CONFIG_FILE;
        }

		/// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="strNoteName">����������</param>
        /// <returns>������ֵ</returns>
        public static string GetConfigNote(string strNoteName)
        {
            XmlDocument xmldoc= new XmlDocument();
            string strNodeValue = "";
 

            xmldoc.Load(strWebConfigPath);

            //�������ļ�����appSettings
            XmlNodeList _node = null;
            foreach(XmlNode ne in xmldoc.DocumentElement.ChildNodes)
            {
                if ( ne.NodeType == XmlNodeType.Element )
                {
                    if(ne.Name.ToLower() == "appsettings")
                    {
                        _node=ne.ChildNodes; //ȡ�����ý�㼯
                        break;
                    }
                }
            }


            foreach(XmlNode ne in _node)
            {
                if (ne.NodeType == XmlNodeType.Element )
                { //ֻ��Element����
                    if(ne.Attributes["key"].Value.ToLower()==strNoteName.ToLower())
                    {
                        strNodeValue = ne.Attributes["value"].Value; // ȡ�ڵ�ֵ
                        break;
                    }
                }
            }

            return strNodeValue;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="strNoteName">����������</param>
        /// <param name="strNoteValue">������ֵ</param>
        public static void SetConfigNote(string strNoteName,string strNoteValue)
        {
            XmlDocument xmldoc= new XmlDocument();
			xmldoc.PreserveWhitespace = true;

            xmldoc.Load(strWebConfigPath);

            //�������ļ�����appSettings
            XmlNodeList _node = null;
            foreach(XmlNode ne in xmldoc.DocumentElement.ChildNodes)
            {
                if ( ne.NodeType == XmlNodeType.Element )
                {
                    if(ne.Name.ToLower() == "appsettings")
                    {
                        _node=ne.ChildNodes; //ȡ�����ý�㼯
                        break;
                    }
                }
            }


            foreach(XmlNode ne in _node)
            {
                if (ne.NodeType == XmlNodeType.Element )
                { //ֻ��Element����
                    if(ne.Attributes["key"].Value.ToLower()==strNoteName.ToLower())
                    {
                        //�������������
                        ne.Attributes["value"].Value=strNoteValue; // ���ýڵ�ֵ
                        break;
                    }
                }
            }

            xmldoc.Save(strWebConfigPath);
        }

	}
}
