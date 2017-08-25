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
         tip:注意使用RazorEngine.Razor引擎的时候
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
        }

        /// <summary>
        /// 初始化引擎服务
        /// </summary>
        public void InitServices(List<string> sTemplateList)
        {
            //初始化文件的嵌套，及公共文件的模板的编译
            if (sTemplateList.Count>0)
            {
                foreach (var m in sTemplateList)
                {
                    string sPath = sTemplateBasePath + "\\" + m + ".cshtml";
                    if (File.Exists(sPath))
                    {//文件存在
                        string fileStr = File.ReadAllText(sPath);
                        service.Compile(fileStr, m);
                    }
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
        public bool MakeHtml(string sfilePath,string sFileName,string sContent)
        {
            string DirectoryPath = sHtmlBasePath + "\\" + sfilePath;
            try
            {
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                string sPath = DirectoryPath+ "\\"+sFileName+ ".html";
                System.IO.File.WriteAllText(sPath, sContent);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    
    }
}
 