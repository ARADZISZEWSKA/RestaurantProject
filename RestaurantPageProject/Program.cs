using Microsoft.EntityFrameworkCore;
using RestaurantPageProject.Data;
using RestaurantPageProject.Models;
using RestaurantPageProject.Repository;
using Microsoft.AspNetCore.Identity;
using RestaurantPageProject;
using Microsoft.AspNetCore.Identity.UI.Services;
using RestaurantPageProject.DbInitilizer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); //dla r�l
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10); //Okre�la maksymalny czas bezczynno�ci sesji - nieaktywna->>usuni�ta
    options.Cookie.HttpOnly = true; //cookie nie b�dzie dost�pny za po�rednictwem j�zyka JavaScript /Cross-Site S.
    options.Cookie.IsEssential = true;//cookie sesji jest istotny dla dzia�ania aplikacji i nie powinien by� automatycznie usuwany, nawet je�li u�ytkownik wy��czy obs�ug� plik�w cookie w przegl�darce.
    options.SlidingExpiration = true; //pozwala sesji pozosta� aktywn�, je�eli u�ytkownik aktywnie korzysta z aplikacji.
});

builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "754235212830936";
    options.AppSecret = "adc5a6aacff9408377f4895b2881acc0";
});

builder.Services.AddDistributedMemoryCache(); // Dodaje pami�� podr�czn� (cache)
builder.Services.AddSession(options => //Dodaje obs�ug� sesji do aplikacji. Sesja pozwala na przechowywanie danych o stanie mi�dzy ��daniami HTTP
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); //Okre�la maksymalny czas bezczynno�ci sesji - nieaktywna->>usuni�ta
    options.Cookie.HttpOnly = true; //cookie nie b�dzie dost�pny za po�rednictwem j�zyka JavaScript /Cross-Site S.
    options.Cookie.IsEssential = true; //cookie sesji jest istotny dla dzia�ania aplikacji i nie powinien by� automatycznie usuwany, nawet je�li u�ytkownik wy��czy obs�ug� plik�w cookie w przegl�darce.
});

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IRepository<MenuItems>, Repository<MenuItems>>();
builder.Services.AddScoped<IRepository<Reservation>, Repository<Reservation>>();




var app = builder.Build();
app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //if username or password is valid
app.UseAuthorization(); // if so, then comes this <-- dawanie r�nych uprawnie�
SeedDatabase();
app.MapRazorPages(); //aby login i reszta dzia�a�y bo s� Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}