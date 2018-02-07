namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    internal class JsonColModel
    {
        private JQGrid _grid;
        private Hashtable _jsonValues;

        public JsonColModel(JQGrid grid)
        {
            this._jsonValues = new Hashtable();
            this._grid = grid;
        }

        public JsonColModel(JQGridColumn column, JQGrid grid) : this(grid)
        {
            this.FromColumn(column);
        }

        private void ApplyFormatterOptions(JQGridColumn column)
        {
            Hashtable hashtable = new Hashtable();
            if (column.EditActionIconsColumn)
            {
                hashtable["keys"] = column.EditActionIconsSettings.SaveOnEnterKeyPress;
                hashtable["editbutton"] = column.EditActionIconsSettings.ShowEditIcon;
                hashtable["delbutton"] = column.EditActionIconsSettings.ShowDeleteIcon;
            }
            if (column.Formatter != null)
            {
                JQGridColumnFormatter formatter = column.Formatter;
                if (formatter is LinkFormatter)
                {
                    LinkFormatter formatter2 = (LinkFormatter) formatter;
                    this._jsonValues["formatter"] = "link";
                    if (!string.IsNullOrEmpty(formatter2.Target))
                    {
                        hashtable["target"] = formatter2.Target;
                    }
                }
                if (formatter is EmailFormatter)
                {
                    this._jsonValues["formatter"] = "email";
                }
                if (formatter is IntegerFormatter)
                {
                    IntegerFormatter formatter3 = (IntegerFormatter) formatter;
                    this._jsonValues["formatter"] = "integer";
                    if (!string.IsNullOrEmpty(formatter3.ThousandsSeparator))
                    {
                        hashtable["thousandsSeparator"] = formatter3.ThousandsSeparator;
                    }
                    if (!string.IsNullOrEmpty(formatter3.DefaultValue))
                    {
                        hashtable["defaultValue"] = formatter3.DefaultValue;
                    }
                }
                if (formatter is NumberFormatter)
                {
                    NumberFormatter formatter4 = (NumberFormatter) formatter;
                    this._jsonValues["formatter"] = "integer";
                    if (!string.IsNullOrEmpty(formatter4.ThousandsSeparator))
                    {
                        hashtable["thousandsSeparator"] = formatter4.ThousandsSeparator;
                    }
                    if (!string.IsNullOrEmpty(formatter4.DefaultValue))
                    {
                        hashtable["defaultValue"] = formatter4.DefaultValue;
                    }
                    if (!string.IsNullOrEmpty(formatter4.DecimalSeparator))
                    {
                        hashtable["decimalSeparator"] = formatter4.DecimalSeparator;
                    }
                    if (formatter4.DecimalPlaces != -1)
                    {
                        hashtable["decimalPlaces"] = formatter4.DecimalPlaces;
                    }
                }
                if (formatter is CurrencyFormatter)
                {
                    CurrencyFormatter formatter5 = (CurrencyFormatter) formatter;
                    this._jsonValues["formatter"] = "currency";
                    if (!string.IsNullOrEmpty(formatter5.ThousandsSeparator))
                    {
                        hashtable["thousandsSeparator"] = formatter5.ThousandsSeparator;
                    }
                    if (!string.IsNullOrEmpty(formatter5.DefaultValue))
                    {
                        hashtable["defaultValue"] = formatter5.DefaultValue;
                    }
                    if (!string.IsNullOrEmpty(formatter5.DecimalSeparator))
                    {
                        hashtable["decimalSeparator"] = formatter5.DecimalSeparator;
                    }
                    if (formatter5.DecimalPlaces != -1)
                    {
                        hashtable["decimalPlaces"] = formatter5.DecimalPlaces;
                    }
                    if (!string.IsNullOrEmpty(formatter5.Prefix))
                    {
                        hashtable["prefix"] = formatter5.Prefix;
                    }
                    if (!string.IsNullOrEmpty(formatter5.Prefix))
                    {
                        hashtable["suffix"] = formatter5.Suffix;
                    }
                }
                if (formatter is CheckBoxFormatter)
                {
                    CheckBoxFormatter formatter6 = (CheckBoxFormatter) formatter;
                    this._jsonValues["formatter"] = "checkbox";
                    if (formatter6.Enabled)
                    {
                        hashtable["disabled"] = false;
                    }
                }
                if (formatter is CustomFormatter)
                {
                    CustomFormatter formatter7 = (CustomFormatter) formatter;
                    if (!string.IsNullOrEmpty(formatter7.FormatFunction))
                    {
                        this._jsonValues["formatter"] = formatter7.FormatFunction;
                    }
                    if (!string.IsNullOrEmpty(formatter7.UnFormatFunction))
                    {
                        this._jsonValues["unformat"] = formatter7.UnFormatFunction;
                    }
                }
            }
            if (hashtable.Count > 0)
            {
                this._jsonValues["formatoptions"] = hashtable;
            }
        }

        public void FromColumn(JQGridColumn column)
        {
            this._jsonValues["index"] = this._jsonValues["name"] = column.DataField;
            if (column.Width != 150)
            {
                this._jsonValues["width"] = column.Width;
            }
            if (!column.Sortable)
            {
                this._jsonValues["sortable"] = false;
            }
            if (column.PrimaryKey)
            {
                this._jsonValues["key"] = true;
            }
            if (!column.Visible)
            {
                this._jsonValues["hidden"] = true;
            }
            if (!column.Searchable)
            {
                this._jsonValues["search"] = false;
            }
            if (column.TextAlign != TextAlign.Left)
            {
                this._jsonValues["align"] = column.TextAlign.ToString().ToLower();
            }
            if (!column.Resizable)
            {
                this._jsonValues["resizable"] = false;
            }
            if (!string.IsNullOrEmpty(column.CssClass))
            {
                this._jsonValues["classes"] = column.CssClass;
            }
            if (column.Fixed)
            {
                this._jsonValues["fixed"] = true;
            }
            switch (column.GroupSummaryType)
            {
                case GroupSummaryType.Min:
                    this._jsonValues["summaryType"] = "min";
                    break;

                case GroupSummaryType.Max:
                    this._jsonValues["summaryType"] = "max";
                    break;

                case GroupSummaryType.Sum:
                    this._jsonValues["summaryType"] = "sum";
                    break;

                case GroupSummaryType.Avg:
                    this._jsonValues["summaryType"] = "avg";
                    break;

                case GroupSummaryType.Count:
                    this._jsonValues["summaryType"] = "count";
                    break;
            }
            if (!string.IsNullOrEmpty(column.GroupTemplate))
            {
                this._jsonValues["summaryTpl"] = column.GroupTemplate;
            }
            if ((column.Formatter != null) || column.EditActionIconsColumn)
            {
                this.ApplyFormatterOptions(column);
            }
            if (column.EditActionIconsColumn)
            {
                this._jsonValues["formatter"] = "actions";
            }
            if (column.Searchable)
            {
                if (column.SearchType == SearchType.DropDown)
                {
                    this._jsonValues["stype"] = "select";
                }
                Hashtable hashtable = new Hashtable();
                if (column.SearchList.Count<SelectListItem>() > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    int num = 0;
                    foreach (SelectListItem item in column.SearchList)
                    {
                        builder.AppendFormat("{0}:{1}", item.Value, item.Text);
                        num++;
                        if (num < column.SearchList.Count<SelectListItem>())
                        {
                            builder.Append(";");
                        }
                    }
                    hashtable["value"] = builder.ToString();
                }
                if ((column.SearchType == SearchType.DatePicker) || (column.SearchType == SearchType.AutoComplete))
                {
                    hashtable["dataInit"] = "attachSearchControlsScript" + column.DataField;
                }
                this._jsonValues["searchoptions"] = hashtable;
            }
            if (column.Editable)
            {
                Hashtable hashtable2 = new Hashtable();
                this._jsonValues["editable"] = true;
                if (column.EditType != EditType.TextBox)
                {
                    this._jsonValues["edittype"] = this.GetEditType(column.EditType);
                }
                if (column.EditType == EditType.CheckBox)
                {
                    column.EditList.Clear();
                    SelectListItem item2 = new SelectListItem();
                    item2.Value = "true:false";
                    column.EditList.Add(item2);
                }
                if (column.EditType == EditType.Custom)
                {
                    Guard.IsNotNullOrEmpty(column.EditTypeCustomCreateElement, "JQGridColumn.EditTypeCustomCreateElement", " should be set to the name of the javascript function creating the element when EditType = EditType.Custom");
                    Guard.IsNotNullOrEmpty(column.EditTypeCustomGetValue, "JQGridColumn.EditTypeCustomGetValue", " should be set to the name of the javascript function getting the value from the element when EditType = EditType.Custom");
                    hashtable2["custom_element"] = column.EditTypeCustomCreateElement;
                    hashtable2["custom_value"] = column.EditTypeCustomGetValue;
                }
                foreach (JQGridEditFieldAttribute attribute in column.EditFieldAttributes)
                {
                    hashtable2[attribute.Name] = attribute.Value;
                }
                if ((column.EditType == EditType.DatePicker) || (column.EditType == EditType.AutoComplete))
                {
                    hashtable2["dataInit"] = "attachEditControlsScript" + column.DataField;
                }
                if (column.EditList.Count<SelectListItem>() > 0)
                {
                    StringBuilder builder2 = new StringBuilder();
                    int num2 = 0;
                    foreach (SelectListItem item3 in column.EditList)
                    {
                        builder2.AppendFormat("{0}:{1}", item3.Value, item3.Text);
                        num2++;
                        if (num2 < column.EditList.Count<SelectListItem>())
                        {
                            builder2.Append(";");
                        }
                    }
                    hashtable2["value"] = builder2.ToString();
                }
                if (hashtable2.Count > 0)
                {
                    this._jsonValues["editoptions"] = hashtable2;
                }
                Hashtable hashtable3 = new Hashtable();
                if (column.EditDialogColumnPosition != 0)
                {
                    hashtable3["colpos"] = column.EditDialogColumnPosition;
                }
                if (column.EditDialogRowPosition != 0)
                {
                    hashtable3["rowpos"] = column.EditDialogRowPosition;
                }
                if (!string.IsNullOrEmpty(column.EditDialogLabel))
                {
                    hashtable3["label"] = column.EditDialogLabel;
                }
                if (!string.IsNullOrEmpty(column.EditDialogFieldPrefix))
                {
                    hashtable3["elmprefix"] = column.EditDialogFieldPrefix;
                }
                if (!string.IsNullOrEmpty(column.EditDialogFieldSuffix))
                {
                    hashtable3["elmsuffix"] = column.EditDialogFieldSuffix;
                }
                if (hashtable3.Count > 0)
                {
                    this._jsonValues["formoptions"] = hashtable3;
                }
                Hashtable hashtable4 = new Hashtable();
                if (!column.Visible && column.Editable)
                {
                    hashtable4["edithidden"] = true;
                }
                if (column.EditClientSideValidators != null)
                {
                    foreach (JQGridEditClientSideValidator validator in column.EditClientSideValidators)
                    {
                        if (validator is DateValidator)
                        {
                            hashtable4["date"] = true;
                        }
                        if (validator is EmailValidator)
                        {
                            hashtable4["email"] = true;
                        }
                        if (validator is IntegerValidator)
                        {
                            hashtable4["integer"] = true;
                        }
                        if (validator is MaxValueValidator)
                        {
                            hashtable4["maxValue"] = ((MaxValueValidator) validator).MaxValue;
                        }
                        if (validator is MinValueValidator)
                        {
                            hashtable4["minValue"] = ((MinValueValidator) validator).MinValue;
                        }
                        if (validator is NumberValidator)
                        {
                            hashtable4["number"] = true;
                        }
                        if (validator is RequiredValidator)
                        {
                            hashtable4["required"] = true;
                        }
                        if (validator is TimeValidator)
                        {
                            hashtable4["time"] = true;
                        }
                        if (validator is UrlValidator)
                        {
                            hashtable4["url"] = true;
                        }
                        if (validator is CustomValidator)
                        {
                            hashtable4["custom"] = true;
                            hashtable4["custom_func"] = ((CustomValidator) validator).ValidationFunction;
                        }
                    }
                }
                if (hashtable4.Count > 0)
                {
                    this._jsonValues["editrules"] = hashtable4;
                }
            }
        }

        private string GetEditType(EditType type)
        {
            switch (type)
            {
                case EditType.CheckBox:
                    return "checkbox";

                case EditType.Custom:
                    return "custom";

                case EditType.DropDown:
                    return "select";

                case EditType.Password:
                    return "password";

                case EditType.TextArea:
                    return "textarea";

                case EditType.TextBox:
                    return "text";
            }
            return "text";
        }

        public static string RemoveQuotesForJavaScriptMethods(string input, JQGrid grid)
        {
            string str = input;
            foreach (JQGridColumn column in grid.Columns)
            {
                if (column.Formatter != null)
                {
                    JQGridColumnFormatter formatter = column.Formatter;
                    if (formatter is CustomFormatter)
                    {
                        CustomFormatter formatter2 = (CustomFormatter) formatter;
                        string oldValue = string.Format("\"formatter\":\"{0}\"", formatter2.FormatFunction);
                        string newValue = string.Format("\"formatter\":{0}", formatter2.FormatFunction);
                        str = str.Replace(oldValue, newValue);
                        oldValue = string.Format("\"unformat\":\"{0}\"", formatter2.UnFormatFunction);
                        newValue = string.Format("\"unformat\":{0}", formatter2.UnFormatFunction);
                        str = str.Replace(oldValue, newValue);
                    }
                }
                foreach (JQGridEditClientSideValidator validator in column.EditClientSideValidators)
                {
                    if (validator is CustomValidator)
                    {
                        CustomValidator validator2 = (CustomValidator) validator;
                        string str4 = string.Format("\"custom_func\":\"{0}\"", validator2.ValidationFunction);
                        string str5 = string.Format("\"custom_func\":{0}", validator2.ValidationFunction);
                        str = str.Replace(str4, str5);
                    }
                }
                if (column.EditType == EditType.Custom)
                {
                    string str6 = string.Format("\"custom_element\":\"{0}\"", column.EditTypeCustomCreateElement);
                    string str7 = string.Format("\"custom_element\":{0}", column.EditTypeCustomCreateElement);
                    str = str.Replace(str6, str7);
                    str6 = string.Format("\"custom_value\":\"{0}\"", column.EditTypeCustomGetValue);
                    str7 = string.Format("\"custom_value\":{0}", column.EditTypeCustomGetValue);
                    str = str.Replace(str6, str7);
                }
                if ((column.Editable && (column.EditType == EditType.DatePicker)) || (column.EditType == EditType.AutoComplete))
                {
                    string str8 = GridUtil.GetAttachEditorsFunction(grid, column.EditType.ToString().ToLower(), column.EditorControlID);
                    str = str.Replace(string.Concat(new object[] { '"', "attachEditControlsScript", column.DataField, '"' }), str8);
                    str = str.Replace('"' + "dataInit" + '"', "dataInit");
                }
                if ((column.Searchable && (column.SearchType == SearchType.DatePicker)) || (column.SearchType == SearchType.AutoComplete))
                {
                    string str9 = GridUtil.GetAttachEditorsFunction(grid, column.SearchType.ToString().ToLower(), column.SearchControlID);
                    str = str.Replace(string.Concat(new object[] { '"', "attachSearchControlsScript", column.DataField, '"' }), str9);
                    str = str.Replace('"' + "dataInit" + '"', "dataInit");
                }
            }
            return str;
        }

        public Hashtable JsonValues
        {
            get
            {
                return this._jsonValues;
            }
            set
            {
                this._jsonValues = value;
            }
        }
    }
}

