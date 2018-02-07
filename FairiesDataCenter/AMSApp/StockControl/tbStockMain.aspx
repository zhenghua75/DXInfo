<%@ Page Language="C#" Title="库存主表" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" AutoEventWireup="true" CodeBehind="tbStockMain.aspx.cs" Inherits="AMSApp.StockControl.tbStockMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="tbStockMain.js" type="text/javascript"></script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="dg">
        <thead>
            <tr>
                <th field="cnnMainId" width="100">
                </th>
                <th field="cnvcSupplierCode" width="100">
                </th>
                <th field="cnvcWhCode" width="100">
                </th>
                <th field="cnvcDeptId" width="100">
                </th>
                <th field="cnvcOperType" width="100">
                </th>
                <th field="cndCreateDate" width="100">
                </th>
                <th field="cnvcCreaterId" width="100">
                </th>
                <th field="cnvcCreaterName" width="100">
                </th>
                <th field="cndCheckDate" width="100">
                </th>
                <th field="cnvcCheckerId" width="100">
                </th>
                <th field="cnvcCheckerName" width="100">
                </th>
                <th field="cndBusinessDate" width="100">
                </th>
                <th field="cnnYear" width="100">
                </th>
                <th field="cnnMonth" width="100">
                </th>
                <th field="cnnStatus" width="100">
                </th>
                <th field="cnvcComments" width="100">
                </th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newtbStockMain()">
            添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="edittbStockMain()">
                修改</a> <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="removetbStockMain()">
                    删除</a> <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchtbStockMain()">
                        搜索</a> <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="true" onclick="tbStockMainExportToExcel()">
                            导出</a>
    </div>
    <div id="dlg" style="width: 400px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            库存主表</div>
        <form id="fm" method="post">
        <div class="fitem">
            <label>
            </label>
            <input id="cnnMainId" name="cnnMainId" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcSupplierCode" name="cnvcSupplierCode" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcWhCode" name="cnvcWhCode" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcDeptId" name="cnvcDeptId" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcOperType" name="cnvcOperType" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cndCreateDate" name="cndCreateDate" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcCreaterId" name="cnvcCreaterId" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcCreaterName" name="cnvcCreaterName" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cndCheckDate" name="cndCheckDate" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcCheckerId" name="cnvcCheckerId" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcCheckerName" name="cnvcCheckerName" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cndBusinessDate" name="cndBusinessDate" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnnYear" name="cnnYear" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnnMonth" name="cnnMonth" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnnStatus" name="cnnStatus" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcComments" name="cnvcComments" class="easyui-validatebox" required="true" />
        </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="savetbStockMain()">
            保存</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">
                取消</a>
    </div>
    <div id="search-dlg" style="width: 400px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            库存主表</div>
        <form id="search-fm" method="post">
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnMainId" name="cnnMainId" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcSupplierCode" name="cnvcSupplierCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcWhCode" name="cnvcWhCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcDeptId" name="cnvcDeptId" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcOperType" name="cnvcOperType" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cndCreateDate" name="cndCreateDate" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcCreaterId" name="cnvcCreaterId" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcCreaterName" name="cnvcCreaterName" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cndCheckDate" name="cndCheckDate" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcCheckerId" name="cnvcCheckerId" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcCheckerName" name="cnvcCheckerName" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cndBusinessDate" name="cndBusinessDate" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnYear" name="cnnYear" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnMonth" name="cnnMonth" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnStatus" name="cnnStatus" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcComments" name="cnvcComments" class="easyui-validatebox" />
        </div>
        </form>
    </div>
    <div id="search-dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="highSearchtbStockMain()">
            查询</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close')">
                取消</a>
    </div>
    <form id="export-excel" method="post">
    </form>
</asp:Content>
