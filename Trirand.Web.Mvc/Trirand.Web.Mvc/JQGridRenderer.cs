namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.Script.Serialization;
    using System.Collections.Generic;

    internal class JQGridRenderer
    {
        private string GetAddOptions(JQGrid grid)
        {
            JsonAddDialog dialog = new JsonAddDialog(grid);
            return dialog.Process();
        }

        private string GetChildSubGridJavaScript(JQGrid grid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type='text/javascript'>\n");
            builder.AppendFormat("function showSubGrid_{0}(subgrid_id, row_id, message, suffix) {{", grid.ID);
            builder.Append("var subgrid_table_id, pager_id;\r\n\t\t                subgrid_table_id = subgrid_id+'_t';\r\n\t\t                pager_id = 'p_'+ subgrid_table_id;\r\n                        if (suffix) { subgrid_table_id += suffix; pager_id += suffix;  }\r\n                        if (message) jQuery('#'+subgrid_id).append(message);                        \r\n\t\t                jQuery('#'+subgrid_id).append('<table id=' + subgrid_table_id + ' class=scroll></table><div id=' + pager_id + ' class=scroll></div>');\r\n                ");
            builder.Append(this.GetStartupOptions(grid, true));
            builder.Append("}");
            builder.Append("</script>");
            return builder.ToString();
        }

        private string GetColModel(JQGrid grid)
        {
            List<string> lTreeGridColumn = new List<string>();
            lTreeGridColumn.Add("level");
            lTreeGridColumn.Add("parent_id");
            lTreeGridColumn.Add("isLeaf");
            lTreeGridColumn.Add("expanded");

            grid.Columns.RemoveAll(g => lTreeGridColumn.Contains(g.DataField));
            Hashtable[] hashtableArray = new Hashtable[grid.Columns.Count];
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                //if (lTreeGridColumn.Contains(grid.Columns[i].DataField)) continue;
                JsonColModel model = new JsonColModel(grid.Columns[i], grid);
                hashtableArray[i] = model.JsonValues;
            }
            return JsonColModel.RemoveQuotesForJavaScriptMethods(new JavaScriptSerializer().Serialize(hashtableArray), grid);
        }

        private string GetColNames(JQGrid grid)
        {
            List<string> lTreeGridColumn = new List<string>();
            lTreeGridColumn.Add("level");
            lTreeGridColumn.Add("parent_id");
            lTreeGridColumn.Add("isLeaf");
            lTreeGridColumn.Add("expanded");

            grid.Columns.RemoveAll(g => lTreeGridColumn.Contains(g.DataField));
            string[] strArray = new string[grid.Columns.Count];
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                //if (lTreeGridColumn.Contains(grid.Columns[i].DataField)) continue;
                JQGridColumn column = grid.Columns[i];
                strArray[i] = string.IsNullOrEmpty(column.HeaderText) ? column.DataField : column.HeaderText;
            }
            return new JavaScriptSerializer().Serialize(strArray);
        }

        private string GetDelOptions(JQGrid grid)
        {
            JsonDelDialog dialog = new JsonDelDialog(grid);
            return dialog.Process();
        }

        private string GetEditOptions(JQGrid grid)
        {
            JsonEditDialog dialog = new JsonEditDialog(grid);
            return dialog.Process();
        }

        private string GetJQuerySubmit(JQGrid grid)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n                        var _theForm = document.getElementsByTagName('FORM')[0];\r\n                        jQuery(_theForm).submit( function() \r\n                        {{  \r\n                            jQuery('#{0}').attr('value', jQuery('#{1}').getGridParam('selrow'));                            \r\n                        }});\r\n                       ", grid.ID + "_SelectedRow", grid.ID, grid.ID + "_CurrentPage");
            return builder.ToString();
        }

        private string GetLoadErrorHandler()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n");
            builder.Append("function jqGrid_aspnet_loadErrorHandler(xht, st, handler) {");
            builder.Append("jQuery(document.body).css('font-size','100%'); jQuery(document.body).html(xht.responseText);");
            builder.Append("}");
            return builder.ToString();
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
            }
            throw new Exception("Should not be here.");
        }

        private string GetSearchOptions(JQGrid grid)
        {
            JsonSearchDialog dialog = new JsonSearchDialog(grid);
            return dialog.Process();
        }

        private string GetStartupJavascript(JQGrid grid, bool subgrid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type='text/javascript'>\n");
            builder.Append("jQuery(document).ready(function() {");
            builder.Append(this.GetStartupOptions(grid, subgrid));
            builder.Append("});");
            builder.Append("</script>");
            return builder.ToString();
        }

        private string GetStartupOptions(JQGrid grid, bool subGrid)
        {
            StringBuilder builder = new StringBuilder();
            string str = subGrid ? "jQuery('#' + subgrid_table_id)" : string.Format("jQuery('#{0}')", grid.ID);
            string str2 = subGrid ? "jQuery('#' + pager_id)" : string.Format("jQuery('#{0}')", grid.ID + "_pager");
            string pagerSelectorID = subGrid ? "'#' + pager_id" : string.Format("'#{0}'", grid.ID + "_pager");
            string str4 = subGrid ? "&parentRowID=' + row_id + '" : string.Empty;
            string str5 = (grid.DataUrl.IndexOf("?") > 0) ? "&" : "?";
            string str6 = (grid.EditUrl.IndexOf("?") > 0) ? "&" : "?";
            string str7 = string.Format("{0}{1}jqGridID={2}{3}", new object[] { grid.DataUrl, str5, grid.ID, str4 });
            string str8 = string.Format("{0}{1}jqGridID={2}&editMode=1{3}", new object[] { grid.EditUrl, str6, grid.ID, str4 });

            
            builder.AppendFormat("{0}.jqGrid({{", str);
            //tree
            if (grid.TreeGrid)
            {
                string str9 = grid.TreeGrid ? "true" : "false";
                builder.AppendFormat("treeGrid:{0},treedatatype:'json',", str9);
                if (grid.TreeGridModel == TreeGridModel.adjacency)
                {
                    builder.Append("treeGridModel: 'adjacency',");
                }
                builder.AppendFormat("ExpandColumn: '{0}',", grid.ExpandColumn);
                builder.Append("ExpandColClick: 'true',");                
                //,level_field:'level',leaf_field:'isLeaf',expanded_field:'expanded'
                //builder.Append("treeReader:{parent_id_field:'" + grid.ParentIdField + "'},");
                builder.Append("treeReader:{level_field:'level',parent_id_field:'parent_id','leaf_field':'isLeaf',expanded_field:'expanded'},");

            }
            //end tree
            builder.AppendFormat("url: '{0}'", str7);
            builder.AppendFormat(",editurl: '{0}'", str8);
            if (grid.TreeGrid)
            {
                builder.AppendFormat(",mtype: 'POST'", new object[0]);
            }
            else
            {
                builder.AppendFormat(",mtype: 'GET'", new object[0]);
            }
            builder.AppendFormat(",datatype: 'json'", new object[0]);
            builder.AppendFormat(",page: {0}", grid.PagerSettings.CurrentPage);
            builder.AppendFormat(",colNames: {0}", this.GetColNames(grid));
            builder.AppendFormat(",colModel: {0}", this.GetColModel(grid));
            builder.AppendFormat(",viewrecords: true", new object[0]);
            builder.AppendFormat(",scrollrows: true", new object[0]);
            builder.AppendFormat(",prmNames: {{ id: \"{0}\" }}", Util.GetPrimaryKeyField(grid));
            if (grid.AppearanceSettings.ShowFooter)
            {
                builder.Append(",footerrow: true");
                builder.Append(",userDataOnFooter: true");
            }
            if (!grid.AppearanceSettings.ShrinkToFit)
            {
                builder.Append(",shrinkToFit: false");
            }
            if (grid.ColumnReordering)
            {
                builder.Append(",sortable: true");
            }
            if (grid.AppearanceSettings.ScrollBarOffset != 0x12)
            {
                builder.AppendFormat(",scrollOffset: {0}", grid.AppearanceSettings.ScrollBarOffset);
            }
            if (grid.AppearanceSettings.RightToLeft)
            {
                builder.Append(",direction: 'rtl'");
            }
            if (grid.AutoWidth)
            {
                builder.Append(",autowidth: true");
            }
            if (!grid.ShrinkToFit)
            {
                builder.Append(",shrinkToFit: false");
            }
            if ((grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Bottom) || (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom))
            {
                builder.AppendFormat(",pager: {0}", str2);
            }
            if ((grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Top) || (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom))
            {
                builder.Append(",toppager: true");
            }
            if (grid.RenderingMode == RenderingMode.Optimized)
            {
                if (grid.HierarchySettings.HierarchyMode != HierarchyMode.None)
                {
                    throw new Exception("Optimized rendering is not compatible with hierarchy.");
                }
                builder.Append(",gridview: true");
            }
            if ((grid.HierarchySettings.HierarchyMode == HierarchyMode.Parent) || (grid.HierarchySettings.HierarchyMode == HierarchyMode.ParentAndChild))
            {
                builder.Append(",subGrid: true");
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.SubGridRowExpanded))
            {
                builder.AppendFormat(",subGridRowExpanded: {0}", grid.ClientSideEvents.SubGridRowExpanded);
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.ServerError))
            {
                builder.AppendFormat(",errorCell: {0}", grid.ClientSideEvents.ServerError);
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowSelect))
            {
                builder.AppendFormat(",onSelectRow: {0}", grid.ClientSideEvents.RowSelect);
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.ColumnSort))
            {
                builder.AppendFormat(",onSortCol: {0}", grid.ClientSideEvents.ColumnSort);
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowDoubleClick))
            {
                builder.AppendFormat(",ondblClickRow: {0}", grid.ClientSideEvents.RowDoubleClick);
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowRightClick))
            {
                builder.AppendFormat(",onRightClickRow: {0}", grid.ClientSideEvents.RowRightClick);
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.LoadDataError))
            {
                builder.AppendFormat(",loadError: {0}", grid.ClientSideEvents.LoadDataError);
            }
            else
            {
                builder.AppendFormat(",loadError: {0}", "jqGrid_aspnet_loadErrorHandler");
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.GridInitialized))
            {
                builder.AppendFormat(",gridComplete: {0}", grid.ClientSideEvents.GridInitialized);
            }
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.BeforeAjaxRequest))
            {
                builder.AppendFormat(",gridComplete: {0}", grid.ClientSideEvents.BeforeAjaxRequest);
            }
            if (!grid.AppearanceSettings.HighlightRowsOnHover)
            {
                builder.Append(",hoverrows: false");
            }
            if (grid.AppearanceSettings.AlternateRowBackground)
            {
                builder.Append(",altRows: true");
            }
            if (grid.AppearanceSettings.ShowRowNumbers)
            {
                builder.Append(",rownumbers: true");
            }
            if (grid.AppearanceSettings.RowNumbersColumnWidth != 0x19)
            {
                builder.AppendFormat(",rownumWidth: {0}", grid.AppearanceSettings.RowNumbersColumnWidth.ToString());
            }
            if (grid.PagerSettings.ScrollBarPaging)
            {
                builder.AppendFormat(",scroll: 1", new object[0]);
            }
            if (!grid.TreeGrid)
            {
                builder.AppendFormat(",rowNum: {0}", grid.PagerSettings.PageSize.ToString());
                builder.AppendFormat(",rowList: {0}", grid.PagerSettings.PageSizeOptions.ToString());
            }
            if (!string.IsNullOrEmpty(grid.PagerSettings.NoRowsMessage))
            {
                builder.AppendFormat(",emptyrecords: '{0}'", grid.PagerSettings.NoRowsMessage.ToString());
            }
            builder.AppendFormat(",editDialogOptions: {0}", this.GetEditOptions(grid));
            builder.AppendFormat(",addDialogOptions: {0}", this.GetAddOptions(grid));
            builder.AppendFormat(",delDialogOptions: {0}", this.GetDelOptions(grid));
            builder.AppendFormat(",searchDialogOptions: {0}", this.GetSearchOptions(grid));
            builder.AppendFormat(",jsonreader: {{ id: \"{0}\" }}", grid.Columns[Util.GetPrimaryKeyIndex(grid)].DataField);
            if (!string.IsNullOrEmpty(grid.SortSettings.InitialSortColumn))
            {
                builder.AppendFormat(",sortname: '{0}'", grid.SortSettings.InitialSortColumn);
            }
            builder.AppendFormat(",sortorder: '{0}'", grid.SortSettings.InitialSortDirection.ToString().ToLower());
            if (grid.MultiSelect)
            {
                builder.Append(",multiselect: true");
                if (grid.MultiSelectMode == MultiSelectMode.SelectOnCheckBoxClickOnly)
                {
                    builder.AppendFormat(",multiboxonly: true", grid.MultiSelect.ToString().ToLower());
                }
                if (grid.MultiSelectKey != MultiSelectKey.None)
                {
                    builder.AppendFormat(",multikey: '{0}'", this.GetMultiKeyString(grid.MultiSelectKey));
                }
            }
            if (!string.IsNullOrEmpty(grid.AppearanceSettings.Caption))
            {
                builder.AppendFormat(",caption: '{0}'", grid.AppearanceSettings.Caption);
            }
            if (!grid.Width.IsEmpty)
            {
                builder.AppendFormat(",width: '{0}'", grid.Width.ToString().Replace("px", ""));
            }
            if (!grid.Height.IsEmpty)
            {
                builder.AppendFormat(",height: '{0}'", grid.Height.ToString().Replace("px", ""));
            }
            if (grid.GroupSettings.GroupFields.Count > 0)
            {
                builder.Append(",grouping:true");
                builder.Append(",groupingView: {");
                builder.AppendFormat("groupField: ['{0}']", grid.GroupSettings.GroupFields[0].DataField);
                builder.AppendFormat(",groupColumnShow: [{0}]", grid.GroupSettings.GroupFields[0].ShowGroupColumn.ToString().ToLower());
                builder.AppendFormat(",groupText: ['{0}']", grid.GroupSettings.GroupFields[0].HeaderText);
                builder.AppendFormat(",groupOrder: ['{0}']", grid.GroupSettings.GroupFields[0].GroupSortDirection.ToString().ToLower());
                builder.AppendFormat(",groupSummary: [{0}]", grid.GroupSettings.GroupFields[0].ShowGroupSummary.ToString().ToLower());
                builder.AppendFormat(",groupCollapse: {0}", grid.GroupSettings.CollapseGroups.ToString().ToLower());
                builder.AppendFormat(",groupDataSorted: true", new object[0]);
                builder.Append("}");
            }
            builder.AppendFormat(",viewsortcols: [{0},'{1}',{2}]", "false", grid.SortSettings.SortIconsPosition.ToString().ToLower(), (grid.SortSettings.SortAction == SortAction.ClickOnHeader) ? "true" : "false");
            builder.AppendFormat("}})\r", new object[0]);
            builder.Append(this.GetToolBarOptions(grid, subGrid, pagerSelectorID));
            builder.Append(this.GetLoadErrorHandler());
            if (grid.ToolBarSettings.ShowSearchToolBar)
            {
                builder.AppendFormat("{0}.filterToolbar();", str);
            }
            //builder.AppendFormat("{0}.jqGrid('bindKeys');", str);
            return builder.ToString();
        }

        private string GetToolBarOptions(JQGrid grid, bool subGrid, string pagerSelectorID)
        {
            StringBuilder builder = new StringBuilder();
            if (!grid.ShowToolBar)
            {
                return string.Empty;
            }
            JsonToolBar bar = new JsonToolBar(grid.ToolBarSettings);
            if (!subGrid)
            {
                builder.AppendFormat(".navGrid('#{0}',{1},{2},{3},{4},{5} )", new object[] { grid.ID + "_pager", new JavaScriptSerializer().Serialize(bar), string.Format("jQuery('#{0}').getGridParam('editDialogOptions')", grid.ID), string.Format("jQuery('#{0}').getGridParam('addDialogOptions')", grid.ID), string.Format("jQuery('#{0}').getGridParam('delDialogOptions')", grid.ID), string.Format("jQuery('#{0}').getGridParam('searchDialogOptions')", grid.ID) });
            }
            else
            {
                builder.AppendFormat(".navGrid('#' + pager_id,{0},{1},{2},{3},{4} )", new object[] { new JavaScriptSerializer().Serialize(bar), "jQuery('#' + subgrid_table_id).getGridParam('editDialogOptions')", "jQuery('#' + subgrid_table_id).getGridParam('addDialogOptions')", "jQuery('#' + subgrid_table_id).getGridParam('delDialogOptions')", "jQuery('#' + subgrid_table_id).getGridParam('searchDialogOptions')" });
            }
            foreach (JQGridToolBarButton button in grid.ToolBarSettings.CustomButtons)
            {
                builder.AppendFormat(".navButtonAdd({0},{1})", pagerSelectorID, new JsonCustomButton(button).Process());
            }
            builder.Append(";");
            return builder.ToString();
        }

        public string RenderHtml(JQGrid grid)
        {
            string format = "<table id='{0}'></table>";
            if (grid.ToolBarSettings.ToolBarPosition != ToolBarPosition.Hidden)
            {
                format = format + "<div id='{0}_pager'></div>";
            }
            //if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
            //{
            //    return "This is a trial version of jqGrid for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
            //}
            if (string.IsNullOrEmpty(grid.ID))
            {
                throw new Exception("You need to set ID for this grid.");
            }
            format = string.Format(format, grid.ID);
            if ((grid.HierarchySettings.HierarchyMode == HierarchyMode.Child) || (grid.HierarchySettings.HierarchyMode == HierarchyMode.ParentAndChild))
            {
                return (format + this.GetChildSubGridJavaScript(grid));
            }
            return (format + this.GetStartupJavascript(grid, false));
        }
    }
}

