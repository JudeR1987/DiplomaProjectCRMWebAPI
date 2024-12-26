
#region Дипломный проект по Web (ASP.NET и Angular) на 28.02.2025г. Макаров Е.С.

/*  ---------------------------------------------------------------------------

	...
    
    --------------------------------------------------------------------------- */

#endregion

using Microsoft.EntityFrameworkCore;
using DiplomaProjectCRMWebAPI.Context;

// построение приложения
var builder = WebApplication.CreateBuilder(args);

// поставщик данных об объектах из базы данных
// builder.Services.AddScoped<DbService>();

// добавление функционала контроллеров API
builder.Services.AddControllers();

// добавление функционала EF Core как сервиса приложения
// строку подключения определяем в appsettings.json  
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CrmContext>(
    options => options
        // подключение ленивой загрузки Lazy Loading
        .UseLazyLoadingProxies()
        .UseSqlServer(connection)
); // AddDbContext

// добавление сервиса CORS (Cross Origin Resource Sharing)
// для разрешения запросов к серверу от других доменов
// т.е. от клиентских приложений, созданных в других проектах
builder.Services.AddCors();

var app = builder.Build();

// подключение статических файлов из папки wwwroot
app.UseStaticFiles();

// подключение маршрутизации для MVC-проекта
app.UseRouting();

// настройка CORS - разрешаем обрабатывать все источники запросов
// и все виды REST-запросов
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod());

// авторизация, работа с учетными записями пользователей
//app.UseAuthorization();

// обязательное задание маршрута
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// запуск приложения
app.Run();