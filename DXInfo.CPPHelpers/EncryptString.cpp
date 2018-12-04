#include "StdAfx.h"
#include "EncryptString.h"

#pragma comment( lib, "cryptlib.lib" )


string CEncryptString::string_to_hex(const std::string& input)
{
    static const char* const lut = "0123456789ABCDEF";
    size_t len = input.length();

    std::string output;
    output.reserve(2 * len);
    for (size_t i = 0; i < len; ++i)
    {
        const unsigned char c = input[i];
        output.push_back(lut[c >> 4]);
        output.push_back(lut[c & 15]);
    }
    return output;
}
string CEncryptString::hex_to_string(const std::string& input)
{
    static const char* const lut = "0123456789ABCDEF";
    size_t len = input.length();
    if (len & 1) throw std::invalid_argument("odd length");

    std::string output;
    output.reserve(len / 2);
    for (size_t i = 0; i < len; i += 2)
    {
        char a = input[i];
        const char* p = std::lower_bound(lut, lut + 16, a);
        if (*p != a) throw std::invalid_argument("not a hex digit");

        char b = input[i + 1];
        const char* q = std::lower_bound(lut, lut + 16, b);
        if (*q != b) throw std::invalid_argument("not a hex digit");

        output.push_back(((p - lut) << 4) | (q - lut));
    }
    return output;
}

CEncryptString::CEncryptString(void)
{
	key[0]= 'z';
	key[1]= 'h';
	key[2]= 'h';
	key[3]= 'l';
	key[4]= 'h';
	key[5]= 'g';
	key[6]= '2';
	key[7]= '0';
	key[8]= '1';
	key[9]= '3';
	key[10]= '0';
	key[11]= '1';
	key[12]= '1';
	key[13]= '3';
	key[14]= '1';
	key[15]= '1';
	iv[0]= 'z';
	iv[1]= 'h';
	iv[2]= 'h';
	iv[3]= 'l';
	iv[4]= 'h';
	iv[5]= 'g';
	iv[6]= '2';
	iv[7]= '0';
	iv[8]= '1';
	iv[9]= '3';
	iv[10]= '0';
	iv[11]= '1';
	iv[12]= '1';
	iv[13]= '3';
	iv[14]= '2';
	iv[15]= '2';
}


CEncryptString::~CEncryptString(void)
{
}

string CEncryptString::Encrypt(string message)
{
	string  strEncTxt;
	 CBC_Mode<AES>::Encryption  Encryptor1(key,AES::DEFAULT_KEYLENGTH,iv);
	 StringSource(   message,
					 true,
					 new StreamTransformationFilter( Encryptor1,
						 new StringSink( strEncTxt ),
						 BlockPaddingSchemeDef::BlockPaddingScheme::ONE_AND_ZEROS_PADDING)
			 );
	 return string_to_hex(strEncTxt);
}
string CEncryptString::Decrypt(string strEncTxt)
{
	 string  strDecTxt;
	 CBC_Mode<AES>::Decryption Decryptor1(key,AES::DEFAULT_KEYLENGTH,iv);
	 StringSource(   hex_to_string(strEncTxt),
					 true,
					 new StreamTransformationFilter( Decryptor1,
						 new StringSink( strDecTxt ),
						 BlockPaddingSchemeDef::BlockPaddingScheme::ONE_AND_ZEROS_PADDING)
			 );
	 return strDecTxt;
}