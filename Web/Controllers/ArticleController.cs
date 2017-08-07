using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;

namespace Web.Controllers
{
    /// <summary>
    /// 文章控制器
    /// </summary>
    public class ArticleController : BaseController
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }


        public ActionResult List()
        {
            result.pageResult.total = 0;
            result.pageResult.rows = new List<object>();
            return Content(result.toJson());
        }

    }
}