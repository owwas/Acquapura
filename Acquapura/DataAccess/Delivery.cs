//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Acquapura.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Delivery
    {
        public long ID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<int> Delivered { get; set; }
        public Nullable<int> Received { get; set; }
        public Nullable<int> Balance { get; set; }
        public Nullable<decimal> CashReceived { get; set; }
        public Nullable<decimal> CashBalance { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
