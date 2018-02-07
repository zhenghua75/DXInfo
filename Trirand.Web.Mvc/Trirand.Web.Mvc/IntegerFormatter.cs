namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class IntegerFormatter : JQGridColumnFormatter
    {
        [CompilerGenerated]
        private string _DefaultValue_k__BackingField;
        [CompilerGenerated]
        private string _ThousandsSeparator_k__BackingField;

        public string DefaultValue
        {
            [CompilerGenerated]
            get
            {
                return this._DefaultValue_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DefaultValue_k__BackingField = value;
            }
        }

        public string ThousandsSeparator
        {
            [CompilerGenerated]
            get
            {
                return this._ThousandsSeparator_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ThousandsSeparator_k__BackingField = value;
            }
        }
    }
}

