namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "РОЛИ" (Roles)
public record RoleDto(

    // идентификатор записи о роли пользователя
    int Id,

    // наименование роли пользователя
    string Name

    );
// record RoleDto