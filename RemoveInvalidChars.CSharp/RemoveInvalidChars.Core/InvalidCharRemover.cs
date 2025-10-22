using System;
using System.IO;
using System.Text;
using System.Linq;

namespace RemoveInvalidChars.Core
{
    /// <summary>
    /// Tipos de caracteres inválidos que podem ser removidos
    /// </summary>
    [Flags]
    public enum InvalidCharType
    {
        /// <summary>Remove caracteres de controle (ASCII 0-31, exceto Tab, CR, LF)</summary>
        RemoveControlChars = 1,

        /// <summary>Remove caracteres ASCII estendido (128-255)</summary>
        RemoveExtendedASCII = 2,

        /// <summary>Remove todos caracteres não-imprimíveis</summary>
        RemoveNonPrintable = 4,

        /// <summary>Remove caracteres especiais definidos</summary>
        RemoveSpecialChars = 8,

        /// <summary>Remove todos os tipos acima</summary>
        RemoveAll = 15
    }

    /// <summary>
    /// Classe principal para remover caracteres inválidos de arquivos e strings
    /// </summary>
    public class InvalidCharRemover
    {
        /// <summary>
        /// Remove caracteres inválidos de um arquivo de texto
        /// </summary>
        /// <param name="sourceFile">Caminho do arquivo de origem</param>
        /// <param name="destFile">Caminho do arquivo de destino (null = sobrescrever origem)</param>
        /// <param name="charType">Tipo de caracteres a remover</param>
        /// <param name="customChars">Caracteres customizados para remover</param>
        /// <param name="encoding">Codificação do arquivo (null = UTF-8)</param>
        /// <returns>True se sucesso, False se erro</returns>
        public static bool RemoveInvalidCharsFromFile(
            string sourceFile,
            string destFile = null,
            InvalidCharType charType = InvalidCharType.RemoveControlChars,
            string customChars = "",
            Encoding encoding = null)
        {
            try
            {
                if (!File.Exists(sourceFile))
                {
                    throw new FileNotFoundException($"Arquivo não encontrado: {sourceFile}");
                }

                // Define codificação padrão
                encoding = encoding ?? Encoding.UTF8;

                // Lê conteúdo do arquivo
                string fileContent = File.ReadAllText(sourceFile, encoding);

                // Remove caracteres inválidos
                string cleanContent = RemoveInvalidChars(fileContent, charType, customChars);

                // Define arquivo de saída
                string outputFile = string.IsNullOrWhiteSpace(destFile) ? sourceFile : destFile;

                // Salva arquivo limpo
                File.WriteAllText(outputFile, cleanContent, encoding);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar arquivo: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Remove caracteres inválidos de uma string
        /// </summary>
        /// <param name="text">Texto a processar</param>
        /// <param name="charType">Tipo de caracteres a remover</param>
        /// <param name="customChars">Caracteres customizados para remover</param>
        /// <returns>String limpa</returns>
        public static string RemoveInvalidChars(
            string text,
            InvalidCharType charType = InvalidCharType.RemoveControlChars,
            string customChars = "")
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var result = new StringBuilder(text.Length);

            foreach (char c in text)
            {
                bool shouldRemove = false;
                int asciiVal = (int)c;

                // Verifica caracteres de controle (exceto Tab=9, LF=10, CR=13)
                if (charType.HasFlag(InvalidCharType.RemoveControlChars))
                {
                    if (asciiVal < 32 && asciiVal != 9 && asciiVal != 10 && asciiVal != 13)
                        shouldRemove = true;

                    if (asciiVal == 127) // DEL
                        shouldRemove = true;
                }

                // Verifica ASCII estendido
                if (charType.HasFlag(InvalidCharType.RemoveExtendedASCII))
                {
                    if (asciiVal > 127)
                        shouldRemove = true;
                }

                // Verifica caracteres não-imprimíveis
                if (charType.HasFlag(InvalidCharType.RemoveNonPrintable))
                {
                    if (asciiVal < 32 && asciiVal != 9 && asciiVal != 10 && asciiVal != 13)
                        shouldRemove = true;

                    if (asciiVal == 127)
                        shouldRemove = true;
                }

                // Verifica caracteres customizados
                if (!string.IsNullOrEmpty(customChars) && customChars.Contains(c))
                {
                    shouldRemove = true;
                }

                // Adiciona caractere se não deve ser removido
                if (!shouldRemove)
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Conta quantos caracteres inválidos existem no arquivo
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="charType">Tipo de caracteres a contar</param>
        /// <param name="encoding">Codificação do arquivo</param>
        /// <returns>Quantidade de caracteres inválidos (-1 se erro)</returns>
        public static int GetInvalidCharsCount(
            string filePath,
            InvalidCharType charType = InvalidCharType.RemoveControlChars,
            Encoding encoding = null)
        {
            try
            {
                if (!File.Exists(filePath))
                    return -1;

                encoding = encoding ?? Encoding.UTF8;
                string fileContent = File.ReadAllText(filePath, encoding);

                int count = 0;

                foreach (char c in fileContent)
                {
                    int asciiVal = (int)c;

                    if (charType.HasFlag(InvalidCharType.RemoveControlChars))
                    {
                        if (asciiVal < 32 && asciiVal != 9 && asciiVal != 10 && asciiVal != 13)
                            count++;

                        if (asciiVal == 127)
                            count++;
                    }

                    if (charType.HasFlag(InvalidCharType.RemoveExtendedASCII))
                    {
                        if (asciiVal > 127)
                            count++;
                    }
                }

                return count;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Função simplificada para limpeza rápida
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <returns>True se sucesso</returns>
        public static bool CleanTextFile(string filePath)
        {
            return RemoveInvalidCharsFromFile(filePath, null, InvalidCharType.RemoveControlChars);
        }

        /// <summary>
        /// Processa múltiplos arquivos em uma pasta
        /// </summary>
        /// <param name="folderPath">Caminho da pasta</param>
        /// <param name="searchPattern">Padrão de busca (ex: "*.txt")</param>
        /// <param name="charType">Tipo de caracteres a remover</param>
        /// <param name="customChars">Caracteres customizados</param>
        /// <returns>Quantidade de arquivos processados</returns>
        public static int ProcessFolder(
            string folderPath,
            string searchPattern = "*.txt",
            InvalidCharType charType = InvalidCharType.RemoveControlChars,
            string customChars = "")
        {
            try
            {
                if (!Directory.Exists(folderPath))
                    return 0;

                var files = Directory.GetFiles(folderPath, searchPattern);
                int processedCount = 0;

                foreach (var file in files)
                {
                    if (RemoveInvalidCharsFromFile(file, null, charType, customChars))
                        processedCount++;
                }

                return processedCount;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Verifica se uma string contém caracteres inválidos
        /// </summary>
        /// <param name="text">Texto a verificar</param>
        /// <param name="charType">Tipo de caracteres a verificar</param>
        /// <returns>True se contém caracteres inválidos</returns>
        public static bool ContainsInvalidChars(
            string text,
            InvalidCharType charType = InvalidCharType.RemoveControlChars)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            foreach (char c in text)
            {
                int asciiVal = (int)c;

                if (charType.HasFlag(InvalidCharType.RemoveControlChars))
                {
                    if (asciiVal < 32 && asciiVal != 9 && asciiVal != 10 && asciiVal != 13)
                        return true;

                    if (asciiVal == 127)
                        return true;
                }

                if (charType.HasFlag(InvalidCharType.RemoveExtendedASCII))
                {
                    if (asciiVal > 127)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Cria backup de um arquivo antes de processar
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="backupSuffix">Sufixo do backup (padrão: "_backup")</param>
        /// <returns>Caminho do arquivo de backup</returns>
        public static string CreateBackup(string filePath, string backupSuffix = "_backup")
        {
            try
            {
                if (!File.Exists(filePath))
                    return null;

                string directory = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);

                string backupPath = Path.Combine(directory, $"{fileName}{backupSuffix}{extension}");

                File.Copy(filePath, backupPath, true);

                return backupPath;
            }
            catch
            {
                return null;
            }
        }
    }
}
