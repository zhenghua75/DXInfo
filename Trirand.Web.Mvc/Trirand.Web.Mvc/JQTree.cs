namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using System.Web.UI.WebControls;

    public class JQTree
    {
        [CompilerGenerated]
        private string _DataUrl_k__BackingField;
        [CompilerGenerated]
        private Unit _Height_k__BackingField;
        [CompilerGenerated]
        private string _ID_k__BackingField;
        [CompilerGenerated]
        private Unit _Width_k__BackingField;

        public JQTree()
        {
            this.ID = "";
            this.DataUrl = "";
            this.Width = Unit.Empty;
            this.Height = Unit.Empty;
        }

        public JsonResult DataBind(List<JQTreeNode> nodes)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = new JavaScriptSerializer().Serialize(this.SerializeNodes(nodes));
            return result;
        }

        private List<object> SerializeNodes(List<JQTreeNode> nodes)
        {
            List<object> list = new List<object>();
            foreach (JQTreeNode node in nodes)
            {
                List<object> item = new List<object>();
                item.Add(node.Text);
                item.Add(node.Value);
                item.Add(node.Url);
                item.Add(Convert.ToInt16(node.Expanded));
                item.Add(Convert.ToInt16(node.Enabled));
                item.Add(this.SerializeNodes(node.Nodes));
                list.Add(item);
            }
            return list;
        }

        public string DataUrl
        {
            [CompilerGenerated]
            get
            {
                return this._DataUrl_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DataUrl_k__BackingField = value;
            }
        }

        public Unit Height
        {
            [CompilerGenerated]
            get
            {
                return this._Height_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Height_k__BackingField = value;
            }
        }

        public string ID
        {
            [CompilerGenerated]
            get
            {
                return this._ID_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ID_k__BackingField = value;
            }
        }

        public Unit Width
        {
            [CompilerGenerated]
            get
            {
                return this._Width_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Width_k__BackingField = value;
            }
        }
    }
}

