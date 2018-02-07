using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
namespace Trirand.Web.Mvc
{
	public class JQTreeView
	{
		public string ID
		{
			get;
			set;
		}
		public string DataUrl
		{
			get;
			set;
		}
		public Unit Height
		{
			get;
			set;
		}
		public Unit Width
		{
			get;
			set;
		}
		public bool HoverOnMouseOver
		{
			get;
			set;
		}
		public bool CheckBoxes
		{
			get;
			set;
		}
		public bool MultipleSelect
		{
			get;
			set;
		}
		public TreeViewClientSideEvents ClientSideEvents
		{
			get;
			set;
		}
		public JQTreeView()
		{
			this.ID = "";
			this.DataUrl = "";
			this.Width = Unit.Empty;
			this.Height = Unit.Empty;
			this.HoverOnMouseOver = true;
			this.CheckBoxes = false;
			this.MultipleSelect = false;
			this.ClientSideEvents = new TreeViewClientSideEvents();
		}
		public JsonResult DataBind(List<JQTreeNode> nodes)
		{
			return new JsonResult
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new JavaScriptSerializer().Serialize(this.SerializeNodes(nodes))
			};
		}
		private List<Hashtable> SerializeNodes(List<JQTreeNode> nodes)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (JQTreeNode current in nodes)
			{
				list.Add(current.ToHashtable());
			}
			return list;
		}
		public List<JQTreeNode> GetAllNodesFlat(List<JQTreeNode> nodes)
		{
			List<JQTreeNode> list = new List<JQTreeNode>();
			foreach (JQTreeNode current in nodes)
			{
				list.Add(current);
				if (current.Nodes.Count > 0)
				{
					this.GetNodesFlat(current.Nodes, list);
				}
			}
			return list;
		}
		private void GetNodesFlat(List<JQTreeNode> nodes, List<JQTreeNode> result)
		{
			foreach (JQTreeNode current in nodes)
			{
				result.Add(current);
				if (current.Nodes.Count > 0)
				{
					this.GetNodesFlat(current.Nodes, result);
				}
			}
		}
	}
}
