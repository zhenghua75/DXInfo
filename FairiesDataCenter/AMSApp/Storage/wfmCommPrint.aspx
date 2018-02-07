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
		<!--这里调用控件ScriptX.cab-->
		<object id="factory"  style="display:none"  classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814"  codebase="../ScriptX.cab#Version=6,3,435,20" VIEWASTEXT></object>

		<script type="text/javascript" language="javascript" >
		//用于设置打印参数
		function printBase() {	
			// -------------------基本功能，可免费使用-----------------------
			factory.printing.header = "";//页眉
			factory.printing.footer = "";//页脚
			//边距设置，需要注意大部分打印机都不能进行零边距打印，即有一个边距的最小值，一般都是6毫米以上
			//设置边距的时候如果设置为零，就会自动调整为它的最小边距
			factory.printing.leftMargin = 7;//左边距
			factory.printing.topMargin = 7;//上边距
			factory.printing.rightMargin = 7;//右边距
			factory.printing.bottomMargin = 7;//下边距
			factory.printing.portrait = true;//是否纵向打印，横向打印为false
			//--------------------高级功能---------------------------------------------
			//factory.printing.printer = "EPSON LQ-1600KIII";//指定使用的打印机
			//factory.printing.printer = "\\\\cosa-data\\HPLaserJ";//如为网络打印机，则需要进行字符转义
			//factory.printing.paperSize = "A4";//指定使用的纸张
			//factory.printing.paperSource = "Manual feed";//进纸方式，这里是手动进纸
			//factory.printing.copies = 2;//打印份数
			//factory.printing.printBackground = false;//是否打印背景图片
			//factory.printing.SetPageRange(false, 1, 3); //打印1至3页	
			//factory.printing.SetMarginMeasure(1);//页边距单位，1为毫米，2为英寸
		}
		</script>
	
  </body>
  
	<script type="text/javascript" language="javascript"> 
	//用于调用设置打印参数的方法和显示预览界面
	function printReport(){
		printBase();
		//printBase("&w&b页码，&p/&P","&u&b&d");//默认
		//window.print();
		//factory.printing.Preview(); //预览
		factory.printing.Print(false);  //false为默认打印机,true为选择打印机
		//factory.printing.PrintSetup();//打印设置
		//factory.printing.WaitForSpoolingComplete();//等待上一个打印任务完全送入打印池，在连续无确认打印时非常有用
		//factory.printing.EnumPrinters(index);//枚举已安装的所有打印机，主要用于生成打印机选择功能
	} 
	</script>    
</html>
