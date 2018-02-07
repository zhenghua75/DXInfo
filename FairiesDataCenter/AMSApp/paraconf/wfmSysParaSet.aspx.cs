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
//					this.SetErrorMsgPageBydir("�Բ�����û��Ȩ��ʹ�ô˹��ܣ�");
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
				this.SetErrorMsgPageBydir("û����Ʒ��Ϣ������¼����Ʒ��Ϣ��");
				return;
			}
			if(lbtNew.Items.Count>=10)
			{
				this.SetErrorMsgPageBydir("�Ƽ���Ʒֻ������10����");
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
				this.SetErrorMsgPageBydir("��ȷ��ѡ��ĳ����Ʒ��");
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
				this.SetErrorMsgPageBydir("Ŀǰ��û��Ҫ�Ƽ�����Ʒ��");
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
				this.SetSuccMsgPageBydir("�Ƽ���Ʒ���óɹ���","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("�Ƽ���Ʒ����ʧ�ܣ�");
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
				this.SetErrorMsgPageBydir("���ѽ���С��0��");
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
				this.SetErrorMsgPageBydir("���ͻ��ַ�ֵ����С��0��");
				return;
			}
			else
			{
				cos.strCommCode=txtIg.Text.Trim();
			}
			cos.strCommSign="IG";
			cos.strComments="��������";

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(m1.UpdateIgComm(cos))
			{
				this.SetSuccMsgPageBydir("���ѻ������óɹ���","paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("���ѻ�������ʧ�ܣ�");
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
                this.SetErrorMsgPageBydir("��ֵ�����������С��0��");
                return;
            }
            else
            {
                cos.strCommCode = txt.Text.Trim();
            }
            cos.strCommName = commName;
            cos.strCommSign = commSign;
            cos.strComments = "��ֵ����";
            htpar.Add(commSign, cos);
        }
		protected void btProm_Click(object sender, System.EventArgs e)
		{
            Hashtable htpar = new Hashtable();
            setPromHt(htpar, txtPromRate1, "��ֵ����100-200", "FP1");
            setPromHt(htpar, txtPromRate2, "��ֵ����200-300", "FP2");
            setPromHt(htpar, txtPromRate3, "��ֵ����300-400", "FP3");
            setPromHt(htpar, txtPromRate4, "��ֵ����400-500", "FP4");
            setPromHt(htpar, txtPromRate5, "��ֵ����500����", "FP5");

			if(m1.UpdateFillPromComm(htpar))
			{
                this.SetSuccMsgPageBydir("��ֵ�������óɹ���", "paraconf/wfmSysParaSet.aspx");
				return;
			}
			else
			{
                this.SetErrorMsgPageBydir("��ֵ��������ʧ�ܣ�");
				return;
			}

		}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //������������
            if (TextBox1.Text.Trim().Length == 0)
            {
                this.SetErrorMsgPageBydir("�����������������");
            }
            if (m1.UpdateSPF(TextBox1.Text))
            {
                this.SetSuccMsgPageBydir("���������������óɹ���", "paraconf/wfmSysParaSet.aspx");
                return;
            }
            else
            {
                this.SetErrorMsgPageBydir("����������������ʧ�ܣ�");
                return;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //��˾����
            if (TextBox2.Text.Trim().Length == 0)
            {
                this.SetErrorMsgPageBydir("�����빫˾����");
            }
            if (m1.UpdateCP_CompnayName(TextBox2.Text))
            {
                this.SetSuccMsgPageBydir("��˾�������óɹ���", "paraconf/wfmSysParaSet.aspx");
                return;
            }
            else
            {
                this.SetErrorMsgPageBydir("��˾��������ʧ�ܣ�");
                return;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //ͼƬ
            if (TextBox3.Text.Trim().Length == 0)
            {
                this.SetErrorMsgPageBydir("������ͼƬ����");
            }
            if (m1.UpdateCP_Img(TextBox3.Text))
            {
                this.SetSuccMsgPageBydir("ͼƬ���óɹ���", "paraconf/wfmSysParaSet.aspx");
                return;
            }
            else
            {
                this.SetErrorMsgPageBydir("ͼƬ����ʧ�ܣ�");
                return;
            }
        }
	}
}
