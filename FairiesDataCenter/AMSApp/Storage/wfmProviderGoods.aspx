<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page language="c#" Codebehind="wfmProviderGoods.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmProviderGoods" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>wfmProviderGoods</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		//树操作
		
		function SelectNode(node)
		{
			var arr = new Array();
			collectParentNode(node);
			if(arr.length != 0)
			{
				for(var i=arr.length -1;i>=0;i--)
					arr[i].setAttribute("expanded",true);
			}
			tv.selectedNodeIndex = node.getNodeIndex();
			DisplayNode(node);
			function collectParentNode(node)
			{
				var parentNode = node.getParent()
				if(parentNode == tv ||parentNode == null) return;
				arr[arr.length] = parentNode;
				collectParentNode(parentNode);
			}
		}
		
		function SearchNode(nodes,id)
		{
			if(nodes != null)
			{
				for(var i=0;i<nodes.length;i++)
				{
					var node = nodes[i];
					if(node.getAttribute("ID") == id)
						return node;
					node = SearchNode(node.getChildren(),id);
					if(node !=null)
						return node;
				}
			}
			return null;
		}

		
		function AddNode(parentNode,type,id,name)
		{
			var node = tv.createTreeNode();
			node.setAttribute("Type",type);
			node.setAttribute("ID",MakeWebID(type,id));
			node.setAttribute("Text",name);
			switch(type)
			{
			case "oper":
				node.setAttribute("ImageUrl","../../images/tree/img-User.gif");
				break;
			case "dept":
				node.setAttribute("ImageUrl","../../images/tree/img-organizationalUnit.gif");
				break;
			}
			parentNode.add(node);
			return node;
		}
		
		function ChangeNodeName(node,name)
		{
			node.setAttribute("Text",name);
		}
		
		function RemoveNode(node)
		{
			node.remove();
		}
		
		function MoveNode(sonNode,parentNode)
		{
			sonNode.remove();
			parentNode.add(sonNode);
		}
		
		
		function MakeWebID(type,id)
		{
			return type+"_"+id;			
		}
		
		function GetActualID(webId)
		{
			var index = webId.indexOf("_");
			return webId.substring(index+1);
		}
		
		function GetSelectedNode(tree)
		{
			return tree.getTreeNode(tree.selectedNodeIndex);
		}
		
		//事件处理
		
		function OnAddNodeResult(parentId,id,name)
		{
			var parentNode = SearchNode(tv.getChildren(),parentId);
			var ss = id.split("_");
			if(ss ==null ||ss.length<2) return;
			var node = AddNode(parentNode,ss[0],ss[1],name);
			SelectNode(node);
		}
		
		function OnChangeNodeResult(id,name)
		{
			var node = SearchNode(tv.getChildren(),id);
			if(node !=null)
			{
				ChangeNodeName(node,name);
			}
		}
		
		function ChangeOperPassword(operId)
		{
			var strWin = "dialogWidth:450px;dialogHeight:450px;center:1;resizable:no;scroll:0;help:0;status:0";
			var strUrl="ChangePassword.aspx?OperID="+operId;
			window.showModalDialog(strUrl,null,strWin);
		}
		
		function onInit()
		{
			tv.onselectedindexchange = tv_onselectedindexchange;
			DisplayNode(GetSelectedNode(tv));
		}
		
		function HandeSubMenu()
		{
			switch(event.menuId)
			{
				case "ChangePassword":
				{
					var node = GetSelectedNode(tv);
					if( node == null || node.getAttribute("Type")!="oper") return;
					var operId = GetActualID(node.getAttribute("ID"));
					ChangeOperPassword(operId);
					break;
				}
				case "FindUser":
					subframe.location="SearchUser.aspx";
					break;
				case "FindDept":
					subframe.location="SearchDept.aspx";
					break;
				case "AddUser":
				{
					var node = GetSelectedNode(tv);
					if(node == null) return;
					subframe.location="UserProperties.aspx?Method=Add&DeptID="+ node.getAttribute("ID");
					break;
				}
				case "AddDept":
				{
					var node = GetSelectedNode(tv);
					if(node == null) return;
					subframe.location="DeptProperties.aspx?Method=Add&ParentDeptID="+ node.getAttribute("ID");
					break;
				}
				case "MoveDept":
				case "MoveUser":
				{
					var node = GetSelectedNode(tv);
					var strID = node.getAttribute("ID");
					MoveObj(strID);
					break;
				}
			}
		}
		
		function MoveObj(srcID)
		{
			var lsWin = "dialogWidth:250px;dialogHeight:350px;center:1;resizable:no;scroll:0;help:0;status:0";
			var loObj = new Object();
			window.showModalDialog("DeptDialog.aspx?SrcID="+srcID,loObj,lsWin);
			if(loObj.dest!=null)
			{
				var srcNode = SearchNode(tv.getChildren(),loObj.src);
				var destNode = SearchNode(tv.getChildren(),loObj.dest);
				if(srcNode == null || destNode ==null) return;
				MoveNode(srcNode,destNode);
				SelectNode(srcNode);
			}
		}
	
		function SelectNodeByID(id)
		{
			var node = SearchNode(tv.getChildren(),id);
			if(node != null)
			{
				SelectNode(node);
				/*
				var evt = document.createEventObject();
				evt.newTreeNodeIndex = node.getNodeIndex();
				tv.fireEvent("onselectedindexchange",evt);
				*/
			}
		}
		
		function DisplayNode(node)
		{
			if(node == null) return;
			switch(node.getAttribute("Type"))
			{
				case "prov":
					if(node.getAttribute("ID")=="0")
					{
						subframe.location="wfmProvGInfo.aspx";
					}
					else
					{
						subframe.location="wfmProvGInfo.aspx?pid="+node.getAttribute("ID")+"&pname="+node.getAttribute("Text");
					}
					break;
				case "good":
					if(node.getAttribute("ID")=="0")
					{
						subframe.location="wfmGoodProvInfo.aspx";
					}
					else
					{
						subframe.location="wfmGoodProvInfo.aspx?gname="+node.getAttribute("Text");
					}
					break;
			}
			
		}
		
		function tv_onselectedindexchange()
		{
			var node = tv.getTreeNode(event.newTreeNodeIndex);
			DisplayNode(node);
			//SelectNode(node);
			event.returnValue=true;
		}
		
		</script>
	</HEAD>
	<body bgColor="#feeff8" MS_POSITIONING="GridLayout" onload="onInit();">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="5" width="95%">
				<TR>
					<TD style="COLOR: #330033; FONT-SIZE: 15pt; FONT-WEIGHT: bold" align="center">供应商存货对照</TD>
				</TR>
			</TABLE>
			<table border="1" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
							<TR>
								<TD style="WIDTH: 94px">
									<asp:RadioButton style="Z-INDEX: 0" id="rbtnProvider" runat="server" Font-Size="10pt" Text="按供应商"
										Checked="True" GroupName="PGG"></asp:RadioButton></TD>
								<TD style="WIDTH: 127px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Size="10pt" Width="72px">供应商编码</asp:label></TD>
								<TD style="WIDTH: 178px" align="left">
									<asp:textbox style="Z-INDEX: 0" id="txtProviderID" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
								<TD style="WIDTH: 118px" align="right"><FONT face="宋体">
										<asp:label style="Z-INDEX: 0" id="Label2" runat="server" Font-Size="10pt" Width="72px">供应商名称</asp:label></FONT></TD>
								<TD style="WIDTH: 222px" align="left">
									<asp:textbox style="Z-INDEX: 0" id="txtProviderName" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 94px">
									<asp:RadioButton style="Z-INDEX: 0" id="rbtnGoods" runat="server" Font-Size="10pt" Text="按供货品" GroupName="PGG"></asp:RadioButton></TD>
								<TD style="WIDTH: 127px" align="right">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server" Font-Size="10pt" Width="72px">货品编码</asp:label></TD>
								<TD style="WIDTH: 178px" align="left">
									<asp:textbox style="Z-INDEX: 0" id="txtGoodsID" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
								<TD style="WIDTH: 118px" align="right"><FONT face="宋体">
										<asp:label style="Z-INDEX: 0" id="Label4" runat="server" Font-Size="10pt" Width="72px">货品名称</asp:label></FONT></TD>
								<TD style="WIDTH: 222px" align="left">
									<asp:textbox style="Z-INDEX: 0" id="txtGoodsName" runat="server" Font-Size="10pt" Width="112px"></asp:textbox></TD>
								<TD><asp:button id="btnQuery" runat="server" Width="64px" Text="查询"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<table border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td width="150" height="100%"><br>
						<iewc:treeview style="OVERFLOW: auto" id="tv" runat="server" height="600" width="150"></iewc:treeview></td>
					<td width="90%" height="100%"><iframe id="subframe" style="BACKGROUND-COLOR: #cccccc" name="subframe" border="0" frameBorder="0"
							width="100%" height="600"></iframe>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
