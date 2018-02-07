using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
namespace Trirand.Web.Mvc
{
	public class JQGridColumn
	{
		public bool Fixed{get;set;}
		public JQGridColumnFormatter Formatter
		{
			get;
			set;
		}
		public List<JQGridEditClientSideValidator> EditClientSideValidators
		{
			get;
			set;
		}
		public List<JQGridEditFieldAttribute> EditFieldAttributes
		{
			get;
			set;
		}
		public int Width
		{
			get;
			set;
		}
		public bool Sortable
		{
			get;
			set;
		}
		public bool Frozen
		{
			get;
			set;
		}
		public bool Resizable
		{
			get;
			set;
		}
		public bool Editable
		{
			get;
			set;
		}
		public bool PrimaryKey
		{
			get;
			set;
		}
		public EditType EditType
		{
			get;
			set;
		}
		public string EditTypeCustomCreateElement
		{
			get;
			set;
		}
		public string EditTypeCustomGetValue
		{
			get;
			set;
		}
		public string EditorControlID
		{
			get;
			set;
		}
		public List<SelectListItem> EditList
		{
			get;
			set;
		}
		public SearchType SearchType
		{
			get;
			set;
		}
		public List<SearchOperation> SearchOptions
		{
			get;
			set;
		}
		public string SearchControlID
		{
			get;
			set;
		}
		public Type DataType
		{
			get;
			set;
		}
		public List<SelectListItem> SearchList
		{
			get;
			set;
		}
		public bool SearchCaseSensitive
		{
			get;
			set;
		}
		public int EditDialogColumnPosition
		{
			get;
			set;
		}
		public int EditDialogRowPosition
		{
			get;
			set;
		}
		public string EditDialogLabel
		{
			get;
			set;
		}
		public string EditDialogFieldPrefix
		{
			get;
			set;
		}
		public string EditDialogFieldSuffix
		{
			get;
			set;
		}
		public bool EditActionIconsColumn
		{
			get;
			set;
		}
		public EditActionIconsSettings EditActionIconsSettings
		{
			get;
			set;
		}
		public string DataField
		{
			get;
			set;
		}
		public string DataFormatString
		{
			get;
			set;
		}
		public string HeaderText
		{
			get;
			set;
		}
		public TextAlign TextAlign
		{
			get;
			set;
		}
		public bool Visible
		{
			get;
			set;
		}
		public bool Searchable
		{
			get;
			set;
		}
		public bool HtmlEncode
		{
			get;
			set;
		}
		public bool HtmlEncodeFormatString
		{
			get;
			set;
		}
		public bool ConvertEmptyStringToNull
		{
			get;
			set;
		}
		public string NullDisplayText
		{
			get;
			set;
		}
		public SearchOperation SearchToolBarOperation
		{
			get;
			set;
		}
		public string FooterValue
		{
			get;
			set;
		}
		public string CssClass
		{
			get;
			set;
		}
		public GroupSummaryType GroupSummaryType
		{
			get;
			set;
		}
		public string GroupTemplate
		{
			get;
			set;
		}
        public List<DataEvent> DataEvents { get; set; }
        //zhh
        //public SearchDataType SearchDataType { get; set; }
        public int Order { get; set; }
        public string DefaultValue { get; set; }
        public bool Exportable { get; set; }
        public bool Viewable { get; set; }
        public bool Hidedlg { get; set; }
        public string DataUrl { get; set; }
        public string BuildSelect { get; set; }
        public string SerializeColumnData { get; set; }
        public string DataInit { get; set; }
        //public string Complete { get; set; }
        public JQGridColumn()
        {
            this.EditClientSideValidators = new List<JQGridEditClientSideValidator>();
            this.EditFieldAttributes = new List<JQGridEditFieldAttribute>();
            this.Width = 100;
            this.Sortable = true;
            this.Frozen = false;
            this.Resizable = true;
            this.Editable = false;
            this.PrimaryKey = false;
            this.EditType = EditType.TextBox;
            this.EditList = new List<SelectListItem>();
            this.EditTypeCustomCreateElement = "";
            this.EditTypeCustomGetValue = "";
            this.SearchType = SearchType.TextBox;
            this.SearchControlID = "";
            this.SearchToolBarOperation = SearchOperation.IsEqualTo;
            this.SearchList = new List<SelectListItem>();
            this.SearchCaseSensitive = false;
            this.EditDialogColumnPosition = 0;
            this.EditDialogRowPosition = 0;
            this.EditDialogLabel = "";
            this.EditDialogFieldPrefix = "";
            this.EditDialogFieldSuffix = "";
            this.EditActionIconsColumn = false;
            this.EditActionIconsSettings = new EditActionIconsSettings();
            this.EditorControlID = "";
            this.DataField = "";
            this.DataFormatString = "";
            this.HeaderText = "";
            this.TextAlign = TextAlign.Left;
            this.Visible = true;
            this.Searchable = true;
            this.HtmlEncode = true;
            this.HtmlEncodeFormatString = true;
            this.ConvertEmptyStringToNull = true;
            this.NullDisplayText = "";
            this.FooterValue = "";
            this.CssClass = "";
            this.GroupSummaryType = GroupSummaryType.None;
            this.GroupTemplate = "";
            this.Fixed = false;
            this.SearchOptions = new List<SearchOperation>();
            //zhh
            //this.SearchDataType = Mvc.SearchDataType.String;
            //this.DataEvents = new List<DataEvent>();
            this.DataEvents = new List<DataEvent>(){
                            new DataEvent(){Type = DataEventType.KeyPress,Function="EnterKeyPress"},
                        };
            this.DefaultValue = "";
            this.Exportable = false;
            this.Viewable = true;
            this.Hidedlg = false;
            this.DataUrl = "";
            this.BuildSelect = "";
            this.SerializeColumnData = "";
            this.DataInit = "";
            //this.Complete = "";
        }
		internal virtual string FormatDataValue(object dataValue, bool encode)
		{
			if (this.IsNull(dataValue))
			{
				return this.NullDisplayText;
			}
            if (dataValue is DateTime)
            {
                dataValue = ((DateTime)dataValue).ToLocalTime();
            }
			string text = dataValue.ToString();
			string dataFormatString = this.DataFormatString;
			int length = text.Length;
			if (!this.HtmlEncodeFormatString)
			{
				if (length > 0 && encode)
				{
					text = HttpUtility.HtmlEncode(text);
				}
				if (length == 0 && this.ConvertEmptyStringToNull)
				{
					return this.NullDisplayText;
				}
				if (dataFormatString.Length == 0)
				{
					return text;
				}
				if (encode)
				{
					return string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[]
					{
						text
					});
				}
				return string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[]
				{
					dataValue
				});
			}
			else
			{
				if (length == 0 && this.ConvertEmptyStringToNull)
				{
					return this.NullDisplayText;
				}
				if (!string.IsNullOrEmpty(dataFormatString))
				{
					text = string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[]
					{
						dataValue
					});
				}
				if (!string.IsNullOrEmpty(text) && encode)
				{
					text = HttpUtility.HtmlEncode(text);
				}
				return text;
			}
		}
		internal bool IsNull(object value)
		{
			return value == null || Convert.IsDBNull(value);
		}
	}
}
