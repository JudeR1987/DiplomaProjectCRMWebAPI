namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы
// "ЗАПИСИ_УСЛУГИ" (RecordsServices)
public record RecordServiceDto(

    // идентификатор записи о связи
    int Id,

    // идентификатор записи на сеанс
    int RecordId,

    // данные об услуге
    ServiceDto Service,

    // количество оказанных услуг
    int Amount,

    // цена на данную услугу
    int Price,

    // скидка на данную услугу
    double Discount,

    // итоговая стоимость услуги
    double TotalPrice,

    // дата и время удаления записи о связи
    DateTime? Deleted

    );
// record RecordServiceDto