using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VeLogDataApp.Models
{
    public class LocationData
    {
        [Required(ErrorMessage = "* Required")]
        public string Division { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Campus { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Course { get; set; }
    
    }
}