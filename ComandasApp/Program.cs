using ComandasApp.Application;
using ComandasApp.Application.Interfaces.Services;
using ComandasApp.GraphQL.Mutations;
using ComandasApp.GraphQL.Queries;
using ComandasApp.Hubs;
using ComandasApp.Infrastructure;
using ComandasApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Inyección de Dependencias
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Configuración de SignalR para tiempo real
builder.Services.AddSignalR();
builder.Services.AddScoped<IOrderNotificationService, OrderNotificationService>();

// Configuración de GraphQL (HotChocolate)
builder.Services
    .AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .AddMutationType<OrderMutation>()
    .AddSorting();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials());
});

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configuración del entorno y archivos de Blazor WebAssembly
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// Mapeo de Endpoints (GraphQL y SignalR)
app.MapGraphQL();
app.MapHub<OrderHub>("/orderhub");

// Fallback para SPA Blazor WebAssembly
app.MapFallbackToFile("index.html");

app.Run();
