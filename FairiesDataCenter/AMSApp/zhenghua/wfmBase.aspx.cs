using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Common;
using CommCenter;
using System.Text;
namespace AMSApp.zhenghua
{
	/// <summary>
	/// wfmBase 的摘要说明。
	/// </summary>
	public class wfmBase : System.Web.UI.Page
	{
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			//Session["Login"] = null;
			if(Session["Login"]==null)
			{											
				Response.Redirect(this.Request.ApplicationPath+"/Exit.aspx");
				//return;
			}
		}
		public CMSMStruct.LoginStruct oper
		{
			get
			{
				return (CMSMStruct.LoginStruct) Session["Login"];
			}
		}
		/// <summary>
		/// 失效天数
		/// </summary>
		public int ExpDate
		{
			get
			{
				DataTable dtNameCode = Application["tbNameCode"] as DataTable;
				DataRow[] drs = dtNameCode.Select("cnvcType='PDEXPDATE'");
				if(drs.Length>0)
					return Convert.ToInt32(drs[0]["cnvcCode"].ToString());
				else
					return 1;
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.Error += new System.EventHandler(this.wfmBase_Error);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//弹出窗口
		public void Popup(string strComments)
		{
			strComments = Regex.Replace(strComments, "'", ""); 
			strComments = Regex.Replace(strComments, "\\r\\n", ""); 
			this.Response.Write("<script>alert('"+strComments+"');</script>");
		}
		public void Popup(string strComments,string strurl)
		{
			strComments = Regex.Replace(strComments, "'", ""); 
			strComments = Regex.Replace(strComments, "\\r\\n", ""); 
			this.Response.Write("<script>alert('"+strComments+"');location.href='"+strurl+"';</script>");
		}
		public bool JudgeIsNull(string strText,string strMessage)
		{
			if(strText.Trim().Length == 0)
			{
				Popup(strMessage+"不能为空");
				return true;
			}
			return false;
		}
		public bool JudgeIsNull(string strText)
		{
			if(strText.Trim().Length == 0)
			{
				//Popup(strMessage+"不能为空");
				return true;
			}
			return false;
		}
		public bool JudgeIsNum(string strText,string strMessage)
		{
			if(!Regex.IsMatch(strText,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				Popup(strMessage+"请输入数字");
				return false;
			}
			return true;
		}
		public bool JudgeIsNum(string strText)
		{
			if(!Regex.IsMatch(strText,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				//Popup(strMessage+"请输入数字");
				return false;
			}
			return true;
		}
		public bool JudgeIsCode(string strProductType,string strProductClass,string strProductCode)
		{
			bool isTrue = false;
			try
			{
				switch(strProductType)
				{
					case "Raw":
					case "Pack":
					case "FINALPRODUCT":
						//用数字
						string[] strProductClasses = strProductClass.Split('~');
						int iProductCode = Convert.ToInt32(strProductCode);
						int iUpProductCode = Convert.ToInt32(strProductClasses[0]);
						int iDownProductCode = Convert.ToInt32(strProductClasses[1]);
						if(iUpProductCode <= iProductCode && iProductCode <= iDownProductCode)
						{
							isTrue = true;
						}
						break;
					case "SEMIPRODUCT":
						//带字母数字
						string[] strProductClasses1 = strProductClass.Split('~');
						string strProductClassLetter = strProductClasses1[0].Substring(0, 1);
						string strPorductCodeLetter = strProductCode.Substring(0, 1);
						//if(strProductClassLetter =)
						int iProductCode1 = Convert.ToInt32(strProductCode.Substring(1,strProductCode.Length-1));
						int iUpProductCode1 = Convert.ToInt32(strProductClasses1[0].Substring(1,strProductCode.Length-1));
						int iDownProductCode1 = Convert.ToInt32(strProductClasses1[1].Substring(1,strProductCode.Length-1));
						if(iUpProductCode1 <= iProductCode1 && iProductCode1 <= iDownProductCode1)
						{
							if(strProductClassLetter == strPorductCodeLetter)
							{
								isTrue = true;
							}
							
						}
						break;
				}
			}
			catch(Exception)
			{
				//Popup("编码错误");
				isTrue = false;
			}
			return isTrue;
		}
		public string GetWhCode(string strdeptid)
		{
			string strlen = GetPara("COMUNITL");
			if(strlen == "") strlen = "4";
			int ilen = Convert.ToInt32(strlen);

			string strwhcode = "";
			string strwhcode2 = "";
			string strsql = "SELECT count(*)+1 FROM tbWarehouse WHERE cnvcDepCode='"+strdeptid+"'";
			string strsql2 = "SELECT max(substring(cnvcWhCode,6,"+strlen+"))+1 FROM tbWarehouse WHERE cnvcDepCode='"+strdeptid+"' AND cnvcWhCode LIKE '"+strdeptid+"%'";
			DataTable dt = Helper.Query(strsql);
			DataTable dt2 = Helper.Query(strsql2);
			if(dt.Rows.Count>0)
			{
				strwhcode = dt.Rows[0][0].ToString();
				strwhcode = strwhcode.PadLeft(ilen,'0');
			}
			if(dt2.Rows.Count>0)
			{
				strwhcode2 = dt2.Rows[0][0].ToString();
				strwhcode2 = strwhcode2.PadLeft(ilen,'0');
			}
			if(Convert.ToInt32(strwhcode)<Convert.ToInt32(strwhcode2))
				strwhcode = strwhcode2;
			return strwhcode;

		}
		public string GetPara(string strtype)
		{
			string strcode = "";
			DataTable dt = Application["tbNameCode"] as DataTable;
			DataRow[] drs = dt.Select("cnvcType='"+strtype+"'");
			if(drs.Length>0)
			{
				strcode = drs[0]["cnvccode"].ToString();
			}
			return strcode;
		}

		public string GetComUnitCode(string strgroupcode)
		{
			string strlen = GetPara("COMUNITL");
			if(strlen == "") strlen = "4";
			int ilen = Convert.ToInt32(strlen);

			string strlen2 = GetPara("COMGROUPL");
			if(strlen2 == "") strlen2 = "4";
			int ilen2 = Convert.ToInt32(strlen2);


			string strcomunitcode = "";
			string strcomunitcode2 = "";
			string strsql = "SELECT count(*)+1 FROM tbComputationUnit WHERE cnvcGroupCode = '"+strgroupcode+"'";
			//string strsql2 = "SELECT CONVERT(int,max(cnvccomunitcode))+1 FROM tbComputationUnit WHERE cnvcGroupCode = '"+strgroupcode+"'";
			string strsql2 = "SELECT max(substring(cnvccomunitcode,"+Convert.ToString(ilen2+1)+","+strlen+"))+1   FROM tbComputationUnit WHERE cnvcGroupCode = '"+strgroupcode+"'";
			DataTable dt = Helper.Query(strsql);
			DataTable dt2 = Helper.Query(strsql2);
			if(dt.Rows.Count>0)
			{
				strcomunitcode = dt.Rows[0][0].ToString();
				strcomunitcode = strcomunitcode.PadLeft(ilen,'0');
			}
			if(dt2.Rows.Count>0)
			{
				strcomunitcode2 = dt2.Rows[0][0].ToString();
				strcomunitcode2 = strcomunitcode2.PadLeft(ilen,'0');
			}
			if(Convert.ToInt32(strcomunitcode)<Convert.ToInt32(strcomunitcode2))
				strcomunitcode = strcomunitcode2;
			return strcomunitcode;
		}
		public string GetComputationGroupCode()
		{
			string strlen = GetPara("COMGROUPL");
			if(strlen == "") strlen = "4";
			int ilen = Convert.ToInt32(strlen);
			string strgroupcode = "";
			string strgroupcode2 = "";
			string strsql = "SELECT count(*)+1 FROM tbComputationGroup";
			string strsql2 = "SELECT CONVERT(int,max(cnvcGroupCode))+1 FROM tbComputationGroup";
			DataTable dt = Helper.Query(strsql);
			DataTable dt2 = Helper.Query(strsql2);
			if(dt.Rows.Count>0)
			{
				strgroupcode = dt.Rows[0][0].ToString();
				strgroupcode = strgroupcode.PadLeft(ilen,'0');
			}
			if(dt2.Rows.Count>0)
			{
				strgroupcode2 = dt2.Rows[0][0].ToString();
				strgroupcode2 = strgroupcode2.PadLeft(ilen,'0');
			}
			if(Convert.ToInt32(strgroupcode)<Convert.ToInt32(strgroupcode2))
				strgroupcode = strgroupcode2;
			return strgroupcode;
		}
		public string GetInvCode(string strInvCCode)
		{			
			string strInvCode = "";
			string[] strInvCCodes = strInvCCode.Split('~');
			
			string strCodeBegin = strInvCCodes[0];
			string strCodeEnd = strInvCCodes[1];

			string strCodeBeginAlpha = "";
			int iCodeBeginIndex = 1;
			string strCodeEndAlpha = "";
			int iCodeEndIndex = 1;

			Regex r = new Regex(@"[A-Za-z]");
			MatchCollection mc1 = r.Matches(strCodeBegin);
			if(mc1.Count>0)
			{
				foreach(Match m in mc1)
				{
					strCodeBeginAlpha += m.Value;					
				}
				iCodeBeginIndex = mc1[mc1.Count-1].Index+1;
				strCodeBegin = strCodeBegin.Substring(iCodeBeginIndex);
				iCodeBeginIndex +=1;
			}
			MatchCollection mc2 = r.Matches(strCodeEnd);
			if(mc2.Count>0)
			{
				foreach(Match m in mc2)
				{
					strCodeEndAlpha += m.Value;					
				}
				iCodeEndIndex = mc2[mc2.Count-1].Index+1;
				strCodeEnd = strCodeEnd.Substring(iCodeEndIndex);
				iCodeEndIndex += 1;
			}
			string strsql = "select top 1 substring(cnvcInvCode,"+Convert.ToString(iCodeBeginIndex)+","+strCodeBegin.Length+") from tbInventory "
+" where cnvcInvCCode='"+strInvCCode+"' and substring(cnvcInvCode,"+Convert.ToString(iCodeBeginIndex)+","+strCodeBegin.Length+")>="+strCodeBegin+" and substring(cnvcInvCode,"+Convert.ToString(iCodeEndIndex)+","+strCodeEnd.Length+")<"+strCodeEnd
+" order by cnvcInvCode desc" ;

			DataTable dt = Helper.Query(strsql);

			if(dt.Rows.Count>0)
			{
				string strCode = dt.Rows[0][0].ToString();
				int iCode = int.Parse(strCode);
				int iEnd = int.Parse(strCodeEnd);
				if(iCode+1<=iEnd)
				{
					strInvCode = strCodeBeginAlpha + Convert.ToString(iCode+1).PadLeft(strCodeBegin.Length,'0');
				}							
			}
			else
			{
				strInvCode = strCodeBeginAlpha+strCodeBegin;
			}
			return strInvCode;
		}
		public void SetDDL(DropDownList ddl,string strvalue)
		{
//			ListItem li = ddl.Items.FindByValue(strvalue);
			//ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(strvalue));
			//ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(strvalue)); 
//			if(li != null)
//			{
//				li.Selected = true;
//			}
			foreach( ListItem item in ddl.Items )
			{
				if( item.Value == strvalue )
				{
					item.Selected = true;
					//ddl.SelectedIndex = ddl.Items.IndexOf(item);
				}
				else
				{
					item.Selected = false;
				}
			}
		}
		public void BindGoods(DropDownList ddl)
		{
			DataTable dtGoods = null;			
			if(Session["tbGoods"] != null)
			{
				dtGoods = (DataTable) Session["tbGoods"];
			}
			else
			{
				dtGoods = Helper.Query("select * from  tbGoods");
				Session["tbGoods"] = dtGoods;
			}
			DataView dvNameCode = new DataView(dtGoods);
			//dvNameCode.RowFilter = strFilter;
			ddl.DataSource = dvNameCode;			
			ddl.DataValueField = "vcGoodsID";
			ddl.DataTextField = "vcGoodsName";
			ddl.DataBind();
		}
		public DataTable GetGoods()
		{
			DataTable dtGoods = null;			
			if(Session["tbGoods"] != null)
			{
				dtGoods = (DataTable) Session["tbGoods"];
			}
			else
			{
				dtGoods = Helper.Query("select * from tbGoods");
				Session["tbGoods"] = dtGoods;
			}
			return dtGoods;
		}
		
		public void BindNameCode(DropDownList ddl,string strFilter)
		{
			DataTable dtNameCode = null;			
			if(Application["tbNameCode"] != null)
			{
				dtNameCode = (DataTable) Application["tbNameCode"];
			}
			else
			{
				dtNameCode = Helper.Query("select * from  tbNameCode");
				Application["tbNameCode"] = dtNameCode;
			}
			DataView dvNameCode = new DataView(dtNameCode);
			dvNameCode.RowFilter = strFilter;
			ddl.DataSource = dvNameCode;			
			ddl.DataValueField = "cnvcCode";
			ddl.DataTextField = "cnvcName";
			ddl.DataBind();
		}
		public void BindProductClass(DropDownList ddl,string strFilter)
		{
			DataTable dtProductClass = null;			
			if(Application["tbProductClass"] != null)
			{
				dtProductClass = (DataTable) Application["tbProductClass"];
			}
			else
			{
				dtProductClass = Helper.Query("select * from  tbProductClass");
				Application["tbProductClass"] = dtProductClass;
			}
			DataView dvProductClass = new DataView(dtProductClass);
			dvProductClass.RowFilter = strFilter;
			ddl.DataSource = dvProductClass;			
			ddl.DataValueField = "cnvcProductClassCode";
			ddl.DataTextField = "cnvcProductClassName";
			ddl.DataBind();
		}

		public void BindComputationGroup(DropDownList ddl,string strFilter)
		{
			DataTable dtComputationGroup = null;			
			if(Application["tbComputationGroup"] != null)
			{
				dtComputationGroup = (DataTable) Application["tbComputationGroup"];
			}
			else
			{
				dtComputationGroup = Helper.Query("select * from  tbComputationGroup");
				Application["tbComputationGroup"] = dtComputationGroup;
			}
			DataView dvComputationGroup = new DataView(dtComputationGroup);
			dvComputationGroup.RowFilter = strFilter;
			ddl.DataSource = dvComputationGroup;			
			ddl.DataValueField = "cnvcGroupCode";
			ddl.DataTextField = "cnvcGroupName";
			ddl.DataBind();
		}
		public void BindComputationUnit(DropDownList ddl,string strFilter)
		{
			DataTable dtComputationUnit = null;			
			if(Application["tbComputationUnit"] != null)
			{
				dtComputationUnit = (DataTable) Application["tbComputationUnit"];
			}
			else
			{
				dtComputationUnit = Helper.Query("select * from  tbComputationUnit");
				Application["tbComputationUnit"] = dtComputationUnit;
			}
			DataView dvComputationUnit = new DataView(dtComputationUnit);
			dvComputationUnit.RowFilter = strFilter;
			ddl.DataSource = dvComputationUnit;			
			ddl.DataValueField = "cnvcComUnitCode";
			ddl.DataTextField = "cnvcComUnitName";
			ddl.DataBind();
		}
		public void BindDept(DropDownList ddl,string strFilter)
		{
			DataTable dtDept = null;			
			if(Application["tbDept"] != null)
			{
				dtDept = (DataTable) Application["tbDept"];
			}
			else
			{
				dtDept = Helper.Query("select * from  tbDept");
				Application["tbDept"] = dtDept;
			}
			DataView dvDept = new DataView(dtDept);
			dvDept.RowFilter = strFilter;
			ddl.DataSource = dvDept;			
			ddl.DataValueField = "cnvcDeptID";
			ddl.DataTextField = "cnvcDeptName";
			ddl.DataBind();
		}
		public void BindOper(DropDownList ddl,string strFilter)
		{
			DataTable dtOper = null;			
			if(Application["tbLogin"] != null)
			{
				dtOper = (DataTable) Application["tbLogin"];
			}
			else
			{
				dtOper = Helper.Query("select * from  tbLogin");
				Application["tbLogin"] = dtOper;
			}
			DataView dvOper = new DataView(dtOper);
			dvOper.RowFilter = strFilter;
			ddl.DataSource = dvOper;			
			ddl.DataValueField = "vcLoginID";
			ddl.DataTextField = "vcOperName";
			ddl.DataBind();
		}

		public void BindWarehouse(DropDownList ddl,string strFilter)
		{
			DataTable dtOper = null;			
			if(Application["tbWarehouse"] != null)
			{
				dtOper = (DataTable) Application["tbWarehouse"];
			}
			else
			{
				dtOper = Helper.Query("select * from  tbWarehouse");
				Application["tbWarehouse"] = dtOper;
			}
			DataView dvOper = new DataView(dtOper);
			dvOper.RowFilter = strFilter;
			ddl.DataSource = dvOper;			
			ddl.DataValueField = "cnvcWhCode";
			ddl.DataTextField = "cnvcWhName";
			ddl.DataBind();
		}

		//DataTable里面的代码转换
		public void DataTableConvert(DataTable dt,string columnName,string strApplicationName,string strIDColumnName,string strCommentsColumnName,string filter)
		{
			if(dt == null)
			{
				Popup("无查询结果");
				return;
			}
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//判断新列是否存在，已经存在就不添加，不存在就添加
			if(dt.Columns[strCommentColumnName]==null)
			{			
				dt.Columns.Add(strCommentColumnName,typeof(string));
			}
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(strApplicationName,strIDColumnName,dr[columnName].ToString(),strCommentsColumnName,filter);					
				dr[strCommentColumnName] = strTemp;					
			}
		}		

		public void DataTableConvert(DataTable dt,string columnName,string newcolumnName,string strApplicationName,string strIDColumnName,string strCommentsColumnName,string filter)
		{
			if(dt == null)
			{
				Popup("无查询结果");
				return;
			}
			string strTemp ;			
			string strCommentColumnName = newcolumnName;//columnName+"Comments";
			//判断新列是否存在，已经存在就不添加，不存在就添加
			if(dt.Columns[strCommentColumnName]==null)
			{			
				dt.Columns.Add(strCommentColumnName,typeof(string));
			}
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(strApplicationName,strIDColumnName,dr[columnName].ToString(),strCommentsColumnName,filter);					
				dr[strCommentColumnName] = strTemp;					
			}
		}
		
		//DataTable里面的代码转换(转换原有列的值,无新增列)
		public void TableConvert(DataTable dt,string columnName,string showItem,string idcol,string comcol)
		{
			string strTemp ;
						
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(showItem,dr[columnName].ToString(),idcol,comcol);					
				dr[columnName] = strTemp;					
			}
		}	
	
		public void TableConvert(DataTable dt,string columnName,string showItem,string idcol,string comcol,string filter)
		{
			string strTemp ;
			
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(showItem,dr[columnName].ToString(),idcol,comcol,filter,1);					
				dr[columnName] = strTemp;					
			}
		}		


