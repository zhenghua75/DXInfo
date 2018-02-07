namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Mvc;

    public class JQGridColumn
    {
        [CompilerGenerated]
        private bool _ConvertEmptyStringToNull_k__BackingField;
        [CompilerGenerated]
        private string _CssClass_k__BackingField;
        [CompilerGenerated]
        private string _DataField_k__BackingField;
        [CompilerGenerated]
        private string _DataFormatString_k__BackingField;
        [CompilerGenerated]
        private Type _DataType_k__BackingField;
        [CompilerGenerated]
        private bool _Editable_k__BackingField;
        [CompilerGenerated]
        private bool _EditActionIconsColumn_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.EditActionIconsSettings _EditActionIconsSettings_k__BackingField;
        [CompilerGenerated]
        private List<JQGridEditClientSideValidator> EditClientSideValidators_k__BackingField;
        [CompilerGenerated]
        private int _EditDialogColumnPosition_k__BackingField;
        [CompilerGenerated]
        private string _EditDialogFieldPrefix_k__BackingField;
        [CompilerGenerated]
        private string _EditDialogFieldSuffix_k__BackingField;
        [CompilerGenerated]
        private string _EditDialogLabel_k__BackingField;
        [CompilerGenerated]
        private int _EditDialogRowPosition_k__BackingField;
        [CompilerGenerated]
        private List<JQGridEditFieldAttribute> EditFieldAttributes_k__BackingField;
        [CompilerGenerated]
        private List<SelectListItem> EditList_k__BackingField;
        [CompilerGenerated]
        private string _EditorControlID_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.EditType _EditType_k__BackingField;
        [CompilerGenerated]
        private string _EditTypeCustomCreateElement_k__BackingField;
        [CompilerGenerated]
        private string _EditTypeCustomGetValue_k__BackingField;
        [CompilerGenerated]
        private bool _Fixed_k__BackingField;
        [CompilerGenerated]
        private string _FooterValue_k__BackingField;
        [CompilerGenerated]
        private JQGridColumnFormatter _Formatter_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.GroupSummaryType _GroupSummaryType_k__BackingField;
        [CompilerGenerated]
        private string _GroupTemplate_k__BackingField;
        [CompilerGenerated]
        private string _HeaderText_k__BackingField;
        [CompilerGenerated]
        private bool _HtmlEncode_k__BackingField;
        [CompilerGenerated]
        private bool _HtmlEncodeFormatString_k__BackingField;
        [CompilerGenerated]
        private string _NullDisplayText_k__BackingField;
        [CompilerGenerated]
        private bool _PrimaryKey_k__BackingField;
        [CompilerGenerated]
        private bool _Resizable_k__BackingField;
        [CompilerGenerated]
        private bool _Searchable_k__BackingField;
        [CompilerGenerated]
        private bool _SearchCaseSensitive_k__BackingField;
        [CompilerGenerated]
        private string _SearchControlID_k__BackingField;
        [CompilerGenerated]
        private List<SelectListItem> SearchList_k__BackingField;
        [CompilerGenerated]
        private SearchOperation _SearchToolBarOperation_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.SearchType _SearchType_k__BackingField;
        [CompilerGenerated]
        private bool _Sortable_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.TextAlign _TextAlign_k__BackingField;
        [CompilerGenerated]
        private bool _Visible_k__BackingField;
        [CompilerGenerated]
        private int _Width_k__BackingField;

        public JQGridColumn()
        {
            this.EditClientSideValidators = new List<JQGridEditClientSideValidator>();
            this.EditFieldAttributes = new List<JQGridEditFieldAttribute>();
            this.Width = 150;
            this.Sortable = true;
            this.Resizable = true;
            this.Editable = false;
            this.PrimaryKey = false;
            this.EditType = Trirand.Web.Mvc.EditType.TextBox;
            this.EditList = new List<SelectListItem>();
            this.EditTypeCustomCreateElement = "";
            this.EditTypeCustomGetValue = "";
            this.SearchType = Trirand.Web.Mvc.SearchType.TextBox;
            this.SearchControlID = "";
            this.SearchToolBarOperation = SearchOperation.Contains;
            this.SearchList = new List<SelectListItem>();
            this.SearchCaseSensitive = false;
            this.EditDialogColumnPosition = 0;
            this.EditDialogRowPosition = 0;
            this.EditDialogLabel = "";
            this.EditDialogFieldPrefix = "";
            this.EditDialogFieldSuffix = "";
            this.EditActionIconsColumn = false;
            this.EditActionIconsSettings = new Trirand.Web.Mvc.EditActionIconsSettings();
            this.EditorControlID = "";
            this.DataField = "";
            this.DataFormatString = "";
            this.HeaderText = "";
            this.TextAlign = Trirand.Web.Mvc.TextAlign.Left;
            this.Visible = true;
            this.Searchable = true;
            this.HtmlEncode = true;
            this.HtmlEncodeFormatString = true;
            this.ConvertEmptyStringToNull = true;
            this.NullDisplayText = "";
            this.FooterValue = "";
            this.CssClass = "";
            this.GroupSummaryType = Trirand.Web.Mvc.GroupSummaryType.None;
            this.GroupTemplate = "";
            this.Fixed = false;
        }

        internal virtual string FormatDataValue(object dataValue, bool encode)
        {
            if (this.IsNull(dataValue))
            {
                return this.NullDisplayText;
            }
            string s = dataValue.ToString();
            string dataFormatString = this.DataFormatString;
            int length = s.Length;
            if (!this.HtmlEncodeFormatString)
            {
                if ((length > 0) && encode)
                {
                    s = HttpUtility.HtmlEncode(s);
                }
                if ((length == 0) && this.ConvertEmptyStringToNull)
                {
                    return this.NullDisplayText;
                }
                if (dataFormatString.Length == 0)
                {
                    return s;
                }
                if (encode)
                {
                    return string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[] { s });
                }
                return string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[] { dataValue });
            }
            if ((length == 0) && this.ConvertEmptyStringToNull)
            {
                return this.NullDisplayText;
            }
            if (!string.IsNullOrEmpty(dataFormatString))
            {
                s = string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[] { dataValue });
            }
            if (!string.IsNullOrEmpty(s) && encode)
            {
                s = HttpUtility.HtmlEncode(s);
            }
            return s;
        }

        internal bool IsNull(object value)
        {
            if ((value != null) && !Convert.IsDBNull(value))
            {
                return false;
            }
            return true;
        }

        public bool ConvertEmptyStringToNull
        {
            [CompilerGenerated]
            get
            {
                return this._ConvertEmptyStringToNull_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ConvertEmptyStringToNull_k__BackingField = value;
            }
        }

        public string CssClass
        {
            [CompilerGenerated]
            get
            {
                return this._CssClass_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._CssClass_k__BackingField = value;
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

        public string DataFormatString
        {
            [CompilerGenerated]
            get
            {
                return this._DataFormatString_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DataFormatString_k__BackingField = value;
            }
        }

        public Type DataType
        {
            [CompilerGenerated]
            get
            {
                return this._DataType_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DataType_k__BackingField = value;
            }
        }

        public bool Editable
        {
            [CompilerGenerated]
            get
            {
                return this._Editable_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Editable_k__BackingField = value;
            }
        }

        public bool EditActionIconsColumn
        {
            [CompilerGenerated]
            get
            {
                return this._EditActionIconsColumn_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditActionIconsColumn_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.EditActionIconsSettings EditActionIconsSettings
        {
            [CompilerGenerated]
            get
            {
                return this._EditActionIconsSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditActionIconsSettings_k__BackingField = value;
            }
        }

        public List<JQGridEditClientSideValidator> EditClientSideValidators
        {
            [CompilerGenerated]
            get
            {
                return this.EditClientSideValidators_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.EditClientSideValidators_k__BackingField = value;
            }
        }

        public int EditDialogColumnPosition
        {
            [CompilerGenerated]
            get
            {
                return this._EditDialogColumnPosition_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditDialogColumnPosition_k__BackingField = value;
            }
        }

        public string EditDialogFieldPrefix
        {
            [CompilerGenerated]
            get
            {
                return this._EditDialogFieldPrefix_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditDialogFieldPrefix_k__BackingField = value;
            }
        }

        public string EditDialogFieldSuffix
        {
            [CompilerGenerated]
            get
            {
                return this._EditDialogFieldSuffix_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditDialogFieldSuffix_k__BackingField = value;
            }
        }

        public string EditDialogLabel
        {
            [CompilerGenerated]
            get
            {
                return this._EditDialogLabel_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditDialogLabel_k__BackingField = value;
            }
        }

        public int EditDialogRowPosition
        {
            [CompilerGenerated]
            get
            {
                return this._EditDialogRowPosition_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditDialogRowPosition_k__BackingField = value;
            }
        }

        public List<JQGridEditFieldAttribute> EditFieldAttributes
        {
            [CompilerGenerated]
            get
            {
                return this.EditFieldAttributes_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.EditFieldAttributes_k__BackingField = value;
            }
        }

        public List<SelectListItem> EditList
        {
            [CompilerGenerated]
            get
            {
                return this.EditList_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.EditList_k__BackingField = value;
            }
        }

        public string EditorControlID
        {
            [CompilerGenerated]
            get
            {
                return this._EditorControlID_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditorControlID_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.EditType EditType
        {
            [CompilerGenerated]
            get
            {
                return this._EditType_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditType_k__BackingField = value;
            }
        }

        public string EditTypeCustomCreateElement
        {
            [CompilerGenerated]
            get
            {
                return this._EditTypeCustomCreateElement_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditTypeCustomCreateElement_k__BackingField = value;
            }
        }

        public string EditTypeCustomGetValue
        {
            [CompilerGenerated]
            get
            {
                return this._EditTypeCustomGetValue_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditTypeCustomGetValue_k__BackingField = value;
            }
        }

        public bool Fixed
        {
            [CompilerGenerated]
            get
            {
                return this._Fixed_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Fixed_k__BackingField = value;
            }
        }

        public string FooterValue
        {
            [CompilerGenerated]
            get
            {
                return this._FooterValue_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._FooterValue_k__BackingField = value;
            }
        }

        public JQGridColumnFormatter Formatter
        {
            [CompilerGenerated]
            get
            {
                return this._Formatter_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Formatter_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.GroupSummaryType GroupSummaryType
        {
            [CompilerGenerated]
            get
            {
                return this._GroupSummaryType_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._GroupSummaryType_k__BackingField = value;
            }
        }

        public string GroupTemplate
        {
            [CompilerGenerated]
            get
            {
                return this._GroupTemplate_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._GroupTemplate_k__BackingField = value;
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

        public bool HtmlEncode
        {
            [CompilerGenerated]
            get
            {
                return this._HtmlEncode_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._HtmlEncode_k__BackingField = value;
            }
        }

        public bool HtmlEncodeFormatString
        {
            [CompilerGenerated]
            get
            {
                return this._HtmlEncodeFormatString_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._HtmlEncodeFormatString_k__BackingField = value;
            }
        }

        public string NullDisplayText
        {
            [CompilerGenerated]
            get
            {
                return this._NullDisplayText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._NullDisplayText_k__BackingField = value;
            }
        }

        public bool PrimaryKey
        {
            [CompilerGenerated]
            get
            {
                return this._PrimaryKey_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PrimaryKey_k__BackingField = value;
            }
        }

        public bool Resizable
        {
            [CompilerGenerated]
            get
            {
                return this._Resizable_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Resizable_k__BackingField = value;
            }
        }

        public bool Searchable
        {
            [CompilerGenerated]
            get
            {
                return this._Searchable_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Searchable_k__BackingField = value;
            }
        }

        public bool SearchCaseSensitive
        {
            [CompilerGenerated]
            get
            {
                return this._SearchCaseSensitive_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SearchCaseSensitive_k__BackingField = value;
            }
        }

        public string SearchControlID
        {
            [CompilerGenerated]
            get
            {
                return this._SearchControlID_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SearchControlID_k__BackingField = value;
            }
        }

        public List<SelectListItem> SearchList
        {
            [CompilerGenerated]
            get
            {
                return this.SearchList_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.SearchList_k__BackingField = value;
            }
        }

        public SearchOperation SearchToolBarOperation
        {
            [CompilerGenerated]
            get
            {
                return this._SearchToolBarOperation_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SearchToolBarOperation_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.SearchType SearchType
        {
            [CompilerGenerated]
            get
            {
                return this._SearchType_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SearchType_k__BackingField = value;
            }
        }

        public bool Sortable
        {
            [CompilerGenerated]
            get
            {
                return this._Sortable_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Sortable_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.TextAlign TextAlign
        {
            [CompilerGenerated]
            get
            {
                return this._TextAlign_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._TextAlign_k__BackingField = value;
            }
        }

        public bool Visible
        {
            [CompilerGenerated]
            get
            {
                return this._Visible_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Visible_k__BackingField = value;
            }
        }

        public int Width
        {
            [CompilerGenerated]
            get
            {
                return this._Width_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Width_k__BackingField = value;
            }
        }
    }
}

