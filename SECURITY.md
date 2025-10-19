# 🔒 Política de Segurança

## 🛡️ Versões Suportadas

Atualmente, as seguintes versões estão recebendo atualizações de segurança:

| Versão | Suportada          |
| ------ | ------------------ |
| 1.0.x  | :white_check_mark: |

## 🔍 Relatando uma Vulnerabilidade

A segurança do **Hashtag Generator API** é uma prioridade. Se você descobrir uma vulnerabilidade de segurança, agradecemos sua ajuda em divulgá-la de forma responsável.

### Como Relatar

**Por favor, NÃO relate vulnerabilidades de segurança através de issues públicas do GitHub.**

Em vez disso:

1. **Envie um e-mail** para: [seu-email@example.com]
2. **Inclua as seguintes informações:**

    - Tipo de vulnerabilidade
    - Localização completa do código-fonte afetado (tag/branch/commit ou URL direto)
    - Passos para reproduzir o problema
    - Código de prova de conceito ou exploit (se possível)
    - Impacto da vulnerabilidade, incluindo como um invasor pode explorar o problema

3. **Aguarde nossa resposta:**
    - Confirmaremos o recebimento em até 48 horas
    - Forneceremos uma estimativa de quando você pode esperar uma correção
    - Manteremos você informado sobre o progresso

### O que Esperar

-   **Resposta inicial:** Dentro de 48 horas
-   **Atualização de status:** A cada 7 dias
-   **Correção:** Depende da gravidade e complexidade

### Política de Divulgação

-   Aguarde que a vulnerabilidade seja corrigida antes de divulgá-la publicamente
-   Não explore a vulnerabilidade em sistemas de produção
-   Mantenha a confidencialidade até que a correção seja lançada

## 🏆 Reconhecimento

Agradecemos aos pesquisadores de segurança que relatam vulnerabilidades de forma responsável. Com sua permissão, teremos prazer em reconhecer sua contribuição publicamente após a correção.

### Hall da Fama de Segurança

_Em breve..._

## 🔐 Práticas de Segurança

### Para Desenvolvedores

-   ✅ Mantenha as dependências atualizadas
-   ✅ Use HTTPS para todas as comunicações
-   ✅ Valide todas as entradas
-   ✅ Não armazene informações sensíveis em texto claro
-   ✅ Siga as práticas recomendadas de segurança do .NET

### Para Usuários

-   ✅ Use sempre a versão mais recente
-   ✅ Configure o Ollama com acesso restrito
-   ✅ Não exponha a API diretamente à internet sem autenticação
-   ✅ Use HTTPS em produção
-   ✅ Monitore logs para atividades suspeitas

## 🔒 Configurações de Segurança Recomendadas

### Ambiente de Produção

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Warning"
        }
    },
    "Ollama": {
        "BaseUrl": "http://localhost:11434"
    }
}
```

### Rate Limiting

Recomendamos implementar rate limiting em produção:

```csharp
// Exemplo de configuração (não incluído na versão atual)
builder.Services.AddRateLimiter(options => {
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.User.Identity?.Name ?? context.Request.Headers.Host.ToString(),
            partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 10,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }
        )
    );
});
```

## 📚 Recursos de Segurança

-   [OWASP Top 10](https://owasp.org/www-project-top-ten/)
-   [Segurança do .NET](https://docs.microsoft.com/pt-br/dotnet/standard/security/)
-   [Melhores Práticas ASP.NET Core](https://docs.microsoft.com/pt-br/aspnet/core/security/)

## ❓ Perguntas?

Se você tiver dúvidas sobre esta política de segurança, entre em contato através das [Issues](https://github.com/LucasLDC/hashtag-generator-api/issues) ou por e-mail.

---

**Última atualização:** 18 de Outubro de 2025
