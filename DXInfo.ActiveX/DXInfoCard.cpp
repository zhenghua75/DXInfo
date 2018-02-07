#include "StdAfx.h"
#include "DXInfoCard.h"
#include "mwrf32.h"
CDXInfoCard::CDXInfoCard(CString akey,CString bkey,CString akey_old,CString bkey_old,long baund,int port,int sector)
{
		m_baund = baund;
		m_Port = port;
		m_Sector = sector;//1扇区
		m_akey = akey;//_T("");
		m_bkey = bkey;//_T("");
		m_akey_old = akey_old;//_T("");
		m_bkey_old = bkey_old;//_T("");
		icdev = NULL;
}

CDXInfoCard::~CDXInfoCard(void)
{
}

bool CDXInfoCard::ReadCard(unsigned char data[33])
{
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
    //unsigned char data[33];
	memset(data,0,33);	
	//__int16 st;
	unsigned char _Status[30];
	memset(_Status,0,30);
	unsigned char m_keymode;

	rf_exit(icdev);
	icdev = rf_init(m_Port,m_baund);
	st = rf_get_status(icdev,_Status);
	if(icdev<0 || st)
		return false;

	st = rf_reset(icdev,5);	
	st = rf_request(icdev,1,&TagType);
	if(st)
	{		
		st = rf_exit(icdev);
		return false;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{		
		st = rf_exit(icdev);
		return false;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{		
		st = rf_exit(icdev);
		return false;
	}
	unsigned char key[7];
	memset(key,0,7);
	
	//验证A密码
	a_hex(m_akey.GetBuffer(12),key,12);	
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{		
		st = rf_exit(icdev);
		return false;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{		
		st = rf_exit(icdev);
		return false;
	}
	//验证B密码
	memset(key,0,7);
	a_hex(m_bkey.GetBuffer(12),key,12);	
	m_keymode=4;	
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{		
		st = rf_exit(icdev);
		return false;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{		
		st = rf_exit(icdev);
		return false;
	}
	////
	//unsigned char temp[33];
	//memset(temp,0,33);	
	memset(data,0,33);
	st = rf_read(icdev,Sec*4,data);		
	if(st)
	{			
		st = rf_exit(icdev);
		return false;
	}
	//hex_a(temp,data,16);
	//m_Data.Format("%s",temp);
	rf_beep(icdev,10);	
	st = rf_exit(icdev);
	return true;
}

// 写卡
bool CDXInfoCard::PutCard(unsigned char data[33])
{
	unsigned char _Status[30];	
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	memset(_Status,0,30);

	if(icdev)
		rf_exit(icdev);
	icdev = rf_init(m_Port,m_baund);
	st = rf_get_status(icdev,_Status);
	if(icdev<0 || st)
	{
		if(icdev)
			st = rf_exit(icdev);
		return false;
	}
	st = rf_reset(icdev,5);	
	st = rf_request(icdev,1,&TagType);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
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
		st = rf_exit(icdev);
		return false;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	//验证B密码
	memset(key,0,7);	
	a_hex(m_bkey_old.GetBuffer(12),key,12);	
	m_keymode=4;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	////
	//unsigned char temp[33];
	//memset(temp,0,33);
	
	//unsigned char data_temp[33];
	//memset(data_temp,0,33);

	//a_hex(data,temp,32);
	//memcpy(data_temp,temp,16);		
	st = rf_write(icdev,Sec*4,data);		
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	unsigned char akey[7];
	unsigned char bkey[7];
	memset(akey,0,7);
	memset(bkey,0,7);
	a_hex(m_akey.GetBuffer(12),akey,12);	
	a_hex(m_bkey.GetBuffer(12),bkey,12);	
	st = rf_changeb3(icdev, Sec, akey, 3, 3, 3, 3, 0, bkey);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}    
	rf_beep(icdev,10);
	st = rf_exit(icdev);
	return true;
}

bool CDXInfoCard::RecycleCard()
{
	unsigned char _Status[30];	
	unsigned char Sec = m_Sector;
    __int16 st;
    unsigned __int16 TagType;
	unsigned long Snr;
	unsigned char Size;
	memset(_Status,0,30);

	if(icdev)
		rf_exit(icdev);
	icdev = rf_init(m_Port,m_baund);
	st = rf_get_status(icdev,_Status);
	if(icdev<0 || st)
	{
		if(icdev)
			st = rf_exit(icdev);
		return false;
	}
	st = rf_reset(icdev,5);	
	st = rf_request(icdev,1,&TagType);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	st = rf_anticoll(icdev,0, &Snr);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	st = rf_select(icdev,Snr,&Size);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	unsigned char key[7];
	unsigned char m_keymode;
	//验证A密码
	
	memset(key,0,7);	
	a_hex(m_akey.GetBuffer(12),key,12);	
	m_keymode=0;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	//验证B密码
	memset(key,0,7);	
	a_hex(m_bkey.GetBuffer(12),key,12);	
	m_keymode=4;
	st = rf_load_key(icdev,m_keymode,Sec,key);
	if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	st = rf_authentication(icdev,m_keymode,Sec);
    if(st)
	{
		st = rf_exit(icdev);
		return false;
	}
	////
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
		st = rf_exit(icdev);
		return false;
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
		st = rf_exit(icdev);
		return false;
	}    
	rf_beep(icdev,10);
	st = rf_exit(icdev);
	return true;
}
