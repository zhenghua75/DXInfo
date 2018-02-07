namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public sealed class GroupSettings
    {
        [CompilerGenerated]
        private bool _CollapseGroups_k__BackingField;
        [CompilerGenerated]
        private List<GroupField> GroupFields_k__BackingField;

        public GroupSettings()
        {
            this.CollapseGroups = false;
            this.GroupFields = new List<GroupField>();
        }

        public bool CollapseGroups
        {
            [CompilerGenerated]
            get
            {
                return this._CollapseGroups_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._CollapseGroups_k__BackingField = value;
            }
        }

        public List<GroupField> GroupFields
        {
            [CompilerGenerated]
            get
            {
                return this.GroupFields_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.GroupFields_k__BackingField = value;
            }
        }
    }
}

