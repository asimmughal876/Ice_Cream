using System;
using System.Collections.Generic;

namespace Epro.Models;

public partial class Feedback
{
    public int FId { get; set; }

    public string? FUserName { get; set; }

    public string? FMessage { get; set; }
}
