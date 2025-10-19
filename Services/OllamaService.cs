using System.Text;
using System.Text.Json;
using HashtagGeneratorApi.Models;

namespace HashtagGeneratorApi.Services;

public class OllamaService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OllamaService> _logger;
    private const string OllamaBaseUrl = "http://localhost:11434";

    public OllamaService(HttpClient httpClient, ILogger<OllamaService> logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(OllamaBaseUrl);
        _logger = logger;
    }

    public async Task<List<string>> GenerateHashtagsAsync(string text, int count, string model)
    {
        // Define o JSON schema para structured outputs
        var jsonSchema = new
        {
            type = "object",
            properties = new
            {
                hashtags = new
                {
                    type = "array",
                    items = new { type = "string" },
                    description = "Array de hashtags"
                }
            },
            required = new[] { "hashtags" }
        };

        // Cria o prompt com instruções específicas
        var prompt = $@"Gere exatamente {count} hashtags relevantes para o seguinte texto. 
Regras:
- Retorne APENAS um objeto JSON com um array 'hashtags'
- Cada hashtag deve começar com #
- Sem espaços nas hashtags (use camelCase ou underscores se necessário)
- Sem hashtags duplicadas
- Faça hashtags relevantes e concisas
- Retorne exatamente {count} hashtags, nem mais, nem menos

Texto: {text}";

        var ollamaRequest = new OllamaRequest
        {
            Model = model,
            Prompt = prompt,
            Stream = false,
            Format = jsonSchema
        };

        var json = JsonSerializer.Serialize(ollamaRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _logger.LogInformation("Enviando requisição ao Ollama com modelo: {Model}, count: {Count}", model, count);

        try
        {
            var response = await _httpClient.PostAsync("/api/generate", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Resposta bruta do Ollama: {Response}", responseContent);

            var ollamaResponse = JsonSerializer.Deserialize<OllamaResponse>(responseContent);

            if (ollamaResponse?.Response == null)
            {
                throw new Exception("Ollama retornou uma resposta vazia");
            }

            // Parse a resposta JSON do Ollama
            var hashtagsData = JsonSerializer.Deserialize<JsonElement>(ollamaResponse.Response);

            var hashtags = new List<string>();

            if (hashtagsData.TryGetProperty("hashtags", out var hashtagsArray))
            {
                foreach (var item in hashtagsArray.EnumerateArray())
                {
                    var hashtag = item.GetString();
                    if (!string.IsNullOrWhiteSpace(hashtag))
                    {
                        // Garante que começa com # e não tem espaços
                        hashtag = hashtag.Trim();
                        if (!hashtag.StartsWith("#"))
                        {
                            hashtag = "#" + hashtag;
                        }
                        hashtag = hashtag.Replace(" ", "");

                        // Evita duplicatas
                        if (!hashtags.Contains(hashtag, StringComparer.OrdinalIgnoreCase))
                        {
                            hashtags.Add(hashtag);
                        }
                    }
                }
            }

            // Se não temos hashtags suficientes, tenta extrair do texto bruto
            if (hashtags.Count < count)
            {
                _logger.LogWarning("Hashtags insuficientes geradas. Esperado: {Expected}, Obtido: {Got}", count, hashtags.Count);
            }

            // Retorna exatamente o número solicitado
            return hashtags.Take(count).ToList();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erro ao comunicar com o Ollama");
            throw new Exception("Falha ao comunicar com o Ollama. Certifique-se de que o Ollama está rodando em " + OllamaBaseUrl, ex);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Erro ao processar resposta do Ollama");
            throw new Exception("Falha ao processar resposta do Ollama", ex);
        }
    }
}
