namespace SpeechToText
{
    partial class MailMainInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailMainInterface));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInbox = new System.Windows.Forms.Button();
            this.btnSent = new System.Windows.Forms.Button();
            this.btnDraft = new System.Windows.Forms.Button();
            this.btnRecycle = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRecycle);
            this.groupBox1.Controls.Add(this.btnDraft);
            this.groupBox1.Controls.Add(this.btnSent);
            this.groupBox1.Controls.Add(this.btnInbox);
            this.groupBox1.Location = new System.Drawing.Point(24, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 302);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnInbox
            // 
            this.btnInbox.BackColor = System.Drawing.Color.SlateGray;
            this.btnInbox.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInbox.ForeColor = System.Drawing.Color.White;
            this.btnInbox.Location = new System.Drawing.Point(13, 39);
            this.btnInbox.Name = "btnInbox";
            this.btnInbox.Size = new System.Drawing.Size(159, 35);
            this.btnInbox.TabIndex = 3;
            this.btnInbox.Text = "Inbox";
            this.btnInbox.UseVisualStyleBackColor = false;
            this.btnInbox.Click += new System.EventHandler(this.btnInbox_Click);
            // 
            // btnSent
            // 
            this.btnSent.BackColor = System.Drawing.Color.SlateGray;
            this.btnSent.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSent.ForeColor = System.Drawing.Color.White;
            this.btnSent.Location = new System.Drawing.Point(13, 106);
            this.btnSent.Name = "btnSent";
            this.btnSent.Size = new System.Drawing.Size(159, 35);
            this.btnSent.TabIndex = 3;
            this.btnSent.Text = "Sent";
            this.btnSent.UseVisualStyleBackColor = false;
            // 
            // btnDraft
            // 
            this.btnDraft.BackColor = System.Drawing.Color.SlateGray;
            this.btnDraft.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDraft.ForeColor = System.Drawing.Color.White;
            this.btnDraft.Location = new System.Drawing.Point(13, 169);
            this.btnDraft.Name = "btnDraft";
            this.btnDraft.Size = new System.Drawing.Size(159, 35);
            this.btnDraft.TabIndex = 3;
            this.btnDraft.Text = "Draft";
            this.btnDraft.UseVisualStyleBackColor = false;
            // 
            // btnRecycle
            // 
            this.btnRecycle.BackColor = System.Drawing.Color.SlateGray;
            this.btnRecycle.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecycle.ForeColor = System.Drawing.Color.White;
            this.btnRecycle.Location = new System.Drawing.Point(13, 237);
            this.btnRecycle.Name = "btnRecycle";
            this.btnRecycle.Size = new System.Drawing.Size(159, 35);
            this.btnRecycle.TabIndex = 3;
            this.btnRecycle.Text = "Recycle bin";
            this.btnRecycle.UseVisualStyleBackColor = false;
            // 
            // MailMainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1132, 554);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "MailMainInterface";
            this.Text = "MailMainInterface";
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.Load += new System.EventHandler(this.MailMainInterface_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRecycle;
        private System.Windows.Forms.Button btnDraft;
        private System.Windows.Forms.Button btnSent;
        private System.Windows.Forms.Button btnInbox;
    }
}