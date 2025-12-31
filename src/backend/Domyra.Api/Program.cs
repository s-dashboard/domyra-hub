using Domyra.Api;
using Domyra.Application;
using Domyra.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
            {
                if (string.IsNullOrEmpty(origin))
                    return false;

                if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                    return false;

                return uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
                       || uri.Host.Equals("127.0.0.1");
            })
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();
app.AddInfrastructure(app.Environment.IsDevelopment());
app.UseCors("AllowLocalhost");
app.UseErrorHandler();

// --------------------
// Middleware
// --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

// --------------------
// Endpoints
// --------------------
app.MapDeviceEndpoints();
app.MapWidgetEndpoints();

app.Run();