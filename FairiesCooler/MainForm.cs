using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;

namespace FairiesCooler
{
    public enum RibbonMarkupCommands : uint
    {
        cmdButtonNew = 1001,
        cmdButtonOpen = 1002,
        cmdButtonSave = 1003,
        cmdButtonExit = 1004,
        cmdButtonDropA = 1008,
        cmdButtonDropB = 1009,
        cmdButtonDropC = 1010,
        cmdTabMain = 1011,
        cmdTabDrop = 1012,
        cmdGroupFileActions = 1013,
        cmdGroupExit = 1014,
        cmdGroupDrop = 1015,
        cmdHelpButton = 1016,
    }

    public partial class MainForm : Form
    {

        private RibbonButton _buttonNew;
        private RibbonButton _buttonDropB;
        private RibbonButton _exitButton;
        private RibbonHelpButton _helpButton;
        //private RibbonTab _mainTab;
        public MainForm()
        {
            InitializeComponent();

            _buttonDropB = new RibbonButton(ribbon1, (uint)RibbonMarkupCommands.cmdButtonDropB);
            //_buttonDropB.Enabled = false;
            //RibbonLib.Interop.PropertyKey pk = new RibbonLib.Interop.PropertyKey();
            //pk.FormatId
            //_buttonDropB.UpdateProperty("Visible",
            _buttonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
            
            _exitButton = new RibbonButton(ribbon1, (uint)RibbonMarkupCommands.cmdButtonExit);
            _helpButton = new RibbonHelpButton(ribbon1, (uint)RibbonMarkupCommands.cmdHelpButton);

            _exitButton.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_exitButton_ExecuteEvent);
            _helpButton.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_helpButton_ExecuteEvent);

            _buttonNew = new RibbonButton(ribbon1, (uint)RibbonMarkupCommands.cmdButtonNew);
            _buttonNew.Enabled = false;
            //_mainTab = new RibbonTab(ribbon1, 1011);
            //_mainTab.Label = "主页";
        }
        void _buttonDropB_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            //MessageBox.Show("drop B button pressed");
        }

        void _exitButton_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // Close form asynchronously since we are in a ribbon event 
            // handler, so the ribbon is still in use, and calling Close 
            // will eventually call _ribbon.DestroyFramework(), which is 
            // a big no-no, if you still use the ribbon.
            this.BeginInvoke(new MethodInvoker(this.Close));
        }

        void _helpButton_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("Help button pressed");
        }
    }
    
}
