using System;
namespace Trirand.Web.Mvc
{
    /// <summary>
    /// EXCEL�ļ���������
    /// </summary>
	public class ExcelExportSettings
	{

        public string Url { get; set; }
        //public string Tag { get; set; }
        public ExcelExportSettings()
		{
			this.Url = "";
            //this.Tag = "excel";
		}
	}
}
