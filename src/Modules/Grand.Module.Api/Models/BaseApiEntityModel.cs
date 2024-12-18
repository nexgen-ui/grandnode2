﻿using System.ComponentModel.DataAnnotations;

namespace Grand.Module.Api.Models;

public class BaseApiEntityModel
{
    [Key] public string Id { get; set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
}