using System;
using System.Reflection;
using System.Diagnostics;
using System.Data;
using System.Collections;
using System.IO;
using System.Xml;

namespace AMSApp.zhenghua.EntityBase
{
	/// <summary>
    /// 实体对像基类，提供数据实体对像的共有访问方法。
    /// fightop@create 2006.6.20
	/// </summary>
	[Serializable]
	public abstract class EntityObjectBase
	{
		private DataTable table;

		#region 构造器
		/// <summary>
        /// 构造器
		/// </summary>
        public EntityObjectBase() 
        { 
            SetOriginalValue(); 
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="row">数据行</param>
        public EntityObjectBase(DataRow row)
        {
            FromRow(row);
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="table">数据表</param>
		public EntityObjectBase(DataTable table)
		{
			FromTable(table);
		}

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="strXML">数据集的XML表示</param>
        public EntityObjectBase(string strXML)
		{
            FromXml(strXML);
		}
		#endregion

        #region 公开成员方法

        #region 判断是否相等

        /// <summary>
        /// 判断相等
        /// </summary>
        /// <param name="objEntity">被比较实体</param>
        /// <returns>是否相等</returns>
        public bool Equals(EntityObjectBase objEntity)
        {
            if(objEntity.GetType() != this.GetType())
            {
                return false;
            }

            TableAttributes taEntity = objEntity.GetEntityColumns();
            TableAttributes taThis   = this.GetEntityColumns();

            bool bEquals = true;
            for(int i = 0; i < taEntity.Columns.Count; i++)
            {
                ColumnAttributes caEntity  = taEntity.Columns[i] as ColumnAttributes;
                ColumnAttributes caThis    = taThis.Columns[i]   as ColumnAttributes;
                if(caEntity.Value.ToString() != caThis.Value.ToString())
                {
                    bEquals = false;
                    break;
                }

            }

            return bEquals;
        }

        /// <summary>
        /// 判断本实例更变的值与被比实体对应值是否相等
        /// </summary>
        /// <param name="objEntity">被比较实体</param>
        /// <returns>是否相等</returns>
        public bool EqualsModify(EntityObjectBase objEntity)
        {
            if (objEntity.GetType() != this.GetType())
            {
                return false;
            }

            TableAttributes taEntity = objEntity.GetEntityColumns();
            TableAttributes taThis = this.GetEntityColumns();

            bool bEquals = true;
            for (int i = 0; i < taEntity.Columns.Count; i++)
            {
                ColumnAttributes caEntity = taEntity.Columns[i] as ColumnAttributes;
                ColumnAttributes caThis = taThis.Columns[i] as ColumnAttributes;
                if (caThis.IsModify && (caEntity.Value.ToString() != caThis.Value.ToString()))
                {
                    bEquals = false;
                    break;
                }

            }

            return bEquals;
        }

        /// <summary>
        /// 判断本实例更变的原始值与被比实体对应当前值是否相等
        /// </summary>
        /// <param name="objEntity">被比较实体</param>
        /// <returns>是否相等</returns>
        public bool EqualsModifyOriginal(EntityObjectBase objEntity)
        {
            if (objEntity.GetType() != this.GetType())
            {
                return false;
            }

            TableAttributes taEntity = objEntity.GetEntityColumns();
            TableAttributes taThis = this.GetEntityColumns();

            bool bEquals = true;
            for (int i = 0; i < taEntity.Columns.Count; i++)
            {
                ColumnAttributes caEntity = taEntity.Columns[i] as ColumnAttributes;
                ColumnAttributes caThis = taThis.Columns[i] as ColumnAttributes;
                if (caThis.IsModify && (caEntity.Value.ToString() != caThis.OriginalValue.ToString()))
                {
                    bEquals = false;
                    break;
                }

            }

            return bEquals;
        }

        #endregion

        #region 取字列表

        public TableAttributes GetEntityColumns()
        {
            return GetEntityColumns(this);
        }

        /// <summary>
        /// 取实体映射的数据库表信息
        /// </summary>
        /// <param name="objEntity">实体对像</param>
        /// <returns>映射表信息</returns>
        public static TableAttributes GetEntityColumns(EntityObjectBase objEntity)
        {
            TableAttributes taEntity = new TableAttributes(); 
            
            #region 取表名称

            object[] objTable = objEntity.GetType().GetCustomAttributes(typeof(TableMapping), false);
            if(objTable.Length > 0)
            {
                if (objTable[0] is TableMapping)
                {
                    TableMapping tmCurrent = objTable[0] as TableMapping;
                    taEntity.TableName = tmCurrent.TableName;
                }
            }

            #endregion

            #region 取字段名称

            foreach (PropertyInfo pi in objEntity.GetType().GetProperties())
            { // 根据属性映射创建表模式并记录属性值

                object[] objColumn = pi.GetCustomAttributes(typeof(ColumnMapping), false);
                if (objColumn.Length > 0)
                {
                    if (objColumn[0] is ColumnMapping)
                    {
                        if (pi.CanRead)
                        {
                            ColumnMapping attrCurrent = objColumn[0] as ColumnMapping;
                            ColumnAttributes caCurrent = new ColumnAttributes();

                            caCurrent.ColumnName      = attrCurrent.ColumnName;
                            caCurrent.IsIdentity      = attrCurrent.IsIdentity;
                            caCurrent.IsPrimaryKey    = attrCurrent.IsPrimaryKey;
                            caCurrent.IsVersionNumber = attrCurrent.IsVersionNumber;
                            caCurrent.Value           = pi.GetValue(objEntity, null);
                            caCurrent.OriginalValue   = objEntity.table.Rows[0][attrCurrent.ColumnName];
                            if (caCurrent.OriginalValue.ToString() != caCurrent.Value.ToString())
                            {
                                caCurrent.IsModify = true;
                            }

                            taEntity.Columns.Add(caCurrent);
                        }
                    }
                }
            }

            #endregion

            return taEntity;
        }

        #endregion

        #region 同步修改值
        /// <summary>
        /// 将指定实体修改过的值同步到本实例
        /// </summary>
        /// <param name="objEntity">同步实体</param>
        public void SynchronizeModifyValue(EntityObjectBase objEntity)
        {
            TableAttributes taEntity = objEntity.GetEntityColumns();
            foreach (ColumnAttributes caCurrent in taEntity.Columns)
            {
                if (true == caCurrent.IsModify)
                {
                    SetMappingValue(this, caCurrent.ColumnName, caCurrent.Value);
                }
            }
        }
        #endregion

        #region 同步设置主键及外键值

        /// <summary>
		/// 同步设置主键及外键值
		/// </summary>
        /// <param name="objForgetKeyEntity">从表实体</param>
		/// <param name="objValue">主键值</param>
        public void SetForgetKeyValue(EntityObjectBase objForgetKeyEntity, object objValue)
		{
            Type type = this.GetType();
			string strKeyCoumnName = "";
			foreach(PropertyInfo pi in  type.GetProperties())
			{
                object[] obj = pi.GetCustomAttributes(typeof(ColumnMapping), false);
				if(obj.Length>0)
				{
                    if (obj[0] is ColumnMapping)
                    {
                        ColumnMapping tmAtrr = obj[0] as ColumnMapping;
                        if (true == tmAtrr.IsPrimaryKey)
                        {
                            pi.SetValue(this, Convert.ChangeType(objValue, pi.PropertyType), null);
							strKeyCoumnName = tmAtrr.ColumnName;
                            break;
                        }
                    }
				}
			}

            type = objForgetKeyEntity.GetType();
			foreach(PropertyInfo pi in  type.GetProperties())
			{
				object[] obj = pi.GetCustomAttributes(typeof(ColumnMapping), false);
				if(obj.Length>0)
				{
					if (obj[0] is ColumnMapping)
					{
						ColumnMapping tmAtrr = obj[0] as ColumnMapping;
						if (strKeyCoumnName == tmAtrr.ColumnName)
						{
                            pi.SetValue(objForgetKeyEntity, Convert.ChangeType(objValue, pi.PropertyType), null);
							break;
						}
					}
				}
			}
		}

		#endregion

		#region 按字段设置值

		/// <summary>
		/// 按字段设置值
		/// </summary>
		/// <param name="strColumnName">字段名称</param>
		/// <param name="objValue">值</param>
		public void SetValueByColumn(string strColumnName, object objValue)
		{
			Type type = this.GetType();
			foreach(PropertyInfo pi in  type.GetProperties())
			{
				object[] obj = pi.GetCustomAttributes(typeof(ColumnMapping), false);
				if(obj.Length>0)
				{
					if (obj[0] is ColumnMapping)
					{
						ColumnMapping tmAtrr = obj[0] as ColumnMapping;
						if (strColumnName == tmAtrr.ColumnName)
						{
							pi.SetValue(this, Convert.ChangeType(objValue, pi.PropertyType), null);
							break;
						}
					}
				}
			}
		}

		#endregion

		#region 按字段读取值

		/// <summary>
		/// 按字段读取值
		/// </summary>
		/// <param name="strColumnName">字段名称</param>
		/// <returns>值</returns>
		public object GetValueByColumn(string strColumnName)
		{
			object objReturn = null;

			Type type = this.GetType();
			foreach(PropertyInfo pi in  type.GetProperties())
			{
				object[] obj = pi.GetCustomAttributes(typeof(ColumnMapping), false);
				if(obj.Length>0)
				{
					if (obj[0] is ColumnMapping)
					{
						ColumnMapping tmAtrr = obj[0] as ColumnMapping;
						if (strColumnName == tmAtrr.ColumnName)
						{
							objReturn = pi.GetValue(this, null);
							break;
						}
					}
				}
			}

			return objReturn;
		}

		#endregion

        #region 设置原始值

        /// <summary>
        /// 设置原始值
        /// </summary>
        public void SetOriginalValue()
        {
            SetOriginalValue(this);
        }

        /// <summary>
        /// 设置原始值
        /// </summary>
        /// <param name="objEntity">实体对像</param>
        public static void SetOriginalValue(EntityObjectBase objEntity)
        {
            objEntity.table = objEntity.ToTable();
        }

        #endregion

        #region 拷贝实体

        public EntityObjectBase Copy()
        {
            return Copy(this);
        }

        public static EntityObjectBase Copy(EntityObjectBase objEntity)
        {
            //生成拷贝实例
            Type type = objEntity.GetType();
            EntityObjectBase objCopy = (EntityObjectBase)Activator.CreateInstance(type);

            //拷贝属性值
            objCopy.FromTable(objEntity.ToTable());

            //拷贝原始值
            objCopy.table = objEntity.table.Copy();

            return objCopy;
        }

        #endregion

        #endregion

        #region 公开的转换成员方法

        /// <summary>
        /// 从数据行初始化对象
        /// </summary>
        /// <returns></returns>
        public void FromRow(DataRow row)
        {
            table = new DataTable();
            foreach(DataColumn column in row.Table.Columns)
            {
                table.Columns.Add(column.ColumnName);
            }
            this.table.Rows.Clear();
            this.table.Rows.Add(row.ItemArray);
            this.Init();
        }

		/// <summary>
		/// 从数据行初始化对象
		/// </summary>
		/// <returns></returns>
		public void ModifyFromRow(DataRow row)
		{
			DataTable tblOriginal = table;
			table = new DataTable();
			foreach(DataColumn column in row.Table.Columns)
			{
				table.Columns.Add(column.ColumnName);
			}
			table.Rows.Add(row.ItemArray);
			ModifyInit(tblOriginal);
		}

        /// <summary>
        ///  从数据表初始化对象
        /// </summary>
        /// <returns></returns>
        public void FromTable(DataTable table)
        {
            this.table = table.Copy();
            this.Init();
        }

        /// <summary>
        /// 从XML初始化对象
        /// </summary>
        /// <returns></returns>
        public void FromXml(string strXML)
        {
            StringReader  reader  = new StringReader(strXML);
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if(ds != null && ds.Tables.Count>0)
            {
                this.table = ds.Tables[0];
                Init();
            }
        }

        #endregion

        #region 公开的转换成员方法

        /// <summary>
        /// 把对像转换为数据行
        /// </summary>
        /// <returns></returns>
        public DataRow ToRow()
        {
            DataTable tblTemp = MappingToTable(this);

            return tblTemp.Rows[0];
        }

		/// <summary>
		///  把对像转换成数据表
		/// </summary>
		/// <returns></returns>
		public DataTable ToTable()
		{
			return MappingToTable(this);
		}

		/// <summary>
        /// 把对像转换成数据集的XML表示
		/// </summary>
		/// <returns></returns>
		public string ToXml()
		{
			DataSet ds = new DataSet();
			ds.Tables.Add(MappingToTable(this));

			return ds.GetXml();
        }

        #endregion

        #region 私有构造初始化方法

        private void Init()
		{
			if(this.table != null && this.table.Rows.Count > 0)
			{
				MappingToObject(this);
			}

            table.Clear();
            SetOriginalValue();
        }

		private void ModifyInit(DataTable tblOriginal)
		{
			if(this.table != null && this.table.Rows.Count > 0)
			{
				MappingToObject(this);
			}

			table.Clear();
			table = tblOriginal;
		}

        #endregion

        #region  实体对像映射到数据表的私有方法

        /// <summary>
        /// 把传输对像转换成数据表
		/// </summary>
        /// <param name="objTranfser">数据传输对像</param>
		/// <returns>转换得到的表</returns>
        private DataTable MappingToTable(EntityObjectBase objTranfser)
        {
            ArrayList alRow = new ArrayList();

            DataTable dt = new DataTable(objTranfser.GetType().Name);
            foreach (PropertyInfo pi in objTranfser.GetType().GetProperties())
            { // 根据属性映射创建表模式并记录属性值

                object[] obj = pi.GetCustomAttributes(typeof(ColumnMapping), false);
                if (obj.Length > 0)
                {
                    if (obj[0] is ColumnMapping)
                    {
                        ColumnMapping tmAtrr = obj[0] as ColumnMapping;

                        if (pi.CanRead && !dt.Columns.Contains(tmAtrr.ColumnName))
                        {
                            dt.Columns.Add(tmAtrr.ColumnName);         // 创建表例

                            alRow.Add(pi.GetValue(objTranfser, null)); // 记录属性值
                        }
                    }
                }
            }

            dt.Rows.Add(alRow.ToArray());

            return dt;
        }

		#endregion

		#region 数据行映射到实体对像的私有方法

		/// <summary>
		/// 数据行与实体对像之间的映射
		/// </summary>
        /// <param name="objTranfser">被映射的传输对像</param>
        private void MappingToObject(EntityObjectBase objTranfser)
        {
            DataRow row = this.table.Rows[0];
            foreach (DataColumn col in this.table.Columns)
            {
				if (row[col] != System.DBNull.Value)
				{
					this.SetMappingValue(objTranfser, col.ColumnName, row[col]);
				}
            }
        }

		/// <summary>
		/// 设置指定属性的值
		/// </summary>
        /// <param name="objTranfser">数据传输对像</param>
        /// <param name="strSrcMappingName">数据字段名称</param>
        /// <param name="objValue">设定的值</param>
        private void SetMappingValue(object objTranfser, string strSrcMappingName, object objValue)
		{
            Type type = objTranfser.GetType();
			foreach(PropertyInfo pi in  type.GetProperties())
			{
                object[] obj = pi.GetCustomAttributes(typeof(ColumnMapping), false);
				if(obj.Length>0)
				{
                    if (obj[0] is ColumnMapping)
                    {
                        ColumnMapping tmAtrr = obj[0] as ColumnMapping;
                        if (strSrcMappingName == tmAtrr.ColumnName)
                        {
                            pi.SetValue(objTranfser, Convert.ChangeType(objValue, pi.PropertyType), null);
                            break;
                        }
                    }
				}
			}
		}

        public void SetMappingValue(string strSrcMappingName, object objValue)
        {
            SetMappingValue(this, strSrcMappingName, objValue);
        }
        
		#endregion

        #region 被覆盖的成员方法

        public override string ToString()
        {
            string strEntity = String.Empty;

            Type type = this.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                object[] obj = pi.GetCustomAttributes(typeof(ColumnMapping), false);
                if (obj.Length > 0)
                {
                    if (obj[0] is ColumnMapping)
                    {
                        ColumnMapping tmAtrr = obj[0] as ColumnMapping;

                        strEntity += tmAtrr.ColumnName + " = "  +  pi.GetValue(this, null).ToString() + " , ";
                    }
                }
            }

            return strEntity;
        }

        #endregion
    }
}
