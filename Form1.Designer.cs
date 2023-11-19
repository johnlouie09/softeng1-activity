namespace Louie_s_Prelim_Exam
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
            this.dataGridwithdb = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.studentname = new System.Windows.Forms.TextBox();
            this.studentage = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridwithdb)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridwithdb
            // 
            this.dataGridwithdb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridwithdb.Location = new System.Drawing.Point(164, 25);
            this.dataGridwithdb.Name = "dataGridwithdb";
            this.dataGridwithdb.Size = new System.Drawing.Size(538, 240);
            this.dataGridwithdb.TabIndex = 0;
            this.dataGridwithdb.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridwithdb_CellClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button1.Location = new System.Drawing.Point(186, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add Student";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // studentname
            // 
            this.studentname.Location = new System.Drawing.Point(399, 297);
            this.studentname.Name = "studentname";
            this.studentname.Size = new System.Drawing.Size(145, 20);
            this.studentname.TabIndex = 2;
            this.studentname.TextChanged += new System.EventHandler(this.studentname_TextChanged);
            // 
            // studentage
            // 
            this.studentage.Location = new System.Drawing.Point(399, 323);
            this.studentage.Name = "studentage";
            this.studentage.Size = new System.Drawing.Size(145, 20);
            this.studentage.TabIndex = 3;
            this.studentage.TextChanged += new System.EventHandler(this.studentage_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(399, 349);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Students Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Age:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button2.Location = new System.Drawing.Point(367, 380);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 42);
            this.button2.TabIndex = 8;
            this.button2.Text = "Delete Student";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button3.Location = new System.Drawing.Point(538, 380);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(165, 42);
            this.button3.TabIndex = 9;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.studentage);
            this.Controls.Add(this.studentname);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridwithdb);
            this.Name = "Form1";
            this.Text = "BSCS_Prelim_Exam";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridwithdb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridwithdb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox studentname;
        private System.Windows.Forms.TextBox studentage;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

