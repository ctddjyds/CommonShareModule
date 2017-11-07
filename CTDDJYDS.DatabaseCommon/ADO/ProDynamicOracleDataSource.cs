using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Data;
using System.Xml;
using System.Reflection;
using System.Runtime;
using System.Security;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;

namespace CTDDJYDS.DatabaseCommon
{
    public class ProDynamicOracleDataSource : ProDataSource
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private Assembly _Assembly;
        public ProDynamicOracleDataSource()
        {
        }
        public ProDynamicOracleDataSource(string connectionString)
        {
            string file = FindDllFile();
            _Assembly = Assembly.LoadFrom(file);
            Type type = _Assembly.GetType("Oracle.DataAccess.Client.OracleConnection");

            //Oracle.DataAccess.Client.OracleConnection t = new Oracle.DataAccess.Client.OracleConnection();
            //Type type = t.GetType();
            //Assembly a = type.Assembly;

            IDbConnection conn = Activator.CreateInstance(type, true) as IDbConnection;
            conn.ConnectionString = connectionString;
            _connection = conn;

            //this._connection	= new OracleConnection(connectionString);
            this._transaction = null;
        }

        public ProDynamicOracleDataSource(IDbConnection conn)
        {
            _connection = conn;
            _Assembly = conn.GetType().Assembly;
            this._transaction = null;
        }

