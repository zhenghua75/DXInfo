// Key.cpp : CKey ��ʵ��
#include "stdafx.h"
#include "Key.h"
#include "atlstr.h"
#include "DXInfoEkey.h"
// CKey
#include "CardIniFile.h"
#pragma comment( lib, "DXInfo.CPPHelpers.lib" )

STDMETHODIMP CKey::Verify(VARIANT_BOOL* issuc)
{
	CardIniFile iniFile = CardIniFile(true);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString appName(iniFile.appName.c_str());
	CString password(iniFile.password.c_str());
	CDXInfoEkey key(appName,password);
	*issuc = key.Verify();
	return S_OK;
}


STDMETHODIMP CKey::GetHardwareID(BSTR* hdId)
{
	// TODO: �ڴ����ʵ�ִ���
	CString strResult;

	// TODO: �ڴ���ӵ��ȴ���������
	CardIniFile iniFile = CardIniFile(true);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString appName(iniFile.appName.c_str());
	CString password(iniFile.password.c_str());
	CDXInfoEkey key(appName,password);
	char hardwareID[64];
	key.GetHardwareID(hardwareID);
	strResult=hardwareID;
	*hdId = strResult.AllocSysString();
	return S_OK;
}


STDMETHODIMP CKey::GetKeyNo(BSTR* data)
{
	// TODO: �ڴ����ʵ�ִ���

	CString strResult;
	CardIniFile iniFile = CardIniFile(true);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString appName(iniFile.appName.c_str());
	CString password(iniFile.password.c_str());
	CDXInfoEkey key(appName,password);
	unsigned char keyno[128];
	key.GetKeyNo(keyno);
	strResult=keyno;
	*data = strResult.AllocSysString();
	return S_OK;
}
