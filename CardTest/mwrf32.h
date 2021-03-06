extern "C"
{
HANDLE __stdcall  usb_init();
HANDLE __stdcall  rf_init(__int16 port,long baud);
__int16 __stdcall rf_exit(HANDLE icdev);
__int16 __stdcall rf_config(HANDLE icdev,unsigned char _Mode,unsigned char _Baud);
__int16 __stdcall rf_request(HANDLE icdev,unsigned char _Mode,unsigned __int16 *TagType);
__int16 __stdcall rf_request_std(HANDLE icdev,unsigned char _Mode,unsigned __int16 *TagType);
__int16 __stdcall rf_anticoll(HANDLE icdev,unsigned char _Bcnt,unsigned long *_Snr);
__int16 __stdcall rf_select(HANDLE icdev,unsigned long _Snr,unsigned char *_Size);
__int16 __stdcall rf_authentication(HANDLE icdev,unsigned char _Mode,unsigned char _SecNr);
__int16 __stdcall rf_halt(HANDLE icdev);
__int16 __stdcall rf_read(HANDLE icdev,unsigned char _Adr,unsigned char *_Data);
__int16 __stdcall rf_read_hex(HANDLE icdev,unsigned char _Adr, char *_Data);
__int16 __stdcall rf_write(HANDLE icdev,unsigned char _Adr,unsigned char *_Data);
__int16 __stdcall rf_write_hex(HANDLE icdev,unsigned char _Adr,char *_Data);
__int16 __stdcall rf_load_key(HANDLE icdev,unsigned char _Mode,unsigned char _SecNr,unsigned char *_NKey);
__int16 __stdcall rf_load_key_hex(HANDLE icdev,unsigned char _Mode,unsigned char _SecNr, char *_NKey);
__int16 __stdcall rf_increment(HANDLE icdev,unsigned char _Adr,unsigned long _Value);
__int16 __stdcall rf_decrement(HANDLE icdev,unsigned char _Adr,unsigned long _Value);
__int16 __stdcall rf_decrement_ml(HANDLE icdev,unsigned __int16 _Value);
__int16 __stdcall rf_restore(HANDLE icdev,unsigned char _Adr);
__int16 __stdcall rf_transfer(HANDLE icdev,unsigned char _Adr);
__int16 __stdcall rf_card(HANDLE icdev,unsigned char _Mode,unsigned long *_Snr);
__int16 __stdcall rf_initval(HANDLE icdev,unsigned char _Adr,unsigned long _Value);
__int16 __stdcall rf_initval_ml(HANDLE icdev,unsigned __int16 _Value);
__int16 __stdcall rf_readval(HANDLE icdev,unsigned char _Adr,unsigned long *_Value);
__int16 __stdcall rf_readval_ml(HANDLE icdev,unsigned __int16 *_Value);
__int16 __stdcall rf_changeb3(HANDLE icdev,unsigned char _SecNr,unsigned char *_KeyA,unsigned char _B0,unsigned char _B1,unsigned char _B2,unsigned char _B3,unsigned char _Bk,unsigned char *_KeyB);
__int16 __stdcall rf_get_status(HANDLE icdev,unsigned char *_Status);
__int16 __stdcall rf_clr_control_bit(HANDLE icdev,unsigned char _b);
__int16 __stdcall rf_set_control_bit(HANDLE icdev,unsigned char _b);
__int16 __stdcall rf_reset(HANDLE icdev,unsigned __int16 _Msec);
__int16 __stdcall rf_HL_decrement(HANDLE icdev,unsigned char _Mode,unsigned char _SecNr,unsigned long _Value,unsigned long _Snr,unsigned long *_NValue,unsigned long *_NSnr);
__int16 __stdcall rf_HL_increment(HANDLE icdev,unsigned char _Mode,unsigned char _SecNr,unsigned long _Value,unsigned long _Snr,unsigned long *_NValue,unsigned long *_NSnr);
__int16 __stdcall rf_HL_write(HANDLE icdev,unsigned char _Mode,unsigned char _Adr,unsigned long *_Snr,unsigned char *_Data);
__int16 __stdcall rf_HL_writehex(HANDLE icdev,unsigned char _Mode,unsigned char _Adr,unsigned long *_Snr, char *_Data);
__int16 __stdcall rf_HL_read(HANDLE icdev,unsigned char _Mode,unsigned char _Adr,unsigned long _Snr,unsigned char *_Data,unsigned long *_NSnr);
__int16 __stdcall rf_HL_readhex(HANDLE icdev,unsigned char _Mode,unsigned char _Adr,unsigned long _Snr, char *_Data,unsigned long *_NSnr);
__int16 __stdcall rf_HL_initval(HANDLE icdev,unsigned char _Mode,unsigned char _SecNr,unsigned long _Value,unsigned long *_Snr);
__int16 __stdcall rf_beep(HANDLE icdev,unsigned short _Msec);
__int16 __stdcall rf_disp8(HANDLE icdev,__int16 pt_mode,unsigned char* disp_str);
__int16 __stdcall rf_disp(HANDLE icdev,unsigned char pt_mode,unsigned short digit);
__int16 __stdcall rf_encrypt(unsigned char *key,unsigned char *ptrSource, unsigned __int16 msgLen,unsigned char *ptrDest);
__int16 __stdcall rf_decrypt(unsigned char *key,unsigned char *ptrSource, unsigned __int16 msgLen,unsigned char *ptrDest);
__int16 __stdcall rf_HL_authentication(HANDLE icdev,unsigned char reqmode,unsigned long snr,unsigned char authmode,unsigned char secnr);
__int16 __stdcall rf_srd_eeprom(HANDLE icdev,__int16 offset,__int16 lenth,unsigned char *rec_buffer);
__int16 __stdcall rf_swr_eeprom(HANDLE icdev,__int16 offset,__int16 lenth,unsigned char* send_buffer);
__int16 __stdcall rf_srd_snr(HANDLE icdev,__int16 lenth,unsigned char *rec_buffer);
__int16 __stdcall rf_check_write(HANDLE icdev,unsigned long Snr,unsigned char authmode,unsigned char Adr,unsigned char * _data);
__int16 __stdcall rf_check_writehex(HANDLE icdev,unsigned long Snr,unsigned char authmode,unsigned char Adr, char * _data);
__int16 __stdcall rf_authentication_2(HANDLE icdev,unsigned char _Mode,unsigned char KeyNr,unsigned char Adr);
__int16 __stdcall rf_decrement_transfer(HANDLE icdev,unsigned char Adr,unsigned long _Value);
__int16 __stdcall rf_setport(HANDLE icdev,unsigned char _Byte);
__int16 __stdcall rf_getport(HANDLE icdev,unsigned char *receive_data);
__int16 __stdcall rf_gettime(HANDLE icdev,unsigned char *time);
__int16 __stdcall rf_gettimehex(HANDLE icdev,char *time);
__int16 __stdcall rf_settime(HANDLE icdev,unsigned char *time);
__int16 __stdcall rf_settimehex(HANDLE icdev,char *time);
__int16 __stdcall rf_setbright(HANDLE icdev,unsigned char bright);
__int16 __stdcall rf_ctl_mode(HANDLE icdev,unsigned char mode);
__int16 __stdcall rf_disp_mode(HANDLE icdev,unsigned char mode);
__int16 __stdcall lib_ver(unsigned char *str_ver);
__int16 __stdcall rf_comm_check(HANDLE icdev,unsigned char _Mode);
__int16 __stdcall set_host_check(unsigned char _Mode);
__int16 __stdcall set_host_485(unsigned char _Mode);
__int16 __stdcall rf_set_485(HANDLE icdev,unsigned char _Mode);
__int16 __stdcall hex_a(unsigned char *hex,char *a,unsigned char length);
__int16 __stdcall a_hex(char *a,unsigned char *hex,unsigned char len);
__int16 __stdcall rf_swr_snr(HANDLE icdev,__int16 lenth,unsigned char* send_buffer);
__int16 __stdcall rf_sam_rst(HANDLE icdev, unsigned char baud, unsigned char *samack);
__int16 __stdcall rf_sam_trn(HANDLE icdev, unsigned char *samblock,unsigned char *recv);
__int16 __stdcall rf_sam_off(HANDLE icdev);
__int16 __stdcall mf2_protocol(HANDLE icdev,unsigned __int16 timeout,unsigned char slen,char *dbuff);
__int16 __stdcall rf_cpu_rst(HANDLE icdev, unsigned char baud, unsigned char *cpuack);
__int16 __stdcall rf_cpu_trn(HANDLE icdev, unsigned char *cpublock,unsigned char *recv);
__int16 __stdcall rf_pro_rst(HANDLE icdev,unsigned char *_Data);
__int16 __stdcall rf_pro_trn(HANDLE icdev,unsigned char *problock,unsigned char *recv);
__int16 __stdcall rf_pro_halt(HANDLE icdev);
__int16 __stdcall rf_get_snr(HANDLE icdev,unsigned char *_Snr);
void __stdcall Set_Reader_Mode(unsigned char _Mode);
}