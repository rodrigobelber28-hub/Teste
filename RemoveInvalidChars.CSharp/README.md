# Remover Caracteres Inválidos de Arquivos TXT - C#

Solução completa em C# para remover caracteres inválidos de arquivos de texto (.txt).

## 📋 Descrição

Esta solução remove caracteres inválidos como:
- Caracteres de controle (ASCII 0-31, exceto Tab, CR, LF)
- ASCII estendido (128-255)
- Caracteres não-imprimíveis
- Caracteres customizados definidos pelo usuário

## 📁 Estrutura da Solução

```
RemoveInvalidChars.CSharp/
├── RemoveInvalidChars.sln                    # Solução Visual Studio
├── RemoveInvalidChars.Core/                  # Biblioteca principal (.NET Standard 2.0)
│   ├── InvalidCharRemover.cs                 # Classe principal
│   └── RemoveInvalidChars.Core.csproj
├── RemoveInvalidChars.WinForms/              # Aplicação Windows Forms (.NET 6.0)
│   ├── MainForm.cs                           # Formulário principal
│   ├── MainForm.Designer.cs                  # Designer do formulário
│   ├── Program.cs                            # Entrada da aplicação
│   └── RemoveInvalidChars.WinForms.csproj
└── RemoveInvalidChars.Examples/              # Exemplos de uso (.NET 6.0)
    ├── ExampleUsage.cs                       # 11 exemplos práticos
    └── RemoveInvalidChars.Examples.csproj
```

## 🚀 Como Abrir o Projeto

### No Visual Studio 2022 ou superior:
1. Abra o Visual Studio
2. Clique em **File** → **Open** → **Project/Solution**
3. Navegue até a pasta `RemoveInvalidChars.CSharp`
4. Selecione o arquivo **RemoveInvalidChars.sln**
5. Pressione **F5** para executar ou **Ctrl+Shift+B** para compilar

### Via .NET CLI:
```bash
cd RemoveInvalidChars.CSharp
dotnet restore
dotnet build
dotnet run --project RemoveInvalidChars.WinForms
```

## 📦 Requisitos

- **.NET 6.0 SDK** ou superior
- **Visual Studio 2022** (opcional, mas recomendado)
- **Windows** (para a aplicação Windows Forms)

## 🎯 Projetos da Solução

### 1. RemoveInvalidChars.Core
Biblioteca de classes (.NET Standard 2.0) com toda a lógica de remoção de caracteres.

**Principais Classes:**
- `InvalidCharRemover` - Classe principal com métodos estáticos
- `InvalidCharType` - Enum com tipos de caracteres a remover

### 2. RemoveInvalidChars.WinForms
Aplicação Windows Forms com interface gráfica amigável.

**Recursos:**
- Seleção de arquivos via dialog
- Múltiplas opções de remoção
- Log de processamento em tempo real
- Contador de caracteres inválidos

### 3. RemoveInvalidChars.Examples
Aplicação console com 11 exemplos práticos de uso.

## 💻 Como Usar

### Método 1: Interface Gráfica (Windows Forms)

1. Execute o projeto `RemoveInvalidChars.WinForms`
2. Selecione o arquivo de origem
3. Escolha o arquivo de destino (ou deixe vazio para sobrescrever)
4. Marque as opções de remoção desejadas
5. Clique em **Processar Arquivo**

### Método 2: Usar a Biblioteca no Seu Projeto

**Instale via referência de projeto:**
```xml
<ItemGroup>
  <ProjectReference Include="..\RemoveInvalidChars.Core\RemoveInvalidChars.Core.csproj" />
</ItemGroup>
```

**Código básico:**
```csharp
using RemoveInvalidChars.Core;

// Exemplo simples
bool result = InvalidCharRemover.CleanTextFile(@"C:\meuarquivo.txt");

// Exemplo avançado
bool success = InvalidCharRemover.RemoveInvalidCharsFromFile(
    sourceFile: @"C:\origem.txt",
    destFile: @"C:\destino.txt",
    charType: InvalidCharType.RemoveControlChars | InvalidCharType.RemoveExtendedASCII,
    customChars: "@#$%"
);
```

