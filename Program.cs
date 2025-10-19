using HashtagGeneratorApi.Models;
using HashtagGeneratorApi.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços
builder.Services.AddHttpClient<OllamaService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint principal: POST /hashtags
app.MapPost("/hashtags", async (
    [FromBody] HashtagRequest request,
    [FromServices] OllamaService ollamaService,
    ILogger<Program> logger) =>
{
    try
    {
        // Validação de entrada
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return Results.BadRequest(new ErrorResponse
            {
                Error = "O campo texto é obrigatório e não pode estar vazio"
            });
        }

        // Define valores padrão
        var count = request.Count ?? 10;
        var model = string.IsNullOrWhiteSpace(request.Model) ? "llama3.2:3b" : request.Model;

        // Valida e ajusta o count
        if (count < 1)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Error = "O count deve ser pelo menos 1"
            });
        }

        if (count > 30)
        {
            count = 30;
            logger.LogWarning("Count excedeu o máximo de 30, ajustado para 30");
        }

        logger.LogInformation("Gerando {Count} hashtags para o texto com modelo {Model}", count, model);

        // Gera hashtags via Ollama
        var hashtags = await ollamaService.GenerateHashtagsAsync(request.Text, count, model);

        // Verifica se obtivemos hashtags
        if (hashtags.Count == 0)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Error = "Falha ao gerar hashtags. Por favor, tente novamente."
            });
        }

        var response = new HashtagResponse
        {
            Model = model,
            Count = hashtags.Count,
            Hashtags = hashtags
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Erro ao processar requisição de hashtags");
        return Results.BadRequest(new ErrorResponse
        {
            Error = $"Ocorreu um erro: {ex.Message}"
        });
    }
})
.WithName("GenerateHashtags")
.Produces<HashtagResponse>(200)
.Produces<ErrorResponse>(400);

// Endpoint de health check
app.MapGet("/", () => new
{
    status = "rodando",
    message = "API Geradora de Hashtags está rodando. Use POST /hashtags para gerar hashtags."
})
.WithName("HealthCheck");

app.Run();
