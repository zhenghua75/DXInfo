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
using Microsoft.Web.UI.WebControls;
using CommCenter;

namespace AMSApp.Storage
{
    /// <summary>
    /// wfmProviderGoods 的摘要说明。
    /// </summary>
    public class wfmProviderGoods : wfmBase
    {
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.TextBox txtProviderID;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.TextBox txtProviderName;
        protected System.Web.UI.WebControls.RadioButton rbtnProvider;
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.Label Label4;
        protected System.Web.UI.WebControls.RadioButton rbtnGoods;
        protected System.Web.UI.WebControls.TextBox txtGoodsID;
        protected System.Web.UI.WebControls.TextBox txtGoodsName;
        protected System.Web.UI.WebControls.Button btnQuery;
        protected Microsoft.Web.UI.WebControls.TreeView tv;

        BusiComm.StorageBusi StoBusi;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            if (Session["Login"] != null)
            {
                CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
                if (!IsPostBack)
                {
                    Session.Remove("QUERY");
                    Session.Remove("page_view");
                }
            }
            else
            {
                Response.Redirect("../Exit.aspx");
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
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        public void FillTree(Microsoft.Web.UI.WebControls.TreeView tvtemp, string strQueryType)
        {
            string strProvID = this.txtProviderID.Text.Trim();
            string strProvName = this.txtProviderName.Text.Trim();
            string strGoodsID = this.txtGoodsID.Text.Trim();
            string strGoodsName = this.txtGoodsName.Text.Trim();
            Hashtable htpara = new Hashtable();
            htpara.Add("strProvID", strProvID);
            htpara.Add("strProvName", strProvName);
            htpara.Add("strGoodsID", strGoodsID);
            htpara.Add("strGoodsName", strGoodsName);

            Hashtable htapp = (Hashtable)Application["appconf"];
            string strcons = (string)htapp["cons"];
            StoBusi = new BusiComm.StorageBusi(strcons);

            DataTable table = StoBusi.GetProviderStockFillTree(strQueryType, htpara);
            tvtemp.Nodes.Clear();
            if (strQueryType == "prov")
            {
                Microsoft.Web.UI.WebControls.TreeNode node = new Microsoft.Web.UI.WebControls.TreeNode();
                node.ID = "0";
                node.Text = "供应商";
                node.Type = "prov";
                node.ImageUrl = "../image/promotion.png";
                tvtemp.Nodes.Add(node);
            }
            else
            {
                Microsoft.Web.UI.WebControls.TreeNode node = new Microsoft.Web.UI.WebControls.TreeNode();
                node.ID = "0";
                node.Text = "货品";
                node.Type = "good";
                node.ImageUrl = "../image/rss.png";
                tvtemp.Nodes.Add(node);

                DataTable dtPClass = (DataTable)Application["PClass"];
                foreach (DataRow dr in dtPClass.Rows)
                {
                    if (dr["vcCommSign"].ToString() == "Pack" || dr["vcCommSign"].ToString() == "Raw")
                    {
                        node = new Microsoft.Web.UI.WebControls.TreeNode();
                        node.ID = "0";
                        node.Text = dr["vcCommName"].ToString();
                        node.Type = "good";
                        node.ImageUrl = "../image/rss.png";
                        tvtemp.Nodes[0].Nodes.Add(node);
                    }
                }
            }

            foreach (DataRow row in table.Rows)
            {
                if (strQueryType == "prov")
                {
                    Microsoft.Web.UI.WebControls.TreeNode node = new Microsoft.Web.UI.WebControls.TreeNode();
                    node.ID = row["cnvcPrvdCode"].ToString();
                    node.Text = row["cnvcPrvdName"].ToString();
                    node.Type = "prov";
                    node.ImageUrl = "../image/next.png";
                    tvtemp.Nodes[0].Nodes.Add(node);
                }
                else
                {
                    Microsoft.Web.UI.WebControls.TreeNode node = new Microsoft.Web.UI.WebControls.TreeNode();
                    node.ID = row["cnvcGoodsName"].ToString();
                    node.Text = row["cnvcGoodsName"].ToString();
                    node.Type = "good";
                    node.ImageUrl = "../image/next.png";
                    foreach (Microsoft.Web.UI.WebControls.TreeNode notmp in tvtemp.Nodes[0].Nodes)
                    {
                        if (notmp.Text == row["cnvcProductClassName"].ToString())
                        {
                            notmp.Nodes.Add(node);
                            break;
                        }
                    }

                }
            }
        }

        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            if (this.rbtnProvider.Checked)
            {
                FillTree(this.tv, "prov");
            }
            else
            {
                FillTree(this.tv, "good");
            }
        }
    }
}
