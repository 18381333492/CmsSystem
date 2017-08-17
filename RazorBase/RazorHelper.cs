using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using Common;

namespace RazorBase
{
    public class RazorHelper
    {
         /*
         tip:注意使用RazorEngine.Razor引擎的时候 在页面编写Razor语法的时候不要用as 用强制的
         */

        private readonly string sTemplateBasePath;
        private readonly string sHtmlBasePath;

        private static RazorHelper instance = null;

        public static RazorHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RazorHelper();
                }        
                return instance;
            }
        }

        //引擎服务接口
        private IRazorEngineService service;

        /// <summary>
        /// 初始化构造函数处理相关配置
        /// </summary>
        public RazorHelper()
        {
            //设置模板和页面目录的位置
            sTemplateBasePath = AppDomain.CurrentDomain.BaseDirectory +C_Config.ReadAppSetting("sTemplatePath");
            sHtmlBasePath = AppDomain.CurrentDomain.BaseDirectory + C_Config.ReadAppSetting("sHtmlPath");

            //我这里用另外一种方法实现自定义的模板，暂时没有用这种方法.
            //var config = new TemplateServiceConfiguration();//获取模板服务配置,需要自定义模板方法需要
            //config.BaseTemplateType = typeof(RazorFunc<>);//设置你自定义的模板
            //service = RazorEngineService.Create(config);//创建拥有自定义模板的引擎服务

            service = RazorEngineService.Create();//创建默认的

            //初始化文件的嵌套，及公共文件的模板的编译
            var sTemplateList = C_Config.ReadAppSetting("sCommonTemplatePath");
            if (!string.IsNullOrEmpty(sTemplateList))
            {
                var list = sTemplateList.Split(',').ToList();
                foreach(var m in list)
                {
                    string sPath =sTemplateBasePath+"\\"+m+".cshtml";
                    string fileStr = File.ReadAllText(sPath);
                    service.Compile(fileStr, m);
                }
            }
        }

        /// <summary>
        /// 解析文件里面的模板
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public string ParseFile(string filePath, object Model = null)
        {
            filePath =sTemplateBasePath+"\\"+filePath;
            string fileStr = File.ReadAllText(filePath);
            string str = ParseString(fileStr, Model);
            return str;
        }

        /// <summary>
        /// 解析模板
        /// </summary>
        /// <param name="sTemplateString">模板字符串</param>
        /// <param name="Model">传入的对象</param>
        /// <returns></returns>
        public string ParseString(string sTemplateString, object Model = null)
        {
            string str = service.RunCompile(sTemplateString, DateTime.Now.ToString("yyyyMMddHHmmssfff"), null, Model);
            return str;
        }

        /// <summary>
        /// 生成静态页面
        /// </summary>
        /// <param name="sfilePath"></param>
        /// <param name="sContent"></param>
        /// <returns></returns>
        public bool MakeHtml(string sfilePath,string sContent)
        {
            try
            {
                if (!Directory.Exists(sfilePath))
                {
                    Directory.CreateDirectory(sHtmlBasePath+sfilePath);
                }
                sfilePath = sHtmlBasePath+sfilePath+".html";
                System.IO.File.WriteAllText(sfilePath, sContent);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    
    }
}
 