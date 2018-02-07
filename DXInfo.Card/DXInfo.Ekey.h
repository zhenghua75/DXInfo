#pragma once
#include "atlstr.h"
using namespace ATL;
class DXInfoEkey
{
public:
	//DXInfoEkey(void);
	DXInfoEkey(CString projName,CString pwd);
	//获取硬件ID
	bool GetHardwareID(char hdID[64]);
	// 是否通过验证
	bool Verify(void);
	bool GetKeyNo(unsigned char data[128]);
	~DXInfoEkey(void);
private:
	CString m_projName;
	CString m_pwd;
};

