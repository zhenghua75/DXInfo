using System;
using System.Text;
namespace Trirand.Web.Mvc
{
	internal class JQChartRenderer
	{
		private JQChart _chart;
		public JQChartRenderer(JQChart chart)
		{
			this._chart = chart;
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
		public string GetStartupJavascript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<div id='{0}' style='height:{1};min-width:{2};clear:both;margin: 0 auto;'></div>", this._chart.ID, this._chart.Height.ToString(), this._chart.Width.ToString());
			stringBuilder.Append("<script type='text/javascript'>");
			stringBuilder.Append("jQuery(document).ready(function() {");
			stringBuilder.Append("var chart = new Highcharts.Chart({");
			stringBuilder.Append(this.GetStartupOptions());
			stringBuilder.Append("});");
			stringBuilder.Append("});");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}
		private string GetStartupOptions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.RenderChartSettings(stringBuilder);
			return stringBuilder.ToString();
		}
		private void RenderChartSettings(StringBuilder s)
		{
			s.AppendFormat("chart:{0}", this._chart.ToJSON());
			s.Append(",credits:{enabled:false}");
			s.AppendFormat(",title:{0}", this._chart.Title.ToJSON());
			s.AppendFormat(",tooltip:{0}", this._chart.ToolTip.ToJSON());
			s.AppendFormat(",subtitle:{0}", this._chart.SubTitle.ToJSON());
			string text = this._chart.XAxis.ToJSON(this._chart);
			string text2 = this._chart.YAxis.ToJSON(this._chart);
			if (!string.IsNullOrEmpty(text))
			{
				s.AppendFormat(",xAxis:{0}", text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				s.AppendFormat(",yAxis:{0}", text2);
			}
			s.AppendFormat(",legend:{0}", this._chart.Legend.ToJSON());
			s.AppendFormat(",exporting:{0}", this._chart.Exporting.ToJSON());
			s.AppendFormat(",plotOptions:{0}", this._chart.PlotOptions.ToJSON(this._chart));
			s.AppendFormat(",series:{0}", this._chart.SeriesToJSON());
			foreach (string text3 in this._chart.ReplaceTable.Keys)
			{
				s.Replace(text3, this._chart.ReplaceTable[text3].ToString());
			}
		}
	}
}
