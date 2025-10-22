Attribute VB_Name = "modRemoveInvalidChars"
'*******************************************************************************
' M�dulo: modRemoveInvalidChars
' Descri��o: Fun��es para remover caracteres inv�lidos de arquivos de texto
' Autor: Sistema
' Data: 2025
'*******************************************************************************

Option Explicit

'Constantes para tipos de caracteres inv�lidos
Public Enum InvalidCharType
    RemoveControlChars = 1      'Remove caracteres de controle (0-31, exceto Tab, CR, LF)
    RemoveExtendedASCII = 2     'Remove caracteres ASCII estendido (128-255)
    RemoveNonPrintable = 4      'Remove todos caracteres n�o-imprim�veis
    RemoveSpecialChars = 8      'Remove caracteres especiais definidos
    RemoveAll = 15              'Remove todos os tipos acima
End Enum

'*******************************************************************************
' Fun��o: RemoveInvalidCharsFromFile
' Descri��o: Remove caracteres inv�lidos de um arquivo txt
' Par�metros:
'   - sourceFile: Caminho completo do arquivo de origem
'   - destFile: Caminho completo do arquivo de destino (vazio = sobrescrever origem)
'   - charType: Tipo de caracteres a remover (InvalidCharType)
'   - customChars: String com caracteres customizados para remover
' Retorno: True se sucesso, False se erro
'*******************************************************************************
Public Function RemoveInvalidCharsFromFile(ByVal sourceFile As String, _
                                           Optional ByVal destFile As String = "", _
                                           Optional ByVal charType As InvalidCharType = RemoveControlChars, _
                                           Optional ByVal customChars As String = "") As Boolean

    On Error GoTo ErrorHandler

    Dim fileNum As Integer
    Dim fileContent As String
    Dim cleanContent As String
    Dim outputFile As String

    'Verifica se arquivo existe
    If Not FileExists(sourceFile) Then
        MsgBox "Arquivo n�o encontrado: " & sourceFile, vbCritical, "Erro"
        RemoveInvalidCharsFromFile = False
        Exit Function
    End If

    'Define arquivo de sa�da
    If Len(Trim(destFile)) = 0 Then
        outputFile = sourceFile
    Else
        outputFile = destFile
    End If

    'L� conte�do do arquivo
    fileNum = FreeFile
    Open sourceFile For Binary As #fileNum
    fileContent = Space$(LOF(fileNum))
    Get #fileNum, , fileContent
    Close #fileNum

    'Remove caracteres inv�lidos
    cleanContent = RemoveInvalidChars(fileContent, charType, customChars)

    'Salva arquivo limpo
    fileNum = FreeFile
    Open outputFile For Binary As #fileNum
    Put #fileNum, , cleanContent
    Close #fileNum

    RemoveInvalidCharsFromFile = True
    Exit Function

ErrorHandler:
    If fileNum > 0 Then Close #fileNum
    MsgBox "Erro ao processar arquivo: " & Err.Description, vbCritical, "Erro"
    RemoveInvalidCharsFromFile = False
End Function

'*******************************************************************************
' Fun��o: RemoveInvalidChars
' Descri��o: Remove caracteres inv�lidos de uma string
' Par�metros:
'   - text: Texto a processar
'   - charType: Tipo de caracteres a remover
'   - customChars: Caracteres customizados para remover
' Retorno: String limpa
'*******************************************************************************
Public Function RemoveInvalidChars(ByVal text As String, _
                                   Optional ByVal charType As InvalidCharType = RemoveControlChars, _
                                   Optional ByVal customChars As String = "") As String

    Dim i As Long
    Dim char As String
    Dim asciiVal As Integer
    Dim result As String
    Dim shouldRemove As Boolean

    result = ""

    For i = 1 To Len(text)
        char = Mid$(text, i, 1)
        asciiVal = Asc(char)
        shouldRemove = False

        'Verifica caracteres de controle (exceto Tab=9, LF=10, CR=13)
        If (charType And RemoveControlChars) = RemoveControlChars Then
            If asciiVal < 32 And asciiVal <> 9 And asciiVal <> 10 And asciiVal <> 13 Then
                shouldRemove = True
            End If
            If asciiVal = 127 Then shouldRemove = True 'DEL
        End If

        'Verifica ASCII estendido
        If (charType And RemoveExtendedASCII) = RemoveExtendedASCII Then
            If asciiVal > 127 Then
                shouldRemove = True
            End If
        End If

        'Verifica caracteres n�o-imprim�veis
        If (charType And RemoveNonPrintable) = RemoveNonPrintable Then
            If asciiVal < 32 And asciiVal <> 9 And asciiVal <> 10 And asciiVal <> 13 Then
                shouldRemove = True
            End If
            If asciiVal = 127 Then shouldRemove = True
        End If

        'Verifica caracteres customizados
        If Len(customChars) > 0 Then
            If InStr(1, customChars, char) > 0 Then
                shouldRemove = True
            End If
        End If

        'Adiciona caractere se n�o deve ser removido
        If Not shouldRemove Then
            result = result & char
        End If
    Next i

    RemoveInvalidChars = result
End Function

