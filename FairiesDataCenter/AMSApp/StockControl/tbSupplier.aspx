<%@ Page Language="C#" Title="供应商" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" AutoEventWireup="true" CodeBehind="tbSupplier.aspx.cs" Inherits="AMSApp.StockControl.tbSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="tbSupplier.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="dg">
        <thead>
            <tr>
                <th field="cnvcCode" width="100">供应商编码
                </th>
                <th field="cnvcName" width="100">供应商名称
                </th>
                <th field="cnvcAddress" width="100">地址
                </th>
                <th field="cnvcPostCode" width="100">邮编
                </th>
                <th field="cnvcPhone" width="100">电话
                </th>
                <th field="cnvcFax" width="100">传真
                </th>
                <th field="cnvcEmail" width="100">电子邮件
                </th>
                <th field="cnvcLinkName" width="100">联系人
                </th>
                <th field="cnvcCreateOper" width="100">创建人
                </th>
                <th field="cndCreateDate" width="100">创建时间
                </th>
                <th field="cnbInvalid" width="100" formatter="formatCheckBox">是否失效
                </th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newtbSupplier();return false;">
            添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="edittbSupplier();return false;">
                修改</a> <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="removetbSupplier();return false;">
                    删除</a> <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchtbSupplier();return false;">
                        搜索</a> <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="true" onclick="tbSupplierExportToExcel();return false;">
                            导出</a>
    </div>
    <div id="dlg" style="width: 500px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            供应商</div>
        <form id="fm" method="post">
        <div class="fitem">
            <label>供应商编码
            </label>
            <input id="cnvcCode" name="cnvcCode" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>供应商名称
            </label>
            <input id="cnvcName" name="cnvcName" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>地址
            </label>
            <input id="cnvcAddress" name="cnvcAddress" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>邮编
            </label>
            <input id="cnvcPostCode" name="cnvcPostCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>电话
            </label>
            <input id="cnvcPhone" name="cnvcPhone" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>传真
            </label>
            <input id="cnvcFax" name="cnvcFax" class="easyui-validatebox"   />
        </div>
        <div class="fitem">
            <label>电子邮件
            </label>
            <input id="cnvcEmail" name="cnvcEmail" class="easyui-validatebox"  />
        </div>
        <div class="fitem">
            <label>联系人
            </label>
            <input id="cnvcLinkName" name="cnvcLinkName" class="easyui-validatebox"   />
        </div>
        <%--<div class="fitem">
            <label>
            </label>
            <input id="cnvcCreateOper" name="cnvcCreateOper" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
            </label>
            <input id="cndCreateDate" name="cndCreateDate" class="easyui-validatebox" required="true" />
        </div>--%>
        <div class="fitem">
            <label>是否失效
            </label>
            <input id="cnbInvalid" name="cnbInvalid" class="easyui-validatebox" type="checkbox"/>
        </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="savetbSupplier();return false;">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');return false;">
            取消</a>
    </div>
    <div id="search-dlg" style="width: 500px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            供应商</div>
        <form id="search-fm" method="post">
        <div class="fitem">
            <label>供应商编码
            </label>
            <input id="search-cnvcCode" name="cnvcCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>供应商名称
            </label>
            <input id="search-cnvcName" name="cnvcName" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>地址
            </label>
            <input id="search-cnvcAddress" name="cnvcAddress" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>邮编
            </label>
            <input id="search-cnvcPostCode" name="cnvcPostCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>电话
            </label>
            <input id="search-cnvcPhone" name="cnvcPhone" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>传真
            </label>
            <input id="search-cnvcFax" name="cnvcFax" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>电子邮件
            </label>
            <input id="search-cnvcEmail" name="cnvcEmail" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>联系人
            </label>
            <input id="search-cnvcLinkName" name="cnvcLinkName" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>创建人
            </label>
            <input id="search-cnvcCreateOper" name="cnvcCreateOper" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>创建时间
            </label>
            <input id="search-cndCreateDate" name="cndCreateDate" class="easyui-validatebox" />
        </div>
        <div>
           <label>
            </label>
            <input id="search-cndCreateDate1" name="cndCreateDate1" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>是否失效
            </label>
            <input id="search-cnbInvalid" name="cnbInvalid" type="checkbox" class="easyui-validatebox"/>
        </div>
        </form>
    </div>
    <div id="search-dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="highSearchtbSupplier();return false;">
            查询</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">
                取消</a>
    </div>
    <form id="export-excel" method="post">
    </form>
</asp:Content>
