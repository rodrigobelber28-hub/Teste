VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmRemoveInvalidChars
   Caption         =   "Remover Caracteres Inv�lidos de Arquivos TXT"
   ClientHeight    =   6045
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   8415
   LinkTopic       =   "Form1"
   ScaleHeight     =   6045
   ScaleWidth      =   8415
   StartUpPosition =   2  'CenterScreen
   Begin MSComDlg.CommonDialog cdlFile
      Left            =   7680
      Top             =   120
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton cmdClose
      Caption         =   "Fechar"
      Height          =   495
      Left            =   6960
      TabIndex        =   15
      Top             =   5400
      Width           =   1335
   End
   Begin VB.CommandButton cmdProcess
      Caption         =   "Processar Arquivo"
      Height          =   495
      Left            =   5400
      TabIndex        =   14
      Top             =   5400
      Width           =   1455
   End
   Begin VB.TextBox txtLog
      Height          =   1935
      Left            =   120
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   13
      Top             =   3240
      Width           =   8175
   End
   Begin VB.Frame Frame2
      Caption         =   "Op��es de Remo��o"
      Height          =   1335
      Left            =   120
      TabIndex        =   6
      Top             =   1800
      Width           =   8175
      Begin VB.TextBox txtCustomChars
         Height          =   285
         Left            =   1920
         TabIndex        =   11
         Top             =   840
         Width           =   6015
      End
      Begin VB.CheckBox chkCustomChars
         Caption         =   "Caracteres Customizados:"
         Height          =   255
         Left            =   120
         TabIndex        =   10
         Top             =   840
         Width           =   1815
      End
      Begin VB.CheckBox chkExtendedASCII
         Caption         =   "Remover ASCII Estendido (128-255)"
         Height          =   255
         Left            =   120
         TabIndex        =   9
         Top             =   600
         Width           =   2895
      End
      Begin VB.CheckBox chkNonPrintable
         Caption         =   "Remover N�o-Imprim�veis"
         Height          =   255
         Left            =   4200
         TabIndex        =   8
         Top             =   360
         Width           =   2295
      End
      Begin VB.CheckBox chkControlChars
         Caption         =   "Remover Caracteres de Controle (0-31)"
         Height          =   255
         Left            =   120
         TabIndex        =   7
         Top             =   360
         Width           =   3255
      End
   End
   Begin VB.Frame Frame1
      Caption         =   "Arquivos"
      Height          =   1575
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   8175
      Begin VB.CommandButton cmdBrowseDest
         Caption         =   "..."
         Height          =   285
         Left            =   7680
         TabIndex        =   5
         Top             =   1080
         Width           =   375
      End
      Begin VB.TextBox txtDestFile
         Height          =   285
         Left            =   1440
         TabIndex        =   4
         Top             =   1080
         Width           =   6135
      End
      Begin VB.CommandButton cmdBrowseSource
         Caption         =   "..."
         Height          =   285
         Left            =   7680
         TabIndex        =   3
         Top             =   600
         Width           =   375
      End
      Begin VB.TextBox txtSourceFile
         Height          =   285
         Left            =   1440
         TabIndex        =   2
         Top             =   600
         Width           =   6135
      End
      Begin VB.Label Label2
         Caption         =   "Arquivo Destino:"
         Height          =   255
         Left            =   120
         TabIndex        =   12
         Top             =   1080
         Width           =   1215
      End
      Begin VB.Label Label1
         Caption         =   "Arquivo Origem:"
         Height          =   255
         Left            =   120
         TabIndex        =   1
         Top             =   600
         Width           =   1215
      End
   End
End
Attribute VB_Name = "frmRemoveInvalidChars"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'*******************************************************************************
' Formul�rio: frmRemoveInvalidChars
' Descri��o: Interface para remover caracteres inv�lidos de arquivos TXT
'*******************************************************************************

Option Explicit

Private Sub Form_Load()
    'Configura��es iniciais
    chkControlChars.Value = 1 'Marcado por padr�o
    txtLog.Text = "Pronto para processar arquivos..." & vbCrLf
End Sub

