using System;
using System.Collections.Generic;
namespace Trirand.Web.Mvc
{
	internal class JsonMultipleSearch
	{
		public string groupOp
		{
			get;
			set;
		}
		public List<MultipleSearchRule> rules
		{
			get;
			set;
		}
	}
}
