namespace nZadarma.Core
{
    public class AuthorizeInfo
    {
        public string ApiKey { get; }
        public string SecretKey { get; }

        public AuthorizeInfo(string apiKey, string secretKey)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;
        }
    }
}