using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChatBot
{
    public partial class Form1 : Form
    {
        private readonly GeminiService _geminiService = new GeminiService();

        public Form1()
        {
            InitializeComponent();
            ConfigurarVisualDarkMode();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            this.AcceptButton = btEnviar; // Permite enviar a mensagem pressionando Enter.
            this.ActiveControl = txEntrada; // Campo de entrada fica piscando ao carregar o formulário.
            EscreverNoChat("🎓 TutoriaBot:", "Olá, estudante! Digite sua dúvida sobre programação e eu tentarei ajudar com IA.", Color.FromArgb(46, 204, 113));
        }
        
        private async void btEnviar_Click(object sender, EventArgs e)
        {
            string entradaUsuario = txEntrada.Text.Trim();

            if (string.IsNullOrEmpty(entradaUsuario) || entradaUsuario == "Digite sua dúvida aqui...") return;

            EscreverNoChat("Você:", entradaUsuario, Color.RoyalBlue);

            txEntrada.Clear();
            txEntrada.Enabled = false; // Desabilita o campo de entrada enquanto aguarda a resposta.
            btEnviar.Enabled = false; // Desabilita o botão de enviar para evitar múltiplas requisições.

            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                EscreverNoChat("🎓 TutoriaBot:", "Você parece estar sem conexão com a internet. Verifique seu Wi-Fi ou cabo de rede.", Color.OrangeRed);
                txEntrada.Enabled = true;
                btEnviar.Enabled = true;
                txEntrada.Focus();
                return;
            }

            EscreverNoChat("🎓 TutoriaBot:","pensando...", Color.FromArgb(46, 204, 113));

            string respostaFinalDoBot = "";
            bool erro = false;

            try
            {
                respostaFinalDoBot = await _geminiService.ObterRespostaGemini(entradaUsuario);
            }
            catch (Exception ex)
            {
                respostaFinalDoBot = ex.Message;
                erro = true;
            }
            finally
            {
                //Limpa a mensagem de "pensando..." deletando a última linha.
                int start = txChat.Text.LastIndexOf("🎓 TutoriaBot: pensando...");
                if (start > -1)
                {
                    txChat.ReadOnly = false;
                    txChat.Select(start, "🎓 TutoriaBot: pensando...".Length + 2);
                    txChat.SelectedText = "";
                    txChat.ReadOnly = true;
                }

                //Escreve a resposta do bot, usando verde para respostas normais e vermelho para mensagens de erro.
                if (!erro)
                {
                    EscreverNoChat("🎓 TutoriaBot:", respostaFinalDoBot, Color.FromArgb(46, 204, 113));
                }
                else
                {
                    EscreverNoChat("🎓 TutoriaBot:", respostaFinalDoBot, Color.FromArgb(231, 76, 60));
                }

                //Reabilita o campo de entrada e o botão para a próxima pergunta.
                txEntrada.Enabled = true;
                btEnviar.Enabled = true;
                txEntrada.Focus();

            }
        }

        // Método auxiliar para formatar o texto
        private void EscreverNoChat(string remetente, string mensagem, Color corDoRemetente)
        {
            // Move o cursor invisível para o final para garantir a rolagem
            txChat.SelectionStart = txChat.TextLength;
            txChat.SelectionLength = 0;

            // 1. Configura e escreve o nome
            txChat.SelectionColor = corDoRemetente;
            txChat.SelectionFont = new Font(txChat.Font, FontStyle.Bold);
            txChat.AppendText(remetente + " ");

            // 2. Configura e escreve a mensagem 
            txChat.SelectionColor = Color.White;
            txChat.SelectionFont = new Font(txChat.Font, FontStyle.Regular);
            txChat.AppendText(mensagem + "\n\n");

            // 3. Rola a tela para a última mensagem automaticamente
            txChat.ScrollToCaret();
        }

        // Método para aplicar a estética visual
        private void ConfigurarVisualDarkMode()
        {
            // Configurações fullscreen
            this.WindowState = FormWindowState.Maximized;
            this.AutoScaleMode = AutoScaleMode.None;

            // Configuração da Fonte Geral
            this.Font = new Font("Segoe UI", 26F, FontStyle.Regular, GraphicsUnit.Point);

            // Fundo geral do Formulário
            this.BackColor = Color.FromArgb(30, 30, 30);

            // RichTextBox
            txChat.BackColor = Color.FromArgb(40, 40, 40);
            txChat.ForeColor = Color.White;
            txChat.BorderStyle = BorderStyle.None; // Tira o visual antigo 3D
            txChat.ReadOnly = true; // Crucial para Confiabilidade (usuário não apaga o chat)

            // TextBox 
            txEntrada.BackColor = Color.FromArgb(50, 50, 50);
            txEntrada.ForeColor = Color.White;
            txEntrada.BorderStyle = BorderStyle.FixedSingle;

            // Botão Enviar
            btEnviar.FlatStyle = FlatStyle.Flat;
            btEnviar.BackColor = Color.FromArgb(46, 204, 113); // Verde da imagem
            btEnviar.ForeColor = Color.White;
            btEnviar.Font = new Font(btEnviar.Font, FontStyle.Bold);
            btEnviar.FlatAppearance.BorderSize = 0; // Tira a borda preta
            btEnviar.Cursor = Cursors.Hand; // Mãozinha de clicar

            //Placeholder
            string mensagemPlaceholder = "Digite sua dúvida aqui...";
            txEntrada.Text = mensagemPlaceholder;
            txEntrada.ForeColor = Color.Gray;

            txEntrada.GotFocus += (s, e) =>
            {
                if (txEntrada.Text == mensagemPlaceholder)
                {
                    txEntrada.Text = "";
                    txEntrada.ForeColor = Color.White;
                }
            };
            txEntrada.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txEntrada.Text))
                {
                    txEntrada.Text = mensagemPlaceholder;
                    txEntrada.ForeColor = Color.Gray;
                }

            };

            // Âncoras para redimensionamento responsivo
            txChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;            
            txEntrada.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btEnviar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btEnviar.Height = txEntrada.Height;
            btEnviar.Top = txEntrada.Top;

        }
    }
}
