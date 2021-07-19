using System;

namespace BikeStore.Models.PostModels
{
    /// <summary>
    /// Base response of POST requests.
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Timestamp of request.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Body will contain the json object that consists of the response.
        /// </summary>
        public String Body { get; set; }
    }
}
