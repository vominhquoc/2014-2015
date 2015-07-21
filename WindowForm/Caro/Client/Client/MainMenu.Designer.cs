namespace Client
{
    partial class MainMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.exit_Button = new System.Windows.Forms.Button();
            this.readme_Button = new System.Windows.Forms.Button();
            this.startgame_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exit_Button
            // 
            this.exit_Button.BackColor = System.Drawing.Color.LawnGreen;
            this.exit_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_Button.ForeColor = System.Drawing.Color.Maroon;
            this.exit_Button.Location = new System.Drawing.Point(250, 356);
            this.exit_Button.Name = "exit_Button";
            this.exit_Button.Size = new System.Drawing.Size(257, 49);
            this.exit_Button.TabIndex = 5;
            this.exit_Button.Text = "Thoát";
            this.exit_Button.UseVisualStyleBackColor = false;
            this.exit_Button.Click += new System.EventHandler(this.exit_Button_Click);
            // 
            // readme_Button
            // 
            this.readme_Button.BackColor = System.Drawing.Color.LawnGreen;
            this.readme_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readme_Button.ForeColor = System.Drawing.Color.Maroon;
            this.readme_Button.Location = new System.Drawing.Point(250, 291);
            this.readme_Button.Name = "readme_Button";
            this.readme_Button.Size = new System.Drawing.Size(257, 59);
            this.readme_Button.TabIndex = 4;
            this.readme_Button.Text = "Hướng Dẫn Chơi";
            this.readme_Button.UseVisualStyleBackColor = false;
            this.readme_Button.Click += new System.EventHandler(this.readme_Button_Click);
            // 
            // startgame_Button
            // 
            this.startgame_Button.BackColor = System.Drawing.Color.LawnGreen;
            this.startgame_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startgame_Button.ForeColor = System.Drawing.Color.Maroon;
            this.startgame_Button.Location = new System.Drawing.Point(250, 215);
            this.startgame_Button.Name = "startgame_Button";
            this.startgame_Button.Size = new System.Drawing.Size(257, 70);
            this.startgame_Button.TabIndex = 3;
            this.startgame_Button.Text = "Bắt Đầu Trò Chơi";
            this.startgame_Button.UseVisualStyleBackColor = false;
            this.startgame_Button.Click += new System.EventHandler(this.startgame_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(145, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(461, 91);
            this.label1.TabIndex = 6;
            this.label1.Text = "Carô Online";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGreen;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exit_Button);
            this.Controls.Add(this.readme_Button);
            this.Controls.Add(this.startgame_Button);
            this.Name = "MainMenu";
            this.Size = new System.Drawing.Size(748, 471);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exit_Button;
        private System.Windows.Forms.Button readme_Button;
        private System.Windows.Forms.Button startgame_Button;
        private System.Windows.Forms.Label label1;
    }
}
