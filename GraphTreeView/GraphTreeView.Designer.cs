namespace GraphTreeView
{
    partial class GraphTreeView
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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Tenants");
            this.btnGetTenantDetails = new System.Windows.Forms.Button();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.btnUserDetails = new System.Windows.Forms.Button();
            this.btnGroupDetails = new System.Windows.Forms.Button();
            this.btnLoadUsers = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnLoadGroups = new System.Windows.Forms.Button();
            this.tenantTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // btnGetTenantDetails
            // 
            this.btnGetTenantDetails.Location = new System.Drawing.Point(10, 10);
            this.btnGetTenantDetails.Name = "btnGetTenantDetails";
            this.btnGetTenantDetails.Size = new System.Drawing.Size(124, 23);
            this.btnGetTenantDetails.TabIndex = 22;
            this.btnGetTenantDetails.Text = "Get Tenant Details";
            this.btnGetTenantDetails.UseVisualStyleBackColor = true;
            this.btnGetTenantDetails.Click += new System.EventHandler(this.btnGetTenantDetails_Click);
            // 
            // txtDetails
            // 
            this.txtDetails.Enabled = false;
            this.txtDetails.Location = new System.Drawing.Point(450, 393);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetails.Size = new System.Drawing.Size(447, 252);
            this.txtDetails.TabIndex = 21;
            // 
            // btnUserDetails
            // 
            this.btnUserDetails.Location = new System.Drawing.Point(300, 50);
            this.btnUserDetails.Name = "btnUserDetails";
            this.btnUserDetails.Size = new System.Drawing.Size(75, 23);
            this.btnUserDetails.TabIndex = 20;
            this.btnUserDetails.Text = "User Details";
            this.btnUserDetails.UseVisualStyleBackColor = true;
            this.btnUserDetails.Click += new System.EventHandler(this.btnUserDetails_Click);
            // 
            // btnGroupDetails
            // 
            this.btnGroupDetails.Location = new System.Drawing.Point(150, 50);
            this.btnGroupDetails.Name = "btnGroupDetails";
            this.btnGroupDetails.Size = new System.Drawing.Size(129, 23);
            this.btnGroupDetails.TabIndex = 19;
            this.btnGroupDetails.Text = "Group Details";
            this.btnGroupDetails.UseVisualStyleBackColor = true;
            this.btnGroupDetails.Click += new System.EventHandler(this.btnGroupDetails_Click);
            // 
            // btnLoadUsers
            // 
            this.btnLoadUsers.Location = new System.Drawing.Point(300, 10);
            this.btnLoadUsers.Name = "btnLoadUsers";
            this.btnLoadUsers.Size = new System.Drawing.Size(75, 23);
            this.btnLoadUsers.TabIndex = 18;
            this.btnLoadUsers.Text = "Load Users";
            this.btnLoadUsers.UseVisualStyleBackColor = true;
            this.btnLoadUsers.Click += new System.EventHandler(this.btnLoadUsers_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Enabled = false;
            this.txtOutput.Location = new System.Drawing.Point(450, 100);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(447, 288);
            this.txtOutput.TabIndex = 17;
            // 
            // btnLoadGroups
            // 
            this.btnLoadGroups.Location = new System.Drawing.Point(150, 10);
            this.btnLoadGroups.Name = "btnLoadGroups";
            this.btnLoadGroups.Size = new System.Drawing.Size(129, 23);
            this.btnLoadGroups.TabIndex = 16;
            this.btnLoadGroups.Text = "Load Groups";
            this.btnLoadGroups.UseVisualStyleBackColor = true;
            this.btnLoadGroups.Click += new System.EventHandler(this.btnLoadGroups_Click);
            // 
            // tenantTree
            // 
            this.tenantTree.Location = new System.Drawing.Point(10, 100);
            this.tenantTree.Name = "tenantTree";
            treeNode5.Name = "Node0";
            treeNode5.Text = "Tenants";
            this.tenantTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.tenantTree.Size = new System.Drawing.Size(417, 547);
            this.tenantTree.TabIndex = 15;
            // 
            // GraphTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 654);
            this.Controls.Add(this.btnGetTenantDetails);
            this.Controls.Add(this.txtDetails);
            this.Controls.Add(this.btnUserDetails);
            this.Controls.Add(this.btnGroupDetails);
            this.Controls.Add(this.btnLoadUsers);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnLoadGroups);
            this.Controls.Add(this.tenantTree);
            this.Name = "GraphTreeView";
            this.Text = "Graph Tree View";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetTenantDetails;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Button btnUserDetails;
        private System.Windows.Forms.Button btnGroupDetails;
        private System.Windows.Forms.Button btnLoadUsers;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnLoadGroups;
        private System.Windows.Forms.TreeView tenantTree;
    }
}

