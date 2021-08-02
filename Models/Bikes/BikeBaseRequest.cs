using System;

namespace BikeStore.Models
{
    /// <summary>
    /// Base request of the Bikes controller.
    /// </summary>
    public class BikeBaseRequest
    {
        public String Model { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? Available { get; set; }
    }
}
