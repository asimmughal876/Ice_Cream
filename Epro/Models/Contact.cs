using System;
using System.Collections.Generic;

namespace Epro.Models;

public partial class Contact
{
    public int CId { get; set; }

    public string CName { get; set; } = null!;

    public string CEmail { get; set; } = null!;

    public long CPhone { get; set; }

    public string CMessage { get; set; } = null!;
}
