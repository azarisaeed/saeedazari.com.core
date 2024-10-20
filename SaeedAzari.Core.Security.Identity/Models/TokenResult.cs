using System.Diagnostics.CodeAnalysis;

namespace SaeedAzari.Core.Security.Identity.Models
{
    public class TokenResult
    {
        public  required string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
