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
    
    public partial class Tiaoxiu
    {
        public int ApplyID { get; set; }
        public int UserID { get; set; }
        public System.DateTime Applykaishi { get; set; }
        public System.DateTime Applyjieshu { get; set; }
        public string kaishi
        {
            get { return Applykaishi.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        public string jieshu
        {
            get { return Applyjieshu.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        public string Applyanpai { get; set; }
        public string BumenShenHe { get; set; }
        public string XiaoquShenHe { get; set; }
    
        public virtual User_YG User_YG { get; set; }
    }
}
