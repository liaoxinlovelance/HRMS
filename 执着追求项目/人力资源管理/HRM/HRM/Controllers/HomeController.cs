using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace HRM.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        HrmDBEntities3 DB = new HrmDBEntities3();

        public ActionResult Login()
        {
            
         
            if (Request.Cookies["Email"] != null)
            {
                HttpCookie aCookie = Request.Cookies["Email"];
                ViewBag.yanzhen = Server.HtmlEncode(aCookie.Value);

            }
            return View();
        }



          [HttpPost]
        public ActionResult Login(User_YG yg, string Email, string PassWord)
        {

   
            if (Email.Length > 0 && PassWord.Length > 0)
            {
                if (ModelState.IsValid && Shujuyanzheng( Email, PassWord))
                {
                    var Luser = DB.User_YG.Where(n => n.UserName == Email).FirstOrDefault();
                    HttpCookie aCookie = new HttpCookie("Email");
                    aCookie.Value = Luser.UserName;
                    aCookie.Expires = DateTime.Now.AddMinutes(2);//设置Cookie 的过期时间
                    System.Web.HttpContext.Current.Response.Cookies.Add(aCookie);
                    HttpCookie Cookie = System.Web.HttpContext.Current.Response.Cookies.Get("Email");
                    Session["Email"] = Luser.UserName;
                    string name=Session["Email"].ToString();
                    //通过姓名和密码查询他的职位
                    var one = (from s in DB.User_YG where s.UserName == Email && s.UserPwd == PassWord select s.RoleID).FirstOrDefault();
                    ViewBag.name = Email;
                    switch (one)
                    {
                        case 5:
                            return RedirectToAction("Main", "Main", new { admin = "admin" });
                        case 4:
                            return RedirectToAction("Main", "Main", new { admin = "人事专员" });
                        case 3:
                            return RedirectToAction("Main", "Main", new { admin = "部门员工" });
                        case 2:
                            return RedirectToAction("Main", "Main", new { admin = "部门主管" });
                        case 1:
                            return RedirectToAction("Main", "Main", new { admin = "校区主任" });
                        default:
                            return RedirectToAction("Login");
                    }
                   
                 
                }
                else
                {
                    ViewBag.yanzheng = "用户名或密码错误";
                    return View();
                }
            }
            else
            {
                ViewBag.yanzheng = "用户名或密码不能为空";
                return View();
            }
           
        }









      
         


        public bool Shujuyanzheng(string UserName, string UserPwd)
        {

            var u = DB.User_YG.FirstOrDefault(n => n.UserName == UserName && n.UserPwd == UserPwd);
            if (u == null)
            {
                return false;
            }
            else
            {
                FormsAuthentication.SetAuthCookie(UserName, false);
                return true;
            }


        }
    }

    
}