## 📊 API Reference

### Classe: InvalidCharRemover

#### RemoveInvalidCharsFromFile
```csharp
public static bool RemoveInvalidCharsFromFile(
    string sourceFile,
    string destFile = null,
    InvalidCharType charType = InvalidCharType.RemoveControlChars,
    string customChars = "",
    Encoding encoding = null)
```

Remove caracteres inválidos de um arquivo.

**Parâmetros:**
- `sourceFile` - Caminho do arquivo de origem
- `destFile` - Caminho do arquivo de destino (null = sobrescrever)
- `charType` - Tipo de caracteres a remover
- `customChars` - Caracteres customizados para remover
- `encoding` - Codificação do arquivo (null = UTF-8)

**Retorno:** `bool` - True se sucesso

**Exemplo:**
```csharp
bool success = InvalidCharRemover.RemoveInvalidCharsFromFile(
    @"C:\dados.txt",
    @"C:\dados_limpo.txt",
    InvalidCharType.RemoveControlChars);
```

#### RemoveInvalidChars
```csharp
public static string RemoveInvalidChars(
    string text,
    InvalidCharType charType = InvalidCharType.RemoveControlChars,
    string customChars = "")
```

Remove caracteres inválidos de uma string.

**Exemplo:**
```csharp
string clean = InvalidCharRemover.RemoveInvalidChars(
    "Texto com" + (char)0 + " caracteres inválidos",
    InvalidCharType.RemoveControlChars);
```

#### GetInvalidCharsCount
```csharp
public static int GetInvalidCharsCount(
    string filePath,
    InvalidCharType charType = InvalidCharType.RemoveControlChars,
    Encoding encoding = null)
```

Conta quantos caracteres inválidos existem no arquivo.

**Exemplo:**
```csharp
int count = InvalidCharRemover.GetInvalidCharsCount(
    @"C:\arquivo.txt",
    InvalidCharType.RemoveControlChars);
Console.WriteLine($"Encontrados {count} caracteres inválidos");
```

#### ProcessFolder
```csharp
public static int ProcessFolder(
    string folderPath,
    string searchPattern = "*.txt",
    InvalidCharType charType = InvalidCharType.RemoveControlChars,
    string customChars = "")
```

Processa múltiplos arquivos em uma pasta.

**Exemplo:**
```csharp
int processed = InvalidCharRemover.ProcessFolder(
    @"C:\dados",
    "*.txt",
    InvalidCharType.RemoveControlChars);
Console.WriteLine($"{processed} arquivos processados");
```

#### ContainsInvalidChars
```csharp
public static bool ContainsInvalidChars(
    string text,
    InvalidCharType charType = InvalidCharType.RemoveControlChars)
```

Verifica se uma string contém caracteres inválidos.

**Exemplo:**
```csharp
bool hasInvalid = InvalidCharRemover.ContainsInvalidChars(
    "Texto qualquer",
    InvalidCharType.RemoveControlChars);
```

#### CreateBackup
```csharp
public static string CreateBackup(
    string filePath,
    string backupSuffix = "_backup")
```

Cria backup de um arquivo antes de processar.

**Exemplo:**
```csharp
string backupPath = InvalidCharRemover.CreateBackup(@"C:\arquivo.txt");
Console.WriteLine($"Backup criado em: {backupPath}");
```

### Enum: InvalidCharType

```csharp
[Flags]
public enum InvalidCharType
{
    RemoveControlChars = 1,    // Remove caracteres de controle (0-31)
    RemoveExtendedASCII = 2,   // Remove ASCII estendido (128-255)
    RemoveNonPrintable = 4,    // Remove não-imprimíveis
    RemoveSpecialChars = 8,    // Remove caracteres especiais
    RemoveAll = 15             // Remove todos os tipos
}
```

**Combinando tipos:**
```csharp
var type = InvalidCharType.RemoveControlChars | InvalidCharType.RemoveExtendedASCII;
```

## 💡 Exemplos Práticos

