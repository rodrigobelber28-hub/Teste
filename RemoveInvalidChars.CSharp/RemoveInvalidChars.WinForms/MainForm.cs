using System;
using System.IO;
using System.Windows.Forms;
using RemoveInvalidChars.Core;

namespace RemoveInvalidChars.WinForms
{
    /// <summary>
    /// Formulário principal para remover caracteres inválidos de arquivos TXT
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Configurações iniciais
            chkControlChars.Checked = true;
            AddLog("Pronto para processar arquivos...");
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
                openFileDialog.Title = "Selecionar Arquivo de Origem";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtSourceFile.Text = openFileDialog.FileName;

                    // Sugere nome para arquivo de destino
                    if (string.IsNullOrWhiteSpace(txtDestFile.Text))
                    {
                        string directory = Path.GetDirectoryName(openFileDialog.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                        string extension = Path.GetExtension(openFileDialog.FileName);

                        txtDestFile.Text = Path.Combine(directory, $"{fileName}_clean{extension}");
                    }
                }
            }
        }

        private void btnBrowseDest_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
                saveFileDialog.Title = "Selecionar Arquivo de Destino";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDestFile.Text = saveFileDialog.FileName;
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtSourceFile.Text))
            {
                MessageBox.Show("Selecione o arquivo de origem!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSourceFile.Focus();
                return;
            }

            if (!File.Exists(txtSourceFile.Text))
            {
                MessageBox.Show("Arquivo de origem não encontrado!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Monta tipo de caracteres a remover
                InvalidCharType charType = 0;
                if (chkControlChars.Checked)
                    charType |= InvalidCharType.RemoveControlChars;
                if (chkExtendedASCII.Checked)
                    charType |= InvalidCharType.RemoveExtendedASCII;
                if (chkNonPrintable.Checked)
                    charType |= InvalidCharType.RemoveNonPrintable;

                // Caracteres customizados
                string customChars = chkCustomChars.Checked ? txtCustomChars.Text : "";

                // Log
                AddLog("Iniciando processamento...");
                AddLog($"Arquivo: {txtSourceFile.Text}");

                // Conta caracteres inválidos antes
                int invalidCount = InvalidCharRemover.GetInvalidCharsCount(txtSourceFile.Text, charType);
                if (invalidCount >= 0)
                {
                    AddLog($"Caracteres inválidos encontrados: {invalidCount}");
                }

                // Processa arquivo
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                bool success = InvalidCharRemover.RemoveInvalidCharsFromFile(
                    txtSourceFile.Text,
                    txtDestFile.Text,
                    charType,
                    customChars);

                Cursor = Cursors.Default;

                if (success)
                {
                    AddLog("Arquivo processado com sucesso!");
                    AddLog($"Arquivo salvo em: {txtDestFile.Text}");

                    MessageBox.Show(
                        $"Arquivo processado com sucesso!\n\n" +
                        $"Caracteres inválidos removidos: {invalidCount}\n" +
                        $"Arquivo salvo em:\n{txtDestFile.Text}",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    AddLog("Erro ao processar arquivo!");
                    MessageBox.Show("Erro ao processar arquivo!", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                AddLog($"Erro: {ex.Message}");
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddLog(string message)
        {
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}\r\n");
        }

        private void chkCustomChars_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomChars.Enabled = chkCustomChars.Checked;
        }
    }
}
