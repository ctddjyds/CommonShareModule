using JobScheduler.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JobScheduler.Web
{
    public static class WebApiConfig
    {
        /// <summary>
        ///  WebAPI 配置和服务
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //启用Web API特性路由
            config.MapHttpAttributeRoutes();
            // Web API 路由
            config.MapHttpAttributeRoutes();
            //路由系统是请求消息进入ASP.NET Web API 消息处理管道的第一道屏障，其根本目的用于解析URL请求
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //自定义路由--匹配到action
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //添加全局异常处理
            config.Filters.Add(new WebApiExceptionFilterAttribute());
        }
    }
}