		//DataTable里面的代码转换
//		public void DataTableConvert(DataTable dt,string columnName,string showItem)
//		{
//			string strTemp ;			
//			string strCommentColumnName = columnName+"Comments";
//			//判断新列是否存在，已经存在就不添加，不存在就添加
//			if(dt.Columns[strCommentColumnName]==null)
//			{			
//				dt.Columns.Add(strCommentColumnName,typeof(string));
//			}
//			foreach (DataRow dr in dt.Rows)
//			{				
//				strTemp = this.CodeConvert(showItem,dr[columnName].ToString());	
//				dr[strCommentColumnName] = strTemp;
//			}			
//		}	

		
		// id -> remark 转换		
		private string CodeConvert(string showItem,string selectId,string idcol,string comcol)
		{
			DataTable dt=(DataTable)Application[showItem];
			string strRemark = selectId;
			if(dt==null)
			{
				throw new Exception("Application 中代码没有找到！");
			}
			for(int i = 0;i<dt.Rows.Count;i++)
			{
				if(dt.Rows[i][idcol].ToString().Equals(selectId))
				{
					strRemark = dt.Rows[i][comcol].ToString();
					break;
				}
			}
			return strRemark;	
		}
		
		//根据 selectId 返回tbCodeName  表中的 中文注释
		private string CodeConvert(string showItem,string selectId,string idcol,string comcol,string filter,int i)
		{
			DataTable dt=(DataTable)Application[showItem];			
			string strRemark ;
			if(dt==null)
			{
				throw new Exception("Application 中代码没有找到！");
			}			
			DataView dw = new DataView(dt);			
			string strfilter = filter +" and "+idcol+" = '"+selectId+"'"; 
			dw.RowFilter = strfilter;			
			if(dw.Count >= 1)
			{
				strRemark = dw[0].Row[comcol].ToString();
			}
			else
			{
				strRemark = selectId;
			}
			return strRemark;				
		}
	
