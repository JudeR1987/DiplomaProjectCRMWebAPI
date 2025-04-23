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


    // время существования токена (5 минут)
    public const int LIFETIME = 5;


    // секретный ключ для шифрации
    private const string KEY = "mysupersecret_secretsecretsecretkey!123";


    // метод получения ключа безопасности(который применяется для
    // генерации токена) из массива байт(созданного по секретному ключу)
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));


    // метод получения даты и времени окончания срока существования токена
    public static DateTime TokenExpires(int minutes = LIFETIME) =>
        DateTime.Now.AddMinutes(minutes);

} // class AuthOptions