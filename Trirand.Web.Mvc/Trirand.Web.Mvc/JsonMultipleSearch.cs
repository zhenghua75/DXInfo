namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JsonMultipleSearch
    {
        [CompilerGenerated]
        private string _groupOp_k__BackingField;
        [CompilerGenerated]
        private List<MultipleSearchRule> rules_k__BackingField;

        public string groupOp
        {
            [CompilerGenerated]
            get
            {
                return this._groupOp_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._groupOp_k__BackingField = value;
            }
        }

        public List<MultipleSearchRule> rules
        {
            [CompilerGenerated]
            get
            {
                return this.rules_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.rules_k__BackingField = value;
            }
        }
    }
}

