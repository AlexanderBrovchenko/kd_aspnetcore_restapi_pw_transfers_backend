using Microsoft.IdentityModel.Tokens;

namespace kd_pw_transfers_backend.Services.Authentication
{
    // Ключ для проверки подписи (публичный)
    public interface IKdPwSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
