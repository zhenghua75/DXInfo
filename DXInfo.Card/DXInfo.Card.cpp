// DXInfo.Card.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "DXInfo.Card.h"
#include "mwrf32.h"
#include "DXInfo.Ekey.h"
#include "CardIniFile.h"
//#include "EncryptString.h"
#pragma comment( lib, "DXInfo.CPPHelpers.lib" )
// 这是导出变量的一个示例
DXINFOCARD_API int nDXInfoCard=0;

// 这是导出函数的一个示例。
DXINFOCARD_API __int16 __stdcall CoolerReadCard(unsigned char data[33],unsigned long *value)
{
	CardIniFile iniFile = CardIniFile(false);	
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	return card.ReadCard(data,value);	
}
DXINFOCARD_API __int16 __stdcall CoolerPutCard(unsigned char data[33])
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	return card.PutCard(data);	
}
DXINFOCARD_API __int16 __stdcall CoolerRechargeCard(unsigned char data[33],unsigned long value)
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	return card.RechargeCard(data,value);	
}
DXINFOCARD_API __int16 __stdcall CancelCoolerRechargeCard(unsigned char data[33],unsigned long value)
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	return card.CancelRechargeCard(data,value);	
}
DXINFOCARD_API __int16 __stdcall CoolerConsumeCard(unsigned char data[33],unsigned long value)
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	return card.ConsumeCard(data,value);	
}
DXINFOCARD_API __int16 __stdcall CancelCoolerConsumeCard(unsigned char data[33],unsigned long value)
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	return card.CancelConsumeCard(data,value);	
}
DXINFOCARD_API __int16 __stdcall CoolerRecycleCard()
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString akey(iniFile.akey.c_str());
	CString bkey(iniFile.bkey.c_str());
	CString akey_old("A3D4C68CD9E5");
	CString bkey_old("B01B4C49A3D3");
	CDXInfoCard card(akey,bkey,akey_old,bkey_old);
	return card.RecycleCard();	
}


DXINFOCARD_API bool __stdcall GetFairiesHardwareID(char hdID[64])
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString appName(iniFile.appName.c_str());
	CString password(iniFile.password.c_str());
	DXInfoEkey key(appName,password);
	return key.GetHardwareID(hdID);
}
DXINFOCARD_API bool __stdcall FairsVerify(void)
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString appName(iniFile.appName.c_str());
	CString password(iniFile.password.c_str());
	DXInfoEkey key(appName,password);
	return key.Verify();
}
DXINFOCARD_API bool __stdcall GetFairsKeyNo(unsigned char data[128])
{
	CardIniFile iniFile = CardIniFile(false);
	if(iniFile.IsIniFile())
	{
		iniFile.GetIniFile();
	}
	CString appName(iniFile.appName.c_str());
	CString password(iniFile.password.c_str());
	DXInfoEkey key(appName,password);
	return key.GetKeyNo(data);
}
// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 DXInfo.Card.h
CDXInfoCard::CDXInfoCard(CString akey,CString bkey,CString akey_old,CString bkey_old,long baund,int port,int sector)
	:m_baund(baund),m_Port(port),m_Sector(sector),m_akey(akey),m_bkey(bkey),m_akey_old(akey_old),m_bkey_old(bkey_old)
{
	return;
}

__int16 CDXInfoCard::ReadCard(unsigned char data[33],unsigned long *value)
{
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	memset(data,0,33);	
	unsigned char _Status[30];
	memset(_Status,0,30);
	unsigned char m_keymode;

	/*rf_exit(icdev);*/
	icdev = rf_init(m_Port,m_baund);
	/*st = rf_get_status(icdev,_Status);
	if(icdev<0 || st)
		return 98;

	st = rf_reset(icdev,5);	*/
	if(icdev<0)
	{		
		return 98;
	}
	st = rf_request(icdev,1,&TagType);
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}

	unsigned char key[7];
	memset(key,0,7);
	
	//验证A密码
	//a_hex(m_akey.GetBuffer(12),key,12);	
	if(!m_akey.IsEmpty())
	{
		memcpy(key,m_akey,6);
	}
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	m_akey.ReleaseBuffer();
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	//验证B密码
	memset(key,0,7);
	//a_hex(m_bkey.GetBuffer(12),key,12);	
	if(!m_bkey.IsEmpty())
	{
		memcpy(key,m_bkey,6);
	}
	m_keymode=4;	
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{		
		rf_exit(icdev);
		return st;
	}	
	memset(data,0,33);
	st = rf_read(icdev,Sec*4,data);		
	if(st)
	{			
		rf_exit(icdev);
		return st;
	}
	st=rf_readval(icdev,Sec*4+1,value);      
	if(st)
	{			
		rf_exit(icdev);
		return st;
	}
	rf_beep(icdev,10);	
	rf_halt(icdev);
	rf_exit(icdev);
	return 0;
}

