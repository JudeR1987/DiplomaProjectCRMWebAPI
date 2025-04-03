namespace Domain.Models.Infrastructure;

// класс, содержащий данные пользователя при регистрации/входе в систему
public class LoginModel(/*string login, */string userName, string phone, string email, string password)
{
    // логин пользователя (логин=телефону только при регистрации) // ИСПРАВИТЬ???
    /*public string Login { get; set; } = login;*/


    // имя пользователя
    public string UserName { get; set; } = userName;


    // номер телефона пользователя
    public string Phone { get; set; } = phone;


    // email пользователя
    public string Email { get; set; } = email;


    // пароль пользователя
    public string Password { get; set; } = password;


    // конструктор по умолчанию
    public LoginModel() : this(/*"", */"", "", "", "") { }

} // class LoginModel