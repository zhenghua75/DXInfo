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
	/// description ����WebForm�ĸ��࣬ʵ������ҳ��Ĺ��÷���������Ȩ��У���Session�Ĺ���
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

		//Ȩ��У��
		/********************************************************
		 * 
		 * �ж�session �Ƿ����			
		 * 
		 ********************************************************/
		protected void OperCheck()
		{			
			if(Session==null||Session["tbOper"]==null)				
				Response.Redirect("/BossWebApp/Sysinit/Exit.aspx");			
		}

        /************************************************************
		 *   
		 * ����Ϊҳ���ض��򲿷�
		 * 
		 * *********************************************************/
		//��׼ҳ���ض��򷽷�
		public void RedirectPage(string strPage)
		{
			if(strPage==null&&strPage.Trim().Length==0)
			{
				this.SetErrorMsgPage("ҳ�����");
			}
			else
			{
				Response.Redirect(strPage,false);
			}
		}

		//���ݴ�����Ϣ�ض��򵽴���ҳ�棬��Ŀ¼
		public void SetErrorMsgPage(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("wfmFalse.aspx");
		}

		//���ݴ�����Ϣ�ض��򵽴���ҳ�棬��Ŀ¼
		public void SetErrorMsgPageBydir(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("../wfmFalse.aspx");
		}

		//���ݴ�����Ϣ�ض��򵽴���ҳ�棬��Ŀ¼
		public void SetErrorMsgPageBy2dir(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("../../wfmFalse.aspx");
		}

		//���ݴ�����Ϣ�ض��򵽴���ҳ�棬��Ŀ¼��������ҳ
		public void SetErrorMsgPageBydirHistory(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			this.RedirectPage("../wfmFalseHistory.aspx");
		}

		//���ݴ�����Ϣ�ض��򵽴���ҳ�棬��Ŀ¼
		public void SetSuccMsgPage(string strMsg)
		{
			Session.Remove("CommMsgNext");
			Session["CommMsg"]=strMsg;
			Session["CommMsgReturn"]="";
			this.RedirectPage("wfmSuccess.aspx");
		}

		//���ݴ�����Ϣ�ض��򵽴���ҳ�棬��Ŀ¼
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
		/// ��������
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
		/// �ж��Ƿ�Ϊ��
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="strMessage"></param>
		/// <returns></returns>
		public bool JudgeIsNull(string strText,string strMessage)
		{
			if(strText.Trim().Length == 0)
			{
				Popup(strMessage+"����Ϊ��");
				return true;
			}
			return false;
		}
		/// <summary>
		/// �ж��Ƿ�������
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="strMessage"></param>
		/// <returns></returns>
		public bool JudgeIsNum(string strText,string strMessage)
		{
			if(!Regex.IsMatch(strText,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				Popup(strMessage+"����������");
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
		 * ����Ϊҳ���д���ת������
		 * 
	     * *********************************************************/
		#region 
		//��������б� �������д��룩
		public void FillDropDownList(string showItem,DropDownList ddl)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application �д���û���ҵ���");
				return;
			}		
			ddl.DataSource = dt;			
			ddl.DataValueField = "vcCommCode";
			ddl.DataTextField = "vcCommName";
			ddl.DataBind();
		}

		//����� filter ���˹��������б�
		public void FillDropDownList(string showItem,DropDownList ddl,string filter)
		{
			DataTable dt = (DataTable)Application[showItem];			
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application �д���û���ҵ���");
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

		//����� filter ���˹��Ĵ����Զ����е������б�
		public void FillDropDownList(string showItem,DropDownList ddl,string filter,string strNewItem)
		{
			FillDropDownList(showItem,ddl,filter);
			ddl.Items.Insert(0,strNewItem);			
		}		

		//����� filter ���˹��Ĵ����Զ����е������б����Զ�datatable
		public void FillDropDownList(DataTable dt,DropDownList ddl,string strNewItem)
		{		
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("������Ϣû���ҵ���");
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

		//���checkbox�б� �������д��룩
		public void FillCheckBoxList(string showItem,CheckBoxList cbl)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application �д���û���ҵ���");
				return;
			}				
			cbl.DataSource = dt;			
			cbl.DataValueField = "vcCommCode";
			cbl.DataTextField = "vcCommName";
			cbl.DataBind();
		}

		//���checkbox�б� �������д��룩
		public void FillCheckBoxList(string showItem,CheckBoxList cbl,string filter)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application �д���û���ҵ���");
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

		//���radio button�б� �������д��룩
		public void FillRadioButtonList(string showItem,RadioButtonList rbl)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application �д���û���ҵ���");
				return;
			}				
			rbl.DataSource = dt;			
			rbl.DataValueField = "cnvcID";
			rbl.DataTextField = "cnvcComments";
			rbl.DataBind();
		}

		//���radio button�б� ����filter���˹��Ĵ��룩
		public void FillRadioButtonList(string showItem,RadioButtonList rbl,string filter)
		{
			//this.FillApplication(showItem);
			DataTable dt = (DataTable)Application[showItem];
			if(dt==null)
			{
				this.SetErrorMsgPageBydir("Application �д���û���ҵ���");
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


		//DataTable����Ĵ���ת��(ת��ԭ���е�ֵ,��������)
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


		//DataTable����Ĵ���ת��
		public void DataTableConvert(DataTable dt,string columnName,string showItem)
		{
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//�ж������Ƿ���ڣ��Ѿ����ھͲ���ӣ������ھ����
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

		//DataTable����Ĵ���ת��(��ѯͨ���������е绰���� ���С�*��ת��Ϊ�����С�)
		public void DataTableSvcTypeConvert(DataTable dt,string columnName,string showItem,string filter,string svcType)
		{
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//�ж������Ƿ���ڣ��Ѿ����ھͲ���ӣ������ھ����
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
							strTemp = "196ҵ��";
						}
						else if(strTemp == "IP")
						{
							strTemp = "IPҵ��";
						}
						break;
					case "PrepayFee":
						if(strTemp == "*")
						{
							strTemp = "���ʺ�";
						}
						break;
					case "AcctFee":
						if(strTemp == "*")
						{
							strTemp = "����";
						}
						break;
					default:
						break;
				}
				if(!strTemp.Equals(dr[columnName].ToString()))
					dr[strCommentColumnName] = strTemp;			
			}	
		}	
	
		//DataTable����Ĵ���ת��
		public void DataTableMultiConvert(DataTable dt,string columnName,string showItem,string filter)
		{
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//�ж������Ƿ���ڣ��Ѿ����ھͲ���ӣ������ھ����
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
	
		//DataTable����Ĵ���ת��
		public void DataTableConvert(DataTable dt,string columnName,string showItem,string filter)
		{
			string strTemp ;			
			string strCommentColumnName = columnName+"Comments";
			//�ж������Ƿ���ڣ��Ѿ����ھͲ���ӣ������ھ����
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

		// id -> remark ת��		
		public string CodeConvert(string showItem,string selectId)
		{
			DataTable dt=(DataTable)Application[showItem];
			string strRemark = selectId;
			if(dt==null)
			{
				throw new Exception("Application �д���û���ҵ���");
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
		
		//���� selectId ����tbCodeName  ���е� ����ע��
		public string CodeConvert(string showItem,string selectId,string filter)
		{
			DataTable dt=(DataTable)Application[showItem];			
			string strRemark ;
			if(dt==null)
			{
				throw new Exception("Application �д���û���ҵ���");
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
	

		// ����ѡ����id �趨��ʾ�������б�
		protected void SetDropDownListSelectedIndex(DropDownList ddl,string strSelectedId){
			ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(strSelectedId));
		}	
	
		// ����ѡ����id �趨��ʾ��RadioButtionList
		protected void SetRadioButtonListSelectedIndex(RadioButtonList rbList,string strSelectedId)
		{
			rbList.SelectedIndex = rbList.Items.IndexOf(rbList.Items.FindByValue(strSelectedId));
		}	

	
		/// <summary>
		/// ����ѡ����id �趨��ʾ��CheckBoxList
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
		///  ��ʾ���߰���
		/// </summary>
		/// <param name="hyperLink"></param>
		protected void SetHelpLink(HyperLink hyperLink)
		{
//			string strUrl = ((TableMenu)Session[ConstApp.COMMON_CURRENT_MENU]).strHelpUrl;
//			if(strUrl.Length>0)
//			{
//				hyperLink.NavigateUrl ="javascript:openwin('"+strUrl+"')";
//				hyperLink.Text = "����˵��";	
//				hyperLink.Font.Size = FontUnit.XSmall;
//			}
//			else
//			{
//				hyperLink.Text = "";	
//			}
		}

		#region add by wdx 2005.5.9
		//DataTable����Ĵ���ת��(�й�������)
		public void tbConvert(DataTable dt,string columnName,string showTable,string strFilter)
		{
			/// <summary>
			/// dt����ת���ı�����columnName:��ת���ı���ֶ���,showTable:Ҫת���ı�����tabColumnCode:Ҫת���ı��ֶδ�������,
			/// tabColumnName:Ҫת���ı��ֶ���������,strFilter:Ҫת���ı�Ĺ�������
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

		//�й�������
		public string _Convert(string tabName,string columnValue,string tabColumnCode,string tabColumnName,string strFilter)
		{
			/// <summary>
			/// tabName��Ҫת���ı�����columnValue:��ת���ı���ֶ�ֵ,tabColumnCode:Ҫת���ı��ֶδ�������,tabColumnName:Ҫת���ı��ֶ���������
			/// </summary>
			
			//			DataTable dt=(DataTable)dat_dsSys.dsSys.Tables[tabName];
			DataView dv = new DataView((DataTable)Application[tabName]);
			dv.RowFilter = strFilter;//��������		
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

		//DataTable����Ĵ���ת��(�޹�������)
		public void tbConvert(DataTable dt,string columnName,string showTable)
		{
			/// <summary>
			/// dt����ת���ı�����columnName:��ת���ı���ֶ���,showTable:Ҫת���ı�����tabColumnCode:Ҫת���ı��ֶδ�������,
			/// tabColumnName:Ҫת���ı��ֶ���������,strFilter:Ҫת���ı�Ĺ�������
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

		//�޹�������
		public string _Convert(string tabName,string columnValue,string tabColumnCode,string tabColumnName)
		{
			/// <summary>
			/// tabName��Ҫת���ı�����columnValue:��ת���ı���ֶ�ֵ,tabColumnCode:Ҫת���ı��ֶδ�������,tabColumnName:Ҫת���ı��ֶ���������
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
		//��ͨDataTable����ת��
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
		//�����ֶδ���ת�����й�������
		public string singleConvert(string tabName,string Value,string strFilter)
		{
			/// <summary>
			/// tabName��Ҫת���ı�����columnValue:��ת���ı���ֶ�ֵ,tabColumnCode:Ҫת���ı��ֶδ�������,tabColumnName:Ҫת���ı��ֶ���������
			/// </summary>
			
			string Code="cnvcID";
			string Name="cnvcComments";
			DataView dv = new DataView((DataTable)Application[tabName]);
			dv.RowFilter = strFilter;//��������		
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
		//ȥ���ַ����������ַ�
		public string stringTxt(string err)
		{
			err=err.Replace("'"," ");
			err=err.Replace("\r"," ");
			err=err.Replace("\n"," ");
			err=err.Replace("\t"," ");
			return err;
		}
		//����ת__��������
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

		#region ������Excel
		//ȥ��DagaGrid��HeadText��������Ϊ�����ƹ淶���ַ�
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
//				Excel.Worksheet xSheet = (Excel.Worksheet)xbook.Sheets["ҵ����"];//�õ�Sheet
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
//				SaveFileDialog1.Filter = "Excel�ļ���*.xls��|*.xls";
//				SaveFileDialog1.FileName="�������ҵ��������" + DateTime.Now.ToShortDateString() + ".xls";
//				if(SaveFileDialog1.ShowDialog() == DialogResult.OK)
//				{
//					xbook.SaveCopyAs(SaveFileDialog1.FileName);//���
//					xbook.Close(false, Application.StartupPath+@"\BusiIncomeModel.xls", Missing.Value);//�ر�
//					xSheet = null;
//					xbook = null;
//					xapp.Quit();
//					xapp = null;
//				}
//				else
//				{
//					xbook.Close(false, Missing.Value, Missing.Value);//�ر�
//					xSheet = null;
//					xbook = null;
//					xapp.Quit();
//					xapp = null;
//				}
//			}
//			catch(Exception err)
//			{
//				MessageBox.Show("����ʱ���������ԣ�","ϵͳ��ʾ",MessageBoxButtons.OK ,System.Windows.Forms.MessageBoxIcon.Error );
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
