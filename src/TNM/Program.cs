using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Domain.Data; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");


Console.WriteLine($"Host: {dbHost}");
Console.WriteLine($"Port: {dbPort}");
Console.WriteLine($"User: {dbUser}");
Console.WriteLine($"Database: {dbName}");
Console.WriteLine($"Database: {dbPassword}");


var connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};Connection Timeout=10;";


builder.Services.AddDbContext<MyApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(10, 11, 6)),
        mySqlOptions => mySqlOptions
            .MigrationsAssembly("TNM") // Wskazanie ze musi do NTM migorwac bo inaczej migruje i nie dzia³a
            .EnableRetryOnFailure(
                maxRetryCount: 5, 
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null)
    ));


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "products",
    pattern: "Products/{action=List}/{id?}",
    defaults: new { controller = "Products" });


app.Run();
