#pragma once
#include "atlstr.h"
using namespace ATL;
class CDXInfoEkey
{
public:
	CDXInfoEkey(CString projName,CString pwd);
	//获取硬件ID
	bool GetHardwareID(char hdID[64]);
	// 是否通过验证
	bool Verify(void);
	bool GetKeyNo(unsigned char data[128]);
	~CDXInfoEkey(void);
private:
	CString m_projName;
	CString m_pwd;
};

