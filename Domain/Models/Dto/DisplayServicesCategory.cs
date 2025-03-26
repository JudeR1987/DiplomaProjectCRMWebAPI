namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения категории услуг с коллекцией услуг,
// относящихся к данной категории
public record DisplayServicesCategory(
    
    // данные о категории услуг
    ServicesCategoryDto ServicesCategory,

    // коллекция услуг, относящихся к данной категории
    List<ServiceDto> Services
    
); // record DisplayServicesCategory