namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы
// "КАТЕГОРИИ_УСЛУГ" (ServicesCategories)
public record ServicesCategoryDto(

    // идентификатор записи о категории услуг
    int Id,

    // наименование категории услуг
    string Name,

    // дата и время удаления записи о категории услуг
    DateTime? Deleted

    );
// record ServicesCategoryDto