using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	internal static class GridUtil
	{
		internal static List<string> GetListOfColumns(JQGrid grid)
		{
			List<string> result = new List<string>();
			grid.Columns.FindAll((JQGridColumn c) => 
                c.EditType == EditType.DatePicker || 
                c.EditType == EditType.DateTimePicker||
                c.EditType == EditType.TimePicker||
                c.EditType == EditType.AutoComplete||
                c.EditType == EditType.Chosen).ForEach(delegate(JQGridColumn c)
			{
				Guard.IsNotNullOrEmpty(c.EditorControlID, "JQGridColumn.EditorControlID", "must be set to the ID of the editing control control if EditType = DatePicker DateTimePicker TimePicker AutoComplete Chosen");
				result.Add(c.EditType.ToString().ToLower() + ":" + c.DataField);
			}
			);
			return result;
		}
		internal static List<string> GetListOfEditors(JQGrid grid)
		{
			List<string> result = new List<string>();
			grid.Columns.FindAll((JQGridColumn c) => 
                c.EditType == EditType.DatePicker ||
                c.EditType == EditType.DateTimePicker ||
                c.EditType == EditType.TimePicker ||
                c.EditType == EditType.AutoComplete||
                c.EditType == EditType.Chosen).ForEach(delegate(JQGridColumn c)
			{
                Guard.IsNotNullOrEmpty(c.EditorControlID, 
                    "JQGridColumn.EditorControlID", 
                    "must be set to the ID of the editing control control if EditType = DatePicker DateTimePicker TimePicker  AutoComplete Chosen");
				result.Add(c.EditorControlID);
			}
			);
			return result;
		}
		internal static List<string> GetListOfSearchColumns(JQGrid grid)
		{
			List<string> result = new List<string>();
			grid.Columns.FindAll((JQGridColumn c) => c.SearchType == SearchType.DatePicker || 
                c.SearchType == SearchType.DateTimePicker||
                c.SearchType == SearchType.TimePicker||
                c.SearchType == SearchType.AutoComplete).ForEach(delegate(JQGridColumn c)
			{
                Guard.IsNotNullOrEmpty(c.SearchControlID, "JQGridColumn.SearchControlID", "must be set to the ID of the searching control control if SearchType = DatePicker,DateTimePicker TimePicker or SearchType = AutoComplete");
				result.Add(c.SearchType.ToString().ToLower() + ":" + c.DataField);
			}
			);
			return result;
		}
		internal static List<string> GetListOfSearchEditors(JQGrid grid)
		{
			List<string> result = new List<string>();
			grid.Columns.FindAll((JQGridColumn c) => c.SearchType == SearchType.DatePicker ||
                c.SearchType == SearchType.DateTimePicker ||
                c.SearchType == SearchType.TimePicker ||
                c.SearchType == SearchType.AutoComplete).ForEach(delegate(JQGridColumn c)
			{
                Guard.IsNotNullOrEmpty(c.SearchControlID, "JQGridColumn.SearchControlID", "must be set to the ID of the searching control control if SearchType = DatePicker,DateTimePicker TimePicker or SearchType = AutoComplete");
				result.Add(c.SearchControlID);
			}
			);
			return result;
		}
		internal static string GetAttachEditorsFunction(JQGrid grid, string editorType, string editorControlID)
		{
			new JavaScriptSerializer();
			GridUtil.GetListOfColumns(grid);
			GridUtil.GetListOfEditors(grid);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("function(el) {");
			stringBuilder.Append("setTimeout(function() {");
			stringBuilder.AppendFormat("var ec = '{0}';", editorControlID);
			if (editorType == "datepicker")
			{
				stringBuilder.Append("if (typeof(jQuery(el).datepicker) !== 'function')");
				stringBuilder.Append("alert('JQDatePicker javascript not present on the page. Please, include jquery.jqDatePicker.min.js');");
				stringBuilder.Append("jQuery(el).datepicker( eval(ec + '_dpid') );");
			}
			if (editorType == "autocomplete")
			{
				stringBuilder.Append("if (typeof(jQuery(el).autocomplete) !== 'function')");
				stringBuilder.Append("alert('JQAutoComplete javascript not present on the page. Please, include jquery.jqAutoComplete.min.js');");
				stringBuilder.Append("jQuery(el).autocomplete( eval(ec + '_acid') );");
			}
            if (editorType == "datetimepicker")
            {
                stringBuilder.Append("if (typeof(jQuery(el).datetimepicker) !== 'function')");
                stringBuilder.Append("alert('datetimepicker javascript not present on the page. Please, include jquery.datetimepicker.min.js');");
                stringBuilder.Append("jQuery(el).datetimepicker( eval(ec + '_dtpid') );");
            }
            if (editorType == "timepicker")
            {
                stringBuilder.Append("if (typeof(jQuery(el).datetimepicker) !== 'function')");
                stringBuilder.Append("alert('datetimepicker javascript not present on the page. Please, include jquery.datetimepicker.min.js');");
                stringBuilder.Append("jQuery(el).timepicker( eval(ec + '_tpid') );");
            }
            if (editorType == "chosen")
            {
                stringBuilder.Append("if (typeof(jQuery(el).chosen) !== 'function')");
                stringBuilder.Append("alert('chosen javascript not present on the page. Please, include chosen.jquery.min.js');");
                stringBuilder.Append("jQuery(el).chosen( eval(ec + '_cnid') );");
            }
			stringBuilder.Append("},200);");
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
