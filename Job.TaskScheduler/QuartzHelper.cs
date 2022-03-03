using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Job.TaskScheduler
{
    public class QuartzHelper
    {
        static QuartzHelper()
        {
        }
        private static IScheduler _scheduler;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object Obj = new object();
        /// <summary>
        /// 缓存任务所在程序集信息
        /// </summary>
        private static Dictionary<string, Assembly> AssemblyDict = new Dictionary<string, Assembly>();
        public static void  CreateQuartzScheduler(string connStr,string datasource)
        {
            try
            {
                lock (Obj)
                {
                    if (_scheduler != null)
                        return;
                    //创建一个作业调度池
                    var properties = new NameValueCollection();
                    //存储类型：第一种类型叫做RAMJobStore（默认，这种方法提供了最佳的性能，因为内存中数据访问最快，足之处是缺乏数据的持久性），第二种类型叫做JDBC作业存储
                    properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
                    //表明数据库表的前缀
                    properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
                    //数据库驱动类型 AdoJobStore 使用驱动器委托
                    // 驱动类型，目前支持如下驱动：
                    //Quartz.Impl.AdoJobStore.FirebirdDelegate
                    //Quartz.Impl.AdoJobStore.MySQLDelegate
                    //Quartz.Impl.AdoJobStore.OracleDelegate
                    //Quartz.Impl.AdoJobStore.SQLiteDelegate
                    //Quartz.Impl.AdoJobStore.SqlServerDelegate
                    properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
                    //数据源名称
                    properties["quartz.jobStore.dataSource"] = datasource;
                    //连接字符串--"server=192.168.2.88;database=QuartzDB;uid=sa;pwd=winner@001"
                    properties["quartz.dataSource.myDS.connectionString"] = connStr;
                    //sqlserver版本
                    // * SqlServer-20         2.0.0.0
                    // * SqlServerCe-351      3.5.1.0
                    // * SqlServerCe-352      3.5.1.50
                    // * SqlServerCe-400      4.0.0.0
                    // * 其他还有OracleODP，Npgsql，SQLite，Firebird，OleDb
                    //数据库提供器
                    properties["quartz.dataSource.myDS.provider"] = "SqlServer-20";
                    // 设置线程池
                    properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
                    //设置线程池的最大线程数量
                    properties["quartz.threadPool.threadCount"] = "10";
                    //设置作业中每个线程的优先级
                    properties["quartz.threadPool.threadPriority"] = ThreadPriority.Normal.ToString();

                    // 远程输出配置
                    //properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
                    //properties["quartz.scheduler.exporter.port"] = "555";  //配置端口号
                    //properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";//名称
                    //properties["quartz.scheduler.exporter.channelType"] = "tcp"; //协议类型
                    //properties["quartz.scheduler.exporter.channelName"] = "httpQuartz";
                    //properties["quartz.scheduler.exporter.rejectRemoteRequests"] = "true";

                    //是否集群
                    //properties["quartz.jobStore.clustered"] = "false";
                    // Quartz Scheduler唯一实例ID，auto：自动生成
                    //properties["quartz.scheduler.instanceId"] = "AUTO";
                    // Quartz Scheduler实例名称
                    //properties["quartz.scheduler.instanceName"] = "QuartzDBDemo";
                    //创建一个工厂
                    var schedulerFactory = new StdSchedulerFactory(properties);
                    //启动
                    _scheduler = schedulerFactory.GetScheduler().Result;
                    //1、开启调度
                    //_scheduler.Start();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// 初始化远程任务调度对象
        /// </summary>
        public static void InitRemoteScheduler(string scheme,string server,string port,string remoteName)
        {
            try
            {

                lock (Obj)
                {

                if (_scheduler != null)
                    return;
                NameValueCollection properties = new NameValueCollection();

                properties["quartz.scheduler.instanceName"] = "RemoteQuartzScheduler";
                properties["quartz.scheduler.proxy"] = "true";
                properties["quartz.scheduler.proxy.address"] = string.Format("{0}://{1}:{2}/{3}", scheme, server, port, remoteName);

                ISchedulerFactory sf = new StdSchedulerFactory(properties);

                //ISchedulerFactory sf = new StdSchedulerFactory();

                _scheduler = sf.GetScheduler().Result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取作业调度器
        /// </summary>
        /// <returns></returns>
        public static IScheduler GetScheduler()
        {
            try
            {
                if (_scheduler != null)
                {
                    return _scheduler;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 开始任务调度
        /// </summary>
        public static void StartSchedule()
        {
            try
            {
                //判断调度是否已经关闭
                if (_scheduler.IsShutdown)
                {
                    //等待任务运行完成
                    _scheduler.Start();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 暂停指定任务计划
        /// </summary>
        /// <returns></returns>
        public static bool PauseScheduleJob(string jobGroup, string jobName,bool isAll=false)
        {
            try
            {
                if (isAll)
                    _scheduler.PauseAll();
                else
                    _scheduler.PauseJob(new JobKey(jobName, jobGroup));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobKey"></param>
        public static void PauseJob(JobKey jk)
        {
            try
            {
                if (_scheduler.CheckExists(jk).Result)
                {
                    //任务已经存在则暂停任务
                    _scheduler.PauseJob(jk);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 开启指定的任务计划
        /// 恢复运行暂停的任务
        /// </summary>
        /// <returns></returns>
        public static bool ResumeScheduleJob(string jobGroup, string jobName,bool isAll=false)
        {
            try
            {
                if (!_scheduler.IsStarted)
                    _scheduler.Start();
                if (isAll)
                    _scheduler.ResumeAll();
                else
                    _scheduler.ResumeJob(new JobKey(jobName, jobGroup));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 恢复运行暂停的任务
        /// </summary>
        /// <param name="jobKey">任务key</param>
        public static void ResumeJob(JobKey jk)
        {
            try
            {
                if (_scheduler.CheckExists(jk).Result)
                {
                    //任务已经存在则暂停任务
                    _scheduler.ResumeJob(jk);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 停止任务调度
        /// </summary>
        public static void ShopSheduleJob()
        {
            if (!_scheduler.IsShutdown)
                //true:等待任务完成后再结束
                _scheduler.Shutdown(true);
        }
        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="jobName"></param>
        public static void RemoveSheduleJob(string jobGroup, string jobName)
        {
            try
            {
                var trigger = new TriggerKey(jobGroup, jobName);
                _scheduler.PauseTrigger(trigger);//停止触发器
                _scheduler.UnscheduleJob(trigger); //移除触发器
                _scheduler.DeleteJob(JobKey.Create(jobName, jobGroup));
            }
            catch(Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// 清除job
        /// </summary>
        public static void ClearSchedule()
        {

            try
            {
                //判断调度是否已经关闭
                if (!_scheduler.IsShutdown)
                {
                    //等待任务运行完成
                    _scheduler.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 获取任务详细信息
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public static IJobDetail GetJobDetail(JobKey jk)
        {
            try
            {
                //判断调度是否已经关闭
                if (_scheduler.CheckExists(jk).Result)
                {
                    //任务已经存在则暂停任务
                    return _scheduler.GetJobDetail(jk).Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// 获取当前执行的任务
        /// </summary>
        /// <returns></returns>
        public static List<IJobExecutionContext> GetCurrentlyExecutingJobs()
        {
            try
            {
                return _scheduler.GetCurrentlyExecutingJobs().Result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 关联任务和触发器
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="trigger"></param>
        public static void ScheduleJob(IJobDetail jobDetail, ITrigger trigger)
        {
            try
            {
                if (_scheduler != null)
                {
                    //先删除现有已存在任务
                    RemoveSheduleJob(jobDetail.Key.Group,jobDetail.Key.Name);


                    _scheduler.ScheduleJob(jobDetail, trigger);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 按分组获取所有任务
        /// </summary>
        /// <returns></returns>
        public static List<IJobDetail> GetAllJobs()
        {
            List<string> jobgroups = _scheduler.GetJobGroupNames().Result.ToList();

            List<IJobDetail> result = null;

            foreach (string str in jobgroups)
            {
                foreach (JobKey job in _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(str)).Result)
                {
                    IJobDetail jobDetail = _scheduler.GetJobDetail(job).Result;

                    if (jobDetail != null)
                    {
                        result.Add(jobDetail);
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// 获取任务关联的触发器
        /// </summary>
        /// <param name="jobkey"></param>
        /// <returns></returns>
        public static List<ITrigger> GetTriggersOfJob(JobKey jobkey)
        {
            try
            {
                return _scheduler.GetTriggersOfJob(jobkey).Result.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取文件的绝对路径,针对window程序和web程序都可使用
        /// </summary>
        /// <param name="relativePath">相对路径地址</param>
        /// <returns>绝对路径地址</returns>
        public static string GetAbsolutePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                throw new ArgumentNullException("参数relativePath空异常！");
            }
            relativePath = relativePath.Replace("/", "\\");
            if (relativePath[0] == '\\')
            {
                relativePath = relativePath.Remove(0, 1);
            }
            //判断是Web程序还是window程序
            if (HttpContext.Current != null)
            {
                return Path.Combine(HttpRuntime.AppDomainAppPath + "\\bin", relativePath);
            }
            else
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\bin", relativePath);
            }
        }

        /// 获取类的属性、方法  
        /// </summary>  
        /// <param name="assemblyName">程序集</param>  
        /// <param name="className">类名</param>  
        public static Type GetClassInfo(string assemblyName, string className)
        {
            try
            {
                assemblyName = GetAbsolutePath(assemblyName + ".dll");
                Assembly assembly = null;
                if (!AssemblyDict.TryGetValue(assemblyName, out assembly))
                {
                    assembly = Assembly.LoadFrom(assemblyName);
                    AssemblyDict[assemblyName] = assembly;
                }
                Type type = assembly.GetType(className, true, true);
                return type;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 新增任务
        /// </summary>
        /// <param name="task"></param>
        public static void AddNewJob(JobEntity task)
        {
            IJobDetail jobDetail = null;
            ITrigger trigger = null;
            try
            {
                if (task != null)
                {

                    jobDetail = new JobDetailImpl(task.JobName, task.JobGroup, QuartzHelper.GetClassInfo(task.AssemblyName, task.ClassName), true, true);

                    if (task.Type == 0)
                    {
                        trigger = new SimpleTriggerImpl()
                        {
                            Name = task.JobName + "_Trigger",
                            Group = task.JobGroup,
                            Description = task.Description,
                            RepeatInterval = task.RepeatInterval,
                            RepeatCount = task.RepeatCount,
                            StartTimeUtc = task.BeginTime,
                            EndTimeUtc = task.EndTime
                        };

                    }
                    else if (task.Type == 1)
                    {
                        trigger = new CronTriggerImpl()
                        {
                            Name = task.JobName + "_Trigger",
                            Group = task.JobGroup,
                            Description = task.Description,
                            CronExpressionString = task.CronExpression,
                            StartTimeUtc = task.BeginTime,
                            EndTimeUtc = task.EndTime
                        };
                    }

                    ScheduleJob(jobDetail, trigger);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// 验证是否Cron表达式
        /// </summary>
        /// <param name="CronExpression"></param>
        /// <returns></returns>
        public static bool IsCronExpression(string cronExpression)
        {
            return CronExpression.IsValidExpression(cronExpression);
        }

        /// <summary>
        /// 时间间隔执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="seconds">时间间隔(单位：秒)</param>
        public static async Task<bool> ExecuteInterval<T>(T t, int seconds) where T : IJobBase
        {
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(t.JobName, t.JobGroup)
                .UsingJobData("myFloatValue", 3.141f) //作业数据对象
                //.WithDescription(m.JobDescription)
                .Build();
            // 3、创建触发器
            ITrigger trigger = TriggerBuilder.Create()
           .StartNow()
           //.StartAt(DateBuilder.FutureDate(5, IntervalUnit.Minute)) // 用 DateBuilder 创建一个将来的日期；如果没有给出开始时间 (如果省略了这一行), 则隐含为 "现在"
           //.ModifiedByCalendar("myHolidays") // 指定节假日不执行
           //.WithCronSchedule("0/5 * * * * ?")     //5秒执行一次
           .WithSimpleSchedule(
                x => x.WithIntervalInSeconds(seconds)             
                //x.WithIntervalInMinutes(1)
                //.WithRepeatCount(10)) // 注意这里设置重复次数10次, 但一共会执行11次
                .RepeatForever())//长久触发
           .WithIdentity(t.JobName, t.JobGroup)
           //.EndAt(DateBuilder.DateOf(22, 0, 0)) //直到 22:00结束
           //.WithDescription(m.JobDescription)
           //.ForJob(myJobKey) // 用 JobKey 标识作业 或用JobDetail本身的句柄标识 job
           //.ForJob("job1", "group1") // 用名称和组名标识作业
           .Build();
            //4、将任务加入到任务池
            await _scheduler.ScheduleJob(job, trigger);
            return true;
        }

        /// <summary>
        /// 指定时间执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="cronExpression">cron表达式，即指定时间点的表达式</param>
        public static async Task<bool> ExecuteByCron<T>(T t, string cronExpression) where T : IJobBase
        {
            /*
             简单说一下Cron表达式:
             由7段构成：秒 分 时 日 月 星期 年（可选）
  
             "-" ：表示范围  MON-WED表示星期一到星期三
             "," ：表示列举 MON,WEB表示星期一和星期三
             "*" ：表是“每”，每月，每天，每周，每年等
             "/" :表示增量：0/15（处于分钟段里面） 每15分钟，在0分以后开始，3/20 每20分钟，从3分钟以后开始
             "?" :只能出现在日，星期段里面，表示不指定具体的值
             "L" :只能出现在日，星期段里面，是Last的缩写，一个月的最后一天，一个星期的最后一天（星期六）
             "W" :表示工作日，距离给定值最近的工作日
             "#" :表示一个月的第几个星期几，例如："6#3"表示每个月的第三个星期五（1=SUN...6=FRI,7=SAT）  
             如果Minutes的数值是 '0/15' ，表示从0开始每15分钟执行  
             如果Minutes的数值是 '3/20' ，表示从3开始每20分钟执行，也就是‘3/23/43’
             */
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(t.JobName, t.JobGroup)
                //.WithDescription(m.JobDescription)
                .Build();
            //3、创建触发器
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
            .StartNow()
            .WithCronSchedule(cronExpression)
            .WithIdentity(t.JobName, t.JobGroup)
            //.WithDescription(m.JobDescription)
            .Build();
            //4、将任务加入到任务池
            await _scheduler.ScheduleJob(job, trigger);
            return true;
        }

        /// <summary>
        /// 获取所有任务组
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroups()
        {
            return _scheduler.GetJobGroupNames().Result.ToList();
        }

        /// <summary>
        /// 根据组名获取任务
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public static List<JobEntity> GetTasks(string GroupName)
        {

            List<JobEntity> jobs = new List<JobEntity>();
            foreach (JobKey job in _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(GroupName)).Result)
            {
                IJobDetail jobDetail = _scheduler.GetJobDetail(job).Result;
                if (jobDetail != null)
                {
                    IList<ITrigger> triggers = GetTriggersOfJob(jobDetail.Key);
                    JobEntity task = new JobEntity();


                    TriggerState State = _scheduler.GetTriggerState(triggers[0].Key).Result;


                    task.JobName = jobDetail.Key.Name;
                    task.JobGroup = jobDetail.Key.Group;
                    //task.Description = jobDetail.Description;
                    task.AssemblyName = jobDetail.JobType.Namespace.ToString();
                    task.ClassName = jobDetail.JobType.FullName.ToString();
                    task.BeginTime = triggers[0].StartTimeUtc;
                    if (triggers[0].EndTimeUtc != null)
                    {
                        task.EndTime = triggers[0].EndTimeUtc;
                    }
                    else
                    {
                        task.EndTime = null;
                    }
                    if (triggers[0] is SimpleTriggerImpl)
                    {
                        task.Type = 0;
                        task.RepeatCount = ((SimpleTriggerImpl)triggers[0]).RepeatCount;
                        task.RepeatInterval = ((SimpleTriggerImpl)triggers[0]).RepeatInterval;
                        task.FiredCount = ((SimpleTriggerImpl)triggers[0]).TimesTriggered;
                        task.Description = ((SimpleTriggerImpl)triggers[0]).Description;
                    }
                    else if (triggers[0] is CronTriggerImpl)
                    {
                        task.Type = 1;
                        task.CronExpression = ((CronTriggerImpl)triggers[0]).CronExpressionString.ToString();
                        task.Description = ((CronTriggerImpl)triggers[0]).Description;
                    }
                    if (triggers[0].GetNextFireTimeUtc() != null)
                    {
                        task.NextFireTime = triggers[0].GetNextFireTimeUtc();
                    }
                    else
                    {
                        task.NextFireTime = null;
                    }
                    task.State = State.ToString();
                    jobs.Add(task);
                }
            }
            return jobs;
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public static JobEntity GetTask(string name, string group)
        {

            JobKey jobkey = new JobKey(name, group);
            IJobDetail jobDetail = _scheduler.GetJobDetail(jobkey).Result;

            if (jobDetail != null)
            {
                JobEntity task = new JobEntity();

                IList<ITrigger> triggers = GetTriggersOfJob(jobDetail.Key);

                TriggerState State = _scheduler.GetTriggerState(triggers[0].Key).Result;

                task.JobName = jobDetail.Key.Name;
                task.JobGroup = jobDetail.Key.Group;
                //task.Description = jobDetail.Description;
                task.AssemblyName = jobDetail.JobType.Namespace.ToString();
                task.ClassName = jobDetail.JobType.FullName.ToString();
                task.BeginTime = triggers[0].StartTimeUtc;
                if (triggers[0].EndTimeUtc != null)
                {
                    task.EndTime = triggers[0].EndTimeUtc;
                }
                else
                {
                    task.EndTime = null;
                }
                if (triggers[0] is SimpleTriggerImpl)
                {
                    task.Type = 0;
                    task.RepeatCount = ((SimpleTriggerImpl)triggers[0]).RepeatCount;
                    task.RepeatInterval = ((SimpleTriggerImpl)triggers[0]).RepeatInterval;
                    task.FiredCount = ((SimpleTriggerImpl)triggers[0]).TimesTriggered;
                    task.Description = ((SimpleTriggerImpl)triggers[0]).Description;
                }
                else if (triggers[0] is CronTriggerImpl)
                {
                    task.Type = 1;
                    task.CronExpression = ((CronTriggerImpl)triggers[0]).CronExpressionString.ToString();
                    task.Description = ((CronTriggerImpl)triggers[0]).Description;
                }
                if (triggers[0].GetNextFireTimeUtc() != null)
                {
                    task.NextFireTime = triggers[0].GetNextFireTimeUtc();
                }
                else
                {
                    task.NextFireTime = null;
                }
                task.State = State.ToString();
                return task;
            }
            return null;
        }
    }
}
