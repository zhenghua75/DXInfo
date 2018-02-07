namespace Trirand.Web.Mvc
{
    using System;
    using System.Text;

    internal class JQChartRenderer
    {
        private JQChart _chart;

        public JQChartRenderer(JQChart chart)
        {
            this._chart = chart;
        }

        public string GetStartupJavascript()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<div id='{0}' style='height:{1};min-width:{2};clear:both;margin: 0 auto;'></div>", this._chart.ID, this._chart.Height.ToString(), this._chart.Width.ToString());
            builder.Append("<script type='text/javascript'>");
            builder.Append("jQuery(document).ready(function() {");
            builder.Append("var chart = new Highcharts.Chart({");
            builder.Append(this.GetStartupOptions());
            builder.Append("});");
            builder.Append("});");
            builder.Append("</script>");
            return builder.ToString();
        }

        private string GetStartupOptions()
        {
            StringBuilder s = new StringBuilder();
            this.RenderChartSettings(s);
            return s.ToString();
        }

        private void RenderChartSettings(StringBuilder s)
        {
            s.AppendFormat("chart:{0}", this._chart.ToJSON());
            s.Append(",credits:{enabled:false}");
            s.AppendFormat(",title:{0}", this._chart.Title.ToJSON());
            s.AppendFormat(",tooltip:{{{0}}}", this._chart.ToolTip.ToJSON());
            s.AppendFormat(",subtitle:{0}", this._chart.SubTitle.ToJSON());
            s.AppendFormat(",xAxis:{0}", this._chart.XAxis.ToJSON());
            s.AppendFormat(",yAxis:{0}", this._chart.YAxis.ToJSON());
            s.AppendFormat(",legend:{0}", this._chart.Legend.ToJSON());
            s.AppendFormat(",series:{0}", this._chart.SeriesToJSON());
        }

        public string RenderHtml()
        {
            if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
            {
                return "This is a trial version of jqSuite for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
            }
            Guard.IsNotNullOrEmpty(this._chart.ID, "ID", "You need to set ID for this JQChart instance.");
            return this.GetStartupJavascript();
        }
    }
}

