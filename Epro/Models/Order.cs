using System;
using System.Collections.Generic;

namespace Epro.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? TotalPrice { get; set; }

    public string? OrderStatus { get; set; }

    public int? OrderUserId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual UserRecord? OrderUser { get; set; }
}
