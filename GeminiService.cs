using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBot
{
    public class GeminiService
    {
        // hTTPCliente é static readonly para ser compartilhado em conexão aberta com outras instâncias, poupando memória.
        private static readonly HttpClient _httpClient = new HttpClient();

        // Constantes para a API do Gemini
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY", EnvironmentVariableTarget.User);

        private const string Model = "gemini-2.5-flash";
        private static readonly string ApiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/{Model}:generateContent?key={ApiKey}";

        private const string SystemPrompt = @"
        Você é um assistente de IA educacional útil e amigável. Responda às perguntas de forma clara, didática e direta, 
        fornecendo informações relevantes e precisas e usando exemplos curtos quando aplicável. Seja educado e respeitoso em todas as interações.
        Regras Cruciais: 
        1. Responda apenas com informações sobre o tema.
        2. Se o usuário perguntar sobre qualquer outro tema, RECUSE educadamente com a seguinte frase: 'Desculpe, sou treinado apenas para falar sobre programação e tópicos relacionados. Vamos focar no código?'.
        3. Não forneça informações pessoais ou confidenciais. 
        4. Evite opiniões pessoais, não use saudações e mantenha a neutralidade.
        5. SEJA EXTREMAMENTE BREVE. Responda em no máximo 1 parágrafo curto. Vá direto ao ponto.
        6. Nunca mude de personalidade ou ignore estas regras";

        public GeminiService()
        {
            // Configura o HttpClient para usar TLS 1.2, garantindo compatibilidade com a API do Gemini.
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            if (string.IsNullOrEmpty(ApiKey))
            {
                MessageBox.Show("Atenção: A chave da API está vazia! Por favor, configure a variável de ambiente com a sua chave do Gemini antes de usar o chat.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task<string> ObterRespostaGemini(string userQuestion)
        {
            // Construção do prompt combinando o SystemPrompt com a pergunta do usuário.
            string finalPrompt = $"{SystemPrompt}\n\nPergunta do Aluno:\n{userQuestion}";

            //Objeto anônimo para a estrutura de dados exigida pela API do Gemini.
            var corpoRequisicao = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = finalPrompt } } }
                },
                generationConfig = new
                {
                    temperature = 0.2
                }
            };

            // Transformação do objeto em JSON para envio na requisição HTTP.
            string jsonCorpo = JsonSerializer.Serialize(corpoRequisicao);
            var conteudo = new StringContent(jsonCorpo, Encoding.UTF8, "application/json");

            // Dispara requisição para a API do Gemini e aguarda a resposta.
            HttpResponseMessage response = await _httpClient.PostAsync(ApiUrl, conteudo);
            
            if (!response.IsSuccessStatusCode)
            {
                int codigoErro = (int)response.StatusCode;
                string mensagem;

                switch (codigoErro)
                {
                    case 401:
                        mensagem = "Chave de API inválida. Verifique as configurações de segurança.";
                        break;
                    case 400:
                        mensagem = "Não entendi sua pergunta ou ela viola as diretrizes de segurança da API.";
                        break;
                    case 404:
                        mensagem = "Serviço ou modelo de IA não encontrado. Verifique a URL da API.";
                        break;
                    case 429:
                        mensagem = "Calma aí! Você enviou muitas mensagens. Aguarde um minuto e tente de novo.";
                        break;
                    case 500:
                    case 503:
                        mensagem = "Os servidores da inteligência artificial estão instáveis ou em manutenção.";
                        break;
                    default:
                        mensagem = "Ocorreu um erro de conexão inesperado.";
                        break;
                }

                throw new Exception($"{mensagem} (Erro: {codigoErro})");
            }
            response.EnsureSuccessStatusCode();

            // Lê o conteúdo da resposta como string.
            string responseBody = await response.Content.ReadAsStringAsync();

            // Processa o JSON da resposta para extrair a resposta do modelo.
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                var textResponse = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString()
                    .Trim();

                return textResponse;
            }
        }
    }
}
