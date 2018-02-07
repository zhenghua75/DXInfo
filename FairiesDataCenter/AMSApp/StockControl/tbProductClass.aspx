<%@ Page language="c#" Title="�������" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" Codebehind="tbProductClass.aspx.cs" AutoEventWireup="true" Inherits="AMSApp.StockControl.tbProductClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="tbProductClass.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <table id="dg">
            <thead>  
                <tr>  
                    <th field="cnvcProductType" width="100" hidden="true">��Ʒ�����</th>  
                    <th field="cnvcProductTypeComments" width="100">��Ʒ��</th>  
                    <th field="cnvcProductClassCode" width="100">������</th>  
                    <th field="cnvcProductClassName" width="100">�������</th>  
                    <th field="cnnDays" width="50">��������</th> 
                    <th field="cnvcComments" width="300">����</th> 
                </tr>  
            </thead> 
        </table> 
        <div id="toolbar">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true"  onclick="newProductClass();return false;">���</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true"  onclick="editProductClass();return false;">�޸�</a> 
            <a href="#" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="removeProductClass();return false;">ɾ��</a>              
            <a href="#" class="easyui-linkbutton" iconCls="icon-search" plain="true"  onclick="searchProductClass();return false;">����</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-save" plain="true"  onclick="productClassExportToExcel();return false;">����</a>  
        </div>  
        <div id="dlg"  style="width:400px;height:280px;padding:10px 20px">  
            <div class="ftitle">�������</div>  
            <form id="fm" method="post">  
                <div class="fitem">  
                    <label>��&nbsp;Ʒ&nbsp;�飺</label>  
                    <input id="cc" name="cnvcProductType" />
                </div>  
                <div class="fitem">  
                    <label>�����룺</label>  
                    <input id="cnvcProductClassCode" name="cnvcProductClassCode" class="easyui-validatebox" validtype="ProductClassCode" required="true"/>  
                </div>  
                <div class="fitem">  
                    <label>������ƣ�</label>  
                    <input name="cnvcProductClassName" class="easyui-validatebox" required="true"/>  
                </div>  
                <div class="fitem">  
                    <label>����������</label>  
                    <input name="cnnDays" class="easyui-validatebox" validtype="NonnegativeInteger" required="true"/>  
                </div>  
                <div class="fitem">  
                    <label>��&nbsp;&nbsp;&nbsp;&nbsp;����</label>  
                    <input name="cnvcComments"/>  
                </div>  
            </form>  
        </div>  
        <div id="dlg-buttons">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="saveProductClass();return false;">����</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');return false;">ȡ��</a>  
        </div>  

        <div id="search-dlg"  style="width:400px;height:280px;padding:10px 20px">  
            <div class="ftitle">�������</div>  
            <form id="search-fm" method="post">  
                <div class="fitem">  
                    <label>��&nbsp;Ʒ&nbsp;�飺</label>  
                    <input id="search-cc" name="cnvcProductType" />
                </div>  
                <div class="fitem">  
                    <label>�����룺</label>  
                    <input id="search-code" name="cnvcProductClassCode" class="easyui-validatebox" />  
                </div>  
                <div class="fitem">  
                    <label>������ƣ�</label>  
                    <input id="search-name" name="cnvcProductClassName" class="easyui-validatebox"/>  
                </div>  
                <div class="fitem">  
                    <label>����������</label>  
                    <input id="search-days" name="cnnDays" class="easyui-validatebox" validtype="NonnegativeInteger" />  
                </div>  
                <div class="fitem">  
                    <label>��&nbsp;&nbsp;&nbsp;&nbsp;����</label>  
                    <input id="search-comments" name="cnvcComments"/>  
                </div>  
            </form>  
        </div>  
        <div id="search-dlg-buttons">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="highSearchProductClass();return false;">��ѯ</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">ȡ��</a>  
        </div>  
        <form id="export-excel" method="post"></form>
</asp:Content>
