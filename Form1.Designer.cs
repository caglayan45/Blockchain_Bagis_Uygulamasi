
namespace Blockchain_Bagis_Uygulamasi
{
    partial class Form1
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
            this.btnAgKur = new System.Windows.Forms.Button();
            this.btnTransactionEkle = new System.Windows.Forms.Button();
            this.btnZincir = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnToplananBagis = new System.Windows.Forms.Button();
            this.btnBagisSorgulama = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAgKur
            // 
            this.btnAgKur.BackColor = System.Drawing.Color.PeachPuff;
            this.btnAgKur.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAgKur.Location = new System.Drawing.Point(12, 12);
            this.btnAgKur.Name = "btnAgKur";
            this.btnAgKur.Size = new System.Drawing.Size(181, 72);
            this.btnAgKur.TabIndex = 0;
            this.btnAgKur.Text = "Join Blockchain Network";
            this.btnAgKur.UseVisualStyleBackColor = false;
            this.btnAgKur.Click += new System.EventHandler(this.btnAgKur_Click);
            // 
            // btnTransactionEkle
            // 
            this.btnTransactionEkle.BackColor = System.Drawing.Color.PeachPuff;
            this.btnTransactionEkle.Enabled = false;
            this.btnTransactionEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTransactionEkle.Location = new System.Drawing.Point(12, 90);
            this.btnTransactionEkle.Name = "btnTransactionEkle";
            this.btnTransactionEkle.Size = new System.Drawing.Size(181, 72);
            this.btnTransactionEkle.TabIndex = 2;
            this.btnTransactionEkle.Text = "Donate";
            this.btnTransactionEkle.UseVisualStyleBackColor = false;
            this.btnTransactionEkle.Click += new System.EventHandler(this.btnTransactionEkle_Click);
            // 
            // btnZincir
            // 
            this.btnZincir.BackColor = System.Drawing.Color.PeachPuff;
            this.btnZincir.Enabled = false;
            this.btnZincir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnZincir.Location = new System.Drawing.Point(12, 168);
            this.btnZincir.Name = "btnZincir";
            this.btnZincir.Size = new System.Drawing.Size(181, 72);
            this.btnZincir.TabIndex = 3;
            this.btnZincir.Text = "Show Chain";
            this.btnZincir.UseVisualStyleBackColor = false;
            this.btnZincir.Click += new System.EventHandler(this.btnZincir_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(199, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(686, 548);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // btnToplananBagis
            // 
            this.btnToplananBagis.BackColor = System.Drawing.Color.PeachPuff;
            this.btnToplananBagis.Enabled = false;
            this.btnToplananBagis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnToplananBagis.Location = new System.Drawing.Point(12, 246);
            this.btnToplananBagis.Name = "btnToplananBagis";
            this.btnToplananBagis.Size = new System.Drawing.Size(181, 72);
            this.btnToplananBagis.TabIndex = 6;
            this.btnToplananBagis.Text = "Donations Collected";
            this.btnToplananBagis.UseVisualStyleBackColor = false;
            this.btnToplananBagis.Click += new System.EventHandler(this.btnToplananBagis_Click);
            // 
            // btnBagisSorgulama
            // 
            this.btnBagisSorgulama.BackColor = System.Drawing.Color.PeachPuff;
            this.btnBagisSorgulama.Enabled = false;
            this.btnBagisSorgulama.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnBagisSorgulama.Location = new System.Drawing.Point(12, 324);
            this.btnBagisSorgulama.Name = "btnBagisSorgulama";
            this.btnBagisSorgulama.Size = new System.Drawing.Size(181, 72);
            this.btnBagisSorgulama.TabIndex = 8;
            this.btnBagisSorgulama.Text = "Donation Inquiry";
            this.btnBagisSorgulama.UseVisualStyleBackColor = false;
            this.btnBagisSorgulama.Click += new System.EventHandler(this.btnBagisSorgulama_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(896, 572);
            this.Controls.Add(this.btnBagisSorgulama);
            this.Controls.Add(this.btnToplananBagis);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnZincir);
            this.Controls.Add(this.btnTransactionEkle);
            this.Controls.Add(this.btnAgKur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Blockchain ile Bağış Uygulaması";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAgKur;
        private System.Windows.Forms.Button btnTransactionEkle;
        private System.Windows.Forms.Button btnZincir;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnToplananBagis;
        private System.Windows.Forms.Button btnBagisSorgulama;
    }
}

