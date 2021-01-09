using Microsoft.IdentityModel.Tokens;

namespace kd_pw_transfers_backend.Services.Authentication
{
    // Ключ для дешифрования данных (приватный)
    public interface IKdPwEncryptingDecodingKey
    {
        SecurityKey GetKey();
    }
}
