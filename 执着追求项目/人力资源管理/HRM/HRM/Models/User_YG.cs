//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_YG
    {
        public User_YG()
        {
            this.Apply_jiludan = new HashSet<Apply_jiludan>();
            this.Chuchai = new HashSet<Chuchai>();
            this.Tiaoxiu = new HashSet<Tiaoxiu>();
            this.Waiqing = new HashSet<Waiqing>();
        }
    
        public int UserID { get; set; }
        public int GhaoID { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public int DeptID { get; set; }
        public string UserPwd { get; set; }
        public string IDCar { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public Nullable<System.DateTime> Starttime { get; set; }
        public string Education { get; set; }
        public decimal Salary { get; set; }
        public string mibaowenti { get; set; }
        public string mimapwd { get; set; }
    
        public virtual ICollection<Apply_jiludan> Apply_jiludan { get; set; }
        public virtual ICollection<Chuchai> Chuchai { get; set; }
        public virtual Dept_bumen Dept_bumen { get; set; }
        public virtual Role_juese Role_juese { get; set; }
        public virtual ICollection<Tiaoxiu> Tiaoxiu { get; set; }
        public virtual ICollection<Waiqing> Waiqing { get; set; }
    }
}
