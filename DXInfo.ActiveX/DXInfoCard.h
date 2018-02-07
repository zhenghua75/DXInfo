#pragma once
#include "atlstr.h"
using namespace ATL;
class CDXInfoCard
{
public:
	CDXInfoCard(CString akey,CString bkey,CString akey_old,CString bkey_old,long baund=9600,int port=0,int sector=1);
	// TODO: 在此添加您的方法。
	bool ReadCard(unsigned char data[33]);
	bool PutCard(unsigned char data[33]);
	bool RecycleCard();
	~CDXInfoCard(void);
private:
	HANDLE icdev;
	long m_baund;
	int m_Port;
	int m_Sector;	
	CString m_akey;
	CString m_bkey;
	CString m_akey_old;
	CString m_bkey_old;
};

