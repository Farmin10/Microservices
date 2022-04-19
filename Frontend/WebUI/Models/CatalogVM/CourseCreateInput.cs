using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.CatalogVM
{
    public class CourseCreateInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Description { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Display(Name ="Category")]
        public string CategoryId { get; set; }
        public string Picture { get; set; }
        public IFormFile PhotoFormFile { get; set; }
    }
}
