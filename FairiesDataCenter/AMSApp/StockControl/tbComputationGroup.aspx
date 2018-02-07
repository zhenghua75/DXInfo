<%@ Page language="c#" Title="������λ��" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" Codebehind="tbComputationGroup.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.StockControl.tbComputationGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="tbComputationGroup.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <table id="dg">
            <thead>  
                <tr>  
                    <th field="cnvcGroupCode" width="100">������λ�����</th>  
                    <th field="cnvcGroupName" width="100">������λ������</th>
                </tr>  
            </thead> 
        </table> 
        <div id="toolbar">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true"  onclick="newComputationGroup();return false;">���</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true"  onclick="editComputationGroup();return false;">�޸�</a> 
            <a href="#" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="removeComputationGroup();return false;">ɾ��</a>              
            <a href="#" class="easyui-linkbutton" iconCls="icon-search" plain="true"  onclick="searchComputationGroup();return false;">����</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-save" plain="true"  onclick="computationGroupExportToExcel();return false;">����</a>  
        </div>  
        <div id="dlg"  style="width:400px;height:280px;padding:10px 20px">  
            <div class="ftitle">������λ��</div>  
            <form id="fm" method="post">  
                <div class="fitem">  
                    <label>������λ����룺</label>  
                    <input id="cnvcGroupCode" name="cnvcGroupCode" class="easyui-validatebox" required="true"/>  
                </div>  
                <div class="fitem">  
                    <label>������λ�����ƣ�</label>  
                    <input name="cnvcGroupName" class="easyui-validatebox" required="true"/>  
                </div> 
            </form>  
        </div>  
        <div id="dlg-buttons">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="saveComputationGroup();return false;">����</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');return false;">ȡ��</a>  
        </div>  

        <div id="search-dlg"  style="width:400px;height:280px;padding:10px 20px">  
            <div class="ftitle">������λ��</div>  
            <form id="search-fm" method="post">  
                <div class="fitem">  
                    <label>������λ�����</label>                      
                    <input id="search-code" name="cnvcGroupCode" class="easyui-validatebox"/>  
                </div>  
                <div class="fitem">  
                    <label>������λ������</label>  
                    <input id="search-name" name="cnvcGroupName" class="easyui-validatebox"/>  
                </div> 
            </form>  
        </div>  
        <div id="search-dlg-buttons">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="highSearchComputationGroup();return false;">��ѯ</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">ȡ��</a>  
        </div>  
        <form id="export-excel" method="post"></form>
</asp:Content>
