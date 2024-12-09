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
    
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.Sells = new HashSet<Sells>();
        }
    
        public int id_product { get; set; }
        public string product_name { get; set; }
        public int product_type_id { get; set; }
        public int product_creator_id { get; set; }
        public int product_seller_id { get; set; }
        public string product_description { get; set; }
        public string product_image { get; set; }
        public double product_retail_price { get; set; }
        public double product_wholesale_price { get; set; }
        public int product_status_id { get; set; }
    
        public virtual ProductCreators ProductCreators { get; set; }
        public virtual ProductSellers ProductSellers { get; set; }
        public virtual ProductStatuses ProductStatuses { get; set; }
        public virtual ProductTypes ProductTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sells> Sells { get; set; }
    }
}