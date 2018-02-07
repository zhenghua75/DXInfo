namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class CustomFormatter : JQGridColumnFormatter
    {
        [CompilerGenerated]
        private string _FormatFunction_k__BackingField;
        [CompilerGenerated]
        private string _UnFormatFunction_k__BackingField;

        public string FormatFunction
        {
            [CompilerGenerated]
            get
            {
                return this._FormatFunction_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._FormatFunction_k__BackingField = value;
            }
        }

        public string UnFormatFunction
        {
            [CompilerGenerated]
            get
            {
                return this._UnFormatFunction_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._UnFormatFunction_k__BackingField = value;
            }
        }
    }
}

