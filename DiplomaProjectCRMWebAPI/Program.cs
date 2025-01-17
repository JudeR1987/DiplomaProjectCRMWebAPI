
#region Дипломный проект по Web (ASP.NET и Angular) на 28.02.2025г. Макаров Е.С.

/*  ---------------------------------------------------------------------------

	...
    
    --------------------------------------------------------------------------- */

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.Repositories;
using Application.Interfaces;
using Application.Services;
using Domain.Models.Infrastructure;
using Domain.Context;

// построение приложения
var builder = WebApplication.CreateBuilder(args);

// добавление функционала контроллеров API
builder.Services.AddControllers();

// поставщик данных об объектах из базы данных
// builder.Services.AddScoped<DbService>();

// репозиторий-поставщик данных из базы данных
builder.Services.AddScoped<IDbRepository, DbRepository>();

// поставщик данных из базы данных с бизнес-логикой
builder.Services.AddScoped<IDbService, DbService>();

// поставщик jwt-токенов
builder.Services.AddSingleton<IJwtService, JwtService>();

// добавление функционала EF Core как сервиса приложения
// строку подключения определяем в appsettings.json  
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CrmContext>(
    options => options
        // подключение ленивой загрузки Lazy Loading
        .UseLazyLoadingProxies()
        .UseSqlServer(connection)
); // AddDbContext


// добавление сервисов аутентификации
// (схема аутентификации - с помощью jwt-токенов)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // подключение аутентификации с помощью jwt-токенов
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {

            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,

            // будет ли валидироваться потребитель токена
            ValidateAudience = true,

            // будет ли валидироваться время существования
            ValidateLifetime = true,

            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,


            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,

            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,

            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
        };
    });

// добавление сервисов авторизации
builder.Services.AddAuthorization();


// добавление сервиса CORS (Cross Origin Resource Sharing)
// для разрешения запросов к серверу от других доменов
// т.е. от клиентских приложений, созданных в других проектах
builder.Services.AddCors();


var app = builder.Build();

// перенаправление запросов по https протоколу
// app.UseHttpsRedirection();

// подключение статических файлов из папки wwwroot
app.UseStaticFiles();

// подключение маршрутизации для MVC-проекта
app.UseRouting();

// настройка CORS - разрешаем обрабатывать все источники запросов
// и все виды REST-запросов
app.UseCors(
    builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
); // UseCors


// добавление middleware аутентификации,
// работа с учетными записями пользователей
app.UseAuthentication();

// добавление middleware авторизации,
// авторизация пользователей
app.UseAuthorization();

// обязательное задание маршрута
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// запуск приложения
app.Run();