Private Sub cmdBrowseSource_Click()
    On Error GoTo ErrorHandler

    cdlFile.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*"
    cdlFile.FilterIndex = 1
    cdlFile.Flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly
    cdlFile.ShowOpen

    If cdlFile.fileName <> "" Then
        txtSourceFile.Text = cdlFile.fileName

        'Sugere nome para arquivo de destino
        If txtDestFile.Text = "" Then
            Dim pos As Integer
            pos = InStrRev(cdlFile.fileName, ".")
            If pos > 0 Then
                txtDestFile.Text = Left(cdlFile.fileName, pos - 1) & "_clean" & Mid(cdlFile.fileName, pos)
            Else
                txtDestFile.Text = cdlFile.fileName & "_clean.txt"
            End If
        End If
    End If

    Exit Sub
ErrorHandler:
    If Err.Number <> 32755 Then 'Cancelado pelo usu�rio
        MsgBox "Erro ao selecionar arquivo: " & Err.Description, vbCritical
    End If
End Sub

Private Sub cmdBrowseDest_Click()
    On Error GoTo ErrorHandler

    cdlFile.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*"
    cdlFile.FilterIndex = 1
    cdlFile.Flags = cdlOFNOverwritePrompt Or cdlOFNHideReadOnly
    cdlFile.ShowSave

    If cdlFile.fileName <> "" Then
        txtDestFile.Text = cdlFile.fileName
    End If

    Exit Sub
ErrorHandler:
    If Err.Number <> 32755 Then 'Cancelado pelo usu�rio
        MsgBox "Erro ao selecionar arquivo: " & Err.Description, vbCritical
    End If
End Sub

Private Sub cmdProcess_Click()
    Dim charType As InvalidCharType
    Dim customChars As String
    Dim invalidCount As Long
    Dim success As Boolean

    'Valida��o
    If Trim(txtSourceFile.Text) = "" Then
        MsgBox "Selecione o arquivo de origem!", vbExclamation
        txtSourceFile.SetFocus
        Exit Sub
    End If

    If Not FileExists(txtSourceFile.Text) Then
        MsgBox "Arquivo de origem n�o encontrado!", vbCritical
        Exit Sub
    End If

    'Monta tipo de caracteres a remover
    charType = 0
    If chkControlChars.Value = 1 Then charType = charType Or RemoveControlChars
    If chkExtendedASCII.Value = 1 Then charType = charType Or RemoveExtendedASCII
    If chkNonPrintable.Value = 1 Then charType = charType Or RemoveNonPrintable

    'Caracteres customizados
    If chkCustomChars.Value = 1 Then
        customChars = txtCustomChars.Text
    Else
        customChars = ""
    End If

    'Log
    AddLog "Iniciando processamento..."
    AddLog "Arquivo: " & txtSourceFile.Text

    'Conta caracteres inv�lidos antes
    invalidCount = GetInvalidCharsCount(txtSourceFile.Text, charType)
    If invalidCount >= 0 Then
        AddLog "Caracteres inv�lidos encontrados: " & invalidCount
    End If

    'Processa arquivo
    Me.MousePointer = vbHourglass
    DoEvents

    success = RemoveInvalidCharsFromFile(txtSourceFile.Text, txtDestFile.Text, charType, customChars)

    Me.MousePointer = vbDefault

    If success Then
        AddLog "Arquivo processado com sucesso!"
        AddLog "Arquivo salvo em: " & txtDestFile.Text
        MsgBox "Arquivo processado com sucesso!" & vbCrLf & vbCrLf & _
               "Caracteres inv�lidos removidos: " & invalidCount & vbCrLf & _
               "Arquivo salvo em: " & vbCrLf & txtDestFile.Text, vbInformation
    Else
        AddLog "Erro ao processar arquivo!"
    End If
End Sub

Private Sub cmdClose_Click()
    Unload Me
End Sub

Private Sub AddLog(ByVal message As String)
    txtLog.Text = txtLog.Text & Format(Now, "hh:nn:ss") & " - " & message & vbCrLf
    txtLog.SelStart = Len(txtLog.Text)
End Sub
