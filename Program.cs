using Microsoft.AspNetCore.Mvc;
using TrueStoryCodeTask.DTOs;
using TrueStoryCodeTask.HttpClients;
using TrueStoryCodeTask.Middleware;
using TrueStoryCodeTask.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<MockApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.restful-api.dev/");
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
        .Where(e => e.Value?.Errors.Count > 0)
        .ToDictionary(
            kvp => kvp.Key.ToLower(),
            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage)
        );

        var result = new BaseErrorResponse
        {
            Code = ErrorCodes.InvalidRequestParameters,
            Message = "Validation failed",
            TraceId = context.HttpContext.TraceIdentifier,
            Errors = errors
        };

        return new BadRequestObjectResult(result);
    };
});

builder.Services.AddSingleton<MockIdStoreService>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
