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
using RazorEngine.Text;

namespace Web
{
    public class RazorHelper
    {
        /*
        tip:注意使用RazorEngine.Razor引擎的时候
        */
        //设置模板和页面目录的位置
        private static readonly string sTemplateBasePath=AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.ReadAppSetting("sTemplatePath");
        private static readonly string sHtmlBasePath= AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.ReadAppSetting("sHtmlPath");

        //引擎服务
        private static IRazorEngineService service;

        private static TemplateServiceConfiguration config;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static RazorHelper()
        {
            //我这里用另外一种方法实现自定义的模板，暂时没有用这种方法.
            var config = new TemplateServiceConfiguration();//获取模板服务配置,需要自定义模板方法需要
            config.BaseTemplateType = typeof(RazorFunc<>);//设置你自定义的模板
            service = RazorEngineService.Create(config);//创建拥有自定义模板的引擎服务
            ///var config = new TemplateServiceConfiguration();
            //config.TemplateManager.Resolve(string name);
            //service = RazorEngineService.Create();//创建默认的
        }

        
        /// <summary>
        /// 预编译模板
        /// </summary>
        /// <param name="sFileStr"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static bool PrevCompileTemplate(string sFileStr,string sKey)
        {
            try
            {
                service.AddTemplate(sKey, sFileStr);
                service.Compile(sKey);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }     
        }

        /// <summary>
        /// 输出html
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IEncodedString Raw(string content)
        {
            return new RawString(content);
        }


        /// <summary>
        /// 解析文件里面的模板
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string ParseFile(string filePath, object Model = null)
        {
            //filePath =sTemplateBasePath+"\\"+filePath;
          //  string fileStr = File.ReadAllText(filePath);
            string str = ParseString(filePath, Model);
            return str;
        }

        /// <summary>
        /// 解析模板
        /// </summary>
        /// <param name="sTemplateString">模板字符串</param>
        /// <param name="Model">传入的对象</param>
        /// <returns></returns>
        public static string ParseString(string sTemplateString, object Model = null)
        {
            string str = service.Run(sTemplateString, null, Model);
           // Engine.Razor.RunCompile(sTemplateString)
            return str;
        }

        /// <summary>
        /// 生成静态页面
        /// </summary>
        /// <param name="sfilePath"></param>
        /// <param name="sContent"></param>
        /// <returns></returns>
        public static bool MakeHtml(string sfilePath,string sFileName,string sContent)
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
            catch (Exception ex)
            {
                return false;
            }
        }
    
    }
}
 