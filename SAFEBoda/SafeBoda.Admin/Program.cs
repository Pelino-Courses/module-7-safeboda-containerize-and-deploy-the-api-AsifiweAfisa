using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using SafeBoda.Admin;
using SafeBoda.Admin.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<ApiClient>(client => 
{
    client.BaseAddress = new Uri("http://localhost:5000"); 
});

builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();