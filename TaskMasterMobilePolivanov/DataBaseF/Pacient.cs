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
    
    public partial class Pacient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pacient()
        {
            this.OrderInfo = new HashSet<OrderInfo>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string EIN { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> PassportS { get; set; }
        public Nullable<int> PassportN { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public Nullable<System.DateTime> DateEnter { get; set; }
        public Nullable<System.DateTime> DateExit { get; set; }
        public Nullable<bool> Attempt { get; set; }
        public Nullable<int> IdInsurancePolicy { get; set; }
    
        public virtual InsurancePolicy InsurancePolicy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}
