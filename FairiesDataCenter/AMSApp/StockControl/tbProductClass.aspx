<%@ Page language="c#" Title="存货分类" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" Codebehind="tbProductClass.aspx.cs" AutoEventWireup="true" Inherits="AMSApp.StockControl.tbProductClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="tbProductClass.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <table id="dg">
            <thead>  
                <tr>  
                    <th field="cnvcProductType" width="100" hidden="true">产品组编码</th>  
                    <th field="cnvcProductTypeComments" width="100">产品组</th>  
                    <th field="cnvcProductClassCode" width="100">类别编码</th>  
                    <th field="cnvcProductClassName" width="100">类别名称</th>  
                    <th field="cnnDays" width="50">过期天数</th> 
                    <th field="cnvcComments" width="300">描述</th> 
                </tr>  
            </thead> 
        </table> 
        <div id="toolbar">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true"  onclick="newProductClass();return false;">添加</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true"  onclick="editProductClass();return false;">修改</a> 
            <a href="#" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="removeProductClass();return false;">删除</a>              
            <a href="#" class="easyui-linkbutton" iconCls="icon-search" plain="true"  onclick="searchProductClass();return false;">搜索</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-save" plain="true"  onclick="productClassExportToExcel();return false;">导出</a>  
        </div>  
        <div id="dlg"  style="width:400px;height:280px;padding:10px 20px">  
            <div class="ftitle">存货分类</div>  
            <form id="fm" method="post">  
                <div class="fitem">  
                    <label>产&nbsp;品&nbsp;组：</label>  
                    <input id="cc" name="cnvcProductType" />
                </div>  
                <div class="fitem">  
                    <label>类别编码：</label>  
                    <input id="cnvcProductClassCode" name="cnvcProductClassCode" class="easyui-validatebox" validtype="ProductClassCode" required="true"/>  
                </div>  
                <div class="fitem">  
                    <label>类别名称：</label>  
                    <input name="cnvcProductClassName" class="easyui-validatebox" required="true"/>  
                </div>  
                <div class="fitem">  
                    <label>过期天数：</label>  
                    <input name="cnnDays" class="easyui-validatebox" validtype="NonnegativeInteger" required="true"/>  
                </div>  
                <div class="fitem">  
                    <label>描&nbsp;&nbsp;&nbsp;&nbsp;述：</label>  
                    <input name="cnvcComments"/>  
                </div>  
            </form>  
        </div>  
        <div id="dlg-buttons">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="saveProductClass();return false;">保存</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');return false;">取消</a>  
        </div>  

        <div id="search-dlg"  style="width:400px;height:280px;padding:10px 20px">  
            <div class="ftitle">存货分类</div>  
            <form id="search-fm" method="post">  
                <div class="fitem">  
                    <label>产&nbsp;品&nbsp;组：</label>  
                    <input id="search-cc" name="cnvcProductType" />
                </div>  
                <div class="fitem">  
                    <label>类别编码：</label>  
                    <input id="search-code" name="cnvcProductClassCode" class="easyui-validatebox" />  
                </div>  
                <div class="fitem">  
                    <label>类别名称：</label>  
                    <input id="search-name" name="cnvcProductClassName" class="easyui-validatebox"/>  
                </div>  
                <div class="fitem">  
                    <label>过期天数：</label>  
                    <input id="search-days" name="cnnDays" class="easyui-validatebox" validtype="NonnegativeInteger" />  
                </div>  
                <div class="fitem">  
                    <label>描&nbsp;&nbsp;&nbsp;&nbsp;述：</label>  
                    <input id="search-comments" name="cnvcComments"/>  
                </div>  
            </form>  
        </div>  
        <div id="search-dlg-buttons">  
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="highSearchProductClass();return false;">查询</a>  
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">取消</a>  
        </div>  
        <form id="export-excel" method="post"></form>
</asp:Content>
