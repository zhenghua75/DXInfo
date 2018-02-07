#pragma once
#include "stdafx.h"
#include <iostream>
#include "default.h"
#include "cryptlib.h"
#include "filters.h"
#include "bench.h"
#include "osrng.h"
#include "hex.h"
#include "modes.h"
#include "files.h"
using namespace std; 
using namespace CryptoPP;
class CEncryptString
{
public:
	CEncryptString(void);
	~CEncryptString(void);
	string string_to_hex(const std::string& input);
	string hex_to_string(const std::string& input);
	string Encrypt(string message);
	string Decrypt(string message);
private:
	unsigned char key[AES::DEFAULT_KEYLENGTH];
	unsigned char iv[AES::DEFAULT_KEYLENGTH];
};

