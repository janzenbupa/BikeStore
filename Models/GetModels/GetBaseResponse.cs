using System;

namespace BikeStore.Models.GetModels
{
    /// <summary>
    /// A base response that will be returned by every GET action.
    /// </summary>
    public class GetBaseResponse
    {
        /// <summary>
        /// Timestamp of the request.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Body will contain the json object that consists of the actual response.
        /// </summary>
        public String Body { get; set; }
    }
}
