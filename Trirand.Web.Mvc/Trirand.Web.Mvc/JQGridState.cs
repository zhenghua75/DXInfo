namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class JQGridState
    {
        [CompilerGenerated]
        private bool _CurrentPageOnly_k__BackingField;
        [CompilerGenerated]
        private NameValueCollection _QueryString_k__BackingField;

        public JQGridState()
        {
            this.QueryString = new NameValueCollection();
            this.CurrentPageOnly = false;
        }

        public bool CurrentPageOnly
        {
            [CompilerGenerated]
            get
            {
                return this._CurrentPageOnly_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._CurrentPageOnly_k__BackingField = value;
            }
        }

        public NameValueCollection QueryString
        {
            [CompilerGenerated]
            get
            {
                return this._QueryString_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._QueryString_k__BackingField = value;
            }
        }
    }
}

