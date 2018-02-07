// DXInfo.Encrypt.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
//#include "EncryptString.h"
#include "CardIniFile.h"
using namespace std; 
#pragma comment( lib, "DXInfo.CPPHelpers.lib" )
int inputTxt(void)
{
	//CEncryptString encrypt= CEncryptString();

	CardIniFile iniFile = CardIniFile(false);

	cout<<"akey:";
	cin>>iniFile.akey;
	cout<<",输入的akey是："+iniFile.akey<<endl;

	cout<<"bkey:";
	cin>>iniFile.bkey;
	cout<<",输入的bkey是："+iniFile.bkey<<endl;

	cout<<"appName:";
	cin>>iniFile.appName;
	cout<<",输入的appName是："+iniFile.appName<<endl;

	cout<<"password:";
	cin>>iniFile.password;
	cout<<",输入的password是："+iniFile.password<<endl;

	iniFile.CreateIniFile();
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	
		cout<<"akey:"<<iniFile.akey<<endl;
		cout<<"bkey:"<<iniFile.bkey<<endl;
		cout<<"appName:"<<iniFile.appName<<endl;
		cout<<"password:"<<iniFile.password<<endl;
	}
	return 0;
}
int noInputTxt(void)
{
	//CEncryptString encrypt= CEncryptString();

	CardIniFile iniFile = CardIniFile(false);

	iniFile.CreateIniFile();
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	
		cout<<"akey:"<<iniFile.akey<<endl;
		cout<<"bkey:"<<iniFile.bkey<<endl;
		cout<<"appName:"<<iniFile.appName<<endl;
		cout<<"password:"<<iniFile.password<<endl;
	}
	return 0;
}
int _tmain(int argc, _TCHAR* argv[])
{
	//noInputTxt();
	inputTxt();
	return 0;
}

