<%@ Page language="c#" Title="计量单位组" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" Codebehind="tbComputationGroup.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.StockControl.tbComputationGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="tbComputationGroup.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <table id="dg">
            <thead>  
                <tr>  
                    <th field="cnvcGroupCode" width="100">计量单位组编码</th>  
                    <th field="cnvcGroupName" width="100">计量单位组名称</th>
                </tr>  
            </thead> 
        </table> 
        <div id="toolbar">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true"  onclick="newComputationGroup();return false;">添加</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true"  onclick="editComputationGroup();return false;">修改</a> 
            <a href="#" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="removeComputationGroup();return false;">删除</a>              
            <a href="#" class="easyui-linkbutton" iconCls="icon-search" plain="true"  onclick="searchComputationGroup();return false;">搜索</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-save" plain="true"  onclick="computationGroupExportToExcel();return false;">导出</a>  
        </div>  
        <div id="dlg"  style="width:400px;height:280px;padding:10px 20px">  
            <div class="ftitle">计量单位组</div>  
            <form id="fm" method="post">  
                <div class="fitem">  
                    <label>计量单位组编码：</label>  
                    <input id="cnvcGroupCode" name="cnvcGroupCode" class="easyui-validatebox" required="true"/>  
                </div>  
                <div class="fitem">  
                    <label>计量单位组名称：</label>  
                    <input name="cnvcGroupName" class="easyui-validatebox" required="true"/>  
                </div> 
            </form>  
        </div>  
        <div id="dlg-buttons">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="saveComputationGroup();return false;">保存</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');return false;">取消</a>  
        </div>  

        <div id="search-dlg"  style="width:400px;height:280px;padding:10px 20px">  
            <div class="ftitle">计量单位组</div>  
            <form id="search-fm" method="post">  
                <div class="fitem">  
                    <label>计量单位组编码</label>                      
                    <input id="search-code" name="cnvcGroupCode" class="easyui-validatebox"/>  
                </div>  
                <div class="fitem">  
                    <label>计量单位组名称</label>  
                    <input id="search-name" name="cnvcGroupName" class="easyui-validatebox"/>  
                </div> 
            </form>  
        </div>  
        <div id="search-dlg-buttons">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="highSearchComputationGroup();return false;">查询</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">取消</a>  
        </div>  
        <form id="export-excel" method="post"></form>
</asp:Content>
