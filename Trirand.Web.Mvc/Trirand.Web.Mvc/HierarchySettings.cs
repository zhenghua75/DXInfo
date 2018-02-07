namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class HierarchySettings
    {
        [CompilerGenerated]
        private Trirand.Web.Mvc.HierarchyMode _HierarchyMode_k__BackingField;

        public HierarchySettings()
        {
            this.HierarchyMode = Trirand.Web.Mvc.HierarchyMode.None;
        }

        public Trirand.Web.Mvc.HierarchyMode HierarchyMode
        {
            [CompilerGenerated]
            get
            {
                return this._HierarchyMode_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._HierarchyMode_k__BackingField = value;
            }
        }
    }
}

