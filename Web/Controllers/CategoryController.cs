using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// </summary>
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
    }
}