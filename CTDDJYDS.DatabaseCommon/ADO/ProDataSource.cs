using System;
using System.Data;
using System.Xml;
using System.Collections.Generic;

namespace CTDDJYDS.DatabaseCommon
{
	/// <summary>
	/// �������Ӷ�����
	/// </summary>
	public abstract class ProDataSource : IDisposable 
	{        
		#region IDisposable ��Ա
		public virtual void Dispose(bool disposing)
		{
			if(!disposing)
				return;
			
			if(this.Connection != null)			
				this.Connection.Close();
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this );
		}
		#endregion		
		/// <summary>
		/// �õ����ݿ��������Ϣ
		/// </summary>
		public abstract IDbConnection Connection
		{
			get;
		}
		/// <summary>
		/// �����������Ӳ���
		/// </summary>
		public abstract IDbTransaction Transaction
		{
			get;			
		}
        
        public abstract  ConnectionState State
        {
            get;
        }
		public abstract void Open();		

		public abstract void Close();

		public abstract void BeginTransaction();

		public abstract void CommitTransaction();

		public abstract void RollBackTransaction();	

		public abstract DatabasePlatformType instDataSourceType();

		#region ExecuteNonQuery
		/// <summary>
		/// ִ��������������κν��
		/// </summary>
		/// <param name="commandText"></param>
		/// <returns></returns>
		public abstract int ExecuteNonQuery(string commandText);
		#endregion

		#region ExecuteReader
		/// <summary>
		/// ִ���������һ�����ͻ���IDataReader
		/// </summary>
		/// <param name="commandText"></param>
		/// <returns></returns>
		public abstract IDataReader  ExecuteReader(string commandText);
		#endregion

		#region ExecuteScalar
		/// <summary>
		/// ִ���������һ��ֵ
		/// </summary>
		/// <param name="commandText"></param>
		/// <returns></returns>
		public abstract  object ExecuteScalar(string commandText);
		#endregion

        #region GetServerTime
        public abstract  DateTime GetServerTime();
		#endregion

		#region GetNewID
		public abstract int GetNewID(string SeqName);

		public abstract  int  GetNewID(string SeqName,int Count);

		#endregion

        public virtual bool ExistTable(string TABLESPACE_NAME, string TABLE_NAME)
        { return true ;}

        public virtual bool ExistField(string table,string field)
        { return true; }

        public abstract DataTable SelectBySql(string strSql);

        public abstract IDbDataAdapter CreateDbDataAdapter();
        public abstract IDbCommand CreateDbCommand(string commandText);
        public abstract System.Data.Common.DbCommandBuilder CreateDbCommandBuilder(System.Data.Common.DbDataAdapter adapter );
        public abstract string UserName { get; }
        public abstract string Service { get; }
        public virtual void WriteByteRow(string tableName, Dictionary<string, object> fieldsNameValueDic) { }
	}
}
