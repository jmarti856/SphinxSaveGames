namespace ReadSaveGames
{
    partial class frm_Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_ubicacion = new System.Windows.Forms.Label();
            this.txtb_rutaPartida = new System.Windows.Forms.TextBox();
            this.btn_cargarArchivo = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtbx_partidaBinario = new System.Windows.Forms.RichTextBox();
            this.rtbx_PartidaTexto = new System.Windows.Forms.RichTextBox();
            this.lbl_guardar = new System.Windows.Forms.Label();
            this.txbx_guardar = new System.Windows.Forms.TextBox();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.btn_convertir = new System.Windows.Forms.Button();
            this.ofd_partida = new System.Windows.Forms.OpenFileDialog();
            this.chbx_EuroLand = new System.Windows.Forms.CheckBox();
            this.sfd_save = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_ubicacion
            // 
            this.lbl_ubicacion.AutoSize = true;
            this.lbl_ubicacion.Location = new System.Drawing.Point(28, 15);
            this.lbl_ubicacion.Name = "lbl_ubicacion";
            this.lbl_ubicacion.Size = new System.Drawing.Size(46, 13);
            this.lbl_ubicacion.TabIndex = 0;
            this.lbl_ubicacion.Text = "Archivo:";
            // 
            // txtb_rutaPartida
            // 
            this.txtb_rutaPartida.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_rutaPartida.Location = new System.Drawing.Point(80, 12);
            this.txtb_rutaPartida.Name = "txtb_rutaPartida";
            this.txtb_rutaPartida.ReadOnly = true;
            this.txtb_rutaPartida.Size = new System.Drawing.Size(556, 20);
            this.txtb_rutaPartida.TabIndex = 1;
            // 
            // btn_cargarArchivo
            // 
            this.btn_cargarArchivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cargarArchivo.Location = new System.Drawing.Point(642, 10);
            this.btn_cargarArchivo.Name = "btn_cargarArchivo";
            this.btn_cargarArchivo.Size = new System.Drawing.Size(75, 23);
            this.btn_cargarArchivo.TabIndex = 2;
            this.btn_cargarArchivo.Text = "Abrir";
            this.btn_cargarArchivo.UseVisualStyleBackColor = true;
            this.btn_cargarArchivo.Click += new System.EventHandler(this.btn_cargarArchivo_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(31, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtbx_partidaBinario);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbx_PartidaTexto);
            this.splitContainer1.Size = new System.Drawing.Size(767, 478);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 3;
            // 
            // rtbx_partidaBinario
            // 
            this.rtbx_partidaBinario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx_partidaBinario.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbx_partidaBinario.Location = new System.Drawing.Point(0, 0);
            this.rtbx_partidaBinario.Name = "rtbx_partidaBinario";
            this.rtbx_partidaBinario.ReadOnly = true;
            this.rtbx_partidaBinario.Size = new System.Drawing.Size(320, 478);
            this.rtbx_partidaBinario.TabIndex = 0;
            this.rtbx_partidaBinario.Text = "";
            // 
            // rtbx_PartidaTexto
            // 
            this.rtbx_PartidaTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx_PartidaTexto.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbx_PartidaTexto.Location = new System.Drawing.Point(0, 0);
            this.rtbx_PartidaTexto.Name = "rtbx_PartidaTexto";
            this.rtbx_PartidaTexto.Size = new System.Drawing.Size(443, 478);
            this.rtbx_PartidaTexto.TabIndex = 0;
            this.rtbx_PartidaTexto.Text = "";
            // 
            // lbl_guardar
            // 
            this.lbl_guardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_guardar.AutoSize = true;
            this.lbl_guardar.Location = new System.Drawing.Point(28, 551);
            this.lbl_guardar.Name = "lbl_guardar";
            this.lbl_guardar.Size = new System.Drawing.Size(33, 13);
            this.lbl_guardar.TabIndex = 4;
            this.lbl_guardar.Text = "Ruta:";
            // 
            // txbx_guardar
            // 
            this.txbx_guardar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbx_guardar.Location = new System.Drawing.Point(67, 548);
            this.txbx_guardar.Name = "txbx_guardar";
            this.txbx_guardar.ReadOnly = true;
            this.txbx_guardar.Size = new System.Drawing.Size(650, 20);
            this.txbx_guardar.TabIndex = 5;
            // 
            // btn_guardar
            // 
            this.btn_guardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_guardar.Location = new System.Drawing.Point(723, 546);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 6;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // btn_convertir
            // 
            this.btn_convertir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_convertir.Location = new System.Drawing.Point(723, 10);
            this.btn_convertir.Name = "btn_convertir";
            this.btn_convertir.Size = new System.Drawing.Size(75, 23);
            this.btn_convertir.TabIndex = 7;
            this.btn_convertir.Text = "Convertir";
            this.btn_convertir.UseVisualStyleBackColor = true;
            this.btn_convertir.Click += new System.EventHandler(this.btn_convertir_Click);
            // 
            // ofd_partida
            // 
            this.ofd_partida.FileName = "Buscar archivo";
            // 
            // chbx_EuroLand
            // 
            this.chbx_EuroLand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chbx_EuroLand.AutoSize = true;
            this.chbx_EuroLand.Location = new System.Drawing.Point(685, 523);
            this.chbx_EuroLand.Name = "chbx_EuroLand";
            this.chbx_EuroLand.Size = new System.Drawing.Size(113, 17);
            this.chbx_EuroLand.TabIndex = 8;
            this.chbx_EuroLand.Text = "Formato EuroLand";
            this.chbx_EuroLand.UseVisualStyleBackColor = true;
            // 
            // sfd_save
            // 
            this.sfd_save.FileName = "hashcodes.txt";
            this.sfd_save.Filter = "txt files (*.txt)|*.txt";
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 581);
            this.Controls.Add(this.chbx_EuroLand);
            this.Controls.Add(this.btn_convertir);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.txbx_guardar);
            this.Controls.Add(this.lbl_guardar);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btn_cargarArchivo);
            this.Controls.Add(this.txtb_rutaPartida);
            this.Controls.Add(this.lbl_ubicacion);
            this.Name = "frm_Main";
            this.Text = "Sphinx y la Maldita Momia";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_ubicacion;
        private System.Windows.Forms.TextBox txtb_rutaPartida;
        private System.Windows.Forms.Button btn_cargarArchivo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtbx_partidaBinario;
        private System.Windows.Forms.RichTextBox rtbx_PartidaTexto;
        private System.Windows.Forms.Label lbl_guardar;
        private System.Windows.Forms.TextBox txbx_guardar;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.Button btn_convertir;
        private System.Windows.Forms.OpenFileDialog ofd_partida;
        private System.Windows.Forms.CheckBox chbx_EuroLand;
        private System.Windows.Forms.SaveFileDialog sfd_save;
    }
}

