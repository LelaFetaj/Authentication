namespace Authentication.Models.Configurations
{
    public class AuthConfiguration
    {
        public string SigningKey { get; set; } = string.Empty;
        public bool ValidateIssuer { get; set; } = true;
        public bool RequireSignedTokens { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = true;
        public bool RequireExpirationTime { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
        public bool ValidateAudience { get; set; } = false;
    }
}
