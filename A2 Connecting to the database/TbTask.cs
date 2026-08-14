using System;
using System.Collections.Generic;

namespace A2_Connecting_to_the_database;

public partial class TbTask
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}
