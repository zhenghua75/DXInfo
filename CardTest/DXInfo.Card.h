// ���� ifdef ���Ǵ���ʹ�� DLL �������򵥵�
// ��ı�׼�������� DLL �е������ļ��������������϶���� DXINFOCARD_EXPORTS
// ���ű���ġ���ʹ�ô� DLL ��
// �κ�������Ŀ�ϲ�Ӧ����˷��š�������Դ�ļ��а������ļ����κ�������Ŀ���Ὣ
// DXINFOCARD_API ������Ϊ�Ǵ� DLL ����ģ����� DLL ���ô˺궨���
// ������Ϊ�Ǳ������ġ�
#ifdef DXINFOCARD_EXPORTS
#define DXINFOCARD_API __declspec(dllexport)
#else
#define DXINFOCARD_API __declspec(dllimport)
#endif
#include "atlstr.h"
using namespace ATL;
// �����Ǵ� DXInfo.Card.dll ������
class DXINFOCARD_API CDXInfoCard {
public:
	CDXInfoCard(CString akey,CString bkey,CString akey_old,CString bkey_old,long baund=9600,int port=0,int sector=1);
	// ����
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