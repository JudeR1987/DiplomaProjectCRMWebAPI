namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "КОМПАНИИ" (Companies)
public record CompanyDto(

    // идентификатор записи о компании
    int Id,

    // идентификатор записи о пользователе-владельце
    int UserOwnerId,

    // название компании
    string Name,

    // данные об адресе
    AddressDto Address,

    // телефон компании
    string Phone,

    // описание компании
    string? Description,

    // путь к файлу изображения логотипа компании
    string Logo,

    // путь к файлу основного изображения компании
    string TitleImage,

    // график работы компании
    string Schedule,

    // сайт компании
    string Site,

    // дата и время удаления записи о компании
    DateTime? Deleted

    );
// record CompanyDto