// 写卡
__int16 CDXInfoCard::PutCard(unsigned char data[33])
{
	unsigned char _Status[30];	
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	memset(_Status,0,30);
	/*if(icdev)
		rf_exit(icdev);*/
	icdev = rf_init(m_Port,m_baund);
	//st = rf_get_status(icdev,_Status);
	/*if(icdev<0 || st)
	{
		if(icdev)
			rf_exit(icdev);
		return 98;
	}
	st = rf_reset(icdev,5);	*/
	if(icdev<0)
	{		
		return 98;
	}
	st = rf_request(icdev,1,&TagType);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char key[7];
	unsigned char m_keymode;
	//验证A密码	
	memset(key,0,7);	
	a_hex(m_akey_old.GetBuffer(12),key,12);	
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	//验证B密码
	memset(key,0,7);	
	a_hex(m_bkey_old.GetBuffer(12),key,12);	
	m_keymode=4;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}	
	st = rf_write(icdev,Sec*4,data);		
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st=rf_initval(icdev,Sec*4+1,0);		
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char akey[7];
	unsigned char bkey[7];
	memset(akey,0,7);
	memset(bkey,0,7);
	//a_hex(m_akey.GetBuffer(12),akey,12);	
	//a_hex(m_bkey.GetBuffer(12),bkey,12);	
	if(!m_akey.IsEmpty())
	{
		memcpy(akey,m_akey,6);
	}
	if(!m_bkey.IsEmpty())
	{
		memcpy(bkey,m_bkey,6);
	}
	st = rf_changeb3(icdev, Sec, akey, 3, 0, 3, 3, 0, bkey);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}    
	rf_beep(icdev,10);
	rf_halt(icdev);
	rf_exit(icdev);
	return 0;
}
//充值
__int16 CDXInfoCard::RechargeCard(unsigned char data[33],unsigned long value)
{
	unsigned char _Status[30];	
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	//memset(_Status,0,30);
	/*if(icdev)
		rf_exit(icdev)*/;
	/*if(icdev<=0)
	{
		
	}*/
	icdev = rf_init(m_Port,m_baund);
	//st = rf_get_status(icdev,_Status);
	/*if(icdev<0 || st)
	{
		if(icdev)
			rf_exit(icdev);
		return st;
	}*/
	if(icdev<0)
	{		
		return 98;
	}
	/*if(icdev<0)
	{
		return icdev;
	}*/
	//st = rf_reset(icdev,5);	
	st = rf_request(icdev,1,&TagType);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char key[7];
	unsigned char m_keymode;
	//验证A密码	
	memset(key,0,7);	
	//a_hex(m_akey.GetBuffer(12),key,12);
	if(!m_akey.IsEmpty())
	{
		memcpy(key,m_akey,6);
	}
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	//验证B密码
	memset(key,0,7);	
	//a_hex(m_bkey.GetBuffer(12),key,12);	
	if(!m_bkey.IsEmpty())
	{
		memcpy(key,m_bkey,6);
	}
	m_keymode=4;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char temp[33];
	memset(temp,0,33);
	st = rf_read(icdev,Sec*4,temp);		
	if(st)
	{			
		rf_exit(icdev);
		return st;
	}
	CString ctemp(temp);
	CString cdata(data);
	if(ctemp!=cdata)
	{
		rf_exit(icdev);
		return 99;
	}
	/*st=rf_initval(icdev,Sec*4+1,value);		
	if(st)
	{
		rf_exit(icdev);
		return st;
	}  
	rf_beep(icdev,10);*/
	st=rf_increment(icdev,Sec*4+1,value); 
	//st=rf_readval(icdev,Sec*4+1,value2);      
	if(st)
	{
		rf_exit(icdev);
		return st;
	}  
	rf_beep(icdev,10);
	rf_halt(icdev);
	rf_exit(icdev);
	return 0;
}
__int16 CDXInfoCard::CancelRechargeCard(unsigned char data[33],unsigned long value)
{
	unsigned char _Status[30];	
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	//memset(_Status,0,30);
	/*if(icdev)
		rf_exit(icdev)*/;
	/*if(icdev<=0)
	{
		
	}*/
	icdev = rf_init(m_Port,m_baund);
	//st = rf_get_status(icdev,_Status);
	/*if(icdev<0 || st)
	{
		if(icdev)
			rf_exit(icdev);
		return st;
	}*/
	if(icdev<0)
	{		
		return 98;
	}
	/*if(icdev<0)
	{
		return icdev;
	}*/
	//st = rf_reset(icdev,5);	
	st = rf_request(icdev,1,&TagType);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char key[7];
	unsigned char m_keymode;
	//验证A密码	
	memset(key,0,7);	
	//a_hex(m_akey.GetBuffer(12),key,12);
	if(!m_akey.IsEmpty())
	{
		memcpy(key,m_akey,6);
	}
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	//验证B密码
	memset(key,0,7);	
	//a_hex(m_bkey.GetBuffer(12),key,12);	
	if(!m_bkey.IsEmpty())
	{
		memcpy(key,m_bkey,6);
	}
	m_keymode=4;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char temp[33];
	memset(temp,0,33);
	st = rf_read(icdev,Sec*4,temp);		
	if(st)
	{			
		rf_exit(icdev);
		return st;
	}
	CString ctemp(temp);
	CString cdata(data);
	if(ctemp!=cdata)
	{
		rf_exit(icdev);
		return 99;
	}
	/*st=rf_initval(icdev,Sec*4+1,value);		
	if(st)
	{
		rf_exit(icdev);
		return st;
	}  
	rf_beep(icdev,10);*/
	st=rf_decrement(icdev,Sec*4+1,value); 
	//st=rf_readval(icdev,Sec*4+1,value2);      
	if(st)
	{
		rf_exit(icdev);
		return st;
	}  
	rf_beep(icdev,10);
	rf_halt(icdev);
	rf_exit(icdev);
	return 0;
}
//消费
__int16 CDXInfoCard::ConsumeCard(unsigned char data[33],unsigned long value)
{
	unsigned char _Status[30];	
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	memset(_Status,0,30);
	/*if(icdev)
		rf_exit(icdev);*/
	icdev = rf_init(m_Port,m_baund);
	/*st = rf_get_status(icdev,_Status);
	if(icdev<0 || st)
	{
		if(icdev)
			rf_exit(icdev);
		return st;
	}
	st = rf_reset(icdev,5);	*/
	if(icdev<0)
	{		
		return 98;
	}
	st = rf_request(icdev,1,&TagType);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char key[7];
	unsigned char m_keymode;
	//验证A密码	
	memset(key,0,7);	
	//a_hex(m_akey.GetBuffer(12),key,12);	
	if(!m_akey.IsEmpty())
	{
		memcpy(key,m_akey,6);
	}
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	//验证B密码
	memset(key,0,7);	
	//a_hex(m_bkey.GetBuffer(12),key,12);
	if(!m_bkey.IsEmpty())
	{
		memcpy(key,m_bkey,6);
	}
	m_keymode=4;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char temp[33];
	memset(temp,0,33);
	st = rf_read(icdev,Sec*4,temp);		
	if(st)
	{			
		rf_exit(icdev);
		return st;
	}
	CString ctemp(temp);
	CString cdata(data);
	if(ctemp!=cdata)
	{
		rf_exit(icdev);
		return 99;
	}
	st=rf_decrement(icdev,Sec*4+1,value); 
	if(st)
	{
		rf_exit(icdev);
		return st;
	}  
	rf_beep(icdev,10);
	rf_halt(icdev);
	rf_exit(icdev);
	return 0;
}
__int16 CDXInfoCard::CancelConsumeCard(unsigned char data[33],unsigned long value)
{
	unsigned char _Status[30];	
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	memset(_Status,0,30);
	/*if(icdev)
		rf_exit(icdev);*/
	icdev = rf_init(m_Port,m_baund);
	/*st = rf_get_status(icdev,_Status);
	if(icdev<0 || st)
	{
		if(icdev)
			rf_exit(icdev);
		return st;
	}
	st = rf_reset(icdev,5);	*/
	if(icdev<0)
	{		
		return 98;
	}
	st = rf_request(icdev,1,&TagType);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char key[7];
	unsigned char m_keymode;
	//验证A密码	
	memset(key,0,7);	
	//a_hex(m_akey.GetBuffer(12),key,12);	
	if(!m_akey.IsEmpty())
	{
		memcpy(key,m_akey,6);
	}
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	//验证B密码
	memset(key,0,7);	
	//a_hex(m_bkey.GetBuffer(12),key,12);
	if(!m_bkey.IsEmpty())
	{
		memcpy(key,m_bkey,6);
	}
	m_keymode=4;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char temp[33];
	memset(temp,0,33);
	st = rf_read(icdev,Sec*4,temp);		
	if(st)
	{			
		rf_exit(icdev);
		return st;
	}
	CString ctemp(temp);
	CString cdata(data);
	if(ctemp!=cdata)
	{
		rf_exit(icdev);
		return 99;
	}
	st=rf_increment(icdev,Sec*4+1,value); 
	if(st)
	{
		rf_exit(icdev);
		return st;
	}  
	rf_beep(icdev,10);
	rf_halt(icdev);
	rf_exit(icdev);
	return 0;
}
__int16 CDXInfoCard::RecycleCard()
{
	unsigned char _Status[30];	
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	memset(_Status,0,30);
	/*if(icdev)
		rf_exit(icdev);*/
	icdev = rf_init(m_Port,m_baund);
	/*st = rf_get_status(icdev,_Status);
	if(icdev<0 || st)
	{
		if(icdev)
			st = rf_exit(icdev);
		return 98;
	}
	st = rf_reset(icdev,5);	*/
	if(icdev<0)
	{		
		return 98;
	}
	st = rf_request(icdev,1,&TagType);
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	unsigned char key[7];
	unsigned char m_keymode;
	//验证A密码
	
	memset(key,0,7);	
	//a_hex(m_akey.GetBuffer(12),key,12);	
	if(!m_akey.IsEmpty())
	{
		memcpy(key,m_akey,6);
	}
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{		
		rf_exit(icdev);
		return st;
	}
	//验证B密码
	memset(key,0,7);	
	//a_hex(m_bkey.GetBuffer(12),key,12);
	if(!m_bkey.IsEmpty())
	{
		memcpy(key,m_bkey,6);
	}
	m_keymode=4;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char temp[33];
	memset(temp,0,33);	
	unsigned char data_temp[33];
	memset(data_temp,0,33);
	char data[33];
	memset(data,0,33);
	a_hex(data,temp,32);
	memcpy(data_temp,temp,16);		
	st = rf_write(icdev,Sec*4,data_temp);		
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	st = rf_write(icdev,Sec*4+1,data_temp);		
	if(st)
	{
		rf_exit(icdev);
		return st;
	}
	unsigned char akey[7];
	unsigned char bkey[7];
	memset(akey,0,7);
	memset(bkey,0,7);
	a_hex(m_akey_old.GetBuffer(12),akey,12);	
	a_hex(m_bkey_old.GetBuffer(12),bkey,12);	
	st = rf_changeb3(icdev, Sec, akey, 3, 3, 3, 3, 0, bkey);
	if(st)
	{
		rf_exit(icdev);
		return st;
	}    
	rf_beep(icdev,10);
	rf_halt(icdev);
	rf_exit(icdev);
	return 0;
}