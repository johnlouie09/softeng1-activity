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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboCam = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.scannerBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.attendanceDataGrid = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scannerBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button1.Location = new System.Drawing.Point(566, 294);
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
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnStart.Location = new System.Drawing.Point(427, 294);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(103, 51);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // scannerBox
            // 
            this.scannerBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scannerBox.Location = new System.Drawing.Point(51, 46);
            this.scannerBox.Name = "scannerBox";
            this.scannerBox.Size = new System.Drawing.Size(336, 258);
            this.scannerBox.TabIndex = 6;
            this.scannerBox.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // attendanceDataGrid
            // 
            this.attendanceDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.attendanceDataGrid.Location = new System.Drawing.Point(393, 54);
            this.attendanceDataGrid.Name = "attendanceDataGrid";
            this.attendanceDataGrid.Size = new System.Drawing.Size(345, 223);
            this.attendanceDataGrid.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 8;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Attendance2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.attendanceDataGrid);
            this.Controls.Add(this.scannerBox);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.comboCam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Attendance2";
            this.Text = "Attendance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Attendance2_FormClosing);
            this.Load += new System.EventHandler(this.Attendance2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scannerBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboCam;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox scannerBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView attendanceDataGrid;
        private System.Windows.Forms.Label label2;
    }
}