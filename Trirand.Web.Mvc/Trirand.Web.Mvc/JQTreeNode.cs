namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JQTreeNode
    {
        [CompilerGenerated]
        private bool _Enabled_k__BackingField;
        [CompilerGenerated]
        private bool _Expanded_k__BackingField;
        [CompilerGenerated]
        private List<JQTreeNode> Nodes_k__BackingField;
        [CompilerGenerated]
        private string _Text_k__BackingField;
        [CompilerGenerated]
        private string _Url_k__BackingField;
        [CompilerGenerated]
        private string _Value_k__BackingField;

        public JQTreeNode()
        {
            this.Text = "";
            this.Value = "";
            this.Nodes = new List<JQTreeNode>();
            this.Expanded = false;
            this.Enabled = true;
            this.Url = "";
        }

        public bool Enabled
        {
            [CompilerGenerated]
            get
            {
                return this._Enabled_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Enabled_k__BackingField = value;
            }
        }

        public bool Expanded
        {
            [CompilerGenerated]
            get
            {
                return this._Expanded_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Expanded_k__BackingField = value;
            }
        }

        public List<JQTreeNode> Nodes
        {
            [CompilerGenerated]
            get
            {
                return this.Nodes_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.Nodes_k__BackingField = value;
            }
        }

        public string Text
        {
            [CompilerGenerated]
            get
            {
                return this._Text_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Text_k__BackingField = value;
            }
        }

        public string Url
        {
            [CompilerGenerated]
            get
            {
                return this._Url_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Url_k__BackingField = value;
            }
        }

        public string Value
        {
            [CompilerGenerated]
            get
            {
                return this._Value_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Value_k__BackingField = value;
            }
        }
    }
}

