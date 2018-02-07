using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MemberManage.Print
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TrackBar trackCustomers;
		private System.Windows.Forms.Button cmdPrintPreview;
		private System.Windows.Forms.Button cmdPageSettings;
		private System.Windows.Forms.Button cmdPrint;
		public static bool IsShowing ;
		private Infragistics.Win.UltraWinGrid.UltraCombo ultraCombo1;
		private Infragistics.Win.Misc.UltraExpandableGroupBox ultraExpandableGroupBox1;
		private Infragistics.Win.Misc.UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
		private Infragistics.Win.Misc.UltraExpandableGroupBox ultraExpandableGroupBox2;
		private Infragistics.Win.Misc.UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel2;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid2;
		private Infragistics.Win.Misc.UltraButton btnProduct;
		// members...
		PrintEngine _engine = new PrintEngine();

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			// create a default print engine...
			CreateEngine(1);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
			IsShowing = false;
		}

		// CreateEngine - create a print engine and populate it with customers...
		public void CreateEngine(int numCustomers)
		{
			// create a new engine...
			_engine = new PrintEngine();

			// loop through the customers...
			for(int n = 0; n < numCustomers; n++)
			{
				// create the customer...
				Customer theCustomer = new Customer();
				theCustomer.Id = n + 1;
				theCustomer.FirstName = "Darren";
				theCustomer.LastName = "Clarke";
				theCustomer.Company = "Madras inc.";
				theCustomer.Email = "darren@pretendcompany.com";
				theCustomer.Phone = "602 555 1234";

				// add the customer to the list...
				_engine.AddPrintObject(theCustomer);
			}
		}
		private void trackCustomers_Scroll(object sender, System.EventArgs e)
		{
			CreateEngine(trackCustomers.Value);
		}

		private void cmdPrintPreview_Click(object sender, System.EventArgs e)
		{
			// tell the print object to display a preview...
			_engine.ShowPreview();
		}

		private void cmdPageSettings_Click(object sender, System.EventArgs e)
		{
			// show the page settings...
			_engine.ShowPageSettings();
		}
		private void cmdPrint_Click(object sender, System.EventArgs e)
		{
			// print...
			_engine.ShowPrintDialog();
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			this.trackCustomers = new System.Windows.Forms.TrackBar();
			this.cmdPrintPreview = new System.Windows.Forms.Button();
			this.cmdPageSettings = new System.Windows.Forms.Button();
			this.cmdPrint = new System.Windows.Forms.Button();
			this.ultraCombo1 = new Infragistics.Win.UltraWinGrid.UltraCombo();
			this.ultraExpandableGroupBox1 = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			this.ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ultraExpandableGroupBox2 = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			this.ultraExpandableGroupBoxPanel2 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			this.ultraGrid2 = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.btnProduct = new Infragistics.Win.Misc.UltraButton();
			((System.ComponentModel.ISupportInitialize)(this.trackCustomers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraCombo1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraExpandableGroupBox1)).BeginInit();
			this.ultraExpandableGroupBox1.SuspendLayout();
			this.ultraExpandableGroupBoxPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraExpandableGroupBox2)).BeginInit();
			this.ultraExpandableGroupBox2.SuspendLayout();
			this.ultraExpandableGroupBoxPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).BeginInit();
			this.SuspendLayout();
			// 
			// trackCustomers
			// 
			this.trackCustomers.Location = new System.Drawing.Point(40, 104);
			this.trackCustomers.Maximum = 128;
			this.trackCustomers.Name = "trackCustomers";
			this.trackCustomers.Size = new System.Drawing.Size(448, 42);
			this.trackCustomers.TabIndex = 0;
			this.trackCustomers.Value = 1;
			this.trackCustomers.Scroll += new System.EventHandler(this.trackCustomers_Scroll);
			// 
			// cmdPrintPreview
			// 
			this.cmdPrintPreview.Location = new System.Drawing.Point(56, 184);
			this.cmdPrintPreview.Name = "cmdPrintPreview";
			this.cmdPrintPreview.Size = new System.Drawing.Size(96, 23);
			this.cmdPrintPreview.TabIndex = 1;
			this.cmdPrintPreview.Text = "PrintPreview ";
			this.cmdPrintPreview.Click += new System.EventHandler(this.cmdPrintPreview_Click);
			// 
			// cmdPageSettings
			// 
			this.cmdPageSettings.Location = new System.Drawing.Point(176, 184);
			this.cmdPageSettings.Name = "cmdPageSettings";
			this.cmdPageSettings.Size = new System.Drawing.Size(96, 23);
			this.cmdPageSettings.TabIndex = 2;
			this.cmdPageSettings.Text = "PageSettings ";
			this.cmdPageSettings.Click += new System.EventHandler(this.cmdPageSettings_Click);
			// 
			// cmdPrint
			// 
			this.cmdPrint.Location = new System.Drawing.Point(304, 184);
			this.cmdPrint.Name = "cmdPrint";
			this.cmdPrint.TabIndex = 3;
			this.cmdPrint.Text = "Print ";
			this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
			// 
			// ultraCombo1
			// 
			this.ultraCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			appearance1.BackColor = System.Drawing.SystemColors.Window;
			appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			this.ultraCombo1.DisplayLayout.Appearance = appearance1;
			this.ultraCombo1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			this.ultraCombo1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			this.ultraCombo1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			this.ultraCombo1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			this.ultraCombo1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			this.ultraCombo1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			this.ultraCombo1.DisplayLayout.MaxColScrollRegions = 1;
			this.ultraCombo1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ultraCombo1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.ultraCombo1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			this.ultraCombo1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			this.ultraCombo1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			this.ultraCombo1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			this.ultraCombo1.DisplayLayout.Override.CellAppearance = appearance8;
			this.ultraCombo1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			this.ultraCombo1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			this.ultraCombo1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			this.ultraCombo1.DisplayLayout.Override.HeaderAppearance = appearance10;
			this.ultraCombo1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			this.ultraCombo1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			this.ultraCombo1.DisplayLayout.Override.RowAppearance = appearance11;
			this.ultraCombo1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ultraCombo1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			this.ultraCombo1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.ultraCombo1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.ultraCombo1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			this.ultraCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			this.ultraCombo1.Location = new System.Drawing.Point(488, 144);
			this.ultraCombo1.Name = "ultraCombo1";
			this.ultraCombo1.Size = new System.Drawing.Size(152, 22);
			this.ultraCombo1.TabIndex = 4;
			this.ultraCombo1.Text = "ultraCombo1";
			// 
			// ultraExpandableGroupBox1
			// 
			this.ultraExpandableGroupBox1.Controls.Add(this.ultraExpandableGroupBoxPanel1);
			this.ultraExpandableGroupBox1.Expanded = false;
			this.ultraExpandableGroupBox1.ExpandedSize = new System.Drawing.Size(304, 200);
			this.ultraExpandableGroupBox1.Location = new System.Drawing.Point(488, 32);
			this.ultraExpandableGroupBox1.Name = "ultraExpandableGroupBox1";
			this.ultraExpandableGroupBox1.Size = new System.Drawing.Size(304, 21);
			this.ultraExpandableGroupBox1.TabIndex = 5;
			this.ultraExpandableGroupBox1.Text = "ultraExpandableGroupBox1";
			// 
			// ultraExpandableGroupBoxPanel1
			// 
			this.ultraExpandableGroupBoxPanel1.Controls.Add(this.ultraGrid1);
			this.ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(-10000, -10000);
			this.ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
			this.ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(298, 178);
			this.ultraExpandableGroupBoxPanel1.TabIndex = 0;
			this.ultraExpandableGroupBoxPanel1.Visible = false;
			// 
			// ultraGrid1
			// 
			this.ultraGrid1.Location = new System.Drawing.Point(32, 24);
			this.ultraGrid1.Name = "ultraGrid1";
			this.ultraGrid1.Size = new System.Drawing.Size(216, 80);
			this.ultraGrid1.TabIndex = 0;
			this.ultraGrid1.Text = "ultraGrid1";
			this.ultraGrid1.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGrid1_InitializeLayout);
			// 
			// ultraExpandableGroupBox2
			// 
			this.ultraExpandableGroupBox2.Controls.Add(this.ultraExpandableGroupBoxPanel2);
			this.ultraExpandableGroupBox2.ExpandedSize = new System.Drawing.Size(304, 185);
			this.ultraExpandableGroupBox2.Location = new System.Drawing.Point(488, 192);
			this.ultraExpandableGroupBox2.Name = "ultraExpandableGroupBox2";
			this.ultraExpandableGroupBox2.Size = new System.Drawing.Size(304, 185);
			this.ultraExpandableGroupBox2.TabIndex = 6;
			this.ultraExpandableGroupBox2.Text = "ultraExpandableGroupBox2";
			// 
			// ultraExpandableGroupBoxPanel2
			// 
			this.ultraExpandableGroupBoxPanel2.Controls.Add(this.ultraGrid2);
			this.ultraExpandableGroupBoxPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraExpandableGroupBoxPanel2.Location = new System.Drawing.Point(3, 19);
			this.ultraExpandableGroupBoxPanel2.Name = "ultraExpandableGroupBoxPanel2";
			this.ultraExpandableGroupBoxPanel2.Size = new System.Drawing.Size(298, 163);
			this.ultraExpandableGroupBoxPanel2.TabIndex = 0;
			// 
			// ultraGrid2
			// 
			this.ultraGrid2.Location = new System.Drawing.Point(16, 16);
			this.ultraGrid2.Name = "ultraGrid2";
			this.ultraGrid2.Size = new System.Drawing.Size(264, 88);
			this.ultraGrid2.TabIndex = 0;
			this.ultraGrid2.Text = "ultraGrid2";
			this.ultraGrid2.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGrid2_InitializeLayout);
			// 
			// btnProduct
			// 
			this.btnProduct.Location = new System.Drawing.Point(200, 272);
			this.btnProduct.Name = "btnProduct";
			this.btnProduct.TabIndex = 7;
			this.btnProduct.Text = "ultraButton1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(856, 381);
			this.Controls.Add(this.btnProduct);
			this.Controls.Add(this.ultraExpandableGroupBox2);
			this.Controls.Add(this.ultraExpandableGroupBox1);
			this.Controls.Add(this.ultraCombo1);
			this.Controls.Add(this.cmdPrint);
			this.Controls.Add(this.cmdPageSettings);
			this.Controls.Add(this.cmdPrintPreview);
			this.Controls.Add(this.trackCustomers);
			this.Name = "Form1";
			this.Text = "Customer Printer";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.trackCustomers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraCombo1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraExpandableGroupBox1)).EndInit();
			this.ultraExpandableGroupBox1.ResumeLayout(false);
			this.ultraExpandableGroupBoxPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraExpandableGroupBox2)).EndInit();
			this.ultraExpandableGroupBox2.ResumeLayout(false);
			this.ultraExpandableGroupBoxPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//MemberManage.Business.Helper.BindProduct(this.ultraCombo1);
			this.ultraGrid1.Dock = DockStyle.Fill;
			MemberManage.Business.Helper.BindProduct(this.ultraGrid1);

			this.ultraGrid2.Dock = DockStyle.Fill;
			MemberManage.Business.Helper.BindMemberProduct(this.ultraGrid2,"普通网络会员");
		}

		private void ultraGrid1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			e.Layout.Bands[0].Columns["cnvcIsSelected"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
			e.Layout.Bands[0].Columns["cnvcIsSelected"].Header.Caption = "选择";
			e.Layout.Bands[0].Columns["cnvcProductName"].Header.Caption = "产品名称";
			e.Layout.Bands[0].Columns["cnnProductPrice"].Header.Caption = "产品单价";
			e.Layout.Bands[0].Columns["cnnProductDiscount"].Header.Caption = "产品折扣";
			e.Layout.Bands[0].Columns["cnnPrepay"].Header.Caption = "实收";

			e.Layout.Bands[0].Columns["cnvcIsSelected"].Width = 30;
			e.Layout.Bands[0].Columns["cnnProductDiscount"].Width = 60;
			e.Layout.Bands[0].Columns["cnnProductPrice"].Width = 60;

			e.Layout.Bands[0].Columns["cnvcProductName"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			e.Layout.Bands[0].Columns["cnnProductPrice"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			//e.Layout.Bands[0].Columns["cnnProductDiscount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

		}

		private void ultraGrid2_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			e.Layout.Bands[0].Columns["cnvcIsSelected"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
			e.Layout.Bands[0].Columns["cnvcIsSelected"].Header.Caption = "选择";
			e.Layout.Bands[0].Columns["cnnMemberCodeID"].Hidden = true;
			e.Layout.Bands[0].Columns["cnvcMemberName"].Header.Caption = "会员资格";
			e.Layout.Bands[0].Columns["cnvcMemberType"].Header.Caption = "服务产品";
			e.Layout.Bands[0].Columns["cnvcMemberValue"].Header.Caption = "免费场次";

			e.Layout.Bands[0].Columns["cnvcIsSelected"].Width = 30;
			e.Layout.Bands[0].Columns["cnvcMemberValue"].Width = 60;

			e.Layout.Bands[0].Columns["cnvcMemberName"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			e.Layout.Bands[0].Columns["cnvcMemberType"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

		}
	}
}
