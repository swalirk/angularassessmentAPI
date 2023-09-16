﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace assessmentApi.Models;

public partial class Aotable
{
    public Aotable()
    {
        this.Forms = new HashSet<Form>();
    }
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public string? Comment { get; set; }

    public int? History { get; set; }

    public int? Boundary { get; set; }

    public int? Log { get; set; }

    public int? Cache { get; set; }

    public int? Notify { get; set; }

    public int? Identifier { get; set; }

    public int Premium { get; set; }

    // public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
   
    public virtual ICollection<Form> Forms { get; }
}
