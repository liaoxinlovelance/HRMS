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
    
    public partial class Dept_bumen
    {
        public Dept_bumen()
        {
            this.User_YG = new HashSet<User_YG>();
        }
    
        public int DeptID { get; set; }
        public string DeptName { get; set; }
    
        public virtual ICollection<User_YG> User_YG { get; set; }
    }
}
