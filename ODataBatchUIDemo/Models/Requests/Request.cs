

using Newtonsoft.Json;

namespace ODataBatchUIDemo.Models.Requests
{
    public class Request
    {
        public string Id { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public Header Headers { get; set; }
        public object Body { get; set; }
    }

    public class Header
    {
        [JsonProperty("content-type")]
        public string ContentType { get; set; }
    }

    public struct Method
    {
        public static string POST = "POST";
        public static string GET = "GET";
        public static string PUT = "PUT";
        public static string DELETE = "DELETE";
    }
}
