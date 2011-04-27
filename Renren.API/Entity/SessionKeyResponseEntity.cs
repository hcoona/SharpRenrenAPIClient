using Newtonsoft.Json;

namespace Renren.API.Entity
{
    public class SessionKeyResponseEntity
    {
        [JsonProperty(PropertyName = "renren_token")]
        public TokenEntity RenrenToken { get; set; }
        [JsonProperty(PropertyName = "oauth_token")]
        public string OAuthToken { get; set; }
        [JsonProperty(PropertyName = "user")]
        public UserEntity User { get; set; }

        public class TokenEntity
        {
            [JsonProperty(PropertyName = "session_key")]
            public string SessionKey { get; set; }
            [JsonProperty(PropertyName = "session_secret")]
            public string SessionSecret { get; set; }
            [JsonProperty(PropertyName = "expires_in")]
            public int ExpiresIn { get; set; }
        }

        public class UserEntity
        {
            [JsonProperty(PropertyName = "id")]
            public int Id { get; set; }
        }
    }
}
