using System;
namespace Trirand.Web.Mvc
{
    /// <summary>
    /// EXCEL文件导出设置
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
