namespace kd_pw_transfers_backend.Services.Authentication
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; 
        public const string AUDIENCE = "MyAuthClient";
        public const string SCHEME = "JwtBearer";
        public const string SIGNING_SECURITY_KEY = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";
        public const string ENCODING_SECURITY_KEY = "k72gnxq3pkum9toiub48o8s8sdbjhme1tg0m3p4jfkzovsgdqzgv6t47ig3tr5d9";
        public const int LIFETIME = 43200; //30 days
        public const int WELCOME_AMOUNT = 50_000;
    }
}
