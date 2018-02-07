// CardTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

#include "DXInfo.Card.h"
//using namespace std; 

int _tmain(int argc, _TCHAR* argv[])
{
	printf("开始\n");
	//cout<<"开始测试"<<endl;
	unsigned char data[33]={0};
	//CString cs("A12345");
	unsigned long value=100;
	__int16 st=0;
	printf("开始1\n");
	st= CoolerReadCard(data,&value);
	printf("开始2\n");
	if(st==0)
	printf("成功，%ul\n",value);
	else
		printf("失败\n");
	printf("开始3\n");
	/*char hdid[64]={0};
	if(GetFairiesHardwareID(hdid))
	{
		printf("成功，%s\n",hdid);
	}*/
	
	return 0;
}

