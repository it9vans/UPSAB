using System;
using System.Collections.Generic;

namespace UPSAB;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Order> OrderClients { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderExecutors { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

}
