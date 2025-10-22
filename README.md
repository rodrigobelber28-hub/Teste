# Remover Caracteres Inválidos de Arquivos TXT - VB6

Este projeto contém código Visual Basic 6 para remover caracteres inválidos de arquivos de texto (.txt).

## 📋 Descrição

O código remove caracteres inválidos como:
- Caracteres de controle (ASCII 0-31, exceto Tab, CR, LF)
- ASCII estendido (128-255)
- Caracteres não-imprimíveis
- Caracteres customizados definidos pelo usuário

## 📁 Arquivos do Projeto

- **modRemoveInvalidChars.bas** - Módulo principal com funções de remoção
- **frmRemoveInvalidChars.frm** - Formulário de exemplo com interface gráfica

## 🚀 Como Usar

### Método 1: Usando o Módulo Diretamente

```vb
' Adicione o módulo modRemoveInvalidChars.bas ao seu projeto VB6

' Exemplo simples - Remove caracteres de controle
Dim resultado As Boolean
resultado = CleanTextFile("C:\meuarquivo.txt")

' Exemplo com opções
resultado = RemoveInvalidCharsFromFile( _
    "C:\origem.txt", _
    "C:\destino.txt", _
    RemoveControlChars, _
    "" _
)

' Remover múltiplos tipos de caracteres
resultado = RemoveInvalidCharsFromFile( _
    "C:\arquivo.txt", _
    "", _
    RemoveControlChars Or RemoveExtendedASCII, _
    "" _
)

' Remover caracteres customizados
resultado = RemoveInvalidCharsFromFile( _
    "C:\arquivo.txt", _
    "C:\limpo.txt", _
    RemoveControlChars, _
    "@#$%" _
)
```

### Método 2: Usando o Formulário

1. Adicione **modRemoveInvalidChars.bas** e **frmRemoveInvalidChars.frm** ao seu projeto
2. Execute o projeto
3. Selecione o arquivo de origem
4. Escolha as opções de remoção
5. Clique em "Processar Arquivo"

## 📊 Funções Disponíveis

### RemoveInvalidCharsFromFile
Remove caracteres inválidos de um arquivo.

**Parâmetros:**
- `sourceFile` (String) - Caminho do arquivo de origem
- `destFile` (String, Opcional) - Caminho do arquivo de destino (vazio = sobrescrever)
- `charType` (InvalidCharType, Opcional) - Tipo de caracteres a remover
- `customChars` (String, Opcional) - Caracteres customizados para remover

**Retorno:**
- `Boolean` - True se sucesso, False se erro

**Exemplo:**
```vb
Dim success As Boolean
success = RemoveInvalidCharsFromFile("C:\dados.txt", "C:\dados_limpo.txt", RemoveControlChars)
```

### RemoveInvalidChars
Remove caracteres inválidos de uma string.

**Parâmetros:**
- `text` (String) - Texto a processar
- `charType` (InvalidCharType, Opcional) - Tipo de caracteres a remover
- `customChars` (String, Opcional) - Caracteres customizados

**Retorno:**
- `String` - Texto limpo

**Exemplo:**
```vb
Dim textoLimpo As String
textoLimpo = RemoveInvalidChars("Texto com caracteres " & Chr(0) & " inválidos", RemoveControlChars)
```

### GetInvalidCharsCount
Conta quantos caracteres inválidos existem no arquivo.

**Parâmetros:**
- `filePath` (String) - Caminho do arquivo
- `charType` (InvalidCharType, Opcional) - Tipo de caracteres a contar

**Retorno:**
- `Long` - Quantidade de caracteres inválidos (-1 se erro)

**Exemplo:**
```vb
Dim quantidade As Long
quantidade = GetInvalidCharsCount("C:\arquivo.txt", RemoveControlChars)
MsgBox "Encontrados " & quantidade & " caracteres inválidos"
```

### CleanTextFile
Função simplificada para limpeza rápida (remove apenas caracteres de controle).

**Parâmetros:**
- `filePath` (String) - Caminho do arquivo

**Retorno:**
- `Boolean` - True se sucesso

**Exemplo:**
```vb
If CleanTextFile("C:\arquivo.txt") Then
    MsgBox "Arquivo limpo com sucesso!"
End If
```

## 🔧 Tipos de Caracteres (InvalidCharType)

