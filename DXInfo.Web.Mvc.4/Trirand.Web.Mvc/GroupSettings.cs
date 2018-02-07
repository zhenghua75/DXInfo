using System;
using System.Collections.Generic;
namespace Trirand.Web.Mvc
{
	public sealed class GroupSettings
	{
		public List<GroupField> GroupFields
		{
			get;
			set;
		}
		public bool CollapseGroups
		{
			get;
			set;
		}
		public GroupSettings()
		{
			this.CollapseGroups = false;
			this.GroupFields = new List<GroupField>();
		}
	}
}
