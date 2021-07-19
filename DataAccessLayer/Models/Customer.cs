using System;

namespace BikeStore.DataAccessLayer.Models
{
    public class Customer
    {
        public Int64 Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
