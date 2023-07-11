using frontend;
using frontend.Services.Graph;
using frontend.Services.Token;
using frontend.Services.Utilidaddes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultScheme =
//	CookieAuthenticationDefaults.AuthenticationScheme;
//	options.DefaultChallengeScheme = MicrosoftAccountDefaults.AuthenticationScheme;
//}).AddCookie().AddMicrosoftAccount(o =>
//{
//	o.ClientId = builder.Configuration.GetValue<string>("Azure:ClientId");
//	o.ClientSecret = builder.Configuration.GetValue<string>("Azure:ClientSecret");
//	o.AuthorizationEndpoint = "https://login.microsoftonline.com/" + builder.Configuration.GetValue<string>("Azure:TenantId") + "/oauth2/v2.0/authorize";
//	o.TokenEndpoint = "https://login.microsoftonline.com/" + builder.Configuration.GetValue<string>("Azure:TenantId") + "/oauth2/v2.0/token";
//	o.SaveTokens = true;
//});

builder.Services.AddAuthentication().AddMicrosoftAccount(o =>
{
	o.ClientId = builder.Configuration.GetValue<string>("Azure:ClientId");
	o.ClientSecret = builder.Configuration.GetValue<string>("Azure:ClientSecret");
	o.AuthorizationEndpoint = "https://login.microsoftonline.com/" + builder.Configuration.GetValue<string>("Azure:TenantId") + "/oauth2/v2.0/authorize";
	o.TokenEndpoint = "https://login.microsoftonline.com/" + builder.Configuration.GetValue<string>("Azure:TenantId") + "/oauth2/v2.0/token";
	o.SaveTokens = true;
	o.Scope.Add("offline_access User.Read Mail.Read Calendars.Read");
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IGetToken, GetToken>();
builder.Services.AddTransient<IMail, Mail>();
builder.Services.AddTransient<IGraphServices, GraphServices>();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
	opciones.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7191/") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://webconfival.azurewebsites.net/") });

//builder.Services.AddRazorPages(options =>
//{
//    //options.Conventions.AuthorizePage("/Home");
//    options.Conventions.AuthorizeFolder("/Views/Home");
//    //options.Conventions.AllowAnonymousToPage("/Private/PublicPage");
//    options.Conventions.AllowAnonymousToFolder("/Views/Lnading");
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
////    The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LandingPage}/{action=Index}/{id?}");

app.Run();
