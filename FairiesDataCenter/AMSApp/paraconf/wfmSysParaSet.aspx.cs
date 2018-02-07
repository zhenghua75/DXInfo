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

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmSysParaSet.
	/// </summary>
	public partial class wfmSysParaSet : wfmBase
	{
		protected System.Web.UI.HtmlControls.HtmlForm Form3;

		Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
//				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit!="CL001")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if (!IsPostBack )
				{
					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					m1=new Manager(strcons);

					DataTable dtgoods=m1.GetAllGoodsName();
					for(int i=0;i<dtgoods.Rows.Count;i++)
					{
						lbtcurrent.Items.Add(dtgoods.Rows[i]["vcGoodsName"].ToString());
						if(dtgoods.Rows[i]["cNewFlag"].ToString()=="1")
						{
							lbtNew.Items.Add(dtgoods.Rows[i]["vcGoodsName"].ToString());
						}	
					}

					DataTable dtig=m1.GetIgPara();
					if(dtig.Rows.Count>0)
					{
						txtFee.Text=dtig.Rows[0]["vcCommName"].ToString();
						txtIg.Text=dtig.Rows[0]["vcCommCode"].ToString();
					}
					else
					{
						txtFee.Text="";
						txtIg.Text="";
					}

					Hashtable htprom=m1.GetPromRate();
					if(htprom.ContainsKey("FP1"))
					{
						txtPromRate1.Text=htprom["FP1"].ToString();
					}
					else
					{
						txtPromRate1.Text="0";
					}
					if(htprom.ContainsKey("FP2"))
					{
						txtPromRate2.Text=htprom["FP2"].ToString();
					}
					else
					{
						txtPromRate2.Text="0";
					}

					if(htprom.ContainsKey("FP3"))
					{
						txtPromRate3.Text=htprom["FP3"].ToString();
					}
					else
					{
						txtPromRate3.Text="0";
					}
                    if (htprom.ContainsKey("FP4"))
                    {
                        txtPromRate4.Text = htprom["FP4"].ToString();
                    }
                    else
                    {
                        txtPromRate4.Text = "0";
                    }
                    if (htprom.ContainsKey("FP5"))
                    {
                        txtPromRate5.Text = htprom["FP5"].ToString();
                    }
                    else
                    {
                        txtPromRate5.Text = "0";
                    }
                    //this.txtPromRate1.Enabled = false;
                    //this.txtPromRate2.Enabled = false;
                    //this.txtPromRate3.Enabled = false;
                    //this.txtPromRate4.Enabled = false;
                    //this.txtPromRate5.Enabled = false;
                    //this.btProm.Enabled = false;

                    TextBox1.Text = m1.GetSPF();

                    Hashtable htCP = m1.GetCP();
                    TextBox2.Text = htCP["strcompanyName"].ToString();
                    TextBox3.Text = htCP["strimg"].ToString();

                    TextBox1.Enabled = false;
                    TextBox2.Enabled = false;
                    TextBox3.Enabled = false;

                    Button1.Enabled = false;
                    Button2.Enabled = false;
                    Button3.Enabled = false;
				}
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

		protected void btAdd_Click(object sender, System.EventArgs e)
		{
			if(lbtcurrent.Items.Count==0)
			{
				this.SetErrorMsgPageBydir("没有商品信息，请先录入商品信息！");
				return;
			}
			if(lbtNew.Items.Count>=10)
			{
				this.SetErrorMsgPageBydir("推荐新品只能设置10个！");
				return;
			}
			for(int i=0;i<lbtNew.Items.Count;i++)
			{
				if(lbtcurrent.SelectedItem.Text==lbtNew.Items[i].ToString())
				{
					return;
				}
			}
			if(lbtcurrent.SelectedItem==null)
			{
				this.SetErrorMsgPageBydir("请确定选中某个商品！");
				return;
			}
			else
			{
				lbtNew.Items.Add(lbtcurrent.SelectedItem.Text);
			}
		}

		protected void btDel_Click(object sender, System.EventArgs e)
		{
			if(lbtNew.Items.Count==0)
			{
				this.SetErrorMsgPageBydir("目前还没有要推荐的商品！");
				return;
			}
			lbtNew.Items.Remove(lbtNew.SelectedItem);
		}

		protected void btNewGoods_Click(object sender, System.EventArgs e)
		{
			ArrayList al=new ArrayList();
			for(int i=0;i<lbtNew.Items.Count;i++)
			{
				al.Add(lbtNew.Items[i].Text.Trim());
			}
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(m1.UpdateGoodsNewFlag(al))
			{
				this.SetSuccMsgPageBydir("推荐新品设置成功！","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("推荐新品设置失败！");
				return;
			}
		}

		protected void btIg_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.CommStruct cos=new CMSMStruct.CommStruct();
			if(txtFee.Text.Trim()=="")
			{
				cos.strCommName="0";
			}
			else if(double.Parse(txtFee.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("消费金额不能小于0！");
				return;
			}
			else
			{
				cos.strCommName=txtFee.Text.Trim();
			}

			if(txtIg.Text.Trim()=="")
			{
				cos.strCommCode="0";
			}
			else if(double.Parse(txtIg.Text.Trim())<0)
			{
				this.SetErrorMsgPageBydir("赠送积分分值不能小于0！");
				return;
			}
			else
			{
				cos.strCommCode=txtIg.Text.Trim();
			}
			cos.strCommSign="IG";
			cos.strComments="积分赠送";

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(m1.UpdateIgComm(cos))
			{
				this.SetSuccMsgPageBydir("消费积分设置成功！","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("消费积分设置失败！");
				return;
			}
		}
        
        private void setPromHt(Hashtable htpar,TextBox txt, string commName, string commSign)
        {
            CMSMStruct.CommStruct cos = new CMSMStruct.CommStruct();
            if (txt.Text.Trim() == "")
            {
                cos.strCommCode = "0";
            }
            else if (double.Parse(txt.Text.Trim()) < 0)
            {
                this.SetErrorMsgPageBydir("充值赠款比例不能小于0！");
                return;
            }
            else
            {
                cos.strCommCode = txt.Text.Trim();
            }
            cos.strCommName = commName;
            cos.strCommSign = commSign;
            cos.strComments = "充值赠款";
            htpar.Add(commSign, cos);
        }
		protected void btProm_Click(object sender, System.EventArgs e)
		{
            Hashtable htpar = new Hashtable();
            setPromHt(htpar, txtPromRate1, "充值赠款100-200", "FP1");
            setPromHt(htpar, txtPromRate2, "充值赠款200-300", "FP2");
            setPromHt(htpar, txtPromRate3, "充值赠款300-400", "FP3");
            setPromHt(htpar, txtPromRate4, "充值赠款400-500", "FP4");
            setPromHt(htpar, txtPromRate5, "充值赠款500以上", "FP5");

			if(m1.UpdateFillPromComm(htpar))
			{
                this.SetSuccMsgPageBydir("充值赠款设置成功！", "paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
                this.SetErrorMsgPageBydir("充值赠款设置失败！");
				return;
			}

		}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //积分清零日期
            if (TextBox1.Text.Trim().Length == 0)
            {
                this.SetErrorMsgPageBydir("请输入积分清零日期");
            }
            if (m1.UpdateSPF(TextBox1.Text))
            {
                this.SetSuccMsgPageBydir("积分清零日期设置成功！", "paraconf/wfmSysParaSet.aspx");
                return;
            }
            else
            {
                this.SetErrorMsgPageBydir("积分清零日期设置失败！");
                return;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //公司名称
            if (TextBox2.Text.Trim().Length == 0)
            {
                this.SetErrorMsgPageBydir("请输入公司名称");
            }
            if (m1.UpdateCP_CompnayName(TextBox2.Text))
            {
                this.SetSuccMsgPageBydir("公司名称设置成功！", "paraconf/wfmSysParaSet.aspx");
                return;
            }
            else
            {
                this.SetErrorMsgPageBydir("公司名称设置失败！");
                return;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //图片
            if (TextBox3.Text.Trim().Length == 0)
            {
                this.SetErrorMsgPageBydir("请输入图片名称");
            }
            if (m1.UpdateCP_Img(TextBox3.Text))
            {
                this.SetSuccMsgPageBydir("图片设置成功！", "paraconf/wfmSysParaSet.aspx");
                return;
            }
            else
            {
                this.SetErrorMsgPageBydir("图片设置失败！");
                return;
            }
        }
	}
}
