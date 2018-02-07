namespace Trirand.Web.Mvc
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Mvc;

    public class JQAutoComplete
    {
        [CompilerGenerated]
        private Trirand.Web.Mvc.AutoCompleteMode _AutoCompleteMode_k__BackingField;
        [CompilerGenerated]
        private string _DataField_k__BackingField;
        [CompilerGenerated]
        private object _DataSource_k__BackingField;
        [CompilerGenerated]
        private string _DataUrl_k__BackingField;
        [CompilerGenerated]
        private int _Delay_k__BackingField;
        [CompilerGenerated]
        private AutoCompleteDisplayMode _DisplayMode_k__BackingField;
        [CompilerGenerated]
        private bool _Enabled_k__BackingField;
        [CompilerGenerated]
        private string _ID_k__BackingField;
        [CompilerGenerated]
        private int _MinLength_k__BackingField;

        public JQAutoComplete()
        {
            this.AutoCompleteMode = Trirand.Web.Mvc.AutoCompleteMode.BeginsWith;
            this.DataField = "";
            this.DataSource = null;
            this.DataUrl = "";
            this.Delay = 300;
            this.DisplayMode = AutoCompleteDisplayMode.Standalone;
            this.Enabled = true;
            this.ID = "";
            this.MinLength = 1;
        }

        public JsonResult DataBind()
        {
            return this.GetJsonResponse();
        }

        public JsonResult DataBind(object dataSource)
        {
            this.DataSource = dataSource;
            return this.DataBind();
        }

        private JsonResult GetJsonResponse()
        {
            Guard.IsNotNull(this.DataSource, "DataSource");
            IQueryable dataSource = this.DataSource as IQueryable;
            Guard.IsNotNull(dataSource, "DataSource", "should implement the IQueryable interface.");
            Guard.IsNotNullOrEmpty(this.DataField, "DataField", "should be set to the datafield (column) of the datasource to search in.");
            SearchOperation isEqualTo = SearchOperation.IsEqualTo;
            if (this.AutoCompleteMode == Trirand.Web.Mvc.AutoCompleteMode.BeginsWith)
            {
                isEqualTo = SearchOperation.BeginsWith;
            }
            else
            {
                isEqualTo = SearchOperation.Contains;
            }
            string str = HttpContext.Current.Request.QueryString["term"];
            if (!string.IsNullOrEmpty(str))
            {
                Util.SearchArguments args = new Util.SearchArguments();
                args.SearchColumn = this.DataField;
                args.SearchOperation = isEqualTo;
                args.SearchString = str;
                dataSource = dataSource.Where(Util.ConstructLinqFilterExpression(this, args), new object[0]);
            }
            JsonResult result2 = new JsonResult();
            result2.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result2.Data = dataSource.ToListOfString(this);
            return result2;
        }

        public Trirand.Web.Mvc.AutoCompleteMode AutoCompleteMode
        {
            [CompilerGenerated]
            get
            {
                return this._AutoCompleteMode_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AutoCompleteMode_k__BackingField = value;
            }
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

        public object DataSource
        {
            [CompilerGenerated]
            get
            {
                return this._DataSource_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DataSource_k__BackingField = value;
            }
        }

        public string DataUrl
        {
            [CompilerGenerated]
            get
            {
                return this._DataUrl_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DataUrl_k__BackingField = value;
            }
        }

        public int Delay
        {
            [CompilerGenerated]
            get
            {
                return this._Delay_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Delay_k__BackingField = value;
            }
        }

        public AutoCompleteDisplayMode DisplayMode
        {
            [CompilerGenerated]
            get
            {
                return this._DisplayMode_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DisplayMode_k__BackingField = value;
            }
        }

        public bool Enabled
        {
            [CompilerGenerated]
            get
            {
                return this._Enabled_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Enabled_k__BackingField = value;
            }
        }

        public string ID
        {
            [CompilerGenerated]
            get
            {
                return this._ID_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ID_k__BackingField = value;
            }
        }

        public int MinLength
        {
            [CompilerGenerated]
            get
            {
                return this._MinLength_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MinLength_k__BackingField = value;
            }
        }
    }
}

