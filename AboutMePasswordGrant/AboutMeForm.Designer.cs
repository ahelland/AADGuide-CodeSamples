namespace AboutMePasswordGrant
{
    partial class AboutMeForm
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
            this.lblAADDomainName = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblResource = new System.Windows.Forms.Label();
            this.lblClientId = new System.Windows.Forms.Label();
            this.txtAADDomainName = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtResource = new System.Windows.Forms.TextBox();
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAADDomainName
            // 
            this.lblAADDomainName.AutoSize = true;
            this.lblAADDomainName.Location = new System.Drawing.Point(10, 10);
            this.lblAADDomainName.Name = "lblAADDomainName";
            this.lblAADDomainName.Size = new System.Drawing.Size(102, 13);
            this.lblAADDomainName.TabIndex = 0;
            this.lblAADDomainName.Text = "AAD Domain Name:";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(10, 135);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(42, 13);
            this.lblOutput.TabIndex = 1;
            this.lblOutput.Text = "Output:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(10, 110);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(10, 85);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username:";
            // 
            // lblResource
            // 
            this.lblResource.AutoSize = true;
            this.lblResource.Location = new System.Drawing.Point(10, 60);
            this.lblResource.Name = "lblResource";
            this.lblResource.Size = new System.Drawing.Size(56, 13);
            this.lblResource.TabIndex = 4;
            this.lblResource.Text = "Resource:";
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(10, 35);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(45, 13);
            this.lblClientId.TabIndex = 5;
            this.lblClientId.Text = "ClientId:";
            // 
            // txtAADDomainName
            // 
            this.txtAADDomainName.Location = new System.Drawing.Point(130, 10);
            this.txtAADDomainName.Name = "txtAADDomainName";
            this.txtAADDomainName.Size = new System.Drawing.Size(250, 20);
            this.txtAADDomainName.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(130, 85);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(250, 20);
            this.txtUsername.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(130, 110);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(250, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(130, 135);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(250, 60);
            this.txtOutput.TabIndex = 5;
            // 
            // txtResource
            // 
            this.txtResource.Location = new System.Drawing.Point(130, 60);
            this.txtResource.Name = "txtResource";
            this.txtResource.Size = new System.Drawing.Size(250, 20);
            this.txtResource.TabIndex = 2;
            this.txtResource.Text = "https://graph.microsoft.com";
            // 
            // txtClientId
            // 
            this.txtClientId.Location = new System.Drawing.Point(130, 35);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(250, 20);
            this.txtClientId.TabIndex = 1;
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(10, 155);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(100, 40);
            this.btnAction.TabIndex = 6;
            this.btnAction.Text = "Get Token &&\r\nDo Lookup";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // AboutMeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 212);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.txtClientId);
            this.Controls.Add(this.txtResource);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtAADDomainName);
            this.Controls.Add(this.lblClientId);
            this.Controls.Add(this.lblResource);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblAADDomainName);
            this.Name = "AboutMeForm";
            this.Text = "About Me - Password Grant Flow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAADDomainName;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblResource;
        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.TextBox txtAADDomainName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtResource;
        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.Button btnAction;
    }
}

