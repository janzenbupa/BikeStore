using System;

namespace BikeStore.DataAccessLayer.Models
{
    public class Order
    {
        public Int64 Id { get; set; }
        public String BikeId { get; set; }
        public int QuantityOfBikes { get; set; }
        public Int64 CustomerId { get; set; }
        public Decimal Price { get; set; }
    }
}
