//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskMasterMobilePolivanov.DataBaseF
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderComplate
    {
        public int Id { get; set; }
        public Nullable<int> IdOrder { get; set; }
        public Nullable<int> IdUser { get; set; }
        public Nullable<System.DateTime> DateComplate { get; set; }
    
        public virtual OrderInfo OrderInfo { get; set; }
        public virtual UserLab UserLab { get; set; }
    }
}
