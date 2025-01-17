using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Domain.Models.Infrastructure;

// класс, описывающий некоторые настройки генерации токена
public class AuthOptions
{
    // издатель токена
    public const string ISSUER = "http://localhost:5297";


    // потребитель токена
    public const string AUDIENCE = "http://localhost:5297";


    // ключ для шифрации
    private const string KEY = "mysupersecret_secretsecretsecretkey!123";


    // метод получения ключа безопасности(который применяется для генерации токена)
    // из массива байт(созданного по секретному ключу)
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

} // class AuthOptions