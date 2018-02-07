namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    internal static class Util
    {
        internal static string ConstructLinqFilterExpression(JQAutoComplete autoComplete, SearchArguments args)
        {
            Guard.IsNotNull(autoComplete.DataField, "DataField", "must be set in order to perform search operations.");
            string filterExpressionCompare = "{0} {1} \"{2}\"";
            return GetLinqExpression(filterExpressionCompare, args, false, typeof(string));
        }

        private static string ConstructLinqFilterExpression(JQGrid grid, SearchArguments args)
        {
            JQGridColumn column = grid.Columns.Find(delegate (JQGridColumn c) {
                return c.DataField == args.SearchColumn;
            });
            if (column.DataType == null)
            {
                throw new DataTypeNotSetException("JQGridColumn.DataType must be set in order to perform search operations.");
            }
            string filterExpressionCompare = (column.DataType == typeof(string)) ? "{0} {1} \"{2}\"" : "{0} {1} {2}";
            if (column.DataType == typeof(DateTime))
            {
                DateTime time = DateTime.Parse(args.SearchString);
                string str2 = string.Format("({0},{1},{2})", time.Year, time.Month, time.Day);
                filterExpressionCompare = "{0} {1} DateTime" + str2;
            }
            return (string.Format("{0} != null AND ", args.SearchColumn) + GetLinqExpression(filterExpressionCompare, args, column.SearchCaseSensitive, column.DataType));
        }

        internal static JsonResult ConvertToJson(JsonResponse response, JQGrid grid, DataTable dt)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (response.records == 0)
            {
                result.Data = new object[0];
                return result;
            }
            result.Data = PrepareJsonResponse(response, grid, dt);
            return result;
        }

        internal static string ConvertToJsonString(JsonResponse response, JQGrid grid, DataTable dt)
        {
            return new JavaScriptSerializer().Serialize(PrepareJsonResponse(response, grid, dt));
        }

        public static Hashtable GetFooterInfo(JQGrid grid)
        {
            Hashtable hashtable = new Hashtable();
            if (grid.AppearanceSettings.ShowFooter)
            {
                foreach (JQGridColumn column in grid.Columns)
                {
                    hashtable[column.DataField] = column.FooterValue;
                }
            }
            return hashtable;
        }

        private static string GetLinqExpression(string filterExpressionCompare, SearchArguments args, bool caseSensitive, Type dataType)
        {
            string str = caseSensitive ? args.SearchString : args.SearchString.ToLower();
            string searchColumn = args.SearchColumn;
            if (((dataType != null) && (dataType == typeof(string))) && !caseSensitive)
            {
                searchColumn = string.Format("{0}.ToLower()", args.SearchColumn);
            }
            switch (args.SearchOperation)
            {
                case SearchOperation.IsEqualTo:
                    return string.Format(filterExpressionCompare, searchColumn, "=", str);

                case SearchOperation.IsNotEqualTo:
                    return string.Format(filterExpressionCompare, searchColumn, "<>", str);

                case SearchOperation.IsLessThan:
                    return string.Format(filterExpressionCompare, searchColumn, "<", str);

                case SearchOperation.IsLessOrEqualTo:
                    return string.Format(filterExpressionCompare, searchColumn, "<=", str);

                case SearchOperation.IsGreaterThan:
                    return string.Format(filterExpressionCompare, searchColumn, ">", str);

                case SearchOperation.IsGreaterOrEqualTo:
                    return string.Format(filterExpressionCompare, searchColumn, ">=", str);

                case SearchOperation.BeginsWith:
                    return string.Format("{0}.StartsWith(\"{1}\")", searchColumn, str);

                case SearchOperation.DoesNotBeginWith:
                    return string.Format("!{0}.StartsWith(\"{1}\")", searchColumn, str);

                case SearchOperation.EndsWith:
                    return string.Format("{0}.EndsWith(\"{1}\")", searchColumn, str);

                case SearchOperation.DoesNotEndWith:
                    return string.Format("!{0}.EndsWith(\"{1}\")", searchColumn, str);

                case SearchOperation.Contains:
                    return string.Format("{0}.Contains(\"{1}\")", searchColumn, str);

                case SearchOperation.DoesNotContain:
                    return string.Format("!{0}.Contains(\"{1}\")", searchColumn, str);
            }
            throw new Exception("Invalid search operation.");
        }

        public static string GetPrimaryKeyField(JQGrid grid)
        {
            int primaryKeyIndex = GetPrimaryKeyIndex(grid);
            return grid.Columns[primaryKeyIndex].DataField;
        }

        public static int GetPrimaryKeyIndex(JQGrid grid)
        {
            foreach (JQGridColumn column in grid.Columns)
            {
                if (column.PrimaryKey)
                {
                    return grid.Columns.IndexOf(column);
                }
            }
            return 0;
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

        public static string GetWhereClause(JQGrid grid, NameValueCollection queryString)
        {
            string str = " && ";
            string str2 = "";
            new Hashtable();
            foreach (JQGridColumn column in grid.Columns)
            {
                string str3 = queryString[column.DataField];
                if (!string.IsNullOrEmpty(str3))
                {
                    SearchArguments arguments2 = new SearchArguments();
                    arguments2.SearchColumn = column.DataField;
                    arguments2.SearchString = str3;
                    arguments2.SearchOperation = column.SearchToolBarOperation;
                    SearchArguments args = arguments2;
                    string str4 = (str2.Length > 0) ? str : "";
                    string str5 = ConstructLinqFilterExpression(grid, args);
                    str2 = str2 + str4 + str5;
                }
            }
            return str2;
        }

        public static string GetWhereClause(JQGrid grid, string filters)
        {
            JsonMultipleSearch search = new JavaScriptSerializer().Deserialize<JsonMultipleSearch>(filters);
            string str = "";
            foreach (MultipleSearchRule rule in search.rules)
            {
                SearchArguments arguments2 = new SearchArguments();
                arguments2.SearchColumn = rule.field;
                arguments2.SearchString = rule.data;
                arguments2.SearchOperation = GetSearchOperationFromString(rule.op);
                SearchArguments args = arguments2;
                string str2 = (str.Length > 0) ? (" " + search.groupOp + " ") : "";
                str = str + str2 + ConstructLinqFilterExpression(grid, args);
            }
            return str;
        }

        public static string GetWhereClause(JQGrid grid, string searchField, string searchString, string searchOper)
        {
            string str = " && ";
            string str2 = "";
            new Hashtable();
            SearchArguments arguments2 = new SearchArguments();
            arguments2.SearchColumn = searchField;
            arguments2.SearchString = searchString;
            arguments2.SearchOperation = GetSearchOperationFromString(searchOper);
            SearchArguments args = arguments2;
            string str3 = (str2.Length > 0) ? str : "";
            string str4 = ConstructLinqFilterExpression(grid, args);
            return (str2 + str3 + str4);
        }

        internal static JsonResponse PrepareJsonResponse(JsonResponse response, JQGrid grid, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] strArray = new string[grid.Columns.Count];
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    JQGridColumn column = grid.Columns[j];
                    string str = "";
                    if (!string.IsNullOrEmpty(column.DataField))
                    {
                        int ordinal = dt.Columns[column.DataField].Ordinal;
                        str = string.IsNullOrEmpty(column.DataFormatString) ? dt.Rows[i].ItemArray[ordinal].ToString() : column.FormatDataValue(dt.Rows[i].ItemArray[ordinal], column.HtmlEncode);
                    }
                    strArray[j] = str;
                }
                string str2 = strArray[GetPrimaryKeyIndex(grid)];
                JsonRow row = new JsonRow();
                row.id = str2;
                row.cell = strArray;
                response.rows[i] = row;
            }
            return response;
        }

        public class SearchArguments
        {
            [CompilerGenerated]
            private string _SearchColumn_k__BackingField;
            [CompilerGenerated]
            private Trirand.Web.Mvc.SearchOperation _SearchOperation_k__BackingField;
            [CompilerGenerated]
            private string _SearchString_k__BackingField;

            public string SearchColumn
            {
                [CompilerGenerated]
                get
                {
                    return this._SearchColumn_k__BackingField;
                }
                [CompilerGenerated]
                set
                {
                    this._SearchColumn_k__BackingField = value;
                }
            }

            public Trirand.Web.Mvc.SearchOperation SearchOperation
            {
                [CompilerGenerated]
                get
                {
                    return this._SearchOperation_k__BackingField;
                }
                [CompilerGenerated]
                set
                {
                    this._SearchOperation_k__BackingField = value;
                }
            }

            public string SearchString
            {
                [CompilerGenerated]
                get
                {
                    return this._SearchString_k__BackingField;
                }
                [CompilerGenerated]
                set
                {
                    this._SearchString_k__BackingField = value;
                }
            }
        }
    }
}

