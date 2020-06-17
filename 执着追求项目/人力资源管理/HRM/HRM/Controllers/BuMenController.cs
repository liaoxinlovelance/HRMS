using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Models;
namespace HRM.Controllers
{
    public class BuMenController : Controller
    {
        //
        // GET: /BuMen/

        public ActionResult Index()
        {
            return View();
        }

        //休假
        public ActionResult XiuJia()
        {
            return View();
        }
        //休假查询
        public ActionResult XiuJiaList(int page = 1, int rows = 5, string sort = "id", string order = "desc", string jldname = "")
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                // List<View_jiludan1> list = db.View_jiludan1.ToList();
                IQueryable<View_QJlist> jld = db.View_QJlist.AsQueryable();
                if (order == "asc")
                {
                    switch (sort)
                    {
                        case "ApplyID":
                            jld = jld.OrderByDescending(u => u.ApplyID);
                            break;
                        case "UserID":
                            jld = jld.OrderByDescending(u => u.UserID);
                            break;
                        case "DeptName":
                            jld = jld.OrderByDescending(u => u.DeptName);
                            break;
                        case "Applykaishi":
                            jld = jld.OrderByDescending(u => u.Applykaishi);
                            break;
                        case "Applyjieshu":
                            jld = jld.OrderByDescending(u => u.Applyjieshu);
                            break;
                        case "Applyyuanyin":
                            jld = jld.OrderByDescending(u => u.Applyyuanyin);
                            break;
                        case "ApplyType":
                            jld = jld.OrderByDescending(u => u.ApplyType);
                            break;
                        default:
                            jld = jld.OrderByDescending(u => u.BumenShenHe);
                            break;
                    }
                }
                else
                {
                    switch (sort)
                    {
                        case "ApplyID":
                            jld = jld.OrderBy(u => u.ApplyID);
                            break;
                        case "UserID":
                            jld = jld.OrderBy(u => u.UserID);
                            break;
                        case "DeptName":
                            jld = jld.OrderBy(u => u.DeptName);
                            break;
                        case "Applykaishi":
                            jld = jld.OrderBy(u => u.Applykaishi);
                            break;
                        case "Applyjieshu":
                            jld = jld.OrderBy(u => u.Applyjieshu);
                            break;
                        case "Applyyuanyin":
                            jld = jld.OrderBy(u => u.Applyyuanyin);
                            break;
                        case "ApplyType":
                            jld = jld.OrderBy(u => u.ApplyType);
                            break;
                        default:
                            jld = jld.OrderBy(u => u.BumenShenHe);
                            break;
                    }
                }

                //分页
                int total = jld.Count();
                if (total > 0)
                {
                    if (page <= 1)
                    {
                        jld = jld.Take(rows);
                    }
                    else
                    {
                        jld = jld.Skip((page - 1) * rows).Take(rows);
                    }
                }
                var obj = new
                {
                    total = total,
                    rows = jld.ToArray()
                };
                return Json(obj, JsonRequestBehavior.AllowGet);

            }
        }


        //休假审核
        public ActionResult XiuJiaShenHe(int id, string rad)
        {
            using (HrmDBEntities3 DB = new HrmDBEntities3())
            {
                Apply_jiludan jld = DB.Apply_jiludan.Find(id);
                jld.BumenShenHe = rad;
                //DB.Entry(DB.Apply_jiludan).State = 
                int i = DB.SaveChanges();
               
                var obj = new
                {
                    success = "true",
                    message = "OK"
                };
                
                return Json(obj, JsonRequestBehavior.AllowGet);
               
            }
        }

    }
}
