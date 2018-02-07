// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 DXINFOCARD_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何其他项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// DXINFOCARD_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。
#ifdef DXINFOCARD_EXPORTS
#define DXINFOCARD_API __declspec(dllexport)
#else
#define DXINFOCARD_API __declspec(dllimport)
#endif
#include "atlstr.h"
using namespace ATL;
// 此类是从 DXInfo.Card.dll 导出的
class DXINFOCARD_API CDXInfoCard {
public:
	CDXInfoCard(CString akey,CString bkey,CString akey_old,CString bkey_old,long baund=9600,int port=0,int sector=1);
	// 读卡
	__int16 CDXInfoCard::ReadCard(unsigned char data[33],unsigned long *value);
	__int16 PutCard(unsigned char data[33]);
	__int16 RechargeCard(unsigned char data[33],unsigned long value);
	__int16 ConsumeCard(unsigned char data[33],unsigned long value);
	__int16 RecycleCard();
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


extern "C" DXINFOCARD_API __int16 __stdcall CoolerReadCard(unsigned char data[33],unsigned long *value);
extern "C" DXINFOCARD_API __int16 __stdcall CoolerPutCard(unsigned char data[33]);
extern "C" DXINFOCARD_API __int16 __stdcall CoolerRechargeCard(unsigned char data[33],unsigned long value);
extern "C" DXINFOCARD_API __int16 __stdcall CoolerConsumeCard(unsigned char data[33],unsigned long value);
extern "C" DXINFOCARD_API __int16 __stdcall CoolerRecycleCard();
extern "C" DXINFOCARD_API bool __stdcall GetFairiesHardwareID(char hdID[64]);
extern "C" DXINFOCARD_API bool __stdcall FairsVerify(void);
extern "C" DXINFOCARD_API bool __stdcall GetFairsKeyNo(unsigned char data[128]);