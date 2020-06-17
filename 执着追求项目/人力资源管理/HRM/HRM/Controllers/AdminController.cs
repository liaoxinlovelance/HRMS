using HRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        HrmDBEntities3 DB = new HrmDBEntities3();
        //修改验证
        public ActionResult yanzheng()
        {
            //传密保
            string name = Session["Email"].ToString();
            var mibao = (from s in DB.User_YG where s.UserName == name select s.mibaowenti).FirstOrDefault();
            ViewBag.mibao = mibao;
            return View();
        }

        [HttpPost]
        public ActionResult yanzheng(string pwd1)
        {
            string name = Session["Email"].ToString();
            if (pwd1.Length == 0)
            {
                ViewBag.tishi = "请输入密保答案后进入安全设置";


            }
            else if (pwd1.Length != 0)
            {
                var anquan = DB.User_YG.FirstOrDefault(n => n.mimapwd == pwd1);
                if (anquan == null)
                {
                    ViewBag.tishi = "答案输入错误";

                }
                else
                {
                    RedirectToAction("/Admin/anquan");
                }
            }
            return View();
        }

        //用户维护
        public ActionResult admin()
        {
            return View();
        }
        //安全设置
        public ActionResult anquan()
        {
            string name = Session["Email"].ToString();
            var mibao = (from s in DB.User_YG where s.UserName == name select s.mibaowenti).FirstOrDefault();
            ViewBag.mibao = mibao;
            return View();
        }

        //修改密码
        public ActionResult update( string pwd2, string daan)
        {
            string name = Session["Email"].ToString();
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
               int i= db.proc_update_mima(name, pwd2, daan);
               if (i > 0)
               { return Content("修改成功！"); }
               else { return Content("修改失败！"); }
                
            }
            
            
        }

        //查询列表
        public ActionResult list(int page, int rows, string sort, string order, string cname = "")
        {

            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                //排序
                List<View_YG> list = db.View_YG.Where(c => c.UserName.Contains(cname)).ToList();

                //排序的方式：order
                switch (sort)
                {
                    case "UserID":
                        if (order == "desc")
                        {
                            list = list.OrderByDescending(m => m.UserID).ToList();
                        }
                        else
                        {
                            list = list.OrderBy(m => m.UserID).ToList();
                        }
                        break;
                    case "Age":
                        if (order == "desc")
                        {
                            list = list.OrderByDescending(m => m.Age).ToList();
                        }
                        else
                        {
                            list = list.OrderBy(m => m.Age).ToList();
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

        //部门、
        public ActionResult GetBrand()
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                List<Dept_bumen> list = db.Dept_bumen.ToList();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
        }

        //角色、
        public ActionResult Getjuese()
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                List<Role_juese> list = db.Role_juese.ToList();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
        }

        //删除

        /// <summary>
        /// 删除 int?:可空（null）int类型
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult Del(IList<int?> cid)
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                foreach (int id in cid)
                {
                    //删除
                    User_YG yg = db.User_YG.Find(id);
                    db.User_YG.Remove(yg);

                }
                int count = db.SaveChanges();
                return Content(count + "条删除成功！");
            }

        }



        //添加员工信息
        public ActionResult Add(User_YG yg)
        {

            DB.User_YG.Add(yg);
            int i = DB.SaveChanges();
            if (i <= 0)
            {
                return Content("添加失败");
            }
            else
            {
                return Content("添加成功");
            }
        }
        //修改信息
        public ActionResult Editcar(int GhaoID)
        {


            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                //int GhaoID, string UserName, int RoleID, string Sex, int DeptID, int Age, string Education, string UserPwd, string IDCar, int Salary, string mibaowenti, string mimapwd,int UserID
                //int i = db.proc_update_yg(GhaoID, UserName, RoleID, Sex, DeptID, Age, Education, UserPwd, IDCar, Salary, mibaowenti, mimapwd, UserID);
                //if (i > 0)
                //{ return Content("修改成功！"); }
                //else { return Content("修改失败！"); }   
                return Content("修改成功！");

            }
            
        }



        //修改权限
        public ActionResult updateqx() {
            return View();
        }
    }

}


