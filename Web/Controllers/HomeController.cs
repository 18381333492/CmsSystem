using RazorBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public void make()
        {
            var t = new student();
            t.name = "TT";
            t.age = 23;
            t.school = "四川理工学院";
            t.score = 98;
            var list = new List<student>();
            list.Add(t);
            list.Add(t);
           
            string content = RazorHelper.Instance.ParseFile("Test.cshtml", new { list= list, sTitle="我的RazorEngine成功了!" });
            RazorHelper.Instance.MakeHtml("Test.html", content);
        }



    }
}