using System;
using DataAccess;
using System.Data;
using CommCenter;
using System.Collections;

namespace BusiComm
{
	/// <summary>
	/// Summary description for Manager.
	/// </summary>
	public class BusiQuery
	{
		string strcon="";
		QueryAcc Qa=null;
		public BusiQuery(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			strcon=strcons;
			Qa=new QueryAcc(strcon);
		}

		public DataTable GetConsQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetConsQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["iSerial"].ColumnName="流水";
				dtout.Columns["vcAssName"].ColumnName="会员名称";
				dtout.Columns["vcAssType"].ColumnName="会员类型";
				dtout.Columns["vcCardID"].ColumnName="会员卡号";
				dtout.Columns["vcGoodsName"].ColumnName="商品名称";
				dtout.Columns["nPrice"].ColumnName="单价";
				dtout.Columns["iCount"].ColumnName="数量";
				dtout.Columns["nFee"].ColumnName="合计";
				dtout.Columns["vcConsType"].ColumnName="付款类型";
				dtout.Columns["vcComments"].ColumnName="备注";
				dtout.Columns["cFlag"].ColumnName="有效状态";
				dtout.Columns["dtConsDate"].ColumnName="消费日期";
				dtout.Columns["vcOperName"].ColumnName="操作员";
				dtout.Columns["vcDeptID"].ColumnName="门店";
			}
			return dtout;
		}

		public DataTable GetFillQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetFillQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["iSerial"].ColumnName="流水";
				dtout.Columns["vcAssName"].ColumnName="会员名称";
				dtout.Columns["vcAssType"].ColumnName="会员类型";
				dtout.Columns["vcCardID"].ColumnName="会员卡号";
				dtout.Columns["nFillFee"].ColumnName="充值金额";
				dtout.Columns["nFillProm"].ColumnName="赠款金额";
				dtout.Columns["nFeeLast"].ColumnName="上次余额";
				dtout.Columns["nFeeCur"].ColumnName="当前余额";
				dtout.Columns["vcComments"].ColumnName="备注";
				dtout.Columns["dtFillDate"].ColumnName="充值日期";
				dtout.Columns["vcOperName"].ColumnName="操作员";
				dtout.Columns["vcDeptID"].ColumnName="操作员门店";
			}
			return dtout;
		}

		public DataTable GetConsKindQuery(Hashtable htpara,string flag)
		{
			DataTable dtout=Qa.GetConsKindQuery(htpara,flag);
			if(dtout!=null)
			{
				if(dtout.Columns.Contains("vcDeptID"))
				{
					dtout.Columns["vcDeptID"].ColumnName="门店";
				}
				if(dtout.Columns.Contains("vcAssType"))
				{
					dtout.Columns["vcAssType"].ColumnName="会员类型";
				}
				if(dtout.Columns.Contains("vcGoodsType"))
				{
					dtout.Columns["vcGoodsType"].ColumnName="商品类型";
				}
				dtout.Columns["vcGoodsName"].ColumnName="商品名称";
				dtout.Columns["tolcount"].ColumnName="数量合计";
				dtout.Columns["tolfee"].ColumnName="金额合计";
			}
			return dtout;
		}

		public DataTable GetBusiLogQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetBusiLogQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["iSerial"].ColumnName="流水";
				dtout.Columns["vcAssName"].ColumnName="会员名称";
				dtout.Columns["vcAssType"].ColumnName="会员类型";
				dtout.Columns["vcCardID"].ColumnName="会员卡号";
				dtout.Columns["vcCommName"].ColumnName="操作类型";
				dtout.Columns["vcOperName"].ColumnName="操作员";
				dtout.Columns["dtOperDate"].ColumnName="操作日期";
				dtout.Columns["vcDeptID"].ColumnName="操作员门店";
				dtout.Columns["vcComments"].ColumnName="备注";
			}
			return dtout;
		}

        public DataTable BusiIncomeReport(Hashtable htpara, string strDeptName, DataTable dtat)
        {
            DataSet dsout = Qa.BusiIncomeReport(htpara);
            DataTable dtIncome = null;
            if (dsout != null)
            {
                dtIncome = ReportConvert(dsout, strDeptName, dtat);
            }
            return dtIncome;
        }

        private DataTable ReportConvert(DataSet dsIn, string strname, DataTable dtat)
        {
            string strtmp;
            DataTable dtdis = new DataTable();

            #region 总体情况，如果查询门店为全部，则只有此总情况，没有下面的本店与他店情况
            strtmp = "";
            dtdis.Columns.Add("type");
            dtdis.Columns.Add("col1");
            dtdis.Columns.Add("col2");
            dtdis.Columns.Add("col3");
            dtdis.Columns.Add("col4");
            dtdis.Columns.Add("col5");
            dtdis.Columns.Add("col6");
            dtdis.Columns.Add("col7");

            DataRow dr;
            foreach (DataRow drr in dsIn.Tables["AllIncome"].Rows)
            {
                if (drr["Type"].ToString().Substring(0, 2) != "AT")
                {
                    switch (drr["Type"].ToString())
                    {
                        case "OldState":
                            dr = dtdis.NewRow();
                            dr["type"] = "原状态";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = drr["REP2"].ToString();
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            dr["col5"] = "......";
                            dr["col6"] = "......";
                            dr["col7"] = "......";
                            dtdis.Rows.Add(dr);
                            break;
                        case "NewState":
                            dr = dtdis.NewRow();
                            dr["type"] = "现状态";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = drr["REP2"].ToString();
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            dr["col5"] = "......";
                            dr["col6"] = "......";
                            dr["col7"] = "......";
                            dtdis.Rows.Add(dr);
                            break;
                        case "NewAss":
                            dr = dtdis.NewRow();
                            dr["type"] = "新入会员";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = drr["REP2"].ToString();
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            dr["col5"] = "......";
                            dr["col6"] = "......";
                            dr["col7"] = "......";
                            dtdis.Rows.Add(dr);
                            break;
                        case "LostAss":
                            dr = dtdis.NewRow();
                            dr["type"] = "挂失会员";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = "......";
                            dr["col5"] = "......";
                            dr["col6"] = "......";
                            dr["col7"] = "......";
                            dtdis.Rows.Add(dr);
                            break;
                        case "CardCycle":
                            dr = dtdis.NewRow();
                            dr["type"] = "卡回收";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            dr["col5"] = drr["REP5"].ToString();
                            dr["col6"] = drr["REP6"].ToString();
                            dr["col7"] = "......";
                            dtdis.Rows.Add(dr);
                            break;
                        case "CardAgain":
                            dr = dtdis.NewRow();
                            dr["type"] = "补卡会员";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = "......";
                            dr["col5"] = "......";
                            dr["col6"] = "......";
                            dr["col7"] = "......";
                            dtdis.Rows.Add(dr);
                            break;
                        case "FillFee":
                            dr = dtdis.NewRow();
                            dr["type"] = "充值";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            dr["col5"] = drr["REP5"].ToString();
                            strtmp = drr["REP6"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col6"] = "0";
                            }
                            else
                            {
                                dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            dr["col7"] = "......";
                            dtdis.Rows.Add(dr);
                            break;
                        //						case "AssCons":
                        //							dr=dtdis.NewRow();
                        //							dr["type"]="普通会员消费";
                        //							dr["col1"]=drr["REP1"].ToString();
                        //							dr["col2"]="......";
                        //							dr["col3"]="......";
                        //							dr["col4"]=drr["REP4"].ToString();
                        //							strtmp=drr["REP5"].ToString();
                        //							if(strtmp=="0")
                        //							{
                        //								dr["col5"]="0";
                        //							}
                        //							else
                        //							{
                        //								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                        //							}
                        //							strtmp=drr["REP6"].ToString();
                        //							if(strtmp=="0")
                        //							{
                        //								dr["col6"]="0";
                        //							}
                        //							else
                        //							{
                        //								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                        //							}
                        //							dr["col7"]=drr["REP7"].ToString();
                        //							dtdis.Rows.Add(dr);
                        //							break;
                        //						case "SAssCons":
                        //							dr=dtdis.NewRow();
                        //							dr["type"]="银卡会员消费";
                        //							dr["col1"]=drr["REP1"].ToString();
                        //							dr["col2"]="......";
                        //							dr["col3"]="......";
                        //							dr["col4"]=drr["REP4"].ToString();
                        //							strtmp=drr["REP5"].ToString();
                        //							if(strtmp=="0")
                        //							{
                        //								dr["col5"]="0";
                        //							}
                        //							else
                        //							{
                        //								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                        //							}
                        //							strtmp=drr["REP6"].ToString();
                        //							if(strtmp=="0")
                        //							{
                        //								dr["col6"]="0";
                        //							}
                        //							else
                        //							{
                        //								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                        //							}
                        //							dr["col7"]=drr["REP7"].ToString();
                        //							dtdis.Rows.Add(dr);
                        //							break;
                        //						case "GAssCons":
                        //							dr=dtdis.NewRow();
                        //							dr["type"]="金卡会员消费";
                        //							dr["col1"]=drr["REP1"].ToString();
                        //							dr["col2"]="......";
                        //							dr["col3"]="......";
                        //							dr["col4"]=drr["REP4"].ToString();
                        //							strtmp=drr["REP5"].ToString();
                        //							if(strtmp=="0")
                        //							{
                        //								dr["col5"]="0";
                        //							}
                        //							else
                        //							{
                        //								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                        //							}
                        //							strtmp=drr["REP6"].ToString();
                        //							if(strtmp=="0")
                        //							{
                        //								dr["col6"]="0";
                        //							}
                        //							else
                        //							{
                        //								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                        //							}
                        //							dr["col7"]=drr["REP7"].ToString();
                        //							dtdis.Rows.Add(dr);
                        //							break;
                        //						case "PromCons":
                        //							dr=dtdis.NewRow();
                        //							dr["type"]="赠卡会员消费";
                        //							dr["col1"]=drr["REP1"].ToString();
                        //							dr["col2"]="......";
                        //							dr["col3"]="......";
                        //							dr["col4"]=drr["REP4"].ToString();
                        //							strtmp=drr["REP5"].ToString();
                        //							if(strtmp=="0")
                        //							{
                        //								dr["col5"]="0";
                        //							}
                        //							else
                        //							{
                        //								dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                        //							}
                        //							strtmp=drr["REP6"].ToString();
                        //							if(strtmp=="0")
                        //							{
                        //								dr["col6"]="0";
                        //							}
                        //							else
                        //							{
                        //								dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                        //							}
                        //							dr["col7"]=drr["REP7"].ToString();
                        //							dtdis.Rows.Add(dr);
                        //							break;
                        case "Retail":
                            dr = dtdis.NewRow();
                            dr["type"] = "零售";
                            dr["col1"] = "......";
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            dr["col5"] = "......";
                            strtmp = drr["REP6"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col6"] = "0";
                            }
                            else
                            {
                                dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            dr["col7"] = drr["REP7"].ToString();
                            dtdis.Rows.Add(dr);
                            break;
                        case "Larg":
                            dr = dtdis.NewRow();
                            dr["type"] = "会员赠送";
                            dr["col1"] = "......";
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = "......";
                            dr["col5"] = "......";
                            strtmp = drr["REP6"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col6"] = "0";
                            }
                            else
                            {
                                dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            dr["col7"] = drr["REP7"].ToString();
                            dtdis.Rows.Add(dr);
                            break;
                        case "IgChange":
                            dr = dtdis.NewRow();
                            dr["type"] = "积分兑换";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = "......";
                            strtmp = drr["REP3"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col3"] = "0";
                            }
                            else
                            {
                                dr["col3"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            dr["col4"] = "......";
                            dr["col5"] = "......";
                            strtmp = drr["REP6"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col6"] = "0";
                            }
                            else
                            {
                                dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            dr["col7"] = drr["REP7"].ToString();
                            dtdis.Rows.Add(dr);
                            break;
                        case "BankRetail":
                            dr = dtdis.NewRow();
                            dr["type"] = "银行卡零售";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            strtmp = drr["REP5"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col5"] = "0";
                            }
                            else
                            {
                                dr["col5"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            strtmp = drr["REP6"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col6"] = "0";
                            }
                            else
                            {
                                dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            dr["col7"] = drr["REP7"].ToString();
                            dtdis.Rows.Add(dr);
                            break;
                        case "BankFillFee":
                            dr = dtdis.NewRow();
                            dr["type"] = "银行卡充值";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            dr["col5"] = drr["REP5"].ToString();
                            strtmp = drr["REP6"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col6"] = "0";
                            }
                            else
                            {
                                dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            dr["col7"] = "......";
                            dtdis.Rows.Add(dr);
                            break;
                    }
                }
                else
                {
                    foreach (DataRow drat in dtat.Rows)
                    {
                        if (drat["vcCommCode"].ToString() == drr["Type"].ToString())
                        {
                            dr = dtdis.NewRow();
                            dr["type"] = drat["vcCommName"].ToString() + "消费";
                            dr["col1"] = drr["REP1"].ToString();
                            dr["col2"] = "......";
                            dr["col3"] = "......";
                            dr["col4"] = drr["REP4"].ToString();
                            strtmp = drr["REP5"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col5"] = "0";
                            }
                            else
                            {
                                dr["col5"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            strtmp = drr["REP6"].ToString();
                            if (strtmp == "0")
                            {
                                dr["col6"] = "0";
                            }
                            else
                            {
                                dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                            }
                            dr["col7"] = drr["REP7"].ToString();
                            dtdis.Rows.Add(dr);
                        }
                    }
                }
            }

            dr = dtdis.NewRow();
            dr["type"] = "";
            dr["col1"] = "会员增加总数";
            dr["col2"] = "积分增加总额";
            dr["col3"] = "余额增加总额";
            dr["col4"] = "现金增加总额";
            dr["col5"] = "赠款增加总额";
            dr["col6"] = "销售总额";
            dr["col7"] = "商品销售总数";
            dtdis.Rows.Add(dr);

            int lastrow = dsIn.Tables["AllIncome"].Rows.Count - 1;
            dr = dtdis.NewRow();
            dr["type"] = "总计";
            dr["col1"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP1"].ToString();
            dr["col2"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP2"].ToString();
            dr["col3"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP3"].ToString();
            dr["col4"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP4"].ToString();
            dr["col5"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP5"].ToString();
            dr["col6"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP6"].ToString();
            dr["col7"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP7"].ToString();
            dtdis.Rows.Add(dr);

            //			if(strname=="全部")
            //			{
            //				dtdis.Columns["type"].ColumnName="所有门店";
            //			}
            //			else
            //			{
            //				dtdis.Columns["type"].ColumnName=strname;
            //			}			
            //			dtdis.Columns["col1"].ColumnName="会员数";
            //			dtdis.Columns["col2"].ColumnName="可用积分";
            //			dtdis.Columns["col3"].ColumnName="使用积分";
            //			dtdis.Columns["col4"].ColumnName="金额";
            //			dtdis.Columns["col5"].ColumnName="附赠情况";
            //			dtdis.Columns["col6"].ColumnName="次数";
            //			dtdis.Columns["col7"].ColumnName="商品数";
            //
            //			dsret.Tables.Add(dtdis);
            #endregion

            if (strname != "全部")
            {
                #region 本店会员在本店充值和消费情况
                strtmp = "";
                dr = dtdis.NewRow();
                dr["type"] = "";
                dr["col1"] = "";
                dr["col2"] = "";
                dr["col3"] = "";
                dr["col4"] = "";
                dr["col5"] = "";
                dr["col6"] = "";
                dr["col7"] = "";
                dtdis.Rows.Add(dr);

                dr = dtdis.NewRow();
                dr["type"] = "本店会员在本店";
                dr["col1"] = "";
                dr["col2"] = "";
                dr["col3"] = "";
                dr["col4"] = "";
                dr["col5"] = "";
                dr["col6"] = "";
                dr["col7"] = "";
                dtdis.Rows.Add(dr);

                foreach (DataRow drr in dsIn.Tables["LocalIncome"].Rows)
                {
                    if (drr["Type"].ToString().Substring(0, 2) != "AT")
                    {
                        switch (drr["Type"].ToString())
                        {
                            case "FillFee-L":
                                dr = dtdis.NewRow();
                                dr["type"] = "充值";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = drr["REP4"].ToString();
                                dr["col5"] = drr["REP5"].ToString();
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = "......";
                                dtdis.Rows.Add(dr);
                                break;
                            //							case "Local-AssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="普通会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "Local-SAssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="银卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "Local-GAssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="金卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "Local-PromCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="赠卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            case "Larg-L":
                                dr = dtdis.NewRow();
                                dr["type"] = "会员赠送";
                                dr["col1"] = "......";
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = "......";
                                dr["col5"] = "......";
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                            case "IgChange-L":
                                dr = dtdis.NewRow();
                                dr["type"] = "积分兑换";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                strtmp = drr["REP3"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col3"] = "0";
                                }
                                else
                                {
                                    dr["col3"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col4"] = "......";
                                dr["col5"] = "......";
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                        }
                    }
                    else
                    {
                        foreach (DataRow drat in dtat.Rows)
                        {
                            if (drat["vcCommCode"].ToString() + "-L" == drr["Type"].ToString())
                            {
                                dr = dtdis.NewRow();
                                dr["type"] = drat["vcCommName"].ToString() + "消费";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = drr["REP4"].ToString();
                                strtmp = drr["REP5"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col5"] = "0";
                                }
                                else
                                {
                                    dr["col5"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                            }
                        }
                    }
                }

                //				dr=dtdis.NewRow();
                //				dr["type"]="";
                //				dr["col1"]="会员增加总数";
                //				dr["col2"]="积分增加总额";
                //				dr["col3"]="余额增加总额";
                //				dr["col4"]="现金增加总额";
                //				dr["col5"]="赠款增加总额";
                //				dr["col6"]="会员消费总额";
                //				dr["col7"]="商品销售总数";
                //				dtdis.Rows.Add(dr);
                //
                //				dr=dtdis.NewRow();
                //				dr["type"]="总计";
                //				dr["col1"]=dtreg.Rows[10]["REP1"].ToString();
                //				dr["col2"]=dtreg.Rows[10]["REP2"].ToString();
                //				dr["col3"]=dtreg.Rows[10]["REP3"].ToString();
                //				dr["col4"]=dtreg.Rows[10]["REP4"].ToString();
                //				dr["col5"]=dtreg.Rows[10]["REP5"].ToString();
                //				dr["col6"]=dtreg.Rows[10]["REP6"].ToString();
                //				dr["col7"]=dtreg.Rows[10]["REP7"].ToString();
                //				dtdis.Rows.Add(dr);
                #endregion

                #region 他店会员在本店充值和消费情况
                strtmp = "";
                dr = dtdis.NewRow();
                dr["type"] = "";
                dr["col1"] = "";
                dr["col2"] = "";
                dr["col3"] = "";
                dr["col4"] = "";
                dr["col5"] = "";
                dr["col6"] = "";
                dr["col7"] = "";
                dtdis.Rows.Add(dr);

                dr = dtdis.NewRow();
                dr["type"] = "他店会员在本店";
                dr["col1"] = "";
                dr["col2"] = "";
                dr["col3"] = "";
                dr["col4"] = "";
                dr["col5"] = "";
                dr["col6"] = "";
                dr["col7"] = "";
                dtdis.Rows.Add(dr);

                foreach (DataRow drr in dsIn.Tables["OtherIncome"].Rows)
                {
                    if (drr["Type"].ToString().Substring(0, 2) != "AT")
                    {
                        switch (drr["Type"].ToString())
                        {
                            case "FillFee-O":
                                dr = dtdis.NewRow();
                                dr["type"] = "充值";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = drr["REP4"].ToString();
                                dr["col5"] = drr["REP5"].ToString();
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = "......";
                                dtdis.Rows.Add(dr);
                                break;
                            //							case "Other-AssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="普通会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "Other-SAssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="银卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "Other-GAssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="金卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "Other-PromCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="赠卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            case "Larg-O":
                                dr = dtdis.NewRow();
                                dr["type"] = "会员赠送";
                                dr["col1"] = "......";
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = "......";
                                dr["col5"] = "......";
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                            case "IgChange-O":
                                dr = dtdis.NewRow();
                                dr["type"] = "积分兑换";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                strtmp = drr["REP3"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col3"] = "0";
                                }
                                else
                                {
                                    dr["col3"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col4"] = "......";
                                dr["col5"] = "......";
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                        }
                    }
                    else
                    {
                        foreach (DataRow drat in dtat.Rows)
                        {
                            if (drat["vcCommCode"].ToString() + "-O" == drr["Type"].ToString())
                            {
                                dr = dtdis.NewRow();
                                dr["type"] = drat["vcCommName"].ToString() + "消费";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = drr["REP4"].ToString();
                                strtmp = drr["REP5"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col5"] = "0";
                                }
                                else
                                {
                                    dr["col5"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region 本店会员在他店充值和消费情况
                strtmp = "";
                dr = dtdis.NewRow();
                dr["type"] = "";
                dr["col1"] = "";
                dr["col2"] = "";
                dr["col3"] = "";
                dr["col4"] = "";
                dr["col5"] = "";
                dr["col6"] = "";
                dr["col7"] = "";
                dtdis.Rows.Add(dr);

                dr = dtdis.NewRow();
                dr["type"] = "本店会员在他店";
                dr["col1"] = "";
                dr["col2"] = "";
                dr["col3"] = "";
                dr["col4"] = "";
                dr["col5"] = "";
                dr["col6"] = "";
                dr["col7"] = "";
                dtdis.Rows.Add(dr);

                foreach (DataRow drr in dsIn.Tables["LocalToOtherIncome"].Rows)
                {
                    if (drr["Type"].ToString().Substring(0, 2) != "AT")
                    {
                        switch (drr["Type"].ToString())
                        {
                            case "FillFee-X":
                                dr = dtdis.NewRow();
                                dr["type"] = "充值";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = drr["REP4"].ToString();
                                dr["col5"] = drr["REP5"].ToString();
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = "......";
                                dtdis.Rows.Add(dr);
                                break;
                            //							case "LocalToOtherAssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="普通会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "LocalToOtherSAssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="银卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "LocalToOtherGAssCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="金卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            //							case "LocalToOtherPromCons":
                            //								dr=dtdis.NewRow();
                            //								dr["type"]="赠卡会员消费";
                            //								dr["col1"]=drr["REP1"].ToString();
                            //								dr["col2"]="......";
                            //								dr["col3"]="......";
                            //								dr["col4"]=drr["REP4"].ToString();
                            //								strtmp=drr["REP5"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col5"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col5"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								strtmp=drr["REP6"].ToString();
                            //								if(strtmp=="0")
                            //								{
                            //									dr["col6"]="0";
                            //								}
                            //								else
                            //								{
                            //									dr["col6"]=strtmp.Substring(0,strtmp.IndexOf(".",0));
                            //								}
                            //								dr["col7"]=drr["REP7"].ToString();
                            //								dtdis.Rows.Add(dr);
                            //								break;
                            case "Larg-X":
                                dr = dtdis.NewRow();
                                dr["type"] = "会员赠送";
                                dr["col1"] = "......";
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = "......";
                                dr["col5"] = "......";
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                            case "IgChange-X":
                                dr = dtdis.NewRow();
                                dr["type"] = "积分兑换";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                strtmp = drr["REP3"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col3"] = "0";
                                }
                                else
                                {
                                    dr["col3"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col4"] = "......";
                                dr["col5"] = "......";
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                        }
                    }
                    else
                    {
                        foreach (DataRow drat in dtat.Rows)
                        {
                            if (drat["vcCommCode"].ToString() + "-X" == drr["Type"].ToString())
                            {
                                dr = dtdis.NewRow();
                                dr["type"] = drat["vcCommName"].ToString() + "消费";
                                dr["col1"] = drr["REP1"].ToString();
                                dr["col2"] = "......";
                                dr["col3"] = "......";
                                dr["col4"] = drr["REP4"].ToString();
                                strtmp = drr["REP5"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col5"] = "0";
                                }
                                else
                                {
                                    dr["col5"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                strtmp = drr["REP6"].ToString();
                                if (strtmp == "0")
                                {
                                    dr["col6"] = "0";
                                }
                                else
                                {
                                    dr["col6"] = strtmp.Substring(0, strtmp.IndexOf(".", 0));
                                }
                                dr["col7"] = drr["REP7"].ToString();
                                dtdis.Rows.Add(dr);
                                break;
                            }
                        }
                    }
                }
                #endregion
            }

            if (strname == "全部")
            {
                dtdis.Columns["type"].ColumnName = "所有门店";
            }
            else
            {
                dtdis.Columns["type"].ColumnName = strname;
            }
            dtdis.Columns["col1"].ColumnName = "会员数";
            dtdis.Columns["col2"].ColumnName = "可用积分";
            dtdis.Columns["col3"].ColumnName = "使用积分";
            dtdis.Columns["col4"].ColumnName = "金额";
            dtdis.Columns["col5"].ColumnName = "附赠情况";
            dtdis.Columns["col6"].ColumnName = "次数";
            dtdis.Columns["col7"].ColumnName = "商品数";

            return dtdis;
        }

		public DataTable GetTopQuery(Hashtable htpara,string strtype)
		{
			DataTable dtout=Qa.GetTopQuery(htpara,strtype);
			if(dtout!=null)
			{
				if(strtype=="0")
				{
                    dtout.Columns["vcGoodsID"].ColumnName = "商品ID";
                    dtout.Columns["vcCommName"].ColumnName = "商品类型";
                    dtout.Columns["vcGoodsName"].ColumnName = "商品名称";
                    dtout.Columns["salecount"].ColumnName = "销售数量";
                    dtout.Columns["nFee"].ColumnName = "销售金额";
				}
				else
				{
					dtout.Columns["vcCardID"].ColumnName="会员卡号";
					dtout.Columns["vcAssName"].ColumnName="会员名称";
					dtout.Columns["salefee"].ColumnName="消费额";
				}
			}
			return dtout;
		}

		public DataTable GetAssInfo(Hashtable htpara)
		{
			DataTable dtout=Qa.GetAssInfo(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcCardID"].ColumnName="会员卡号";
				dtout.Columns["vcAssName"].ColumnName="会员姓名";
				dtout.Columns["vcLinkPhone"].ColumnName="联系电话";
				dtout.Columns["vcSpell"].ColumnName="拼音简码";
				dtout.Columns["vcAssType"].ColumnName="会员类型";
				dtout.Columns["vcAssState"].ColumnName="会员状态";
				dtout.Columns["nCharge"].ColumnName="当前余额";
				dtout.Columns["iIgValue"].ColumnName="当前积分";
				dtout.Columns["vcDeptID"].ColumnName="门店";
				dtout.Columns["dtCreateDate"].ColumnName="创建日期";
				dtout.Columns["vcAssNbr"].ColumnName="身份证号";
				dtout.Columns["vcLinkAddress"].ColumnName="联系地址";
				dtout.Columns["vcEmail"].ColumnName="Email";
				dtout.Columns["dtOperDate"].ColumnName="操作日期";
				dtout.Columns["vcComments"].ColumnName="备注";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["操作"]="<a href='wfmAssMod.aspx?aid=" + dtout.Rows[i]["iAssID"].ToString() + "&cid="+ dtout.Rows[i]["会员卡号"].ToString() + "'>修改</a>";
				}
				dtout.Columns.Remove("iAssID");
			}
			return dtout;
		}

		public CMSMStruct.AssociatorStruct GetAssDetailInfo(string strAssid,string strCardID)
		{
			DataTable dtout=Qa.GetAssDetailInfo(strAssid,strCardID);
			CMSMStruct.AssociatorStruct as1=new CommCenter.CMSMStruct.AssociatorStruct();
			if(dtout!=null)
			{
				as1.strAssID=dtout.Rows[0]["iAssID"].ToString();
				as1.strCardID=dtout.Rows[0]["vcCardID"].ToString();
				as1.strAssName=dtout.Rows[0]["vcAssName"].ToString();
				as1.strSpell=dtout.Rows[0]["vcSpell"].ToString();
				as1.strAssNbr=dtout.Rows[0]["vcAssNbr"].ToString();
				as1.strLinkPhone=dtout.Rows[0]["vcLinkPhone"].ToString();
				as1.strLinkAddress=dtout.Rows[0]["vcLinkAddress"].ToString();
				as1.strEmail=dtout.Rows[0]["vcEmail"].ToString();
                as1.strAssType = dtout.Rows[0]["vcAssType"].ToString();
				as1.strComments=dtout.Rows[0]["vcComments"].ToString();
                as1.strAssTypeDisp = dtout.Rows[0]["vcAssTypeDisp"].ToString();
			}
			else
			{
				as1=null;
			}
			return as1;
		}

		public bool UpdateAssDetail(CMSMStruct.AssociatorStruct asnew,CMSMStruct.AssociatorStruct asold,CMSMStruct.LoginStruct ls1)
		{
			string sqlset="";
			if(asnew.strAssName!=asold.strAssName)
			{
				sqlset+="vcAssName='" + asnew.strAssName + "',";
			}
			if(asnew.strSpell!=asold.strSpell)
			{
				sqlset+="vcSpell='" + asnew.strSpell + "',";
			}
			if(asnew.strAssNbr!=asold.strAssNbr)
			{
				sqlset+="vcAssNbr='" + asnew.strAssNbr + "',";
			}
			if(asnew.strLinkPhone!=asold.strLinkPhone)
			{
				sqlset+="vcLinkPhone='" + asnew.strLinkPhone + "',";
			}
			if(asnew.strLinkAddress!=asold.strLinkAddress)
			{
				sqlset+="vcLinkAddress='" + asnew.strLinkAddress + "',";
			}
			if(asnew.strEmail!=asold.strEmail)
			{
				sqlset+="vcEmail='" + asnew.strEmail + "',";
			}
			if(asnew.strComments!=asold.strComments)
			{
				sqlset+="vcComments='" + asnew.strComments + "',";
			}
            if (asnew.strAssType != asold.strAssType)
            {
                sqlset += "vcAssType='" + asnew.strAssType + "',";
            }

			if(sqlset!="")
			{
				sqlset=sqlset.Substring(0,sqlset.Length-1);
				int recount=Qa.UpdateAssDetail(asnew.strAssID,asnew.strCardID,sqlset,ls1);
				if(recount<=0)
				{
					return false;
				}
			}
			
			return true;
		}

		public DataTable GetConsOperList(string strDeptID,string strbegin,string strend)
		{
			DataTable dtout=Qa.GetConsOperList(strDeptID, strbegin, strend);
			return dtout;
		}

		public DataTable GetUpDownQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetUpDownQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["Dept"].ColumnName="数据来源";
				dtout.Columns["vcFileName"].ColumnName="文件名称";
				dtout.Columns["FileSize"].ColumnName="文件大小";
				dtout.Columns["dtStartDate"].ColumnName="开始处理时间";
				dtout.Columns["dtFinDate"].ColumnName="处理完成时间";
				dtout.Columns["Type"].ColumnName="上下传类型";
			}
			return dtout;
		}

		public DataTable GetDailyCashQuery(Hashtable htpara)
		{
			DataTable dtDaily=new DataTable();
			DataTable dtout=Qa.GetDailyCashQuery(htpara);
			if(dtout.Rows.Count>0)
			{
				dtDaily.Columns.Add("统计项");
				string stroper=dtout.Rows[0]["vcOperName"].ToString();
				dtDaily.Columns.Add(stroper);
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(stroper!=dtout.Rows[i]["vcOperName"].ToString())
					{
						stroper=dtout.Rows[i]["vcOperName"].ToString();
						dtDaily.Columns.Add(stroper);
					}
				}
				DataRow drFillCount=dtDaily.NewRow();
				drFillCount["统计项"]="现金充值次数";
				DataRow drFillFee=dtDaily.NewRow();
				drFillFee["统计项"]="现金充值金额";
				DataRow drBankFillCount=dtDaily.NewRow();
				drBankFillCount["统计项"]="银行卡充值次数";
				DataRow drBankFillFee=dtDaily.NewRow();
				drBankFillFee["统计项"]="银行卡充值金额";
				DataRow drConsCount=dtDaily.NewRow();
				drConsCount["统计项"]="消费次数";
				DataRow drRetailCons=dtDaily.NewRow();
				drRetailCons["统计项"]="现金零售金额";
				DataRow drBankRetailCons=dtDaily.NewRow();
				drBankRetailCons["统计项"]="银行卡零售金额";
				DataRow drAssCons=dtDaily.NewRow();
				drAssCons["统计项"]="会员消费金额";
				DataRow drReCycleCount=dtDaily.NewRow();
				drReCycleCount["统计项"]="回收卡数";
				DataRow drReCycleFee=dtDaily.NewRow();
				drReCycleFee["统计项"]="回收退款金额";
				DataRow drLargCount=dtDaily.NewRow();
				drLargCount["统计项"]="赠送次数";
				DataRow drCashFee=dtDaily.NewRow();
				drCashFee["统计项"]="现金总额";
				for(int k=1;k<dtDaily.Columns.Count;k++)
				{
					drFillCount[k]=0;
					drFillFee[k]=0;
					drBankFillCount[k]=0;
					drBankFillFee[k]=0;
					drConsCount[k]=0;
					drRetailCons[k]=0;
					drBankRetailCons[k]=0;
					drAssCons[k]=0;
					drReCycleCount[k]=0;
					drReCycleFee[k]=0;
					drLargCount[k]=0;
					drCashFee[k]=0;
				}
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					switch(dtout.Rows[i]["vcConsType"].ToString())
					{
						case "PT001":
							drConsCount[dtout.Rows[i]["vcOperName"].ToString()]=int.Parse(drConsCount[dtout.Rows[i]["vcOperName"].ToString()].ToString())+int.Parse(dtout.Rows[i]["ConsCount"].ToString());
							drAssCons[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							break;
						case "PT002":
							drConsCount[dtout.Rows[i]["vcOperName"].ToString()]=int.Parse(drConsCount[dtout.Rows[i]["vcOperName"].ToString()].ToString())+int.Parse(dtout.Rows[i]["ConsCount"].ToString());
							drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							drRetailCons[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							break;
						case "PT004":
							drLargCount[dtout.Rows[i]["vcOperName"].ToString()]=int.Parse(drLargCount[dtout.Rows[i]["vcOperName"].ToString()].ToString())+int.Parse(dtout.Rows[i]["ConsCount"].ToString());
							break;
						case "PT008":
							drConsCount[dtout.Rows[i]["vcOperName"].ToString()]=int.Parse(drConsCount[dtout.Rows[i]["vcOperName"].ToString()].ToString())+int.Parse(dtout.Rows[i]["ConsCount"].ToString());
                            //drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							drBankRetailCons[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							break;
						case "Fill":
							drFillCount[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsCount"].ToString();
							drFillFee[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							break;
						case "FillBank":
							drBankFillCount[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsCount"].ToString();
							drBankFillFee[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
                            //drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							break;
						case "CradRoll":
							drReCycleCount[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsCount"].ToString();
							drReCycleFee[dtout.Rows[i]["vcOperName"].ToString()]=dtout.Rows[i]["ConsFee"].ToString();
							drCashFee[dtout.Rows[i]["vcOperName"].ToString()]=double.Parse(drCashFee[dtout.Rows[i]["vcOperName"].ToString()].ToString())+double.Parse(dtout.Rows[i]["ConsFee"].ToString());
							break;
					}
				}

				dtDaily.Rows.Add(drFillCount);
				dtDaily.Rows.Add(drFillFee);
				dtDaily.Rows.Add(drBankFillCount);
				dtDaily.Rows.Add(drBankFillFee);
				dtDaily.Rows.Add(drConsCount);
				dtDaily.Rows.Add(drRetailCons);
				dtDaily.Rows.Add(drBankRetailCons);
				dtDaily.Rows.Add(drAssCons);
				dtDaily.Rows.Add(drReCycleCount);
				dtDaily.Rows.Add(drReCycleFee);
				dtDaily.Rows.Add(drLargCount);
				dtDaily.Rows.Add(drCashFee);
			}
			else
			{
				dtDaily.Columns.Add("无任何记录！");
			}
			return dtDaily;
		}

		public DataTable GetExpInvList()
		{
			DataTable dtout=Qa.GetExpInvList();
			return dtout;
		}

		public DataTable GetSpecConsQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetSpecConsQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcConsType"].ColumnName="特殊消费类型";
				dtout.Columns["vcGoodsName"].ColumnName="商品名称";
				dtout.Columns["tolCount"].ColumnName="数量";
				dtout.Columns["tolfee"].ColumnName="销售金额";
				dtout.Columns["tolcash"].ColumnName="现金";
			}
			return dtout;
		}

        public DataTable GetIGQuery(Hashtable htpara)
        {
            DataTable dtout = Qa.GetIGQuery(htpara);
            if (dtout != null)
            {
                dtout.Columns["iSerial"].ColumnName = "流水";
                dtout.Columns["vcAssName"].ColumnName = "会员名称";
                dtout.Columns["vcAssType"].ColumnName = "会员类型";
                dtout.Columns["vcCardID"].ColumnName = "会员卡号";
                dtout.Columns["iIgLast"].ColumnName = "上次余额";
                dtout.Columns["iIgGet"].ColumnName = "本次积分";
                dtout.Columns["iIgArrival"].ColumnName = "当前积分";
                dtout.Columns["vcComments"].ColumnName = "备注";
                dtout.Columns["dtIgDate"].ColumnName = "积分日期";
                dtout.Columns["vcOperName"].ColumnName = "操作员";
                dtout.Columns["vcDeptID"].ColumnName = "操作员门店";
            }
            return dtout;
        }
        public DataTable GetCardRecycle(Hashtable htpara)
        {
            DataTable dtout = Qa.GetCardRecycle(htpara);
            if (dtout != null)
            {
                dtout.Columns["vcCardID"].ColumnName = "会员卡号";
                dtout.Columns["vcAssName"].ColumnName = "会员姓名";
                dtout.Columns["vcLinkPhone"].ColumnName = "联系电话";
                dtout.Columns["vcSpell"].ColumnName = "拼音简码";
                dtout.Columns["vcAssType"].ColumnName = "会员类型";
                dtout.Columns["vcAssState"].ColumnName = "会员状态";
                dtout.Columns["nCharge"].ColumnName = "当前余额";
                dtout.Columns["iIgValue"].ColumnName = "当前积分";
                dtout.Columns["vcDeptID"].ColumnName = "门店";
                dtout.Columns["dtCreateDate"].ColumnName = "创建日期";
                dtout.Columns["vcAssNbr"].ColumnName = "身份证号";
                dtout.Columns["vcLinkAddress"].ColumnName = "联系地址";
                dtout.Columns["vcEmail"].ColumnName = "Email";
                dtout.Columns["dtOperDate"].ColumnName = "操作日期";
                dtout.Columns["vcComments"].ColumnName = "备注";
            }
            return dtout;
        }

        public DataTable GetConsQueryMd(Hashtable htpara)
        {
            DataTable dtout = Qa.GetConsQueryMd(htpara);
            if (dtout != null)
            {
                dtout.Columns["iSerial"].ColumnName = "流水";
                dtout.Columns["vcAssName"].ColumnName = "会员名称";
                dtout.Columns["vcAssType"].ColumnName = "会员类型";
                dtout.Columns["vcCardID"].ColumnName = "会员卡号";
                dtout.Columns["vcLocalDeptId"].ColumnName = "发卡门店";
                dtout.Columns["vcGoodsID"].ColumnName = "商品编号";
                dtout.Columns["nPrice"].ColumnName = "单价";
                dtout.Columns["iCount"].ColumnName = "数量";
                dtout.Columns["nFee"].ColumnName = "合计";
                dtout.Columns["vcConsType"].ColumnName = "付款类型";
                dtout.Columns["vcComments"].ColumnName = "备注";
                dtout.Columns["cFlag"].ColumnName = "有效状态";
                dtout.Columns["dtConsDate"].ColumnName = "消费日期";
                dtout.Columns["vcOperName"].ColumnName = "操作员";
                dtout.Columns["vcDeptID"].ColumnName = "门店";
            }
            return dtout;
        }

        public DataTable GetFillQueryMd(Hashtable htpara)
        {
            DataTable dtout = Qa.GetFillQueryMd(htpara);
            if (dtout != null)
            {
                dtout.Columns["iSerial"].ColumnName = "流水";
                dtout.Columns["vcAssName"].ColumnName = "会员名称";
                dtout.Columns["vcAssType"].ColumnName = "会员类型";
                dtout.Columns["vcCardID"].ColumnName = "会员卡号";
                dtout.Columns["vcLocalDeptId"].ColumnName = "发卡门店";
                dtout.Columns["nFillFee"].ColumnName = "充值金额";
                dtout.Columns["nFillProm"].ColumnName = "赠款金额";
                dtout.Columns["nFeeLast"].ColumnName = "上次余额";
                dtout.Columns["nFeeCur"].ColumnName = "当前余额";
                dtout.Columns["vcComments"].ColumnName = "备注";
                dtout.Columns["dtFillDate"].ColumnName = "充值日期";
                dtout.Columns["vcOperName"].ColumnName = "操作员";
                dtout.Columns["vcDeptID"].ColumnName = "操作员门店";
            }
            return dtout;
        }
	}
}
