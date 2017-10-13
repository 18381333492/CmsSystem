using RazorBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Server
{
    /// <summary>
    /// 通用公共函数
    /// </summary>
    public partial class FuncHelper
    {

        private static FuncHelper instance = null;

        /// <summary>
        ///  初始化公共编译模板
        /// </summary>
        public void initRazorServices()
        {
            using (var db = new Entities())
            {
                List<string> list = db.TG_Templet.Where(m => m.bIsCompile == true).Select(m => m.sTempletEnName).ToList();
                //初始化公共模板的编译
                RazorHelper.Instance.InitServices(list);
            }
        }


        /// <summary>
        /// 获取站点信息
        /// </summary>
        public TG_WebSite WebSite
        {
            get
            {
                using (var db = new Entities())
                {
                    var web = db.TG_WebSite.FirstOrDefault();
                    return web;
                }
            }
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static FuncHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new FuncHelper();
                return instance;
            }
        }

        //********************************************************常用的方法的封装************************************************************//

    
        /// <summary>
        /// 根据主键ID获取实体对象
        /// </summary>
        /// <returns></returns>
        public T GetModel<T>(int ID) where T: class, new()
        {
            using (var db = new Entities())
            {
                T item = db.Set<T>().Find(ID);
                return item;
            }
        }

        /// <summary>
        /// 获取实体所有的数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetModelList<T>() where T : class, new()
        {
            using (var db = new Entities())
            {
                List<T> list = db.Set<T>().ToList();
                return list;
            }
        }


        /// <summary>
        /// 获取所有显示的栏目
        /// </summary>
        public List<TG_Category> GetShowCategory()
        {
            using (var db = new Entities())
            {
                var list = db.TG_Category.Where(m => m.bIsShowNav == true && m.CategoryId == 0)
                                          .OrderBy(m => m.iOrder).ToList();
                return list;
            }
        }


        /// <summary>
        /// 根据栏目的英文标识获取栏目
        /// </summary>
        /// <param name="sEnName"></param>
        /// <returns></returns>
        public TG_Category GetCategoryByEnName(string sEnName)
        {
            using (var db = new Entities())
            {
                var category = db.TG_Category.FirstOrDefault(m => m.sEnName == sEnName);
                return category;
            }
        }

        /// <summary>
        /// 根据栏目主键ID获取栏目下的文章列表
        /// </summary>
        /// <param name="iCategoryId"></param>
        /// <returns></returns>
        public List<TG_Article> GetArticleListByCategoryId(int iCategoryId)
        {
            using (var db = new Entities())
            {
                var ArticleList = db.TG_Article.Where(m => m.iCategoryId == iCategoryId).ToList();
                return ArticleList;
            }
        
        }



    }
}