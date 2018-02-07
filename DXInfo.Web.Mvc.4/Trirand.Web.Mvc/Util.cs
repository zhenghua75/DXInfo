using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	internal static class Util
	{
		internal class SearchArguments
		{
			public string SearchColumn
			{
				get;
				set;
			}
			public string SearchString
			{
				get;
				set;
			}
			public SearchOperation SearchOperation
			{
				get;
				set;
			}
		}
		internal static JsonResponse PrepareJsonResponse(JsonResponse response, JQGrid grid, DataTable dt)
		{
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string[] array = new string[grid.Columns.Count];
				for (int j = 0; j < grid.Columns.Count; j++)
				{
					JQGridColumn jQGridColumn = grid.Columns[j];
					string text = "";
					if (!string.IsNullOrEmpty(jQGridColumn.DataField))
					{
						int ordinal = dt.Columns[jQGridColumn.DataField].Ordinal;
						text = (string.IsNullOrEmpty(jQGridColumn.DataFormatString) ? dt.Rows[i].ItemArray[ordinal].ToString() : jQGridColumn.FormatDataValue(dt.Rows[i].ItemArray[ordinal], jQGridColumn.HtmlEncode));
					}
					array[j] = text;
				}
				string id = array[Util.GetPrimaryKeyIndex(grid)];
				JsonRow jsonRow = new JsonRow();
				jsonRow.id = id;
				jsonRow.cell = array;
				response.rows[i] = jsonRow;
			}
			return response;
		}
		internal static JsonTreeResponse PrepareJsonTreeResponse(JsonTreeResponse response, JQGrid grid, DataTable dt)
		{
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				response.rows[i] = new Hashtable();
				for (int j = 0; j < grid.Columns.Count; j++)
				{
					JQGridColumn jQGridColumn = grid.Columns[j];
					string value = "";
					if (!string.IsNullOrEmpty(jQGridColumn.DataField))
					{
						int ordinal = dt.Columns[jQGridColumn.DataField].Ordinal;
						value = (string.IsNullOrEmpty(jQGridColumn.DataFormatString) ? dt.Rows[i].ItemArray[ordinal].ToString() : jQGridColumn.FormatDataValue(dt.Rows[i].ItemArray[ordinal], jQGridColumn.HtmlEncode));
					}
					response.rows[i].Add(jQGridColumn.DataField, value);
				}
				try
				{
					response.rows[i].Add("tree_level", dt.Rows[i]["tree_level"] as int?);
				}
				catch
				{
				}
				try
				{
					response.rows[i].Add("tree_parent", Convert.ToString(dt.Rows[i]["tree_parent"]));
				}
				catch
				{
				}
				try
				{
					response.rows[i].Add("tree_leaf", dt.Rows[i]["tree_leaf"] as bool?);
				}
				catch
				{
				}
				try
				{
					response.rows[i].Add("tree_expanded", dt.Rows[i]["tree_expanded"] as bool?);
				}
				catch
				{
				}
				try
				{
					response.rows[i].Add("tree_loaded", dt.Rows[i]["tree_loaded"] as bool?);
				}
				catch
				{
				}
				try
				{
					response.rows[i].Add("tree_icon", dt.Rows[i]["tree_icon"] as string);
				}
				catch
				{
				}
			}
			return response;
		}
        internal static JsonResult ConvertToJson(JsonResponse response, JQGrid grid, DataTable dt)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonResult.Data = Util.PrepareJsonResponse(response, grid, dt);
            return jsonResult;
        }
		internal static JsonResult ConvertToTreeJson(JsonTreeResponse response, JQGrid grid, DataTable dt)
		{
			return new JsonResult
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = Util.PrepareJsonTreeResponse(response, grid, dt)
			};
		}
		public static int GetPrimaryKeyIndex(JQGrid grid)
		{
			foreach (JQGridColumn current in grid.Columns)
			{
				if (current.PrimaryKey)
				{
					return grid.Columns.IndexOf(current);
				}
			}
			return 0;
		}
		public static string GetPrimaryKeyField(JQGrid grid)
		{
			int primaryKeyIndex = Util.GetPrimaryKeyIndex(grid);
			return grid.Columns[primaryKeyIndex].DataField;
		}
		public static Hashtable GetFooterInfo(JQGrid grid)
		{
			Hashtable hashtable = new Hashtable();
			if (grid.AppearanceSettings.ShowFooter)
			{
				foreach (JQGridColumn current in grid.Columns)
				{
					hashtable[current.DataField] = current.FooterValue;
				}
			}
			return hashtable;
		}
		public static string GetWhereClause(JQGrid grid, NameValueCollection queryString)
		{
			string text = " && ";
			string text2 = "";
			new Hashtable();
			foreach (JQGridColumn current in grid.Columns)
			{
				string text3 = queryString[current.DataField];
				if (!string.IsNullOrEmpty(text3))
				{
					Util.SearchArguments args = new Util.SearchArguments
					{
						SearchColumn = current.DataField,
						SearchString = text3,
						SearchOperation = current.SearchToolBarOperation
					};
					string str = (text2.Length > 0) ? text : "";
					string str2 = Util.ConstructLinqFilterExpression(grid, args);
					text2 = text2 + str + str2;
				}
			}
			return text2;
		}
		public static string GetWhereClause(JQGrid grid, string searchField, string searchString, string searchOper)
		{
			string text = " && ";
			string text2 = "";
			new Hashtable();
			Util.SearchArguments args = new Util.SearchArguments
			{
				SearchColumn = searchField,
				SearchString = searchString,
				SearchOperation = Util.GetSearchOperationFromString(searchOper)
			};
			string str = (text2.Length > 0) ? text : "";
			string str2 = Util.ConstructLinqFilterExpression(grid, args);
			return text2 + str + str2;
		}
		public static string GetWhereClause(JQGrid grid, string filters)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			JsonMultipleSearch jsonMultipleSearch = javaScriptSerializer.Deserialize<JsonMultipleSearch>(filters);
			string text = "";
			foreach (MultipleSearchRule current in jsonMultipleSearch.rules)
			{
                if (string.IsNullOrEmpty(current.op)) continue;
				Util.SearchArguments args = new Util.SearchArguments
				{
					SearchColumn = current.field,
					SearchString = current.data,
					SearchOperation = Util.GetSearchOperationFromString(current.op)
				};
				string str = (text.Length > 0) ? (" " + jsonMultipleSearch.groupOp + " ") : "";
				text = text + str + Util.ConstructLinqFilterExpression(grid, args);
			}
			return text;
		}
        public static string GetWhereClause(JQGrid grid, string filters, string ignoreFilterField)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            JsonMultipleSearch jsonMultipleSearch = javaScriptSerializer.Deserialize<JsonMultipleSearch>(filters);
            string text = "";
            foreach (MultipleSearchRule current in jsonMultipleSearch.rules)
            {
                if (string.IsNullOrEmpty(current.op)) continue;
                if (!string.IsNullOrWhiteSpace(ignoreFilterField) && current.field == ignoreFilterField) continue;
                Util.SearchArguments args = new Util.SearchArguments
                {
                    SearchColumn = current.field,
                    SearchString = current.data,
                    SearchOperation = Util.GetSearchOperationFromString(current.op)
                };
                string str = (text.Length > 0) ? (" " + jsonMultipleSearch.groupOp + " ") : "";
                text = text + str + Util.ConstructLinqFilterExpression(grid, args);
            }
            return text;
        }
		private static string ConstructLinqFilterExpression(JQGrid grid, Util.SearchArguments args)
		{
			JQGridColumn jQGridColumn = grid.Columns.Find((JQGridColumn c) => c.DataField == args.SearchColumn);
            if (jQGridColumn == null)
            {
                throw new Exception("JqGridColumn √ª”–…Ë÷√");
            }
			if (jQGridColumn.DataType == null)
			{
				throw new DataTypeNotSetException("JQGridColumn.DataType must be set in order to perform search operations.");
			}
			string filterExpressionCompare = (jQGridColumn.DataType == typeof(string)) ? "{0} {1} \"{2}\"" : "{0} {1} {2}";
			if (jQGridColumn.DataType == typeof(DateTime))
			{
                if (!string.IsNullOrEmpty(args.SearchString))
                {
                    DateTime dateTime = DateTime.Parse(args.SearchString);
                    
                    string str = string.Format("({0},{1},{2},{3},{4},{5})", dateTime.Year, dateTime.Month, dateTime.Day,dateTime.Hour,dateTime.Minute,dateTime.Second);
                    filterExpressionCompare = "{0} {1} DateTime" + str;
                }
			}
            //string str2 = string.Format("{0} != null AND ", args.SearchColumn);
            string str2 ="";
            if (jQGridColumn.DataType == typeof(Guid))
            {
                filterExpressionCompare = "{0} {1} Guid(\"{2}\")";
                //str2 = "";//string.Format("{0} != Guid.Empty AND ", args.SearchColumn);
            }
			
			return str2 + Util.GetLinqExpression(filterExpressionCompare, args, jQGridColumn.SearchCaseSensitive, jQGridColumn.DataType);
		}
		internal static string ConstructLinqFilterExpression(JQAutoComplete autoComplete, Util.SearchArguments args)
		{
			Guard.IsNotNull(autoComplete.DataField, "DataField", "must be set in order to perform search operations. If you get this error from search/export method, make sure you setup(initialize) the grid again prior to filtering/exporting.");
			string filterExpressionCompare = "{0} {1} \"{2}\"";
			return Util.GetLinqExpression(filterExpressionCompare, args, false, typeof(string));
		}
		private static string GetLinqExpression(string filterExpressionCompare, Util.SearchArguments args, bool caseSensitive, Type dataType)
		{
			string text = caseSensitive ? args.SearchString : args.SearchString.ToLower();
			string arg = args.SearchColumn;
			if (dataType != null && dataType == typeof(string) && !caseSensitive)
			{
				arg = string.Format("{0}.ToLower()", args.SearchColumn);
			}
			switch (args.SearchOperation)
			{
			case SearchOperation.IsEqualTo:
                    if (string.IsNullOrEmpty(text)) return "1=1";
				return string.Format(filterExpressionCompare, arg, "=", text);

			case SearchOperation.IsNotEqualTo:
				return string.Format(filterExpressionCompare, arg, "<>", text);

			case SearchOperation.IsLessThan:
				return string.Format(filterExpressionCompare, arg, "<", text);

			case SearchOperation.IsLessOrEqualTo:
				return string.Format(filterExpressionCompare, arg, "<=", text);

			case SearchOperation.IsGreaterThan:
				return string.Format(filterExpressionCompare, arg, ">", text);

			case SearchOperation.IsGreaterOrEqualTo:
				return string.Format(filterExpressionCompare, arg, ">=", text);

			case SearchOperation.BeginsWith:
				return string.Format("{0}.StartsWith(\"{1}\")", arg, text);

			case SearchOperation.DoesNotBeginWith:
				return string.Format("!{0}.StartsWith(\"{1}\")", arg, text);

			case SearchOperation.EndsWith:
				return string.Format("{0}.EndsWith(\"{1}\")", arg, text);

			case SearchOperation.DoesNotEndWith:
				return string.Format("!{0}.EndsWith(\"{1}\")", arg, text);

			case SearchOperation.Contains:
				return string.Format("{0}.Contains(\"{1}\")", arg, text);

			case SearchOperation.DoesNotContain:
				return string.Format("!{0}.Contains(\"{1}\")", arg, text);
			}
			throw new Exception("Invalid search operation.");
		}
		private static SearchOperation GetSearchOperationFromString(string searchOperation)
		{
			switch (searchOperation)
			{
			case "eq":
				return SearchOperation.IsEqualTo;

			case "ne":
				return SearchOperation.IsNotEqualTo;

			case "lt":
				return SearchOperation.IsLessThan;

			case "le":
				return SearchOperation.IsLessOrEqualTo;

			case "gt":
				return SearchOperation.IsGreaterThan;

			case "ge":
				return SearchOperation.IsGreaterOrEqualTo;

			case "in":
				return SearchOperation.IsIn;

			case "ni":
				return SearchOperation.IsNotIn;

			case "bw":
				return SearchOperation.BeginsWith;

			case "bn":
				return SearchOperation.DoesNotBeginWith;

			case "ew":
				return SearchOperation.EndsWith;

			case "en":
				return SearchOperation.DoesNotEndWith;

			case "cn":
				return SearchOperation.Contains;

			case "nc":
				return SearchOperation.DoesNotContain;
			}
			throw new Exception("Search operation not known: " + searchOperation);
		}
	}
}
