using System;
using System.Collections.Generic;

#nullable disable

namespace firstwebapi.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Reviews = new HashSet<Review>();
            TransactionVrms = new HashSet<TransactionVrm>();
        }

        public int Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TransactionVrm> TransactionVrms { get; set; }
    }
}
