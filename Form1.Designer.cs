namespace ChatBot
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txChat = new System.Windows.Forms.RichTextBox();
            this.txEntrada = new System.Windows.Forms.TextBox();
            this.btEnviar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txChat
            // 
            this.txChat.Location = new System.Drawing.Point(12, 12);
            this.txChat.Name = "txChat";
            this.txChat.ReadOnly = true;
            this.txChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txChat.Size = new System.Drawing.Size(776, 385);
            this.txChat.TabIndex = 1;
            this.txChat.TabStop = false;
            this.txChat.Text = "";
            // 
            // txEntrada
            // 
            this.txEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.txEntrada.Location = new System.Drawing.Point(12, 403);
            this.txEntrada.Name = "txEntrada";
            this.txEntrada.Size = new System.Drawing.Size(695, 44);
            this.txEntrada.TabIndex = 0;
            // 
            // btEnviar
            // 
            this.btEnviar.Location = new System.Drawing.Point(713, 415);
            this.btEnviar.Name = "btEnviar";
            this.btEnviar.Size = new System.Drawing.Size(75, 23);
            this.btEnviar.TabIndex = 2;
            this.btEnviar.Text = ">";
            this.btEnviar.UseVisualStyleBackColor = true;
            this.btEnviar.Click += new System.EventHandler(this.btEnviar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btEnviar);
            this.Controls.Add(this.txEntrada);
            this.Controls.Add(this.txChat);
            this.Name = "Form1";
            this.Text = "TutoriaBot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txChat;
        private System.Windows.Forms.TextBox txEntrada;
        private System.Windows.Forms.Button btEnviar;
    }
}

