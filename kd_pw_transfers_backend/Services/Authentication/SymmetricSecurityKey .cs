using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace kd_pw_transfers_backend.Services.Authentication
{
    public class SigningSymmetricKey : IKdPwSigningEncodingKey, IKdPwSigningDecodingKey
    {
        private readonly SymmetricSecurityKey _secretKey;

        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

        public SigningSymmetricKey(string key)
        {
            this._secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public SecurityKey GetKey() => this._secretKey;
    }
}
