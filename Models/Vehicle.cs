using System;
using System.Collections.Generic;

#nullable disable

namespace firstwebapi.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Reviews = new HashSet<Review>();
            TransactionVrms = new HashSet<TransactionVrm>();
        }

        public int VehicleId { get; set; }
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public string VehicleType { get; set; }
        public int DailyRent { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TransactionVrm> TransactionVrms { get; set; }
    }
}
