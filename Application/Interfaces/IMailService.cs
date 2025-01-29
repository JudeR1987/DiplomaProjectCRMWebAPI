using Domain.Models.Entities;

namespace Application.Interfaces;

// интерфейс сервиса отправки электронных писем на почту
public interface IMailService
{
    // 1. отправить эл.письмо на почту
    Task<(bool, string)> SendMailAsync(User user, string subject);

} // interface IMailService