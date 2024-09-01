using System;
using System.Collections.Generic;

namespace Epro.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRecord> UserRecords { get; } = new List<UserRecord>();
}
