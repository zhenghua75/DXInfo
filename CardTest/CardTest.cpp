// CardTest.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"

#include "DXInfo.Card.h"
//using namespace std; 

int _tmain(int argc, _TCHAR* argv[])
{
	printf("��ʼ\n");
	//cout<<"��ʼ����"<<endl;
	unsigned char data[33]={0};
	//CString cs("A12345");
	unsigned long value=100;
	__int16 st=0;
	printf("��ʼ1\n");
	st= CoolerReadCard(data,&value);
	printf("��ʼ2\n");
	if(st==0)
	printf("�ɹ���%ul\n",value);
	else
		printf("ʧ��\n");
	printf("��ʼ3\n");
	/*char hdid[64]={0};
	if(GetFairiesHardwareID(hdid))
	{
		printf("�ɹ���%s\n",hdid);
	}*/
	
	return 0;
}

