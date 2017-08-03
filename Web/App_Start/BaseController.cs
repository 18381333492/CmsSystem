using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Server;

namespace Web.App_Start
{
    /// <summary>
    /// 基础Controller
    /// </summary>
    public class BaseController: Controller
    {
        protected EFHelper mangae;
        protected Result result;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public BaseController()
        {
            mangae = new EFHelper();
            result = new Result();
        }

        /// <summary>
        /// 在Action之前调用
        /// tip:主要来验证用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        //    if (bool.Parse(filterContext.HttpContext.Application["bIsStartUp"].ToString()) == true)
        //    {//网站能继续运行
        //        if (!(filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoLogin), true).Length == 1))
        //        {//有NoLogin属性;不判断登录
        //            if (Session[SESSION.AdminUser] == null)
        //            {
        //                /*登录过时,session过期*/
        //                if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
        //                {
        //                    /*跳转到登录过期提示页面*/
        //                    var LoginPath = C_Config.ReadAppSetting("virtualPath");
        //                    filterContext.Result = new RedirectResult(LoginPath + "/Admin/AdminUser/Login");
        //                }
        //                else
        //                {
        //                    result.over = true;//登录过时
        //                    ContentResult res = new ContentResult();
        //                    res.Content = result.toJson();
        //                    filterContext.Result = res;
        //                }
        //            }
        //            else
        //            {//session存在

        //            }
        //        }
        //    }
        //    else
        //    {//网站过期
        //        if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
        //        {
        //            /*跳转到网站到期提示页面*/
        //            var LoginPath = C_Config.ReadAppSetting("virtualPath");
        //            filterContext.Result = new RedirectResult("http://www.baidu.com");
        //        }
        //        else
        //        {
        //            result.success = false;
        //            result.info = "网站运行时间已到期,不能执行任何操作.请及时充值!";
        //            ContentResult res = new ContentResult();
        //            res.Content = result.toJson();
        //            filterContext.Result = res;
        //        }
        //    }
        }


        /// <summary>
        /// 在Action之后调用
        /// 主要处理返回的数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var actionMethod = filterContext.Controller
              .GetType()
              .GetMethod(filterContext.ActionDescriptor.ActionName);//获取访问方法   
            if (actionMethod.ReturnType.Name.ToString() == "Void" && request.IsAjaxRequest() && request.HttpMethod.ToUpper() == "POST")
            {//POST的返回结果处理
                filterContext.Result = Content(result.toJson()); /**统一处理ajax的返回结果**/
            }
        }
    }
}