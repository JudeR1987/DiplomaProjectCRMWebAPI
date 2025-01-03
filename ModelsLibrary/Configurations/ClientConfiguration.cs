using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;

namespace Domain.Configurations;

// конфигурация для сущности Client, задаётся атрибутом в классе сущности
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    void IEntityTypeConfiguration<Client>.Configure(EntityTypeBuilder<Client> builder)
    {

        #region Задание ограничений полей таблицы "КЛИЕНТЫ" при помощи Fluent API

        // настроить ограничение поля Surname для Client:
        // задать ограничение максимальной длины строкового поля фамилии клиента
        // nvarchar(50) not null
        builder
            .Property(client => client.Surname)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Name для Client:
        // задать ограничение максимальной длины строкового поля имени клиента
        // nvarchar(30) not null
        builder
            .Property(client => client.Name)
            .HasMaxLength(30)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Patronymic для Client:
        // задать ограничение максимальной длины строкового поля отчества клиента
        // nvarchar(50) not null
        builder
            .Property(client => client.Patronymic)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Phone для Client:
        // задать ограничение максимальной длины строкового поля телефона клиента
        // nvarchar(12) not null
        builder
            .Property(client => client.Phone)
            .HasMaxLength(12)
            .IsRequired()
            .IsUnicode();


        // настроить ограничение поля Email для Client:
        // задать ограничение максимальной длины строкового поля
        // адреса электронной почты клиента
        // nvarchar(50) not null
        builder
            .Property(client => client.Email)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить SQL-ограничение поля Gender для Client:
        // задать ограничение минимального и максимального значений
        // целочисленного значения пола клиента
        builder
            .ToTable(client => client
            .HasCheckConstraint("Gender", "Gender between 0 and 2"));

        // настроить SQL-ограничение поля ImportanceId для Client:
        // задать ограничение минимального и максимального значений
        // целочисленного значения класса важности клиента
        builder
            .ToTable(client => client
            .HasCheckConstraint("ImportanceId", "ImportanceId between 0 and 3"));

        // настроить SQL-ограничение поля Discount для Client:
        // задать ограничения минимального и максимального значений скидки клиента
        builder
            .ToTable(client => client
            .HasCheckConstraint("Discount", "Discount between 0 and 100"));

        // настроить ограничение поля Card для Client:
        // задать ограничение максимальной длины строкового поля
        // номера карты клиента
        // nvarchar(50) not null
        builder
            .Property(client => client.Card)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить SQL-ограничение поля BirthDate для Client:
        // задать ограничение максимального значения даты рождения клиента
        builder
            .ToTable(client => client
            .HasCheckConstraint("BirthDate", "BirthDate <= GetDate()"));

        // настроить ограничение поля Comment для Client:
        // задать ограничение максимальной длины строкового поля
        // комментария к записи о клиенте
        // nvarchar(300) not null
        builder
            .Property(client => client.Comment)
            .HasMaxLength(300)
            .IsRequired()
            .IsUnicode();

        // настроить SQL-ограничение поля Spent для Client:
        // задать ограничение минимального значения суммы потраченных
        // средств клиентом в компании на момент добавления
        builder
            .ToTable(client => client
            .HasCheckConstraint("Spent", "Spent >= 0"));

        // настроить SQL-ограничение поля Balance для Client:
        // задать ограничение минимального значения баланса клиента
        builder
            .ToTable(client => client
            .HasCheckConstraint("Balance", "Balance >= 0"));

        // настроить SQL-ограничение поля SmsBirthday для Client:
        // задать ограничения минимального и максимального значений признака
        // отправки поздравления на День Рождения клиента по SMS
        builder
            .ToTable(client => client
            .HasCheckConstraint("SmsBirthday", "SmsBirthday between 0 and 1"));

        // настроить SQL-ограничение поля SmsNot для Client:
        // задать ограничения минимального и максимального значений признака
        // исключения клиента из SMS-рассылок
        builder
            .ToTable(client => client
            .HasCheckConstraint("SmsNot", "SmsNot between 0 and 1"));

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "КЛИЕНТЫ"

        var clients = new List<Client> {
            new() { Id =  1, Surname = "Носов",      Name = "Андрей",    Patronymic = "Николаевич",    Phone = "+72332123456", Email = "diaspora@inbox.ru",     Gender = 1, ImportanceId = 0, Discount =  3, Card = "АБ 0095", BirthDate = new DateTime(1978, 01, 04), Comment = "Запись на начало месяца",        Spent = 0, Balance = 105, SmsBirthday = 1, SmsNot = 0 },
            new() { Id =  2, Surname = "Ольховская", Name = "Вераника",  Patronymic = "Андреевна",     Phone = "+72252546231", Email = "pilka@bk.ru",           Gender = 2, ImportanceId = 2, Discount =  3, Card = "АБ 0091", BirthDate = new DateTime(2000, 11, 19), Comment = "Пьёт капучино",                  Spent = 0, Balance = 213, SmsBirthday = 1, SmsNot = 0 },
            new() { Id =  3, Surname = "Кочерга",    Name = "Иван",      Patronymic = "Александрович", Phone = "+74251635241", Email = "holiday@hotmail.com",   Gender = 0, ImportanceId = 3, Discount = 15, Card = "АБ 0075", BirthDate = new DateTime(1994, 12, 28), Comment = "Опоздал на 15 мин.",             Spent = 0, Balance =  87, SmsBirthday = 0, SmsNot = 1 },
            new() { Id =  4, Surname = "Марченко",   Name = "Татьяна",   Patronymic = "Дмитриевна",    Phone = "+78990123987", Email = "astrology@live.com",    Gender = 0, ImportanceId = 0, Discount =  5, Card = "АБ 0044", BirthDate = new DateTime(1978, 03, 24), Comment = "Приходит с ребёнком",            Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id =  5, Surname = "Макаренко",  Name = "Вадим",     Patronymic = "Леонидович",    Phone = "+79090159951", Email = "kabala@furmail.ru",     Gender = 1, ImportanceId = 2, Discount = 10, Card = "АБ 0046", BirthDate = new DateTime(1997, 07, 28), Comment = "Стрижка по машинку всегда",      Spent = 0, Balance =  13, SmsBirthday = 1, SmsNot = 1 },
            new() { Id =  6, Surname = "Комарова",   Name = "Инна",      Patronymic = "Анатольевна",   Phone = "+73221123654", Email = "maximalist@yandex.ru",  Gender = 2, ImportanceId = 0, Discount =  7, Card = "АБ 0088", BirthDate = new DateTime(1992, 10, 12), Comment = "Постоянно переносит запись",     Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id =  7, Surname = "Вороненко",  Name = "Даниил",    Patronymic = "Олегович",      Phone = "+76452329485", Email = "forte@mail.ru",         Gender = 1, ImportanceId = 2, Discount =  3, Card = "АБ 0105", BirthDate = new DateTime(2010, 09, 16), Comment = "Длинные волосы",                 Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id =  8, Surname = "Ямал",       Name = "Инга",      Patronymic = "Валериевна",    Phone = "+71195453268", Email = "fight@gmail.com",       Gender = 0, ImportanceId = 0, Discount =  3, Card = "АБ 0101", BirthDate = new DateTime(1982, 02, 17), Comment = "Не нравится громкий телевизор",  Spent = 0, Balance =   0, SmsBirthday = 0, SmsNot = 0 },
            new() { Id =  9, Surname = "Кондрашова", Name = "Виктория",  Patronymic = "Евгеньевна",    Phone = "+74363255595", Email = "krupchatka@furmail.ru", Gender = 2, ImportanceId = 1, Discount =  7, Card = "АБ 0019", BirthDate = new DateTime(2008, 06, 24), Comment = "Очень аллергенная",              Spent = 0, Balance = 114, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 10, Surname = "Патько",     Name = "Елизавета", Patronymic = "Дмитриевна",    Phone = "+73314499774", Email = "militant@live.com",     Gender = 2, ImportanceId = 0, Discount =  5, Card = "АБ 0076", BirthDate = new DateTime(2009, 08, 18), Comment = "Возможно, осталась недовольна",  Spent = 0, Balance =   7, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 11, Surname = "Степко",     Name = "Андрей",    Patronymic = "Геннадиевич",   Phone = "+79655233111", Email = "seven@list.ru",         Gender = 1, ImportanceId = 1, Discount =  5, Card = "АБ 0041", BirthDate = new DateTime(1999, 04, 22), Comment = "Часто не приходит",              Spent = 0, Balance =  44, SmsBirthday = 1, SmsNot = 1 },
            new() { Id = 12, Surname = "Галушко",    Name = "Наталья",   Patronymic = "Викторовна",    Phone = "+71229366781", Email = "mirabella@gmail.com",   Gender = 2, ImportanceId = 1, Discount =  3, Card = "АБ 0006", BirthDate = new DateTime(2002, 12, 09), Comment = "Онихолизис на ногтях",           Spent = 0, Balance =  15, SmsBirthday = 0, SmsNot = 1 },
            new() { Id = 13, Surname = "Никифорова", Name = "Алла",      Patronymic = "Григорьевна",   Phone = "+72798556944", Email = "wizard@inbox.ru",       Gender = 2, ImportanceId = 0, Discount = 10, Card = "АБ 0010", BirthDate = new DateTime(1977, 05, 06), Comment = "Класть дополнительную конфетку", Spent = 0, Balance =   0, SmsBirthday = 0, SmsNot = 1 },
            new() { Id = 14, Surname = "Лепская",    Name = "Олеся",     Patronymic = "Олеговна",      Phone = "+71355986532", Email = "beluga@gmail.com",      Gender = 2, ImportanceId = 3, Discount = 10, Card = "АБ 0119", BirthDate = new DateTime(2002, 12, 09), Comment = "Был кератин и ботокс",           Spent = 0, Balance =  22, SmsBirthday = 0, SmsNot = 0 },
            new() { Id = 15, Surname = "Юрченко",    Name = "Эвелина",   Patronymic = "Андреевна",     Phone = "+74431292754", Email = "slag@bk.ru",            Gender = 2, ImportanceId = 0, Discount =  5, Card = "АБ 0084", BirthDate = new DateTime(1996, 03, 08), Comment = "Пьёт американо",                 Spent = 0, Balance =  16, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 16, Surname = "Некрасов",   Name = "Роман",     Patronymic = "Юрьевич",       Phone = "+76551264656", Email = "germans@gmail.com",     Gender = 1, ImportanceId = 2, Discount =  5, Card = "АБ 0077", BirthDate = new DateTime(1978, 08, 14), Comment = "Красавчик)",                     Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 17, Surname = "Оноприенко", Name = "Татьяна",   Patronymic = "Евгеньевна",    Phone = "+79596134521", Email = "maslina@yandex.ru",     Gender = 2, ImportanceId = 0, Discount =  3, Card = "АБ 0080", BirthDate = new DateTime(1981, 03, 18), Comment = "Укрепление гелем",               Spent = 0, Balance =   0, SmsBirthday = 0, SmsNot = 0 },
            new() { Id = 18, Surname = "Курчак",     Name = "Дмитрий",   Patronymic = "Валериевич",    Phone = "+72231564523", Email = "oner@hotmail.com",      Gender = 0, ImportanceId = 0, Discount =  3, Card = "АБ 0060", BirthDate = new DateTime(1988, 01, 30), Comment = "Очень придирчивый",              Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 19, Surname = "Данилов",    Name = "Олег",      Patronymic = "Романович",     Phone = "+72120321211", Email = "rabkor@list.ru",        Gender = 1, ImportanceId = 0, Discount =  7, Card = "АБ 0037", BirthDate = new DateTime(1996, 09, 12), Comment = "",                               Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 20, Surname = "Ломакин",    Name = "Анатолий",  Patronymic = "Юрьевич",       Phone = "+77581056944", Email = "undo@inbox.ru",         Gender = 1, ImportanceId = 3, Discount = 10, Card = "АБ 0055", BirthDate = new DateTime(2003, 11, 24), Comment = "Сложная кутикула",               Spent = 0, Balance = 220, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 21, Surname = "Борисова",   Name = "Наталья",   Patronymic = "Андреевна",     Phone = "+72525999111", Email = "guy@furmail.ru",        Gender = 2, ImportanceId = 2, Discount =  5, Card = "АБ 0049", BirthDate = new DateTime(1961, 01, 07), Comment = "Только классический маникюр",    Spent = 0, Balance =  17, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 22, Surname = "Боголюб",    Name = "Юлия",      Patronymic = "Вячеславовна",  Phone = "+71337373799", Email = "amphora@live.com",      Gender = 2, ImportanceId = 1, Discount =  3, Card = "АБ 0022", BirthDate = new DateTime(1975, 10, 19), Comment = "Классический педикюр",           Spent = 0, Balance =  14, SmsBirthday = 1, SmsNot = 1 },
            new() { Id = 23, Surname = "Никольский", Name = "Андрей",    Patronymic = "Юрьевич",       Phone = "+77788954856", Email = "genesis@mail.ru",       Gender = 1, ImportanceId = 1, Discount =  5, Card = "АБ 0087", BirthDate = new DateTime(1989, 01, 08), Comment = "Сделать скидку",                 Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 24, Surname = "Павлова",    Name = "Ада",       Patronymic = "Дмитриевна",    Phone = "+75152500400", Email = "woman@inbox.ru",        Gender = 2, ImportanceId = 1, Discount =  3, Card = "АБ 0073", BirthDate = new DateTime(2004, 12, 22), Comment = "Напоминать о записи за 1час",    Spent = 0, Balance =   0, SmsBirthday = 0, SmsNot = 1 },
            new() { Id = 25, Surname = "Сибиряк",    Name = "Анжелика",  Patronymic = "Валериевна",    Phone = "+75451004005", Email = "frigate@bk.ru",         Gender = 2, ImportanceId = 0, Discount =  3, Card = "АБ 0015", BirthDate = new DateTime(1999, 06, 15), Comment = "Забыла отдать повербанк",        Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 26, Surname = "Симонова",   Name = "Яна",       Patronymic = "Евгеньевна" ,   Phone = "+72023321852", Email = "grope@gmail.com",       Gender = 2, ImportanceId = 2, Discount = 10, Card = "АБ 0040", BirthDate = new DateTime(1998, 02, 23), Comment = "Не идёт на контакт",             Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 27, Surname = "Донской",    Name = "Дмитрий",   Patronymic = "Владимирович",  Phone = "+71237373789", Email = "appear@gmail.com",      Gender = 1, ImportanceId = 0, Discount =  5, Card = "АБ 0013", BirthDate = new DateTime(1988, 10, 24), Comment = "Не понравился шампунь",          Spent = 0, Balance = 132, SmsBirthday = 0, SmsNot = 0 },
            new() { Id = 28, Surname = "Дроздов",    Name = "Иван",      Patronymic = "Дмитриевич",    Phone = "+77186951856", Email = "vitaminize@yandex.ru",  Gender = 1, ImportanceId = 1, Discount =  7, Card = "АБ 0042", BirthDate = new DateTime(1973, 07, 13), Comment = "",                               Spent = 0, Balance =   0, SmsBirthday = 0, SmsNot = 0 },
            new() { Id = 29, Surname = "Носова",     Name = "Татьяна",   Patronymic = "Олеговна",      Phone = "+75002530410", Email = "ricans@mail.ru",        Gender = 2, ImportanceId = 3, Discount = 15, Card = "АБ 0017", BirthDate = new DateTime(1995, 08, 23), Comment = "Очень чувствительна",            Spent = 0, Balance =  11, SmsBirthday = 1, SmsNot = 0 },
            new() { Id = 30, Surname = "Емельянова", Name = "Надежда",   Patronymic = "Александровна", Phone = "+71459024075", Email = "capacity@yandex.ru",    Gender = 2, ImportanceId = 1, Discount =  7, Card = "АБ 0047", BirthDate = new DateTime(2000, 02, 08), Comment = "Громко разговаривает",           Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 1 },
            new() { Id = 31, Surname = "Андрюшкина", Name = "Валерия",   Patronymic = "Юрьевна" ,      Phone = "+72113391752", Email = "big@hotmail.com",       Gender = 2, ImportanceId = 0, Discount =  7, Card = "АБ 0021", BirthDate = new DateTime(1991, 09, 03), Comment = "Положительная и позитивная",     Spent = 0, Balance =   0, SmsBirthday = 1, SmsNot = 0 }
        };

        // инициализация таблицы "КЛИЕНТЫ"
        builder.HasData(clients);

        #endregion

    } // Configure

} // class ClientConfiguration