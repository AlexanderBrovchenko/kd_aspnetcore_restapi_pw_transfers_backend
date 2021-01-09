using Microsoft.IdentityModel.Tokens;

namespace kd_pw_transfers_backend.Services.Authentication
{
    // Ключ для шифрования данных (публичный)
    public interface IKdPwEncryptingEncodingKey
    {
        string SigningAlgorithm { get; }

        string EncryptingAlgorithm { get; }

        SecurityKey GetKey();
    }
}
