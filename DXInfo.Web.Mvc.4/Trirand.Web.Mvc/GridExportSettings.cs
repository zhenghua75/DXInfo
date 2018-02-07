using System;
namespace Trirand.Web.Mvc
{
    /// <summary>
    /// �ļ���������
    /// </summary>
	public class GridExportSettings
	{
		public string CSVSeparator
		{
			get;
			set;
		}
		public string ExportUrl
		{
			get;
			set;
		}
		public bool ExportHeaders
		{
			get;
			set;
		}
		public ExportDataRange ExportDataRange
		{
			get;
			set;
		}
		public GridExportSettings()
		{
			this.ExportUrl = "";
			this.CSVSeparator = ",";
			this.ExportHeaders = true;
			this.ExportDataRange = ExportDataRange.Filtered;
		}
	}
}
