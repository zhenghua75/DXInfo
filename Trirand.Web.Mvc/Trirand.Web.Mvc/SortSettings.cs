namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class SortSettings
    {
        [CompilerGenerated]
        private string _InitialSortColumn_k__BackingField;
        [CompilerGenerated]
        private SortDirection _InitialSortDirection_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.SortAction _SortAction_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.SortIconsPosition _SortIconsPosition_k__BackingField;

        public SortSettings()
        {
            this.InitialSortColumn = "";
            this.InitialSortDirection = SortDirection.Asc;
            this.SortIconsPosition = Trirand.Web.Mvc.SortIconsPosition.Vertical;
            this.SortAction = Trirand.Web.Mvc.SortAction.ClickOnHeader;
        }

        public string InitialSortColumn
        {
            [CompilerGenerated]
            get
            {
                return this._InitialSortColumn_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._InitialSortColumn_k__BackingField = value;
            }
        }

        public SortDirection InitialSortDirection
        {
            [CompilerGenerated]
            get
            {
                return this._InitialSortDirection_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._InitialSortDirection_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.SortAction SortAction
        {
            [CompilerGenerated]
            get
            {
                return this._SortAction_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SortAction_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.SortIconsPosition SortIconsPosition
        {
            [CompilerGenerated]
            get
            {
                return this._SortIconsPosition_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SortIconsPosition_k__BackingField = value;
            }
        }
    }
}

