namespace Do_Hoa_May_Tinh
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNBezier = new System.Windows.Forms.Button();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnPointer = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 253);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnNBezier);
            this.panel2.Controls.Add(this.btnEllipse);
            this.panel2.Controls.Add(this.btnCircle);
            this.panel2.Controls.Add(this.btnLine);
            this.panel2.Controls.Add(this.btnPointer);
            this.panel2.Location = new System.Drawing.Point(177, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 43);
            this.panel2.TabIndex = 1;
            // 
            // btnNBezier
            // 
            this.btnNBezier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNBezier.Image = ((System.Drawing.Image)(resources.GetObject("btnNBezier.Image")));
            this.btnNBezier.Location = new System.Drawing.Point(274, 0);
            this.btnNBezier.Name = "btnNBezier";
            this.btnNBezier.Size = new System.Drawing.Size(67, 43);
            this.btnNBezier.TabIndex = 2;
            this.btnNBezier.Tag = "Bezier";
            this.btnNBezier.UseVisualStyleBackColor = true;
            // 
            // btnEllipse
            // 
            this.btnEllipse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEllipse.Image = ((System.Drawing.Image)(resources.GetObject("btnEllipse.Image")));
            this.btnEllipse.Location = new System.Drawing.Point(204, 0);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(69, 43);
            this.btnEllipse.TabIndex = 1;
            this.btnEllipse.Tag = "Ellipse";
            this.btnEllipse.UseVisualStyleBackColor = true;
            // 
            // btnCircle
            // 
            this.btnCircle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.Location = new System.Drawing.Point(137, 0);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(66, 43);
            this.btnCircle.TabIndex = 2;
            this.btnCircle.Tag = "Circle";
            this.btnCircle.UseVisualStyleBackColor = true;
            // 
            // btnLine
            // 
            this.btnLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
            this.btnLine.Location = new System.Drawing.Point(70, 0);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(66, 43);
            this.btnLine.TabIndex = 1;
            this.btnLine.Tag = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            // 
            // btnPointer
            // 
            this.btnPointer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPointer.Image = ((System.Drawing.Image)(resources.GetObject("btnPointer.Image")));
            this.btnPointer.Location = new System.Drawing.Point(3, 0);
            this.btnPointer.Name = "btnPointer";
            this.btnPointer.Size = new System.Drawing.Size(65, 43);
            this.btnPointer.TabIndex = 0;
            this.btnPointer.Tag = "Pointer";
            this.btnPointer.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 308);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Graphic Computer: DAS";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPointer;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnNBezier;
    }
}

