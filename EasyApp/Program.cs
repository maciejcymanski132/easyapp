using Microsoft.EntityFrameworkCore;
using Airtrafic.Data;
using Airtrafic.Models;
using Airtrafic.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AirtraficContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AirtraficContext") ?? throw new InvalidOperationException("Connection string 'AirtraficContext' not found.")));

builder.Services.AddIdentity<AirtraficUser,AirtraficUserRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AirtraficContext>().AddDefaultUI().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<FlightCalculatorService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Configure Identity options here
    options.SignIn.RequireConfirmedAccount = false;
    // other configurations...
});
builder.Services.AddRazorPages();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
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
app.UseAuthentication();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
