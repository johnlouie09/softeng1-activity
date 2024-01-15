namespace Louie_s_Prelim_Exam
{
    partial class Attendance2
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboCam = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.scannerBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.scannerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button1.Location = new System.Drawing.Point(207, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 74);
            this.button1.TabIndex = 0;
            this.button1.Text = "Register";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera:";
            // 
            // comboCam
            // 
            this.comboCam.FormattingEnabled = true;
            this.comboCam.Location = new System.Drawing.Point(225, 19);
            this.comboCam.Name = "comboCam";
            this.comboCam.Size = new System.Drawing.Size(142, 21);
            this.comboCam.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(427, 50);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 227);
            this.textBox1.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnStart.Location = new System.Drawing.Point(449, 294);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(103, 51);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button3.Location = new System.Drawing.Point(579, 294);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 51);
            this.button3.TabIndex = 5;
            this.button3.Text = "Decode";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // scannerBox
            // 
            this.scannerBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scannerBox.Location = new System.Drawing.Point(91, 72);
            this.scannerBox.Name = "scannerBox";
            this.scannerBox.Size = new System.Drawing.Size(255, 205);
            this.scannerBox.TabIndex = 6;
            this.scannerBox.TabStop = false;
            // 
            // Attendance2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scannerBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboCam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Attendance2";
            this.Text = "Attendance2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Attendance2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scannerBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboCam;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox scannerBox;
    }
}