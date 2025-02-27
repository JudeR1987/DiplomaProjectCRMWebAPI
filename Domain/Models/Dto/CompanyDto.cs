namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "КОМПАНИИ" (Companies)
public record CompanyDto(

    // идентификатор записи о компании
    int Id,

    // идентификатор записи о пользователе-владельце
    int OwnerUserId,

    // название компании
    string Name,

    // данные об адресе
    AddressDto Address,

    // телефон компании
    string Phone,

    // описание компании
    string Description,

    // дата и время удаления записи о компании
    DateTime? Deleted

    );
// record CompanyDto