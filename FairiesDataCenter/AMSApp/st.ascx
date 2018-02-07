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
//Ϊstring�������һ�����ǰ��ո������
String.prototype.trim = function()
{
	return this.replace(new RegExp("(^[\s]*)|([\s]*$)", "g"), "");
}
//��ʾ������Ϣ
function ShowSuggest(objInputText)
{
	objInputText.onkeyup = ControlSuggest(objInputText);
	objInputText.onblur = RemoveSuggest(objInputText);
	var oldValue = objInputText.parentElement.getElementsByTagName("h6")[0];
}
//����ʾ��Ŀ���
function ControlSuggest()
{
	var ie = (document.all)?true:false;
	if(ie)
	{
		var keycode = event.keyCode;
		if(keycode == 40)
		{//����
			ChangeSelection(false);
			return ;
		}
		if(CheckSuggest())
		{
			if(keycode == 38)
			{//����
				ChangeSelection(true);
				return ;
			}
			if(keycode == 13)
			{//�س�
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
//ɾ����ʾǰ���ı�����ز���
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
//ɾ����ʾ�ķ����������ı����κβ���
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
//������ʾ��
function CreateSuggest()
{
	//��ʾ����ڣ������ı���ֵ���ϴε����벻ͬʱ���Ž�������ļ��ع���
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
	{//��ʾ�����,�����ı���û������,��ʱɾ����ʾ��
		DeleteSuggest();
		oldValue.innerText = "";
		return ;
	}
	//�������Ϊ�ո񣬾��˳�
    if(objInputText.value.trim().length == 0)
	{
		return ;
	}
	//������Դ��ȡ����
    var suggestList = GetSuggestList();
	if(suggestList == null||suggestList.length < 1)
	{//�Դ������������жϣ�Ϊ�ջ����б�Ϊ0���˳�
		DeleteSuggest();                                  //��ʼ����������ʾ��������������û����ʾ����������ԴΪ��ʱҪ����ɾ����ʾ
		oldValue.innerText = "";
		return ;
	}
	oldValue.innerText = objInputText.value;              //�������������ϣ���������Դ����������

	var inputIndex = document.createElement("input");     //�����ؿؼ����������ı���
	inputIndex.type = "hidden";
	inputIndex.id = "inputIndex";
	inputIndex.value = -1;
	var suggest = "";                                     //��������Դ��дdiv��ʾ��Ϣ
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
	var panelSuggest = document.createElement("div");                //����װ��ʾ�������div

	panelSuggest.id = "divSuggestPanel";
	panelSuggest.className = "pnlSuggest";                           //���ö������
	panelSuggest.style.width = objInputText.clientWidth + "px";      //���ö���Ŀ�ȣ����ı�������ͬ
	panelSuggest.style.top = (GetPosition()[0] + objInputText.offsetHeight + 1) + "px";
	panelSuggest.style.left = GetPosition()[1] + "px";
	panelSuggest.innerHTML = suggest;
	document.body.appendChild(panelSuggest);                         //����ʾ��������ؼ���ӽ���        
	document.body.appendChild(inputIndex);
}
//����ѡ��
function ChangeSelection(isup)
{
	//�����´�����ʾ
    if(!CheckSuggest() && objInputText.value.trim().length !=0 && !isup)
	{//�ı���������,��ʾ������,����
		CreateSuggest();
		return;
	}
	if(CheckSuggest())
	{
		var inputIndex = document.getElementById("inputIndex");                 //�õ�������ֵ
		var selectIndex = Number(inputIndex.value);
		var panelSuggest = document.getElementById("divSuggestPanel");          //�õ���ʾ��
		var tb = panelSuggest.getElementsByTagName("table")[0];
		var maxIndex = tb.rows.length - 1;                                      //��ʾ��Ϣ���������

		if(isup)
		{//����
			if(selectIndex >= 0)                                                //��������Ϊ��
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
			if(selectIndex < maxIndex)                                          //���ڵ�����������Ͳ����κβ���
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
//�жϻ�����Ƿ�Ϊobj����Ĵ�������
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
//�����ʾ���Ƿ����
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
//��ȡ�ı����λ��
function GetPosition()
{
	var top = 0,left = 0;
	var obj = objInputText;
	do
	{
		top += obj.offsetTop;         //���붥��
        left += obj.offsetLeft;       //�������
    }
	while(obj = obj.offsetParent);
	var arr = new Array();
	arr[0] = top;
	arr[1] = left;
	return arr;
}
//�õ���ʾ����
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
