using Microsoft.Extensions.Configuration;
using NotionNotesReader.App;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NotionNotesReader.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            PrintHeader();

            try
            {
                // Carregar configurações
                var configuration = LoadConfiguration();
                var apiKey = configuration["Notion:ApiKey"];
                var databaseId = configuration["Notion:DatabaseId"];

                // Validar configurações
                if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Contains("SEU_TOKEN"))
                {
                    Console.WriteLine("❌ ERRO: Configure sua API Key do Notion no arquivo appsettings.json");
                    Console.WriteLine("\n📚 Como obter sua API Key:");
                    Console.WriteLine("   1. Acesse https://www.notion.so/my-integrations");
                    Console.WriteLine("   2. Clique em 'New integration'");
                    Console.WriteLine("   3. Dê um nome e selecione o workspace");
                    Console.WriteLine("   4. Copie o 'Internal Integration Token'");
                    Console.WriteLine("   5. Cole no arquivo appsettings.json no campo 'ApiKey'");
                    return;
                }

                if (string.IsNullOrWhiteSpace(databaseId) || databaseId.Contains("SEU_DATABASE"))
                {
                    Console.WriteLine("❌ ERRO: Configure o Database ID no arquivo appsettings.json");
                    Console.WriteLine("\n📚 Como obter o Database ID:");
                    Console.WriteLine("   1. Abra o database no Notion");
                    Console.WriteLine("   2. Copie o link da página");
                    Console.WriteLine("   3. O ID está na URL: notion.so/workspace/[DATABASE_ID]?v=...");
                    Console.WriteLine("   4. Cole no arquivo appsettings.json no campo 'DatabaseId'");
                    Console.WriteLine("\n💡 Dica: Não esqueça de compartilhar o database com sua integração!");
                    return;
                }

                // Criar serviço do Notion
                var notionService = new NotionService(apiKey, databaseId);

                // Menu principal
                await ShowMainMenu(notionService);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erro crítico: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"   Detalhes: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("\n\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        static void PrintHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════╗
║                                                          ║
║        📔  NOTION NOTES READER - Leitor de Anotações    ║
║                                                          ║
╚══════════════════════════════════════════════════════════╝
");
            Console.ResetColor();
        }

        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        static async Task ShowMainMenu(NotionService notionService)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n═══════════════════════════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("📋 MENU PRINCIPAL");
                Console.ResetColor();
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("1. 📝 Listar todas as anotações");
                Console.WriteLine("2. 🔍 Ver conteúdo de uma anotação específica");
                Console.WriteLine("3. 📊 Listar databases disponíveis");
                Console.WriteLine("4. 🚪 Sair");
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.Write("\n➤ Escolha uma opção: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await ListNotes(notionService);
                        break;
                    case "2":
                        await ViewNoteContent(notionService);
                        break;
                    case "3":
                        await ListDatabases(notionService);
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("\n👋 Até logo!");
                        break;
                    default:
                        Console.WriteLine("\n❌ Opção inválida! Tente novamente.");
                        break;
                }
            }
        }

        static async Task ListNotes(NotionService notionService)
        {
            Console.WriteLine("\n\n🔄 Buscando anotações...\n");

            try
            {
                var notes = await notionService.GetNotesAsync();

                if (notes.Count == 0)
                {
                    Console.WriteLine("📭 Nenhuma anotação encontrada no database.");
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ {notes.Count} anotação(ões) encontrada(s):\n");
                Console.ResetColor();

                for (int i = 0; i < notes.Count; i++)
                {
                    Console.WriteLine(notes[i].ToString());

                    if (i < notes.Count - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao listar anotações: {ex.Message}");
            }
        }

        static async Task ViewNoteContent(NotionService notionService)
        {
            Console.Write("\n➤ Digite o ID da página (sem hífens): ");
            var pageId = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(pageId))
            {
                Console.WriteLine("❌ ID inválido!");
                return;
            }

            Console.WriteLine("\n🔄 Buscando conteúdo...\n");

            try
            {
                var content = await notionService.GetPageContentAsync(pageId);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ Conteúdo da página:");
                Console.ResetColor();
                Console.WriteLine("\n" + new string('─', 60));
                Console.WriteLine(content);
                Console.WriteLine(new string('─', 60));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao buscar conteúdo: {ex.Message}");
                Console.WriteLine("💡 Verifique se o ID está correto e se a integração tem acesso à página.");
            }
        }

        static async Task ListDatabases(NotionService notionService)
        {
            Console.WriteLine("\n\n🔄 Buscando databases...\n");

            try
            {
                var databases = await notionService.ListDatabasesAsync();

                if (databases.Count == 0)
                {
                    Console.WriteLine("📭 Nenhum database encontrado.");
                    Console.WriteLine("💡 Certifique-se de que a integração tem acesso aos databases.");
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ {databases.Count} database(s) encontrado(s):\n");
                Console.ResetColor();

                for (int i = 0; i < databases.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {databases[i]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao listar databases: {ex.Message}");
            }
        }
    }
}
