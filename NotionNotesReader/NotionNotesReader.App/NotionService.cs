using Notion.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotionNotesReader.App
{
    /// <summary>
    /// Serviço para interagir com a API do Notion e ler anotações
    /// </summary>
    public class NotionService
    {
        private readonly NotionClient _notionClient;
        private readonly string _databaseId;

        public NotionService(string apiKey, string databaseId)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API Key do Notion é obrigatória", nameof(apiKey));

            if (string.IsNullOrWhiteSpace(databaseId))
                throw new ArgumentException("Database ID é obrigatório", nameof(databaseId));

            _notionClient = NotionClientFactory.Create(new ClientOptions
            {
                AuthToken = apiKey
            });
            _databaseId = databaseId;
        }

        /// <summary>
        /// Lista todas as páginas (anotações) do database do Notion
        /// </summary>
        public async Task<List<NotionNote>> GetNotesAsync()
        {
            try
            {
                var queryParams = new DatabasesQueryParameters();
                var pages = await _notionClient.Databases.QueryAsync(_databaseId, queryParams);

                var notes = new List<NotionNote>();

                foreach (var page in pages.Results)
                {
                    var note = new NotionNote
                    {
                        Id = page.Id,
                        CreatedTime = page.CreatedTime,
                        LastEditedTime = page.LastEditedTime,
                        Url = page.Url
                    };

                    // Extrair título
                    if (page.Properties.ContainsKey("Name") || page.Properties.ContainsKey("Title"))
                    {
                        var titleKey = page.Properties.ContainsKey("Name") ? "Name" : "Title";
                        var titleProperty = page.Properties[titleKey] as TitlePropertyValue;
                        if (titleProperty?.Title != null && titleProperty.Title.Any())
                        {
                            note.Title = string.Join("", titleProperty.Title.Select(t => t.PlainText));
                        }
                    }

                    notes.Add(note);
                }

                return notes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar anotações: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtém o conteúdo completo de uma página específica
        /// </summary>
        public async Task<string> GetPageContentAsync(string pageId)
        {
            try
            {
                var blocks = await _notionClient.Blocks.RetrieveChildrenAsync(pageId);
                var content = new System.Text.StringBuilder();

                foreach (var block in blocks.Results)
                {
                    content.AppendLine(ExtractTextFromBlock(block));
                }

                return content.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar conteúdo da página: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Extrai texto de diferentes tipos de blocos do Notion
        /// </summary>
        private string ExtractTextFromBlock(IBlock block)
        {
            return block switch
            {
                ParagraphBlock paragraph => ExtractRichText(paragraph.Paragraph.RichText),
                HeadingOneBlock h1 => $"# {ExtractRichText(h1.Heading_1.RichText)}",
                HeadingTwoBlock h2 => $"## {ExtractRichText(h2.Heading_2.RichText)}",
                HeadingThreeBlock h3 => $"### {ExtractRichText(h3.Heading_3.RichText)}",
                BulletedListItemBlock bullet => $"• {ExtractRichText(bullet.BulletedListItem.RichText)}",
                NumberedListItemBlock numbered => $"1. {ExtractRichText(numbered.NumberedListItem.RichText)}",
                ToDoBlock todo => $"[{(todo.ToDo.Checked == true ? "x" : " ")}] {ExtractRichText(todo.ToDo.RichText)}",
                CodeBlock code => $"```\n{ExtractRichText(code.Code.RichText)}\n```",
                QuoteBlock quote => $"> {ExtractRichText(quote.Quote.RichText)}",
                _ => ""
            };
        }

        /// <summary>
        /// Extrai texto simples de rich text do Notion
        /// </summary>
        private string ExtractRichText(IEnumerable<RichTextBase>? richTexts)
        {
            if (richTexts == null) return string.Empty;
            return string.Join("", richTexts.Select(rt => rt.PlainText));
        }

        /// <summary>
        /// Lista todos os databases disponíveis
        /// </summary>
        public async Task<List<string>> ListDatabasesAsync()
        {
            try
            {
                var search = await _notionClient.Search.SearchAsync(new SearchParameters
                {
                    Filter = new SearchFilter { Property = "object", Value = "database" }
                });

                var databases = new List<string>();
                foreach (var result in search.Results)
                {
                    if (result is Database db)
                    {
                        var title = db.Title.Any() ? string.Join("", db.Title.Select(t => t.PlainText)) : "Sem título";
                        databases.Add($"{title} (ID: {db.Id})");
                    }
                }

                return databases;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar databases: {ex.Message}");
                throw;
            }
        }
    }

    /// <summary>
    /// Modelo representando uma anotação do Notion
    /// </summary>
    public class NotionNote
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = "Sem título";
        public DateTime CreatedTime { get; set; }
        public DateTime LastEditedTime { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"""
                    ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
                    📝 {Title}
                    ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
                    🆔 ID: {Id}
                    📅 Criado: {CreatedTime:dd/MM/yyyy HH:mm}
                    ✏️  Editado: {LastEditedTime:dd/MM/yyyy HH:mm}
                    🔗 URL: {Url}
                    """;
        }
    }
}