        private string FindDllFile()
        {
            string path = FindOracleHomePath();
            string[] files = Directory.GetFiles(path, "Oracle.DataAccess.dll");
            if (files.Length > 0)
                return files[0];
            //bin���ϼ�Ŀ¼��
            path = Path.GetDirectoryName(path) + "\\odp.net";
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path, "Oracle.DataAccess.dll", SearchOption.AllDirectories);
                if (files.Length == 1)
                    return files[0];
                else if (files.Length > 1)
                {
                    foreach (string file in files)
                    {
                        if (file.Contains("2."))
                            return file;
                    }
                }
            }

            throw new ApplicationException("û���ҵ�Oracle.DataAccess.dll�ļ�");
        }

        private string FindOracleHomePath()
        {
            //�ӻ��������ж�ȡ oracle ��װ·��
            string path = get_HJBLZ("path");
            string[] pathArr = path.Split(';');
            string oraPath = ""; //���� c:\oracle\product\10.2.0\client_1\bin
            for (int i = 0; i < pathArr.Length; i++)
            {
                if (pathArr[i].IndexOf(@"\oracle\", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    oraPath = pathArr[i];
                    break;
                }
            }
            //��ע����ȡ·��
            if (oraPath == "") //���� c:\oracle\product\10.2.0\client_1
                oraPath = FindRegDll() + "\\bin";
            if (oraPath == "" || oraPath == "\\bin")
                throw new ApplicationException("����oracle��װ���·��ʧ��");
            return oraPath;
        }

        private string FindRegDll()
        {
            Microsoft.Win32.RegistryKey regKey = null;//= Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey subKey = null; //= reg.OpenSubKey(@"SOFTWARE\ORACLE");
            try
            {
                regKey = Microsoft.Win32.Registry.LocalMachine;
                subKey = regKey.OpenSubKey(@"SOFTWARE\ORACLE");
                if (subKey == null)
                    return "";

                //�ȱ��� key 
                string[] names = subKey.GetSubKeyNames();
                foreach (string name in names)
                {
                    RegistryKey regkey2 = subKey.OpenSubKey(name);
                    try
                    {   //�ڱ��� ֵ����
                        string[] valueNames = regkey2.GetValueNames();
                        foreach (string valueName in valueNames)
                        {
                            if (valueName.ToUpper() == "ORACLE_HOME")
                                return regkey2.GetValue(valueName).ToString();
                        }
                    }
                    finally { regkey2.Close(); }
                }
            }
            finally
            {
                if (subKey != null) subKey.Close();
                if (regKey != null) regKey.Close();
            }

            return "";
        }

        private string get_HJBLZ(string name)
        {
            name = name.ToUpper();
            // �ѻ������������е�ֵȡ�������ŵ�����environment�� 
            IDictionary environment = Environment.GetEnvironmentVariables();
            // ����environment�����м�ֵ 
            foreach (string key in environment.Keys)
            {
                // ��ӡ�����л������������ƺ�ֵ 
                if (key.ToUpper() == name)
                    return environment[key].ToString();
            }

            return "";
        }

        /// <summary>
        /// �õ����ݿ��������Ϣ
        /// </summary>
        public override IDbConnection Connection
        {
            get
            {
                return this._connection;
            }
        }

        /// <summary>
        /// �����������Ӳ���
        /// </summary>
        public override IDbTransaction Transaction
        {
            get
            {
                return this._transaction;
            }
        }

        public override ConnectionState State
        {
            get
            {
                return this._connection.State;
            }
        }
        public override void Open()
        {
            if (this._connection.State != ConnectionState.Open)
                this._connection.Open();

        }

        public override void Close()
        {
            if (this._connection.State == ConnectionState.Open)
                this._connection.Close();
        }

        public override void BeginTransaction()
        {
            this._transaction = this._connection.BeginTransaction();
        }

        public override void CommitTransaction()
        {
            this._transaction.Commit();
        }

        public override void RollBackTransaction()
        {
            this._transaction.Rollback();
        }
        public override DatabasePlatformType instDataSourceType()
        {
            return DatabasePlatformType.DB_Oracle;
        }
        #region ExecuteNonQuery
        /// <summary>
        /// ִ��������������κν��
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public override int ExecuteNonQuery(string commandText)
        {
            //OracleCommand command = new OracleCommand(commandText, this._connection) ;
            Type type = _Assembly.GetType("Oracle.DataAccess.Client.OracleCommand");
            IDbCommand command = Activator.CreateInstance(type, true) as IDbCommand;
            command.CommandText = commandText;
            command.Connection = this._connection;
            return command.ExecuteNonQuery();

        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// ִ���������һ�����ͻ���IDataReader
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(string commandText)
        {

            //OracleCommand command = new OracleCommand(commandText, this._connection);
            Type type = _Assembly.GetType("Oracle.DataAccess.Client.OracleCommand");
            IDbCommand command = Activator.CreateInstance(type, true) as IDbCommand;

            command.CommandText = commandText;
            command.Connection = this._connection;

            return command.ExecuteReader();

        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// ִ���������һ��ֵ
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public override object ExecuteScalar(string commandText)
        {

            //OracleCommand command = new OracleCommand(commandText, this._connection);
            Type type = _Assembly.GetType("Oracle.DataAccess.Client.OracleCommand");
            IDbCommand command = Activator.CreateInstance(type, true) as IDbCommand;
            command.CommandText = commandText;
            command.Connection = this._connection;

            return command.ExecuteScalar();

        }
        #endregion

        #region GetServerTime
        public override DateTime GetServerTime()
        {
            DateTime DtmServerdate = DateTime.Now;
            IDataReader objRead = null;

            string strQuerySql = "";

            strQuerySql = "SELECT SYSDATE FROM DUAL";

            try
            {
                objRead = ExecuteReader(strQuerySql) as IDataReader;
            }
            catch (Exception ex)
            {
                if (objRead != null)
                {
                    objRead.Close();
                }
                throw new Exception("GetServerTimeʧ��!", ex);
            }
            objRead.Read();
            DtmServerdate = objRead.GetDateTime(0);

            if (objRead != null)
            {
                objRead.Close();
            }
            return DtmServerdate;
        }
        #endregion

        #region GetNewID ��ȡ���ݿ�����ֵ

        public override int GetNewID(string SeqName)
        {
            int intNewid = GetNewID(SeqName, 1);
            return intNewid;
        }
        private object get_ProperyValue(object o, string properyName)
        {
            if (o.GetType() == typeof(string))
                return o.ToString();
            else if (o.GetType() == typeof(double) || o.GetType() == typeof(int) || o.GetType() == typeof(decimal))
                return Convert.ToDecimal(o);
            else if (o.GetType() == typeof(DateTime))
                return Convert.ToDateTime(o);

            PropertyInfo p = o.GetType().GetProperty(properyName);
            if (p != null)
            {
                return p.GetValue(o, null);
            }
            return null;
        }
        public override int GetNewID(string SeqName, int Count)
        {
            int intNewid = 0;
            string strCreateSql = "";
            string strQuerySql = "";
            IDataReader objRead = null;

            strQuerySql = "Select TO_NUMBER(" + SeqName + ".NEXTVAL) From dual";

        right:
            try
            {

                objRead = ExecuteReader(strQuerySql) as IDataReader;
            }
            //catch ( OracleException  ex)
            catch (Exception ex)
            {
                if (objRead != null)
                    objRead.Close();
                object errNum = get_ProperyValue(ex, "Number");
                if (errNum != null && errNum.ToString() == "-2147217900")
                {
                    strCreateSql = "CREATE SEQUENCE " + SeqName + " INCREMENT BY 1 START WITH 1 MINVALUE 1 NOCYCLE NOCACHE NOORDER";
                    ExecuteNonQuery(strCreateSql);
                    goto right;
                }
                else
                {
                    throw new Exception("GetNewIDʧ��!", ex);
                }

            }

            objRead.Read();
            intNewid = Convert.ToInt32(objRead.GetValue(0));

            if (objRead != null)
            {
                objRead.Close();
            }
            return intNewid;
        }

        #endregion

        public override bool ExistTable(string TABLESPACE_NAME, string TABLE_NAME)
        {
            string strSql = string.Format("select count(*) from sys.user_tables where upper(TABLE_NAME)='{0}' and upper(TABLESPACE_NAME)='{1}'", TABLE_NAME.ToUpper(), TABLESPACE_NAME.ToUpper());
            //if ( ((OracleDecimal) this.ExecuteScalar(strSql)).Value>0 )
            //    return true;
            //else 
            //    return false ;
            object v = this.ExecuteScalar(strSql);
            v = get_ProperyValue(v, "Value");
            if (v != null && v != DBNull.Value && int.Parse(v.ToString()) > 0)
                return true;
            else
                return false;
        }

        public static ProDataSource CreateInstance(string HOST, string PORT, string SID, string UserId, string Password)
        {
            string connstring =
              string.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))" +
              "(CONNECT_DATA=(SID={2})));User Id={3};Password={4};", HOST, PORT, SID, UserId, Password);//���Ҳ���Էŵ�Web.Config�С�
            return new ProDynamicOracleDataSource (connstring);
        }

        public override DataTable SelectBySql(string strSql)
        {
            DataTable datatable = new DataTable();

            System.Data.Common.DbDataAdapter oraAdapter = this.CreateDbDataAdapter() as System.Data.Common.DbDataAdapter; ;
            System.Data.IDbCommand command = this.CreateDbCommand(strSql);
            (oraAdapter as IDbDataAdapter ).SelectCommand = command;
            oraAdapter.Fill(datatable);
            return datatable;
        }

        public override System.Data.IDbCommand CreateDbCommand(string commandText)
        {
            Type type = _Assembly.GetType("Oracle.DataAccess.Client.OracleCommand");
            IDbCommand command = Activator.CreateInstance(type, true) as IDbCommand;
            if (command ==null)
                throw new ApplicationException("����OracleCommandʧ��");
            command.Connection = this.Connection;
            command.CommandText = commandText;
            command.Transaction = this.Transaction;

            return command ;
        }

        public override System.Data.IDbDataAdapter CreateDbDataAdapter()
        {
            Type type = _Assembly.GetType("Oracle.DataAccess.Client.OracleDataAdapter");
            IDbDataAdapter a= Activator.CreateInstance(type, true) as System.Data.IDbDataAdapter;
            if (a == null)
                throw new ApplicationException("����OracleDataAdapterʧ��");
            return a;
        }

        #region ��̬����
        public override System.Data.Common.DbCommandBuilder CreateDbCommandBuilder(System.Data.Common.DbDataAdapter adapter)
        {
            Type type = _Assembly.GetType("Oracle.DataAccess.Client.OracleCommandBuilder");
            System.Data.Common.DbCommandBuilder builder = Activator.CreateInstance(type) as System.Data.Common.DbCommandBuilder;
            if (builder!=null )
                builder.DataAdapter = adapter;

            //if (builder == null)
            //    throw new ApplicationException("��̬����DbCommandBuilderʧ��");

            return builder;
        }

        public static string CreateConnetionString(string HOST, string PORT, string SID, string UserId, string Password,bool sysdba)
        {
            string s= string.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))" +
              "(CONNECT_DATA=(SID={2})));User Id={3};Password={4};", HOST, PORT, SID, UserId, Password);//���Ҳ���Էŵ�Web.Config�С�
            if (sysdba)
                s = s + "DBA Privilege=SYSDBA;";
            return s;
        }

        public static ProDataSource ConnectDataSource(string HOST, string PORT, string SID, string UserId, string Password,bool sysdba)
        {
            //ʹ�ð�װ��¼
            ProDataSource ds = null;
            string exMsg = string.Empty;
            string connstr = CreateConnetionString(HOST, PORT, SID, UserId, Password, sysdba);
            try
            {
                ds = new ProDynamicOracleDataSource (connstr);
                ds.Open();

            }
            catch (Exception ex) { ds = null; exMsg = ex.Message; }

            //�ⰲװ�����ӷ�ʽ
            string file =typeof( ProDynamicOracleDataSource).Assembly.CodeBase.Substring (8) + "\\Oracle.DataAccess.dll";
            if (ds == null && System.IO.File.Exists(file))
                try
                {
                    Assembly a = Assembly.LoadFrom(file);
                    Type type = a.GetType("Oracle.DataAccess.Client.OracleConnection");
                    IDbConnection conn = Activator.CreateInstance(type, true) as IDbConnection;
                    conn.ConnectionString = connstr;
                    ds = new ProDynamicOracleDataSource(conn);
                    ds.Open();
                }
                catch (Exception ex) { ds = null; exMsg = exMsg + "\n" + ex.Message; }

            //΢�������
            if (ds == null)
                try
                {
                    ds = ProDataSourceFactory.CreateInstance(HOST, "", UserId, Password, DatabasePlatformType.DB_Oracle);
                    ds.Open();
                }
                catch (Exception ex) { ds = null; exMsg = exMsg + "\n" + ex.Message; }

            if (ds == null)
                throw new ApplicationException(exMsg);
            return ds;
        }
        #endregion
        public override string UserName
        {
            get {
                string[] a = this.Connection.ConnectionString.Split(new char[] { ';' });
                foreach (string s in a)
                {
                    if (s.ToUpper().StartsWith("USER"))
                    {
                        string[] a2 = s.Split(new char[] { '=' });
                        return a2[1].Trim();
                    }
                }
                return "";
            }
        }
        public override string Service
        {
            get {
                string[] a = this.Connection.ConnectionString.Split(new char[] { ';' });
                string datasouce = "";
                foreach (string s in a)
                {
                    if (s.ToUpper().StartsWith("DATA"))
                    {
                        string[] a2 = s.Split(new char[] { '=' });
                        datasouce = a2[1].Trim();
                        break;
                    }
                }

                if (datasouce == "")
                    return "";
                if (datasouce.ToUpper().Contains("SID="))
                {//��̬���ӵ����/Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))" +    "(CONNECT_DATA=(SID={2})));User Id={3};Password={4};
                    int i = datasouce.ToUpper().IndexOf("SID=");
                    string s1 = datasouce.Substring(i);
                    i=s1.IndexOf (")");
                    return s1.Substring(0, i);
                }
                else
                    return datasouce;
            }
        }
    }
}
