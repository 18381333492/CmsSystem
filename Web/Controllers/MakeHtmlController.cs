using RazorBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;
using Web.Models;

namespace Web.Controllers
{

    /// <summary>
    /// 生成页面的控制器
    /// </summary>
    public class MakeHtmlController : BaseController
    {
        // GET: MakeHtml
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 生成栏目页
        /// </summary>
        /// <param name="Ids"></param>
        public void MakeCategoryPage(string Ids)
        {
            var CategoryList = mangae.db.TG_Category.OrderBy(m=>m.ID).AsQueryable();//栏目数据
            var TempletList = mangae.db.TG_Templet.OrderBy(m => m.ID).AsQueryable();//模板数据
            if (!string.IsNullOrEmpty(Ids))
            {
                var listIds = Ids.Split(',').Select(m=>Convert.ToInt32(m)).ToList();
                CategoryList = CategoryList.Where(m => listIds.Contains(m.ID));
                //获取栏目下的所有模板ID
                var listIds_Templet = CategoryList.Select(m => m.iTemplateId.Value).ToList();
                TempletList = TempletList.Where(m => listIds_Templet.Contains(m.ID));
            }
            foreach(var category in CategoryList)
            {
                var templet = TempletList.Where(m => m.ID == category.iTemplateId).SingleOrDefault();
                if (templet != null)
                {
                   string templetHtmlString=RazorHelper.Instance.ParseFile(templet.sTempletEnName+".cshtml", category);
                   string sHtmlPath = GetHtmlPath(category);
                   var res=RazorHelper.Instance.MakeHtml(sHtmlPath, templetHtmlString);
                }
            }

        }

        /// <summary>
        /// 根据栏目获取栏目生成文件的路径
        /// </summary>
        /// <returns></returns>
        public string GetHtmlPath(TG_Category category)
        {
            string sPath = string.Empty;
            if (category.CategoryId == 0)
            {//一级栏目
                sPath = category.sEnName.ToLower();
            }
            else
            {//子级栏目
                var categoryList = mangae.db.TG_Category.ToList();
                TG_Category parentCategory = null;
                do
                {
                    parentCategory = categoryList.FirstOrDefault(m=>m.ID== category.CategoryId);
                    sPath =sPath+"\\"+parentCategory.sEnName.ToLower();
                }
                while (parentCategory!=null&&parentCategory.CategoryId!=0);
            }

            return sPath;
        }




    }
}