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

builder.Services.AddHttpClient("Dot7Api", options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("ConnectionStrings:ApiConnection").Value);
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<GameService>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IRankingService, RankingService>();
builder.Services.AddTransient<PokerViewModel>();

builder.Services.AddMudServices();

builder.Services.AddBlazoredToast();

builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomHttpHandler>();

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
