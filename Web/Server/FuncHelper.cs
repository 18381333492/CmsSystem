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
        private Entities Context;

        private static FuncHelper instance = null;

        /// <summary>
        /// 获取站点信息
        /// </summary>
        public TG_WebSite WebSite
        {
            get
            {
                var web=Context.TG_WebSite.FirstOrDefault();
                return web;
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

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public FuncHelper()
        {
            Context = new Entities();
        }

        //********************************************************常用的方法的封装************************************************************//


        /// <summary>
        /// 根据主键ID获取实体对象
        /// </summary>
        /// <returns></returns>
        public T GetModel<T>(int ID) where T: class, new()
        {
            T item = Context.Set<T>().Find(ID); 
            return item;
        }

        /// <summary>
        /// 获取实体所有的数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetModelList<T>() where T : class, new()
        {
            List<T> list = Context.Set<T>().ToList();
            return list;
        }



    }
}