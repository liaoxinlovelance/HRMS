using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Staff/
        HrmDBEntities3 DB = new HrmDBEntities3();


        //休假申情
        public ActionResult appraisal() {
            string name = Session["Email"].ToString();
            ViewBag.name = name;
            return View();
        }
        public ActionResult list(int page, int rows, string sort, string order, string cname = "")
        {

            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                string name = Session["Email"].ToString();
                List<View_QJlist> list = db.View_QJlist.Where(c => c.UserName.Contains(cname)).Where(c=>c.UserName.StartsWith(name)).ToList();
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
       //请假
        [HttpPost]
        public ActionResult Add( string kaishi, string jieshu, string qjsy, string type)
        {
            if (qjsy.Length < 1) {
                return Content("请描述请假理由");
            }
            else if (kaishi.Length<1) {
                return Content("请假日期不能为空！");
            }
            else if (jieshu.Length < 1)
            {
                return Content("请假日期不能为空！");
            }
            else if (type.Length < 1) {
                return Content("请选择类型！");
            }
            int typeid = 1;
            string sh = "未审核";
            string sh2 = "未审核";
            string name = Session["Email"].ToString();
            DateTime kaishi1 = DateTime.Parse(kaishi);
            DateTime jieshu1 = DateTime.Parse(jieshu);
            int i = DB.proc_add_qj(name, typeid, kaishi1, jieshu1, qjsy, type, sh,sh2);
            if (i > 0)
            {
                return Content("添加成功！");
             
            }
            else { return Content("添加失败！"); }
        }

    }
}
