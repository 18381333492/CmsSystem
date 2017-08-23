using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Server
{
    public partial class FuncHelper
    {
        /// <summary>
        /// 根据栏目获取栏目生成文件的路径
        /// </summary>
        /// <returns></returns>
        public string GetHtmlPath(TG_Category category, List<TG_Category> categoryList)
        {
            string sPath = string.Empty;
            if (category.CategoryId != 0)
            {//子级栏目
                TG_Category parentCategory = null;
                do
                {
                    parentCategory = categoryList.FirstOrDefault(m => m.ID == category.CategoryId);
                    sPath = sPath + "\\" + parentCategory.sEnName.ToLower();
                }
                while (parentCategory != null && parentCategory.CategoryId != 0);
                sPath = sPath.TrimStart('\\');
            }
            return sPath;
        }
    }
}