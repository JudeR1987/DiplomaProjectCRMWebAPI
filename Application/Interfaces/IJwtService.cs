using Domain.Models.Entities;

namespace Application.Interfaces;

// интерфейс сервиса-поставщика jwt-токенов
public interface IJwtService
{
    // 1. создать токен безопасности
    string CreateJwtToken(User user);


    // 2. создать токен обновления
    string CreateUserToken();

} // interface IJwtService