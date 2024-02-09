using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebPokerVideoGame.App2;
using WebPokerVideoGame.App2.Shared.Providers;
using WebPokerVideoGame.App2.Services;
using WebPokerVideoGame.App2.Interfaces;
using WebPokerVideoGame.App2.ViewModels;
using Microsoft.Extensions.FileProviders;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// part 3 tutoriala
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7116/") });

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
builder.Services.AddTransient<RankingService>();
builder.Services.AddTransient<GameHistoryService>();
builder.Services.AddTransient<PokerViewModel>();

builder.Services.AddMudServices();
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
