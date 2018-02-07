using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class EntityJQGrid : JQGrid
    {
        public EntityJQGrid()
        {
            this.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.AppearanceSettings.AlternateRowBackground = true;
            this.AppearanceSettings.HighlightRowsOnHover = true;
            this.AppearanceSettings.ShowRowNumbers = true;
            this.ToolBarSettings.ShowAddButton = true;
            this.ToolBarSettings.ShowEditButton = true;
            this.ToolBarSettings.ShowViewRowDetailsButton = true;
            this.ToolBarSettings.ShowDeleteButton = true;
            this.ToolBarSettings.ShowSearchButton = true;
            this.ToolBarSettings.ShowRefreshButton = true;
            this.ToolBarSettings.ShowExcelButton = true;
            this.ToolBarSettings.ShowColumnChooser = true;
            this.SearchDialogSettings.MultipleSearch = true;
            this.SearchDialogSettings.Resizable = true;
            //this.AutoWidth = true;
            this.Height = Unit.Percentage(100);
            this.ClientSideEvents.RowDoubleClick = "RowDoubleClick";        
        }
    }
}