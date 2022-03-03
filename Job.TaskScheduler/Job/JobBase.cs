using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Job.TaskScheduler
{
    public abstract class JobBase : IJob
    {
        /// <summary>
        /// 执行指定任务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public void ExecuteJob(IJobExecutionContext context, Action action)
        {
            try
            {
                //记录文件日志- 开始执行相关任务
                string msg = $"{context.Trigger.Key.Name }开始执行";
                //开始执行
                action();

                //记录文件日志- 开始执行相关任务
                msg = $"{context.Trigger.Key.Name }成功执行";
               
            }
            catch (Exception ex)
            {
                JobExecutionException e2 = new JobExecutionException(ex);
                //true  是立即重新执行任务 
                e2.RefireImmediately = true;

                //记录文件日志- 开始执行相关任务
              
                //logger.Error(e2.Message);

            }
        }
        public abstract Task Execute(IJobExecutionContext context);
    }
}
