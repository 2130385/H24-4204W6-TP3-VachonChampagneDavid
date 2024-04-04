using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace TP3.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public virtual List<Score> Scores { get; set; }

    }
}
