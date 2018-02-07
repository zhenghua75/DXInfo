<%@ Page Language="C#" Title="库存子表" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" AutoEventWireup="true" CodeBehind="tbStockDetail.aspx.cs"
    Inherits="AMSApp.StockControl.tbStockDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="tbStockDetail.js" type="text/javascript"></script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="dg">
        <thead>
            <tr>
                <th field="cnnDetailId" width="100">
                </th>
                <th field="cnnMainId" width="100">
                </th>
                <th field="cnvcInvCode" width="100">
                </th>
                <th field="cnvcComUnitCode" width="100">
                </th>
                <th field="cnnQuantity" width="100">
                </th>
                <th field="cnvcMainComUnitCode" width="100">
                </th>
                <th field="cnnMainQuantity" width="100">
                </th>
                <th field="cnnPrice" width="100">
                </th>
                <th field="cnnAmount" width="100">
                </th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newtbStockDetail()">
            添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="edittbStockDetail()">
                修改</a> <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="removetbStockDetail()">
                    删除</a> <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchtbStockDetail()">
                        搜索</a> <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="true" onclick="tbStockDetailExportToExcel()">
                            导出</a>
    </div>
    <div id="dlg-detail" style="width: 400px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            库存子表</div>
        <form id="fm-detail" method="post">
        <div class="fitem">
            <label>
            </label>
            <input id="cnnDetailId" name="cnnDetailId" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnnMainId" name="cnnMainId" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcInvCode" name="cnvcInvCode" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcComUnitCode" name="cnvcComUnitCode" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnnQuantity" name="cnnQuantity" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnvcMainComUnitCode" name="cnvcMainComUnitCode" class="easyui-validatebox"
                required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnnMainQuantity" name="cnnMainQuantity" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnnPrice" name="cnnPrice" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cnnAmount" name="cnnAmount" class="easyui-validatebox" required="true" />
        </div>
        </form>
    </div>
    <div id="dlg-detail-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="savetbStockDetail()">
            保存</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">
                取消</a>
    </div>
    <div id="search-dlg" style="width: 400px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            库存子表</div>
        <form id="search-fm" method="post">
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnDetailId" name="cnnDetailId" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnMainId" name="cnnMainId" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcInvCode" name="cnvcInvCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcComUnitCode" name="cnvcComUnitCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnQuantity" name="cnnQuantity" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnvcMainComUnitCode" name="cnvcMainComUnitCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnMainQuantity" name="cnnMainQuantity" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnPrice" name="cnnPrice" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="search-cnnAmount" name="cnnAmount" class="easyui-validatebox" />
        </div>
        </form>
    </div>
    <div id="search-dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="highSearchtbStockDetail()">
            查询</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close')">
                取消</a>
    </div>
    <form id="export-excel" method="post">
    </form>
</asp:Content>
