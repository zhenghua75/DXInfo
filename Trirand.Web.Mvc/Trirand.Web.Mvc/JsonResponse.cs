namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    internal class JsonResponse
    {
        [CompilerGenerated]
        private int _page_k__BackingField;
        [CompilerGenerated]
        private int _records_k__BackingField;
        [CompilerGenerated]
        private JsonRow[] _rows_k__BackingField;
        [CompilerGenerated]
        private int _total_k__BackingField;
        [CompilerGenerated]
        private Hashtable _userdata_k__BackingField;

        public JsonResponse(int currentPage, int totalPagesCount, int totalRowCount, int pageSize, int actualRows, Hashtable userData)
        {
            this.page = currentPage;
            this.total = totalPagesCount;
            this.records = totalRowCount;
            this.rows = new JsonRow[actualRows];
            this.userdata = userData;
        }

        public int page
        {
            [CompilerGenerated]
            get
            {
                return this._page_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._page_k__BackingField = value;
            }
        }

        public int records
        {
            [CompilerGenerated]
            get
            {
                return this._records_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._records_k__BackingField = value;
            }
        }

        public JsonRow[] rows
        {
            [CompilerGenerated]
            get
            {
                return this._rows_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._rows_k__BackingField = value;
            }
        }

        public int total
        {
            [CompilerGenerated]
            get
            {
                return this._total_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._total_k__BackingField = value;
            }
        }

        public Hashtable userdata
        {
            [CompilerGenerated]
            get
            {
                return this._userdata_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._userdata_k__BackingField = value;
            }
        }
    }
}

