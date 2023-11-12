using System;
using System.Collections.Generic;

namespace UPSAB;

public partial class Order
{
    public int Id { get; set; }

    public int DeviceId { get; set; }

    public int DefectId { get; set; }

    public int ClientId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int StatusId { get; set; }

    public int? ExecutorId { get; set; }

    public string? ExecutorComment { get; set; }

    public virtual User Client { get; set; } = null!;

    public virtual Defect Defect { get; set; } = null!;

    public virtual Device Device { get; set; } = null!;

    public virtual User? Executor { get; set; }

    public virtual Status Status { get; set; } = null!;
}
