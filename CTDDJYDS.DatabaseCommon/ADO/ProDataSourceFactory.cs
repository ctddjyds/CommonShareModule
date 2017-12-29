using System;
using System.Data.OleDb;
using System.Data.Odbc;

namespace CTDDJYDS.Database.Common
{
    public static  class ProDataSourceFactory
    {
        public static string connectionString="";
        public static DatabasePlatformType databaseType { get; set; }

		
        #region ʵ�����������Ӷ���

	    public static ProDataSource CreateInstance()
	    {
            return ProDataSourceFactory.CreateInstance(connectionString, databaseType);
	    }

        /// <summary>
        /// �����ݿ�
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static ProDataSource CreateInstance(string connectionString, DatabasePlatformType databaseType) 
	    {	
		    ProDataSource dataSource = null;
            switch (databaseType) 
		    {
                case DatabasePlatformType.SQLServer:
                    dataSource = new ProSqlServerDataSource(connectionString);
				    break;
                case DatabasePlatformType.DB_Oracle:
                    dataSource = new ProOracleClientDataSource(connectionString);
				    break;
                case DatabasePlatformType.DB_OLEDB_Access:
                    dataSource = new ProOleDataSource(connectionString);
				    break;			   
			    default:
                    dataSource = new ProSqlServerDataSource(connectionString);
				    break;
		    }

		    return dataSource;
	    }

        public static ProDataSource CreateInstance(DatabaseSite site)
        {
            string conString = "";
            switch (site.dataSourceType)
            {
                case DatabasePlatformType.SQLServer:
                    if (string.IsNullOrEmpty(site.dataSourceName) && string.IsNullOrEmpty(site.userID))//Windows�����֤ģʽ��ʹ��Ĭ�����ݿ�ʵ��;
                        conString = string.Format("server=.;database={0};integrated security=SSPI", site.dbName);
                    else if (string.IsNullOrEmpty(site.userID))//�����ʹ��Ĭ�����ݿ�ʵ�������� server =./sid;
                        conString = string.Format("server=./{0};database={1};integrated security=SSPI", site.dataSourceName, site.dbName);
                    else//dataSourceName һ��Ϊ  ip/sid ģʽ
                        conString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", site.dataSourceName, site.dbName, site.userID, site.passWord);
                    break;
                case DatabasePlatformType.DB_Oracle:
                    conString = "Data Source=" + site.dataSourceName + ";User ID=" + site.userID + ";Password=" + site.passWord;                    
                    break;
                case DatabasePlatformType.DB_OLEDB_Access:
                    conString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" + site.dataSourceName;                    
                    break;
                default:
                    conString = "Data Source=" + site.dataSourceName + ";Initial Catalog=" + site.dbName + ";User ID=" + site.userID + ";Password=" + site.passWord;
                    break;
            }
            connectionString = conString;
            databaseType = site.dataSourceType;
            return ProDataSourceFactory.CreateInstance(conString, site.dataSourceType);

        }

        /// <summary>
        /// �����ݿ�����
        /// </summary>
        /// <param name="dataSourceName">�������ݿ��Ӧ�ļ������,һ��ΪIP��ַ</param>
        /// <param name="catalog">���ݿ�����</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static ProDataSource CreateInstance(string dataSourceName, DatabasePlatformType dbType, string catalog="", string username="", string password="")
	    {
            string conString = "";
            switch (dbType)
		    {
                case DatabasePlatformType.SQLServer:
                    if(string.IsNullOrEmpty(dataSourceName) && string.IsNullOrEmpty(username))//Windows�����֤ģʽ��ʹ��Ĭ�����ݿ�ʵ��;
                        conString = string.Format("server=.;database={0};integrated security=SSPI", catalog);
                    else if(string.IsNullOrEmpty(username))//�����ʹ��Ĭ�����ݿ�ʵ�������� server =./sid;
                        conString = string.Format("server=./{0};database={1};integrated security=SSPI", dataSourceName, catalog);
                    else//dataSourceName һ��Ϊ  ip/sid ģʽ
                        conString =string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", dataSourceName, catalog, username, password);
				    break;
                case DatabasePlatformType.DB_Oracle:
                    conString = "Data Source=" + dataSourceName + ";User ID=" + username + ";Password=" + password;
				    break;
                case DatabasePlatformType.DB_OLEDB_Access:
                    if (username ==null || username =="")//dataSourceNameΪAccess�ļ�·��
                        conString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" + dataSourceName ;
                    else
                        conString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" + dataSourceName + ";User ID=" + username ;
                    if (password !=null && password != string.Empty)
                    {
                        conString = string.Format("{0};Persist Security Info=true;Jet OLEDB:Database Password={1}", conString, password);
                    }
                    break;
                default:
                    conString = "Data Source=" + dataSourceName + ";Initial Catalog=" + catalog + ";User ID=" + username + ";Password=" + password;
                    break;
		    }
            connectionString = conString;
            databaseType = dbType;
            return ProDataSourceFactory.CreateInstance(conString, databaseType);
	    }

	    #endregion	
    }
    
}
