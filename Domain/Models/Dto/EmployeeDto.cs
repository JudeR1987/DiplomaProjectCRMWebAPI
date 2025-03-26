namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "СОТРУДНИКИ" (Employees)
public record EmployeeDto(

    // идентификатор записи о сотруднике
    int Id,

    // имя сотрудника
    string Name,

    // идентификатор записи данных о пользователе
    int UserId,

    // данные о компании
    CompanyDto Company,

    // данные о специальности
    SpecializationDto Specialization,

    // данные о должности
    PositionDto Position,

    // рейтинг сотрудника
    int Rating,

    // путь к файлу аватарки сотрудника
    string Avatar,

    // услуги, предоставляемые сотрудником
    List<ServiceDto> Services,

    // дата и время удаления записи о сотруднике
    DateTime? Deleted

    );
// record EmployeeDto