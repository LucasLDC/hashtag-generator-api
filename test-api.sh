#!/bin/bash
# Script de teste para a Hashtag Generator API (Linux/Mac)

echo "=== Hashtag Generator API - Script de Testes ==="
echo ""

BASE_URL="http://localhost:5000"

# Cores
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Verifica se a API está rodando
echo "Verificando se a API está rodando..."
if curl -s "$BASE_URL/" > /dev/null; then
    echo -e "${GREEN}✓ API está rodando!${NC}"
    echo ""
else
    echo -e "${RED}✗ API não está rodando. Execute 'dotnet run' primeiro.${NC}"
    exit 1
fi

# Teste 1: Gerar 8 hashtags
echo -e "${YELLOW}Teste 1: Gerar 8 hashtags${NC}"
curl -X POST "$BASE_URL/hashtags" \
  -H "Content-Type: application/json" \
  -d '{
    "text": "Aprendendo a criar APIs em .NET com inteligência artificial usando Ollama",
    "count": 8,
    "model": "llama3.2:3b"
  }' | jq .
echo ""
echo "-------------------------------------------"
echo ""

# Teste 2: Count padrão
echo -e "${YELLOW}Teste 2: Usar count padrão (10)${NC}"
curl -X POST "$BASE_URL/hashtags" \
  -H "Content-Type: application/json" \
  -d '{
    "text": "Desenvolvimento de software moderno com práticas de DevOps e CI/CD",
    "model": "llama3.2:3b"
  }' | jq .
echo ""
echo "-------------------------------------------"
echo ""

# Teste 3: 5 hashtags
echo -e "${YELLOW}Teste 3: Gerar 5 hashtags${NC}"
curl -X POST "$BASE_URL/hashtags" \
  -H "Content-Type: application/json" \
  -d '{
    "text": "Explorando o mundo da programação funcional",
    "count": 5
  }' | jq .
echo ""
echo "-------------------------------------------"
echo ""

# Teste 4: Erro - texto vazio
echo -e "${YELLOW}Teste 4: Erro - texto vazio${NC}"
curl -X POST "$BASE_URL/hashtags" \
  -H "Content-Type: application/json" \
  -d '{
    "text": "",
    "count": 8
  }' | jq .
echo ""
echo "-------------------------------------------"
echo ""

echo -e "${GREEN}=== Testes Concluídos ===${NC}"
