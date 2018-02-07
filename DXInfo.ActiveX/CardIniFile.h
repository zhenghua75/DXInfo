#pragma once
#include <string>
#include "EncryptString.h"
using namespace std; 

class CardIniFile
{
public:
	string akey;
	string bkey;
	string appName;
	string password;
	CardIniFile(bool isActivex);
	~CardIniFile(void);
	int CreateIniFile(void);
	int GetIniFile(void);
	bool IsIniFile(void);
private:
	string FileName;
};

