using System;
using System.Data.SqlClient;


namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ����ݿ����ӳء�
    /// �汾��V1.0
    /// ������Fightop Lin
    /// ���ڣ�2005-12-18
    /// ������ʹ�õ�ʵ��ģʽʵ�����ӳع���
    ///       �������ӵ��ã�ConnectionPool.Instance.BConnection()��
    ///       �黹���ӵ��ã�ConnectionPool.Instance.RConnection(SqlConnection conn)
    ///
    /// Log ��1
    /// �汾��V2.0
    /// �޸ģ�Fightop Lin
    /// ���ڣ�2006-1-11
    /// ��������ҵ����۲��Ƶ����ݷ��ʲ㣬���Ӿ�̬��Ա���� BorrowConnection()
    ///                                                    ReturnConnection(SqlConnection conn)
    ///       ����ԭʵ����Ա������������Ψһʵ�����Ӷ��򻯵��ù��̡�
    ///       �������ӣ�ConnectionPool.BorrowConnection()
    ///       �黹���ӣ�ConnectionPool.ReturnConnection(SqlConnection conn)
    ///       
    /// </summary>
    public sealed class ConnectionPool : ObjectPool
    {
        #region ʹ�þ�̬��������˽��ʵ����˽�г�Ա����
        /// <summary>
        /// �����ӳ��н���һ������
        /// </summary>
        /// <returns>���Ӷ���</returns>
        public static SqlConnection BorrowConnection()
        {
            return Instance.BConnection();
        }

        /// <summary>
        /// �黹һ�����ӵ����ӳ�
        /// </summary>
        /// <param name="conn">���Ӷ���</param>
        public static void ReturnConnection(SqlConnection conn)
        {
            Instance.RConnection(conn);
        }
        #endregion

        #region ˽�г�Ա����
        // Ψһʵ��
        private static readonly ConnectionPool Instance = new ConnectionPool();

        // ���Ӵ�
        private static string _connectionString = "";
        #endregion

        #region ���û�ȡ�����ַ�������
        public  static string ConnectionString
        {
            get{ return _connectionString ; }
            //set{ _connectionString = value; }
        }
        #endregion
 
        #region ��̬������ʹ��ConnectionManager��ʼ�������ַ���
        // ���ʾ�̬ʵ��ǰ��ʼ�������ַ���
        static ConnectionPool()
        {
			//δ�������Ӵ�
//			string _connectionStringSet = AMSApp.zhenghua.Common.ConfigAdapter.GetConfigNote("SetConnectionString").Trim();
//			if(String.Empty != _connectionStringSet)
//			{
//				_connectionStringSet = AMSApp.zhenghua.Common.DataSecurity.Encrypt(_connectionStringSet);
//				AMSApp.zhenghua.Common.ConfigAdapter.SetConfigNote("ConnectionString",_connectionStringSet);
//                AMSApp.zhenghua.Common.ConfigAdapter.SetConfigNote("SetConnectionString", String.Empty);
//			}
			
            _connectionString = AMSApp.zhenghua.Common.ConfigAdapter.GetConfigNote("ConnectionString");
			//_connectionString = AMSApp.zhenghua.Common.DataSecurity.Decrypt(_connectionString);
        }
        #endregion

        #region ˽�й�������ֹ���ⲿ���ɶ���ʵ��
        private ConnectionPool() { }
        #endregion

        #region ʵ�ֻ���ĳ��󷽷�����������֤��������ݿ�����
        // ʵ��Create�������µ����Ӷ���
        protected override object Create()
        {
            SqlConnection temp = new SqlConnection(_connectionString);
            temp.Open();

            return(temp);
        }


        // ʵ��Validate����֤���Ӷ����Ƿ���Ч
        protected override bool Validate(object o)
        {
            try
            {
                SqlConnection temp = (SqlConnection)o;

                return(
                    !((temp.State.Equals(System.Data.ConnectionState.Closed)))
                    );
            }
            catch(SqlException)
            {
                return false;
            }
        }

        // ʵ��Expire���رղ�������Ӷ���
        protected override void Expire(object o)
        {
            try
            {
                SqlConnection temp = (SqlConnection)o;
                temp.Close();
                temp.Dispose();
            }
            catch(SqlException){}
        }
        #endregion

        #region ������黹����(���������ʵ�ַ���)
        // ����һ������
        private SqlConnection BConnection()
        {
            try
            {
                return((SqlConnection)base.GetObjectFromPool());
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// �黹һ�����ӵ����ӳ�
        /// </summary>
        /// <param name="conn">���Ӷ���</param>
        private void RConnection(SqlConnection conn)
        {
            base.ReturnObjectToPool(conn);
        
        }
        #endregion

    }
}
