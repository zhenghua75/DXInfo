namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    internal class JsonToolBar
    {
        [CompilerGenerated]
        private bool _add_k__BackingField;
        [CompilerGenerated]
        private bool _cloneToTop_k__BackingField;
        [CompilerGenerated]
        private bool _del_k__BackingField;
        [CompilerGenerated]
        private bool _edit_k__BackingField;
        [CompilerGenerated]
        private string _position_k__BackingField;
        [CompilerGenerated]
        private bool _refresh_k__BackingField;
        [CompilerGenerated]
        private bool _search_k__BackingField;
        [CompilerGenerated]
        private bool _view_k__BackingField;

        public JsonToolBar(ToolBarSettings settings)
        {
            this.edit = settings.ShowEditButton;
            //this.edittext = "±à¼­";
            this.add = settings.ShowAddButton;
            this.del = settings.ShowDeleteButton;
            this.search = settings.ShowSearchButton;
            this.refresh = settings.ShowRefreshButton;
            this.view = settings.ShowViewRowDetailsButton;
            //this.viewtext = "²é¿´";
            this.position = settings.ToolBarAlign.ToString().ToLower();
            this.cloneToTop = true;
        }
        //public string edittext { get; set; }
        //public string viewtext { get; set; }
        public bool add
        {
            [CompilerGenerated]
            get
            {
                return this._add_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._add_k__BackingField = value;
            }
        }

        public bool cloneToTop
        {
            [CompilerGenerated]
            get
            {
                return this._cloneToTop_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._cloneToTop_k__BackingField = value;
            }
        }

        public bool del
        {
            [CompilerGenerated]
            get
            {
                return this._del_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._del_k__BackingField = value;
            }
        }

        public bool edit
        {
            [CompilerGenerated]
            get
            {
                return this._edit_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._edit_k__BackingField = value;
            }
        }

        public string position
        {
            [CompilerGenerated]
            get
            {
                return this._position_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._position_k__BackingField = value;
            }
        }

        public bool refresh
        {
            [CompilerGenerated]
            get
            {
                return this._refresh_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._refresh_k__BackingField = value;
            }
        }

        public bool search
        {
            [CompilerGenerated]
            get
            {
                return this._search_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._search_k__BackingField = value;
            }
        }

        public bool view
        {
            [CompilerGenerated]
            get
            {
                return this._view_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._view_k__BackingField = value;
            }
        }
    }
}

