namespace SquizzConnector
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
            this.lineItemButton = new System.Windows.Forms.Button();
            this.cardItemsButton = new System.Windows.Forms.Button();
            this.joinTables = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lineItemButton
            // 
            this.lineItemButton.Location = new System.Drawing.Point(28, 56);
            this.lineItemButton.Name = "lineItemButton";
            this.lineItemButton.Size = new System.Drawing.Size(75, 23);
            this.lineItemButton.TabIndex = 0;
            this.lineItemButton.Text = "LineItems";
            this.lineItemButton.UseVisualStyleBackColor = true;
            this.lineItemButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // cardItemsButton
            // 
            this.cardItemsButton.Location = new System.Drawing.Point(157, 56);
            this.cardItemsButton.Name = "cardItemsButton";
            this.cardItemsButton.Size = new System.Drawing.Size(75, 23);
            this.cardItemsButton.TabIndex = 1;
            this.cardItemsButton.Text = "Card Items";
            this.cardItemsButton.UseVisualStyleBackColor = true;
            this.cardItemsButton.Click += new System.EventHandler(this.cardItemsButton_Click);
            // 
            // joinTables
            // 
            this.joinTables.Location = new System.Drawing.Point(72, 128);
            this.joinTables.Name = "joinTables";
            this.joinTables.Size = new System.Drawing.Size(129, 23);
            this.joinTables.TabIndex = 2;
            this.joinTables.Text = "Generate Contacts";
            this.joinTables.UseVisualStyleBackColor = true;
            this.joinTables.Click += new System.EventHandler(this.joinTables_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.joinTables);
            this.Controls.Add(this.cardItemsButton);
            this.Controls.Add(this.lineItemButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lineItemButton;
        private System.Windows.Forms.Button cardItemsButton;
        private System.Windows.Forms.Button joinTables;
    }
}

