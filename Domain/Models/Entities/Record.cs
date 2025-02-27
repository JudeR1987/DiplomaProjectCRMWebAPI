using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "ЗАПИСИ_НА_СЕАНС" (Records)
// связь "многие ко многим" между таблицами
// "СОТРУДНИКИ" (Employees) и "КЛИЕНТЫ" (Clients)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(RecordConfiguration))]
public class Record(int employeeId, int clientId, DateTime date,
    DateTime createDate, int length, string? comment, int attendance,
    bool isOnline, bool isPaid, DateTime? deleted)
{
    // первичный ключ - идентификатор записи на сеанс
    public int Id { get; set; }


    // данные о сотруднике
    // свойство внешнего ключа
    public int EmployeeId { get; set; } = employeeId;

    // связное свойство для таблицы "СОТРУДНИКИ", связь М:1
    // (во многих записях на сеанс может быть только один сотрудник)
    public virtual Employee Employee { get; set; } = null!;


    // данные о клиенте
    // свойство внешнего ключа
    public int ClientId { get; set; } = clientId;

    // связное свойство для таблицы "КЛИЕНТЫ", связь М:1
    // (во многих связях может быть только одна услуга)
    // (на многие записи на сеанс может быть записан только один клиент)
    public virtual Client Client { get; set; } = null!;


    // дата и время начала записи на сеанс
    public DateTime Date { get; set; } = date;


    // дата и время создания записи
    public DateTime CreateDate { get; set; } = createDate;


    // длительность сеанса(в секундах)
    public int Length { get; set; } = length;


    // комментарий к записи на сеанс
    public string? Comment { get; set; } = comment;


    // статус посещения клиентом записи на сеанс
    // -1 - клиент не пришел на визит,
    //  0 - ожидание клиента,
    //  1 - клиент пришел, услуги оказаны,
    //  2 - клиент подтвердил запись
    public int Attendance { get; set; } = attendance;


    // принадлежность записи на сеанс к онлайн-записи
    // (true - онлайн-запись, false - запись внес администратор)
    public bool IsOnline { get; set; } = isOnline;


    // статус оплаты
    // (true - запись на сеанс оплачена, false - запись на сеанс НЕ_оплачена)
    public bool IsPaid { get; set; } = isPaid;


    // дата и время удаления записи о сеансе
    public DateTime? Deleted { get; set; } = deleted;


    // навигационные свойства для связи "многие ко многим" Records <--> Services

    // связное свойство для таблицы "ЗАПИСИ_УСЛУГИ", связь 1:M
    // (одна запись на сеанс может быть во многих связях)
    public virtual List<RecordService> RecordsServices { get; set; } = [];

    // связное свойство для таблицы "УСЛУГИ", связь M:M
    // (во многих записях на сеансы может быть множество услуг)
    public virtual List<Service> Services { get; set; } = [];


    // вычисляемые свойства

    // итоговая стоимость всех предоставленных услуг в данной записи на сеанс
    public double TotalPrice => RecordsServices
        .Sum(recordService => recordService.TotalPrice);


    // clients_count integer<int32>

    // last_change_date string <date-time> Дата последнего редактирования записи

    // prepaid boolean Доступна ли онлайн-оплата для записи

    // prepaid_confirmed boolean Статус online-оплаты

    // activity_id number ID групповой записи


    // конструктор по умолчанию
    public Record() : this(0, 0, DateTime.Now.AddDays(1),
        DateTime.Now.AddDays(-1), 3600, null, 0, false, false, null) {
    } // Record


    // статический метод, возвращающий новый объект-копию
    public static Record NewRecord(Record srcRecord) =>
        new(srcRecord.EmployeeId,
            srcRecord.ClientId,
            srcRecord.Date,
            srcRecord.CreateDate,
            srcRecord.Length,
            srcRecord.Comment,
            srcRecord.Attendance,
            srcRecord.IsOnline,
            srcRecord.IsPaid,
            srcRecord.Deleted) {
            Id = srcRecord.Id,
            Employee = srcRecord.Employee,
            Client = srcRecord.Client,
            RecordsServices = srcRecord.RecordsServices,
            Services = srcRecord.Services
        };


    // статический метод, возвращающий объект-DTO
    public static RecordDto RecordToDto(Record srcRecord) =>
        new(srcRecord.Id,
            Employee.EmployeeToDto(srcRecord.Employee),
            Client.ClientToDto(srcRecord.Client),
            srcRecord.Date,
            srcRecord.CreateDate,
            srcRecord.Length,
            srcRecord.Comment,
            srcRecord.Attendance,
            srcRecord.IsOnline,
            srcRecord.IsPaid,
            RecordService.RecordsServicesToDto(srcRecord.RecordsServices),
            srcRecord.TotalPrice,
            srcRecord.Deleted
        );

} // class Record