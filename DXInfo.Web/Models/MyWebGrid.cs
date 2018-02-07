using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace DXInfo.Web.Models
{
    public class MyWebGrid:WebGrid
    {
        public MyWebGrid(IEnumerable<dynamic> source=null,IEnumerable<string> columnNames=null,string defaultSort=null,int rowsPerPage=10,bool canPage=true,
            string ajaxUpdateContainerId=null,string ajaxUpdateCallback=null,string fieldNamePrefix=null,
            string pageFieldName=null,string selectionFieldName=null,string sortFieldName=null,string sortDirectionFieldName=null)
            : base(source: source, columnNames: columnNames, defaultSort: defaultSort, rowsPerPage: rowsPerPage, canPage: canPage, canSort: true,
            ajaxUpdateContainerId:ajaxUpdateContainerId,ajaxUpdateCallback:ajaxUpdateCallback,fieldNamePrefix:fieldNamePrefix,
            pageFieldName:pageFieldName,selectionFieldName:selectionFieldName,sortFieldName:sortFieldName,sortDirectionFieldName:sortDirectionFieldName)
        {

        }
        public IHtmlString GetHtml(IEnumerable<WebGridColumn> gridColumns)
        {
            return GetHtml(
            mode: WebGridPagerModes.All,
            numericLinksCount: 50,
            firstText: "首页",
            previousText: "上页",
            nextText: "下页",
            lastText: "最后页",
            columns: gridColumns,
            tableStyle: "ui-jqgrid ui-jqgrid-view ui-jqgrid-bdiv ui-jqgrid-btable",
            rowStyle: "ui-widget-content jqgrow ui-row-ltr",
            alternatingRowStyle: "ui-widget-content jqgrow ui-row-ltr ui-priority-secondary",
            headerStyle: "ui-state-default ui-jqgrid-hdiv",
            footerStyle: "ui-state-default ui-jqgrid-pager ui-corner-bottom");
        }
    }
}