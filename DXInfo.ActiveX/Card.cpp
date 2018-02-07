// Card.cpp : CCard ��ʵ��
#include "stdafx.h"
#include "Card.h"
#include "atlstr.h"
#include "DXInfoCard.h"
// CCard
#include "CardIniFile.h"
#pragma comment( lib, "DXInfo.CPPHelpers.lib" )

STDMETHODIMP CCard::ReadCard(BSTR* data)
{
	// TODO: �ڴ����ʵ�ִ���
	CString strResult;
	
	// TODO: �ڴ���ӵ��ȴ���������
	unsigned char reddata[33];
	CardIniFile iniFile = CardIniFile(true);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	if(card.ReadCard(reddata))
		strResult.Format("%s",reddata);
	else
		strResult="";
	*data = strResult.AllocSysString();
	return S_OK;
}


STDMETHODIMP CCard::PutCard(BSTR data, VARIANT_BOOL* issuc)
{
	// TODO: �ڴ����ʵ�ִ���
	CardIniFile iniFile = CardIniFile(true);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);

	//-----------------
	int length = SysStringLen(data);  //ȡBSTR����
	unsigned char b[33]; 
	memset(b,0,33);
	for(int i=0; i <length; i++)b[i]=(unsigned char)data[i]; 
	if(card.PutCard(b))
		*issuc=true;
	else
		*issuc=false;
	return S_OK;
}


STDMETHODIMP CCard::RecycleCard(VARIANT_BOOL* issuc)
{
	// TODO: �ڴ����ʵ�ִ���
	CardIniFile iniFile = CardIniFile(true);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	if(card.RecycleCard())
		*issuc=true;
	else
		*issuc=false;
	return S_OK;
}
