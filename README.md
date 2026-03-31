# 🎓 TutoriaBot - Assistente Educacional com IA (Gemini)

Um aplicativo desktop desenvolvido em **C# (Windows Forms)** que atua como um tutor de programação. Integrado com a API do **Google Gemini (modelo 2.5 Flash)**, o bot é configurado por meio de um *System Prompt* rigoroso para responder exclusivamente a dúvidas sobre desenvolvimento de software de forma didática e direta.

## 🚀 Funcionalidades

* **Chat Inteligente:** Respostas rápidas e precisas usando o modelo Gemini 2.5 Flash.
* **Foco Educacional:** A IA é restrita a tópicos de programação. Se questionada sobre outros assuntos, ela recusa educadamente.
* **Interface Dark Mode:** UI responsiva, moderna e confortável para os olhos, com rolagem automática e feedback visual de "pensando...".
* **Tratamento de Erros:** Identificação e mensagens claras para erros de conexão, limite de requisições (Status 429), chaves inválidas ou falta de internet.
* **Segurança Criptográfica:** Uso do protocolo TLS 1.2 garantido via código.
* **Boas Práticas:** Gerenciamento eficiente de requisições HTTP (`static readonly HttpClient`) e leitura nativa de JSON (`System.Text.Json`).

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C# (.NET)
* **Interface:** Windows Forms
* **Integração:** REST API (Google Generative Language API)
* **Serialização:** `System.Text.Json`

## ⚙️ Como executar o projeto na sua máquina

Para testar este projeto localmente, você precisará configurar a sua própria chave de API do Google Gemini.

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/SeuUsuario/SeuRepositorio.git](https://github.com/SeuUsuario/SeuRepositorio.git)
   ```
   
2. **Obtenha a sua API Key:**
   Crie uma chave gratuita no [Google AI Studio](https://aistudio.google.com/).

3. **Configure a Variável de Ambiente:**
   Para manter a segurança, o projeto não possui a chave escrita no código. No Windows, abra o Prompt de Comando e rode:
   
   ```cmd
   setx GEMINI_API_KEY "cole-sua-chave-aqui"
   ```
   (Atenção: reinicie o Visual Studio após rodar este comando para que ele reconheça a nova variável).

4. **Execute o projeto:**
Abra o arquivo ```.sln``` ou ```.csproj``` no Visual Studio e inicie a aplicação.
   
