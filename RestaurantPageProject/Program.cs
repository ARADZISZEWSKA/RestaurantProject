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

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); //dla ról
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10); //Okreœla maksymalny czas bezczynnoœci sesji - nieaktywna->>usuniêta
    options.Cookie.HttpOnly = true; //cookie nie bêdzie dostêpny za poœrednictwem jêzyka JavaScript /Cross-Site S.
    options.Cookie.IsEssential = true;//cookie sesji jest istotny dla dzia³ania aplikacji i nie powinien byæ automatycznie usuwany, nawet jeœli u¿ytkownik wy³¹czy obs³ugê plików cookie w przegl¹darce.
    options.SlidingExpiration = true; //pozwala sesji pozostaæ aktywn¹, je¿eli u¿ytkownik aktywnie korzysta z aplikacji.
});

builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "754235212830936";
    options.AppSecret = "adc5a6aacff9408377f4895b2881acc0";
});

builder.Services.AddDistributedMemoryCache(); // Dodaje pamiêæ podrêczn¹ (cache)
builder.Services.AddSession(options => //Dodaje obs³ugê sesji do aplikacji. Sesja pozwala na przechowywanie danych o stanie miêdzy ¿¹daniami HTTP
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); //Okreœla maksymalny czas bezczynnoœci sesji - nieaktywna->>usuniêta
    options.Cookie.HttpOnly = true; //cookie nie bêdzie dostêpny za poœrednictwem jêzyka JavaScript /Cross-Site S.
    options.Cookie.IsEssential = true; //cookie sesji jest istotny dla dzia³ania aplikacji i nie powinien byæ automatycznie usuwany, nawet jeœli u¿ytkownik wy³¹czy obs³ugê plików cookie w przegl¹darce.
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
app.UseAuthorization(); // if so, then comes this <-- dawanie ró¿nych uprawnieñ
SeedDatabase();
app.MapRazorPages(); //aby login i reszta dzia³a³y bo s¹ Razor Pages
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