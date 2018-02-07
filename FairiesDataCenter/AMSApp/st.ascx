<%@ Control Language="c#" AutoEventWireup="false" Codebehind="st.ascx.cs" Inherits="AMSApp.st" %>
<style type="text/css">
.pnlSuggest { BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; Z-INDEX: 9527; OVERFLOW: hidden; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; POSITION: absolute; TEXT-OVERFLOW: clip; BACKGROUND-COLOR: #ffffff }
.pnlSuggest TABLE { WIDTH: 100% }
.pnlSuggest TR { WIDTH: 100% }
.trmouseover { WIDTH: 100%; BACKGROUND-COLOR: #eeeeee }
.trmouseover TD { OVERFLOW: hidden; WIDTH: 100%; TEXT-OVERFLOW: clip; BACKGROUND-COLOR: #eeeeee; TEXT-ALIGN: left }
.trmouseout { WIDTH: 100%; BACKGROUND-COLOR: #ffffff }
.trmouseout TD { OVERFLOW: hidden; WIDTH: 100%; TEXT-OVERFLOW: clip; BACKGROUND-COLOR: #ffffff; TEXT-ALIGN: left }
.ddlDataSource { DISPLAY: none }
</style>
<script language="javascript">
//为string对象添加一个清除前后空格的属性
String.prototype.trim = function()
{
	return this.replace(new RegExp("(^[\s]*)|([\s]*$)", "g"), "");
}
//显示下拉信息
function ShowSuggest(objInputText)
{
	objInputText.onkeyup = ControlSuggest(objInputText);
	objInputText.onblur = RemoveSuggest(objInputText);
	var oldValue = objInputText.parentElement.getElementsByTagName("h6")[0];
}
//对提示框的控制
function ControlSuggest()
{
	var ie = (document.all)?true:false;
	if(ie)
	{
		var keycode = event.keyCode;
		if(keycode == 40)
		{//向下
			ChangeSelection(false);
			return ;
		}
		if(CheckSuggest())
		{
			if(keycode == 38)
			{//向上
				ChangeSelection(true);
				return ;
			}
			if(keycode == 13)
			{//回车
				RemoveSuggest();
				return ;
			}
			if(keycode == 46)
			{//del
				DeleteSuggest();
				objInputText.value = oldValue.innerText;
				oldValue.innerText = "";
				return ;
			}
			if((keycode >= 16 && keycode <= 36) || (keycode >= 41 && keycode <= 47))
			{
				return;
			}
		}
		CreateSuggest();
	}
}
//删除提示前对文本做相关操作
function RemoveSuggest()
{
	if(CheckSuggest())
	{
		var panelSuggest = document.getElementById("divSuggestPanel");
		var inputIndex = document.getElementById("inputIndex");
		if( CheckActiveElement(panelSuggest) || event.keyCode == 13)
		{
			var selectIndex = Number(inputIndex.value);
			if(selectIndex >= 0)
			{
				var tb = panelSuggest.getElementsByTagName("table")[0];
				objInputText.value = tb.rows[selectIndex].cells[0].innerText;
			}
		}
		else
		{
			objInputText.value = oldValue.innerText;
		}
		document.body.removeChild(inputIndex);
		document.body.removeChild(panelSuggest);
		oldValue.innerText = "";
	}
	else
	{
		return ;
	}
}
//删除提示的方法，不对文本做任何操作
function DeleteSuggest()
{
	if(CheckSuggest())
	{
		var panelSuggest = document.getElementById("divSuggestPanel");
		var inputIndex = document.getElementById("inputIndex");
		document.body.removeChild(inputIndex);
		document.body.removeChild(panelSuggest);
	}
}
//加载提示框
function CreateSuggest()
{
	//提示框存在，而且文本框值与上次的输入不同时，才进行下面的加载工作
	if(CheckSuggest())
	{
		if( oldValue.innerText.trim() == objInputText.value.trim())
		{
			return ;
		}
		else
		{
			DeleteSuggest();
		}
	}
	if(CheckSuggest() && objInputText.value.trim().length ==0)
	{//提示框存在,但是文本框没有内容,这时删除提示框
		DeleteSuggest();
		oldValue.innerText = "";
		return ;
	}
	//如果输入为空格，就退出
    if(objInputText.value.trim().length == 0)
	{
		return ;
	}
	//从数据源中取数据
    var suggestList = GetSuggestList();
	if(suggestList == null||suggestList.length < 1)
	{//对传入的数组进行判断，为空或者列表为0就退出
		DeleteSuggest();                                  //开始的输入有提示，后面的输入可能没有提示，所以数据源为空时要尝试删除提示
		oldValue.innerText = "";
		return ;
	}
	oldValue.innerText = objInputText.value;              //以上条件都符合，根据数据源来创建数据

	var inputIndex = document.createElement("input");     //用隐藏控件来做索引的保存
	inputIndex.type = "hidden";
	inputIndex.id = "inputIndex";
	inputIndex.value = -1;
	var suggest = "";                                     //根据数据源来写div提示信息
	suggest += "<table>";
	for(var nIndex = 0; nIndex < suggestList.length; nIndex++)
	{
		suggest += "<tr onmouseover=this.className='trmouseover'";
		var inputIndex = document.getElementById('inputIndex');
		inputIndex.value = this.rowIndex; 
		suggest +=" onmouseout=this.className='trmouseout'";
		suggest +="  ><td>";
		suggest += suggestList[nIndex];
		suggest += "</td></tr>";
	}
	suggest += "</table>";
	var panelSuggest = document.createElement("div");                //创建装提示框的容器div

	panelSuggest.id = "divSuggestPanel";
	panelSuggest.className = "pnlSuggest";                           //设置对象的类
	panelSuggest.style.width = objInputText.clientWidth + "px";      //设置对象的宽度，与文本框宽度相同
	panelSuggest.style.top = (GetPosition()[0] + objInputText.offsetHeight + 1) + "px";
	panelSuggest.style.left = GetPosition()[1] + "px";
	panelSuggest.innerHTML = suggest;
	document.body.appendChild(panelSuggest);                         //把提示框和索引控件添加进来        
	document.body.appendChild(inputIndex);
}
//更换选项
function ChangeSelection(isup)
{
	//按向下创建提示
    if(!CheckSuggest() && objInputText.value.trim().length !=0 && !isup)
	{//文本框有内容,提示不存在,向下
		CreateSuggest();
		return;
	}
	if(CheckSuggest())
	{
		var inputIndex = document.getElementById("inputIndex");                 //得到索引的值
		var selectIndex = Number(inputIndex.value);
		var panelSuggest = document.getElementById("divSuggestPanel");          //得到提示框
		var tb = panelSuggest.getElementsByTagName("table")[0];
		var maxIndex = tb.rows.length - 1;                                      //提示信息的最大索引

		if(isup)
		{//向上
			if(selectIndex >= 0)                                                //索引不能为负
            {
				tb.rows[selectIndex].className = "trmouseout";
				selectIndex--;
				if(selectIndex >= 0)
				{
					tb.rows[selectIndex].className = "trmouseover";
				}
			}
		}
		else
		{
			if(selectIndex < maxIndex)                                          //大于等于最大索引就不做任何操作
            {
				if(selectIndex >= 0)
				{
					tb.rows[selectIndex].className = "trmouseout";
				}
				selectIndex++;
				tb.rows[selectIndex].className = "trmouseover";
			}
		}
		inputIndex.value = selectIndex;
		if(selectIndex >= 0)
		{
			objInputText.value = tb.rows[selectIndex].cells[0].innerText;
		}
		else
		{
			objInputText.value = oldValue.innerText;
		}
	}
}
//判断活动对象是否为obj对象的从属对象
function CheckActiveElement(obj)
{
	var isAe = false;
	var objtemp = document.activeElement;
	while(objtemp != null)
	{
		if(objtemp == obj)
		{
			isAe = true;
			break;
		}
		objtemp = objtemp.parentElement;
	}
	return isAe;
}
//检查提示框是否存在
function CheckSuggest()
{
	var panelSuggest = document.getElementById("divSuggestPanel");
	if(panelSuggest == null)
	{
		return false;
	}
	else
	{
		return true;
	}
}
//获取文本框的位置
function GetPosition()
{
	var top = 0,left = 0;
	var obj = objInputText;
	do
	{
		top += obj.offsetTop;         //距离顶部
        left += obj.offsetLeft;       //距离左边
    }
	while(obj = obj.offsetParent);
	var arr = new Array();
	arr[0] = top;
	arr[1] = left;
	return arr;
}
//得到提示数据
function GetSuggestList()
{
	var intIndex=0;suggestList = new Array();
	var dataoptions = objInputText.parentElement.getElementsByTagName("option");
	var inputtxt = objInputText.value;
	for(var n = 0; n < dataoptions.length; n++)
	{
		if(dataoptions[n].text.indexOf(inputtxt) > -1)
		{
			suggestList[intIndex++] = dataoptions[n].text;
		}
	}
	return suggestList;
}
</script>
<div>
	<input type="text" id="txtInput" name="txtInput" onkeydown="ShowSuggest(this);">
	<h6 style="DISPLAY: none"></h6>
	<asp:DropDownList ID="ddlDataSource" runat="server" CssClass="ddlDataSource"></asp:DropDownList>
</div>
