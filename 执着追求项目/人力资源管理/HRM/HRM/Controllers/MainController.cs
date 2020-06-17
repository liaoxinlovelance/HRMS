using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
       
        public ActionResult Main(string admin)
        {
            ViewBag.time = DateTime.Now.ToString("yyyy"+"年"+"MM"+"月"+"dd"+"日");
            //权限判断
            ViewBag.quanxian = admin;
            string name = Session["Email"].ToString();
            ViewBag.name = name;
            return View();
        }

    }
}
