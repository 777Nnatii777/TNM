using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 11, 6)),
        b => b.MigrationsAssembly("TNM") // To tak ma byæ bo inaczej migracje sie w Domain robia i nei dzia³a a ja chce modle ³aczenai w Shaerd ale migracje konkretnie dla projketu w konkretnym projekcie
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