		//根据 selectId 返回tbCodeName  表中的 中文注释
		private string CodeConvert(string strApplicationName,string strIDColumnName,string selectId,string strCommentsColumnName,string strfilter)
		{
			this.FillApplication(strApplicationName);
			DataTable dt=(DataTable)Application[strApplicationName];			
			string strRemark ;
			if(dt==null)
			{
				throw new Exception("Application 中代码没有找到！");
			}			
			DataView dw = new DataView(dt);			
			//string strfilter = "";
			if(strfilter == "")
			{
				strfilter = strIDColumnName+" = '"+selectId+"'"; 
			}
			else
			{
				strfilter = strfilter +" and "+strIDColumnName+" = '"+selectId+"'"; 
			}
			
			dw.RowFilter = strfilter;			
			if(dw.Count == 1)
			{
				strRemark = dw[0].Row[strCommentsColumnName].ToString();
			}
			else
			{
				strRemark = "";//selectId;
				//throw new Exception(strCommentsColumnName+"参数不全，请配置参数！");
			}
			return strRemark;				
		}
		private void FillApplication(string strApplicationName)
		{
			if(Application["tbDept"] == null)
			{
				Application["tbDept"] = Helper.Query("select * from tbDept");
			}
			if(Application["tbLogin"] == null)
			{
				Application["tbLogin"] = Helper.Query("select * from tbLogin");
			}
			if(Application["tbNameCode"] == null)
			{
				Application["tbNameCode"] = Helper.Query("select * from tbNameCode");
			}
			if(Application["tbProductClass"] == null)
			{
				Application["tbProductClass"] = Helper.Query("select * from tbProductClass");
			}
			if(Application["tbComputationGroup"] == null)
			{
				Application["tbComputationGroup"] = Helper.Query("select * from tbComputationGroup");
			}
			if(Application["tbComputationUnit"] == null)
			{
				Application["tbComputationUnit"] = Helper.Query("select * from tbComputationUnit");
			}
			if(Application["tbInventory"] == null)
			{
				Application["tbInventory"] = Helper.Query("select * from tbInventory");
			}
		}

