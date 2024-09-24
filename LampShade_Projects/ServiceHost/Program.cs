    using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using _0_Framework.Application.Email;
using _0_Framework.Application.Sms;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Infrastucture;
using AccountManagement.Infrastucture.Configuration;
using BlogManagement.Infrastucture.Configuration;
using CommentManagement.Infrastucture.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastucture.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using ServiceHost;
using ServiceHost.Controllers;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);
//این پروژه متعلق به ابوالفضل رستمی می باشد
#region DbContext

// Add service in Shop
ShopBootstrap.Configuration(builder.Services,
    "Data Source=.;Initial Catalog=Lampshade;Integrated Security=True;TrustServerCertificate=true");

// Add service in Discount
DiscountManagementBootstrapper.Configure(builder.Services,
    "Data Source=.;Initial Catalog=Lampshade;Integrated Security=True;TrustServerCertificate=true");

// Add service in Inventory
InventoryBootstrapper.Configure(builder.Services,
    "Data Source=.;Initial Catalog=Lampshade;Integrated Security=True;TrustServerCertificate=true");

// Add service in Blogging
BlogBootstrapper.Configure(builder.Services,
    "Data Source=.;Initial Catalog=Lampshade;Integrated Security=True;TrustServerCertificate=true");

// Add service in Comment
CommentBootstrapper.Configure(builder.Services,
    "Data Source=.;Initial Catalog=Lampshade;Integrated Security=True;TrustServerCertificate=true");

// Add service in Account
AccountBootstrapper.Configure(builder.Services,
    "Data Source=.;Initial Catalog=Lampshade;Integrated Security=True;TrustServerCertificate=true");

#endregion

builder.Services.AddHttpContextAccessor();

// Hash in Password
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

// add service in Uploader
builder.Services.AddTransient<IFileUploader, FileUploader>();

// Title site
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

// signin and signOut
builder.Services.AddTransient<IAuthHelper, AuthHelper>();

// ZarinPal
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

// SMS
builder.Services.AddTransient<ISmsService, SmsService>();

// SMS
builder.Services.AddTransient<IEmailService, EmailService>();


    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.Lax;
    });

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
        {
            o.LoginPath = new PathString("/Account");
            o.LogoutPath = new PathString("/Account");
            o.AccessDeniedPath = new PathString("/AccessDenied");
        });

// محدودیت افراد به پنل ادمین
    builder.Services.AddAuthorization(opetions =>
    {
        opetions.AddPolicy("AdminArea",
            builder => builder.RequireRole(new List<string> { Roles.Administration, Roles.ContentUploader,Roles.ColleagueUser }));

        opetions.AddPolicy("Shop",
            builder => builder.RequireRole(new List<string> { Roles.Administration }));
        
        opetions.AddPolicy("Discount",
            builder => builder.RequireRole(new List<string> { Roles.Administration }));
        
        opetions.AddPolicy("Account",
            builder => builder.RequireRole(new List<string> { Roles.Administration }));

    });

// add cors in api
    builder.Services.AddCors(options => options.AddPolicy("MyPolicy", builder => 
        builder
            .WithOrigins("https://localhost:7030")
            .AllowAnyHeader()
            .AllowAnyMethod()));

// Add services to the container.
    builder.Services.AddRazorPages()
        .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
        .AddRazorPagesOptions(opetions =>
        {
            opetions.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
            opetions.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
            opetions.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
        });
        //.AddApplicationPart(typeof(ProductController).Assembly)
        //.AddApplicationPart(typeof(InventoryController).Assembly);
        //.AddNewtonsoftJson();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.MapDefaultControllerRoute();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.UseCors("MyPolicy");

app.MapRazorPages();

app.Run();
