using System;

namespace BikeStore.DataAccessLayer.Models
{
    public class Bike
    {
        public Int64 Id { get; set; }
        public String Model { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Available { get; set; }
    }
}