		public void DataGridToExcel(DataGrid dg,string strFileName)
		{
			Response.Clear();
			//Response.AddHeader("content-disposition", "attachment;filename="+strFileName+".xls");
			Response.AddHeader("content-disposition", "attachment;filename="+System.Web.HttpUtility.UrlEncode(strFileName)+".xls");
			//Response.Charset = "";
			Response.Charset = "GB2312";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			dg.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();
		}
		private void wfmBase_Error(object sender, System.EventArgs e)
		{
			// 记录错误日志 
			Exception errorLast = Server.GetLastError();
			if (errorLast is ConcurrentException || errorLast.InnerException is ConcurrentException)
			{
				Server.ClearError();
				Popup("其它用户修改或删除了当前信息，页面刷新获取了最新的数据！");
				Server.Transfer(Request.Url.PathAndQuery);
				return;
			}
			else if (errorLast is SqlException)
			{
				SqlException se = errorLast as SqlException;
				if (SqlErrorCode.Duplicate_Key == se.Number)
				{
					Server.ClearError();
					Popup("非常抱歉，将要创建的信息已存在！");
					Server.Transfer(Request.Url.PathAndQuery);
					return;
				}
			}

			LogAdapter.WriteInterfaceException(errorLast);
			Popup(errorLast.Message,"../wfmError.aspx");
			//Response.Redirect("../wfmError.aspx");

		}

