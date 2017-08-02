using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{

    /// <summary>
    /// 模板控制器
    /// </summary>
    public class TempletController : Controller
    {
        // GET: Templet
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
            return Json(new { rows = new List<object>(), total = 0 });
        }
    }
}