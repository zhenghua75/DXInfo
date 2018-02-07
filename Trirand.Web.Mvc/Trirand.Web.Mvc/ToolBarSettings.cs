namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ToolBarSettings
    {
        [CompilerGenerated]
        private List<JQGridToolBarButton> CustomButtons_k__BackingField;
        [CompilerGenerated]
        private bool _ShowAddButton_k__BackingField;
        [CompilerGenerated]
        private bool _ShowDeleteButton_k__BackingField;
        [CompilerGenerated]
        private bool _ShowEditButton_k__BackingField;
        [CompilerGenerated]
        private bool _ShowRefreshButton_k__BackingField;
        [CompilerGenerated]
        private bool _ShowSearchButton_k__BackingField;
        [CompilerGenerated]
        private bool _ShowSearchToolBar_k__BackingField;
        [CompilerGenerated]
        private bool _ShowViewRowDetailsButton_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.ToolBarAlign _ToolBarAlign_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.ToolBarPosition _ToolBarPosition_k__BackingField;

        public ToolBarSettings()
        {
            this.ShowEditButton = false;
            this.ShowAddButton = false;
            this.ShowDeleteButton = false;
            this.ShowSearchButton = false;
            this.ShowRefreshButton = false;
            this.ShowViewRowDetailsButton = false;
            this.ShowSearchToolBar = false;
            this.ToolBarAlign = Trirand.Web.Mvc.ToolBarAlign.Left;
            this.ToolBarPosition = Trirand.Web.Mvc.ToolBarPosition.Bottom;
            this.CustomButtons = new List<JQGridToolBarButton>();
        }
        //public string EditText { get; set; }
        //public string ViewText { get; set; }
        public List<JQGridToolBarButton> CustomButtons
        {
            [CompilerGenerated]
            get
            {
                return this.CustomButtons_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.CustomButtons_k__BackingField = value;
            }
        }

        public bool ShowAddButton
        {
            [CompilerGenerated]
            get
            {
                return this._ShowAddButton_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowAddButton_k__BackingField = value;
            }
        }

        public bool ShowDeleteButton
        {
            [CompilerGenerated]
            get
            {
                return this._ShowDeleteButton_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowDeleteButton_k__BackingField = value;
            }
        }

        public bool ShowEditButton
        {
            [CompilerGenerated]
            get
            {
                return this._ShowEditButton_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowEditButton_k__BackingField = value;
            }
        }

        public bool ShowRefreshButton
        {
            [CompilerGenerated]
            get
            {
                return this._ShowRefreshButton_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowRefreshButton_k__BackingField = value;
            }
        }

        public bool ShowSearchButton
        {
            [CompilerGenerated]
            get
            {
                return this._ShowSearchButton_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowSearchButton_k__BackingField = value;
            }
        }

        public bool ShowSearchToolBar
        {
            [CompilerGenerated]
            get
            {
                return this._ShowSearchToolBar_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowSearchToolBar_k__BackingField = value;
            }
        }

        public bool ShowViewRowDetailsButton
        {
            [CompilerGenerated]
            get
            {
                return this._ShowViewRowDetailsButton_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowViewRowDetailsButton_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.ToolBarAlign ToolBarAlign
        {
            [CompilerGenerated]
            get
            {
                return this._ToolBarAlign_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ToolBarAlign_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.ToolBarPosition ToolBarPosition
        {
            [CompilerGenerated]
            get
            {
                return this._ToolBarPosition_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ToolBarPosition_k__BackingField = value;
            }
        }
    }
}

