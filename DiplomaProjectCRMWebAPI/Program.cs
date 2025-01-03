
#region ��������� ������ �� Web (ASP.NET � Angular) �� 28.02.2025�. ������� �.�.

/*  ---------------------------------------------------------------------------

	...
    
    --------------------------------------------------------------------------- */

#endregion

using Microsoft.EntityFrameworkCore;
using Domain.Context;
using Application.Repositories;
using Application.Interfaces;
using Application.Services;

// ���������� ����������
var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� ������������ API
builder.Services.AddControllers();

// ��������� ������ �� �������� �� ���� ������
// builder.Services.AddScoped<DbService>();

// �����������-��������� ������ �� ���� ������
builder.Services.AddScoped<IDbRepository, DbRepository>();

// ��������� ������ �� ���� ������ � ������-�������
builder.Services.AddScoped<IDbService, DbService>();

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
builder.Services.AddAuthentication("Barer")
    // ����������� �������������� � ������� jwt-�������
    .AddJwtBearer();

// ���������� �������� �����������
builder.Services.AddAuthorization();


// ���������� ������� CORS (Cross Origin Resource Sharing)
// ��� ���������� �������� � ������� �� ������ �������
// �.�. �� ���������� ����������, ��������� � ������ ��������
builder.Services.AddCors();


var app = builder.Build();

// ����������� ����������� ������ �� ����� wwwroot
app.UseStaticFiles();

// ����������� ������������� ��� MVC-�������
app.UseRouting();

// ��������� CORS - ��������� ������������ ��� ��������� ��������
// � ��� ���� REST-��������
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod());


// ���������� middleware ��������������,
// ������ � �������� �������� �������������
app.UseAuthentication();

// ���������� middleware �����������,
// ����������� �������������
app.UseAuthorization();


/*app.Map("/hello", [Authorize] () => "Hello World!");
app.Map("/", () => "Home Page");*/


// ������������ ������� ��������
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ������ ����������
app.Run();