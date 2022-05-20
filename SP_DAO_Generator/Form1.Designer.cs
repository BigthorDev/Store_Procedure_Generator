namespace SP_DAO_Generator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBasePath = new System.Windows.Forms.TextBox();
            this.listTables = new System.Windows.Forms.ListBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkModels = new System.Windows.Forms.CheckBox();
            this.chkSPs = new System.Windows.Forms.CheckBox();
            this.chkDAOs = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output File Path";
            // 
            // txtBasePath
            // 
            this.txtBasePath.Location = new System.Drawing.Point(12, 228);
            this.txtBasePath.Name = "txtBasePath";
            this.txtBasePath.Size = new System.Drawing.Size(386, 23);
            this.txtBasePath.TabIndex = 2;
            // 
            // listTables
            // 
            this.listTables.FormattingEnabled = true;
            this.listTables.ItemHeight = 15;
            this.listTables.Location = new System.Drawing.Point(12, 55);
            this.listTables.Name = "listTables";
            this.listTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listTables.Size = new System.Drawing.Size(386, 94);
            this.listTables.TabIndex = 3;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 362);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(386, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "PROCESS";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(12, 180);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(386, 23);
            this.txtNamespace.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Namespace (for models and DAOs)";
            // 
            // chkModels
            // 
            this.chkModels.AutoSize = true;
            this.chkModels.Location = new System.Drawing.Point(12, 275);
            this.chkModels.Name = "chkModels";
            this.chkModels.Size = new System.Drawing.Size(115, 19);
            this.chkModels.TabIndex = 7;
            this.chkModels.Text = "Generate Models";
            this.chkModels.UseVisualStyleBackColor = true;
            // 
            // chkSPs
            // 
            this.chkSPs.AutoSize = true;
            this.chkSPs.Checked = true;
            this.chkSPs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSPs.Location = new System.Drawing.Point(158, 275);
            this.chkSPs.Name = "chkSPs";
            this.chkSPs.Size = new System.Drawing.Size(94, 19);
            this.chkSPs.TabIndex = 8;
            this.chkSPs.Text = "Generate SPs";
            this.chkSPs.UseVisualStyleBackColor = true;
            // 
            // chkDAOs
            // 
            this.chkDAOs.AutoSize = true;
            this.chkDAOs.Location = new System.Drawing.Point(292, 275);
            this.chkDAOs.Name = "chkDAOs";
            this.chkDAOs.Size = new System.Drawing.Size(106, 19);
            this.chkDAOs.TabIndex = 9;
            this.chkDAOs.Text = "Generate DAOs";
            this.chkDAOs.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 447);
            this.Controls.Add(this.chkDAOs);
            this.Controls.Add(this.chkSPs);
            this.Controls.Add(this.chkModels);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.listTables);
            this.Controls.Add(this.txtBasePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtBasePath;
        private ListBox listTables;
        private Button btnGenerate;
        private TextBox txtNamespace;
        private Label label3;
        private CheckBox chkModels;
        private CheckBox chkSPs;
        private CheckBox chkDAOs;
    }
}