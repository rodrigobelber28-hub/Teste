Attribute VB_Name = "ExemploUso"
'*******************************************************************************
' M�dulo: ExemploUso
' Descri��o: Exemplos de uso das fun��es de remo��o de caracteres inv�lidos
'*******************************************************************************

Option Explicit

'*******************************************************************************
' EXEMPLO 1: Uso B�sico
'*******************************************************************************
Public Sub Exemplo1_UsoBasico()
    Dim arquivo As String
    Dim resultado As Boolean

    ' Define arquivo
    arquivo = "C:\temp\teste.txt"

    ' Verifica se existe
    If Not FileExists(arquivo) Then
        MsgBox "Arquivo n�o encontrado: " & arquivo, vbExclamation
        Exit Sub
    End If

    ' Processa arquivo (sobrescreve original)
    resultado = CleanTextFile(arquivo)

    If resultado Then
        MsgBox "Arquivo processado com sucesso!", vbInformation
    Else
        MsgBox "Erro ao processar arquivo!", vbCritical
    End If
End Sub

'*******************************************************************************
' EXEMPLO 2: Salvar em arquivo diferente
'*******************************************************************************
Public Sub Exemplo2_ArquivoDiferente()
    Dim origem As String
    Dim destino As String
    Dim resultado As Boolean

    origem = "C:\dados\arquivo_original.txt"
    destino = "C:\dados\arquivo_limpo.txt"

    ' Remove caracteres de controle e salva em novo arquivo
    resultado = RemoveInvalidCharsFromFile(origem, destino, RemoveControlChars)

    If resultado Then
        MsgBox "Arquivo limpo salvo em: " & destino, vbInformation
    End If
End Sub

'*******************************************************************************
' EXEMPLO 3: Remover m�ltiplos tipos de caracteres
'*******************************************************************************
Public Sub Exemplo3_MultiplosTipos()
    Dim arquivo As String
    Dim tipo As InvalidCharType

    arquivo = "C:\dados\dados.txt"

    ' Combina m�ltiplos tipos
    tipo = RemoveControlChars Or RemoveExtendedASCII

    ' Processa
    If RemoveInvalidCharsFromFile(arquivo, "", tipo) Then
        MsgBox "Caracteres de controle e ASCII estendido removidos!", vbInformation
    End If
End Sub

'*******************************************************************************
' EXEMPLO 4: Contar caracteres inv�lidos antes de remover
'*******************************************************************************
Public Sub Exemplo4_ContarAntes()
    Dim arquivo As String
    Dim quantidade As Long

    arquivo = "C:\dados\log.txt"

    ' Conta caracteres inv�lidos
    quantidade = GetInvalidCharsCount(arquivo, RemoveControlChars)

    If quantidade > 0 Then
        MsgBox "Encontrados " & quantidade & " caracteres inv�lidos." & vbCrLf & _
               "Deseja remov�-los?", vbQuestion + vbYesNo

        ' Remove se usu�rio concordar
        If vbYes = MsgBox("Remover?", vbYesNo) Then
            RemoveInvalidCharsFromFile arquivo, "", RemoveControlChars
            MsgBox "Caracteres removidos!", vbInformation
        End If
    Else
        MsgBox "Nenhum caractere inv�lido encontrado!", vbInformation
    End If
End Sub

'*******************************************************************************
' EXEMPLO 5: Processar todos os arquivos de uma pasta
'*******************************************************************************
Public Sub Exemplo5_ProcessarPasta()
    Dim pasta As String
    Dim arquivo As String
    Dim contador As Integer

    pasta = "C:\dados\"
    arquivo = Dir(pasta & "*.txt")
    contador = 0

    ' Loop por todos os arquivos .txt
    Do While arquivo <> ""
        Debug.Print "Processando: " & arquivo

        If RemoveInvalidCharsFromFile(pasta & arquivo, "", RemoveControlChars) Then
            contador = contador + 1
        End If

        arquivo = Dir ' Pr�ximo arquivo
    Loop

    MsgBox "Total de arquivos processados: " & contador, vbInformation
End Sub

'*******************************************************************************
' EXEMPLO 6: Remover caracteres customizados espec�ficos
'*******************************************************************************
Public Sub Exemplo6_CaracteresCustomizados()
    Dim arquivo As String
    Dim caracteresRemover As String

    arquivo = "C:\dados\especial.txt"
    caracteresRemover = "@#$%&*"  ' Caracteres a remover

    ' Remove caracteres de controle + caracteres customizados
    If RemoveInvalidCharsFromFile(arquivo, "", RemoveControlChars, caracteresRemover) Then
        MsgBox "Caracteres inv�lidos e especiais removidos!", vbInformation
    End If
End Sub

'*******************************************************************************
' EXEMPLO 7: Processar string diretamente
'*******************************************************************************
Public Sub Exemplo7_ProcessarString()
    Dim textoOriginal As String
    Dim textoLimpo As String

    ' Texto com caracteres de controle
    textoOriginal = "Ol�" & Chr(0) & Chr(1) & Chr(2) & " Mundo" & Chr(127)

    ' Remove caracteres inv�lidos
    textoLimpo = RemoveInvalidChars(textoOriginal, RemoveControlChars)

    Debug.Print "Original: [" & textoOriginal & "]"
    Debug.Print "Limpo: [" & textoLimpo & "]"

    MsgBox "Texto limpo: " & textoLimpo, vbInformation
