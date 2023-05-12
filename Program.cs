using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = MicrosoftAccountDefaults.AuthenticationScheme;
}).AddCookie().AddMicrosoftAccount(o =>
{
	o.ClientId = "57f0978d-23bc-4172-ae60-d548461c018d";
	o.ClientSecret = "PMg8Q~~v5L5tXpeasGljOnNIcCZbdmIRZ5_sUazM";
	o.AuthorizationEndpoint = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/authorize";
	o.TokenEndpoint = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/token";
});

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
