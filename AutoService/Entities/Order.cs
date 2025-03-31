//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoService.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.OrderProduct = new HashSet<OrderProduct>();
        }
    
        public int OrderID { get; set; }
        public string OrderStatus { get; set; }
        public System.DateTime OrderDeliveryDate { get; set; }
        public int OrderPickupPoint { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string ClientFullName { get; set; }
        public int ReceiptCode { get; set; }
    
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual PickupPoint PickupPoint { get; set; }
    }
}