End Sub

'*******************************************************************************
' EXEMPLO 8: Criar backup antes de processar
'*******************************************************************************
Public Sub Exemplo8_ComBackup()
    Dim arquivoOriginal As String
    Dim arquivoBackup As String

    arquivoOriginal = "C:\importante\dados.txt"
    arquivoBackup = "C:\importante\dados_backup.txt"

    ' Verifica se arquivo existe
    If Not FileExists(arquivoOriginal) Then
        MsgBox "Arquivo n�o encontrado!", vbCritical
        Exit Sub
    End If

    ' Cria backup
    On Error GoTo ErroBackup
    FileCopy arquivoOriginal, arquivoBackup
    MsgBox "Backup criado: " & arquivoBackup, vbInformation

    ' Processa arquivo original
    If RemoveInvalidCharsFromFile(arquivoOriginal, "", RemoveControlChars) Then
        MsgBox "Arquivo processado!" & vbCrLf & _
               "Original preservado em: " & arquivoBackup, vbInformation
    End If

    Exit Sub

ErroBackup:
    MsgBox "Erro ao criar backup: " & Err.Description, vbCritical
End Sub

'*******************************************************************************
' EXEMPLO 9: Processar arquivo CSV
'*******************************************************************************
Public Sub Exemplo9_ProcessarCSV()
    Dim arquivoCSV As String
    Dim arquivoLimpo As String
    Dim quantidade As Long

    arquivoCSV = "C:\dados\vendas.csv"
    arquivoLimpo = "C:\dados\vendas_limpo.csv"

    ' Verifica quantidade de caracteres inv�lidos
    quantidade = GetInvalidCharsCount(arquivoCSV, RemoveControlChars)

    Debug.Print "Arquivo: " & arquivoCSV
    Debug.Print "Caracteres inv�lidos: " & quantidade

    If quantidade > 0 Then
        ' Remove e salva em novo arquivo
        If RemoveInvalidCharsFromFile(arquivoCSV, arquivoLimpo, RemoveControlChars) Then
            MsgBox quantidade & " caracteres inv�lidos removidos!" & vbCrLf & _
                   "Arquivo salvo em: " & arquivoLimpo, vbInformation
        End If
    Else
        MsgBox "CSV est� limpo! Nenhum caractere inv�lido encontrado.", vbInformation
    End If
End Sub

'*******************************************************************************
' EXEMPLO 10: Processar com relat�rio detalhado
'*******************************************************************************
Public Sub Exemplo10_RelatorioDetalhado()
    Dim arquivo As String
    Dim arquivoSaida As String
    Dim countBefore As Long
    Dim relatorio As String

    arquivo = "C:\dados\log.txt"
    arquivoSaida = "C:\dados\log_limpo.txt"

    ' Monta relat�rio
    relatorio = "=== RELAT�RIO DE LIMPEZA ===" & vbCrLf & vbCrLf
    relatorio = relatorio & "Arquivo: " & arquivo & vbCrLf

    ' Conta antes
    countBefore = GetInvalidCharsCount(arquivo, RemoveControlChars)
    relatorio = relatorio & "Caracteres inv�lidos encontrados: " & countBefore & vbCrLf

    If countBefore > 0 Then
        ' Processa
        If RemoveInvalidCharsFromFile(arquivo, arquivoSaida, RemoveControlChars) Then
            relatorio = relatorio & "Status: PROCESSADO COM SUCESSO" & vbCrLf
            relatorio = relatorio & "Arquivo limpo: " & arquivoSaida & vbCrLf
        Else
            relatorio = relatorio & "Status: ERRO AO PROCESSAR" & vbCrLf
        End If
    Else
        relatorio = relatorio & "Status: NENHUM CARACTERE INV�LIDO" & vbCrLf
    End If

    ' Exibe relat�rio
    Debug.Print relatorio
    MsgBox relatorio, vbInformation, "Relat�rio"
End Sub

'*******************************************************************************
' EXEMPLO 11: Processar com tratamento de erro completo
'*******************************************************************************
Public Sub Exemplo11_TratamentoErros()
    On Error GoTo TrataErro

    Dim arquivo As String
    Dim resultado As Boolean

    arquivo = "C:\dados\teste.txt"

    ' Valida��es
    If Trim(arquivo) = "" Then
        Err.Raise vbObjectError + 1, , "Nome do arquivo n�o pode ser vazio"
    End If

    If Not FileExists(arquivo) Then
        Err.Raise vbObjectError + 2, , "Arquivo n�o encontrado: " & arquivo
    End If

    ' Processa
    resultado = RemoveInvalidCharsFromFile(arquivo, "", RemoveControlChars)

    If resultado Then
        MsgBox "Processamento conclu�do com sucesso!", vbInformation
    Else
        Err.Raise vbObjectError + 3, , "Falha ao processar arquivo"
    End If

    Exit Sub

TrataErro:
    Select Case Err.Number
        Case vbObjectError + 1, vbObjectError + 2, vbObjectError + 3
            MsgBox Err.Description, vbCritical, "Erro"
        Case Else
            MsgBox "Erro inesperado: " & Err.Description, vbCritical, "Erro"
    End Select
End Sub
