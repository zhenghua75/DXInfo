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
    /// ʵ�������࣬�ṩ����ʵ�����Ĺ��з��ʷ�����
    /// fightop@create 2006.6.20
	/// </summary>
	[Serializable]
	public abstract class EntityObjectBase
	{
		private DataTable table;

		#region ������
		/// <summary>
        /// ������
		/// </summary>
        public EntityObjectBase() 
        { 
            SetOriginalValue(); 
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="row">������</param>
        public EntityObjectBase(DataRow row)
        {
            FromRow(row);
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="table">���ݱ�</param>
		public EntityObjectBase(DataTable table)
		{
			FromTable(table);
		}

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="strXML">���ݼ���XML��ʾ</param>
        public EntityObjectBase(string strXML)
		{
            FromXml(strXML);
		}
		#endregion

        #region ������Ա����

        #region �ж��Ƿ����

        /// <summary>
        /// �ж����
        /// </summary>
        /// <param name="objEntity">���Ƚ�ʵ��</param>
        /// <returns>�Ƿ����</returns>
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
        /// �жϱ�ʵ�������ֵ�뱻��ʵ���Ӧֵ�Ƿ����
        /// </summary>
        /// <param name="objEntity">���Ƚ�ʵ��</param>
        /// <returns>�Ƿ����</returns>
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
        /// �жϱ�ʵ�������ԭʼֵ�뱻��ʵ���Ӧ��ǰֵ�Ƿ����
        /// </summary>
        /// <param name="objEntity">���Ƚ�ʵ��</param>
        /// <returns>�Ƿ����</returns>
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

        #region ȡ���б�

        public TableAttributes GetEntityColumns()
        {
            return GetEntityColumns(this);
        }

        /// <summary>
        /// ȡʵ��ӳ������ݿ����Ϣ
        /// </summary>
        /// <param name="objEntity">ʵ�����</param>
        /// <returns>ӳ�����Ϣ</returns>
        public static TableAttributes GetEntityColumns(EntityObjectBase objEntity)
        {
            TableAttributes taEntity = new TableAttributes(); 
            
            #region ȡ������

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

            #region ȡ�ֶ�����

            foreach (PropertyInfo pi in objEntity.GetType().GetProperties())
            { // ��������ӳ�䴴����ģʽ����¼����ֵ

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

        #region ͬ���޸�ֵ
        /// <summary>
        /// ��ָ��ʵ���޸Ĺ���ֵͬ������ʵ��
        /// </summary>
        /// <param name="objEntity">ͬ��ʵ��</param>
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

        #region ͬ���������������ֵ

        /// <summary>
		/// ͬ���������������ֵ
		/// </summary>
        /// <param name="objForgetKeyEntity">�ӱ�ʵ��</param>
		/// <param name="objValue">����ֵ</param>
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

		#region ���ֶ�����ֵ

		/// <summary>
		/// ���ֶ�����ֵ
		/// </summary>
		/// <param name="strColumnName">�ֶ�����</param>
		/// <param name="objValue">ֵ</param>
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

		#region ���ֶζ�ȡֵ

		/// <summary>
		/// ���ֶζ�ȡֵ
		/// </summary>
		/// <param name="strColumnName">�ֶ�����</param>
		/// <returns>ֵ</returns>
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

        #region ����ԭʼֵ

        /// <summary>
        /// ����ԭʼֵ
        /// </summary>
        public void SetOriginalValue()
        {
            SetOriginalValue(this);
        }

        /// <summary>
        /// ����ԭʼֵ
        /// </summary>
        /// <param name="objEntity">ʵ�����</param>
        public static void SetOriginalValue(EntityObjectBase objEntity)
        {
            objEntity.table = objEntity.ToTable();
        }

        #endregion

        #region ����ʵ��

        public EntityObjectBase Copy()
        {
            return Copy(this);
        }

        public static EntityObjectBase Copy(EntityObjectBase objEntity)
        {
            //���ɿ���ʵ��
            Type type = objEntity.GetType();
            EntityObjectBase objCopy = (EntityObjectBase)Activator.CreateInstance(type);

            //��������ֵ
            objCopy.FromTable(objEntity.ToTable());

            //����ԭʼֵ
            objCopy.table = objEntity.table.Copy();

            return objCopy;
        }

        #endregion

        #endregion

        #region ������ת����Ա����

        /// <summary>
        /// �������г�ʼ������
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
		/// �������г�ʼ������
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
        ///  �����ݱ��ʼ������
        /// </summary>
        /// <returns></returns>
        public void FromTable(DataTable table)
        {
            this.table = table.Copy();
            this.Init();
        }

        /// <summary>
        /// ��XML��ʼ������
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

        #region ������ת����Ա����

        /// <summary>
        /// �Ѷ���ת��Ϊ������
        /// </summary>
        /// <returns></returns>
        public DataRow ToRow()
        {
            DataTable tblTemp = MappingToTable(this);

            return tblTemp.Rows[0];
        }

		/// <summary>
		///  �Ѷ���ת�������ݱ�
		/// </summary>
		/// <returns></returns>
		public DataTable ToTable()
		{
			return MappingToTable(this);
		}

		/// <summary>
        /// �Ѷ���ת�������ݼ���XML��ʾ
		/// </summary>
		/// <returns></returns>
		public string ToXml()
		{
			DataSet ds = new DataSet();
			ds.Tables.Add(MappingToTable(this));

			return ds.GetXml();
        }

        #endregion

        #region ˽�й����ʼ������

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

        #region  ʵ�����ӳ�䵽���ݱ��˽�з���

        /// <summary>
        /// �Ѵ������ת�������ݱ�
		/// </summary>
        /// <param name="objTranfser">���ݴ������</param>
		/// <returns>ת���õ��ı�</returns>
        private DataTable MappingToTable(EntityObjectBase objTranfser)
        {
            ArrayList alRow = new ArrayList();

            DataTable dt = new DataTable(objTranfser.GetType().Name);
            foreach (PropertyInfo pi in objTranfser.GetType().GetProperties())
            { // ��������ӳ�䴴����ģʽ����¼����ֵ

                object[] obj = pi.GetCustomAttributes(typeof(ColumnMapping), false);
                if (obj.Length > 0)
                {
                    if (obj[0] is ColumnMapping)
                    {
                        ColumnMapping tmAtrr = obj[0] as ColumnMapping;

                        if (pi.CanRead && !dt.Columns.Contains(tmAtrr.ColumnName))
                        {
                            dt.Columns.Add(tmAtrr.ColumnName);         // ��������

                            alRow.Add(pi.GetValue(objTranfser, null)); // ��¼����ֵ
                        }
                    }
                }
            }

            dt.Rows.Add(alRow.ToArray());

            return dt;
        }

		#endregion

		#region ������ӳ�䵽ʵ������˽�з���

		/// <summary>
		/// ��������ʵ�����֮���ӳ��
		/// </summary>
        /// <param name="objTranfser">��ӳ��Ĵ������</param>
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
		/// ����ָ�����Ե�ֵ
		/// </summary>
        /// <param name="objTranfser">���ݴ������</param>
        /// <param name="strSrcMappingName">�����ֶ�����</param>
        /// <param name="objValue">�趨��ֵ</param>
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

        #region �����ǵĳ�Ա����

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
