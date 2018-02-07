namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class GroupField
    {
        [CompilerGenerated]
        private string _DataField_k__BackingField;
        [CompilerGenerated]
        private SortDirection _GroupSortDirection_k__BackingField;
        [CompilerGenerated]
        private string _HeaderText_k__BackingField;
        [CompilerGenerated]
        private bool _ShowGroupColumn_k__BackingField;
        [CompilerGenerated]
        private bool _ShowGroupSummary_k__BackingField;

        public GroupField()
        {
            this.DataField = "";
            this.HeaderText = "<b>{0}</b>";
            this.ShowGroupColumn = true;
            this.GroupSortDirection = SortDirection.Asc;
            this.ShowGroupSummary = false;
        }

        public string DataField
        {
            [CompilerGenerated]
            get
            {
                return this._DataField_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DataField_k__BackingField = value;
            }
        }

        public SortDirection GroupSortDirection
        {
            [CompilerGenerated]
            get
            {
                return this._GroupSortDirection_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._GroupSortDirection_k__BackingField = value;
            }
        }

        public string HeaderText
        {
            [CompilerGenerated]
            get
            {
                return this._HeaderText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._HeaderText_k__BackingField = value;
            }
        }

        public bool ShowGroupColumn
        {
            [CompilerGenerated]
            get
            {
                return this._ShowGroupColumn_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowGroupColumn_k__BackingField = value;
            }
        }

        public bool ShowGroupSummary
        {
            [CompilerGenerated]
            get
            {
                return this._ShowGroupSummary_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowGroupSummary_k__BackingField = value;
            }
        }
    }
}

