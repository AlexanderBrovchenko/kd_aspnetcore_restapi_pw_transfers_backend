using Microsoft.IdentityModel.Tokens;

namespace kd_pw_transfers_backend.Services.Authentication
{
    // Ключ для создания подписи (приватный)
    public interface IKdPwSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
