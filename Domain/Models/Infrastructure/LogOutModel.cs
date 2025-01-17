namespace Domain.Models.Infrastructure;

// класс, содержащий данные пользователя
// для выхода из учётной записи
public record LogOutModel(

    // идентификатор пользователя
    int UserId,


    // пользовательский токен обновления
    string UserToken,


    // состояние входа пользователя в учётную запись
    // (true - вошёл, false - вышел)
    bool IsLogin

    );
// record LogOutModel