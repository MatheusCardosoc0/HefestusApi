using System.Text.Json.Serialization;

namespace HefestusApi.Models.Interfaces
{
    public class TimeTrail
    {
        [JsonIgnore]
        public string CreatedAt { get; private set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        [JsonIgnore]
        public string LastModifiedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
