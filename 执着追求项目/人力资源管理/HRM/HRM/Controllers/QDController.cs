using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Models;
using System.Data;
using System.Collections;

namespace HRM.Controllers
{
    public class QDController : Controller
    {
        //
        // GET: /QD/

        //签到管理部分
        public ActionResult FQqiandao(string name)
        {
            ViewBag.name = name;
            return View();
        }

        public ActionResult FQQD(qiandao qd)
        {

            if (!ModelState.IsValid)
            {
                using (HrmDBEntities3 db = new HrmDBEntities3())
                {
                    qd.QDtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    db.qiandao.Add(qd);
                    db.SaveChanges();


                }
                //return Content("<script>alert('发起签到成功！')</script>");
                //return Json("发起签到成功！", JsonRequestBehavior.AllowGet);
                return Content("<script language='javascript' type='text/javascript'>alert('发起签到成功！');history.go(-1);location.reload();</script>");
            }
            else
            {
                return Content("失败");
            }
        }
        public ActionResult getqiandaostate()
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                List<qiandao> list = db.qiandao.ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult jieshuqiandao(qiandao qd)
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {


                db.Entry(qd).State = EntityState.Modified;
                db.SaveChanges();
                //return Content("成功结束！");
                //return Content("<script>alert('成功结束！')</script>");
                return Content("<script language='javascript' type='text/javascript'>alert('成功结束！');history.go(-1);location.reload();</script>");
            }

        }
        public ActionResult chongkaiqiandao(qiandao qd)
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {


                db.Entry(qd).State = EntityState.Modified;
                db.SaveChanges();
                // return Content("成功重开签到！");
                // return Content("<script>alert('成功重开签到！')</script>");
                return Content("<script language='javascript' type='text/javascript'>alert('成功重开签到！');history.go(-1);location.reload();</script>");

            }
        }
        public ActionResult shanchuqiandao(int id)
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                db.qiandao.Remove(db.qiandao.Find(id));
                db.SaveChanges();
                //return Content("成功删除签到！");
                //return Content("<script>alert('成功删除签到！')</script>");
                return Content("<script language='javascript' type='text/javascript'>alert('成功删除签到');history.go(-1);location.reload();</script>");

            }
        }
        //签到查看部分
        public ActionResult chayue()
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                List<qiandao> list = (from qd in db.qiandao where qd.QDstate == false select qd).ToList();
               // ViewBag.listxm = "张三,李四,王五,admin,麻子,张无忌,达到DAS,张明德,李逵,杨过";

                var list2 = from user in db.User_YG select user.UserName;
                List<string> jihe = list2.ToList();
                //List转化为数组
                //string[] shuzu = jihe.ToArray();

                jihe.Sort();//进行排序
                var result = String.Join(",", jihe.ToArray());//把List集合转化成string类型，变成一条字符串
                ViewBag.list = list;

                ViewBag.listxm2 = result;
                return View(new { list, jihe });
            }

        }
        //签到打卡部分
        public ActionResult DaKa(string name)
        {
            ViewBag.name = name;
            return View();


        }
        public ActionResult wyDaKa()
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {
                string name = Session["Email"].ToString();
                ViewBag.name = name;
                //List<qiandao> list = (from qd in db.qiandao where qd.QDZ ==  select qd).ToList();
                //var dt4 = (from des in db.ModelsVehicleRecognition
                //           where (des.PlateNum.Contains("A3"))
                //           select new { plateMun = des.PlateNum });
                //var var4 = dt4.ToList();

                //var dt = (from qd in db.qiandao
                //          where (qd.QDZ.Contains(name))
                //          select new { qdz = qd.QDZ });
                //var list = dt.ToList();

                //List<qiandao> list = (from qd in db.qiandao
                //                      where (qd.QDZ.Contains(name))
                //                      select new { qdz = qd.QDZ }).ToList();

                List<qiandao> list = (from qd in db.qiandao select qd).ToList();

                //               在linq to sql实现where条件中in或exists的用法：

                //from a in TableA where !(from b in TableB where 条件 select b.ID).Contains(a.ID)
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult chenggongqiandao(qiandao qd)
        {
            using (HrmDBEntities3 db = new HrmDBEntities3())
            {


                db.Entry(qd).State = EntityState.Modified;
                db.SaveChanges();
                // return Content("成功重开签到！");
                //return Content("<script>alert('成功签到！')</script>");
                return Content("<script language='javascript' type='text/javascript'>alert('成功签到');history.go(-1);location.reload();</script>");
            }
        }
        //柱状图
        public ActionResult zhuzhuangtu()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult zhuzhuangtu()
        //{
        //    using(HrmDBEntities3 db=new HrmDBEntities3()){
        //        //得到签到表的签到名称数据
        //        List<string> list = (from qd in db.qiandao select qd.QDtitle).ToList();
        //        //得到总数
        //        var count = from user in db.User_YG select user.UserName.Count();
               
        //    }
        //}

        }
}