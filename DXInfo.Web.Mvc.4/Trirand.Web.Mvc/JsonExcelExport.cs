using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.Script.Serialization;

namespace Trirand.Web.Mvc
{
    internal class JsonExcelExport
    {
        private Hashtable _jsonValues;
		private JQGrid _grid;
        public JsonExcelExport(JQGrid grid)
		{
			this._jsonValues = new Hashtable();
			this._grid = grid;
		}
		public string Process()
		{
            ExcelExportSettings excelExportSettings = this._grid.ExcelExportSettings;

            if (!string.IsNullOrEmpty(excelExportSettings.Url))
            {
                this._jsonValues["url"] = excelExportSettings.Url;
            }
            else
            {
                if (!string.IsNullOrEmpty(this._grid.DataUrl))
                {
                    this._jsonValues["url"] = this._grid.DataUrl;
                }
            }
			
			string json = new JavaScriptSerializer().Serialize(this._jsonValues);
            return json;
		}
    }
}
