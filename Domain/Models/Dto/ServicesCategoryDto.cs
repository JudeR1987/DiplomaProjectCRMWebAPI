﻿namespace Domain.Models.Dto;

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

    // ?
    // api_id  Integer Внешний идентификатор категории

    // ? вес категории(используется для сортировки категорий при отображении)
    //int Weight

    // ?
    // staff array   Список ID сотрудников, оказывающих услугу

    );
// record ServicesCategoryDto