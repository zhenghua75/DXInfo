using System;
using System.Data.SqlClient;


namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：数据库连接池。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2005-12-18
    /// 描述：使用单实例模式实现连接池管理。
    ///       借用连接调用：ConnectionPool.Instance.BConnection()。
    ///       归还连接调用：ConnectionPool.Instance.RConnection(SqlConnection conn)
    ///
    /// Log ：1
    /// 版本：V2.0
    /// 修改：Fightop Lin
    /// 日期：2006-1-11
    /// 描述：从业务外观层移到数据访问层，增加静态成员方法 BorrowConnection()
    ///                                                    ReturnConnection(SqlConnection conn)
    ///       公开原实例成员方法，并隐藏唯一实例，从而简化调用过程。
    ///       借用连接：ConnectionPool.BorrowConnection()
    ///       归还连接：ConnectionPool.ReturnConnection(SqlConnection conn)
    ///       
    /// </summary>
    public sealed class ConnectionPool : ObjectPool
    {
        #region 使用静态方法公开私有实例的私有成员方法
        /// <summary>
        /// 从连接池中借用一个连接
        /// </summary>
        /// <returns>连接对象</returns>
        public static SqlConnection BorrowConnection()
        {
            return Instance.BConnection();
        }

        /// <summary>
        /// 归还一个连接到连接池
        /// </summary>
        /// <param name="conn">连接对象</param>
        public static void ReturnConnection(SqlConnection conn)
        {
            Instance.RConnection(conn);
        }
        #endregion

        #region 私有成员变量
        // 唯一实例
        private static readonly ConnectionPool Instance = new ConnectionPool();

        // 连接串
        private static string _connectionString = "";
        #endregion

        #region 设置获取连接字符串属性
        public  static string ConnectionString
        {
            get{ return _connectionString ; }
            //set{ _connectionString = value; }
        }
        #endregion
 
        #region 静态构造器使用ConnectionManager初始化连接字符串
        // 访问静态实例前初始化连接字符串
        static ConnectionPool()
        {
			//未加密连接串
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

        #region 私有构造器防止从外部生成对象实例
        private ConnectionPool() { }
        #endregion

        #region 实现基类的抽象方法：创建、验证、清除数据库连接
        // 实现Create，创建新的连接对象
        protected override object Create()
        {
            SqlConnection temp = new SqlConnection(_connectionString);
            temp.Open();

            return(temp);
        }


        // 实现Validate，验证连接对象是否有效
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

        // 实现Expire，关闭并清除连接对象
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

        #region 借出、归还连接(适配基类已实现方法)
        // 借用一个连接
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
        /// 归还一个连接到连接池
        /// </summary>
        /// <param name="conn">连接对象</param>
        private void RConnection(SqlConnection conn)
        {
            base.ReturnObjectToPool(conn);
        
        }
        #endregion

    }
}
