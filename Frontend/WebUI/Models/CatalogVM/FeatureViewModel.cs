﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.CatalogVM
{
    public class FeatureViewModel
    {
        [Required]
        public int Duration { get; set; }
    }
}
