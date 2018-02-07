using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace AMSApp.zhenghua.Common
{
	/// <summary>
	/// 数据安全访问类.
	/// fightop@create 2006.6.21
	/// </summary>
	public class DataSecurity
	{
		#region 默认密钥及Salt长度定义

        /// <summary>
        /// 默认算法加密密钥
        /// </summary>
        private const string ENCRYPT_KEY = "A^8&o}*z"; //必须是8位

        /// <summary>
        /// Salt长度
        /// </summary>
        private const int    SALT_LENGTH = 10;         //建议接近密码长度不等于密码长度

		#endregion

		#region 密码散列加密

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="strPassword">待加密明文</param>
        /// <returns>密文</returns>
        public static string EncryptPassword(string strPassword)
        {
            return EncryptPassword(strPassword,ENCRYPT_KEY);
        }


        /// <summary>
        /// 加密密码(不可逆)
        /// </summary>
        /// <param name="strPassword">待加密明文</param>
        /// <param name="strKey">密钥</param>
        /// <returns>密文</returns>
        public static string EncryptPassword(string strPassword,string strKey) 
        {
            // 加密密钥
            Byte[] byKey = System.Text.UTF8Encoding.UTF8.GetBytes(strKey);

            // 加密原文
            Byte[] inputByteArray = System.Text.UTF8Encoding.UTF8.GetBytes(strPassword);


            //使用 HMACSHA1算法加密
            MemoryStream ms = new  MemoryStream();
            CryptoStream cs = new CryptoStream(ms,new HMACSHA1(byKey),CryptoStreamMode.Write);
            cs.Write(inputByteArray,0,inputByteArray.Length);
            cs.FlushFinalBlock();


            return Convert.ToBase64String(ms.ToArray());
        }

		#endregion

		#region 带Salt的散列加密

        /// <summary>
        /// 带Salt加密密码
        /// </summary>
        /// <param name="strPassword">待加密明文</param>
        /// <returns>密文</returns>
        public static string EncryptSaltPassword(string strPassword)
        {
            return EncryptSaltPassword(strPassword,ENCRYPT_KEY);
        }


        /// <summary>
        /// 带Salt加密密码
        /// </summary>
        /// <param name="strPassword">待加密明文</param>
        /// <param name="strKey">密钥</param>
        /// <returns>密文</returns>
        public static string EncryptSaltPassword(string strPassword,string strKey)
        {
            // 加密原文
            byte[] unsaltedPassword = Convert.FromBase64String(EncryptPassword(strPassword,strKey));

            // 取Salt
            byte[] saltValue = new byte[SALT_LENGTH];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltValue);

            // 合并Slat
            byte[] rawSalted = new byte[unsaltedPassword.Length + saltValue.Length];
            unsaltedPassword.CopyTo(rawSalted,0);
            saltValue.CopyTo(rawSalted,unsaltedPassword.Length);

            // 再次加密合并结果
            string strSaltedPassword = EncryptPassword(Convert.ToBase64String(rawSalted));
            byte[] saltedPassword    = Convert.FromBase64String(strSaltedPassword);

            // 再次合并Slat到加密结果
            byte[] savePassword = new byte[saltedPassword.Length + saltValue.Length];
            saltValue.CopyTo(savePassword,0);
            saltedPassword.CopyTo(savePassword,saltValue.Length);

            // 返回合并结果
            return Convert.ToBase64String(savePassword);
        }

		#endregion

		#region 带Salt的散列比较

        /// <summary>
        /// 比较带Salt密码
        /// </summary>
        /// <param name="strPassword">密码原文</param>
        /// <param name="strStoredPassword">存储的密码密文</param>
        /// <returns>比较结果</returns>
        public static bool CompareSaltPassword(string strPassword,string strStoredPassword)
        {
            return CompareSaltPassword(strPassword,strStoredPassword,ENCRYPT_KEY);
        }


        /// <summary>
        /// 比较带Salt密码
        /// </summary>
        /// <param name="strPassword">密码原文</param>
        /// <param name="strStoredPassword">存储的密码密文</param>
        /// <param name="strKey">密钥</param>
        /// <returns>比较结果</returns>
        public static bool CompareSaltPassword(string strPassword,string strStoredPassword,string strKey)
        {
            byte[] storedPassword = Convert.FromBase64String(strStoredPassword);

            // 从存储密码到Salt
            byte[] saltValue = new byte[SALT_LENGTH];
            for(int i=0; i < SALT_LENGTH; i++)
            {
                saltValue[i] = storedPassword[i];
            }

            // 加密比较密码
            byte[] unsaltedPassword = Convert.FromBase64String(EncryptPassword(strPassword,strKey));

            // 合并Slat
            byte[] rawSalted = new byte[unsaltedPassword.Length + saltValue.Length];
            unsaltedPassword.CopyTo(rawSalted,0);
            saltValue.CopyTo(rawSalted,unsaltedPassword.Length);

            // 再次加密合并结果
            string strSaltedPassword = EncryptPassword(Convert.ToBase64String(rawSalted));
            byte[] saltedPassword    = Convert.FromBase64String(strSaltedPassword);

            // 长度不相等
            if(saltedPassword.Length != storedPassword.Length - SALT_LENGTH)
            {
                return false;
            }

            // 比较结果
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

		#region 文本可逆加密

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="strText">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string strText)
        {
            return Encrypt(strText,ENCRYPT_KEY + ENCRYPT_KEY);
        }


        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="strText">明文</param>
        /// <param name="strKey">加密key必须是16位</param>
        /// <returns>密文</returns>
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
        /// 解密字符串
        /// </summary>
        /// <param name="strCrypto">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string strCrypto)
        {
            return Decrypt(strCrypto,ENCRYPT_KEY + ENCRYPT_KEY);
        }


        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="strCrypto">密文</param>
        /// <param name="strKey">解密key必须是16位</param>
        /// <returns>明文</returns>
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

		#region 带Salt的可逆加密

		/// <summary>
		/// 带Salt加密
		/// </summary>
		/// <param name="strText">明文</param>
		/// <returns>带Salt密文</returns>
		public static string EncryptSalt(string strText)
		{
			return EncryptSalt(strText,ENCRYPT_KEY + ENCRYPT_KEY);
		}


		/// <summary>
		/// 带Salt加密
		/// </summary>
		/// <param name="strText">明文</param>
		/// <param name="strKey">加密密钥</param>
		/// <returns>带Salt密文</returns>
		public static string EncryptSalt(string strText,string strKey)
		{
			// 原文
			byte[] unsaltedText = System.Text.Encoding.UTF8.GetBytes(strText);

			// 取Salt
			byte[] saltValue = new byte[SALT_LENGTH];
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(saltValue);

			// 合并Slat
			byte[] rawSalted = new byte[unsaltedText.Length + saltValue.Length];
			saltValue.CopyTo(rawSalted,0);
			unsaltedText.CopyTo(rawSalted,saltValue.Length);

			// 加密合并结果
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
		/// 带Salt解密
		/// </summary>
		/// <param name="strCrypto">带Salt的密文</param>
		/// <returns>明文</returns>
		public static string DecryptSalt(string strCrypto)
		{
			return DecryptSalt(strCrypto,ENCRYPT_KEY + ENCRYPT_KEY);
		}


		/// <summary>
		/// 带Salt解密
		/// </summary>
		/// <param name="strCrypto">带Salt的密文</param>
		/// <param name="strKey">解密密钥</param>
		/// <returns>明文</returns>
		public static string DecryptSalt(string strCrypto,string strKey)
		{
			// 解密
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


			// 去除开头Salt
			if( storedSaltCrypto.Length <= SALT_LENGTH )
			{
				return	String.Empty;
			}
			byte[] storedCrypto   = new byte[storedSaltCrypto.Length - SALT_LENGTH];
			for(int i = 0; i < storedCrypto.Length; i++)
			{
				storedCrypto[i] = storedSaltCrypto[i + SALT_LENGTH];
			}

			// 返回原文

			System.Text.Encoding encoding = new System.Text.UTF8Encoding();
			return encoding.GetString(storedCrypto);
		}

		#endregion
	}
}
