using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Models;
namespace HRM.Controllers
{
    public class WaiqinController : Controller
    {
        //
        // GET: /Waiqin/

        HrmDBEntities3 DB = new HrmDBEntities3();
        //调休申请
        public ActionResult waiqin()
        {
            string name = Session["Email"].ToString();
            ViewBag.name = name;
            return View();
        }
        public ActionResult list(int page, int rows, string sort, string order, string cname = "")
        {

            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                string name = Session["Email"].ToString();
                List<View_Waiqin> list = db.View_Waiqin.Where(c => c.UserName.Contains(cname)).Where(c=>c.UserName.StartsWith(name)).ToList();
                //排序的方式：order
                switch (sort)
                {
                    case "ApplyID":
                        if (order == "asc")
                        {
                            list = list.OrderByDescending(m => m.ApplyID).ToList();
                        }
                        else
                        {
                            list = list.OrderBy(m => m.ApplyID).ToList();
                        }
                        break;
                    case "Applykaishi":
                        if (order == "asc")
                        {
                            list = list.OrderByDescending(m => m.Applykaishi).ToList();
                        }
                        else
                        {
                            list = list.OrderBy(m => m.Applykaishi).ToList();
                        }
                        break;

                    default:
                        break;
                }
                //分页
                //首先定义变量保存总行数
                int zong = list.Count();

                if (page == 1)//第一页直接取前三条 
                {
                    list = list.Take(rows).ToList();//Take:取出指定序列的元素 
                }
                else
                {
                    list = list.Skip((page - 1) * rows).Take(rows).ToList();//Skip：跳过指定序列的元素
                }

                //转换json
                var json = new
                {
                    total = zong,
                    rows = list.ToArray()
                };
                return Json(json, JsonRequestBehavior.AllowGet);

            }


        }

        //model转换时间格式
        //public string Time { get { return Starttime.ToString("yyyy-MM-dd"); } }
        //外勤
        [HttpPost]
        public ActionResult Add(string kaishi, string jieshu, string dizhi, string liyou)
        {
            if (dizhi.Length < 1)
            {
                return Content("请输入目的地");
            }
            else if (kaishi.Length < 1)
            {
                return Content("请假日期不能为空！");
            }
            else if (jieshu.Length < 1)
            {
                return Content("请假日期不能为空！");
            }
          
            string sh = "未审核";
            string sh2 = "未审核";
            string name = Session["Email"].ToString();
            DateTime kaishi1 = DateTime.Parse(kaishi);
            DateTime jieshu1 = DateTime.Parse(jieshu);
            int i = DB.proc_add_waiqin(name, kaishi1, jieshu1, dizhi, liyou, sh, sh2);
            if (i > 0)
            {
                return Content("添加成功！");

            }
            else { return Content("添加失败！"); }
        }



        //部门审核
        //外勤
        public ActionResult Waiqinjiemian()
        {
            return View();
        }
        //外勤查询
        public ActionResult WaiqinList(int page = 1, int rows = 5, string sort = "id", string order = "desc", string jldname = "")
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                // List<View_jiludan1> list = db.View_jiludan1.ToList();
                IQueryable<View_Waiqin> jld = db.View_Waiqin.AsQueryable();
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
                        case "Applydizhi":
                            jld = jld.OrderByDescending(u => u.Applydizhi);
                            break;
                        case "ApplyLiyou":
                            jld = jld.OrderByDescending(u => u.ApplyLiyou);
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
                        case "Applydizhi":
                            jld = jld.OrderBy(u => u.Applydizhi);
                            break;
                        case "ApplyLiyou":
                            jld = jld.OrderBy(u => u.ApplyLiyou);
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
        //外勤审核
        public ActionResult WaiqinShenHe(int id, string rad)
        {
            using (HrmDBEntities3 DB = new HrmDBEntities3())
            {
                Waiqing jld = DB.Waiqing.Find(id);
                jld.BumenShenHe = rad;
                int i = DB.SaveChanges();

                var obj = new
                {
                    success = "true",
                    message = "OK"
                };

                return Json(obj, JsonRequestBehavior.AllowGet);

            }
        }


        //校区审核
        //外勤
        public ActionResult WaiqinjiemianXQ()
        {
            return View();
        }
        //外勤查询
        public ActionResult WaiqinListXQ(int page = 1, int rows = 5, string sort = "id", string order = "desc")
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                // List<View_jiludan1> list = db.View_jiludan1.Where(u => u.BumenShenHe.Contains(rad1)).ToList();
                string rad = "部门同意";
                IQueryable<View_Waiqin> jld = db.View_Waiqin.Where(u => u.BumenShenHe.StartsWith(rad));


                //IQueryable<View_jiludan1> jld = db.View_jiludan1.AsQueryable();

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
                        case "Applydizhi":
                            jld = jld.OrderByDescending(u => u.Applydizhi);
                            break;
                        case "ApplyLiyou":
                            jld = jld.OrderByDescending(u => u.ApplyLiyou);
                            break;
                        default:
                            jld = jld.OrderByDescending(u => u.XiaoquShenHe);
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
                        case "Applydizhi":
                            jld = jld.OrderBy(u => u.Applydizhi);
                            break;
                        case "ApplyLiyou":
                            jld = jld.OrderBy(u => u.ApplyLiyou);
                            break;
                        default:
                            jld = jld.OrderBy(u => u.XiaoquShenHe);
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


        //外勤审核
        public ActionResult WaiqinShenHeXQ(int id, string rad)
        {
            using (HrmDBEntities3 DB = new HrmDBEntities3())
            {
                Waiqing jld = DB.Waiqing.Find(id);
                jld.XiaoquShenHe = rad;
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