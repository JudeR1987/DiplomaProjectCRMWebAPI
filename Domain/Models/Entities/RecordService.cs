using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "ЗАПИСИ_УСЛУГИ" (RecordsServices)
// связь "многие ко многим" между таблицами
// "ЗАПИСИ_НА_СЕАНС" (Records) и "УСЛУГИ" (Services)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(RecordServiceConfiguration))]
public class RecordService(int recordId, int serviceId, int amount,
    int price, double discount, DateTime? deleted)
{
    // первичный ключ - идентификатор связи
    public int Id { get; set; }


    // данные о записи на сеанс
    // свойство внешнего ключа
    public int RecordId { get; set; } = recordId;

    // связное свойство для таблицы "ЗАПИСИ_НА_СЕАНС", связь М:1
    // (во многих связях может быть только одна запись на сеанс)
    public virtual Record Record { get; set; } = null!;


    // данные об услуге
    // свойство внешнего ключа
    public int ServiceId { get; set; } = serviceId;

    // связное свойство для таблицы "УСЛУГИ", связь М:1
    // (во многих связях может быть только одна услуга)
    public virtual Service Service { get; set; } = null!;


    // количество оказанных услуг
    public int Amount { get; set; } = amount;


    // цена на данную услугу
    public int Price { get; set; } = price;


    // скидка на данную услугу
    public double Discount { get; set; } = discount;


    // дата и время удаления записи о связи
    public DateTime? Deleted { get; set; } = deleted;


    // вычисляемые свойства

    // итоговая стоимость услуги
    public double TotalPrice => Amount * Price * (1 - Discount / 100);


    // конструктор по умолчанию
    public RecordService() : this(0, 0, 0, 0, 0, null) {
    } // RecordService()


    // статический метод, возвращающий новый объект-копию
    public static RecordService NewRecordService(RecordService srcRecordService) =>
        new(srcRecordService.RecordId,
            srcRecordService.ServiceId,
            srcRecordService.Amount,
            srcRecordService.Price,
            srcRecordService.Discount,
            srcRecordService.Deleted) {
            Id = srcRecordService.Id,
            Record = srcRecordService.Record,
            Service = srcRecordService.Service
        };


    // статический метод, возвращающий объект-DTO
    public static RecordServiceDto RecordServiceToDto(RecordService srcRecordService) =>
        new(srcRecordService.Id,
            srcRecordService.RecordId,
            Service.ServiceToDto(srcRecordService.Service),
            srcRecordService.Amount,
            srcRecordService.Price,
            srcRecordService.Discount,
            srcRecordService.TotalPrice,
            srcRecordService.Deleted
        );


    // статический метод, возвращающий список объектов-DTO
    public static List<RecordServiceDto> RecordsServicesToDto(
        List<RecordService> srcRecordsServices) =>
        srcRecordsServices.Select(RecordServiceToDto).ToList();

} // class RecordService