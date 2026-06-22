using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// This tells the client where the API is
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5138")
});
builder.Services.AddScoped<Client.Services.ApiService>();
builder.Services.AddSingleton<Client.Services.AuthService>();
await builder.Build().RunAsync();