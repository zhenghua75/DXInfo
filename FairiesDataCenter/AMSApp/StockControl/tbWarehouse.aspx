<%@ Page Language="C#" Title="仓库" MasterPageFile="~/AMSApp/StockControl/StockControl.Master" AutoEventWireup="true" CodeBehind="tbWarehouse.aspx.cs" Inherits="AMSApp.StockControl.tbWarehouse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="tbWarehouse.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="dg">
        <thead>
            <tr>
                <th field="cnvcWhCode" width="100">仓库编码
                </th>
                <th field="cnvcWhName" width="100">仓库名称
                </th>
                <th field="cnvcDepID" width="100" hidden="true">部门编码
                </th>
                <th field="cnvcDepIDComments" width="100">部门名称
                </th>
                <th field="cnvcWhAddress" width="100">地址
                </th>
                <th field="cnvcWhPhone" width="100">电话
                </th>
                <th field="cnvcWhPerson" width="100" hidden="true">负责人编码
                </th>
                <th field="cnvcWhPersonComments" width="100">负责人姓名
                </th>
                <th field="cnvcWhComments" width="100">描述
                </th>
                <th field="cnbInvalid" width="100" formatter="formatCheckBox">是否失效
                </th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newtbWarehouse();return false;">
            添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="edittbWarehouse();return false;">
                修改</a> <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="removetbWarehouse();return false;">
                    删除</a> <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchtbWarehouse();return false;">
                        搜索</a> <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="true" onclick="tbWarehouseExportToExcel();return false;">
                            导出</a>
    </div>
    <div id="dlg" style="width: 500px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            仓库</div>
        <form id="fm" method="post">
        <div class="fitem">
            <label>仓库编码
            </label>
            <input id="cnvcWhCode" name="cnvcWhCode" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>仓库名称
            </label>
            <input id="cnvcWhName" name="cnvcWhName" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>部门
            </label>
            <input id="cnvcDepID" name="cnvcDepID" class="easyui-validatebox"/>
        </div>
        <div class="fitem">
            <label>地址
            </label>
            <input id="cnvcWhAddress" name="cnvcWhAddress" class="easyui-validatebox"  />
        </div>
        <div class="fitem">
            <label>电话
            </label>
            <input id="cnvcWhPhone" name="cnvcWhPhone" class="easyui-validatebox"  />
        </div>
        <div class="fitem">
            <label>负责人
            </label>
            <input id="cnvcWhPerson" name="cnvcWhPerson" class="easyui-validatebox"/>
        </div>
        <div class="fitem">
            <label>描述
            </label>
            <input id="cnvcWhComments" name="cnvcWhComments" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>是否失效
            </label>
            <input id="cnbInvalid" name="cnbInvalid" class="easyui-validatebox" type="checkbox" />
        </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="savetbWarehouse();return false;">
            保存</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');return false;">
                取消</a>
    </div>
    <div id="search-dlg" style="width: 400px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            仓库</div>
        <form id="search-fm" method="post">
        <div class="fitem">
            <label>仓库编码
            </label>
            <input id="search-cnvcWhCode" name="cnvcWhCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>仓库名称
            </label>
            <input id="search-cnvcWhName" name="cnvcWhName" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>部门
            </label>
            <input id="search-cnvcDepID" name="cnvcDepID" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>地址
            </label>
            <input id="search-cnvcWhAddress" name="cnvcWhAddress" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>电话
            </label>
            <input id="search-cnvcWhPhone" name="cnvcWhPhone" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>负责人
            </label>
            <input id="search-cnvcWhPerson" name="cnvcWhPerson" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>描述
            </label>
            <input id="search-cnvcWhComments" name="cnvcWhComments" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>是否失效
            </label>
            <input id="search-cnbInvalid" name="cnbInvalid" class="easyui-validatebox" type="checkbox" />
        </div>
        </form>
    </div>
    <div id="search-dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="highSearchtbWarehouse();return false;">
            查询</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">
                取消</a>
    </div>
    <form id="export-excel" method="post">
    </form>
</asp:Content>
