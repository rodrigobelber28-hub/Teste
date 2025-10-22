using System;
using System.IO;
using RemoveInvalidChars.Core;

namespace RemoveInvalidChars.Examples
{
    /// <summary>
    /// Exemplos de uso das funções de remoção de caracteres inválidos
    /// </summary>
    public class ExampleUsage
    {
        /// <summary>
        /// EXEMPLO 1: Uso Básico
        /// </summary>
        public static void Example1_BasicUsage()
        {
            string filePath = @"C:\temp\teste.txt";

            // Verifica se existe
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Arquivo não encontrado: {filePath}");
                return;
            }

            // Processa arquivo (sobrescreve original)
            bool result = InvalidCharRemover.CleanTextFile(filePath);

            if (result)
                Console.WriteLine("Arquivo processado com sucesso!");
            else
                Console.WriteLine("Erro ao processar arquivo!");
        }

        /// <summary>
        /// EXEMPLO 2: Salvar em arquivo diferente
        /// </summary>
        public static void Example2_DifferentFile()
        {
            string sourceFile = @"C:\dados\arquivo_original.txt";
            string destFile = @"C:\dados\arquivo_limpo.txt";

            // Remove caracteres de controle e salva em novo arquivo
            bool result = InvalidCharRemover.RemoveInvalidCharsFromFile(
                sourceFile,
                destFile,
                InvalidCharType.RemoveControlChars);

            if (result)
                Console.WriteLine($"Arquivo limpo salvo em: {destFile}");
        }

        /// <summary>
        /// EXEMPLO 3: Remover múltiplos tipos de caracteres
        /// </summary>
        public static void Example3_MultipleTypes()
        {
            string filePath = @"C:\dados\dados.txt";

            // Combina múltiplos tipos
            var charType = InvalidCharType.RemoveControlChars | InvalidCharType.RemoveExtendedASCII;

            // Processa
            if (InvalidCharRemover.RemoveInvalidCharsFromFile(filePath, null, charType))
            {
                Console.WriteLine("Caracteres de controle e ASCII estendido removidos!");
            }
        }

        /// <summary>
        /// EXEMPLO 4: Contar caracteres inválidos antes de remover
        /// </summary>
        public static void Example4_CountBefore()
        {
            string filePath = @"C:\dados\log.txt";

            // Conta caracteres inválidos
            int count = InvalidCharRemover.GetInvalidCharsCount(
                filePath,
                InvalidCharType.RemoveControlChars);

            if (count > 0)
            {
                Console.WriteLine($"Encontrados {count} caracteres inválidos.");
                Console.WriteLine("Deseja removê-los? (S/N)");

                if (Console.ReadLine()?.ToUpper() == "S")
                {
                    InvalidCharRemover.RemoveInvalidCharsFromFile(
                        filePath,
                        null,
                        InvalidCharType.RemoveControlChars);

                    Console.WriteLine("Caracteres removidos!");
                }
            }
            else
            {
                Console.WriteLine("Nenhum caractere inválido encontrado!");
            }
        }

        /// <summary>
        /// EXEMPLO 5: Processar todos os arquivos de uma pasta
        /// </summary>
        public static void Example5_ProcessFolder()
        {
            string folderPath = @"C:\dados";

            // Processa todos os arquivos .txt da pasta
            int count = InvalidCharRemover.ProcessFolder(
                folderPath,
                "*.txt",
                InvalidCharType.RemoveControlChars);

            Console.WriteLine($"Total de arquivos processados: {count}");
        }

        /// <summary>
        /// EXEMPLO 6: Remover caracteres customizados específicos
        /// </summary>
        public static void Example6_CustomCharacters()
        {
            string filePath = @"C:\dados\especial.txt";
            string customChars = "@#$%&*";

            // Remove caracteres de controle + caracteres customizados
            if (InvalidCharRemover.RemoveInvalidCharsFromFile(
                filePath,
                null,
                InvalidCharType.RemoveControlChars,
                customChars))
            {
                Console.WriteLine("Caracteres inválidos e especiais removidos!");
            }
        }

