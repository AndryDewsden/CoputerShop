//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoputerShop.ApplicationData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.Sells = new HashSet<Sells>();
        }
    
        public int id_order { get; set; }
        public int order_user_id { get; set; }
        public string order_indification_number { get; set; }
        public System.DateTime order_creating_date { get; set; }
        public Nullable<System.DateTime> order_clossing_date { get; set; }
        public int order_status_id { get; set; }
    
        public virtual OrderStatuses OrderStatuses { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sells> Sells { get; set; }
    }
}
