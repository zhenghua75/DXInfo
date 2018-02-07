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
using BusiComm;
using CommCenter;
using System.Management;
using AMSApp.zhenghua.Business;
using System.Runtime.InteropServices;
using System.Web.Security;

namespace AMSApp
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
	public class _default2 : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden txtMACAddr;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden txtIPAddr;
		protected System.Web.UI.WebControls.TextBox txtLoginID;
		protected System.Web.UI.WebControls.TextBox txtPwd;
        //protected System.Web.UI.WebControls.Button Button1;
        protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtDNSName;

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

		private void Page_Load(object sender, System.EventArgs e)
		{
            

            //if(!this.IsPostBack)
            //{
            //    if(Session!=null)
            //    {
            //        //Session.RemoveAll();
            //        Session.Abandon();
            //    }	
            //}		
	
			// Put user code to initialize the page here 
//			DataTable dttmp=(DataTable)Application["MAC"];
//			if(dttmp!=null||dttmp.Rows.Count>0)
//			{
//				bool okflag=false;
//				ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
//				ManagementObjectCollection moc2 = mc.GetInstances();
//				foreach(ManagementObject mo in moc2)
//				{
//					if((bool)mo["IPEnabled"] == true)
//					{
//						for(int i=0;i<dttmp.Rows.Count;i++)
//						{
//							if(dttmp.Rows[i][0].ToString()==mo["MacAddress"].ToString())
//							{
//								okflag=true;
//								mo.Dispose();
//								break;
//							}
//							mo.Dispose();
//						}
//					}
//					if(okflag)
//					{
//						break;
//					}
//				}
//
//				if(!okflag)
//				{
//					Response.Redirect("sorry.htm");
//					return;
//				}
//			}
//			else
//			{
//				Response.Redirect("sorry.htm");
//				return;
//			}
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
			//this.Button1.Click += new System.EventHandler(this.Button1_Click);
            this.ImageButton1.Click += new ImageClickEventHandler(ImageButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

        void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //throw new NotImplementedException();
            LogIn();
        }
		#endregion

        private string getMac()
        {
            string mac_dest = "";
            try
            {
                string userip = Request.UserHostAddress;
                string strClientIP = Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //目的地的ip
                Int32 lhost = inet_addr("");   //本地服务器的ip
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                if (mac_src == "0")
                {
                    //if (userip == "127.0.0.1")
                    //    Response.Write("正在访问Localhost!");
                    //else
                    //    Response.Write("欢迎来自IP为" + userip + "的朋友！" + "<br>");
                    return "";
                }

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = ":" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
                //Response.Write("欢迎来自IP为" + userip + "<br>" + ",MAC地址为" + mac_dest + "的朋友！"
                // + "<br>");

            }
            catch (Exception err)
            {
                Response.Write(err.Message);
            }
            return mac_dest;
        }
		//根据错误信息重定向到错误页面，根目录
		public void SetErrorMsgPage(string strMsg)
		{			
			Session["CommMsg"]=strMsg;
			Response.Redirect("wfmFalse.aspx",false);
		}

		private void LogIn()
		{
			try
			{
				string strLoginid=this.txtLoginID.Text.Trim();

				DataTable dtMac=(DataTable)Application["MAC"];
				if(dtMac==null || dtMac.Rows.Count==0 )
				{
					Response.Redirect("sorry.htm");
					return;
					
				}
				else
				{
                    string strmac = getMac();//this.Request.Form["txtMACAddr"].ToString();
                    
					AMSLog clog=new AMSLog();
					clog.WriteLine("LoginID:"+strLoginid+";    Mac:"+strmac+";");
					//					if (strLoginid !="myj"  || strLoginid !="admin")
					//					{
					//						strmac="1";
					//						
					//					}
					if(strmac=="")
					{
						
						Response.Redirect("sorry.htm");
						return;
					}

					else
					{
						bool okflag=false;
						if( strLoginid=="admin")
						{
							okflag=true;
						}
						else
						{
							for(int i=0;i<dtMac.Rows.Count;i++)
							{
								if(dtMac.Rows[i][0].ToString()==strmac)
								{
									okflag=true;
									break;
								}
							}
						}
						if(!okflag)
						{
							Response.Redirect("nopromexplor.htm");
							return;
						}
						else
						{
							//							string strLoginid=this.txtLoginID.Text.Trim();
							string strpwd=this.txtPwd.Text.Trim();
							if(strLoginid==""||strpwd=="")
							{
								this.SetErrorMsgPage("请输入用户名和密码！");
							}
							else
							{
								Hashtable htapp=(Hashtable)Application["appconf"];
								string strcons=(string)htapp["cons"];
								Manager m1=new Manager(strcons);
								CMSMStruct.LoginStruct ls1=m1.GetLoginInfo(strLoginid);
								if(ls1==null)
								{
									this.SetErrorMsgPage("对不起，用户不存在！");
									Session["Login"]=null;
								}
								else
								{
									if(ls1.strPwd!=strpwd)
									{
										this.SetErrorMsgPage("对不起，密码不正确！");
									}
									else
									{
										DataTable dtDeptMap=(DataTable)Application["DeptMapInfo"];
										foreach(DataRow dr in dtDeptMap.Rows)
										{
											if(dr["cnvcOldDeptID"].ToString()==ls1.strDeptID)
											{
												ls1.strNewDeptID=dr["cnvcNewDeptID"].ToString();
												break;
											}
										}
										CMSMStruct.OperStruct OperNew=new CMSMStruct.OperStruct();
										OperNew.strDeptID=ls1.strDeptID;
										OperNew.strOperID=ls1.strLoginID;
                                        OperNew.strMacAddress = strmac;//this.Request.Form["txtMACAddr"].ToString();
										m1.InsertOperLog(OperNew);
										Session["Login"]=ls1;
                                        
                                        FormsAuthentication.RedirectFromLoginPage(ls1.strLoginID,false);
										//Session["tbNotice"] = Helper.Query("select cnnNoticeID,cnvcComments,Convert(varchar(10),cndReleaseDate,21) as cndReleaseDate from tbNotice where cnvcIsActive ='1'");
										//Response.Redirect("wfmMain.aspx",false);
									}
								}
							}
						}
					}
				}
			}
			catch(Exception er)
			{
				AMSLog clog=new AMSLog();
				clog.WriteLine(er);
				Response.Redirect("sorry.htm");
			}
		}

	}
}
