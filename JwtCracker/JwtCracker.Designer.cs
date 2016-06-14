namespace JwtCracker
{
    partial class frmJwtCracker
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
            this.btnDecodeJwt = new System.Windows.Forms.Button();
            this.lblJwtIn = new System.Windows.Forms.Label();
            this.lblJwtOut = new System.Windows.Forms.Label();
            this.txtJwtOut = new System.Windows.Forms.TextBox();
            this.txtJwtIn = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnDecodeJwt
            // 
            this.btnDecodeJwt.Location = new System.Drawing.Point(25, 10);
            this.btnDecodeJwt.Name = "btnDecodeJwt";
            this.btnDecodeJwt.Size = new System.Drawing.Size(90, 25);
            this.btnDecodeJwt.TabIndex = 0;
            this.btnDecodeJwt.Text = "Decode JWT";
            this.btnDecodeJwt.UseVisualStyleBackColor = true;
            this.btnDecodeJwt.Click += new System.EventHandler(this.btnDecodeJwt_Click);
            // 
            // lblJwtIn
            // 
            this.lblJwtIn.AutoSize = true;
            this.lblJwtIn.Location = new System.Drawing.Point(25, 40);
            this.lblJwtIn.Name = "lblJwtIn";
            this.lblJwtIn.Size = new System.Drawing.Size(76, 13);
            this.lblJwtIn.TabIndex = 1;
            this.lblJwtIn.Text = "Encoded JWT";
            // 
            // lblJwtOut
            // 
            this.lblJwtOut.AutoSize = true;
            this.lblJwtOut.Location = new System.Drawing.Point(530, 40);
            this.lblJwtOut.Name = "lblJwtOut";
            this.lblJwtOut.Size = new System.Drawing.Size(77, 13);
            this.lblJwtOut.TabIndex = 2;
            this.lblJwtOut.Text = "Decoded JWT";
            // 
            // txtJwtOut
            // 
            this.txtJwtOut.Location = new System.Drawing.Point(530, 60);
            this.txtJwtOut.Multiline = true;
            this.txtJwtOut.Name = "txtJwtOut";
            this.txtJwtOut.ReadOnly = true;
            this.txtJwtOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJwtOut.Size = new System.Drawing.Size(500, 500);
            this.txtJwtOut.TabIndex = 3;
            // 
            // txtJwtIn
            // 
            this.txtJwtIn.Location = new System.Drawing.Point(25, 60);
            this.txtJwtIn.Multiline = true;
            this.txtJwtIn.Name = "txtJwtIn";
            this.txtJwtIn.Size = new System.Drawing.Size(500, 500);
            this.txtJwtIn.TabIndex = 4;
            // 
            // frmJwtCracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 581);
            this.Controls.Add(this.txtJwtIn);
            this.Controls.Add(this.txtJwtOut);
            this.Controls.Add(this.lblJwtOut);
            this.Controls.Add(this.lblJwtIn);
            this.Controls.Add(this.btnDecodeJwt);
            this.Name = "frmJwtCracker";
            this.Text = "JwtCracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDecodeJwt;
        private System.Windows.Forms.Label lblJwtIn;
        private System.Windows.Forms.Label lblJwtOut;
        private System.Windows.Forms.TextBox txtJwtOut;
        private System.Windows.Forms.TextBox txtJwtIn;
    }
}

