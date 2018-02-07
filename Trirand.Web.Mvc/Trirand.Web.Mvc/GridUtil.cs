namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Script.Serialization;

    internal static class GridUtil
    {
        internal static string GetAttachEditorsFunction(JQGrid grid, string editorType, string editorControlID)
        {
            new JavaScriptSerializer();
            GetListOfColumns(grid);
            GetListOfEditors(grid);
            StringBuilder builder = new StringBuilder();
            builder.Append("function(el) {");
            builder.Append("setTimeout(function() {");
            builder.AppendFormat("var ec = '{0}';", editorControlID);
            if (editorType == "datepicker")
            {
                builder.Append("if (typeof(jQuery(el).datepicker) !== 'function')");
                builder.Append("alert('JQDatePicker javascript not present on the page. Please, include jquery.jqDatePicker.min.js');");
                builder.Append("jQuery(el).datepicker( eval(ec + '_dpid') );");
            }
            if (editorType == "autocomplete")
            {
                builder.Append("if (typeof(jQuery(el).autocomplete) !== 'function')");
                builder.Append("alert('JQAutoComplete javascript not present on the page. Please, include jquery.jqAutoComplete.min.js');");
                builder.Append("jQuery(el).autocomplete( eval(ec + '_acid') );");
            }
            builder.Append("},200);");
            builder.Append("}");
            return builder.ToString();
        }

        internal static List<string> GetListOfColumns(JQGrid grid)
        {
            List<string> result = new List<string>();
            grid.Columns.FindAll(delegate (JQGridColumn c) {
                if (c.EditType != EditType.DatePicker)
                {
                    return c.EditType == EditType.AutoComplete;
                }
                return true;
            }).ForEach(delegate (JQGridColumn c) {
                Guard.IsNotNullOrEmpty(c.EditorControlID, "JQGridColumn.EditorControlID", "must be set to the ID of the editing control control if EditType = DatePicker or EditType = AutoComplete");
                result.Add(c.EditType.ToString().ToLower() + ":" + c.DataField);
            });
            return result;
        }

        internal static List<string> GetListOfEditors(JQGrid grid)
        {
            List<string> result = new List<string>();
            grid.Columns.FindAll(delegate (JQGridColumn c) {
                if (c.EditType != EditType.DatePicker)
                {
                    return c.EditType == EditType.AutoComplete;
                }
                return true;
            }).ForEach(delegate (JQGridColumn c) {
                Guard.IsNotNullOrEmpty(c.EditorControlID, "JQGridColumn.EditorControlID", "must be set to the ID of the editing control control if EditType = DatePicker or EditType = AutoComplete");
                result.Add(c.EditorControlID);
            });
            return result;
        }

        internal static List<string> GetListOfSearchColumns(JQGrid grid)
        {
            List<string> result = new List<string>();
            grid.Columns.FindAll(delegate (JQGridColumn c) {
                if (c.SearchType != SearchType.DatePicker)
                {
                    return c.SearchType == SearchType.AutoComplete;
                }
                return true;
            }).ForEach(delegate (JQGridColumn c) {
                Guard.IsNotNullOrEmpty(c.SearchControlID, "JQGridColumn.SearchControlID", "must be set to the ID of the searching control control if SearchType = DatePicker or SearchType = AutoComplete");
                result.Add(c.SearchType.ToString().ToLower() + ":" + c.DataField);
            });
            return result;
        }

        internal static List<string> GetListOfSearchEditors(JQGrid grid)
        {
            List<string> result = new List<string>();
            grid.Columns.FindAll(delegate (JQGridColumn c) {
                if (c.SearchType != SearchType.DatePicker)
                {
                    return c.SearchType == SearchType.AutoComplete;
                }
                return true;
            }).ForEach(delegate (JQGridColumn c) {
                Guard.IsNotNullOrEmpty(c.SearchControlID, "JQGridColumn.SearchControlID", "must be set to the ID of the searching control control if SearchType = DatePicker or SearchType = AutoComplete");
                result.Add(c.SearchControlID);
            });
            return result;
        }
    }
}

