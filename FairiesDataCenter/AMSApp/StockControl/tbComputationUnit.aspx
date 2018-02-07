<%@ Page Language="C#" Title="计量单位" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" AutoEventWireup="true" CodeBehind="tbComputationUnit.aspx.cs"
    Inherits="AMSApp.StockControl.tbComputationUnit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="tbComputationUnit.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="dg">
        <thead>
            <tr>
                <th field="cnvcComunitCode" width="100">计量单位编码
                </th>
                <th field="cnvcComUnitName" width="100">计量单位名称
                </th>
                <th field="cnvcGroupCode" width="100" hidden="true">计量单位组
                </th>
                <th field="cnvcGroupCodeComments" width="100">计量单位组
                </th>
                <th field="cnbMainUnit" width="100" formatter="formatCheckBox">是否主计量单位
                </th>
                <th field="cniChangRate" width="100" formatter="toFixed2">换算率
                </th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newtbComputationUnit();return false;">
            添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="edittbComputationUnit();return false;">
                修改</a> <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="removetbComputationUnit();return false;">
                    删除</a> <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchtbComputationUnit();return false;">
                        搜索</a> <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="true" onclick="tbComputationUnitExportToExcel();return false;">
                            导出</a>
    </div>
    <div id="dlg" style="width: 400px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            计量单位</div>
        <form id="fm" method="post">
        <div class="fitem">
            <label>计量单位编码
            </label>
            <input id="cnvcComunitCode" name="cnvcComunitCode" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>计量单位名称
            </label>
            <input name="cnvcComUnitName" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>计量单位组
            </label>
            <input id="cc" name="cnvcGroupCode" class="easyui-validatebox"/>
        </div>
        <div class="fitem">
            <label>是否主计量单位
            </label>
            <input name="cnbMainUnit" class="easyui-validatebox" required="true" type="checkbox"/>
        </div>
        <div class="fitem">
            <label>换算率
            </label>
            <input name="cniChangRate" class="easyui-validatebox" validtype="Digit" required="true" />
        </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="savetbComputationUnit();return false;">
            保存</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');return false;">
                取消</a>
    </div>
    <div id="search-dlg" style="width: 400px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            计量单位</div>
        <form id="search-fm" method="post">
        <div class="fitem">
            <label>计量单位编码
            </label>
            <input id="search-cnvcComunitCode" name="cnvcComunitCode" />
        </div>
        <div class="fitem">
            <label>计量单位名称
            </label>
            <input id="search-cnvcComUnitName" name="cnvcComUnitName" />
        </div>
        <div class="fitem">
            <label>计量单位组
            </label>
            <input id="search-cnvcGroupCode" name="cnvcGroupCode" />
        </div>
        <div class="fitem">
            <label>是否主计量单位
            </label>
            <input id="search-cnbMainUnit" name="cnbMainUnit" type="checkbox"/>
        </div>
        <div class="fitem">
            <label>换算率
            </label>
            <input id="search-cniChangRate" name="cniChangRate" class="easyui-validatebox" validtype="Digit"/>
        </div>
        </form>
    </div>
    <div id="search-dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="highSearchtbComputationUnit();return false;">
            查询</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">
                取消</a>
    </div>
    <form id="export-excel" method="post">
    </form>
</asp:Content>
