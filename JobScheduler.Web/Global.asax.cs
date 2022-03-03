using JobScheduler.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JobScheduler.Web
{
    /// <summary>
    /// IIS启动入口
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 注册
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //添加全局异常处理
            GlobalConfiguration.Configuration.Filters.Add(new WebApiExceptionFilterAttribute());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 全局式异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Server.ClearError();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
