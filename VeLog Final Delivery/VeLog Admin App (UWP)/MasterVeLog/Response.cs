using System;
using System.ComponentModel;

namespace MasterVeLog
{
    public class VeLog // Vehicle Log Details
    {    
        public int Id { get; set; }  
        public DateTime CreationDate { get; set; }
        public string TripId { get; set; }
        public double StartMileage { get; set; }
        public double EndMileage { get; set; }
        public double Distance { get; set; }
        public string Division { get; set; }
        public string Campus { get; set; }
        public string Course { get; set; }
        public string DriverName { get; set; }
        public string VehicleRego { get; set; }
        public object Comment { get; set; }
        public object Status { get; set; }
    }

    public class VeLogDivisions // Division details
    {
        public int Id { get; set; }
        public string Division { get; set; }
    }    

    public class VeLogCampus // Campus details
    {
        public int Id { get; set; }
        public string Campus { get; set; }
    }

    public class VeLogCourses // Course details
    {
        public int Id { get; set; }
        public string Courses { get; set; }
        public string Division { get; set; }
    }

    // Users Service/API is NOT currently used by this App

    //public class VeLogUsers // User Details
    //{
    //    public int Id { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string Role { get; set; }
    //}

    public class VeLogCars // Car details
    {
        public int Id { get; set; }
    public string Registration { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Colour { get; set; }
    }
}
