namespace RemoveInvalidChars.WinForms
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseDest = new System.Windows.Forms.Button();
            this.txtDestFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCustomChars = new System.Windows.Forms.TextBox();
            this.chkCustomChars = new System.Windows.Forms.CheckBox();
            this.chkExtendedASCII = new System.Windows.Forms.CheckBox();
            this.chkNonPrintable = new System.Windows.Forms.CheckBox();
            this.chkControlChars = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.btnBrowseDest);
            this.groupBox1.Controls.Add(this.txtDestFile);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBrowseSource);
            this.groupBox1.Controls.Add(this.txtSourceFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arquivos";
            //
            // btnBrowseDest
            //
            this.btnBrowseDest.Location = new System.Drawing.Point(710, 65);
            this.btnBrowseDest.Name = "btnBrowseDest";
            this.btnBrowseDest.Size = new System.Drawing.Size(35, 23);
            this.btnBrowseDest.TabIndex = 5;
            this.btnBrowseDest.Text = "...";
            this.btnBrowseDest.UseVisualStyleBackColor = true;
            this.btnBrowseDest.Click += new System.EventHandler(this.btnBrowseDest_Click);
            //
            // txtDestFile
            //
            this.txtDestFile.Location = new System.Drawing.Point(120, 65);
            this.txtDestFile.Name = "txtDestFile";
            this.txtDestFile.Size = new System.Drawing.Size(580, 20);
            this.txtDestFile.TabIndex = 4;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Arquivo Destino:";
            //
            // btnBrowseSource
            //
            this.btnBrowseSource.Location = new System.Drawing.Point(710, 30);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(35, 23);
            this.btnBrowseSource.TabIndex = 2;
            this.btnBrowseSource.Text = "...";
            this.btnBrowseSource.UseVisualStyleBackColor = true;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            //
            // txtSourceFile
            //
            this.txtSourceFile.Location = new System.Drawing.Point(120, 30);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(580, 20);
            this.txtSourceFile.TabIndex = 1;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arquivo Origem:";
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.txtCustomChars);
            this.groupBox2.Controls.Add(this.chkCustomChars);
            this.groupBox2.Controls.Add(this.chkExtendedASCII);
            this.groupBox2.Controls.Add(this.chkNonPrintable);
            this.groupBox2.Controls.Add(this.chkControlChars);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opções de Remoção";
            //
            // txtCustomChars
            //
            this.txtCustomChars.Enabled = false;
            this.txtCustomChars.Location = new System.Drawing.Point(200, 65);
            this.txtCustomChars.Name = "txtCustomChars";
            this.txtCustomChars.Size = new System.Drawing.Size(545, 20);
            this.txtCustomChars.TabIndex = 4;
            //
            // chkCustomChars
            //
            this.chkCustomChars.AutoSize = true;
            this.chkCustomChars.Location = new System.Drawing.Point(18, 67);
            this.chkCustomChars.Name = "chkCustomChars";
            this.chkCustomChars.Size = new System.Drawing.Size(176, 17);
            this.chkCustomChars.TabIndex = 3;
            this.chkCustomChars.Text = "Caracteres Customizados:";
            this.chkCustomChars.UseVisualStyleBackColor = true;
            this.chkCustomChars.CheckedChanged += new System.EventHandler(this.chkCustomChars_CheckedChanged);
            //
            // chkExtendedASCII
            //
            this.chkExtendedASCII.AutoSize = true;
            this.chkExtendedASCII.Location = new System.Drawing.Point(18, 44);
            this.chkExtendedASCII.Name = "chkExtendedASCII";
            this.chkExtendedASCII.Size = new System.Drawing.Size(228, 17);
            this.chkExtendedASCII.TabIndex = 2;
            this.chkExtendedASCII.Text = "Remover ASCII Estendido (128-255)";
            this.chkExtendedASCII.UseVisualStyleBackColor = true;
            //
            // chkNonPrintable
            //
            this.chkNonPrintable.AutoSize = true;
            this.chkNonPrintable.Location = new System.Drawing.Point(380, 21);
            this.chkNonPrintable.Name = "chkNonPrintable";
            this.chkNonPrintable.Size = new System.Drawing.Size(190, 17);
            this.chkNonPrintable.TabIndex = 1;
            this.chkNonPrintable.Text = "Remover Não-Imprimíveis";
            this.chkNonPrintable.UseVisualStyleBackColor = true;
            //
            // chkControlChars
            //
            this.chkControlChars.AutoSize = true;
            this.chkControlChars.Location = new System.Drawing.Point(18, 21);
            this.chkControlChars.Name = "chkControlChars";
            this.chkControlChars.Size = new System.Drawing.Size(261, 17);
            this.chkControlChars.TabIndex = 0;
            this.chkControlChars.Text = "Remover Caracteres de Controle (0-31)";
            this.chkControlChars.UseVisualStyleBackColor = true;
            //
            // txtLog
            //
            this.txtLog.Location = new System.Drawing.Point(12, 244);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(760, 150);
            this.txtLog.TabIndex = 2;
            //
            // btnProcess
            //
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(520, 410);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(150, 35);
            this.btnProcess.TabIndex = 3;
            this.btnProcess.Text = "Processar Arquivo";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            //
            // btnClose
            //
            this.btnClose.Location = new System.Drawing.Point(680, 410);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Log:";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remover Caracteres Inválidos de Arquivos TXT";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowseDest;
        private System.Windows.Forms.TextBox txtDestFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCustomChars;
        private System.Windows.Forms.CheckBox chkCustomChars;
        private System.Windows.Forms.CheckBox chkExtendedASCII;
        private System.Windows.Forms.CheckBox chkNonPrintable;
        private System.Windows.Forms.CheckBox chkControlChars;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
    }
}
