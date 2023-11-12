using System;
using System.Collections.Generic;

namespace UPSAB;

public partial class Defect
{
    public int Id { get; set; }

    public string DefectName { get; set; } = null!;

    public int RepairPrice { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
