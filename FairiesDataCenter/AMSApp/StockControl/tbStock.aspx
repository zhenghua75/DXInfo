<%@ Page Title="库存" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" Language="C#"
    AutoEventWireup="true" CodeBehind="tbStock.aspx.cs" Inherits="AMSApp.StockControl.tbStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="tbStock.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <table id="dg-main">
        </table>
        <div id="dlg-main" style="width: 850px; height: 580px; padding: 10px 20px">
            <div class="ftitle">
                库存主表</div>
            <form id="fm-main" method="post">
            <div class="fitem" style="display: none">
                <label>
                </label>
                <input type="text" id="cnnMainId" name="cnnMainId" class="easyui-validatebox" />
            </div>
            <div class="fitem" id='divcnvcSupplierCode'>
                <label>
                    供应商
                </label>
                <input type="text" id="cnvcSupplierCode" name="cnvcSupplierCode" class="easyui-validatebox"
                    style="width: 160px" />
            </div>
            <div id="OutWh" class="ftitle">
                调出仓</div>
            <div class="fitem">
                <label>
                    仓库
                </label>
                <input type="text" id="cnvcWhCode" name="cnvcWhCode" class="easyui-validatebox" style="width: 160px" />
            </div>
            <div class="fitem">
                <label>
                    部门
                </label>
                <input type="text" id="cnvcDeptId" name="cnvcDeptId" class="easyui-validatebox" style="width: 160px" />
            </div>
            <div id="InWh" class="ftitle">
                调入仓</div>
            <div class="fitem" id="divInWh">
                <label>
                    仓库
                </label>
                <input type="text" id="incnvcWhCode" name="incnvcWhCode" class="easyui-validatebox" style="width: 160px" />
            </div>
            <div class="fitem" id="divInDept">
                <label>
                    部门
                </label>
                <input type="text" id="incnvcDeptId" name="incnvcDeptId" class="easyui-validatebox" style="width: 160px" />
            </div>
            <div class="fitem" style="display: none">
                <label>
                </label>
                <input type="text" id="cnnOperType" name="cnnOperType" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    业务日期
                </label>
                <input type="text" id="cndBusinessDate" name="cndBusinessDate" class="easyui-validatebox"
                    style="width: 160px" />
            </div>
            <div class="fitem">
                <label>
                    描述
                </label>
                <input type="text" id="cnvcComments" name="cnvcComments" class="easyui-validatebox" />
            </div>
            </form>
            <table id="dg-detail">
            </table>
        </div>
        <div id="dlg-detail" style="width: 500px; height: 280px; padding: 10px 20px">
            <div class="ftitle">
                库存子表</div>
            <form id="fm-detail" method="post">
            <div class="fitem" style="display: none">
                <label>
                </label>
                <input type="text" id="cnnMainId-detail" name="cnnMainId" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    子表流水
                </label>
                <input type="text" id="cnnDetailId" name="cnnDetailId" class="easyui-validatebox"
                    readonly="true" />
            </div>
            <div class="fitem">
                <label>
                    存货
                </label>
                <input type="text" id="cnvcInvCode" name="cnvcInvCode" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    计量单位
                </label>
                <input type="text" id="cnvcComUnitCode" name="cnvcComUnitCode" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    数量
                </label>
                <input type="text" id="cnnQuantity" name="cnnQuantity" class="easyui-validatebox"
                    required="true" />
            </div>
            <div id="divcnnPrice" class="fitem">
                <label>
                    单价
                </label>
                <input type="text" id="cnnPrice" name="cnnPrice" class="easyui-validatebox" required="true" />
            </div>
            <div id="divcnnAmount" class="fitem">
                <label>
                    金额
                </label>
                <input type="text" id="cnnAmount" name="cnnAmount" class="easyui-validatebox" required="true" />
            </div>
            </form>
        </div>
        <div id="search-dlg" style="width: 500px; height: 280px; padding: 10px 20px">
            <form id="search-fm" method="post">
            <div class="ftitle">
                库存主表</div>
            <div class="fitem">
                <label>
                    主表流水
                </label>
                <input type="text" id="search-cnnMainId" name="cnnMainId" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    供应商
                </label>
                <input type="text" id="search-cnvcSupplierCode" name="cnvcSupplierCode" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    仓库
                </label>
                <input type="text" id="search-cnvcWhCode" name="cnvcWhCode" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    部门
                </label>
                <input type="text" id="search-cnvcDeptId" name="cnvcDeptId" class="easyui-validatebox" />
            </div>
            <%--<div class="fitem">
            <label>                
            </label>
            <input id="search-cnnOperType" name="cnvcOperType" class="easyui-validatebox" hidden="true"/>
        </div>--%>
            <div class="fitem">
                <label>
                    创建时间
                </label>
                <input type="text" id="search-cndCreateDate" name="cndCreateDate" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                </label>
                <input type="text" id="search-cndCreateDate2" name="cndCreateDate2" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    创建人编码
                </label>
                <input type="text" id="search-cnvcCreaterId" name="cnvcCreaterId" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    创建人姓名
                </label>
                <input type="text" id="search-cnvcCreaterName" name="cnvcCreaterName" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    审核时间
                </label>
                <input type="text" id="search-cndCheckDate" name="cndCheckDate" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                </label>
                <input type="text" id="search-cndCheckDate2" name="cndCheckDate2" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    审核人编码
                </label>
                <input type="text" id="search-cnvcCheckerId" name="cnvcCheckerId" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    审核人姓名
                </label>
                <input type="text" id="search-cnvcCheckerName" name="cnvcCheckerName" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    业务日期
                </label>
                <input type="text" id="search-cndBusinessDate" name="cndBusinessDate" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                </label>
                <input type="text" id="search-cndBusinessDate2" name="cndBusinessDate2" class="easyui-validatebox" />
            </div>
            <%--<div class="fitem">
            <label>
            </label>
            <input id="search-cnnYear" name="cnnYear" class="easyui-validatebox" hidden="true"/>
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnMonth" name="cnnMonth" class="easyui-validatebox" hidden="true"/>
        </div>--%>
            <div class="fitem">
                <label>
                    状态
                </label>
                <input type="text" id="search-cnnStatus" name="cnnStatus" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    描述
                </label>
                <input type="text" id="search-cnvcComments" name="cnvcComments" class="easyui-validatebox" />
            </div>
            <div class="ftitle">
                库存子表</div>
            <div class="fitem">
                <label>
                    子表流水
                </label>
                <input type="text" id="search-cnnDetailId" name="cnnDetailId" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    存货
                </label>
                <input type="text" id="search-cnvcInvCode" name="cnvcInvCode" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    计量单位
                </label>
                <input type="text" id="search-cnvcComUnitCode" name="cnvcComUnitCode" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    数量
                </label>
                <input type="text" id="search-cnnQuantity" name="cnnQuantity" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    单价
                </label>
                <input type="text" id="search-cnnPrice" name="cnnPrice" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    金额
                </label>
                <input type="text" id="search-cnnAmount" name="cnnAmount" class="easyui-validatebox" />
            </div>
            </form>
        </div>
        <div id="dlg-monthlyBalance" style="width: 950px; height: 480px; padding: 10px 20px">
            <table id="dg-monthlyBalance">
            </table>
        </div>
        <div id="dlg-monthlyBalance-oper" style="width: 500px; height: 280px; padding: 10px 20px">
            <form id="fm-monthlyBalance-oper" method="post">
            <div class="ftitle">
                月结</div>
            <div class="fitem">
                <label>
                    年
                </label>
                <input type="text" id="cnnYear" name="cnnYear" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    月
                </label>
                <input type="text" id="cnnMonth" name="cnnMonth" class="easyui-validatebox" />
            </div>
            </form>
        </div>
        <div id="dlg-print" style="width: 800px; height: 380px; padding: 10px 20px">
            <div id="divprint">
                <div class="fitem">
                    <label>
                        主表流水
                    </label>
                    <span id="print-cnnMainId"></span>
                </div>
                <div class="fitem">
                    <label>
                        供应商
                    </label>
                    <span id="print-cnvcSupplierCodeComments"></span>
                </div>
                <div class="fitem">
                    <label>
                        仓库
                    </label>
                    <span id="print-cnvcWhCodeComments"></span>
                </div>
                <div class="fitem">
                    <label>
                        部门
                    </label>
                    <span id="print-cnvcDeptIdComments"></span>
                </div>
                <div class="fitem">
                    <label>
                        业务日期
                    </label>
                    <span id="print-cndBusinessDate"></span>
                </div>
                <div class="fitem">
                    <label>
                        描述
                    </label>
                    <span id="print-cnvcComments"></span>
                </div>
                <table id="dg-print">
                </table>
            </div>
        </div>
        <form id="export-excel" method="post">
        </form>
    </div>
</asp:Content>