		//填充下拉列表 （含所有代码）
		public void FillDropDownList(string showItem,DropDownList ddl)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application 中代码没有找到！");
				return;
			}		
			ddl.DataSource = dt;			
			ddl.DataValueField = "vcCommCode";
			ddl.DataTextField = "vcCommName";
			ddl.DataBind();
		}

		//填充用 filter 过滤过的下拉列表
		public void FillDropDownList(string showItem,DropDownList ddl,string filter)
		{
			DataTable dt = (DataTable)Application[showItem];			
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application 中代码没有找到！");
				return;
			}
			DataView dv = new DataView(dt);			
			dv.RowFilter = filter;
			//dv.Sort = "cnvcComments";
			ddl.DataSource = dv;			
			ddl.DataValueField = "vcCommCode";
			ddl.DataTextField = "vcCommName";
			ddl.DataBind();
		}		

		//填充用 filter 过滤过的带有自定义行的下拉列表
		public void FillDropDownList(string showItem,DropDownList ddl,string filter,string strNewItem)
		{
			FillDropDownList(showItem,ddl,filter);
			ddl.Items.Insert(0,strNewItem);			
		}		

		//填充用 filter 过滤过的带有自定义行的下拉列表，用自定datatable
		public void FillDropDownList(DataTable dt,DropDownList ddl,string strNewItem)
		{		
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("操作员信息代码没有找到！");
				return;
			}
			DataView dv = new DataView(dt);
			ddl.DataSource = dv;			
			ddl.DataValueField = "vcOperName";
			ddl.DataTextField = "vcOperName";
			ddl.DataBind();
			ddl.Items.Insert(0,strNewItem);			
		}
		//根据错误信息重定向到错误页面，子目录
		public void SetErrorMsgPageBydir(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("../../wfmFalse.aspx");
		}
		//标准页面重定向方法
		public void RedirectPage(string strPage)
		{
			if(strPage==null&&strPage.Trim().Length==0)
			{
				this.SetErrorMsgPage("页面错误！");
			}
			else
			{
				Response.Redirect(strPage,false);
			}
		}
		//根据错误信息重定向到错误页面，根目录
		public void SetErrorMsgPage(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("wfmFalse.aspx");
		}
		public void FillDropDownList(DataTable dt,DropDownList ddl)
		{
			DataView dv = new DataView(dt);
			ddl.DataSource = dv;			
			ddl.DataValueField = "cnvcCode";
			ddl.DataTextField = "cnvcName";
			ddl.DataBind();		
		}

		#region zhenghua@add 2010-04-14 导出EXCEL
		public string ExportTable(DataTable tb) 
		{ 
			StringBuilder sb = new StringBuilder(); 
			//data = ds.DataSetName + "\n"; 
			int count = 0; 

			//			foreach (DataTable tb in ds.Tables) 
			//			{ 
			//data += tb.TableName + "\n"; 
				
			sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">"); 
			sb.Append("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">"); 
			//写出列名 
			sb.Append("<tr style=\"font-weight: bold; white-space: nowrap;\">"); 
			foreach (DataColumn column in tb.Columns) 
			{ 
				if(column.ColumnName.IndexOf("0#柴油")>0||
					column.ColumnName.IndexOf("93#汽油")>0||
					column.ColumnName.IndexOf("97#汽油")>0)
				{
					string strcn = column.ColumnName;
					strcn = strcn.Replace("style='color:White;background-color:#000084;font-size:Small;font-weight:bold;'","style='font-weight: bold; white-space: nowrap;'");
					strcn = strcn.Replace("<table>","<table cellspacing='0' cellpadding='5' rules='all' border='1'>");
					sb.Append("<td colspan='3'>" + strcn + "</td>"); 
				}
				else if(column.ColumnName.IndexOf("汽油小计")>0)
				{
					string strcn = column.ColumnName;
					strcn = strcn.Replace("style='color:White;background-color:#000084;font-size:Small;font-weight:bold;'","style='font-weight: bold; white-space: nowrap;'");
					strcn = strcn.Replace("<table>","<table cellspacing='0' cellpadding='5' rules='all' border='1'>");
					sb.Append("<td>" + strcn + "</td>"); 
				}
				else
				{
					sb.Append("<td>" + column.ColumnName + "</td>"); 
				}
			} 
			sb.Append("</tr>"); 

			//写出数据 
			foreach (DataRow row in tb.Rows) 
			{ 
				sb.Append("<tr>"); 
				foreach (DataColumn column in tb.Columns) 
				{ 
					if (column.ColumnName.Equals("证件编号") || column.ColumnName.Equals("报名编号")) 
						sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>"); 
						////style="vnd.ms-excel.numberformat:@" 可以去除自动科学计数法的困扰 
					else 
						sb.Append("<td>" + row[column].ToString() + "</td>"); 
				} 
				sb.Append("</tr>"); 
				count++; 
			} 
			sb.Append("</table>"); 
			//} 

			return sb.ToString(); 
		} 

		//		public void ExportDsToXls(Page page, string sql)
		//		{
		//			ExportDsToXls(page, "FileName", sql);
		//		}
		//		public void ExportDsToXls(Page page, string fileName, string sql)
		//		{
		//			DataSet ds = DBUtil.GetDataSet(sql);
		//			if (ds != null) ExportDsToXls(page, fileName, ds);
		//		}
		public void ExportToXls(Page page, string strfile)
		{
			ExportToXls(page, "FileName", strfile);
		}
		public void ExportToXls(Page page, string fileName, string strfile)
		{
			page.Response.Clear();
			page.Response.Buffer = true;
			page.Response.Charset = "GB2312";
			//page.Response.Charset = "UTF-8";
			page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + System.DateTime.Now.ToString("_yyyyMMdd_hhmm") + ".xls");
			page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
			page.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
			page.EnableViewState = false;
			page.Response.Write(strfile);
			page.Response.End();
		}
		#endregion
	}
}
