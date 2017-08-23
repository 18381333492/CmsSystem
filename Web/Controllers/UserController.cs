using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;
using Web.Models;
using Common;

namespace Web.Controllers
{
    /// <summary>
    /// 后台用户控制器
    /// </summary>
    public class UserController : BaseController
    {

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int ID)
        {
            ViewBag.ID = ID;
            return View();
        }

        /// <summary>
        /// 分页获取用户数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo)
        {
            var query = mangae.db.TG_User.Where(m=>m.iUserType==0).OrderByDescending(m => m.ID).AsQueryable();
            if (!string.IsNullOrEmpty(pageInfo.sKeyWord))
            {//模糊查询
                query = query.Where(m => m.sUserName.Contains(pageInfo.sKeyWord) || m.sRealName.Contains(pageInfo.sKeyWord));
            }
            var total = query.Count();
            query = query.Skip((pageInfo.page - 1) * pageInfo.rows).Take(pageInfo.rows);

            result.pageResult.total = total;
            result.pageResult.rows = query;
            return Content(result.pageResult.toJson());
        }
        

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="user"></param>
        public void Insert(TG_User user)
        {
            if (mangae.db.TG_User.Any(m => m.sUserName == user.sUserName))
            {
                result.info = "已存在相同账户管理员";
            }
            else
            {
                user.sUserName = user.sUserName.Trim();
                user.iUserType = 0;
                user.sPassword = C_Security.MD5(user.sPassword);
                mangae.Add<TG_User>(user);
                result.success = mangae.SaveChange();
            }
        }

        /// <summary>
        ///修改密码
        /// </summary>
        /// <param name="user"></param>
        public void Update(TG_User user)
        {
            var item = mangae.db.TG_User.Find(user.ID);
            item.sPassword = C_Security.MD5(user.sPassword);
            result.success = mangae.SaveChange();
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="Ids"></param>
        public void Cancel(string Ids)
        {
            result.success = mangae.Delete<TG_User>(Ids);
        }


        public ActionResult Login()
        {
            return View();
        }
    }
}