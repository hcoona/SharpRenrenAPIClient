using Newtonsoft.Json;

namespace Renren.API.Entity
{
    public class FriendInfoEntity
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "tinyurl")]
        public string TinyHeadImageUrl { get; set; }
        [JsonProperty(PropertyName = "headurl")]
        public string MedianHeadImageUrl { get; set; }
    }
}