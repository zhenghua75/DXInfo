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
				dtout.Columns["iSerial"].ColumnName="��ˮ";
				dtout.Columns["vcAssName"].ColumnName="��Ա����";
				dtout.Columns["vcAssType"].ColumnName="��Ա����";
				dtout.Columns["vcCardID"].ColumnName="��Ա����";
				dtout.Columns["vcGoodsName"].ColumnName="��Ʒ����";
				dtout.Columns["nPrice"].ColumnName="����";
				dtout.Columns["iCount"].ColumnName="����";
				dtout.Columns["nFee"].ColumnName="�ϼ�";
				dtout.Columns["vcConsType"].ColumnName="��������";
				dtout.Columns["vcComments"].ColumnName="��ע";
				dtout.Columns["cFlag"].ColumnName="��Ч״̬";
				dtout.Columns["dtConsDate"].ColumnName="��������";
				dtout.Columns["vcOperName"].ColumnName="����Ա";
				dtout.Columns["vcDeptID"].ColumnName="�ŵ�";
			}
			return dtout;
		}

		public DataTable GetFillQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetFillQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["iSerial"].ColumnName="��ˮ";
				dtout.Columns["vcAssName"].ColumnName="��Ա����";
				dtout.Columns["vcAssType"].ColumnName="��Ա����";
				dtout.Columns["vcCardID"].ColumnName="��Ա����";
				dtout.Columns["nFillFee"].ColumnName="��ֵ���";
				dtout.Columns["nFillProm"].ColumnName="������";
				dtout.Columns["nFeeLast"].ColumnName="�ϴ����";
				dtout.Columns["nFeeCur"].ColumnName="��ǰ���";
				dtout.Columns["vcComments"].ColumnName="��ע";
				dtout.Columns["dtFillDate"].ColumnName="��ֵ����";
				dtout.Columns["vcOperName"].ColumnName="����Ա";
				dtout.Columns["vcDeptID"].ColumnName="����Ա�ŵ�";
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
					dtout.Columns["vcDeptID"].ColumnName="�ŵ�";
				}
				if(dtout.Columns.Contains("vcAssType"))
				{
					dtout.Columns["vcAssType"].ColumnName="��Ա����";
				}
				if(dtout.Columns.Contains("vcGoodsType"))
				{
					dtout.Columns["vcGoodsType"].ColumnName="��Ʒ����";
				}
				dtout.Columns["vcGoodsName"].ColumnName="��Ʒ����";
				dtout.Columns["tolcount"].ColumnName="�����ϼ�";
				dtout.Columns["tolfee"].ColumnName="���ϼ�";
			}
			return dtout;
		}

		public DataTable GetBusiLogQuery(Hashtable htpara)
		{
			DataTable dtout=Qa.GetBusiLogQuery(htpara);
			if(dtout!=null)
			{
				dtout.Columns["iSerial"].ColumnName="��ˮ";
				dtout.Columns["vcAssName"].ColumnName="��Ա����";
				dtout.Columns["vcAssType"].ColumnName="��Ա����";
				dtout.Columns["vcCardID"].ColumnName="��Ա����";
				dtout.Columns["vcCommName"].ColumnName="��������";
				dtout.Columns["vcOperName"].ColumnName="����Ա";
				dtout.Columns["dtOperDate"].ColumnName="��������";
				dtout.Columns["vcDeptID"].ColumnName="����Ա�ŵ�";
				dtout.Columns["vcComments"].ColumnName="��ע";
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

            #region ��������������ѯ�ŵ�Ϊȫ������ֻ�д��������û������ı������������
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
                            dr["type"] = "ԭ״̬";
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
                            dr["type"] = "��״̬";
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
                            dr["type"] = "�����Ա";
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
                            dr["type"] = "��ʧ��Ա";
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
                            dr["type"] = "������";
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
                            dr["type"] = "������Ա";
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
                            dr["type"] = "��ֵ";
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
                        //							dr["type"]="��ͨ��Ա����";
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
                        //							dr["type"]="������Ա����";
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
                        //							dr["type"]="�𿨻�Ա����";
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
                        //							dr["type"]="������Ա����";
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
                            dr["type"] = "����";
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
                            dr["type"] = "��Ա����";
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
                            dr["type"] = "���ֶһ�";
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
                            dr["type"] = "���п�����";
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
                            dr["type"] = "���п���ֵ";
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
                            dr["type"] = drat["vcCommName"].ToString() + "����";
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
            dr["col1"] = "��Ա��������";
            dr["col2"] = "���������ܶ�";
            dr["col3"] = "��������ܶ�";
            dr["col4"] = "�ֽ������ܶ�";
            dr["col5"] = "���������ܶ�";
            dr["col6"] = "�����ܶ�";
            dr["col7"] = "��Ʒ��������";
            dtdis.Rows.Add(dr);

            int lastrow = dsIn.Tables["AllIncome"].Rows.Count - 1;
            dr = dtdis.NewRow();
            dr["type"] = "�ܼ�";
            dr["col1"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP1"].ToString();
            dr["col2"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP2"].ToString();
            dr["col3"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP3"].ToString();
            dr["col4"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP4"].ToString();
            dr["col5"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP5"].ToString();
            dr["col6"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP6"].ToString();
            dr["col7"] = dsIn.Tables["AllIncome"].Rows[lastrow]["REP7"].ToString();
            dtdis.Rows.Add(dr);

            //			if(strname=="ȫ��")
            //			{
            //				dtdis.Columns["type"].ColumnName="�����ŵ�";
            //			}
            //			else
            //			{
            //				dtdis.Columns["type"].ColumnName=strname;
            //			}			
            //			dtdis.Columns["col1"].ColumnName="��Ա��";
            //			dtdis.Columns["col2"].ColumnName="���û���";
            //			dtdis.Columns["col3"].ColumnName="ʹ�û���";
            //			dtdis.Columns["col4"].ColumnName="���";
            //			dtdis.Columns["col5"].ColumnName="�������";
            //			dtdis.Columns["col6"].ColumnName="����";
            //			dtdis.Columns["col7"].ColumnName="��Ʒ��";
            //
            //			dsret.Tables.Add(dtdis);
            #endregion

            if (strname != "ȫ��")
            {
                #region �����Ա�ڱ����ֵ���������
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
                dr["type"] = "�����Ա�ڱ���";
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
                                dr["type"] = "��ֵ";
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
                            //								dr["type"]="��ͨ��Ա����";
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
                            //								dr["type"]="������Ա����";
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
                            //								dr["type"]="�𿨻�Ա����";
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
                            //								dr["type"]="������Ա����";
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
                                dr["type"] = "��Ա����";
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
                                dr["type"] = "���ֶһ�";
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
                                dr["type"] = drat["vcCommName"].ToString() + "����";
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
                //				dr["col1"]="��Ա��������";
                //				dr["col2"]="���������ܶ�";
                //				dr["col3"]="��������ܶ�";
                //				dr["col4"]="�ֽ������ܶ�";
                //				dr["col5"]="���������ܶ�";
                //				dr["col6"]="��Ա�����ܶ�";
                //				dr["col7"]="��Ʒ��������";
                //				dtdis.Rows.Add(dr);
                //
                //				dr=dtdis.NewRow();
                //				dr["type"]="�ܼ�";
                //				dr["col1"]=dtreg.Rows[10]["REP1"].ToString();
                //				dr["col2"]=dtreg.Rows[10]["REP2"].ToString();
                //				dr["col3"]=dtreg.Rows[10]["REP3"].ToString();
                //				dr["col4"]=dtreg.Rows[10]["REP4"].ToString();
                //				dr["col5"]=dtreg.Rows[10]["REP5"].ToString();
                //				dr["col6"]=dtreg.Rows[10]["REP6"].ToString();
                //				dr["col7"]=dtreg.Rows[10]["REP7"].ToString();
                //				dtdis.Rows.Add(dr);
                #endregion

                #region �����Ա�ڱ����ֵ���������
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
                dr["type"] = "�����Ա�ڱ���";
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
                                dr["type"] = "��ֵ";
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
                            //								dr["type"]="��ͨ��Ա����";
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
                            //								dr["type"]="������Ա����";
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
                            //								dr["type"]="�𿨻�Ա����";
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
                            //								dr["type"]="������Ա����";
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
                                dr["type"] = "��Ա����";
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
                                dr["type"] = "���ֶһ�";
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
                                dr["type"] = drat["vcCommName"].ToString() + "����";
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

                #region �����Ա�������ֵ���������
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
                dr["type"] = "�����Ա������";
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
                                dr["type"] = "��ֵ";
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
                            //								dr["type"]="��ͨ��Ա����";
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
                            //								dr["type"]="������Ա����";
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
                            //								dr["type"]="�𿨻�Ա����";
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
                            //								dr["type"]="������Ա����";
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
                                dr["type"] = "��Ա����";
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
                                dr["type"] = "���ֶһ�";
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
                                dr["type"] = drat["vcCommName"].ToString() + "����";
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

            if (strname == "ȫ��")
            {
                dtdis.Columns["type"].ColumnName = "�����ŵ�";
            }
            else
            {
                dtdis.Columns["type"].ColumnName = strname;
            }
            dtdis.Columns["col1"].ColumnName = "��Ա��";
            dtdis.Columns["col2"].ColumnName = "���û���";
            dtdis.Columns["col3"].ColumnName = "ʹ�û���";
            dtdis.Columns["col4"].ColumnName = "���";
            dtdis.Columns["col5"].ColumnName = "�������";
            dtdis.Columns["col6"].ColumnName = "����";
            dtdis.Columns["col7"].ColumnName = "��Ʒ��";

            return dtdis;
        }

		public DataTable GetTopQuery(Hashtable htpara,string strtype)
		{
			DataTable dtout=Qa.GetTopQuery(htpara,strtype);
			if(dtout!=null)
			{
				if(strtype=="0")
				{
                    dtout.Columns["vcGoodsID"].ColumnName = "��ƷID";
                    dtout.Columns["vcCommName"].ColumnName = "��Ʒ����";
                    dtout.Columns["vcGoodsName"].ColumnName = "��Ʒ����";
                    dtout.Columns["salecount"].ColumnName = "��������";
                    dtout.Columns["nFee"].ColumnName = "���۽��";
				}
				else
				{
					dtout.Columns["vcCardID"].ColumnName="��Ա����";
					dtout.Columns["vcAssName"].ColumnName="��Ա����";
					dtout.Columns["salefee"].ColumnName="���Ѷ�";
				}
			}
			return dtout;
		}

		public DataTable GetAssInfo(Hashtable htpara)
		{
			DataTable dtout=Qa.GetAssInfo(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcCardID"].ColumnName="��Ա����";
				dtout.Columns["vcAssName"].ColumnName="��Ա����";
				dtout.Columns["vcLinkPhone"].ColumnName="��ϵ�绰";
				dtout.Columns["vcSpell"].ColumnName="ƴ������";
				dtout.Columns["vcAssType"].ColumnName="��Ա����";
				dtout.Columns["vcAssState"].ColumnName="��Ա״̬";
				dtout.Columns["nCharge"].ColumnName="��ǰ���";
				dtout.Columns["iIgValue"].ColumnName="��ǰ����";
				dtout.Columns["vcDeptID"].ColumnName="�ŵ�";
				dtout.Columns["dtCreateDate"].ColumnName="��������";
				dtout.Columns["vcAssNbr"].ColumnName="���֤��";
				dtout.Columns["vcLinkAddress"].ColumnName="��ϵ��ַ";
				dtout.Columns["vcEmail"].ColumnName="Email";
				dtout.Columns["dtOperDate"].ColumnName="��������";
				dtout.Columns["vcComments"].ColumnName="��ע";
				dtout.Columns.Add("����");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["����"]="<a href='wfmAssMod.aspx?aid=" + dtout.Rows[i]["iAssID"].ToString() + "&cid="+ dtout.Rows[i]["��Ա����"].ToString() + "'>�޸�</a>";
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
				dtout.Columns["Dept"].ColumnName="������Դ";
				dtout.Columns["vcFileName"].ColumnName="�ļ�����";
				dtout.Columns["FileSize"].ColumnName="�ļ���С";
				dtout.Columns["dtStartDate"].ColumnName="��ʼ����ʱ��";
				dtout.Columns["dtFinDate"].ColumnName="�������ʱ��";
				dtout.Columns["Type"].ColumnName="���´�����";
			}
			return dtout;
		}

		public DataTable GetDailyCashQuery(Hashtable htpara)
		{
			DataTable dtDaily=new DataTable();
			DataTable dtout=Qa.GetDailyCashQuery(htpara);
			if(dtout.Rows.Count>0)
			{
				dtDaily.Columns.Add("ͳ����");
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
				drFillCount["ͳ����"]="�ֽ��ֵ����";
				DataRow drFillFee=dtDaily.NewRow();
				drFillFee["ͳ����"]="�ֽ��ֵ���";
				DataRow drBankFillCount=dtDaily.NewRow();
				drBankFillCount["ͳ����"]="���п���ֵ����";
				DataRow drBankFillFee=dtDaily.NewRow();
				drBankFillFee["ͳ����"]="���п���ֵ���";
				DataRow drConsCount=dtDaily.NewRow();
				drConsCount["ͳ����"]="���Ѵ���";
				DataRow drRetailCons=dtDaily.NewRow();
				drRetailCons["ͳ����"]="�ֽ����۽��";
				DataRow drBankRetailCons=dtDaily.NewRow();
				drBankRetailCons["ͳ����"]="���п����۽��";
				DataRow drAssCons=dtDaily.NewRow();
				drAssCons["ͳ����"]="��Ա���ѽ��";
				DataRow drReCycleCount=dtDaily.NewRow();
				drReCycleCount["ͳ����"]="���տ���";
				DataRow drReCycleFee=dtDaily.NewRow();
				drReCycleFee["ͳ����"]="�����˿���";
				DataRow drLargCount=dtDaily.NewRow();
				drLargCount["ͳ����"]="���ʹ���";
				DataRow drCashFee=dtDaily.NewRow();
				drCashFee["ͳ����"]="�ֽ��ܶ�";
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
				dtDaily.Columns.Add("���κμ�¼��");
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
				dtout.Columns["vcConsType"].ColumnName="������������";
				dtout.Columns["vcGoodsName"].ColumnName="��Ʒ����";
				dtout.Columns["tolCount"].ColumnName="����";
				dtout.Columns["tolfee"].ColumnName="���۽��";
				dtout.Columns["tolcash"].ColumnName="�ֽ�";
			}
			return dtout;
		}

        public DataTable GetIGQuery(Hashtable htpara)
        {
            DataTable dtout = Qa.GetIGQuery(htpara);
            if (dtout != null)
            {
                dtout.Columns["iSerial"].ColumnName = "��ˮ";
                dtout.Columns["vcAssName"].ColumnName = "��Ա����";
                dtout.Columns["vcAssType"].ColumnName = "��Ա����";
                dtout.Columns["vcCardID"].ColumnName = "��Ա����";
                dtout.Columns["iIgLast"].ColumnName = "�ϴ����";
                dtout.Columns["iIgGet"].ColumnName = "���λ���";
                dtout.Columns["iIgArrival"].ColumnName = "��ǰ����";
                dtout.Columns["vcComments"].ColumnName = "��ע";
                dtout.Columns["dtIgDate"].ColumnName = "��������";
                dtout.Columns["vcOperName"].ColumnName = "����Ա";
                dtout.Columns["vcDeptID"].ColumnName = "����Ա�ŵ�";
            }
            return dtout;
        }
        public DataTable GetCardRecycle(Hashtable htpara)
        {
            DataTable dtout = Qa.GetCardRecycle(htpara);
            if (dtout != null)
            {
                dtout.Columns["vcCardID"].ColumnName = "��Ա����";
                dtout.Columns["vcAssName"].ColumnName = "��Ա����";
                dtout.Columns["vcLinkPhone"].ColumnName = "��ϵ�绰";
                dtout.Columns["vcSpell"].ColumnName = "ƴ������";
                dtout.Columns["vcAssType"].ColumnName = "��Ա����";
                dtout.Columns["vcAssState"].ColumnName = "��Ա״̬";
                dtout.Columns["nCharge"].ColumnName = "��ǰ���";
                dtout.Columns["iIgValue"].ColumnName = "��ǰ����";
                dtout.Columns["vcDeptID"].ColumnName = "�ŵ�";
                dtout.Columns["dtCreateDate"].ColumnName = "��������";
                dtout.Columns["vcAssNbr"].ColumnName = "���֤��";
                dtout.Columns["vcLinkAddress"].ColumnName = "��ϵ��ַ";
                dtout.Columns["vcEmail"].ColumnName = "Email";
                dtout.Columns["dtOperDate"].ColumnName = "��������";
                dtout.Columns["vcComments"].ColumnName = "��ע";
            }
            return dtout;
        }

        public DataTable GetConsQueryMd(Hashtable htpara)
        {
            DataTable dtout = Qa.GetConsQueryMd(htpara);
            if (dtout != null)
            {
                dtout.Columns["iSerial"].ColumnName = "��ˮ";
                dtout.Columns["vcAssName"].ColumnName = "��Ա����";
                dtout.Columns["vcAssType"].ColumnName = "��Ա����";
                dtout.Columns["vcCardID"].ColumnName = "��Ա����";
                dtout.Columns["vcLocalDeptId"].ColumnName = "�����ŵ�";
                dtout.Columns["vcGoodsID"].ColumnName = "��Ʒ���";
                dtout.Columns["nPrice"].ColumnName = "����";
                dtout.Columns["iCount"].ColumnName = "����";
                dtout.Columns["nFee"].ColumnName = "�ϼ�";
                dtout.Columns["vcConsType"].ColumnName = "��������";
                dtout.Columns["vcComments"].ColumnName = "��ע";
                dtout.Columns["cFlag"].ColumnName = "��Ч״̬";
                dtout.Columns["dtConsDate"].ColumnName = "��������";
                dtout.Columns["vcOperName"].ColumnName = "����Ա";
                dtout.Columns["vcDeptID"].ColumnName = "�ŵ�";
            }
            return dtout;
        }

        public DataTable GetFillQueryMd(Hashtable htpara)
        {
            DataTable dtout = Qa.GetFillQueryMd(htpara);
            if (dtout != null)
            {
                dtout.Columns["iSerial"].ColumnName = "��ˮ";
                dtout.Columns["vcAssName"].ColumnName = "��Ա����";
                dtout.Columns["vcAssType"].ColumnName = "��Ա����";
                dtout.Columns["vcCardID"].ColumnName = "��Ա����";
                dtout.Columns["vcLocalDeptId"].ColumnName = "�����ŵ�";
                dtout.Columns["nFillFee"].ColumnName = "��ֵ���";
                dtout.Columns["nFillProm"].ColumnName = "������";
                dtout.Columns["nFeeLast"].ColumnName = "�ϴ����";
                dtout.Columns["nFeeCur"].ColumnName = "��ǰ���";
                dtout.Columns["vcComments"].ColumnName = "��ע";
                dtout.Columns["dtFillDate"].ColumnName = "��ֵ����";
                dtout.Columns["vcOperName"].ColumnName = "����Ա";
                dtout.Columns["vcDeptID"].ColumnName = "����Ա�ŵ�";
            }
            return dtout;
        }
	}
}
