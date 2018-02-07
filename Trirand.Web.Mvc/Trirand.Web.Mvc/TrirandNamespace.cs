namespace Trirand.Web.Mvc
{
    using System;
    using System.ComponentModel;
    using System.Web.Mvc;

    public class TrirandNamespace
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool Equals(object value)
        {
            return base.Equals(value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private int GetHashCode()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private Type GetType()
        {
            return base.GetType();
        }

        public MvcHtmlString JQAutoComplete(Trirand.Web.Mvc.JQAutoComplete autoComplete, string id)
        {
            JQAutoCompleteRenderer renderer = new JQAutoCompleteRenderer(autoComplete);
            autoComplete.ID = id;
            return MvcHtmlString.Create(renderer.RenderHtml());
        }

        public MvcHtmlString JQChart(Trirand.Web.Mvc.JQChart chart, string id)
        {
            JQChartRenderer renderer = new JQChartRenderer(chart);
            chart.ID = id;
            return MvcHtmlString.Create(renderer.RenderHtml());
        }

        public MvcHtmlString JQDatePicker(Trirand.Web.Mvc.JQDatePicker datePicker, string id)
        {
            JQDatePickerRenderer renderer = new JQDatePickerRenderer(datePicker);
            datePicker.ID = id;
            return MvcHtmlString.Create(renderer.RenderHtml());
        }

        public MvcHtmlString JQGrid(Trirand.Web.Mvc.JQGrid grid, string id)
        {
            JQGridRenderer renderer = new JQGridRenderer();
            grid.ID = id;
            return MvcHtmlString.Create(renderer.RenderHtml(grid));
        }

        public MvcHtmlString JQTree(Trirand.Web.Mvc.JQTree tree, string id)
        {
            JQTreeRenderer renderer = new JQTreeRenderer(tree);
            tree.ID = id;
            return MvcHtmlString.Create(renderer.RenderHtml());
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string ToString()
        {
            return base.ToString();
        }
    }
}

