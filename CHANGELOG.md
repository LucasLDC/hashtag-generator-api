# Changelog

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

O formato é baseado em [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/),
e este projeto adere ao [Semantic Versioning](https://semver.org/lang/pt-BR/).

## [1.0.0] - 2025-10-18

### ✨ Adicionado

-   Endpoint POST `/hashtags` para geração de hashtags
-   Integração com Ollama usando o modelo llama3.2:3b
-   Structured outputs com JSON Schema
-   Validações completas (hashtags com #, sem espaços, sem duplicatas)
-   Suporte a português brasileiro
-   Scripts de teste automatizado (PowerShell e Bash)
-   Documentação Swagger interativa
-   README completo com exemplos
-   Guia de contribuição (CONTRIBUTING.md)
-   GitHub Actions para CI/CD
-   EditorConfig para padronização de código

### 🔧 Configuração

-   Minimal API com .NET 8
-   Count padrão: 10 hashtags
-   Count máximo: 30 hashtags
-   Count mínimo: 1 hashtag
-   Modelo padrão: llama3.2:3b

### 📚 Documentação

-   README detalhado com exemplos de uso
-   Arquivo test.http para testes manuais
-   Scripts de teste automatizado
-   Estrutura do projeto documentada

### 🧪 Testes

-   5 cenários de teste implementados
-   Validação de entrada
-   Validação de saída
-   Tratamento de erros

---

## [Unreleased]

### 🔜 Planejado

-   [ ] Suporte a múltiplos idiomas
-   [ ] Cache de respostas
-   [ ] Rate limiting
-   [ ] Autenticação JWT
-   [ ] Métricas e telemetria
-   [ ] Suporte a outros modelos Ollama
-   [ ] API versioning
-   [ ] Testes unitários
-   [ ] Testes de integração
-   [ ] Docker support
-   [ ] Kubernetes manifests

---

[1.0.0]: https://github.com/SEU-USUARIO/hashtag-generator-api/releases/tag/v1.0.0
