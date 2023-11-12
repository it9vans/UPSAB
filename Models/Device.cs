using System;
using System.Collections.Generic;

namespace UPSAB;

public partial class Device
{
    public int Id { get; set; }

    public string DeviceName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
