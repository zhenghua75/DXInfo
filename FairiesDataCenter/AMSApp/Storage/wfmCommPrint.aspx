<%@ Page language="c#" Codebehind="wfmCommPrint.aspx.cs" AutoEventWireup="false" Inherits="AMSApp.Storage.wfmCommPrint" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>wfmCommPrint</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <style type="text/css">
		@media print {
		.noprint {display:none}
		}
	</style>    
  </head>
  <body MS_POSITIONING="GridLayout" bgcolor="#feeff8" topmargin="50">
		<!--������ÿؼ�ScriptX.cab-->
		<object id="factory"  style="display:none"  classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814"  codebase="../ScriptX.cab#Version=6,3,435,20" VIEWASTEXT></object>

		<script type="text/javascript" language="javascript" >
		//�������ô�ӡ����
		function printBase() {	
			// -------------------�������ܣ������ʹ��-----------------------
			factory.printing.header = "";//ҳü
			factory.printing.footer = "";//ҳ��
			//�߾����ã���Ҫע��󲿷ִ�ӡ�������ܽ�����߾��ӡ������һ���߾����Сֵ��һ�㶼��6��������
			//���ñ߾��ʱ���������Ϊ�㣬�ͻ��Զ�����Ϊ������С�߾�
			factory.printing.leftMargin = 7;//��߾�
			factory.printing.topMargin = 7;//�ϱ߾�
			factory.printing.rightMargin = 7;//�ұ߾�
			factory.printing.bottomMargin = 7;//�±߾�
			factory.printing.portrait = true;//�Ƿ������ӡ�������ӡΪfalse
			//--------------------�߼�����---------------------------------------------
			//factory.printing.printer = "EPSON LQ-1600KIII";//ָ��ʹ�õĴ�ӡ��
			//factory.printing.printer = "\\\\cosa-data\\HPLaserJ";//��Ϊ�����ӡ��������Ҫ�����ַ�ת��
			//factory.printing.paperSize = "A4";//ָ��ʹ�õ�ֽ��
			//factory.printing.paperSource = "Manual feed";//��ֽ��ʽ���������ֶ���ֽ
			//factory.printing.copies = 2;//��ӡ����
			//factory.printing.printBackground = false;//�Ƿ��ӡ����ͼƬ
			//factory.printing.SetPageRange(false, 1, 3); //��ӡ1��3ҳ	
			//factory.printing.SetMarginMeasure(1);//ҳ�߾൥λ��1Ϊ���ף�2ΪӢ��
		}
		</script>
	
  </body>
  
	<script type="text/javascript" language="javascript"> 
	//���ڵ������ô�ӡ�����ķ�������ʾԤ������
	function printReport(){
		printBase();
		//printBase("&w&bҳ�룬&p/&P","&u&b&d");//Ĭ��
		//window.print();
		//factory.printing.Preview(); //Ԥ��
		factory.printing.Print(false);  //falseΪĬ�ϴ�ӡ��,trueΪѡ���ӡ��
		//factory.printing.PrintSetup();//��ӡ����
		//factory.printing.WaitForSpoolingComplete();//�ȴ���һ����ӡ������ȫ�����ӡ�أ���������ȷ�ϴ�ӡʱ�ǳ�����
		//factory.printing.EnumPrinters(index);//ö���Ѱ�װ�����д�ӡ������Ҫ�������ɴ�ӡ��ѡ����
	} 
	</script>    
</html>
