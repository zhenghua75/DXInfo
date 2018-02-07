using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommCenter;
using System.Web.Security;
using System.Data;
using System.Runtime.InteropServices;
using System.Collections;
using BusiComm;
using AMSApp.zhenghua.Business;

namespace AMSApp
{
    public partial class _default : System.Web.UI.Page
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            lblyanzheng.Text = CreateRandomCode(5);
            //.Focus();
        }
        protected string CreateRandomCode(int codeCount)
        {
            string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,k,m,n,p,q,r,s,t,u,v,w,x,y,z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(53);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        public void SetErrorMsgPage(string strMsg)
        {
            Session["CommMsg"] = strMsg;
            Response.Redirect("wfmFalse.aspx");
        }
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
                AMSLog clog = new AMSLog();
                clog.WriteLine("Mac_src:" + mac_src + ";");
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
        private void LogIn()
        {
            try
            {
                Session["Login"] = null;
                string strLoginid = this.txtLoginID.Text.Trim();
                FormsAuthentication.SetAuthCookie(strLoginid, false);
                DataTable dtMac = (DataTable)Application["MAC"];
                if (dtMac == null || dtMac.Rows.Count == 0)
                {
                    Response.Redirect("sorry.htm");
                    return;

                }
                else
                {
                    string strmac = "00:22:64:89:96:14";//getMac();

                    AMSLog clog = new AMSLog();
                    clog.WriteLine("LoginID:" + strLoginid + ";    Mac:" + strmac + ";");
                    if (strmac == "" && strLoginid!="admin")
                    {

                        Response.Redirect("sorry.htm");
                        return;
                    }

                    else
                    {
                        bool okflag = true;
                        //if (strLoginid == "admin")
                        //{
                        //    okflag = true;
                        //}
                        //else
                        //{
                        //    for (int i = 0; i < dtMac.Rows.Count; i++)
                        //    {
                        //        if (dtMac.Rows[i][0].ToString() == strmac)
                        //        {
                        //            okflag = true;
                        //            break;
                        //        }
                        //    }
                        //}
                        if (!okflag)
                        {
                            this.SetErrorMsgPage("对不起，你的计算机尚未授权访问本系统！");
                        }
                        else
                        {
                            string strpwd = this.txtPwd.Text.Trim();
                            if (strLoginid == "" || strpwd == "")
                            {
                                
                                this.SetErrorMsgPage("请输入用户名和密码！");
                            }
                            else
                            {
                                Hashtable htapp = (Hashtable)Application["appconf"];
                                string strcons = (string)htapp["cons"];
                                Manager m1 = new Manager(strcons);
                                CMSMStruct.LoginStruct ls1 = m1.GetLoginInfo(strLoginid);
                                if (ls1 == null)
                                {                                    
                                    this.SetErrorMsgPage("对不起，用户不存在！");
                                    
                                   
                                }
                                else
                                {
                                    FormsAuthentication.SetAuthCookie(ls1.strLoginID, false);
                                    if (ls1.strPwd != strpwd)
                                    {
                                        
                                        this.SetErrorMsgPage("对不起，密码不正确！");
                                    }
                                    else
                                    {
                                        DataTable dtDeptMap = (DataTable)Application["DeptMapInfo"];
                                        foreach (DataRow dr in dtDeptMap.Rows)
                                        {
                                            if (dr["cnvcOldDeptID"].ToString() == ls1.strDeptID)
                                            {
                                                ls1.strNewDeptID = dr["cnvcNewDeptID"].ToString();
                                                break;
                                            }
                                        }
                                        CMSMStruct.OperStruct OperNew = new CMSMStruct.OperStruct();
                                        OperNew.strDeptID = ls1.strDeptID;
                                        OperNew.strOperID = ls1.strLoginID;
                                        OperNew.strMacAddress = strmac;//this.Request.Form["txtMACAddr"].ToString();
                                        m1.InsertOperLog(OperNew);                                                                                
                                        //Session["tbNotice"] = Helper.Query("select cnnNoticeID,cnvcComments,Convert(varchar(10),cndReleaseDate,21) as cndReleaseDate from tbNotice where cnvcIsActive ='1'");
                                        Session["Login"] = ls1;
                                        Response.Redirect("wfmMain.aspx");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException tax)
            {
                AMSLog clog = new AMSLog();
                clog.WriteLine(tax);
            } 
            catch (Exception er)
            {
                AMSLog clog = new AMSLog();
                clog.WriteLine(er);
                this.SetErrorMsgPage(er.Message);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (txtyan.Text.Trim().ToUpper() != lblyanzheng.Text.Trim().ToUpper())
            {
                lblyan.Text = "验证码不正确";
                lblyanzheng.Text = CreateRandomCode(5);
                return;
            }
            LogIn();
        }

    }
}