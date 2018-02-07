using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	internal class JsonColModel
	{
		private Hashtable _jsonValues;
		private JQGrid _grid;
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
		public JsonColModel(JQGrid grid)
		{
			this._jsonValues = new Hashtable();
			this._grid = grid;
		}
		public JsonColModel(JQGridColumn column, JQGrid grid) : this(grid)
		{
			this.FromColumn(column);
		}
		public void FromColumn(JQGridColumn column)
		{
			this._jsonValues["index"] = (this._jsonValues["name"] = column.DataField);
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
            if (!column.Viewable)
            {
                this._jsonValues["viewable"] = false;
            }
            if (column.Hidedlg)
            {
                this._jsonValues["hidedlg"] = true;
            }            
			if (column.TextAlign != TextAlign.Left)
			{
				this._jsonValues["align"] = column.TextAlign.ToString().ToLower();
			}
			if (!column.Resizable)
			{
				this._jsonValues["resizable"] = false;
			}
			if (column.Frozen)
			{
				this._jsonValues["frozen"] = true;
			}
			if (!string.IsNullOrEmpty(column.CssClass))
			{
				this._jsonValues["classes"] = column.CssClass;
			}
			if (column.Fixed)
			{
				this._jsonValues["fixed"] = true;
			}
			if (column.GroupSummaryType != GroupSummaryType.None)
			{
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
			}
			if (!string.IsNullOrEmpty(column.GroupTemplate))
			{
				this._jsonValues["summaryTpl"] = column.GroupTemplate;
			}
			if (column.Formatter != null || column.EditActionIconsColumn)
			{
				this.ApplyFormatterOptions(column);
			}
			if (column.EditActionIconsColumn)
			{
				this._jsonValues["formatter"] = "actions";
			}
            //zhh
            if (column.DataType != null)
            {
                this._jsonValues["sorttype"] = column.DataType.Name.ToLower();
            }
			if (column.Searchable)
			{
				Hashtable hashtable = new Hashtable();
				if (column.SearchType == SearchType.DropDown)
				{
					this._jsonValues["stype"] = "select";
				}
				if (!column.Visible)
				{
					hashtable["searchhidden"] = true;
				}
				if (column.SearchList.Count<SelectListItem>() > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					int num = 0;
					foreach (SelectListItem current in column.SearchList)
					{
						stringBuilder.AppendFormat("{0}:{1}", current.Value, current.Text);
						num++;
						if (num < column.SearchList.Count<SelectListItem>())
						{
							stringBuilder.Append(";");
						}
						if (current.Selected)
						{
							hashtable["defaultValue"] = current.Value;
						}
					}
					hashtable["value"] = stringBuilder.ToString();
				}
				if (column.SearchType == SearchType.DatePicker || 
                    column.SearchType == SearchType.AutoComplete||
                    column.SearchType==SearchType.DateTimePicker||
                    column.SearchType == SearchType.TimePicker)
				{
					hashtable["dataInit"] = "attachSearchControlsScript" + column.DataField;
				}
				if (column.SearchOptions.Count > 0)
				{
					hashtable["sopt"] = this.GetSearchOptionsArray(column.SearchOptions);
				}
                
				this._jsonValues["searchoptions"] = hashtable;

                Hashtable hashtable4 = new Hashtable();
                if (column.EditClientSideValidators != null)
                {
                    
                    foreach (JQGridEditClientSideValidator current4 in column.EditClientSideValidators)
                    {
                        if (current4 is DateValidator)
                        {
                            hashtable4["date"] = true;
                        }
                        if (current4 is EmailValidator)
                        {
                            hashtable4["email"] = true;
                        }
                        if (current4 is IntegerValidator)
                        {
                            hashtable4["integer"] = true;
                        }
                        if (current4 is MaxValueValidator)
                        {
                            hashtable4["maxValue"] = ((MaxValueValidator)current4).MaxValue;
                        }
                        if (current4 is MinValueValidator)
                        {
                            hashtable4["minValue"] = ((MinValueValidator)current4).MinValue;
                        }
                        if (current4 is NumberValidator)
                        {
                            hashtable4["number"] = true;
                        }
                        if (current4 is SearchRequiredValidator)
                        {
                            hashtable4["required"] = true;
                        }
                        if (current4 is TimeValidator)
                        {
                            hashtable4["time"] = true;
                        }
                        if (current4 is UrlValidator)
                        {
                            hashtable4["url"] = true;
                        }
                        if (current4 is CustomValidator)
                        {
                            hashtable4["custom"] = true;
                            hashtable4["custom_func"] = ((CustomValidator)current4).ValidationFunction;
                        }
                    }
                }
                if (hashtable4.Count > 0)
                {
                    this._jsonValues["searchrules"] = hashtable4;
                }
			}
            Hashtable hashtable2 = new Hashtable();
            if (column.EditList.Count<SelectListItem>() > 0)
            {
                StringBuilder stringBuilder2 = new StringBuilder();
                int num2 = 0;
                foreach (SelectListItem current3 in column.EditList)
                {
                    stringBuilder2.AppendFormat("{0}:{1}", current3.Value, current3.Text);
                    num2++;
                    if (num2 < column.EditList.Count<SelectListItem>())
                    {
                        stringBuilder2.Append(";");
                    }
                }
                hashtable2["value"] = stringBuilder2.ToString();
            }
			if (column.Editable)
			{				
				this._jsonValues["editable"] = true;
				if (column.EditType == EditType.CheckBox)
				{
					hashtable2["value"] = "true:false";
				}
				if (column.EditType != EditType.TextBox)
				{
					this._jsonValues["edittype"] = this.GetEditType(column.EditType);
				}
				if (column.EditType == EditType.CheckBox)
				{
					column.EditList.Clear();
					column.EditList.Add(new SelectListItem
					{
						Value = "true:false"
					});
				}
				if (column.EditType == EditType.Custom)
				{
					Guard.IsNotNullOrEmpty(column.EditTypeCustomCreateElement, "JQGridColumn.EditTypeCustomCreateElement", " should be set to the name of the javascript function creating the element when EditType = EditType.Custom");
					Guard.IsNotNullOrEmpty(column.EditTypeCustomGetValue, "JQGridColumn.EditTypeCustomGetValue", " should be set to the name of the javascript function getting the value from the element when EditType = EditType.Custom");
					hashtable2["custom_element"] = column.EditTypeCustomCreateElement;
					hashtable2["custom_value"] = column.EditTypeCustomGetValue;
				}
				foreach (JQGridEditFieldAttribute current2 in column.EditFieldAttributes)
				{
					hashtable2[current2.Name] = current2.Value;
				}
                if (column.EditType == EditType.DatePicker ||
                    column.EditType == EditType.DateTimePicker ||
                    column.EditType == EditType.TimePicker ||
                    column.EditType == EditType.AutoComplete||
                    column.EditType == EditType.Chosen)
                {
                    hashtable2["dataInit"] = "attachEditControlsScript" + column.DataField;
                }
				
                if (column.DataEvents.Count<DataEvent>()>0)
                {
                    //zhh 20120825
                    List<DataEvent> lde = column.DataEvents.FindAll(f => f.Type != DataEventType.No);
                    if (lde.Count > 0)
                    {
                        Hashtable[] al = new Hashtable[lde.Count];

                        for(int i=0;i<lde.Count;i++)
                        {
                            Hashtable ht = new Hashtable();
                            ht["type"] = GetDateEventType(lde[i].Type);
                            ht["fn"] = lde[i].Function;
                            al[i] = ht;
                        }
                        //string input = new JavaScriptSerializer().Serialize(al);
                        hashtable2["dataEvents"] = al;//RemoveQuotesForJavaScriptMethods(input,lde);
                    }
                }
                if (!string.IsNullOrEmpty(column.DefaultValue))
                {
                    hashtable2["defaultValue"] = column.DefaultValue;
                }
                if (column.EditType == EditType.DropDown)
                {
                    if (!string.IsNullOrEmpty(column.DataUrl))
                    {
                        hashtable2["dataUrl"] = column.DataUrl;                        
                    }
                    if (!string.IsNullOrEmpty(column.BuildSelect))
                    {
                        hashtable2["buildSelect"] = column.BuildSelect;
                    }
                    if (!string.IsNullOrEmpty(column.SerializeColumnData))
                    {
                        hashtable2["postData"] = column.SerializeColumnData;
                    }
                    //if (!string.IsNullOrEmpty(column.Complete))
                    //{
                    //    hashtable2["complete"] = column.Complete;
                    //}
                }
                if (!string.IsNullOrEmpty(column.DataInit))
                {
                    hashtable2["dataInit"] = column.DataInit;
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
					foreach (JQGridEditClientSideValidator current4 in column.EditClientSideValidators)
					{
						if (current4 is DateValidator)
						{
							hashtable4["date"] = true;
						}
						if (current4 is EmailValidator)
						{
							hashtable4["email"] = true;
						}
						if (current4 is IntegerValidator)
						{
							hashtable4["integer"] = true;
						}
						if (current4 is MaxValueValidator)
						{
							hashtable4["maxValue"] = ((MaxValueValidator)current4).MaxValue;
						}
						if (current4 is MinValueValidator)
						{
							hashtable4["minValue"] = ((MinValueValidator)current4).MinValue;
						}
						if (current4 is NumberValidator)
						{
							hashtable4["number"] = true;
						}
						if (current4 is RequiredValidator)
						{
							hashtable4["required"] = true;
						}
						if (current4 is TimeValidator)
						{
							hashtable4["time"] = true;
						}
						if (current4 is UrlValidator)
						{
							hashtable4["url"] = true;
						}
						if (current4 is CustomValidator)
						{
							hashtable4["custom"] = true;
							hashtable4["custom_func"] = ((CustomValidator)current4).ValidationFunction;
						}
					}
				}                
				if (hashtable4.Count > 0)
				{
					this._jsonValues["editrules"] = hashtable4;
				}
			}
            if (hashtable2.Count > 0)
            {
                this._jsonValues["editoptions"] = hashtable2;
            }
		}
		private void ApplyFormatterOptions(JQGridColumn column)
		{
			Hashtable hashtable = new Hashtable();
			if (column.EditActionIconsColumn)
			{
				hashtable["keys"] = column.EditActionIconsSettings.SaveOnEnterKeyPress;
				hashtable["editbutton"] = column.EditActionIconsSettings.ShowEditIcon;
                if (column.EditActionIconsSettings.ShowEditIcon&&!string.IsNullOrEmpty(column.EditActionIconsSettings.onEdit))
                {
                    hashtable["onEdit"] = column.EditActionIconsSettings.onEdit;;
                    //editOptions : {} 
                }
				hashtable["delbutton"] = column.EditActionIconsSettings.ShowDeleteIcon;
                if (column.EditActionIconsSettings.ShowDeleteIcon)
                {
                    Hashtable ht2 = new Hashtable();
                    if (!string.IsNullOrEmpty(column.EditActionIconsSettings.DelErrorTextFormat))
                    {
                        
                        ht2["errorTextFormat"] = column.EditActionIconsSettings.DelErrorTextFormat;
                        
                    }
                    if (!string.IsNullOrEmpty(_grid.ClientSideEvents.SerializeDelData))
                    {
                        ht2["serializeDelData"] = _grid.ClientSideEvents.SerializeDelData;
                    }
                    if (ht2.Count > 0)
                    {
                        hashtable["delOptions"] = ht2;
                    }
                }
			}
			if (column.Formatter != null)
			{
				JQGridColumnFormatter formatter = column.Formatter;
				if (formatter is LinkFormatter)
				{
					LinkFormatter linkFormatter = (LinkFormatter)formatter;
					this._jsonValues["formatter"] = "link";
					if (!string.IsNullOrEmpty(linkFormatter.Target))
					{
						hashtable["target"] = linkFormatter.Target;
					}
				}
				if (formatter is EmailFormatter)
				{
					this._jsonValues["formatter"] = "email";
				}
				if (formatter is IntegerFormatter)
				{
					IntegerFormatter integerFormatter = (IntegerFormatter)formatter;
					this._jsonValues["formatter"] = "integer";
					if (!string.IsNullOrEmpty(integerFormatter.ThousandsSeparator))
					{
						hashtable["thousandsSeparator"] = integerFormatter.ThousandsSeparator;
					}
					if (!string.IsNullOrEmpty(integerFormatter.DefaultValue))
					{
						hashtable["defaultValue"] = integerFormatter.DefaultValue;
					}
				}
				if (formatter is NumberFormatter)
				{
					NumberFormatter numberFormatter = (NumberFormatter)formatter;
					this._jsonValues["formatter"] = "integer";
					if (!string.IsNullOrEmpty(numberFormatter.ThousandsSeparator))
					{
						hashtable["thousandsSeparator"] = numberFormatter.ThousandsSeparator;
					}
					if (!string.IsNullOrEmpty(numberFormatter.DefaultValue))
					{
						hashtable["defaultValue"] = numberFormatter.DefaultValue;
					}
					if (!string.IsNullOrEmpty(numberFormatter.DecimalSeparator))
					{
						hashtable["decimalSeparator"] = numberFormatter.DecimalSeparator;
					}
					if (numberFormatter.DecimalPlaces != -1)
					{
						hashtable["decimalPlaces"] = numberFormatter.DecimalPlaces;
					}
				}
				if (formatter is CurrencyFormatter)
				{
					CurrencyFormatter currencyFormatter = (CurrencyFormatter)formatter;
					this._jsonValues["formatter"] = "currency";
					if (!string.IsNullOrEmpty(currencyFormatter.ThousandsSeparator))
					{
						hashtable["thousandsSeparator"] = currencyFormatter.ThousandsSeparator;
					}
					if (!string.IsNullOrEmpty(currencyFormatter.DefaultValue))
					{
						hashtable["defaultValue"] = currencyFormatter.DefaultValue;
					}
					if (!string.IsNullOrEmpty(currencyFormatter.DecimalSeparator))
					{
						hashtable["decimalSeparator"] = currencyFormatter.DecimalSeparator;
					}
					if (currencyFormatter.DecimalPlaces != -1)
					{
						hashtable["decimalPlaces"] = currencyFormatter.DecimalPlaces;
					}
					if (!string.IsNullOrEmpty(currencyFormatter.Prefix))
					{
						hashtable["prefix"] = currencyFormatter.Prefix;
					}
					if (!string.IsNullOrEmpty(currencyFormatter.Prefix))
					{
						hashtable["suffix"] = currencyFormatter.Suffix;
					}
				}
				if (formatter is CheckBoxFormatter)
				{
					CheckBoxFormatter checkBoxFormatter = (CheckBoxFormatter)formatter;
					this._jsonValues["formatter"] = "checkbox";
					if (checkBoxFormatter.Enabled)
					{
						hashtable["disabled"] = false;
					}
				}
                if (formatter is DropDownFormatter)
                {
                    this._jsonValues["formatter"] = "select";
                }
				if (formatter is CustomFormatter)
				{
					CustomFormatter customFormatter = (CustomFormatter)formatter;
					if (!string.IsNullOrEmpty(customFormatter.FormatFunction))
					{
						this._jsonValues["formatter"] = customFormatter.FormatFunction;
					}
					if (!string.IsNullOrEmpty(customFormatter.UnFormatFunction))
					{
						this._jsonValues["unformat"] = customFormatter.UnFormatFunction;
					}
				}
			}
			if (hashtable.Count > 0)
			{
				this._jsonValues["formatoptions"] = hashtable;
			}
		}

		public static string RemoveQuotesForJavaScriptMethods(string input, JQGrid grid)
		{
			string text = input;
			foreach (JQGridColumn current in grid.Columns)
			{
				if (current.Formatter != null)
				{
					JQGridColumnFormatter formatter = current.Formatter;
					if (formatter is CustomFormatter)
					{
						CustomFormatter customFormatter = (CustomFormatter)formatter;
						string oldValue = string.Format("\"formatter\":\"{0}\"", customFormatter.FormatFunction);
						string newValue = string.Format("\"formatter\":{0}", customFormatter.FormatFunction);
						text = text.Replace(oldValue, newValue);
						oldValue = string.Format("\"unformat\":\"{0}\"", customFormatter.UnFormatFunction);
						newValue = string.Format("\"unformat\":{0}", customFormatter.UnFormatFunction);
						text = text.Replace(oldValue, newValue);
					}
				}
				foreach (JQGridEditClientSideValidator current2 in current.EditClientSideValidators)
				{
					if (current2 is CustomValidator)
					{
						CustomValidator customValidator = (CustomValidator)current2;
						string oldValue2 = string.Format("\"custom_func\":\"{0}\"", customValidator.ValidationFunction);
						string newValue2 = string.Format("\"custom_func\":{0}", customValidator.ValidationFunction);
						text = text.Replace(oldValue2, newValue2);
					}
				}
				if (current.EditType == EditType.Custom)
				{
					string oldValue3 = string.Format("\"custom_element\":\"{0}\"", current.EditTypeCustomCreateElement);
					string newValue3 = string.Format("\"custom_element\":{0}", current.EditTypeCustomCreateElement);
					text = text.Replace(oldValue3, newValue3);
					oldValue3 = string.Format("\"custom_value\":\"{0}\"", current.EditTypeCustomGetValue);
					newValue3 = string.Format("\"custom_value\":{0}", current.EditTypeCustomGetValue);
					text = text.Replace(oldValue3, newValue3);
				}
				if (current.Editable && (
                    current.EditType == EditType.DatePicker || 
                    current.EditType == EditType.DateTimePicker||
                    current.EditType == EditType.TimePicker||
                    current.EditType == EditType.AutoComplete||
                    current.EditType == EditType.Chosen))
				{
					string attachEditorsFunction = GridUtil.GetAttachEditorsFunction(grid, current.EditType.ToString().ToLower(), current.EditorControlID);
					text = text.Replace(string.Concat(new object[]
					{
						'"',
						"attachEditControlsScript",
						current.DataField,
						'"'
					}), attachEditorsFunction);					
				}
				if (current.Searchable && (current.SearchType == SearchType.DatePicker || 
                    current.SearchType == SearchType.AutoComplete||
                    current.SearchType==SearchType.DateTimePicker||
                    current.SearchType== SearchType.TimePicker))
				{
					string attachEditorsFunction2 = GridUtil.GetAttachEditorsFunction(grid, current.SearchType.ToString().ToLower(), current.SearchControlID);
					text = text.Replace(string.Concat(new object[]
					{
						'"',
						"attachSearchControlsScript",
						current.DataField,
						'"'
					}), attachEditorsFunction2);
				}
                if (current.DataEvents.Count > 0)
                {
                    List<DataEvent> lde = current.DataEvents.FindAll(f => f.Type != DataEventType.No);
                    if (lde.Count > 0)
                    {
                        foreach (DataEvent de in lde)
                        {
                            string oldValue = string.Format("\"fn\":\"{0}\"", de.Function);
                            string newValue = string.Format("\"fn\":{0}", de.Function);
                            text = text.Replace(oldValue, newValue);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(current.DataUrl))
                {
                    string oldValue = string.Format("\"dataUrl\":\"{0}\"", current.DataUrl);
                    string newValue = string.Format("\"dataUrl\":{0}", current.DataUrl);
                    text = text.Replace(oldValue, newValue);
                }
                if (!string.IsNullOrEmpty(current.BuildSelect))
                {
                    string oldValue = string.Format("\"buildSelect\":\"{0}\"", current.BuildSelect);
                    string newValue = string.Format("\"buildSelect\":{0}", current.BuildSelect);
                    text = text.Replace(oldValue, newValue);
                }
                if (!string.IsNullOrEmpty(current.SerializeColumnData))
                {
                    string oldValue = string.Format("\"postData\":\"{0}\"", current.SerializeColumnData);
                    string newValue = string.Format("\"postData\":{0}", current.SerializeColumnData);
                    text = text.Replace(oldValue, newValue);
                }
                if (!string.IsNullOrEmpty(current.DataInit))
                {
                    string oldValue = string.Format("\"dataInit\":\"{0}\"", current.DataInit);
                    string newValue = string.Format("\"dataInit\":{0}", current.DataInit);
                    text = text.Replace(oldValue, newValue);
                }
                //if (!string.IsNullOrEmpty(current.Complete))
                //{
                //    string oldValue = string.Format("\"complete\":\"{0}\"", current.Complete);
                //    string newValue = string.Format("\"complete\":{0}", current.Complete);
                //    text = text.Replace(oldValue, newValue);
                //}
                if (current.EditActionIconsColumn)
                {
                    if (current.EditActionIconsSettings.ShowEditIcon&&!string.IsNullOrEmpty(current.EditActionIconsSettings.onEdit))
                    {
                        string oldValue = string.Format("\"onEdit\":\"{0}\"", current.EditActionIconsSettings.onEdit);
                        string newValue = string.Format("\"onEdit\":{0}", current.EditActionIconsSettings.onEdit);
                        text = text.Replace(oldValue, newValue);
                    }
                    if (current.EditActionIconsSettings.ShowDeleteIcon)
                    {
                        if (!string.IsNullOrEmpty(current.EditActionIconsSettings.DelErrorTextFormat))
                        {
                            string oldValue = string.Format("\"errorTextFormat\":\"{0}\"", current.EditActionIconsSettings.DelErrorTextFormat);
                            string newValue = string.Format("\"errorTextFormat\":{0}", current.EditActionIconsSettings.DelErrorTextFormat);
                            text = text.Replace(oldValue, newValue);
                        }
                        if (!string.IsNullOrEmpty(grid.ClientSideEvents.SerializeDelData))
                        {
                            string oldValue = string.Format("\"serializeDelData\":\"{0}\"", grid.ClientSideEvents.SerializeDelData);
                            string newValue = string.Format("\"serializeDelData\":{0}", grid.ClientSideEvents.SerializeDelData);
                            text = text.Replace(oldValue, newValue);
                        }
                    }
                }
			}
            text = text.Replace('"' + "dataInit" + '"', "dataInit");
			return text;
		}
		private ArrayList GetSearchOptionsArray(List<SearchOperation> options)
		{
			ArrayList arrayList = new ArrayList();
			foreach (SearchOperation current in options)
			{
				arrayList.Add(this.GetStringFromSearchOperation(current));
			}
			return arrayList;
		}
		public string GetStringFromSearchOperation(SearchOperation operation)
		{
			switch (operation)
			{
			case SearchOperation.IsEqualTo:
				return "eq";

			case SearchOperation.IsNotEqualTo:
				return "ne";

			case SearchOperation.IsLessThan:
				return "lt";

			case SearchOperation.IsLessOrEqualTo:
				return "le";

			case SearchOperation.IsGreaterThan:
				return "gt";

			case SearchOperation.IsGreaterOrEqualTo:
				return "ge";

			case SearchOperation.IsIn:
				return "in";

			case SearchOperation.IsNotIn:
				return "ni";

			case SearchOperation.BeginsWith:
				return "bw";

			case SearchOperation.DoesNotBeginWith:
				return "bn";

			case SearchOperation.EndsWith:
				return "ew";

			case SearchOperation.DoesNotEndWith:
				return "en";

			case SearchOperation.Contains:
				return "cn";

			case SearchOperation.DoesNotContain:
				return "nc";

			default:
				return "eq";
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
                case EditType.Chosen:
                    return "select";
                //case EditType.Button:
                //    return "button";
            }
			return "text";
		}
        private string GetDateEventType(DataEventType type)
        {
            switch (type)
            {
                case DataEventType.Click:
                    return "click";
                case DataEventType.Change:
                    return "change";
                case DataEventType.KeyPress:
                    return "keypress";
                case DataEventType.KeyUp:
                    return "keyup";
            }
            return "keypress";
        }
	}
}