'*******************************************************************************
' Fun��o: RemoveInvalidCharsFromFileAdvanced
' Descri��o: Vers�o avan�ada com callback de progresso
' Par�metros:
'   - sourceFile: Caminho do arquivo de origem
'   - destFile: Caminho do arquivo de destino
'   - charType: Tipo de caracteres a remover
'   - customChars: Caracteres customizados
'   - replaceWith: Caractere para substituir inv�lidos (vazio = remover)
' Retorno: True se sucesso
'*******************************************************************************
Public Function RemoveInvalidCharsFromFileAdvanced(ByVal sourceFile As String, _
                                                    Optional ByVal destFile As String = "", _
                                                    Optional ByVal charType As InvalidCharType = RemoveControlChars, _
                                                    Optional ByVal customChars As String = "", _
                                                    Optional ByVal replaceWith As String = "") As Boolean

    On Error GoTo ErrorHandler

    Dim fileNum As Integer
    Dim outFileNum As Integer
    Dim fileContent As String
    Dim cleanContent As String
    Dim outputFile As String
    Dim i As Long
    Dim char As String
    Dim asciiVal As Integer
    Dim shouldRemove As Boolean

    'Verifica se arquivo existe
    If Not FileExists(sourceFile) Then
        MsgBox "Arquivo n�o encontrado: " & sourceFile, vbCritical, "Erro"
        RemoveInvalidCharsFromFileAdvanced = False
        Exit Function
    End If

    'Define arquivo de sa�da
    If Len(Trim(destFile)) = 0 Then
        outputFile = sourceFile & ".tmp"
    Else
        outputFile = destFile
    End If

    'L� arquivo
    fileNum = FreeFile
    Open sourceFile For Binary As #fileNum
    fileContent = Space$(LOF(fileNum))
    Get #fileNum, , fileContent
    Close #fileNum

    'Processa caracteres
    cleanContent = ""
    For i = 1 To Len(fileContent)
        char = Mid$(fileContent, i, 1)
        asciiVal = Asc(char)
        shouldRemove = False

        'Verifica se deve remover
        If (charType And RemoveControlChars) = RemoveControlChars Then
            If asciiVal < 32 And asciiVal <> 9 And asciiVal <> 10 And asciiVal <> 13 Then
                shouldRemove = True
            End If
            If asciiVal = 127 Then shouldRemove = True
        End If

        If (charType And RemoveExtendedASCII) = RemoveExtendedASCII Then
            If asciiVal > 127 Then shouldRemove = True
        End If

        If Len(customChars) > 0 Then
            If InStr(1, customChars, char) > 0 Then shouldRemove = True
        End If

        'Adiciona ou substitui
        If shouldRemove Then
            If Len(replaceWith) > 0 Then
                cleanContent = cleanContent & replaceWith
            End If
        Else
            cleanContent = cleanContent & char
        End If
    Next i

    'Salva arquivo
    outFileNum = FreeFile
    Open outputFile For Binary As #outFileNum
    Put #outFileNum, , cleanContent
    Close #outFileNum

    'Se era tempor�rio, substitui original
    If destFile = "" Then
        Kill sourceFile
        Name outputFile As sourceFile
    End If

    RemoveInvalidCharsFromFileAdvanced = True
    Exit Function

ErrorHandler:
    If fileNum > 0 Then Close #fileNum
    If outFileNum > 0 Then Close #outFileNum
    MsgBox "Erro: " & Err.Description, vbCritical, "Erro"
    RemoveInvalidCharsFromFileAdvanced = False
End Function

'*******************************************************************************
' Fun��o: FileExists
' Descri��o: Verifica se arquivo existe
'*******************************************************************************
Public Function FileExists(ByVal fileName As String) As Boolean
    On Error Resume Next
    FileExists = (Dir$(fileName) <> "")
End Function

'*******************************************************************************
' Fun��o: CleanTextFile
' Descri��o: Fun��o simplificada para limpeza r�pida
'*******************************************************************************
Public Function CleanTextFile(ByVal filePath As String) As Boolean
    CleanTextFile = RemoveInvalidCharsFromFile(filePath, "", RemoveControlChars)
End Function

'*******************************************************************************
' Fun��o: GetInvalidCharsCount
' Descri��o: Conta quantos caracteres inv�lidos existem no arquivo
'*******************************************************************************
Public Function GetInvalidCharsCount(ByVal filePath As String, _
                                     Optional ByVal charType As InvalidCharType = RemoveControlChars) As Long

    On Error GoTo ErrorHandler

    Dim fileNum As Integer
    Dim fileContent As String
    Dim i As Long
    Dim char As String
    Dim asciiVal As Integer
    Dim count As Long

    If Not FileExists(filePath) Then
        GetInvalidCharsCount = -1
        Exit Function
    End If

    fileNum = FreeFile
    Open filePath For Binary As #fileNum
    fileContent = Space$(LOF(fileNum))
    Get #fileNum, , fileContent
    Close #fileNum

    count = 0
    For i = 1 To Len(fileContent)
        char = Mid$(fileContent, i, 1)
        asciiVal = Asc(char)

        If (charType And RemoveControlChars) = RemoveControlChars Then
            If asciiVal < 32 And asciiVal <> 9 And asciiVal <> 10 And asciiVal <> 13 Then
                count = count + 1
            End If
            If asciiVal = 127 Then count = count + 1
        End If

        If (charType And RemoveExtendedASCII) = RemoveExtendedASCII Then
            If asciiVal > 127 Then count = count + 1
        End If
    Next i

    GetInvalidCharsCount = count
    Exit Function

ErrorHandler:
    If fileNum > 0 Then Close #fileNum
    GetInvalidCharsCount = -1
End Function
