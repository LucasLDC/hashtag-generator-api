# 🏷️ Hashtag Generator API

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)
![Ollama](https://img.shields.io/badge/Ollama-AI-000000?style=for-the-badge&logo=ai)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

[![Build](https://github.com/LucasLDC/hashtag-generator-api/actions/workflows/dotnet.yml/badge.svg)](https://github.com/LucasLDC/hashtag-generator-api/actions/workflows/dotnet.yml)
[![GitHub issues](https://img.shields.io/github/issues/LucasLDC/hashtag-generator-api)](https://github.com/LucasLDC/hashtag-generator-api/issues)
[![GitHub stars](https://img.shields.io/github/stars/LucasLDC/hashtag-generator-api)](https://github.com/LucasLDC/hashtag-generator-api/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/LucasLDC/hashtag-generator-api)](https://github.com/LucasLDC/hashtag-generator-api/network)

**API REST em .NET 8 que gera hashtags automaticamente usando Inteligência Artificial**

[Sobre](#-sobre) • [Recursos](#-recursos) • [Instalação](#-instalação) • [Uso](#-uso) • [Testes](#-testes) • [Documentação](#-documentação)

</div>

---

## 📋 Sobre

A **Hashtag Generator API** é uma aplicação desenvolvida em **.NET 8** usando **Minimal API** que integra com o **Ollama** para gerar hashtags relevantes automaticamente a partir de textos fornecidos. O projeto utiliza **structured outputs** e controle preciso de prompts para garantir que exatamente N hashtags sejam geradas conforme solicitado.

### ✨ Características Principais

-   🚀 **Minimal API** - Arquitetura moderna e performática do .NET 8
-   🤖 **Integração com Ollama** - Usa modelos de linguagem localmente (llama3.2:3b)
-   📐 **Structured Outputs** - JSON Schema para respostas estruturadas
-   ✅ **Validações Completas** - Hashtags com #, sem espaços, sem duplicatas
-   🌍 **Português** - Toda a aplicação em português brasileiro
-   🧪 **Testes Automatizados** - Scripts de teste em PowerShell e Bash
-   📚 **Documentação Swagger** - Interface interativa para testes

---

## 🎯 Recursos

### Endpoint Principal

**POST** `/hashtags` - Gera hashtags a partir de um texto

**Request:**

```json
{
    "text": "Desenvolvimento de APIs modernas com .NET",
    "count": 8,
    "model": "llama3.2:3b"
}
```

**Response:**

```json
{
    "model": "llama3.2:3b",
    "count": 8,
    "hashtags": [
        "#APIs",
        "#DotNet",
        "#Desenvolvimento",
        "#ModernDevelopment",
        "#WebAPI",
        "#Backend",
        "#CSharp",
        "#Tecnologia"
    ]
}
```

### Validações Implementadas

✅ Texto não pode ser vazio  
✅ Count entre 1 e 30 (padrão: 10)  
✅ Hashtags sempre começam com `#`  
✅ Hashtags sem espaços  
✅ Sem hashtags duplicadas  
✅ Modelo Ollama especificado (padrão: llama3.2:3b)

---

## 🛠️ Tecnologias

<div align="center">

| Tecnologia       | Versão   | Uso                    |
| ---------------- | -------- | ---------------------- |
| **.NET SDK**     | 8.0+     | Framework principal    |
| **ASP.NET Core** | 8.0+     | Minimal API            |
| **Ollama**       | Latest   | Motor de IA local      |
| **Swashbuckle**  | 9.0.6    | Documentação OpenAPI   |
| **HttpClient**   | Built-in | Comunicação com Ollama |

</div>

---

## 📦 Instalação

### Pré-requisitos

1. **.NET SDK 8.0+**

    ```bash
    winget install Microsoft.DotNet.SDK.8
    ```

2. **Ollama**
    - Baixe em: https://ollama.com/download
    - Instale o modelo:
        ```bash
        ollama pull llama3.2:3b
        ```

### Clone o Repositório

```bash
git clone https://github.com/LucasLDC/hashtag-generator-api.git
cd hashtag-generator-api
```

### Restaure as Dependências

```bash
dotnet restore
```

### Compile o Projeto

```bash
dotnet build
```

---

## 🚀 Uso

### 1. Inicie o Ollama

```bash
ollama serve
```

### 2. Execute a API

```bash
dotnet run --urls http://localhost:5000
```

### 3. Acesse a Documentação

Abra no navegador: **http://localhost:5000/swagger**

### 4. Teste o Endpoint

#### Usando cURL:

```bash
curl -X POST http://localhost:5000/hashtags \
  -H "Content-Type: application/json" \
  -d '{
    "text": "Inteligência artificial e machine learning",
    "count": 5
  }'
```

#### Usando PowerShell:

```powershell
$body = @{
    text = "Inteligência artificial e machine learning"
    count = 5
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/hashtags" `
  -Method POST `
  -ContentType "application/json" `
  -Body $body
```

---

## 🧪 Testes

### Script de Teste Automatizado (PowerShell)

Execute todos os testes de uma vez:

```powershell
# Inicia a API automaticamente
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd 'CAMINHO_DO_PROJETO'; dotnet run --urls http://localhost:5000"

# Aguarda inicialização
Start-Sleep -Seconds 5

# Executa os testes
.\test-api.ps1
```

### Cenários Testados

| Teste      | Cenário              | Resultado Esperado            |
| ---------- | -------------------- | ----------------------------- |
| ✅ Teste 1 | 8 hashtags           | Retorna exatamente 8 hashtags |
| ✅ Teste 2 | Count padrão         | Retorna 10 hashtags           |
| ✅ Teste 3 | 5 hashtags           | Retorna exatamente 5 hashtags |
| ✅ Teste 4 | 30 hashtags (máximo) | Retorna 30 hashtags           |
| ✅ Teste 5 | Texto vazio          | Erro 400 com mensagem         |

### Teste Manual

Use o arquivo `test.http` no VS Code com a extensão REST Client:

```http
### Teste 1: Gerar 8 hashtags
POST http://localhost:5000/hashtags
Content-Type: application/json

{
  "text": "Desenvolvimento de APIs REST com .NET 8",
  "count": 8
}
```

---

## 📊 Estrutura do Projeto

```
hashtag-generator-api/
│
├── 📄 Program.cs                      # Configuração da Minimal API
├── 📄 HashtagGeneratorApi.csproj     # Configurações do projeto
├── 📄 appsettings.json               # Configurações da aplicação
├── 📄 LICENSE                        # Licença MIT
├── 📄 README.md                      # Esta documentação
├── 📄 CONTRIBUTING.md                # Guia de contribuição
├── 📄 .editorconfig                  # Configurações do editor
├── 📄 .gitignore                     # Arquivos ignorados pelo Git
│
├── 📁 .github/                       # Configurações do GitHub
│   └── workflows/
│       └── dotnet.yml               # GitHub Actions CI/CD
│
├── 📁 Models/                        # Modelos de dados
│   ├── HashtagRequest.cs            # DTO de entrada
│   ├── HashtagResponse.cs           # DTO de saída
│   ├── ErrorResponse.cs             # DTO de erro
│   ├── OllamaRequest.cs             # Request para Ollama
│   └── OllamaResponse.cs            # Response do Ollama
│
├── 📁 Services/                      # Camada de serviços
│   └── OllamaService.cs             # Integração com Ollama
│
├── 📁 Properties/
│   └── launchSettings.json          # Configurações de execução
│
├── 🧪 test-api.ps1                   # Script de testes (PowerShell)
├── 🧪 test-api.sh                    # Script de testes (Bash)
└── 🧪 test.http                      # Testes REST Client
```

---

## 🎬 Demonstração

Um vídeo de demonstração está disponível mostrando:

-   ✅ Execução da API
-   ✅ Testes automatizados
-   ✅ Validações funcionando
-   ✅ Integração com Ollama

[▶️ Assistir Demonstração](#) <!-- Adicione o link do vídeo aqui -->

---

## 📚 Documentação Técnica

### Como Funciona

1. **Recepção da Requisição**: API recebe JSON com texto e quantidade
2. **Validação**: Verifica se texto não é vazio e count está entre 1-30
3. **Preparação do Prompt**: Cria prompt estruturado em português
4. **Chamada ao Ollama**: Envia requisição com JSON Schema
5. **Processamento**: Ollama gera hashtags usando LLM
6. **Validação da Resposta**: Verifica formato, duplicatas, espaços
7. **Retorno**: Devolve JSON com hashtags validadas

### JSON Schema Utilizado

```json
{
    "type": "object",
    "properties": {
        "hashtags": {
            "type": "array",
            "items": { "type": "string" }
        }
    },
    "required": ["hashtags"]
}
```

### Configuração do Ollama

A API usa as seguintes configurações:

-   `stream: false` - Resposta completa de uma vez
-   `format: "json"` - Saída estruturada em JSON
-   `model: "llama3.2:3b"` - Modelo de 2GB (padrão)

---

## ⚙️ Configuração

### Alterar Porta da API

```bash
dotnet run --urls http://localhost:8080
```

### Alterar URL do Ollama

Edite `appsettings.json`:

```json
{
    "Ollama": {
        "BaseUrl": "http://localhost:11434"
    }
}
```

### Usar Outro Modelo

Na requisição, especifique o modelo:

```json
{
    "text": "Seu texto aqui",
    "count": 10,
    "model": "llama3.1:7b"
}
```

---

## 🐛 Resolução de Problemas

### API não inicia

```bash
# Verifique se a porta 5000 está livre
netstat -ano | findstr :5000

# Use outra porta
dotnet run --urls http://localhost:8080
```

### Ollama não responde

```bash
# Verifique se está rodando
ollama list

# Reinicie o serviço
ollama serve
```

### Modelo não encontrado

```bash
# Liste modelos instalados
ollama list

# Instale o modelo necessário
ollama pull llama3.2:3b
```

### Erros de compilação

```bash
# Limpe e recompile
dotnet clean
dotnet build
```

---

## 📝 Requisitos Cumpridos

Esta API foi desenvolvida seguindo os seguintes requisitos:

-   [x] Minimal API em .NET 8+
-   [x] Endpoint POST /hashtags
-   [x] Integração com Ollama via HttpClient
-   [x] Uso de `stream: false`
-   [x] Structured outputs com JSON Schema
-   [x] Prompt controlado para gerar exatamente N hashtags
-   [x] Formato de request: `{text, count, model}`
-   [x] Formato de response: `{model, count, hashtags[]}`
-   [x] Validação de hashtags (# no início, sem espaços, sem duplicatas)
-   [x] Valores padrão (count=10, máximo=30)
-   [x] Respostas de erro (400) com mensagens úteis
-   [x] Arquivos de teste (.http, .ps1, .sh)
-   [x] Documentação completa
-   [x] Código em português brasileiro

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/NovaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/NovaFeature`)
5. Abra um Pull Request

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👤 Autor

**Lucas LDC**

-   GitHub: [@LucasLDC](https://github.com/LucasLDC)
-   LinkedIn: [Lucas Leal das Chagas](https://www.linkedin.com/in/lucas-leal-das-chagas-3424a2210/)

---

## 🙏 Agradecimentos

-   [Ollama](https://ollama.com/) - Pela ferramenta incrível de IA local
-   [Meta AI](https://ai.meta.com/) - Pelo modelo Llama 3.2
-   [.NET Team](https://dotnet.microsoft.com/) - Pelo framework excepcional

---

<div align="center">

**⭐ Se este projeto foi útil, considere dar uma estrela!**

Made with ❤️ using .NET 8 and Ollama

</div>
