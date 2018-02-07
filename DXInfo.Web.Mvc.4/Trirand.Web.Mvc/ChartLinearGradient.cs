using System;
namespace Trirand.Web.Mvc
{
	public class ChartLinearGradient
	{
		public string FromX
		{
			get;
			set;
		}
		public string FromY
		{
			get;
			set;
		}
		public string ToX
		{
			get;
			set;
		}
		public string ToY
		{
			get;
			set;
		}
		public ChartLinearGradient()
		{
			this.FromX = "0";
			this.FromY = "0";
			this.ToX = "0";
			this.ToY = "0";
		}
	}
}
