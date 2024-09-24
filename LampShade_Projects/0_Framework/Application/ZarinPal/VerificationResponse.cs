using System.Text.Json.Serialization;

namespace _0_Framework.Application.ZarinPal
{
    public class VerificationResponse
    {
        [JsonPropertyName("Status")]
        public int Status { get; set; }

        [JsonPropertyName("RefID")]
        public long RefID { get; set; }
    }
}
