using System;
using System.ComponentModel;
using System.Web.Mvc;
namespace Trirand.Web.Mvc
{
	public class TrirandNamespace
	{
        public MvcHtmlString JQGrid(JQGrid grid, string id)
        {
            JQGridRenderer jQGridRenderer = new JQGridRenderer();
            grid.ID = id;
            if (grid.ToolBarSettings.ShowExcelButton)
            {
                grid.ToolBarSettings.CustomButtons.Add(
                    new JQGridToolBarButton()
                {
                    Text = "<span class='ui-pg-button-text'>导出</span>",
                    ToolTip = "导出EXCEL",
                    ButtonIcon = "ui-icon-extlink",
                    Position = ToolBarButtonPosition.Last,
                    OnClick = "excelExport",
                });
            }
            if (grid.ToolBarSettings.ShowColumnChooser)
            {
                grid.ToolBarSettings.CustomButtons.Add(
                    new JQGridToolBarButton()
                {
                    Text = "<span class='ui-pg-button-text'>选择列</span>",
                    ToolTip = "选择列",
                    ButtonIcon = "ui-icon-calculator",
                    OnClick = "columnChooser",
                    Position = ToolBarButtonPosition.Last,
                });
            }
            return MvcHtmlString.Create(jQGridRenderer.RenderHtml(grid));
        }
		public MvcHtmlString JQTreeView(JQTreeView tree, string id)
		{
			JQTreeViewRenderer jQTreeViewRenderer = new JQTreeViewRenderer(tree);
			tree.ID = id;
			return MvcHtmlString.Create(jQTreeViewRenderer.RenderHtml());
		}
		public MvcHtmlString JQDropDownList(JQDropDownList dropDownList, string id)
		{
			JQDropDownListRenderer jQDropDownListRenderer = new JQDropDownListRenderer(dropDownList);
			dropDownList.ID = id;
			return MvcHtmlString.Create(jQDropDownListRenderer.RenderHtml());
		}
		public MvcHtmlString JQMultiSelect(JQMultiSelect multiSelect, string id)
		{
			JQMultiSelectRenderer jQMultiSelectRenderer = new JQMultiSelectRenderer(multiSelect);
			multiSelect.ID = id;
			return MvcHtmlString.Create(jQMultiSelectRenderer.RenderHtml());
		}
		public MvcHtmlString JQDatePicker(JQDatePicker datePicker, string id)
		{
			JQDatePickerRenderer jQDatePickerRenderer = new JQDatePickerRenderer(datePicker);
			datePicker.ID = id;
			return MvcHtmlString.Create(jQDatePickerRenderer.RenderHtml());
		}
        public MvcHtmlString JQDateTimePicker(JQDateTimePicker dateTimePicker, string id)
        {
            JQDateTimePickerRenderer jQDateTimePickerRenderer = new JQDateTimePickerRenderer(dateTimePicker);
            dateTimePicker.ID = id;
            return MvcHtmlString.Create(jQDateTimePickerRenderer.RenderHtml());
        }
        public MvcHtmlString JQTimePicker(JQTimePicker timePicker, string id)
        {
            JQTimePickerRenderer jQTimePickerRenderer = new JQTimePickerRenderer(timePicker);
            timePicker.ID = id;
            return MvcHtmlString.Create(jQTimePickerRenderer.RenderHtml());
        }
		public MvcHtmlString JQAutoComplete(JQAutoComplete autoComplete, string id)
		{
			JQAutoCompleteRenderer jQAutoCompleteRenderer = new JQAutoCompleteRenderer(autoComplete);
			autoComplete.ID = id;
			return MvcHtmlString.Create(jQAutoCompleteRenderer.RenderHtml());
		}
        public MvcHtmlString JQChosen(JQChosen chosen, string id)
        {
            JQChosenRenderer jQChosenRenderer = new JQChosenRenderer(chosen);
            chosen.ID = id;
            return MvcHtmlString.Create(jQChosenRenderer.RenderHtml());
        }
		public MvcHtmlString JQChart(JQChart chart, string id)
		{
			JQChartRenderer jQChartRenderer = new JQChartRenderer(chart);
			chart.ID = id;
			return MvcHtmlString.Create(jQChartRenderer.RenderHtml());
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		private new bool Equals(object value)
		{
			return base.Equals(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		private new int GetHashCode()
		{
			return base.GetHashCode();
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		private new Type GetType()
		{
			return base.GetType();
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		private new string ToString()
		{
			return base.ToString();
		}
	}
}
