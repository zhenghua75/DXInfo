using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.ReportingServices;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace DXInfo.Web.Print
{
    public partial class PurchaseInStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["Id"] != "")
                {
                    this.ReportViewer1.LocalReport.EnableExternalImages = true;
                    string path = Server.MapPath("~/Print");
                    string strId = Request["Id"];
                    string vouchType = Request["vouchType"];
                    switch (vouchType)
                    {
                        case "001":
                            ReportViewer1.LocalReport.ReportPath = path+@"\PurchaseInStock.rdlc";
                            break;
                        case "002":
                            ReportViewer1.LocalReport.ReportPath = path + @"\SaleOutStock.rdlc";
                            break;
                        case "003":
                            ReportViewer1.LocalReport.ReportPath = path + @"\OtherInStock.rdlc";
                            break;
                        case "004":
                            ReportViewer1.LocalReport.ReportPath = path + @"\OtherOutStock.rdlc";
                            break;
                        case "005":
                            ReportViewer1.LocalReport.ReportPath = path + @"\MaterialOutStock.rdlc";
                            break;
                        case "006":
                            ReportViewer1.LocalReport.ReportPath = path + @"\ProductInStock.rdlc";
                            break;
                        case "007":
                            ReportViewer1.LocalReport.ReportPath = path + @"\InitStock.rdlc";
                            break;
                        case "008":
                            ReportViewer1.LocalReport.ReportPath = path + @"\ScrapVouch.rdlc";
                            break;
                        case "009":
                            ReportViewer1.LocalReport.ReportPath = path + @"\TransVouch.rdlc";
                            break;
                        case "010":
                            ReportViewer1.LocalReport.ReportPath = path + @"\CheckVouch.rdlc";
                            break;
                        case "011":
                            ReportViewer1.LocalReport.ReportPath = path + @"\AdjustLocatorVouch.rdlc";
                            break;
                        case "012":
                            ReportViewer1.LocalReport.ReportPath = path + @"\MixVouch.rdlc";
                            break;

                    }
                    Guid id = Guid.Parse(strId);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    DataTable dt;
                    ReportDataSource dataSource;
                    switch (vouchType)
                    {
                        case "001":
                        case "002":
                        case "003":
                        case "004":
                        case "005":
                        case "006":
                        case "007":
                            RdRecord_RdRecordsTableAdapters.RdRecord_RdRecordsTableAdapter rr = new RdRecord_RdRecordsTableAdapters.RdRecord_RdRecordsTableAdapter();
                            dt = rr.GetData(id);
                            this.SetImageFilePath(dt);
                            dataSource = new ReportDataSource("RdRecord_RdRecords", dt);
                            ReportViewer1.LocalReport.DataSources.Add(dataSource);
                            break;
                        case "008":
                            ScrapVouch_ScrapVouchsTableAdapters.ScrapVouch_ScrapVouchsTableAdapter sv = new ScrapVouch_ScrapVouchsTableAdapters.ScrapVouch_ScrapVouchsTableAdapter();
                            dt = sv.GetData(id);
                            dataSource = new ReportDataSource("ScrapVouch_ScrapVouchs", dt);
                            ReportViewer1.LocalReport.DataSources.Add(dataSource);
                            break;
                        case "009":
                            TransVouch_TransVouchsTableAdapters.TransVouch_TransVouchsTableAdapter tv = new TransVouch_TransVouchsTableAdapters.TransVouch_TransVouchsTableAdapter();
                            dt = tv.GetData(id);
                            dataSource = new ReportDataSource("TransVouch_TransVouchs", dt);
                            ReportViewer1.LocalReport.DataSources.Add(dataSource);
                            break;
                        case "010":
                            CheckVouch_CheckVouchsTableAdapters.CheckVouch_CheckVouchsTableAdapter cv = new CheckVouch_CheckVouchsTableAdapters.CheckVouch_CheckVouchsTableAdapter();
                            dt = cv.GetData(id);
                            dataSource = new ReportDataSource("CheckVouch_CheckVouchs", dt);
                            ReportViewer1.LocalReport.DataSources.Add(dataSource);
                            break;
                        case "011":
                            AdjustLocatorVouch_AdjustLocatorVouchsTableAdapters.AdjustLocatorVouch_AdjustLocatorVouchsTableAdapter av = new AdjustLocatorVouch_AdjustLocatorVouchsTableAdapters.AdjustLocatorVouch_AdjustLocatorVouchsTableAdapter();
                            dt = av.GetData(id);
                            dataSource = new ReportDataSource("AdjustLocatorVouch_AdjustLocatorVouchs", dt);
                            ReportViewer1.LocalReport.DataSources.Add(dataSource);
                            break;
                        case "012":
                            MixVouch_MixVouchsTableAdapters.MixVouch_MixVouchsTableAdapter mt = new MixVouch_MixVouchsTableAdapters.MixVouch_MixVouchsTableAdapter();
                            dt = mt.GetData(id);
                            dataSource = new ReportDataSource("MixVouch_MixVouchs", dt);
                            ReportViewer1.LocalReport.DataSources.Add(dataSource);
                            break;
                    }
                    ReportViewer1.LocalReport.Refresh();
                }
            }
        }

        private void SetImageFilePath(DataTable dt)
        {
            Uri uri = this.Request.Url;
            string t1 = this.Request.Url.AbsoluteUri;
            string t2 = this.Request.Url.PathAndQuery;
            string path = this.Request.Url.AbsoluteUri.Substring(0,t1.Length-t2.Length);
            if (dt.Columns.Contains("ImageFileName"))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string imageFileName = dr["ImageFileName"].ToString();
                    string imageFilePath = System.IO.Path.Combine(path, imageFileName);
                    dr["ImageFileName"] = path + "/ckfinder/userfiles/images/";
                }
            }
        }
    }
}