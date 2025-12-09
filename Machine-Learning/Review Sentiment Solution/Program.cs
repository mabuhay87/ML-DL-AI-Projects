using ReviewSentiment.Api.Models;
using ReviewSentiment.Core.Services;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for React
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// ML model service registration
builder.Services.AddSingleton<SentimentModelService>(sp =>
{
    var service = new SentimentModelService();

    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var modelPath = Path.Combine(env.ContentRootPath, "model.zip");

    Console.WriteLine($"[API] Looking for model at: {modelPath}");

    if (File.Exists(modelPath))
    {
        service.LoadModel(modelPath);
        Console.WriteLine("[API] ML model loaded successfully.");
    }
    else
    {
        Console.WriteLine("[API] model.zip not found.");
    }

    return service;
});

var app = builder.Build();

// Dev tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✔ CORS MUST BE HERE
app.UseCors();

app.UseStaticFiles();

app.MapGet("/", () => "Review Sentiment API is running.");

app.MapGet("/ui", () => Results.Redirect("/index.html"));

app.MapPost("/predict", (PredictRequest request, SentimentModelService modelService) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
        return Results.BadRequest("Text is required.");

    var prediction = modelService.Predict(request.Text);

    return Results.Ok(new PredictResponse
    {
        Text = request.Text,
        IsPositive = prediction.PredictedLabel,
        Probability = prediction.Probability
    });
});

app.Run();