```vb
Public Enum InvalidCharType
    RemoveControlChars = 1      'Remove caracteres de controle (0-31, exceto Tab, CR, LF)
    RemoveExtendedASCII = 2     'Remove ASCII estendido (128-255)
    RemoveNonPrintable = 4      'Remove todos não-imprimíveis
    RemoveSpecialChars = 8      'Remove caracteres especiais definidos
    RemoveAll = 15              'Remove todos os tipos acima
End Enum
```

**Combinando tipos:**
```vb
' Remove caracteres de controle E ASCII estendido
Dim tipo As InvalidCharType
tipo = RemoveControlChars Or RemoveExtendedASCII
```

## 💡 Exemplos de Uso

### Exemplo 1: Limpar arquivo CSV com caracteres inválidos
```vb
Private Sub LimparCSV()
    Dim arquivo As String
    arquivo = "C:\dados\vendas.csv"

    ' Conta caracteres inválidos
    Dim count As Long
    count = GetInvalidCharsCount(arquivo, RemoveControlChars)

    If count > 0 Then
        MsgBox "Encontrados " & count & " caracteres inválidos. Limpando..."

        ' Remove caracteres de controle
        If RemoveInvalidCharsFromFile(arquivo, "", RemoveControlChars) Then
            MsgBox "Arquivo limpo com sucesso!"
        End If
    Else
        MsgBox "Nenhum caractere inválido encontrado!"
    End If
End Sub
```

### Exemplo 2: Processar múltiplos arquivos
```vb
Private Sub ProcessarPasta()
    Dim arquivo As String
    Dim pasta As String
    Dim contador As Integer

    pasta = "C:\dados\"
    arquivo = Dir(pasta & "*.txt")
    contador = 0

    Do While arquivo <> ""
        If RemoveInvalidCharsFromFile(pasta & arquivo, "", RemoveControlChars) Then
            contador = contador + 1
        End If
        arquivo = Dir
    Loop

    MsgBox "Processados " & contador & " arquivos!"
End Sub
```

### Exemplo 3: Remover caracteres específicos
```vb
Private Sub RemoverCaracteresEspecificos()
    Dim arquivo As String
    Dim caracteresRemover As String

    arquivo = "C:\dados.txt"
    caracteresRemover = "±§¶"  ' Caracteres específicos a remover

    If RemoveInvalidCharsFromFile(arquivo, "", RemoveControlChars, caracteresRemover) Then
        MsgBox "Caracteres removidos com sucesso!"
    End If
End Sub
```

### Exemplo 4: Criar backup antes de processar
```vb
Private Sub ProcessarComBackup()
    Dim arquivoOriginal As String
    Dim arquivoBackup As String
    Dim arquivoLimpo As String

    arquivoOriginal = "C:\dados.txt"
    arquivoBackup = "C:\dados_backup.txt"
    arquivoLimpo = "C:\dados_limpo.txt"

    ' Cria backup
    FileCopy arquivoOriginal, arquivoBackup

    ' Processa arquivo
    If RemoveInvalidCharsFromFile(arquivoOriginal, arquivoLimpo, RemoveControlChars Or RemoveExtendedASCII) Then
        MsgBox "Processamento concluído!" & vbCrLf & _
               "Original: " & arquivoBackup & vbCrLf & _
               "Limpo: " & arquivoLimpo
    End If
End Sub
```

## ⚙️ Requisitos

- Visual Basic 6.0
- Windows 95 ou superior
- COMDLG32.OCX (para o formulário de exemplo)

## 📝 Notas Importantes

1. **Backup**: Sempre faça backup dos arquivos originais antes de processá-los
2. **Codificação**: O código trabalha com arquivos em modo binário para preservar a codificação
3. **Tamanho**: Para arquivos muito grandes (>10MB), considere processar em blocos
4. **Caracteres Preservados**: Por padrão, Tab (9), Line Feed (10) e Carriage Return (13) são preservados

## 🔍 Caracteres de Controle (ASCII 0-31)

Os seguintes caracteres são considerados de controle:
- **0-8**: NUL, SOH, STX, ETX, EOT, ENQ, ACK, BEL, BS
- **9**: TAB (preservado por padrão)
- **10**: LF - Line Feed (preservado por padrão)
- **11-12**: VT, FF
- **13**: CR - Carriage Return (preservado por padrão)
- **14-31**: SO, SI, DLE, DC1-4, NAK, SYN, ETB, CAN, EM, SUB, ESC, FS, GS, RS, US
- **127**: DEL

## 📄 Licença

Este código é fornecido "como está", sem garantias de qualquer tipo.

## 👤 Autor

Sistema - 2025

## 🤝 Contribuições

Sinta-se livre para modificar e adaptar o código às suas necessidades.
