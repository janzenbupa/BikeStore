using System;

namespace BikeStore.Models.Orders
{
    /// <summary>
    /// Base request to be sent into the Orders controller.
    /// </summary>
    public class OrderBaseRequest
    {
        /// <summary>
        /// Customer first name.
        /// </summary>
        public String FirstName { get; set; }
        /// <summary>
        /// Customer last name.
        /// </summary>
        public String LastName { get; set; }
        /// <summary>
        /// The model of each bike will be passed into this parameter separated by a '-'.
        /// </summary>
        public String Model { get; set; }
        /// <summary>
        /// This will be based on the amount of models in the Model field.
        /// </summary>
        public int Quantity { get; set; }
    }
}