### Exemplo 1: Limpar arquivo CSV
```csharp
string csvFile = @"C:\dados\vendas.csv";

// Conta caracteres inválidos
int count = InvalidCharRemover.GetInvalidCharsCount(
    csvFile,
    InvalidCharType.RemoveControlChars);

if (count > 0)
{
    Console.WriteLine($"Encontrados {count} caracteres inválidos. Limpando...");

    if (InvalidCharRemover.RemoveInvalidCharsFromFile(csvFile))
        Console.WriteLine("CSV limpo com sucesso!");
}
```

### Exemplo 2: Processar pasta inteira
```csharp
string folder = @"C:\dados";

int processed = InvalidCharRemover.ProcessFolder(
    folder,
    "*.txt",
    InvalidCharType.RemoveControlChars);

Console.WriteLine($"Processados {processed} arquivos!");
```

### Exemplo 3: Remover caracteres específicos
```csharp
string file = @"C:\dados.txt";
string charsToRemove = "±§¶";

InvalidCharRemover.RemoveInvalidCharsFromFile(
    file,
    null,
    InvalidCharType.RemoveControlChars,
    charsToRemove);
```

### Exemplo 4: Criar backup antes de processar
```csharp
string file = @"C:\importante.txt";

// Cria backup
string backup = InvalidCharRemover.CreateBackup(file);
Console.WriteLine($"Backup: {backup}");

// Processa arquivo
InvalidCharRemover.RemoveInvalidCharsFromFile(
    file,
    null,
    InvalidCharType.RemoveControlChars | InvalidCharType.RemoveExtendedASCII);
```

### Exemplo 5: Uso com LINQ
```csharp
var files = Directory.GetFiles(@"C:\dados", "*.txt");

var results = files.Select(file => new
{
    FileName = Path.GetFileName(file),
    InvalidCount = InvalidCharRemover.GetInvalidCharsCount(file),
    Processed = InvalidCharRemover.RemoveInvalidCharsFromFile(file)
});

foreach (var result in results)
{
    Console.WriteLine($"{result.FileName}: {result.InvalidCount} inválidos");
}
```

## 🔧 Compilação

### Visual Studio:
1. Abra a solução `RemoveInvalidChars.sln`
2. **Build** → **Build Solution** (Ctrl+Shift+B)
3. Os executáveis estarão em `bin\Debug\net6.0\` ou `bin\Release\net6.0\`

### .NET CLI:
```bash
# Debug
dotnet build

# Release
dotnet build -c Release

# Publicar aplicação standalone
dotnet publish -c Release -r win-x64 --self-contained
```

## 📝 Notas Importantes

1. **Codificação**: Por padrão usa UTF-8, mas pode ser especificada
2. **Thread-safe**: Os métodos são estáticos e thread-safe
3. **Performance**: Otimizado com StringBuilder para grandes arquivos
4. **Caracteres Preservados**: Tab (9), Line Feed (10) e Carriage Return (13) são preservados por padrão

## 🔍 Caracteres de Controle (ASCII 0-31)

- **0-8**: NUL, SOH, STX, ETX, EOT, ENQ, ACK, BEL, BS
- **9**: TAB (preservado)
- **10**: LF - Line Feed (preservado)
- **11-12**: VT, FF
- **13**: CR - Carriage Return (preservado)
- **14-31**: SO, SI, DLE, DC1-4, NAK, SYN, ETB, CAN, EM, SUB, ESC, FS, GS, RS, US
- **127**: DEL

## 🧪 Testes

Para testar a biblioteca, execute o projeto de exemplos:

```bash
dotnet run --project RemoveInvalidChars.Examples
```

## 📄 Licença

Este código é fornecido "como está", sem garantias de qualquer tipo.

## 👤 Autor

Sistema - 2025

## 🤝 Contribuições

Sinta-se livre para modificar e adaptar o código às suas necessidades.

## 🆚 Compatibilidade

- **.NET Standard 2.0** (biblioteca Core - compatível com .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+)
- **.NET 6.0** (aplicações WinForms e Examples)
- **Windows** (para Windows Forms)
- **Cross-platform** (biblioteca Core funciona em Windows, Linux, macOS)
