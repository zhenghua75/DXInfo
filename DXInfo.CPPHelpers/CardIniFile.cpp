#include "StdAfx.h"
#include "CardIniFile.h"
#include "IniFile.h"
#include <iostream>
#include <fstream>
#include <sys/stat.h> //stat, stat()

//#include <windows.h>
#include <Shlobj.h>
#include <string>

CardIniFile::CardIniFile(bool isActivex)
{
	//玉溪 之前是空密码
	akey="";//"7A68656E676875616C6867";//"zhenghualhg";lhgzhenghua
	bkey="";//"6C756F68756169677A6868";//"luohuaigzhh";zhhluohuaig
	appName="FairiesMemberManage";
	password="234359545fe80d55b50fc048e10ce898";
	
	if(isActivex)
	{
		TCHAR szPath[MAX_PATH];	 
		if(SUCCEEDED(SHGetFolderPath(NULL, 
									 CSIDL_SYSTEMX86 , 
									 NULL, 
									 0, 
									 szPath))) 
		{
			int size= wcslen(szPath); 
			char ansi_string[MAX_PATH]; 
			wcstombs(ansi_string, szPath, size+1); 
			FileName = ansi_string;
			FileName.append("\\DXInfo\\DXInfo.Card.ini");
		}
	}
	else
	{
		FileName = "DXInfo.Card.ini";
	}
}

CardIniFile::~CardIniFile(void)
{
}

int CardIniFile::CreateIniFile(void)
{
	if (!CIniFile::Create(FileName)) 
		cout << "文件创建失败" << endl << endl;

	if (!CIniFile::AddSection("sec1",FileName)) 
		cout << "card节创建失败" << endl << endl;
	CEncryptString encrypt= CEncryptString();
	if (!CIniFile::SetValue("rec11",encrypt.Encrypt(akey),"sec1",FileName)) 
		cout << "akey记录创建失败" << endl << endl;

	if (!CIniFile::SetValue("rec12",encrypt.Encrypt(bkey),"sec1",FileName)) 
		cout << "bkey记录创建失败" << endl << endl;


	if (!CIniFile::AddSection("sec2",FileName)) 
		cout << "ekey节创建失败" << endl << endl;

	if (!CIniFile::SetValue("rec21",encrypt.Encrypt(appName),"sec2",FileName)) 
		cout << "appName记录创建失败" << endl << endl;

	if (!CIniFile::SetValue("rec22",encrypt.Encrypt(password),"sec2",FileName)) 
		cout << "password记录创建失败" << endl << endl;

	return 0;
}

int CardIniFile::GetIniFile(void)
{
	if (!CIniFile::SectionExists("sec1",FileName)) 
		return 1;
	if (!CIniFile::RecordExists("rec11","sec1",FileName)) 
		return 11;
	if (!CIniFile::RecordExists("rec12","sec1",FileName)) 
		return 12;
	if (!CIniFile::SectionExists("sec2",FileName)) 
		return 2;
	if (!CIniFile::RecordExists("rec21","sec2",FileName)) 
		return 21;
	if (!CIniFile::RecordExists("rec22","sec2",FileName)) 
		return 22;
	CEncryptString encrypt= CEncryptString();
	string akey_t = CIniFile::GetValue("rec11","sec1",FileName);
	if(akey_t.length()>0)
	{
	}
		akey = encrypt.Decrypt(akey_t);
	string bkey_t = CIniFile::GetValue("rec12","sec1",FileName);
	if(bkey_t.length()>0)
	{
		bkey = encrypt.Decrypt(bkey_t);
	}
	appName = encrypt.Decrypt(CIniFile::GetValue("rec21","sec2",FileName));
	password = encrypt.Decrypt(CIniFile::GetValue("rec22","sec2",FileName));

	//CIniFile::SetValue("rec32","akey:"+akey,"sec3",FileName);
	//CIniFile::SetValue("rec33","bkey:"+bkey,"sec3",FileName);
}

bool CardIniFile::IsIniFile(void)
{	
	/*struct stat info;
	int ret = -1;
	ret = stat(FileName.c_str(), &info);
	if(ret == 0) 
    {
		return true;
    } 
    else 
    {
		return false;
    }	*/

	

	bool isExist = false;
	ifstream inFile (FileName.c_str());	
	if (inFile.is_open())
	{
		isExist=true;
	}
	inFile.close();
	//CIniFile::AddSection("sec3",FileName);
	//CIniFile::SetValue("rec31","读取文件成功"+FileName,"sec3",FileName);
	return isExist;
}