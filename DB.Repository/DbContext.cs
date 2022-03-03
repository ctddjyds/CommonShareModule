using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repository
{
    /// <summary>
    /// 继承类不允许使用静态对象，保证每次都NEW出来
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbContext<T> where T : class, new()
    {
        public DbContext(DbType dbType,string connectionString)
        {
           
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = dbType,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息,默认SystemTable, 字段信息读取
                IsAutoCloseConnection = true,//开启自动释放模式

            });
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                try
                {
                    string par = Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                    //string par = JsonHelper.GetJson(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                    string sqlAll = $"{sql}\r\n{par}";
                    //Log4NetHelper.Log(sqlAll, LogAppenderType.SQLLog);
                }
                catch (Exception ex)
                {
                    //Log4NetHelper.Log(ex);
                }
            };

        }
        /// <summary>
        /// 注意：不能写成静态的
        /// 用来处理事务多表查询和复杂的操作
        /// </summary>
        protected SqlSugarClient Db;

        /// <summary>
        /// 用来操作当前表的数据
        /// </summary>
        protected SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询单个记录
        /// </summary>
        /// <returns></returns>
        public virtual T GetSingle(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.GetSingle(whereExpression);
        }
        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        public virtual List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel, orderByExpression, orderByType);
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual T GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return CurrentDb.DeleteById(id);
        }


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(T data)
        {
            return CurrentDb.Delete(data);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic[] ids)
        {
            return CurrentDb.AsDeleteable().In(ids).ExecuteCommand() > 0;
        }
        public bool IsExits(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.IsAny(whereExpression);
        }
        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(List<T> objs)
        {
            return CurrentDb.UpdateRange(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(T obj)
        {
            int t = Db.Insertable(obj).IgnoreColumns(true).ExecuteCommand();
            if (t > 0)
                return true;
            else
                return false;
        }

        public virtual int InsertReturnOID(T t)
        {
            return CurrentDb.AsInsertable(t).IgnoreColumns(true).ExecuteCommand();//插入返回自增列
        }

        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(List<T> objs)
        {
            return CurrentDb.InsertRange(objs);
        }


        //自已扩展更多方法 
        public virtual System.Data.DataTable GetDataTable(string strSql)
        {
            return Db.Ado.GetDataTable(strSql);
        }

        public int GetInt(string strSql)
        {
            return Db.Ado.GetInt(strSql);
        }
    }
}
