namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class PagerSettings
    {
        [CompilerGenerated]
        private int _CurrentPage_k__BackingField;
        [CompilerGenerated]
        private string _NoRowsMessage_k__BackingField;
        [CompilerGenerated]
        private int _PageSize_k__BackingField;
        [CompilerGenerated]
        private string _PageSizeOptions_k__BackingField;
        [CompilerGenerated]
        private string _PagingMessage_k__BackingField;
        [CompilerGenerated]
        private bool _ScrollBarPaging_k__BackingField;

        public PagerSettings()
        {
            this.PageSize = 10;
            this.CurrentPage = 1;
            this.PageSizeOptions = "[10,20,30,50,100]";
            this.NoRowsMessage = "";
            this.ScrollBarPaging = false;
            this.PagingMessage = "";
        }

        public int CurrentPage
        {
            [CompilerGenerated]
            get
            {
                return this._CurrentPage_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._CurrentPage_k__BackingField = value;
            }
        }

        public string NoRowsMessage
        {
            [CompilerGenerated]
            get
            {
                return this._NoRowsMessage_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._NoRowsMessage_k__BackingField = value;
            }
        }

        public int PageSize
        {
            [CompilerGenerated]
            get
            {
                return this._PageSize_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PageSize_k__BackingField = value;
            }
        }

        public string PageSizeOptions
        {
            [CompilerGenerated]
            get
            {
                return this._PageSizeOptions_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PageSizeOptions_k__BackingField = value;
            }
        }

        public string PagingMessage
        {
            [CompilerGenerated]
            get
            {
                return this._PagingMessage_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PagingMessage_k__BackingField = value;
            }
        }

        public bool ScrollBarPaging
        {
            [CompilerGenerated]
            get
            {
                return this._ScrollBarPaging_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ScrollBarPaging_k__BackingField = value;
            }
        }
    }
}

