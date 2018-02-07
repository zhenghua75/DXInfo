using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CommCenter;

namespace AMSApp.Storage.Report
{
	/// <summary>
	/// Summary description for wfmSaleDailyChart.
	/// </summary>
	public class wfmSaleDailyChart : wfmBase
	{
		protected System.Web.UI.WebControls.DropDownList ddlAcctMonth;
		protected System.Web.UI.WebControls.DropDownList ddlYAXis;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnOk;
		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					string strvalue="";
					string strYear=DateTime.Now.Year.ToString();
					for(int i=0;i<12;i++)
					{
						if(i!=0&&DateTime.Now.AddMonths(-i).Month==12)
						{
							strYear=DateTime.Now.AddYears(-1).Year.ToString();
						}
						if(DateTime.Now.AddMonths(-i).Month<10)
						{
							strvalue=strYear+"0"+(DateTime.Now.AddMonths(-i).Month).ToString();
						}
						else
						{
							strvalue=strYear+(DateTime.Now.AddMonths(-i).Month).ToString();
						}
						this.ddlAcctMonth.Items.Add(new ListItem(strvalue,strvalue));
					}
				}
				this.Image1.Visible = false;
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnOk.ServerClick += new System.EventHandler(this.btnOk_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected DataTable Query(string strAcctMonth)
		{
			DataTable dtout=new DataTable();
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				dtout=StoBusi.GetDailySaleChart(strAcctMonth);
				if(dtout==null)
				{
					this.SetErrorMsgPageBy2dir("查询出错，请重试！");
					return dtout;
				}
				else
				{
					return dtout;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBy2dir("查询错误，请重试！");
				return dtout;
			}
		}

		protected void DrawChart(string strAcctMonth,string strFileName)
		{
			int iYear = System.Int16.Parse(strAcctMonth.Substring(0,4));
			int iMonth = System.Int16.Parse(strAcctMonth.Substring(4,2));
			
			
			// Put user code to initialize the page here
			LineChart c = new LineChart ( 640 , 480 ) ;
			c.Title = "";
			c.Title = strAcctMonth.Substring(0,4)+"年"+strAcctMonth.Substring(4,2)+"月各分店销售额日走势（万元）" ;
			c.strXTitle = "日期";
			c.strYTitle = "销售额（万元）";
			int iDays = System.DateTime.DaysInMonth(iYear,iMonth);
			int iScaleX = iDays;
			c.Xorigin = 0 ; c.ScaleX = iScaleX; c.Xdivs = iScaleX;
			c.Yorigin = 0 ; c.ScaleY = Convert.ToInt32(this.ddlYAXis.SelectedItem.Value) ; c.Ydivs = 20 ;
			
			DataTable dtData = this.Query(strAcctMonth);

			//画图
			DataTable dtDept=(DataTable)Application["tbCommCode"];
			DataView dvDept=new DataView(dtDept,"vcCommSign='MD'","vcCommCode",System.Data.DataViewRowState.CurrentRows);
			string strDeptName="";
			string strDeptID="";
			int iCurrentDay = DateTime.Now.Day;
			int iEndDay = 0;
			if(this.ddlAcctMonth.SelectedItem.Value==DateTime.Now.Year+DateTime.Now.Month.ToString("D2"))
			{
				iEndDay = DateTime.Now.Day;
			}
			else
			{
				iEndDay = iDays;
			}
			c.DrawCoordinate();

			ArrayList alColor=new ArrayList();
			alColor.Add(new Pen(Color.Red,2));
			alColor.Add(new Pen(Color.Green,2));
			alColor.Add(new Pen(Color.Blue,2));
			alColor.Add(new Pen(Color.Yellow,2));
			alColor.Add(new Pen(Color.Black,2));
			alColor.Add(new Pen(Color.Purple,2));
			alColor.Add(new Pen(Color.DarkGoldenrod,2));
			alColor.Add(new Pen(Color.Fuchsia,2));
			alColor.Add(new Pen(Color.Aqua,2));
			alColor.Add(new Pen(Color.Silver,2));
			alColor.Add(new Pen(Color.PaleGoldenrod,2));
			alColor.Add(new Pen(Color.GreenYellow,2));
			alColor.Add(new Pen(Color.LightSeaGreen,2));
			alColor.Add(new Pen(Color.LightPink,2));
			
			string strtablesample="<TABLE style='FONT-SIZE: 10pt; Z-INDEX: 104; LEFT: 712px; WIDTH: 200px; POSITION: absolute; TOP: 88px; HEIGHT: 104px' cellSpacing='0' cellPadding='0' width='200' align='left' border='0'>";
			strtablesample+="<tr><td style='WIDTH: 150px; HEIGHT: 34px'>注：<BR>X：日期<br>Y：销售额（万元）</TD><TD style='WIDTH: 120px; HEIGHT: 34px'></TD></tr>";
			for(int j=0;j<dvDept.Count;j++)
			{
				c.ClearOldPoint();
				DataRowView drvDept = (DataRowView)dvDept[j];
				strDeptName=drvDept["vcCommName"].ToString();
				strDeptID=drvDept["vcCommCode"].ToString();

				string strFilter = "vcDeptID='"+strDeptID+"'";
				DataView dvSale = new DataView(dtData,strFilter,"SaleDay",System.Data.DataViewRowState.CurrentRows);
				if(dvSale.Count>0)
				{
					ArrayList alCount = new ArrayList();
					for(int i=0;i<=31;i++)
					{
						alCount.Add(0);
					}
					for(int i=0;i<dvSale.Count;i++)
					{			
						DataRowView drv = (DataRowView)dvSale[i];
						int iDayIndex = Convert.ToInt16(drv["SaleDay"].ToString().Substring(6,2));
						alCount[iDayIndex] = Convert.ToSingle(drv["SaleFee"]);
					}
			
					for(int i=1;i<=iEndDay;i++)
					{
						c.AddValue(i,Convert.ToSingle(alCount[i]));
					}
					Pen Pen1 =(Pen)alColor[j];
					c.DrawLine(Pen1,false);
					string strColorName=Pen1.Color.Name;
					strtablesample+="<tr><TD style='WIDTH: 150px' align='left'><FONT face='宋体'><HR style='WIDTH: 70.68%; HEIGHT: 2px' width='70.68%' color='"+strColorName+"' SIZE='2'></FONT></TD><TD style='WIDTH: 120px'><FONT face='宋体'>"+strDeptName+"</FONT></TD></TR>";
				}
			}
			c.SaveChart(strFileName,ImageFormat.Png);
			strtablesample+="</TABLE>";
			this.Response.Write(strtablesample);		
		}

		private void btnOk_ServerClick(object sender, System.EventArgs e)
		{
			string strAcctMonth = this.ddlAcctMonth.SelectedItem.Value;
			string strYAxis = this.ddlYAXis.SelectedItem.Value;
			string strPictureName = "/images/"+strAcctMonth+strYAxis+".png";
			string strPicturePath = HttpContext.Current.Server.MapPath("/")+strPictureName;
			if(System.IO.File.Exists(strPicturePath))
			{
				System.IO.File.Delete(strPicturePath);
			}
			this.DrawChart(strAcctMonth,strPicturePath);
			this.Image1.ImageUrl = strPictureName;			
			this.Image1.Visible = true;
		}
	}
}
