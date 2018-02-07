using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Linq;
namespace Trirand.Web.Mvc
{
	internal class JQGridRenderer
	{
		public string RenderHtml(JQGrid grid)
		{
			string text = "<table id='{0}'></table>";
			if (grid.ToolBarSettings.ToolBarPosition != ToolBarPosition.Hidden)
			{
				text += "<div id='{0}_pager'></div>";
			}
			if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
			{
				return "This is a trial version of jqGrid for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
			}
			if (string.IsNullOrEmpty(grid.ID))
			{
				throw new Exception("You need to set ID for this grid.");
			}
			text = string.Format(text, grid.ID);
			if (grid.HierarchySettings.HierarchyMode == HierarchyMode.Child || grid.HierarchySettings.HierarchyMode == HierarchyMode.ParentAndChild)
			{
				text += this.GetChildSubGridJavaScript(grid);
			}
			else
			{
				text += this.GetStartupJavascript(grid, false);
			}
			return text;
		}
        private string defaultFilter(JQGrid grid)
        {
            string retStr = "";
            Hashtable ht = new Hashtable();
            ht["groupOp"] = "AND";
            var cols = (from d in grid.Columns where d.Searchable select d).ToList();
            Hashtable[] ht1 = new Hashtable[cols.Count];
            for (int i = 0; i < cols.Count; i++)
            {
                Hashtable ht2 = new Hashtable();
                ht2["field"] = cols[i].DataField;
                ht2["op"] = "eq";
                ht2["data"] = "";
                ht1[i] = ht2;
            }
            ht["rules"] = ht1;
            string filtersStr = new JavaScriptSerializer().Serialize(ht);
            retStr += "var filtersStr = '"+filtersStr+"';";
            retStr += "var postData = $(\"#"+grid.ID+"\").jqGrid(\"getGridParam\", \"postData\");";
            retStr += "$.extend(postData, { filters: filtersStr });";

            retStr += "$(\".ui-pg-input\").css({ width: \"30px\" });";
            retStr += "$(\".ui-jqgrid-title\").css({ \"font-size\": \"20px\" });";
            retStr += "$(\"#" + grid.ID + "_toppager_center\").attr(\"align\", \"right\");";
            retStr += "$(\"#" + grid.ID + "_toppager_right\").css({ width: \"150px\" });";  
            return retStr;
        }
        private string GetStartupJavascript(JQGrid grid, bool subgrid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script type='text/javascript'>\n");
            stringBuilder.Append("jQuery(document).ready(function() {");
            stringBuilder.Append(this.GetStartupOptions(grid, subgrid));
            //zhh 20121217 去除默认条件列表
            //stringBuilder.Append(this.defaultFilter(grid));
            //标题居中 zhh 20131106
            stringBuilder.Append("$('#" + grid.ID + "').closest('div.ui-jqgrid-view')");
            stringBuilder.Append(".children('div.ui-jqgrid-titlebar')");
            stringBuilder.Append(".css('text-align', 'center')");
            stringBuilder.Append(".css('font-size', 'large')");
            stringBuilder.Append(".children('span.ui-jqgrid-title')");
            stringBuilder.Append(".css('float', 'none');");
            ////
            stringBuilder.Append("createContexMenuFromNavigatorButtons($('#" + grid.ID + "'), '#" + grid.ID + "_pager');");
            //stringBuilder.Append("resizeGrid($('#" + grid.ID + "'));");//
            stringBuilder.Append("$('#" + grid.ID + "').jqGrid('gridResize');");
            //stringBuilder.Append("resize_the_grid();");
            stringBuilder.Append("});");//$(window).resize(resize_the_grid);

            //stringBuilder.Append("function resize_the_grid(){$('#" + grid.ID + "').fluidGrid({base:'.ui-layout-center', offset:0});}");
            //Excel
            //if (!string.IsNullOrEmpty(grid.ExcelExportSettings.Url) || grid.ToolBarSettings.ShowExcelButton)
            //{
                stringBuilder.Append("\nfunction excelExport() {\n");
                stringBuilder.AppendFormat("$('#{0}').jqGrid('excelExport',{1});\n", grid.ID, this.GetExcelExportOptions(grid));
                stringBuilder.Append("}\n");
            //}
                stringBuilder.Append("function errorTextFormat(data){return '错误:'+data.responseText;}");
            if (grid.ToolBarSettings.ShowColumnChooser)
            {
                string strcc = @"function columnChooser(){$('#" + grid.ID + "').jqGrid('columnChooser');}";
                stringBuilder.Append(strcc);
            }
            //stringBuilder.Append("function FormatNumber(num) {if (num.length > 0) {return parseFloat(num);} return '';}\n");
            //stringBuilder.Append("function UnFormatNumber(num) {return num;}\n");            
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowDoubleClick) &&
                grid.ClientSideEvents.RowDoubleClick == "RowDoubleClick")
            {
                string strRowDoubleClick = "function RowDoubleClick(rowid){";
                strRowDoubleClick += "if(rowid){$('#" + grid.ID + "').jqGrid('editGridRow', rowid,$('#" + grid.ID + "').getGridParam('editDialogOptions'));}";
                strRowDoubleClick +="}\n";
                stringBuilder.Append(strRowDoubleClick);
            }            
            stringBuilder.Append("</script>");
            return stringBuilder.ToString();
        }
		private string GetChildSubGridJavaScript(JQGrid grid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<script type='text/javascript'>\n");
			stringBuilder.AppendFormat("function showSubGrid_{0}(subgrid_id, row_id, message, suffix) {{", grid.ID);
			stringBuilder.Append("var subgrid_table_id, pager_id;\r\n\t\t                subgrid_table_id = subgrid_id+'_t';\r\n\t\t                pager_id = 'p_'+ subgrid_table_id;\r\n                        if (suffix) { subgrid_table_id += suffix; pager_id += suffix;  }\r\n                        if (message) jQuery('#'+subgrid_id).append(message);                        \r\n\t\t                jQuery('#'+subgrid_id).append('<table id=' + subgrid_table_id + ' class=scroll></table><div id=' + pager_id + ' class=scroll></div>');\r\n                ");
			stringBuilder.Append(this.GetStartupOptions(grid, true));
			stringBuilder.Append("}");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}
		private string GetStartupOptions(JQGrid grid, bool subGrid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string arg = subGrid ? "jQuery('#' + subgrid_table_id)" : string.Format("jQuery('#{0}')", grid.ID);
			string arg2 = subGrid ? "jQuery('#' + pager_id)" : string.Format("jQuery('#{0}')", grid.ID + "_pager");
			string pagerSelectorID = subGrid ? "'#' + pager_id" : string.Format("'#{0}'", grid.ID + "_pager");
			string text = subGrid ? "&parentRowID=' + row_id + '" : string.Empty;
			string text2 = (grid.DataUrl.IndexOf("?") > 0) ? "&" : "?";
			string text3 = (grid.EditUrl.IndexOf("?") > 0) ? "&" : "?";
			string arg3 = string.Format("{0}{1}jqGridID={2}{3}", new object[]
			{
				grid.DataUrl,
				text2,
				grid.ID,
				text
			});
			string arg4 = string.Format("{0}{1}jqGridID={2}&editMode=1{3}", new object[]
			{
				grid.EditUrl,
				text3,
				grid.ID,
				text
			});
			if (grid.Columns.Count > 0 && grid.Columns[0].Frozen)
			{
				grid.AppearanceSettings.ShrinkToFit = false;
			}
			stringBuilder.AppendFormat("{0}.jqGrid({{", arg);
			stringBuilder.AppendFormat("url: '{0}'", arg3);
            stringBuilder.AppendFormat(",loadui: '{0}'", grid.LoadUI);
			stringBuilder.AppendFormat(",editurl: '{0}'", arg4);
			stringBuilder.AppendFormat(",mtype: 'GET'", new object[0]);
			stringBuilder.AppendFormat(",datatype: '{0}'", grid.DataType);
			stringBuilder.AppendFormat(",page: {0}", grid.PagerSettings.CurrentPage);
			stringBuilder.AppendFormat(",colNames: {0}", this.GetColNames(grid));
			stringBuilder.AppendFormat(",colModel: {0}", this.GetColModel(grid));
			stringBuilder.AppendFormat(",viewrecords: true", new object[0]);
			stringBuilder.AppendFormat(",scrollrows: false", new object[0]);
			stringBuilder.AppendFormat(",prmNames: {{ id: \"{0}\" }}", Util.GetPrimaryKeyField(grid));
			if (grid.AppearanceSettings.ShowFooter)
			{
				stringBuilder.Append(",footerrow: true");
				stringBuilder.Append(",userDataOnFooter: true");
			}
			if (!grid.AppearanceSettings.ShrinkToFit)
			{
				stringBuilder.Append(",shrinkToFit: false");
			}
			if (grid.ColumnReordering)
			{
				stringBuilder.Append(",sortable: true");
			}
			if (grid.AppearanceSettings.ScrollBarOffset != 18)
			{
				stringBuilder.AppendFormat(",scrollOffset: {0}", grid.AppearanceSettings.ScrollBarOffset);
			}
			if (grid.AppearanceSettings.RightToLeft)
			{
				stringBuilder.Append(",direction: 'rtl'");
			}
			if (grid.AutoWidth)
			{
				stringBuilder.Append(",autowidth: true");
			}
			if (!grid.ShrinkToFit)
			{
				stringBuilder.Append(",shrinkToFit: false");
			}
			if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Bottom || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom)
			{
				stringBuilder.AppendFormat(",pager: {0}", arg2);
			}
			if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Top || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom)
			{
				stringBuilder.Append(",toppager: true");
			}
			if (grid.RenderingMode == RenderingMode.Optimized)
			{
				if (grid.HierarchySettings.HierarchyMode != HierarchyMode.None)
				{
					throw new Exception("Optimized rendering is not compatible with hierarchy.");
				}
				stringBuilder.Append(",gridview: true");
			}
			if (grid.HierarchySettings.HierarchyMode == HierarchyMode.Parent || grid.HierarchySettings.HierarchyMode == HierarchyMode.ParentAndChild)
			{
                stringBuilder.Append(",subGrid: true");
				stringBuilder.AppendFormat(",subGridOptions: {0}", grid.HierarchySettings.ToJSON());
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.SubGridRowExpanded))
			{
				stringBuilder.AppendFormat(",subGridRowExpanded: {0}", grid.ClientSideEvents.SubGridRowExpanded);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.ServerError))
			{
				stringBuilder.AppendFormat(",errorCell: {0}", grid.ClientSideEvents.ServerError);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowSelect))
			{
				stringBuilder.AppendFormat(",onSelectRow: {0}", grid.ClientSideEvents.RowSelect);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.ColumnSort))
			{
				stringBuilder.AppendFormat(",onSortCol: {0}", grid.ClientSideEvents.ColumnSort);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowDoubleClick))
			{
				stringBuilder.AppendFormat(",ondblClickRow: {0}", grid.ClientSideEvents.RowDoubleClick);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowRightClick))
			{
				stringBuilder.AppendFormat(",onRightClickRow: {0}", grid.ClientSideEvents.RowRightClick);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.LoadDataError))
			{
				stringBuilder.AppendFormat(",loadError: {0}", grid.ClientSideEvents.LoadDataError);
			}
			else
			{
				stringBuilder.AppendFormat(",loadError: {0}", "jqGrid_aspnet_loadErrorHandler");
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.GridInitialized))
			{
				stringBuilder.AppendFormat(",gridComplete: {0}", grid.ClientSideEvents.GridInitialized);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.BeforeAjaxRequest))
			{
				stringBuilder.AppendFormat(",beforeRequest: {0}", grid.ClientSideEvents.BeforeAjaxRequest);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.AfterAjaxRequest))
			{
				stringBuilder.AppendFormat(",loadComplete: {0}", grid.ClientSideEvents.AfterAjaxRequest);
			}
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.SerializeGridData))
            {
                stringBuilder.AppendFormat(",serializeGridData:{0}", grid.ClientSideEvents.SerializeGridData);
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.SerializeRowData))
            {
                stringBuilder.AppendFormat(",serializeRowData:{0}", grid.ClientSideEvents.SerializeRowData);
            }
			if (grid.TreeGridSettings.Enabled)
			{
				stringBuilder.AppendFormat(",treeGrid: true", new object[0]);
				stringBuilder.AppendFormat(",treedatatype: 'json'", new object[0]);
				stringBuilder.AppendFormat(",treeGridModel: 'adjacency'", new object[0]);
				string arg5 = "{ level_field: 'tree_level', parent_id_field: 'tree_parent', leaf_field: 'tree_leaf', expanded_field: 'tree_expanded', loaded: 'tree_loaded', icon_field: 'tree_icon' }";
				stringBuilder.AppendFormat(",treeReader: {0}", arg5);
				stringBuilder.AppendFormat(",ExpandColumn: '{0}'", this.GetFirstVisibleDataField(grid));
				Hashtable hashtable = new Hashtable();
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.CollapsedIcon))
				{
					hashtable.Add("plus", grid.TreeGridSettings.CollapsedIcon);
				}
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.ExpandedIcon))
				{
					hashtable.Add("minus", grid.TreeGridSettings.ExpandedIcon);
				}
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.LeafIcon))
				{
					hashtable.Add("leaf", grid.TreeGridSettings.LeafIcon);
				}
				if (hashtable.Count > 0)
				{
					stringBuilder.AppendFormat(",treeIcons: {0}", new JavaScriptSerializer().Serialize(hashtable));
				}
			}
			if (!grid.AppearanceSettings.HighlightRowsOnHover)
			{
				stringBuilder.Append(",hoverrows: false");
			}
			if (grid.AppearanceSettings.AlternateRowBackground)
			{
				stringBuilder.Append(",altRows: true");
			}
			if (grid.AppearanceSettings.ShowRowNumbers)
			{
				stringBuilder.Append(",rownumbers: true");
			}
			if (grid.AppearanceSettings.RowNumbersColumnWidth != 25)
			{
				stringBuilder.AppendFormat(",rownumWidth: {0}", grid.AppearanceSettings.RowNumbersColumnWidth.ToString());
			}
			if (grid.PagerSettings.ScrollBarPaging)
			{
				stringBuilder.AppendFormat(",scroll: 1", new object[0]);
			}
			stringBuilder.AppendFormat(",rowNum: {0}", grid.PagerSettings.PageSize.ToString());
			stringBuilder.AppendFormat(",rowList: {0}", grid.PagerSettings.PageSizeOptions.ToString());
            //stringBuilder.Append(",recordpos:left");
            if (!string.IsNullOrEmpty(grid.PagerSettings.NoRowsMessage))
			{
				stringBuilder.AppendFormat(",emptyrecords: '{0}'", grid.PagerSettings.NoRowsMessage.ToString());
			}
			stringBuilder.AppendFormat(",editDialogOptions: {0}", this.GetEditOptions(grid));
			stringBuilder.AppendFormat(",addDialogOptions: {0}", this.GetAddOptions(grid));
			stringBuilder.AppendFormat(",delDialogOptions: {0}", this.GetDelOptions(grid));
			stringBuilder.AppendFormat(",searchDialogOptions: {0}", this.GetSearchOptions(grid));
			string format;
			if (grid.TreeGridSettings.Enabled)
			{
				format = ",jsonReader: {{ id: \"{0}\", repeatitems:false,subgrid:{{repeatitems:false}} }}";
			}
			else
			{
				format = ",jsonReader: {{ id: \"{0}\" }}";
			}
			stringBuilder.AppendFormat(format, grid.Columns[Util.GetPrimaryKeyIndex(grid)].DataField);
			if (!string.IsNullOrEmpty(grid.SortSettings.InitialSortColumn))
			{
				stringBuilder.AppendFormat(",sortname: '{0}'", grid.SortSettings.InitialSortColumn);
			}
			stringBuilder.AppendFormat(",sortorder: '{0}'", grid.SortSettings.InitialSortDirection.ToString().ToLower());
			if (grid.MultiSelect)
			{
				stringBuilder.Append(",multiselect: true");
				if (grid.MultiSelectMode == MultiSelectMode.SelectOnCheckBoxClickOnly)
				{
					stringBuilder.AppendFormat(",multiboxonly: true", grid.MultiSelect.ToString().ToLower());
				}
				if (grid.MultiSelectKey != MultiSelectKey.None)
				{
					stringBuilder.AppendFormat(",multikey: '{0}'", this.GetMultiKeyString(grid.MultiSelectKey));
				}
			}
			if (!string.IsNullOrEmpty(grid.AppearanceSettings.Caption))
			{
				stringBuilder.AppendFormat(",caption: '{0}'", grid.AppearanceSettings.Caption);
			}
			if (!grid.Width.IsEmpty)
			{
				stringBuilder.AppendFormat(",width: '{0}'", grid.Width.ToString().Replace("px", ""));
			}
			if (!grid.Height.IsEmpty)
			{
				stringBuilder.AppendFormat(",height: '{0}'", grid.Height.ToString().Replace("px", ""));
			}
			if (grid.GroupSettings.GroupFields.Count > 0)
			{
				stringBuilder.Append(",grouping:true");
				stringBuilder.Append(",groupingView: {");
				stringBuilder.AppendFormat("groupField: ['{0}']", grid.GroupSettings.GroupFields[0].DataField);
				stringBuilder.AppendFormat(",groupColumnShow: [{0}]", grid.GroupSettings.GroupFields[0].ShowGroupColumn.ToString().ToLower());
				stringBuilder.AppendFormat(",groupText: ['{0}']", grid.GroupSettings.GroupFields[0].HeaderText);
				stringBuilder.AppendFormat(",groupOrder: ['{0}']", grid.GroupSettings.GroupFields[0].GroupSortDirection.ToString().ToLower());
				stringBuilder.AppendFormat(",groupSummary: [{0}]", grid.GroupSettings.GroupFields[0].ShowGroupSummary.ToString().ToLower());
				stringBuilder.AppendFormat(",groupCollapse: {0}", grid.GroupSettings.CollapseGroups.ToString().ToLower());
				stringBuilder.AppendFormat(",groupDataSorted: true", new object[0]);
				stringBuilder.Append("}");
			}
			stringBuilder.AppendFormat(",viewsortcols: [{0},'{1}',{2}]", "false", grid.SortSettings.SortIconsPosition.ToString().ToLower(), (grid.SortSettings.SortAction == SortAction.ClickOnHeader) ? "true" : "false");
            
			stringBuilder.AppendFormat("}})\r", new object[0]);
			stringBuilder.Append(this.GetToolBarOptions(grid, subGrid, pagerSelectorID));
			if (!grid.PagerSettings.ScrollBarPaging)
			{
				stringBuilder.AppendFormat(".bindKeys()", new object[0]);
			}
			if (grid.Columns.Count > 0 && grid.Columns[0].Frozen)
			{
				stringBuilder.AppendFormat(".setFrozenColumns()", new object[0]);
			}
			stringBuilder.Append(";");
			stringBuilder.Append(this.GetLoadErrorHandler());
			stringBuilder.Append(";");
			if (grid.HeaderGroups.Count > 0)
			{
				List<Hashtable> list = new List<Hashtable>();
				foreach (JQGridHeaderGroup current in grid.HeaderGroups)
				{
					list.Add(current.ToHashtable());
				}
				stringBuilder.AppendFormat("{0}.setGroupHeaders( {{ useColSpanStyle:true,groupHeaders:{1} }});", arg, new JavaScriptSerializer().Serialize(list));
			}
			if (grid.ToolBarSettings.ShowSearchToolBar)
			{
				stringBuilder.AppendFormat("{0}.filterToolbar({1});", arg, new JsonSearchToolBar(grid).Process());
			}
			return stringBuilder.ToString();
		}
		private string GetEditOptions(JQGrid grid)
		{
			JsonEditDialog jsonEditDialog = new JsonEditDialog(grid);
			return jsonEditDialog.Process();
		}
		private string GetAddOptions(JQGrid grid)
		{
			JsonAddDialog jsonAddDialog = new JsonAddDialog(grid);
			return jsonAddDialog.Process();
		}
		private string GetDelOptions(JQGrid grid)
		{
			JsonDelDialog jsonDelDialog = new JsonDelDialog(grid);
			return jsonDelDialog.Process();
		}
		private string GetSearchOptions(JQGrid grid)
		{
			JsonSearchDialog jsonSearchDialog = new JsonSearchDialog(grid);
			return jsonSearchDialog.Process();
		}
        private string GetExcelExportOptions(JQGrid grid)
        {
            JsonExcelExport jsonGridExport = new JsonExcelExport(grid);
            return jsonGridExport.Process();
        }
		private string GetColNames(JQGrid grid)
		{
			string[] array = new string[grid.Columns.Count];
			for (int i = 0; i < grid.Columns.Count; i++)
			{
				JQGridColumn jQGridColumn = grid.Columns[i];
				array[i] = (string.IsNullOrEmpty(jQGridColumn.HeaderText) ? jQGridColumn.DataField : jQGridColumn.HeaderText);
			}
			return new JavaScriptSerializer().Serialize(array);
		}
		private string GetColModel(JQGrid grid)
		{
			Hashtable[] array = new Hashtable[grid.Columns.Count];
			for (int i = 0; i < grid.Columns.Count; i++)
			{
				JsonColModel jsonColModel = new JsonColModel(grid.Columns[i], grid);
				array[i] = jsonColModel.JsonValues;
			}
			string input = new JavaScriptSerializer().Serialize(array);
			return JsonColModel.RemoveQuotesForJavaScriptMethods(input, grid);
		}
		private string GetMultiKeyString(MultiSelectKey key)
		{
			switch (key)
			{
			case MultiSelectKey.Shift:
				return "shiftKey";

			case MultiSelectKey.Ctrl:
				return "ctrlKey";

			case MultiSelectKey.Alt:
				return "altKey";

			default:
				throw new Exception("Should not be here.");
			}
		}
		private string GetToolBarOptions(JQGrid grid, bool subGrid, string pagerSelectorID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (grid.ShowToolBar)
			{
				JsonToolBar obj = new JsonToolBar(grid.ToolBarSettings);
                string json = new JavaScriptSerializer().Serialize(obj);
                ClientSideEvents clientSideEvents = grid.ClientSideEvents;
                json = JsonUtil.RenderClientSideEvent(json, "beforeRefresh", clientSideEvents.BeforeRefresh);
                if (!subGrid)
                {
                    stringBuilder.AppendFormat(".navGrid('#{0}',{1},{2},{3},{4},{5} )", new object[]
					{
						grid.ID + "_pager",
						//new JavaScriptSerializer().Serialize(obj),
                        json,
						string.Format("jQuery('#{0}').getGridParam('editDialogOptions')", grid.ID),
						string.Format("jQuery('#{0}').getGridParam('addDialogOptions')", grid.ID),
						string.Format("jQuery('#{0}').getGridParam('delDialogOptions')", grid.ID),
						string.Format("jQuery('#{0}').getGridParam('searchDialogOptions')", grid.ID),                        
					});
                }
                else
                {
                    stringBuilder.AppendFormat(".navGrid('#' + pager_id,{0},{1},{2},{3},{4} )", new object[]
					{
						//new JavaScriptSerializer().Serialize(obj),
                        json,
						"jQuery('#' + subgrid_table_id).getGridParam('editDialogOptions')",
						"jQuery('#' + subgrid_table_id).getGridParam('addDialogOptions')",
						"jQuery('#' + subgrid_table_id).getGridParam('delDialogOptions')",
						"jQuery('#' + subgrid_table_id).getGridParam('searchDialogOptions')"
					});
                }
				foreach (JQGridToolBarButton current in grid.ToolBarSettings.CustomButtons)
				{
					if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Bottom || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom)
					{
						JsonCustomButton jsonCustomButton = new JsonCustomButton(current);
						stringBuilder.AppendFormat(".navButtonAdd({0},{1})", pagerSelectorID, jsonCustomButton.Process());
					}
					if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Top)
					{
						JsonCustomButton jsonCustomButton2 = new JsonCustomButton(current);
						stringBuilder.AppendFormat(".navButtonAdd({0},{1})", pagerSelectorID.Replace("_pager", "_toppager"), jsonCustomButton2.Process());
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}
		private string GetLoadErrorHandler()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\n");
			stringBuilder.Append("function jqGrid_aspnet_loadErrorHandler(xht, st, handler) {");
			stringBuilder.Append("jQuery(document.body).css('font-size','100%'); jQuery(document.body).html(xht.responseText);");
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
		private string GetJQuerySubmit(JQGrid grid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("\r\n                        var _theForm = document.getElementsByTagName('FORM')[0];\r\n                        jQuery(_theForm).submit( function() \r\n                        {{  \r\n                            jQuery('#{0}').attr('value', jQuery('#{1}').getGridParam('selrow'));                            \r\n                        }});\r\n                       ", grid.ID + "_SelectedRow", grid.ID, grid.ID + "_CurrentPage");
			return stringBuilder.ToString();
		}
		private string GetFirstVisibleDataField(JQGrid grid)
		{
			foreach (JQGridColumn current in grid.Columns)
			{
				if (current.Visible)
				{
					return current.DataField;
				}
			}
			return grid.Columns[0].DataField;
		}
	}
}
