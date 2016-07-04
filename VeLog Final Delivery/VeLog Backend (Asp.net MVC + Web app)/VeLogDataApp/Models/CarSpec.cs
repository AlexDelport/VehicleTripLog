using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeLogDataApp.Models
{
    public class CarSpec
    {
        public string make { set; get; }
        public string model { set; get; }
        public string color { set; get; }
    }

    public class CarMileage
    {
        public string rego { set; get; }
        public double? startMileage { set; get; }
        public double? endMileage { set; get; }
    }
}