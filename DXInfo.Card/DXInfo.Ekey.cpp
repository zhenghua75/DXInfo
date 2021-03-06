#include "StdAfx.h"
#include "DXInfo.Ekey.h"
#include "NT77Api.h"



DXInfoEkey::DXInfoEkey(CString projName,CString pwd)
	:m_projName(projName),m_pwd(pwd)
{
}
bool DXInfoEkey::Verify(void)
{
	long nRet = 0;
	nRet = NTFindFirst(m_projName.GetBuffer());
	if(0 != nRet)
	{
		return false;
	}

	nRet = NTLogin(m_pwd.GetBuffer());
	if( 0 != nRet)
	{
		return false;
	}
	nRet = NTLogout();
	if( 0 != nRet)
	{
		return false;
	}
	return true;
}
bool DXInfoEkey::GetHardwareID(char hdID[64])
{
	long nRet = 0;
	//char hwID[64];	
	memset(hdID,0,64);
	nRet = NTFindFirst(m_projName.GetBuffer());
	if(0 != nRet)
	{
		return false;
	}
	nRet = NTLogin(m_pwd.GetBuffer());
	if( 0 != nRet)
	{
		return false;
	}
	nRet = NTGetHardwareID(hdID);	
	if( 0 != nRet)
	{
		return false;
	}
	//hardwareID = hwID;
	nRet = NTLogout();
	if( 0 != nRet)
	{
		return false;
	}
	return true;
}
bool DXInfoEkey::GetKeyNo(unsigned char data[128])
{
	long nRet = 0;
	//char hwID[64];	
	memset(data,0,128);
	nRet = NTFindFirst(m_projName.GetBuffer());
	if(0 != nRet)
	{
		return false;
	}
	nRet = NTLogin(m_pwd.GetBuffer());
	if( 0 != nRet)
	{
		return false;
	}
	//nRet = NTGetHardwareID(hdID);	
	nRet = NTRead(0, 128, data);
	if( 0 != nRet)
	{
		return false;
	}
	//hardwareID = hwID;
	nRet = NTLogout();
	if( 0 != nRet)
	{
		return false;
	}
	return true;
}

DXInfoEkey::~DXInfoEkey(void)
{
	m_projName.ReleaseBuffer();
	m_pwd.ReleaseBuffer();
}
