using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMeinProfilMvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// service db
string db = builder.Configuration.GetConnectionString("MyDB");
builder.Services.AddDbContext<AppIdentityDbContext>(o => o.UseSqlServer(db));

// service identity
builder.Services.AddIdentity<AppUser, IdentityRole>(o =>
{
    // settings passowrd config
    o.Password.RequiredLength = 5;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    //o.Lockout.MaxFailedAccessAttempts = 3;
})
    .AddEntityFrameworkStores<AppIdentityDbContext>()// simpan db
    .AddDefaultTokenProviders(); // token

// set session expired
builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromMinutes(1); // session 1 menit
    o.SlidingExpiration = true;
});

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
