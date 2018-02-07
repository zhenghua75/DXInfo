<%@ Page Language="c#" Title="存货档案" MasterPageFile="~/AMSApp/StockControl/StockControl.Master"
    CodeBehind="tbInventory.aspx.cs" AutoEventWireup="true" Inherits="AMSApp.StockControl.tbInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="tbInventory.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="dg">
        <thead>
            <tr>
                <th field="cnvcInvCCode" width="100" hidden="true">
                    存货类别
                </th>
                <th field="cnvcInvCCodeComments" width="100">
                    存货类别
                </th>
                <th field="cnvcInvCode" width="100">
                    存货编码
                </th>
                <th field="cnvcInvName" width="100">
                    存货名称
                </th>
                <th field="cnbProductBill" width="100" formatter="formatCheckBox">
                    允许生产订单
                </th>
                <th field="cnbSale" width="100" formatter="formatCheckBox">
                    是否销售
                </th>
                <th field="cnbPurchase" width="100" formatter="formatCheckBox">
                    是否外购
                </th>
                <th field="cnbSelf" width="100" formatter="formatCheckBox">
                    是否自制
                </th>
                <th field="cnbComsume" width="100" formatter="formatCheckBox">
                    是否生产耗用
                </th>
                <th field="cniInvCCost" width="100">
                    参考成本（元）
                </th>
                <th field="cniInvNCost" width="100">
                    最新成本（元）
                </th>
                <th field="cniSafeNum" width="100">
                    安全库存量
                </th>
                <th field="cniLowSum" width="100">
                    最低库存量
                </th>
                <th field="cnnExpire" width="100">
                    过期限制（天）
                </th>
                <th field="cnnDue" width="100">
                    到期提示（天）
                </th>
                <th field="cndSDate" width="100">
                    启用日期
                </th>
                <th field="cndEDate" width="100">
                    停用日期
                </th>
                <th field="cnvcCreatePerson" width="100" hidden="true">
                    建档人
                </th>
                <th field="cnvcCreatePersonComments" width="100">
                    建档人
                </th>
                <th field="cnvcModifyPerson" width="100" hidden="true">
                    变更人
                </th>
                <th field="cnvcModifyPersonComments" width="100">
                    变更人
                </th>
                <th field="cndModifyDate" width="100">
                    变更日期
                </th>
                <th field="cnvcValueType" width="100" hidden="true">
                    计价方式
                </th>
                <th field="cnvcValueTypeComments" width="100">
                    计价方式
                </th>
                <th field="cnvcInvStd" width="100">
                    规格型号
                </th>
                <th field="cnvcGroupCode" width="100" hidden="true">
                    计量单位组编码
                </th>
                <th field="cnvcGroupCodeComments" width="100">
                    计量单位组
                </th>
                <th field="cnvcComUnitCode" width="100" hidden="true">
                    主计量单位编码
                </th>
                <th field="cnvcComUnitCodeComments" width="100">
                    主计量单位
                </th>
                <th field="cnvcSAComUnitCode" width="100" hidden="true">
                    销售计量单位
                </th>
                <th field="cnvcSAComUnitCodeComments" width="100">
                    销售计量单位
                </th>
                <th field="cnvcPUComUnitCode" width="100" hidden="true">
                    采购计量单位
                </th>
                <th field="cnvcPUComUnitCodeComments" width="100">
                    采购计量单位
                </th>
                <th field="cnvcSTComUnitCode" width="100" hidden="true">
                    库存计量单位
                </th>
                <th field="cnvcSTComUnitCodeComments" width="100">
                    库存计量单位
                </th>
                <th field="cnvcProduceUnitCode" width="100" hidden="true">
                    生产计量单位
                </th>
                <th field="cnvcProduceUnitCodeComments" width="100">
                    生产计量单位
                </th>
                <th field="cnfRetailPrice" width="100">
                    零售价格
                </th>
                <th field="cnvcShopUnitCode" width="100" hidden="true">
                    零售计量单位
                </th>
                <th field="cnvcShopUnitCodeComments" width="100">
                    零售计量单位
                </th>
                <th field="cnvcFeel" width="100">
                    口感
                </th>
                <th field="cnvcOrganise" width="100">
                    组织
                </th>
                <th field="cnvcColor" width="100">
                    内馅
                </th>
                <th field="cnvcTaste" width="100">
                    表面装饰
                </th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newInventory();return false;">
            添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editInventory();return false;">
                修改</a> <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="removeInventory();return false;">
                    删除</a> <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="true" onclick="searchInventory();return false;">
                        搜索</a> <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="true" onclick="inventoryExportToExcel();return false;">
                            导出</a>
    </div>
    <div id="dlg" style="width: 500px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            存货档案</div>
        <form id="fm" method="post">
        <div class="fitem">
            <label>
                存货类别：</label>
            <input id="cc" name="cnvcInvCCode" />
        </div>
        <div class="fitem">
            <label>
                存货编码：</label>
            <input name="cnvcInvCode" class="easyui-validatebox" validtype="InvCode" required="true" />
        </div>
        <div class="fitem">
            <label>
                存货名称：</label>
            <input name="cnvcInvName" class="easyui-validatebox" required="true" />
        </div>
        <div class="fitem">
            <label>
                允许生产订单：</label>
            <input name="cnbProductBill" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                是否销售：</label>
            <input name="cnbSale" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                是否外购：</label>
            <input name="cnbPurchase" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                是否自制：</label>
            <input name="cnbSelf" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                是否生产耗用：</label>
            <input name="cnbComsume" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                参考成本（元）：</label>
            <input name="cniInvCCost" />
        </div>
        <div class="fitem">
            <label>
                最新成本（元）：</label>
            <input name="cniInvNCost" />
        </div>
        <div class="fitem">
            <label>
                安全库存量：</label>
            <input name="cniSafeNum" />
        </div>
        <div class="fitem">
            <label>
                最低库存量：</label>
            <input name="cniLowSum" />
        </div>
        <div class="fitem">
            <label>
                过期限制（天）：</label>
            <input name="cnnExpire" />
        </div>
        <div class="fitem">
            <label>
                到期提示（天）：</label>
            <input name="cnnDue" />
        </div>
        <div class="fitem">
            <label>
                启用日期：</label>
            <input id="dd1" name="cndSDate" />
        </div>
        <div class="fitem">
            <label>
                停用日期：</label>
            <input id="dd2" name="cndEDate" />
        </div>
        <%--<div class="fitem">  
                    <label>建档人：</label>  
                    <input name="cnvcCreatePerson"/>  
                </div>
                <div class="fitem">  
                    <label>变更人：</label>  
                    <input name="cnvcModifyPerson"/>  
                </div>
                 <div class="fitem">  
                    <label>变更日期：</label>  
                    <input name="cndModifyDate"/>  
                </div>--%>
        <div class="fitem">
            <label>
                计价方式：</label>
            <input id="cnvcValueType" name="cnvcValueType" />
        </div>
        <div class="fitem">
            <label>
                规格型号：</label>
            <input name="cnvcInvStd" />
        </div>
        <div class="fitem">
            <label>
                计量单位组：</label>
            <input id="cnvcGroupCode" name="cnvcGroupCode" />
        </div>
        <div class="fitem">
            <label>
                主计量单位：</label>
            <input id="cnvcComUnitCode" name="cnvcComUnitCode" />
        </div>
        <div class="fitem">
            <label>
                销售计量单位：</label>
            <input id="cnvcSAComUnitCode" name="cnvcSAComUnitCode" />
        </div>
        <div class="fitem">
            <label>
                采购计量单位：</label>
            <input id="cnvcPUComUnitCode" name="cnvcPUComUnitCode" />
        </div>
        <div class="fitem">
            <label>
                库存计量单位：</label>
            <input id="cnvcSTComUnitCode" name="cnvcSTComUnitCode" />
        </div>
        <div class="fitem">
            <label>
                生产计量单位：</label>
            <input id="cnvcProduceUnitCode" name="cnvcProduceUnitCode" />
        </div>
        <div class="fitem">
            <label>
                零售价格：</label>
            <input name="cnfRetailPrice" class="easyui-validatebox" validtype="Digit" required="true"/>
        </div>
        <div class="fitem">
            <label>
                零售计量单位：</label>
            <input id="cnvcShopUnitCode" name="cnvcShopUnitCode" />
        </div>
        <div class="fitem">
            <label>
                口感：</label>
            <input name="cnvcFeel" />
        </div>
        <div class="fitem">
            <label>
                组织：</label>
            <input name="cnvcOrganise" />
        </div>
        <div class="fitem">
            <label>
                内馅：</label>
            <input name="cnvcColor" />
        </div>
        <div class="fitem">
            <label>
                表面装饰：</label>
            <input name="cnvcTaste" />
        </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveInventory();return false;">
            保存</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');return false;">
                取消</a>
    </div>
    <div id="search-dlg" style="width: 500px; height: 280px; padding: 10px 20px">
        <div class="ftitle">
            存货分类</div>
        <form id="search-fm" method="post">
        <div class="fitem">
            <label>
                存货类别：</label>
            <input id="search-cc" name="cnvcInvCCode" />
        </div>
        <div class="fitem">
            <label>
                存货编码：</label>
            <input id="search-cnvcInvCode" name="cnvcInvCode" class="easyui-validatebox" />
        </div>
        <div class="fitem">
            <label>
                存货名称：</label>
            <input id="search-cnvcInvName" name="cnvcInvName" class="easyui-validatebox"/>
        </div>
        <div class="fitem">
            <label>
                允许生产订单：</label>
            <input id="search-cnbProductBill" name="cnbProductBill" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                是否销售</label>
            <input id="search-cnbSale" name="cnbSale" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                是否外购</label>
            <input id="search-cnbPurchase" name="cnbPurchase" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                是否自制</label>
            <input id="search-cnbSelf" name="cnbSelf" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                是否生产耗用</label>
            <input id="search-cnbComsume" name="cnbComsume" type="checkbox" />
        </div>
        <div class="fitem">
            <label>
                参考成本（元）</label>
            <input id="search-cniInvCCost" name="cniInvCCost" />
        </div>
        <div class="fitem">
            <label>
                最新成本（元）</label>
            <input id="search-cniInvNCost" name="cniInvNCost" />
        </div>
        <div class="fitem">
            <label>
                安全库存量</label>
            <input id="search-cniSafeNum" name="cniSafeNum" />
        </div>
        <div class="fitem">
            <label>
                最低库存量</label>
            <input id="search-cniLowSum" name="cniLowSum" />
        </div>
        <div class="fitem">
            <label>
                过期限制（天）</label>
            <input id="search-cnnExpire" name="cnnExpire" />
        </div>
        <div class="fitem">
            <label>
                >到期提示（天）</label>
            <input id="search-cnnDue" name="cnnDue" />
        </div>
        <div class="fitem">
            <label>
                启用日期</label>
            <input id="search-dd1" name="cndSDate" />
            
        </div>
        <div class="fitem">
            <label>
                </label>
            <input id="search-cndSDate1" name="cndSDate1" />
        </div>
        <div class="fitem">
            <label>
                停用日期</label>
            <input id="search-dd2" name="cndEDate" />
            
        </div>
        <div class="fitem">
            <label>
                </label>
            <input id="search-cndEDate1" name="cndEDate1" />
        </div>
        <div class="fitem">
            <label>
                建档人</label>
            <input id="search-cnvcCreatePerson" name="cnvcCreatePerson" />
        </div>
        <div class="fitem">
            <label>
                变更人</label>
            <input id="search-cnvcModifyPerson" name="cnvcModifyPerson" />
        </div>
        <div class="fitem">
            <label>
                变更日期</label>
            <input id="search-dd3" name="cndModifyDate" />
            
        </div>
        <div class="fitem">
            <label>
                </label>
            <input id="search-cndModifyDate1" name="cndModifyDate1" />
        </div>
        <div class="fitem">
            <label>
                计价方式</label>
            <input id="search-cnvcValueType" name="cnvcValueType" />
        </div>
        <div class="fitem">
            <label>
                规格型号</label>
            <input id="search-cnvcInvStd" name="cnvcInvStd" />
        </div>
        <div class="fitem">
            <label>
                计量单位组</label>
            <input id="search-cnvcGroupCode" name="cnvcGroupCode" />
        </div>
        <div class="fitem">
            <label>
                主计量单位</label>
            <input id="search-cnvcComUnitCode" name="cnvcComUnitCode" />
        </div>
        <div class="fitem">
            <label>
                销售计量单位</label>
            <input id="search-cnvcSAComUnitCode" name="cnvcSAComUnitCode" />
        </div>
        <div class="fitem">
            <label>
                采购计量单位</label>
            <input id="search-cnvcPUComUnitCode" name="cnvcPUComUnitCode" />
        </div>
        <div class="fitem">
            <label>
                库存计量单位</label>
            <input id="search-cnvcSTComUnitCode" name="cnvcSTComUnitCode" />
        </div>
        <div class="fitem">
            <label>
                生产计量单位</label>
            <input id="search-cnvcProduceUnitCode" name="cnvcProduceUnitCode" />
        </div>
        <div class="fitem">
            <label>
                零售价格</label>
            <input id="search-cnfRetailPrice" name="cnfRetailPrice" />
        </div>
        <div class="fitem">
            <label>
                零售计量单位</label>
            <input id="search-cnvcShopUnitCode" name="cnvcShopUnitCode" />
        </div>
        <div class="fitem">
            <label>
                口感</label>
            <input id="search-cnvcFeel" name="cnvcFeel" />
        </div>
        <div class="fitem">
            <label>
                组织</label>
            <input id="search-cnvcOrganise" name="cnvcOrganise" />
        </div>
        <div class="fitem">
            <label>
                内馅</label>
            <input id="search-cnvcColor" name="cnvcColor" />
        </div>
        <div class="fitem">
            <label>
                表面装饰</label>
            <input id="search-cnvcTaste" name="cnvcTaste" />
        </div>
        </form>
    </div>
    <div id="search-dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="highSearchInventory();return false;">
            查询</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#search-dlg').dialog('close');return false;">
                取消</a>
    </div>
    <form id="export-excel" method="post">
    </form>
</asp:Content>
