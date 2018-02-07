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
	/// Summary description for wfmGoodsDetail.
	/// </summary>
	public partial class wfmGoodsDetail : wfmBase
	{

		Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			string strid=Request.QueryString["id"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			if(!IsPostBack)
			{
				this.FillDropDownList("tbCommCode", this.DropDownList1,"vcCommSign='GT'");
				if(strid==""||strid==null)
				{
					this.btAdd.Enabled=true;
					this.btDel.Enabled=false;
					this.btMod.Enabled=false;
					this.txtGoodsID.ReadOnly=true;
					lbltitle.Text="新商品资料录入";
					this.Label1.Text = "商品类别";
					this.DropDownList1.Visible = true;
					this.txtGoodsID.Visible = false;
				    this.CBDiscount.Checked=true;
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btDel.Enabled=false;
					this.btMod.Enabled=true;
					CMSMStruct.GoodsStruct gs=m1.GetGoodsInfo(strid);
					txtGoodsID.Text=gs.strGoodsID;
					txtGoodsName.Text=gs.strGoodsName;
					txtSpell.Text=gs.strSpell;
					txtPrice.Text=gs.dPrice.ToString();
					txtigvalue.Text=gs.iIgValue.ToString();
//					txtComments.Text=gs.strComments;
					this.txtGoodsID.ReadOnly=true;
					this.txtGoodsName.ReadOnly=false;
					lbltitle.Text="商品资料修改删除";
					Session["gsold"]=gs;
					this.Label1.Text = "商品ID";
					this.txtGoodsID.Visible = true;
					this.DropDownList1.Visible = false;
					if(gs.strComments=="" || gs.strComments=="否")
					{
						this.CBDiscount.Checked=false;
					}
					else
					{
						this.CBDiscount.Checked=true;
					}
					
				}
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

		protected void btcancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmGoods.aspx");
		}

		protected void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.GoodsStruct gs=new CMSMStruct.GoodsStruct();
			gs.strGoodsType = this.DropDownList1.SelectedItem.Text;
			string strGoodsTypeEN= this.DropDownList1.SelectedValue;

			if(txtGoodsName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("商品名称不能为空！");
				return;
			}
			else if(m1.ChkGoodsNameDup(txtGoodsName.Text.Trim()))
			{
				gs.strGoodsName=txtGoodsName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("该商品名称已经存在，请重新输入！");
				return;	
			}

			if(txtPrice.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("商品单价不能为空！");
				return;
			}
			else
			{
				gs.dPrice=Double.Parse(txtPrice.Text.Trim());
			}

			if(txtigvalue.Text.Trim()==""||txtigvalue.Text.Trim()=="0"||int.Parse(txtigvalue.Text.Trim())<-1)
			{
				this.SetErrorMsgPageBydir("兑换分值不正确！");
				return;
			}
			else
			{
				gs.iIgValue=int.Parse(txtigvalue.Text.Trim());
			}
			if(this.CBDiscount.Checked==true)
			{
				gs.strComments="是";
			}
			else
			{
				gs.strComments="否";
			}

			gs.strSpell=txtSpell.Text.Trim().ToLower();
//			gs.strComments=txtComments.Text.Trim();

			if(!m1.InsertGoods(gs,strGoodsTypeEN))
			{
				this.SetErrorMsgPageBydir("添加商品信息失败，商品编号可能已经达到最大值，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("添加商品信息成功！","");
				return;
			}
		}

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.GoodsStruct gsold=(CMSMStruct.GoodsStruct)Session["gsold"];
			if(gsold.strGoodsID!=txtGoodsID.Text.Trim())
			{
				this.SetErrorMsgPageBydir("保存失败，请重试！");
				return;
			}

			CMSMStruct.GoodsStruct gsnew=new CMSMStruct.GoodsStruct();
			gsnew.strGoodsID=txtGoodsID.Text.Trim();

			if(txtGoodsName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("商品名称不能为空！");
				return;
			}
			else if(m1.ChkNewGoodsNameDup(txtGoodsName.Text.Trim(),gsnew.strGoodsID))
			{
				gsnew.strGoodsName=txtGoodsName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("该商品名称已经存在，请重新输入！");
				return;	
			}

			if(txtPrice.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("商品单价不能为空！");
				return;
			}
			else
			{
				gsnew.dPrice=Double.Parse(txtPrice.Text.Trim());
			}

			if(txtigvalue.Text.Trim()==""||txtigvalue.Text.Trim()=="0"||int.Parse(txtigvalue.Text.Trim())<-1)
			{
				this.SetErrorMsgPageBydir("兑换分值不正确！");
				return;
			}
			else
			{
				gsnew.iIgValue=int.Parse(txtigvalue.Text.Trim());
			}
			if(this.CBDiscount.Checked==true)
			{
				gsnew.strComments="是";
			}
			else
			{
				gsnew.strComments="否";
			}


			gsnew.strSpell=txtSpell.Text.Trim().ToLower();
//			gsnew.strComments=txtComments.Text.Trim();

			if(!m1.UpdateGoods(gsnew,gsold))
			{
				this.SetErrorMsgPageBydir("保存商品修改信息失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("保存商品修改信息成功！","");
				return;
			}
		}

		protected void btDel_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
