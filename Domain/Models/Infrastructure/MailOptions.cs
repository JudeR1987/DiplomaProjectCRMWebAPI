namespace Domain.Models.Infrastructure;

// класс, описывающий некоторые настройки отправки электронных писем
public class MailOptions
{
    // заголовок письма с паролем после регистрации
    public const string SUBJECT_PASSWORD = "password";

    // заголовок письма с паролем после изменения
    public const string SUBJECT_NEW_PASSWORD = "new password";

    // имя отправителя
    public const string NAME = "CRM_WebAPI";

    // адрес отправителя
    public const string ADDRESS = "j_makarov@mail.ru";

    // хост почтового сервиса для отправки письма
    public const string HOST = "smtp.mail.ru";

    // порт приёма сигнала у почтового сервиса для отправки письма
    public const int PORT_465 = 465;
    public const int PORT_587 = 587;

    // пароль для аутентификации приложения
    public const string PASSWORD = "iBejK09x1Yd1DsVRT50B";

} // class MailOptions