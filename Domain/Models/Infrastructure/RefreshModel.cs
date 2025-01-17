namespace Domain.Models.Infrastructure;

// класс, содержащий данные пользователя
// для обновления jwt-токена безопасности
public record RefreshModel(

    // идентификатор пользователя
    int UserId,


    // пользовательский токен обновления
    string UserToken

    );
// record RefreshModel