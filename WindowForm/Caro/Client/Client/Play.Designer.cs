namespace Client
{
    partial class Play
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
            this.back_Button = new System.Windows.Forms.Button();
            this.menu_Panel = new System.Windows.Forms.Panel();
            this.username_Label2 = new System.Windows.Forms.Label();
            this.username_Label1 = new System.Windows.Forms.Label();
            this.board_Panel = new System.Windows.Forms.Panel();
            this.status_Label = new System.Windows.Forms.Label();
            this.wait_Label = new System.Windows.Forms.Label();
            this.menu_Panel.SuspendLayout();
            this.board_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // back_Button
            // 
            this.back_Button.Location = new System.Drawing.Point(668, 2);
            this.back_Button.Name = "back_Button";
            this.back_Button.Size = new System.Drawing.Size(75, 24);
            this.back_Button.TabIndex = 0;
            this.back_Button.Text = "Back";
            this.back_Button.UseVisualStyleBackColor = true;
            this.back_Button.Click += new System.EventHandler(this.back_Button_Click);
            // 
            // menu_Panel
            // 
            this.menu_Panel.Controls.Add(this.username_Label2);
            this.menu_Panel.Controls.Add(this.username_Label1);
            this.menu_Panel.Controls.Add(this.back_Button);
            this.menu_Panel.Location = new System.Drawing.Point(0, 442);
            this.menu_Panel.Name = "menu_Panel";
            this.menu_Panel.Size = new System.Drawing.Size(745, 29);
            this.menu_Panel.TabIndex = 1;
            // 
            // username_Label2
            // 
            this.username_Label2.AutoSize = true;
            this.username_Label2.Location = new System.Drawing.Point(73, 8);
            this.username_Label2.Name = "username_Label2";
            this.username_Label2.Size = new System.Drawing.Size(13, 13);
            this.username_Label2.TabIndex = 2;
            this.username_Label2.Text = "--";
            // 
            // username_Label1
            // 
            this.username_Label1.AutoSize = true;
            this.username_Label1.Location = new System.Drawing.Point(3, 8);
            this.username_Label1.Name = "username_Label1";
            this.username_Label1.Size = new System.Drawing.Size(64, 13);
            this.username_Label1.TabIndex = 1;
            this.username_Label1.Text = "Username : ";
            // 
            // board_Panel
            // 
            this.board_Panel.Controls.Add(this.status_Label);
            this.board_Panel.Controls.Add(this.wait_Label);
            this.board_Panel.Location = new System.Drawing.Point(3, 3);
            this.board_Panel.Name = "board_Panel";
            this.board_Panel.Size = new System.Drawing.Size(742, 435);
            this.board_Panel.TabIndex = 2;
            this.board_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.board_Panel_Paint);
            this.board_Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.board_Panel_MouseDown);
            // 
            // status_Label
            // 
            this.status_Label.AutoSize = true;
            this.status_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_Label.ForeColor = System.Drawing.Color.Olive;
            this.status_Label.Location = new System.Drawing.Point(209, 156);
            this.status_Label.Name = "status_Label";
            this.status_Label.Size = new System.Drawing.Size(313, 44);
            this.status_Label.TabIndex = 2;
            this.status_Label.Text = "BẠN ĐÃ THẮNG";
            this.status_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.status_Label.Visible = false;
            // 
            // wait_Label
            // 
            this.wait_Label.AutoSize = true;
            this.wait_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wait_Label.ForeColor = System.Drawing.Color.Blue;
            this.wait_Label.Location = new System.Drawing.Point(99, 200);
            this.wait_Label.Name = "wait_Label";
            this.wait_Label.Size = new System.Drawing.Size(586, 39);
            this.wait_Label.TabIndex = 1;
            this.wait_Label.Text = "ĐANG CHỜ NGƯỜI CHƠI KHÁC ...";
            this.wait_Label.Visible = false;
            // 
            // Play
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.board_Panel);
            this.Controls.Add(this.menu_Panel);
            this.Name = "Play";
            this.Size = new System.Drawing.Size(748, 471);
            this.menu_Panel.ResumeLayout(false);
            this.menu_Panel.PerformLayout();
            this.board_Panel.ResumeLayout(false);
            this.board_Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button back_Button;
        private System.Windows.Forms.Panel menu_Panel;
        private System.Windows.Forms.Panel board_Panel;
        private System.Windows.Forms.Label username_Label1;
        private System.Windows.Forms.Label username_Label2;
        private System.Windows.Forms.Label wait_Label;
        private System.Windows.Forms.Label status_Label;

    }
}
