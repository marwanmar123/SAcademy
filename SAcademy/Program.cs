using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Models;
using SAcademy.Services;
using System.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("_Acceess",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication()
                //.AddGoogle(options =>
                //{
                //    IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authentication:Google");

                //    options.ClientId = googleAuthSection["ClientId"];
                //    options.ClientSecret = googleAuthSection["ClientSecret"];
                //})
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
                });
                //.AddMicrosoftAccount(microsoftOptions =>
                //{
                //    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                //    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
                //});
//builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("Acceess");
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
