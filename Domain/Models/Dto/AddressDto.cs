namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "АДРЕСА" (Addresses)
public record AddressDto(

    // идентификатор записи об адресе
    int Id,

    // данные о городе
    CityDto City,

    // данные об улице
    StreetDto Street,

    // дом/строение
    string Building,

    // квартира(возможно её отсутствие)
    int? Flat,

    // дата и время удаления записи об адресе
    DateTime? Deleted

    );
// record AddressDto