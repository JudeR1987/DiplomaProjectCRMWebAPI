namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы
// "СОТРУДНИКИ_УСЛУГИ" (EmployeesServices)
public record EmployeeServiceDto(

    // идентификатор записи о связи
    int Id,

    // идентификатор записи о сотруднике
    int EmployeeId,

    // идентификатор записи об услуге
    int ServiceId,

    // дата и время удаления записи о связи
    DateTime? Deleted

    );
// record EmployeeServiceDto