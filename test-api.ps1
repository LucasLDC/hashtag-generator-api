# Script de teste para a Hashtag Generator API

Write-Host "=== Hashtag Generator API - Script de Testes ===" -ForegroundColor Cyan
Write-Host ""

$baseUrl = "http://localhost:5000"

# Funcao para fazer requisicoes
function Test-Endpoint {
    param(
        [string]$Name,
        [string]$Endpoint,
        [hashtable]$Body
    )
    
    Write-Host "Teste: $Name" -ForegroundColor Yellow
    Write-Host "Endpoint: POST $Endpoint" -ForegroundColor Gray
    Write-Host "Body:" -ForegroundColor Gray
    Write-Host ($Body | ConvertTo-Json) -ForegroundColor DarkGray
    Write-Host ""
    
    try {
        $response = Invoke-RestMethod -Uri "$baseUrl$Endpoint" `
            -Method Post `
            -ContentType "application/json" `
            -Body ($Body | ConvertTo-Json)
        
        Write-Host "SUCESSO!" -ForegroundColor Green
        Write-Host "Resposta:" -ForegroundColor Gray
        Write-Host ($response | ConvertTo-Json -Depth 10) -ForegroundColor White
    }
    catch {
        Write-Host "ERRO!" -ForegroundColor Red
        Write-Host $_.Exception.Message -ForegroundColor Red
        if ($_.ErrorDetails.Message) {
            Write-Host $_.ErrorDetails.Message -ForegroundColor Red
        }
    }
    
    Write-Host ""
    Write-Host "-------------------------------------------" -ForegroundColor DarkGray
    Write-Host ""
}

# Verifica se a API esta rodando
Write-Host "Verificando se a API esta rodando..." -ForegroundColor Cyan
try {
    $health = Invoke-RestMethod -Uri "$baseUrl/" -Method Get
    Write-Host "SUCESSO - API esta rodando!" -ForegroundColor Green
    Write-Host ($health | ConvertTo-Json) -ForegroundColor White
    Write-Host ""
    Write-Host "-------------------------------------------" -ForegroundColor DarkGray
    Write-Host ""
}
catch {
    Write-Host "ERRO - API nao esta rodando. Execute 'dotnet run' primeiro." -ForegroundColor Red
    exit 1
}

# Teste 1: Gerar 8 hashtags com modelo padrão
Test-Endpoint -Name "Gerar 8 hashtags" -Endpoint "/hashtags" -Body @{
    text = "Aprendendo a criar APIs em .NET com inteligência artificial usando Ollama"
    count = 8
    model = "llama3.2:3b"
}

# Teste 2: Usar count padrão (10)
Test-Endpoint -Name "Usar count padrão (10)" -Endpoint "/hashtags" -Body @{
    text = "Desenvolvimento de software moderno com práticas de DevOps e CI/CD"
    model = "llama3.2:3b"
}

# Teste 3: Gerar 5 hashtags
Test-Endpoint -Name "Gerar 5 hashtags" -Endpoint "/hashtags" -Body @{
    text = "Explorando o mundo da programação funcional e paradigmas modernos"
    count = 5
}

# Teste 4: Testar limite máximo (30)
Test-Endpoint -Name "Testar limite máximo (30)" -Endpoint "/hashtags" -Body @{
    text = "Tecnologia, inovação, inteligência artificial, machine learning e o futuro da computação"
    count = 30
    model = "llama3.2:3b"
}

# Teste 5: Erro - texto vazio
Test-Endpoint -Name "Erro - texto vazio" -Endpoint "/hashtags" -Body @{
    text = ""
    count = 8
}

Write-Host "=== Testes Concluídos ===" -ForegroundColor Cyan
