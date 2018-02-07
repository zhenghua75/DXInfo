using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace AMSApp.zhenghua.Common
{
	/// <summary>
	/// ���ݰ�ȫ������.
	/// fightop@create 2006.6.21
	/// </summary>
	public class DataSecurity
	{
		#region Ĭ����Կ��Salt���ȶ���

        /// <summary>
        /// Ĭ���㷨������Կ
        /// </summary>
        private const string ENCRYPT_KEY = "A^8&o}*z"; //������8λ

        /// <summary>
        /// Salt����
        /// </summary>
        private const int    SALT_LENGTH = 10;         //����ӽ����볤�Ȳ��������볤��

		#endregion

		#region ����ɢ�м���

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strPassword">����������</param>
        /// <returns>����</returns>
        public static string EncryptPassword(string strPassword)
        {
            return EncryptPassword(strPassword,ENCRYPT_KEY);
        }


        /// <summary>
        /// ��������(������)
        /// </summary>
        /// <param name="strPassword">����������</param>
        /// <param name="strKey">��Կ</param>
        /// <returns>����</returns>
        public static string EncryptPassword(string strPassword,string strKey) 
        {
            // ������Կ
            Byte[] byKey = System.Text.UTF8Encoding.UTF8.GetBytes(strKey);

            // ����ԭ��
            Byte[] inputByteArray = System.Text.UTF8Encoding.UTF8.GetBytes(strPassword);


            //ʹ�� HMACSHA1�㷨����
            MemoryStream ms = new  MemoryStream();
            CryptoStream cs = new CryptoStream(ms,new HMACSHA1(byKey),CryptoStreamMode.Write);
            cs.Write(inputByteArray,0,inputByteArray.Length);
            cs.FlushFinalBlock();


            return Convert.ToBase64String(ms.ToArray());
        }

		#endregion

		#region ��Salt��ɢ�м���

        /// <summary>
        /// ��Salt��������
        /// </summary>
        /// <param name="strPassword">����������</param>
        /// <returns>����</returns>
        public static string EncryptSaltPassword(string strPassword)
        {
            return EncryptSaltPassword(strPassword,ENCRYPT_KEY);
        }


        /// <summary>
        /// ��Salt��������
        /// </summary>
        /// <param name="strPassword">����������</param>
        /// <param name="strKey">��Կ</param>
        /// <returns>����</returns>
        public static string EncryptSaltPassword(string strPassword,string strKey)
        {
            // ����ԭ��
            byte[] unsaltedPassword = Convert.FromBase64String(EncryptPassword(strPassword,strKey));

            // ȡSalt
            byte[] saltValue = new byte[SALT_LENGTH];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltValue);

            // �ϲ�Slat
            byte[] rawSalted = new byte[unsaltedPassword.Length + saltValue.Length];
            unsaltedPassword.CopyTo(rawSalted,0);
            saltValue.CopyTo(rawSalted,unsaltedPassword.Length);

            // �ٴμ��ܺϲ����
            string strSaltedPassword = EncryptPassword(Convert.ToBase64String(rawSalted));
            byte[] saltedPassword    = Convert.FromBase64String(strSaltedPassword);

            // �ٴκϲ�Slat�����ܽ��
            byte[] savePassword = new byte[saltedPassword.Length + saltValue.Length];
            saltValue.CopyTo(savePassword,0);
            saltedPassword.CopyTo(savePassword,saltValue.Length);

            // ���غϲ����
            return Convert.ToBase64String(savePassword);
        }

		#endregion

		#region ��Salt��ɢ�бȽ�

        /// <summary>
        /// �Ƚϴ�Salt����
        /// </summary>
        /// <param name="strPassword">����ԭ��</param>
        /// <param name="strStoredPassword">�洢����������</param>
        /// <returns>�ȽϽ��</returns>
        public static bool CompareSaltPassword(string strPassword,string strStoredPassword)
        {
            return CompareSaltPassword(strPassword,strStoredPassword,ENCRYPT_KEY);
        }


        /// <summary>
        /// �Ƚϴ�Salt����
        /// </summary>
        /// <param name="strPassword">����ԭ��</param>
        /// <param name="strStoredPassword">�洢����������</param>
        /// <param name="strKey">��Կ</param>
        /// <returns>�ȽϽ��</returns>
        public static bool CompareSaltPassword(string strPassword,string strStoredPassword,string strKey)
        {
            byte[] storedPassword = Convert.FromBase64String(strStoredPassword);

            // �Ӵ洢���뵽Salt
            byte[] saltValue = new byte[SALT_LENGTH];
            for(int i=0; i < SALT_LENGTH; i++)
            {
                saltValue[i] = storedPassword[i];
            }

            // ���ܱȽ�����
            byte[] unsaltedPassword = Convert.FromBase64String(EncryptPassword(strPassword,strKey));

            // �ϲ�Slat
            byte[] rawSalted = new byte[unsaltedPassword.Length + saltValue.Length];
            unsaltedPassword.CopyTo(rawSalted,0);
            saltValue.CopyTo(rawSalted,unsaltedPassword.Length);

            // �ٴμ��ܺϲ����
            string strSaltedPassword = EncryptPassword(Convert.ToBase64String(rawSalted));
            byte[] saltedPassword    = Convert.FromBase64String(strSaltedPassword);

            // ���Ȳ����
            if(saltedPassword.Length != storedPassword.Length - SALT_LENGTH)
            {
                return false;
            }

            // �ȽϽ��
            bool bReturn = true;
            for(int i=0; i < saltedPassword.Length; i++)
            {
                if(saltedPassword[i] != storedPassword[SALT_LENGTH+i])
                {
                    bReturn = false;
                    break;
                }
            }

            return bReturn;
        }

		#endregion

		#region �ı��������

        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="strText">����</param>
        /// <returns>����</returns>
        public static string Encrypt(string strText)
        {
            return Encrypt(strText,ENCRYPT_KEY + ENCRYPT_KEY);
        }


        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="strText">����</param>
        /// <param name="strKey">����key������16λ</param>
        /// <returns>����</returns>
        public static string Encrypt(string strText,string strKey)
        {
            byte[] byKey=null;   
            byte[] IV= {0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};

            byKey = System.Text.Encoding.UTF8.GetBytes(strKey.Substring(0,16));
            RijndaelManaged RMCrypto = new RijndaelManaged();
            byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new  MemoryStream();
            CryptoStream cs = new  CryptoStream(ms, RMCrypto.CreateEncryptor(byKey, IV), CryptoStreamMode.Write) ;
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }


        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="strCrypto">����</param>
        /// <returns>����</returns>
        public static string Decrypt(string strCrypto)
        {
            return Decrypt(strCrypto,ENCRYPT_KEY + ENCRYPT_KEY);
        }


        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="strCrypto">����</param>
        /// <param name="strKey">����key������16λ</param>
        /// <returns>����</returns>
        public static string Decrypt(string strCrypto,string strKey)
        {
            byte[] byKey = null; 
            byte[] IV= {0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16}; 
            byte[] inputByteArray = new Byte[strCrypto.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(strKey.Substring(0,16));
            RijndaelManaged RMCrypto = new RijndaelManaged();
            inputByteArray = Convert.FromBase64String(strCrypto);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, RMCrypto.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

			System.Text.Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetString(ms.ToArray());
        }

		#endregion

		#region ��Salt�Ŀ������

		/// <summary>
		/// ��Salt����
		/// </summary>
		/// <param name="strText">����</param>
		/// <returns>��Salt����</returns>
		public static string EncryptSalt(string strText)
		{
			return EncryptSalt(strText,ENCRYPT_KEY + ENCRYPT_KEY);
		}


		/// <summary>
		/// ��Salt����
		/// </summary>
		/// <param name="strText">����</param>
		/// <param name="strKey">������Կ</param>
		/// <returns>��Salt����</returns>
		public static string EncryptSalt(string strText,string strKey)
		{
			// ԭ��
			byte[] unsaltedText = System.Text.Encoding.UTF8.GetBytes(strText);

			// ȡSalt
			byte[] saltValue = new byte[SALT_LENGTH];
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(saltValue);

			// �ϲ�Slat
			byte[] rawSalted = new byte[unsaltedText.Length + saltValue.Length];
			saltValue.CopyTo(rawSalted,0);
			unsaltedText.CopyTo(rawSalted,saltValue.Length);

			// ���ܺϲ����
			byte[] byKey=null;   
			byte[] IV= {0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};

			byKey = System.Text.Encoding.UTF8.GetBytes(strKey.Substring(0,16));
			RijndaelManaged RMCrypto = new RijndaelManaged();
			byte[] inputByteArray = rawSalted;
			MemoryStream ms = new  MemoryStream();
			CryptoStream cs = new  CryptoStream(ms, RMCrypto.CreateEncryptor(byKey, IV), CryptoStreamMode.Write) ;
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();

			return Convert.ToBase64String(ms.ToArray());
		}


		/// <summary>
		/// ��Salt����
		/// </summary>
		/// <param name="strCrypto">��Salt������</param>
		/// <returns>����</returns>
		public static string DecryptSalt(string strCrypto)
		{
			return DecryptSalt(strCrypto,ENCRYPT_KEY + ENCRYPT_KEY);
		}


		/// <summary>
		/// ��Salt����
		/// </summary>
		/// <param name="strCrypto">��Salt������</param>
		/// <param name="strKey">������Կ</param>
		/// <returns>����</returns>
		public static string DecryptSalt(string strCrypto,string strKey)
		{
			// ����
			byte[] byKey = null; 
			byte[] IV= {0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16}; 
			byte[] inputByteArray = new Byte[strCrypto.Length];

			byKey = System.Text.Encoding.UTF8.GetBytes(strKey.Substring(0,16));
			RijndaelManaged RMCrypto = new RijndaelManaged();
			inputByteArray = Convert.FromBase64String(strCrypto);
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, RMCrypto.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();

			byte[] storedSaltCrypto = ms.ToArray();


			// ȥ����ͷSalt
			if( storedSaltCrypto.Length <= SALT_LENGTH )
			{
				return	String.Empty;
			}
			byte[] storedCrypto   = new byte[storedSaltCrypto.Length - SALT_LENGTH];
			for(int i = 0; i < storedCrypto.Length; i++)
			{
				storedCrypto[i] = storedSaltCrypto[i + SALT_LENGTH];
			}

			// ����ԭ��

			System.Text.Encoding encoding = new System.Text.UTF8Encoding();
			return encoding.GetString(storedCrypto);
		}

		#endregion
	}
}
