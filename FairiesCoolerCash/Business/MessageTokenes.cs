using System.Windows.Data;
using System.Windows.Controls;

namespace FairiesCoolerCash.Business
{
    public class RibbonMessageToken
    {
        public Microsoft.Windows.Controls.Ribbon.Ribbon MyRibbon { get; set; }
    }
    public class ViewCollectionViewSourceMessageToken
    {
        public CollectionViewSource CVS { get; set; }
    }
    public class CloseViewMessageToken
    {
    }
    public class CloseUserControlMessageToken
    {
    }
    public class ChangeUserControlMessageToken
    {
        public UserControl MyContent { get; set; }
    }

    public class DataGridMessageToken
    {
        public DataGrid MyDataGrid { get; set; }
    }
}
