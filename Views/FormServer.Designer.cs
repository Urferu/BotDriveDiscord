namespace DriveBot.Views
{
    partial class FormServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtTocken = new System.Windows.Forms.TextBox();
            this.lblTocken = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnAgregarJuego = new System.Windows.Forms.Button();
            this.bwBot = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // txtTocken
            // 
            this.txtTocken.Location = new System.Drawing.Point(24, 39);
            this.txtTocken.Name = "txtTocken";
            this.txtTocken.Size = new System.Drawing.Size(366, 20);
            this.txtTocken.TabIndex = 0;
            this.txtTocken.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblTocken
            // 
            this.lblTocken.AutoSize = true;
            this.lblTocken.Location = new System.Drawing.Point(21, 23);
            this.lblTocken.Name = "lblTocken";
            this.lblTocken.Size = new System.Drawing.Size(66, 13);
            this.lblTocken.TabIndex = 1;
            this.lblTocken.Text = "Tocken Bot:";
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(24, 65);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 2;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAgregarJuego
            // 
            this.btnAgregarJuego.Location = new System.Drawing.Point(299, 65);
            this.btnAgregarJuego.Name = "btnAgregarJuego";
            this.btnAgregarJuego.Size = new System.Drawing.Size(91, 50);
            this.btnAgregarJuego.TabIndex = 3;
            this.btnAgregarJuego.Text = "Agregar/Editar Juego";
            this.btnAgregarJuego.UseVisualStyleBackColor = true;
            // 
            // bwBot
            // 
            this.bwBot.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwBot_DoWork);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 127);
            this.ControlBox = false;
            this.Controls.Add(this.btnAgregarJuego);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.lblTocken);
            this.Controls.Add(this.txtTocken);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Propiedades del servidor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTocken;
        private System.Windows.Forms.Label lblTocken;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnAgregarJuego;
        private System.ComponentModel.BackgroundWorker bwBot;
    }
}