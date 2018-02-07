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
	public partial class wfmGoodsChart : wfmBase
	{
		BusiComm.StorageBusi StoBusi;
	
		protected void Page_Load(object sender, System.EventArgs e)
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
					this.FillDropDownList("Goods", ddlGoods);
					this.FillDropDownList("tbCommCode",ddlGoodType,"vcCommSign='GT'","全部");
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

		}
		#endregion

		protected DataTable Query(string strAcctMonth,string strGoodsID)
		{
			DataTable dtout=new DataTable();
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				dtout=StoBusi.GetGoodsChart(strAcctMonth,strGoodsID);
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
			
			string strGoodsName=this.ddlGoods.SelectedItem.Text;
			// Put user code to initialize the page here
			LineChart c = new LineChart ( 640 , 480 ) ;
			c.Title = "";
			c.Title = strAcctMonth.Substring(0,4)+"年"+strAcctMonth.Substring(4,2)+"月各"+strGoodsName+"销售量日走势（个）" ;
			c.strXTitle = "日期";
			c.strYTitle = "销售量（个）";
			int iDays = System.DateTime.DaysInMonth(iYear,iMonth);
			int iScaleX = iDays;
			c.Xorigin = 0 ; c.ScaleX = iScaleX; c.Xdivs = iScaleX;
//			c.Yorigin = 0 ; c.ScaleY = Convert.ToInt32(this.ddlYAXis.SelectedItem.Value) ; c.Ydivs = 20 ;
			
			string strGoodsID=this.ddlGoods.SelectedValue;
			DataTable dtData = this.Query(strAcctMonth,strGoodsID);
			int maxcount=0;
			for(int q=0;q<dtData.Rows.Count;q++)
			{
				if(int.Parse(dtData.Rows[q]["SaleCount"].ToString())>maxcount)
					maxcount=int.Parse(dtData.Rows[q]["SaleCount"].ToString());
			}
			c.Yorigin = 0 ; c.ScaleY = (maxcount/20+1)*20 ; c.Ydivs = 20 ;

			//画图
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
			alColor.Add(new Pen(Color.Gainsboro,2));
			alColor.Add(new Pen(Color.LightPink,2));
			alColor.Add(new Pen(Color.Goldenrod,2));
			alColor.Add(new Pen(Color.DarkViolet,2));
			alColor.Add(new Pen(Color.Cyan,2));
			alColor.Add(new Pen(Color.Firebrick,2));
			
			string strtablesample="<TABLE style='FONT-SIZE: 10pt; WIDTH: 200px; HEIGHT: 104px' cellSpacing='0' cellPadding='0' width='200' align='left' border='0'>";
			strtablesample+="<tr><td style='WIDTH: 150px; HEIGHT: 34px'>注：<BR>X：日期<br>Y：销售量（个）</TD><TD style='WIDTH: 120px; HEIGHT: 34px'></TD></tr>";

			int ColorIndexUsed=0;
			c.ClearOldPoint();
			DataView dvSale = dtData.DefaultView;
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
					alCount[iDayIndex] = Convert.ToSingle(drv["SaleCount"]);
				}
		
				for(int i=1;i<=iEndDay;i++)
				{
					c.AddValue(i,Convert.ToSingle(alCount[i]));
				}
				Pen Pen1 =(Pen)alColor[ColorIndexUsed];
				ColorIndexUsed++;
				c.DrawLine(Pen1,false);
				string strColorName=Pen1.Color.Name;
				strtablesample+="<tr><TD style='WIDTH: 150px' align='left'><FONT face='宋体'><HR style='WIDTH: 70.68%; HEIGHT: 2px' width='70.68%' color='"+strColorName+"' SIZE='2'></FONT></TD><TD style='WIDTH: 120px'><FONT face='宋体'>"+strGoodsName+"</FONT></TD></TR>";
			}
			c.SaveChart(strFileName,ImageFormat.Png);
			strtablesample+="</TABLE>";
            info.InnerHtml = strtablesample;
			//this.Response.Write(strtablesample);		
		}

		protected void btnOk_ServerClick(object sender, System.EventArgs e)
		{
			string strAcctMonth = this.ddlAcctMonth.SelectedItem.Value;
			string strrename=DateTime.Now.Hour.ToString()+DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString();
			string strPictureName = "/images/"+"AMS"+strAcctMonth+strrename+".png";
			string strPicturePath = HttpContext.Current.Server.MapPath("/");
			string[] strfiles=System.IO.Directory.GetFiles(strPicturePath+"/images","AMS*.png");
			for(int i=0;i<strfiles.Length;i++)
			{
				System.IO.File.Delete(strfiles[i]);
			}
			this.DrawChart(strAcctMonth,strPicturePath+strPictureName);
			this.Image1.ImageUrl = strPictureName;			
			this.Image1.Visible = true;
		}

		private DataTable SeachGoods()
		{
			DataTable dtout=new DataTable();
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				string strGoodType=this.ddlGoodType.SelectedValue;
				string strGoodName=this.txtGoodName.Text.Trim();
				if(strGoodType=="全部")
					strGoodType="";
				if(!(strGoodType==""&&strGoodName==""))
				{
					dtout=StoBusi.ChartSeachGoods(strGoodType,strGoodName);
				}
				return dtout;
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBy2dir("查询错误，请重试！");
				return dtout;
			}
		}

		protected void ddlGoodType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtg=this.SeachGoods();
			this.FillDropDownList(dtg,ddlGoods);
			if(this.txtGoodName.Text.Trim()==""&&this.ddlGoodType.SelectedValue=="全部")
				this.FillDropDownList("Goods", ddlGoods);
		}

		protected void btnserch_ServerClick(object sender, System.EventArgs e)
		{
			if(txtGoodName.Text.Trim()=="")
			{
				this.Popup("请输入要筛选要商品名称！");
				return;
			}
			DataTable dtg=this.SeachGoods();
			this.FillDropDownList(dtg,ddlGoods);
		}
	}
}
