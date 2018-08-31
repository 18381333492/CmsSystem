﻿using System;
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

namespace RazorBase
{
    public class RazorHelper
    {
        /*
        tip:注意使用RazorEngine.Razor引擎的时候
        */
        //设置模板和页面目录的位置
        private static readonly string sTemplateBasePath=AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.ReadAppSetting("sTemplatePath");
        private static readonly string sHtmlBasePath= AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.ReadAppSetting("sHtmlPath");

        /// <summary>
        /// 预编译模板
        /// </summary>
        /// <param name="sFileStr"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static bool PrevCompileTemplate(string sKey,string sTemplateStr)
        {
            try
            {
                Engine.Razor.AddTemplate(sKey, sTemplateStr);
                Engine.Razor.Compile(sKey);
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
        /// 解析模板
        /// </summary>
        /// <param name="sTemplateName">模板标识</param>
        /// <param name="Model">传入的对象</param>
        /// <returns></returns>
        public static string ParseString(string sTemplateName, object Model = null)
        {
            string str = Engine.Razor.Run(sTemplateName, null, Model);
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
 