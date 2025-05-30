
#region ��������� ������ �� Web (ASP.NET � Angular) �� 14.04.2025�. ������� �.�.

/*  ---------------------------------------------------------------------------

	����: CRM-������� ��� ������� �������
    
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

// ���������� ����������
var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� ������������ API
builder.Services.AddControllers();

// �����������-��������� ������ �� ���� ������
builder.Services.AddScoped<IDbRepository, DbRepository>();

// ��������� ������ �� ���� ������ � ������-�������
builder.Services.AddScoped<IDbService, DbService>();

// ��������� jwt-�������
builder.Services.AddSingleton<IJwtService, JwtService>();

// ������ �������� ����������� ����� �� �����
builder.Services.AddSingleton<IMailService, MailService>();

// ������ ��� ������ � ���������/��������� ������
builder.Services.AddSingleton<ILoadService, LoadService>();

// ���������� ����������� EF Core ��� ������� ����������
// ������ ����������� ���������� � appsettings.json  
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CrmContext>(
    options => options
        // ����������� ������� �������� Lazy Loading
        .UseLazyLoadingProxies()
        .UseSqlServer(connection)
); // AddDbContext


// ���������� �������� ��������������
// (����� �������������� - � ������� jwt-�������)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // ����������� �������������� � ������� jwt-�������
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {

            // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,

            // ����� �� �������������� ����������� ������
            ValidateAudience = true,

            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,

            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,


            // ������, �������������� ��������
            ValidIssuer = AuthOptions.ISSUER,

            // ��������� ����������� ������
            ValidAudience = AuthOptions.AUDIENCE,

            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
        };
    });

// ���������� �������� �����������
builder.Services.AddAuthorization();


// ���������� ������� CORS (Cross Origin Resource Sharing)
// ��� ���������� �������� � ������� �� ������ �������
// �.�. �� ���������� ����������, ��������� � ������ ��������
builder.Services.AddCors();


var app = builder.Build();

// ��������������� �������� �� https ���������
// app.UseHttpsRedirection();

// ����������� ����������� ������ �� ����� wwwroot
app.UseStaticFiles();

// ����������� ������������� ��� MVC-�������
app.UseRouting();

// ��������� CORS - ��������� ������������ ��� ��������� ��������
// � ��� ���� REST-��������
app.UseCors(
    builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
); // UseCors


// ���������� middleware ��������������,
// ������ � �������� �������� �������������
app.UseAuthentication();

// ���������� middleware �����������,
// ����������� �������������
app.UseAuthorization();

// ������������ ������� ��������
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Images",
    pattern: "{controller}/{action}/{directory1?}/{directory2?}/{directory3?}/{fileName?}");

// ������ ����������
app.Run();