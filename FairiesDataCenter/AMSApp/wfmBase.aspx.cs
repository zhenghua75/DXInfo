using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AMSApp
{
	/// <summary>
	/// description 所有WebForm的父类，实现所有页面的公用方法，还有权限校验和Session的管理
	/// date 2003-5-4
	/// author yinkai
	/// version 1.0
	/// </summary>
	public class wfmBase : System.Web.UI.Page
	{
		public CommCenter.AMSLog clog=new CommCenter.AMSLog();
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Session["Login"] = null;
            //if(Session["Login"]==null)
            //{											
            //    Response.Redirect(this.Request.ApplicationPath+"/Exit.aspx");
            //    //return;
            //}
		}

		//权限校验
		/********************************************************
		 * 
		 * 判断session 是否存在			
		 * 
		 ********************************************************/
		protected void OperCheck()
		{			
			if(Session==null||Session["tbOper"]==null)				
				Response.Redirect("/BossWebApp/Sysinit/Exit.aspx");			
		}

        /************************************************************
		 *   
		 * 以下为页面重定向部分
		 * 
		 * *********************************************************/
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

		//根据错误信息重定向到错误页面，子目录
		public void SetErrorMsgPageBydir(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("../wfmFalse.aspx");
		}

		//根据错误信息重定向到错误页面，子目录
		public void SetErrorMsgPageBy2dir(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("../../wfmFalse.aspx");
		}

		//根据错误信息重定向到错误页面，子目录，返回上页
		public void SetErrorMsgPageBydirHistory(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("../wfmFalseHistory.aspx");
		}

		//根据错误信息重定向到错误页面，根目录
		public void SetSuccMsgPage(string strMsg)
		{
			Session.Remove("CommMsgNext");
			Session["CommMsg"]=strMsg;
			Session["CommMsgReturn"]="";
			this.RedirectPage("wfmSuccess.aspx");
		}

		//根据错误信息重定向到错误页面，子目录
		public void SetSuccMsgPageBydir(string strMsg,string returnpage)
		{
			Session.Remove("CommMsgNext");
			Session["CommMsg"]=strMsg;
			Session["CommMsgReturn"]=returnpage;
			this.RedirectPage("../wfmSuccess.aspx");
		}

		public void SetSuccMsgPageBydir(string strMsg,string returnpage,string nextpage,string strnextname)
		{
			Session.Remove("CommMsgNext");
			Session["CommMsg"]=strMsg;
			Session["CommMsgReturn"]=returnpage;
			Session["CommMsgNext"]=nextpage;
			Session["CommMsgNextName"]=strnextname;
			this.RedirectPage("../wfmSuccess2bt.aspx");
		}

		/// <summary>
		/// 弹出窗口
		/// </summary>
		/// <param name="strComments"></param>
		public void Popup(string strComments)
		{
			this.Response.Write("<script>alert('"+UrnHtml(strComments)+"');</script>");
		}
		public string UrnHtml(string strHtml)
		{
			string[] aryReg ={ "(",")","'", "<", ">", "%","\"\"", ",", ".", ">=", "=<", "-", "_", ";", "||", "[", "]", "&", "/", "-", "|"," ", };
			for (int i = 0; i < aryReg.Length; i++)
			{
				strHtml = strHtml.Replace(aryReg[i], string.Empty);
			}

			return strHtml;
		} 

		/// <summary>
		/// 判断是否为空
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="strMessage"></param>
		/// <returns></returns>
		public bool JudgeIsNull(string strText,string strMessage)
		{
			if(strText.Trim().Length == 0)
			{
				Popup(strMessage+"不能为空");
				return true;
			}
			return false;
		}
		/// <summary>
		/// 判断是否是数字
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="strMessage"></param>
		/// <returns></returns>
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
				return false;
			}
			return true;
		}
		public bool JudgeIsDate(string strDate)
		{
			string strRegex = @"(?:(?:(?:(?:19|20)(?:(?:[02468][048])|(?:[13579][26]))-0?2-29))|(?:\d{4}-0?(?!(?:[4|6|9]-31)|(?:2-29)|(?:11-31)|(?:2-30)|(?:2-31))((?!0|(?:1[3-9]))(?:1?[0-9])-(?!0|(?:3[2-9]))(?:[1-3]?[0-9]))))\s+(?!(?:2[4-9]))(?:[1-2]?[0-9]):0?0:0?0";
			Regex re = new Regex(strRegex);
			if (re.IsMatch(strDate))
				return (true);
			else
				return (false);
		}

		public bool JudgeIs24Hour(string strText)
		{
			try
			{
				if(int.Parse(strText)<0||int.Parse(strText)>24)
				{				
					return false;
				}
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		public bool JudgeIs60Minute(string strText)
		{
			try
			{
				if(int.Parse(strText)<0||int.Parse(strText)>60)
				{				
					return false;
				}
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		/************************************************************
		 *   
		 * 以下为页面中代码转换部分
		 * 
	     * *********************************************************/
		#region 
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
				this.SetErrorMsgPageBydir("加载信息没有找到！");
				return;
			}
			DataView dv = new DataView(dt);
			ddl.DataSource = dv;			
			ddl.DataValueField = "vcOperName";
			ddl.DataTextField = "vcOperName";
			ddl.DataBind();
			ddl.Items.Insert(0,strNewItem);			
		}

		public void FillDropDownList(DataTable dt,DropDownList ddl)
		{
			DataView dv = new DataView(dt);
			ddl.DataSource = dv;			
			ddl.DataValueField = "cnvcCode";
			ddl.DataTextField = "cnvcName";
			ddl.DataBind();		
		}

		//填充checkbox列表 （含所有代码）
		public void FillCheckBoxList(string showItem,CheckBoxList cbl)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application 中代码没有找到！");
				return;
			}				
			cbl.DataSource = dt;			
			cbl.DataValueField = "vcCommCode";
			cbl.DataTextField = "vcCommName";
			cbl.DataBind();
		}

		//填充checkbox列表 （含所有代码）
		public void FillCheckBoxList(string showItem,CheckBoxList cbl,string filter)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application 中代码没有找到！");
				return;
			}		
			DataView dv = new DataView(dt);
			dv.RowFilter = filter;
			//dv.Sort = "cnvcComments";			
			cbl.DataSource = dv;			
			cbl.DataValueField = "vcCommCode";
			cbl.DataTextField = "vcCommName";
			cbl.DataBind();
		}

		//填充radio button列表 （含所有代码）
		public void FillRadioButtonList(string showItem,RadioButtonList rbl)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application 中代码没有找到！");
				return;
			}				
			rbl.DataSource = dt;			
			rbl.DataValueField = "cnvcID";
			rbl.DataTextField = "cnvcComments";
			rbl.DataBind();
		}

		//填充radio button列表 （含filter过滤过的代码）
		public void FillRadioButtonList(string showItem,RadioButtonList rbl,string filter)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application 中代码没有找到！");
				return;
			}		
			DataView dv = new DataView(dt);
			dv.RowFilter = filter;
			//dv.Sort = "cnvcComments";			
			rbl.DataSource = dv;			
			rbl.DataValueField = "cnvcID";
			rbl.DataTextField = "cnvcComments";
			rbl.DataBind();
		}


		//DataTable里面的代码转换(转换原有列的值,无新增列)
		public void TableConvert(DataTable dt,string columnName,string showItem)
		{
			string strTemp ;
						
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(showItem,dr[columnName].ToString());					
				dr[columnName] = strTemp;					
			}
		}	
	
		public void TableConvert(DataTable dt,string columnName,string showItem,string filter)
		{
			string strTemp ;
			
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(showItem,dr[columnName].ToString(),filter);					
				dr[columnName] = strTemp;					
			}
		}		


		//DataTable里面的代码转换
		public void DataTableConvert(DataTable dt,string columnName,string showItem)
		{
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//判断新列是否存在，已经存在就不添加，不存在就添加
			if(dt.Columns[strCommentColumnName]==null)
			{			
				dt.Columns.Add(strCommentColumnName,typeof(string));
			}
			foreach (DataRow dr in dt.Rows)
			{				
				strTemp = this.CodeConvert(showItem,dr[columnName].ToString());	
				dr[strCommentColumnName] = strTemp;
			}			
		}	

		//DataTable里面的代码转换(查询通话量报表中电话种类 其中‘*’转换为‘所有’)
		public void DataTableSvcTypeConvert(DataTable dt,string columnName,string showItem,string filter,string svcType)
		{
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//判断新列是否存在，已经存在就不添加，不存在就添加
			if(dt.Columns[strCommentColumnName]==null)
			{			
				dt.Columns.Add(strCommentColumnName,typeof(string));
			}
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(showItem,dr[columnName].ToString(),filter);
				switch(svcType)
				{
					case "Source":
						if(strTemp == "L19X")
						{
							strTemp = "196业务";
						}
						else if(strTemp == "IP")
						{
							strTemp = "IP业务";
						}
						break;
					case "PrepayFee":
						if(strTemp == "*")
						{
							strTemp = "主帐号";
						}
						break;
					case "AcctFee":
						if(strTemp == "*")
						{
							strTemp = "所有";
						}
						break;
					default:
						break;
				}
				if(!strTemp.Equals(dr[columnName].ToString()))
					dr[strCommentColumnName] = strTemp;			
			}	
		}	
	
		//DataTable里面的代码转换
		public void DataTableMultiConvert(DataTable dt,string columnName,string showItem,string filter)
		{
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//判断新列是否存在，已经存在就不添加，不存在就添加
			if(dt.Columns[strCommentColumnName]==null)
			{			
				dt.Columns.Add(strCommentColumnName,typeof(string));
			}
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(showItem,dr[columnName].ToString(),filter);		
				if(!strTemp.Equals(dr[columnName].ToString()))
					dr[strCommentColumnName] = strTemp;			
			}	
		}	
	
		//DataTable里面的代码转换
		public void DataTableConvert(DataTable dt,string columnName,string showItem,string filter)
		{
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//判断新列是否存在，已经存在就不添加，不存在就添加
			if(dt.Columns[strCommentColumnName]==null)
			{			
				dt.Columns.Add(strCommentColumnName,typeof(string));
			}
			foreach (DataRow dr in dt.Rows)
			{
				strTemp = this.CodeConvert(showItem,dr[columnName].ToString(),filter);					
				dr[strCommentColumnName] = strTemp;					
			}
		}		

		// id -> remark 转换		
		public string CodeConvert(string showItem,string selectId)
		{
			DataTable dt=(DataTable)Application[showItem];
			string strRemark = selectId;
			if(dt==null)
			{
				throw new Exception("Application 中代码没有找到！");
			}
			for(int i = 0;i<dt.Rows.Count;i++)
			{
				if(dt.Rows[i]["vcCommCode"].ToString().Equals(selectId))
				{
					strRemark = dt.Rows[i]["vcCommName"].ToString();
					break;
				}
			}
			return strRemark;	
		}
		
		//根据 selectId 返回tbCodeName  表中的 中文注释
		public string CodeConvert(string showItem,string selectId,string filter)
		{
			DataTable dt=(DataTable)Application[showItem];			
			string strRemark ;
			if(dt==null)
			{
				throw new Exception("Application 中代码没有找到！");
			}			
			DataView dw = new DataView(dt);			
			string strfilter = filter +" and vcCommCode = '"+selectId+"'"; 
			dw.RowFilter = strfilter;			
			if(dw.Count >= 1)
			{
				strRemark = dw[0].Row["vcCommName"].ToString();
			}
			else
			{
				strRemark = selectId;
			}
			return strRemark;				
		}
	

		// 根据选定的id 设定显示的下拉列表
		protected void SetDropDownListSelectedIndex(DropDownList ddl,string strSelectedId){
			ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(strSelectedId));
		}	
	
		// 根据选定的id 设定显示的RadioButtionList
		protected void SetRadioButtonListSelectedIndex(RadioButtonList rbList,string strSelectedId)
		{
			rbList.SelectedIndex = rbList.Items.IndexOf(rbList.Items.FindByValue(strSelectedId));
		}	

	
		/// <summary>
		/// 根据选定的id 设定显示的CheckBoxList
		/// </summary>
		/// <param name="cbList">CheckBoxList</param>
		/// <param name="alSelectedId">ArrayList</param>
		protected void SetCheckBoxListSelectedIndex(CheckBoxList cbList,ArrayList alSelectedId)
		{
			
			for(int j=0;j<cbList.Items.Count;j++)
			{
				for(int i= 0;i<alSelectedId.Count;i++)
				{
					if(alSelectedId[i].ToString().Equals(cbList.Items[j].Value))
					{
						cbList.Items[j].Selected = true;
						break;
					}
					else
					{
						cbList.Items[j].Selected = false;
					}
				}
			}
		}	
        #endregion

		/// <summary>
		///  显示在线帮助
		/// </summary>
		/// <param name="hyperLink"></param>
		protected void SetHelpLink(HyperLink hyperLink)
		{
//			string strUrl = ((TableMenu)Session[ConstApp.COMMON_CURRENT_MENU]).strHelpUrl;
//			if(strUrl.Length>0)
//			{
//				hyperLink.NavigateUrl ="javascript:openwin('"+strUrl+"')";
//				hyperLink.Text = "操作说明";	
//				hyperLink.Font.Size = FontUnit.XSmall;
//			}
//			else
//			{
//				hyperLink.Text = "";	
//			}
		}

		#region add by wdx 2005.5.9
		//DataTable里面的代码转换(有过滤条件)
		public void tbConvert(DataTable dt,string columnName,string showTable,string strFilter)
		{
			/// <summary>
			/// dt：被转换的表名，columnName:被转换的表的字段名,showTable:要转换的表名，tabColumnCode:要转换的表字段代码列名,
			/// tabColumnName:要转换的表字段内容列名,strFilter:要转换的表的过滤条件
			/// </summary>
			string tabColumnCode,tabColumnName;
			tabColumnCode="cnvcID";
			tabColumnName="cnvcComments";
			string strTemp ;
			string strCommentColumnName = columnName;
	
			foreach (DataRow dr in dt.Rows)
			{				
				strTemp = this._Convert(showTable,dr[columnName].ToString(),tabColumnCode,tabColumnName,strFilter);	
				dr[strCommentColumnName] = strTemp;
			}
		}

		//有过滤条件
		public string _Convert(string tabName,string columnValue,string tabColumnCode,string tabColumnName,string strFilter)
		{
			/// <summary>
			/// tabName：要转换的表名，columnValue:被转换的表的字段值,tabColumnCode:要转换的表字段代码列名,tabColumnName:要转换的表字段内容列名
			/// </summary>
			
			//			DataTable dt=(DataTable)dat_dsSys.dsSys.Tables[tabName];
			DataView dv = new DataView((DataTable)Application[tabName]);
			dv.RowFilter = strFilter;//过滤条件		
			string strRemark = columnValue;

			for(int i = 0;i<dv.Count;i++)
			{
				if(dv[i][""+tabColumnCode+""].ToString().Equals(columnValue))
				{
					strRemark = dv[i][""+tabColumnName+""].ToString();
					break;
				}
			}
			return strRemark;	
		}

		//DataTable里面的代码转换(无过滤条件)
		public void tbConvert(DataTable dt,string columnName,string showTable)
		{
			/// <summary>
			/// dt：被转换的表名，columnName:被转换的表的字段名,showTable:要转换的表名，tabColumnCode:要转换的表字段代码列名,
			/// tabColumnName:要转换的表字段内容列名,strFilter:要转换的表的过滤条件
			/// </summary>
			string tabColumnCode,tabColumnName;
			tabColumnCode="cnvcID";
			tabColumnName="cnvcComments";
			string strTemp ;
			string strCommentColumnName = columnName;
	
			foreach (DataRow dr in dt.Rows)
			{				
				strTemp = this._Convert(showTable,dr[columnName].ToString(),tabColumnCode,tabColumnName);	
				dr[strCommentColumnName] = strTemp;
			}			
		}

		//无过滤条件
		public string _Convert(string tabName,string columnValue,string tabColumnCode,string tabColumnName)
		{
			/// <summary>
			/// tabName：要转换的表名，columnValue:被转换的表的字段值,tabColumnCode:要转换的表字段代码列名,tabColumnName:要转换的表字段内容列名
			/// </summary>
			
			//			DataTable dt=(DataTable)dat_dsSys.dsSys.Tables[tabName];
			DataView dv = new DataView();
			dv = ((DataTable)Application[tabName]).DefaultView;
					
			string strRemark = columnValue;
		
			for(int i = 0;i<dv.Count;i++)
			{
				if(dv[i][""+tabColumnCode+""].ToString().Equals(columnValue))
				{
					strRemark = dv[i][""+tabColumnName+""].ToString();
					break;
				}
			}
			return strRemark;	
		}
		//普通DataTable代码转换
		public void tbConvert(string Code,string Comments,DataTable dt,string columnName)
		{
			string[] code=Code.Split(',');
			string[] comments=Comments.Split(',');
			int count=(code.Length<comments.Length)?code.Length:comments.Length;
			for(int i=0;i<count;i++)
			{
				if(!comments[i].Trim().Equals(""))
				{
					for(int j=0;j<dt.Rows.Count;j++)
					{
						if(dt.Rows[j][columnName].ToString().Trim()==code[i])
							dt.Rows[j][columnName]=comments[i];
					}
				}
			}
		}
		//单个字段代码转换，有过滤条件
		public string singleConvert(string tabName,string Value,string strFilter)
		{
			/// <summary>
			/// tabName：要转换的表名，columnValue:被转换的表的字段值,tabColumnCode:要转换的表字段代码列名,tabColumnName:要转换的表字段内容列名
			/// </summary>
			
			string Code="cnvcID";
			string Name="cnvcComments";
			DataView dv = new DataView((DataTable)Application[tabName]);
			dv.RowFilter = strFilter;//过滤条件		
			string strRemark = Value;

			for(int i = 0;i<dv.Count;i++)
			{
				if(dv[i][""+Code+""].ToString().Equals(Value))
				{
					strRemark = dv[i][""+Name+""].ToString();
					break;
				}
			}
			return strRemark;	
		}
		//去掉字符串中特殊字符
		public string stringTxt(string err)
		{
			err=err.Replace("'"," ");
			err=err.Replace("\r"," ");
			err=err.Replace("\n"," ");
			err=err.Replace("\t"," ");
			return err;
		}
		//代码转__名称描述
		public void CodeToName(DataTable dt,string Name)
		{
			string[] name= Name.Split(',');
			int count=(name.Length<dt.Columns.Count)?name.Length:dt.Columns.Count;
			for(int i=0;i<count;i++)
			{
				if(!name[i].Trim().Equals(""))
					dt.Columns[i].ColumnName=name[i];
			}
		}
		
		#endregion

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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		#region English to Chinese
//		protected void EnToCh(string strChinese,string strWidth,DataTable dt,System.Web.UI.WebControls.DataGrid dg)
//		{
//			if(dt!=null)
//			{
//				int col_count=0;
//				string[] str=strChinese.Split(',');
//				string[] wid=strWidth.Split(',');
//				System.Web.UI.da
//				System.Windows.Forms.DataGridTableStyle t_style=new System.Windows.Forms.DataGridTableStyle();
//				t_style.MappingName=dt.TableName;
//				System.Windows.Forms.DataGridColumnStyle[] c_style=new System.Windows.Forms.DataGridColumnStyle[str.Length];
//				dg.TableStyles.Clear();
//				col_count=(str.Length<dt.Columns.Count)?str.Length:dt.Columns.Count;
//				for(int i=0;i<col_count;i++)
//				{
//					c_style[i]=new System.Windows.Forms.DataGridTextBoxColumn();
//					c_style[i].MappingName=dt.Columns[i].ColumnName;
//					c_style[i].HeaderText=(str[i]!="")?str[i]:dt.Columns[i].ColumnName;
//					if(i<wid.Length && wid[i] != "")
//						c_style[i].Width=Convert.ToInt32(wid[i]);
//					t_style.GridColumnStyles.Add(c_style[i]);
//				}
//				t_style.AlternatingBackColor=System.Drawing.Color.WhiteSmoke;
//				t_style.BackColor=System.Drawing.Color.White;
//				t_style.GridLineColor=System.Drawing.Color.LightSkyBlue;
//				t_style.HeaderBackColor=Color.Gainsboro;
//				t_style.HeaderForeColor=System.Drawing.SystemColors.ControlText;
//				t_style.SelectionBackColor=System.Drawing.SystemColors.Info;
//				t_style.SelectionForeColor=Color.Blue;
//
//				dg.Capture=true;
//
//				dg.TableStyles.Add(t_style);
//				dg.BorderStyle=System.Windows.Forms.BorderStyle.Fixed3D;
//				dg.ReadOnly=true;
//			}
//		}
		#endregion

		#region 导出到Excel
		//去掉DagaGrid的HeadText不符合做为列名称规范的字符
		protected string replace(string str)
		{
			str=str.Replace("(","");
			str=str.Replace(")","");
			str=str.Replace("-","");
			return str;
		}

		private string repText(string str)
		{
			str=str.Replace("'","");
			return str;
		}

		protected void BusiIncomeExportToExcel(string tabname,string tabdate,DataTable dtIncome)
		{
//			try
//			{
//				Excel.Application xapp=new Excel.ApplicationClass();
//				Excel.Workbook xbook=xapp.Workbooks.Open(Application.StartupPath+@"\BusiIncomeModel.xls",Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
//				Excel.Worksheet xSheet = (Excel.Worksheet)xbook.Sheets["业务量"];//得到Sheet
//
//				xSheet.get_Range("A1", Missing.Value).Value2 = tabname;
//				xSheet.get_Range("A2", Missing.Value).Value2 = tabdate;
//				for(int i=1;i<dtIncome.Rows.Count-2;i++)
//				{
//					for(int j=1;j<8;j++)
//					{
//						xSheet.Cells[i+3,j+1] = dtIncome.Rows[i][j].ToString();
//					}
//				}
//
//				for(int i=1;i<8;i++)
//				{
//					xSheet.Cells[14,i+1] = dtIncome.Rows[11][i].ToString();
//				}
//
//				SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
//				SaveFileDialog1.Filter = "Excel文件（*.xls）|*.xls";
//				SaveFileDialog1.FileName="面包工坊业务量报表" + DateTime.Now.ToShortDateString() + ".xls";
//				if(SaveFileDialog1.ShowDialog() == DialogResult.OK)
//				{
//					xbook.SaveCopyAs(SaveFileDialog1.FileName);//另存
//					xbook.Close(false, Application.StartupPath+@"\BusiIncomeModel.xls", Missing.Value);//关闭
//					xSheet = null;
//					xbook = null;
//					xapp.Quit();
//					xapp = null;
//				}
//				else
//				{
//					xbook.Close(false, Missing.Value, Missing.Value);//关闭
//					xSheet = null;
//					xbook = null;
//					xapp.Quit();
//					xapp = null;
//				}
//			}
//			catch(Exception err)
//			{
//				MessageBox.Show("导出时出错，请重试！","系统提示",MessageBoxButtons.OK ,System.Windows.Forms.MessageBoxIcon.Error );
//				clog.WriteLine(err);
//			}
//			finally
//			{
//
//			}
		}
		#endregion
	}
}
