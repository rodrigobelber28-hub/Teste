# Notion Notes Reader - Leitor de Anotações do Notion

Aplicação console em C# para ler e visualizar anotações armazenadas no Notion através da API oficial.

## Recursos

- Lista todas as anotações de um database do Notion
- Visualiza o conteúdo completo de anotações específicas
- Lista todos os databases disponíveis
- Suporta diferentes tipos de blocos (parágrafos, títulos, listas, código, etc.)
- Interface interativa no console

## Pré-requisitos

- .NET 6.0 ou superior
- Uma conta no Notion
- Uma integração criada no Notion

## Configuração

### 1. Criar uma Integração no Notion

1. Acesse [https://www.notion.so/my-integrations](https://www.notion.so/my-integrations)
2. Clique em **"New integration"**
3. Dê um nome à integração (ex: "Notes Reader")
4. Selecione o workspace desejado
5. Configure as permissões:
   - **Read content**: Sim
   - **Update content**: Não (opcional)
   - **Insert content**: Não (opcional)
6. Clique em **"Submit"**
7. Copie o **"Internal Integration Token"** - essa é sua API Key

### 2. Compartilhar Database com a Integração

1. Abra o database que deseja acessar no Notion
2. Clique nos **três pontos** no canto superior direito
3. Role até o final e clique em **"Add connections"**
4. Selecione a integração que você criou
5. Confirme a permissão

### 3. Obter o Database ID

O Database ID pode ser encontrado na URL do database:

```
https://www.notion.so/workspace/DATABASE_ID?v=...
                              ^^^^^^^^^^^^
```

Copie essa sequência de caracteres (geralmente 32 caracteres sem hífens).

### 4. Configurar a Aplicação

Edite o arquivo `appsettings.json`:

```json
{
  "Notion": {
    "ApiKey": "secret_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
    "DatabaseId": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
  }
}
```

Substitua:
- `ApiKey`: pelo Internal Integration Token
- `DatabaseId`: pelo ID do database (sem hífens)

## Como Usar

### Compilar o Projeto

```bash
cd NotionNotesReader/NotionNotesReader.App
dotnet build
```

### Executar a Aplicação

```bash
dotnet run
```

### Funcionalidades

Após executar, você verá um menu interativo com as seguintes opções:

#### 1. Listar todas as anotações

Exibe todas as páginas do database configurado, mostrando:
- Título da anotação
- ID da página
- Data de criação
- Data da última edição
- URL da página no Notion

#### 2. Ver conteúdo de uma anotação específica

Digite o ID de uma página para visualizar seu conteúdo completo formatado.

#### 3. Listar databases disponíveis

Lista todos os databases que a integração tem acesso, útil para descobrir IDs de outros databases.

#### 4. Sair

Encerra a aplicação.

## Estrutura do Projeto

```
NotionNotesReader/
├── NotionNotesReader.App/
│   ├── Program.cs              # Ponto de entrada e interface do usuário
│   ├── NotionService.cs        # Lógica de integração com API do Notion
│   ├── NotionNotesReader.App.csproj
│   └── appsettings.json        # Configurações (API Key e Database ID)
└── README.md
```

## Dependências

- **Notion.Client** (v6.1.0): SDK oficial do Notion para .NET
- **Microsoft.Extensions.Configuration** (v6.0.0): Sistema de configuração
- **Microsoft.Extensions.Configuration.Json** (v6.0.0): Suporte a arquivos JSON

## Tipos de Blocos Suportados

A aplicação reconhece e formata os seguintes tipos de conteúdo do Notion:

- Parágrafos
- Títulos (H1, H2, H3)
- Listas com marcadores
- Listas numeradas
- Itens de to-do (com checkbox)
- Blocos de código
- Citações (quotes)

## Solução de Problemas

### Erro: "API Key do Notion é obrigatória"

Verifique se você configurou corretamente o `appsettings.json` com sua API Key.

### Erro: "Nenhuma anotação encontrada"

Possíveis causas:
- O database está vazio
- A integração não tem acesso ao database (compartilhe o database com a integração)
- O Database ID está incorreto

### Erro ao buscar conteúdo

- Verifique se o ID da página está correto
- Confirme que a integração tem permissão de leitura
- Certifique-se de que a página pertence ao workspace correto

## Segurança

- **NUNCA** compartilhe seu `appsettings.json` com suas credenciais
- Adicione `appsettings.json` ao `.gitignore` se usar controle de versão
- As API Keys do Notion podem ser revogadas a qualquer momento em [https://www.notion.so/my-integrations](https://www.notion.so/my-integrations)

## Exemplo de Uso

```
╔══════════════════════════════════════════════════════════╗
║                                                          ║
║        📔  NOTION NOTES READER - Leitor de Anotações    ║
║                                                          ║
╚══════════════════════════════════════════════════════════╝

═══════════════════════════════════════════════════════════
📋 MENU PRINCIPAL
═══════════════════════════════════════════════════════════
1. 📝 Listar todas as anotações
2. 🔍 Ver conteúdo de uma anotação específica
3. 📊 Listar databases disponíveis
4. 🚪 Sair
═══════════════════════════════════════════════════════════

➤ Escolha uma opção: 1

🔄 Buscando anotações...

✅ 3 anotação(ões) encontrada(s):

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📝 Minha Primeira Nota
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🆔 ID: abc123...
📅 Criado: 22/10/2025 14:30
✏️  Editado: 22/10/2025 15:45
🔗 URL: https://notion.so/...
```

## Licença

Este projeto é fornecido como está, para fins educacionais e de demonstração.

## Autor

Desenvolvido com C# e a API do Notion.
