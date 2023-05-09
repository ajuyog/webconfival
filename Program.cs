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
    //o.ClientId = "16c40bdc-7a32-494b-9d04-dd04034219b3";
    //o.ClientSecret = "zl88Q~gmj2Iuxv4wrwQi5KzNgUM~BW7fmanZ2cU9";
    //o.AuthorizationEndpoint = "https://login.microsoftonline.com/c58a8ce1-0a33-43c1-8b7c-1a1a95370660/oauth2/v2.0/authorize";
    //o.TokenEndpoint = "https://login.microsoftonline.com/c58a8ce1-0a33-43c1-8b7c-1a1a95370660/oauth2/v2.0/token";

	o.ClientId = "57f0978d-23bc-4172-ae60-d548461c018d";
	o.ClientSecret = "PMg8Q~~v5L5tXpeasGljOnNIcCZbdmIRZ5_sUazM";
	o.AuthorizationEndpoint = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/authorize";
	o.TokenEndpoint = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/token";
});
 


//builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
//{
//    microsoftOptions.AuthorizationEndpoint = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/authorize";
//    microsoftOptions.TokenEndpoint = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/token";
//    microsoftOptions.ClientId = "57f0978d-23bc-4172-ae60-d548461c018d";
//    microsoftOptions.ClientSecret = "PMg8Q~~v5L5tXpeasGljOnNIcCZbdmIRZ5_sUazM";
//});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7191/") });
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
