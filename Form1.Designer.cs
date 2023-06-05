namespace Bingo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonProgress = new System.Windows.Forms.Button();
            this.buttonBoard = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonGetBingos = new System.Windows.Forms.Button();
            this.buttonLogbingo = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonProgress
            // 
            this.buttonProgress.Location = new System.Drawing.Point(3, 3);
            this.buttonProgress.Name = "buttonProgress";
            this.buttonProgress.Size = new System.Drawing.Size(155, 23);
            this.buttonProgress.TabIndex = 0;
            this.buttonProgress.Text = "Clear Progress";
            this.buttonProgress.UseVisualStyleBackColor = true;
            // 
            // buttonBoard
            // 
            this.buttonBoard.Location = new System.Drawing.Point(3, 32);
            this.buttonBoard.Name = "buttonBoard";
            this.buttonBoard.Size = new System.Drawing.Size(155, 23);
            this.buttonBoard.TabIndex = 1;
            this.buttonBoard.Text = "New Board";
            this.buttonBoard.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonGetBingos);
            this.panel1.Controls.Add(this.buttonLogbingo);
            this.panel1.Controls.Add(this.buttonProgress);
            this.panel1.Controls.Add(this.buttonBoard);
            this.panel1.Location = new System.Drawing.Point(627, 300);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 116);
            this.panel1.TabIndex = 2;
            // 
            // buttonGetBingos
            // 
            this.buttonGetBingos.Location = new System.Drawing.Point(3, 90);
            this.buttonGetBingos.Name = "buttonGetBingos";
            this.buttonGetBingos.Size = new System.Drawing.Size(154, 23);
            this.buttonGetBingos.TabIndex = 3;
            this.buttonGetBingos.Text = "Get Bingo Log";
            this.buttonGetBingos.UseVisualStyleBackColor = true;
            // 
            // buttonLogbingo
            // 
            this.buttonLogbingo.Location = new System.Drawing.Point(3, 61);
            this.buttonLogbingo.Name = "buttonLogbingo";
            this.buttonLogbingo.Size = new System.Drawing.Size(154, 23);
            this.buttonLogbingo.TabIndex = 2;
            this.buttonLogbingo.Text = "Log Bingo";
            this.buttonLogbingo.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 445);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Bingo";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button buttonProgress;
        private System.Windows.Forms.Button buttonBoard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonLogbingo;
        private System.Windows.Forms.Button buttonGetBingos;
    }
}