        /// <summary>
        /// EXEMPLO 7: Processar string diretamente
        /// </summary>
        public static void Example7_ProcessString()
        {
            // Texto com caracteres de controle
            string originalText = "Olá" + (char)0 + (char)1 + (char)2 + " Mundo" + (char)127;

            // Remove caracteres inválidos
            string cleanText = InvalidCharRemover.RemoveInvalidChars(
                originalText,
                InvalidCharType.RemoveControlChars);

            Console.WriteLine($"Original: [{originalText}]");
            Console.WriteLine($"Limpo: [{cleanText}]");
        }

        /// <summary>
        /// EXEMPLO 8: Criar backup antes de processar
        /// </summary>
        public static void Example8_WithBackup()
        {
            string filePath = @"C:\importante\dados.txt";

            // Verifica se arquivo existe
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Arquivo não encontrado!");
                return;
            }

            try
            {
                // Cria backup
                string backupPath = InvalidCharRemover.CreateBackup(filePath);
                Console.WriteLine($"Backup criado: {backupPath}");

                // Processa arquivo original
                if (InvalidCharRemover.RemoveInvalidCharsFromFile(
                    filePath,
                    null,
                    InvalidCharType.RemoveControlChars))
                {
                    Console.WriteLine($"Arquivo processado!");
                    Console.WriteLine($"Original preservado em: {backupPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// EXEMPLO 9: Processar arquivo CSV
        /// </summary>
        public static void Example9_ProcessCSV()
        {
            string csvFile = @"C:\dados\vendas.csv";
            string cleanFile = @"C:\dados\vendas_limpo.csv";

            // Verifica quantidade de caracteres inválidos
            int count = InvalidCharRemover.GetInvalidCharsCount(
                csvFile,
                InvalidCharType.RemoveControlChars);

            Console.WriteLine($"Arquivo: {csvFile}");
            Console.WriteLine($"Caracteres inválidos: {count}");

            if (count > 0)
            {
                // Remove e salva em novo arquivo
                if (InvalidCharRemover.RemoveInvalidCharsFromFile(
                    csvFile,
                    cleanFile,
                    InvalidCharType.RemoveControlChars))
                {
                    Console.WriteLine($"{count} caracteres inválidos removidos!");
                    Console.WriteLine($"Arquivo salvo em: {cleanFile}");
                }
            }
            else
            {
                Console.WriteLine("CSV está limpo! Nenhum caractere inválido encontrado.");
            }
        }

        /// <summary>
        /// EXEMPLO 10: Verificar se contém caracteres inválidos
        /// </summary>
        public static void Example10_CheckInvalidChars()
        {
            string text = "Texto normal sem caracteres inválidos";

            bool hasInvalid = InvalidCharRemover.ContainsInvalidChars(
                text,
                InvalidCharType.RemoveControlChars);

            if (hasInvalid)
                Console.WriteLine("Texto contém caracteres inválidos!");
            else
                Console.WriteLine("Texto está OK!");
        }

        /// <summary>
        /// EXEMPLO 11: Uso com LINQ para processar múltiplos arquivos
        /// </summary>
        public static void Example11_WithLinq()
        {
            string folderPath = @"C:\dados";

            var files = Directory.GetFiles(folderPath, "*.txt");

            var results = files.Select(file => new
            {
                FileName = Path.GetFileName(file),
                InvalidCount = InvalidCharRemover.GetInvalidCharsCount(
                    file,
                    InvalidCharType.RemoveControlChars),
                Processed = InvalidCharRemover.RemoveInvalidCharsFromFile(
                    file,
                    null,
                    InvalidCharType.RemoveControlChars)
            });

            foreach (var result in results)
            {
                Console.WriteLine($"{result.FileName}: " +
                    $"{result.InvalidCount} inválidos, " +
                    $"Processado: {(result.Processed ? "SIM" : "NÃO")}");
            }
        }

        /// <summary>
        /// Método principal para executar todos os exemplos
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("=== EXEMPLOS DE USO - REMOVE INVALID CHARS ===\n");

            // Descomente o exemplo que deseja executar:

            // Example1_BasicUsage();
            // Example2_DifferentFile();
            // Example3_MultipleTypes();
            // Example4_CountBefore();
            // Example5_ProcessFolder();
            // Example6_CustomCharacters();
            // Example7_ProcessString();
            // Example8_WithBackup();
            // Example9_ProcessCSV();
            // Example10_CheckInvalidChars();
            // Example11_WithLinq();

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
