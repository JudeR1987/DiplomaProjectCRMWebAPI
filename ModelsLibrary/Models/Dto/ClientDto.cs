namespace ModelsLibrary.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "КЛИЕНТЫ" (Clients)
public record ClientDto(

    // идентификатор записи о клиенте
    int Id,

    // фамилия клиента
    string Surname,

    // имя клиента
    string Name,

    // отчество клиента
    string Patronymic,

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
    string Card,

    // birth_date string Дата рождения клиента в формате yyyy-mm-dd

    // дата рождения клиента
    DateTime BirthDate,

    // комментарий к записи о клиенте
    string Comment,

    // ? сумма потраченных средств в компании на момент добавления
    int Spent,

    // баланс клиента
    int Balance,

    // признак отправки поздравления на День Рождения клиента по SMS
    // (0 - не поздравлять, 1 - поздравлять)
    int SmsBirthday,

    // признак исключения клиента из SMS-рассылок
    // (0 - не исключать, 1 - исключить)
    int SmsNot

    // ? categories object Массив идентификаторов категорий клиента

    // ? custom_fields object Массив дополнительных полей клиента
    // в виде пар "api-key": "value"

    );
// record ClientDto