using MvcValidationExtensions.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VeLogDataApp.Models
{
   public class VeLogDataView
    {

        
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        [UIHint("Start Mileage")]
        [Required(ErrorMessage = "* Required")]
        [Range(1, Double.MaxValue,ErrorMessage ="Should be Greater than Zero")]
        public double StartMileage { get; set; }
        [Required(ErrorMessage = "* Required")]
        [GreaterThan("StartMileage",ErrorMessage = "End Mileage Should be Greater than Start Mileage")]
        [Range(1, Double.MaxValue, ErrorMessage = "Should be Greater than Zero")]
        public double EndMileage { get; set; }
        public string Comment { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required(ErrorMessage = "* Required")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Division { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Campus { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Course { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string VehicleRego { get; set; }
        public double Distance { get; set; }
        public string Status { get; set; }
        public string TripId { get; set; }

        public string MakeCmb { set; get; }

        public string MakeMileage { get; set; }


        public List<LocationData> LocationList { set; get; }
       



    }
}

