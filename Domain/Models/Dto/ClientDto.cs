namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "КЛИЕНТЫ" (Clients)
public record ClientDto(

    // идентификатор записи о клиенте
    int Id,

    // фамилия клиента
    string? Surname,

    // имя клиента
    string Name,

    // отчество клиента
    string? Patronymic,

    // номер телефона клиента
    string Phone,

    // адрес электронной почты клиента
    string Email,

    // пол клиента
    // (0 - не известен, 1 - мужской, 2 - женский)
    int Gender,

    // класс важности клиента
    // (0 - нет, 1 - бронза, 2 - серебро, 3 - золото)
    int ImportanceId,

    // скидка клиента
    double Discount,

    // номер карты клиента
    string? Card,

    // дата рождения клиента
    DateTime BirthDate,

    // комментарий к записи о клиенте
    string? Comment,

    // ? сумма потраченных средств в компании на момент добавления
    int Spent,

    // баланс клиента
    int Balance,

    // дата и время удаления записи о клиенте
    DateTime? Deleted

    );
// record ClientDto