namespace Planning_Tool
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_startseite = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tab_xml_input = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.xml_path_input_textbox = new System.Windows.Forms.TextBox();
            this.xml_importieren_button = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tab_startseite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tab_xml_input.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_startseite);
            this.tabControl1.Controls.Add(this.tab_xml_input);
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1171, 605);
            this.tabControl1.TabIndex = 1;
            // 
            // tab_startseite
            // 
            this.tab_startseite.Controls.Add(this.label1);
            this.tab_startseite.Controls.Add(this.pictureBox1);
            this.tab_startseite.Location = new System.Drawing.Point(4, 25);
            this.tab_startseite.Name = "tab_startseite";
            this.tab_startseite.Padding = new System.Windows.Forms.Padding(3);
            this.tab_startseite.Size = new System.Drawing.Size(1163, 576);
            this.tab_startseite.TabIndex = 0;
            this.tab_startseite.Text = "Startseite";
            this.tab_startseite.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Herzlich Willkommen";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(172, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(596, 382);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tab_xml_input
            // 
            this.tab_xml_input.Controls.Add(this.xml_importieren_button);
            this.tab_xml_input.Controls.Add(this.button1);
            this.tab_xml_input.Controls.Add(this.xml_path_input_textbox);
            this.tab_xml_input.Location = new System.Drawing.Point(4, 25);
            this.tab_xml_input.Name = "tab_xml_input";
            this.tab_xml_input.Padding = new System.Windows.Forms.Padding(3);
            this.tab_xml_input.Size = new System.Drawing.Size(1163, 576);
            this.tab_xml_input.TabIndex = 1;
            this.tab_xml_input.Text = "XML Input";
            this.tab_xml_input.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(431, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Auswählen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // xml_path_input_textbox
            // 
            this.xml_path_input_textbox.Location = new System.Drawing.Point(50, 187);
            this.xml_path_input_textbox.Name = "xml_path_input_textbox";
            this.xml_path_input_textbox.ReadOnly = true;
            this.xml_path_input_textbox.Size = new System.Drawing.Size(375, 22);
            this.xml_path_input_textbox.TabIndex = 0;
            this.xml_path_input_textbox.TextChanged += new System.EventHandler(this.xml_path_input_textbox_TextChanged);
            // 
            // xml_importieren_button
            // 
            this.xml_importieren_button.Location = new System.Drawing.Point(50, 226);
            this.xml_importieren_button.Name = "xml_importieren_button";
            this.xml_importieren_button.Size = new System.Drawing.Size(137, 32);
            this.xml_importieren_button.TabIndex = 2;
            this.xml_importieren_button.Text = "XML Importieren";
            this.xml_importieren_button.UseVisualStyleBackColor = true;
            this.xml_importieren_button.Click += new System.EventHandler(this.xml_importieren_button_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 601);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tab_startseite.ResumeLayout(false);
            this.tab_startseite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tab_xml_input.ResumeLayout(false);
            this.tab_xml_input.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_startseite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tab_xml_input;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox xml_path_input_textbox;
        private System.Windows.Forms.Button xml_importieren_button;

    }
}

