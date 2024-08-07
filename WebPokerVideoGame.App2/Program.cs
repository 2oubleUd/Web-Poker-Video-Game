using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebPokerVideoGame.App;
using WebPokerVideoGame.App.Shared.Providers;
using WebPokerVideoGame.App.Services;
using WebPokerVideoGame.App.Interfaces;
using WebPokerVideoGame.App.ViewModels;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// coonection to API by ApiConection field form appsettings.json
builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(builder.Configuration.GetSection("ConnectionStrings:ApiConnection").Value)
});

// Add services to have an ability to use Api function through Web Application
//builder.Services.AddHttpClient<IPlayerService, PlayerService>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7116/");
//});

builder.Services.AddHttpClient();

builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddTransient<GameService>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IRankingService, RankingService>();
//builder.Services.AddTransient<GameHistoryService>();
builder.Services.AddTransient<PokerViewModel>();

builder.Services.AddMudServices();

builder.Services.AddBlazoredToast();

builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
