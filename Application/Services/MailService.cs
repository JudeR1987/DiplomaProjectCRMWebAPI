using Application.Interfaces;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using MimeKit;

namespace Application.Services;

// сервис отправки электронных писем на почту
public class MailService : IMailService
{
    // адрес отправителя
    public readonly MailboxAddress addressFrom =
        new(MailOptions.NAME, MailOptions.ADDRESS);

    // объект отправляемого письма
    public MimeMessage Message { get; set; } = new();

    // объект клиента для передачи письма
    public MailKit.Net.Smtp.SmtpClient Client { get; set; } = new();


    // 1. отправить эл.письмо на почту
    public async Task<(bool, string)> SendMailAsync(User user, string subject) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // адрес получателя
            var addressTo = new MailboxAddress(user.UserName, user.Email);

            // создание письма
            Message = new();
            Message.From.Add(addressFrom);
            Message.To.Add(addressTo);
            Message.Subject = subject;

            // тело сообщения
            var body = new BodyBuilder();
            body.TextBody = user.Password;      // <-- ПАРОЛЬ!!!
            Message.Body = body.ToMessageBody();


            // создание объекта клиента для передачи письма
            using (Client = new()) {

                // настройка соединения
                var useSsl = true;
                await Client.ConnectAsync(MailOptions.HOST, MailOptions.PORT_465, useSsl);

                // аутентификация приложения на почтовом ресурсе
                await Client.AuthenticateAsync(MailOptions.ADDRESS, MailOptions.PASSWORD);


                // отправка письма
                await Client.SendAsync(Message);


                // прервать соединение
                await Client.DisconnectAsync(true);

            } // using

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // SendMailAsync

} // class Mail