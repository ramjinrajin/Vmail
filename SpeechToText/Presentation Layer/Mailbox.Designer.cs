namespace SpeechToText
{
    partial class Mailbox
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
        public void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mailbox));
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnAccountSettings = new System.Windows.Forms.Button();
            this.btnRecycle = new System.Windows.Forms.Button();
            this.btnDraft = new System.Windows.Forms.Button();
            this.btnSent = new System.Windows.Forms.Button();
            this.btnInbox = new System.Windows.Forms.Button();
            this.groupBoxMessages = new System.Windows.Forms.GroupBox();
            this.NoMailLabel = new System.Windows.Forms.Label();
            this.gridviewMails = new System.Windows.Forms.DataGridView();
            this.btnCompose = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbSignout = new System.Windows.Forms.LinkLabel();
            this.btnDeleteMail = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.ColTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMsgId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxActions.SuspendLayout();
            this.groupBoxMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewMails)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.BackgroundImage = global::SpeechToText.Properties.Resources.circles_highlights_background_form_size_47438_3840x2400;
            this.groupBoxActions.Controls.Add(this.btnAccountSettings);
            this.groupBoxActions.Controls.Add(this.btnRecycle);
            this.groupBoxActions.Controls.Add(this.btnDraft);
            this.groupBoxActions.Controls.Add(this.btnSent);
            this.groupBoxActions.Controls.Add(this.btnInbox);
            this.groupBoxActions.Location = new System.Drawing.Point(12, 98);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(238, 376);
            this.groupBoxActions.TabIndex = 0;
            this.groupBoxActions.TabStop = false;
            // 
            // btnAccountSettings
            // 
            this.btnAccountSettings.BackColor = System.Drawing.Color.SlateGray;
            this.btnAccountSettings.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccountSettings.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAccountSettings.Location = new System.Drawing.Point(15, 319);
            this.btnAccountSettings.Name = "btnAccountSettings";
            this.btnAccountSettings.Size = new System.Drawing.Size(202, 39);
            this.btnAccountSettings.TabIndex = 4;
            this.btnAccountSettings.Text = "User Settings";
            this.btnAccountSettings.UseVisualStyleBackColor = false;
            this.btnAccountSettings.Click += new System.EventHandler(this.btnAccountSettings_Click);
            // 
            // btnRecycle
            // 
            this.btnRecycle.BackColor = System.Drawing.Color.SlateGray;
            this.btnRecycle.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecycle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRecycle.Location = new System.Drawing.Point(15, 250);
            this.btnRecycle.Name = "btnRecycle";
            this.btnRecycle.Size = new System.Drawing.Size(202, 39);
            this.btnRecycle.TabIndex = 3;
            this.btnRecycle.Text = "Recycle bin";
            this.btnRecycle.UseVisualStyleBackColor = false;
            this.btnRecycle.Click += new System.EventHandler(this.btnRecycle_Click);
            // 
            // btnDraft
            // 
            this.btnDraft.BackColor = System.Drawing.Color.SlateGray;
            this.btnDraft.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDraft.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDraft.Location = new System.Drawing.Point(15, 177);
            this.btnDraft.Name = "btnDraft";
            this.btnDraft.Size = new System.Drawing.Size(202, 39);
            this.btnDraft.TabIndex = 2;
            this.btnDraft.Text = "Draft";
            this.btnDraft.UseVisualStyleBackColor = false;
            this.btnDraft.Click += new System.EventHandler(this.btnDraft_Click);
            // 
            // btnSent
            // 
            this.btnSent.BackColor = System.Drawing.Color.SlateGray;
            this.btnSent.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSent.Location = new System.Drawing.Point(15, 108);
            this.btnSent.Name = "btnSent";
            this.btnSent.Size = new System.Drawing.Size(202, 39);
            this.btnSent.TabIndex = 1;
            this.btnSent.Text = "Sent";
            this.btnSent.UseVisualStyleBackColor = false;
            this.btnSent.Click += new System.EventHandler(this.btnSent_Click);
            // 
            // btnInbox
            // 
            this.btnInbox.BackColor = System.Drawing.Color.SlateGray;
            this.btnInbox.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInbox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInbox.Location = new System.Drawing.Point(15, 32);
            this.btnInbox.Name = "btnInbox";
            this.btnInbox.Size = new System.Drawing.Size(202, 39);
            this.btnInbox.TabIndex = 0;
            this.btnInbox.Text = "Inbox";
            this.btnInbox.UseVisualStyleBackColor = false;
            this.btnInbox.Click += new System.EventHandler(this.btnInbox_Click);
            // 
            // groupBoxMessages
            // 
            this.groupBoxMessages.Controls.Add(this.NoMailLabel);
            this.groupBoxMessages.Controls.Add(this.gridviewMails);
            this.groupBoxMessages.Location = new System.Drawing.Point(269, 98);
            this.groupBoxMessages.Name = "groupBoxMessages";
            this.groupBoxMessages.Size = new System.Drawing.Size(898, 542);
            this.groupBoxMessages.TabIndex = 1;
            this.groupBoxMessages.TabStop = false;
            // 
            // NoMailLabel
            // 
            this.NoMailLabel.AutoSize = true;
            this.NoMailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoMailLabel.Location = new System.Drawing.Point(368, 81);
            this.NoMailLabel.Name = "NoMailLabel";
            this.NoMailLabel.Size = new System.Drawing.Size(125, 17);
            this.NoMailLabel.TabIndex = 1;
            this.NoMailLabel.Text = "No mail found !!!";
            // 
            // gridviewMails
            // 
            this.gridviewMails.AllowUserToAddRows = false;
            this.gridviewMails.AllowUserToDeleteRows = false;
            this.gridviewMails.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(97)))), ((int)(((byte)(176)))));
            this.gridviewMails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewMails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTo,
            this.ColFileName,
            this.ColMsg,
            this.ColFrom,
            this.ColSubject,
            this.ColMsgId});
            this.gridviewMails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gridviewMails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridviewMails.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridviewMails.Location = new System.Drawing.Point(6, 13);
            this.gridviewMails.MultiSelect = false;
            this.gridviewMails.Name = "gridviewMails";
            this.gridviewMails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridviewMails.Size = new System.Drawing.Size(885, 527);
            this.gridviewMails.TabIndex = 0;
            this.gridviewMails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnCompose
            // 
            this.btnCompose.BackColor = System.Drawing.Color.SlateGray;
            this.btnCompose.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCompose.Location = new System.Drawing.Point(12, 47);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(238, 45);
            this.btnCompose.TabIndex = 1;
            this.btnCompose.Text = "Compose";
            this.btnCompose.UseVisualStyleBackColor = false;
            this.btnCompose.Click += new System.EventHandler(this.btnCompose_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(269, 47);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(730, 45);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.SlateGray;
            this.btnSearch.Font = new System.Drawing.Font("Trebuchet MS", 15.75F);
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSearch.Location = new System.Drawing.Point(1005, 47);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(162, 45);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.BackgroundImage = global::SpeechToText.Properties.Resources.circles_highlights_background_form_size_47438_3840x2400;
            this.groupBoxInfo.Controls.Add(this.label5);
            this.groupBoxInfo.Controls.Add(this.label4);
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.label2);
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Location = new System.Drawing.Point(13, 480);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(237, 171);
            this.groupBoxInfo.TabIndex = 6;
            this.groupBoxInfo.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(97)))), ((int)(((byte)(187)))));
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(36, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Chat : Disabled ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(97)))), ((int)(((byte)(187)))));
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(36, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Status : Online";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(97)))), ((int)(((byte)(187)))));
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(36, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Memory : 1kb";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(97)))), ((int)(((byte)(187)))));
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(36, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Mails : 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(97)))), ((int)(((byte)(187)))));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(36, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configuration : POP ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1103, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "hi admin !!!";
            // 
            // lbSignout
            // 
            this.lbSignout.AutoSize = true;
            this.lbSignout.Location = new System.Drawing.Point(1113, 26);
            this.lbSignout.Name = "lbSignout";
            this.lbSignout.Size = new System.Drawing.Size(43, 13);
            this.lbSignout.TabIndex = 5;
            this.lbSignout.TabStop = true;
            this.lbSignout.Text = "Signout";
            this.lbSignout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbSignout_LinkClicked);
            // 
            // btnDeleteMail
            // 
            this.btnDeleteMail.BackColor = System.Drawing.Color.SlateGray;
            this.btnDeleteMail.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteMail.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeleteMail.Location = new System.Drawing.Point(744, 665);
            this.btnDeleteMail.Name = "btnDeleteMail";
            this.btnDeleteMail.Size = new System.Drawing.Size(163, 39);
            this.btnDeleteMail.TabIndex = 5;
            this.btnDeleteMail.Text = "Delete Forever";
            this.btnDeleteMail.UseVisualStyleBackColor = false;
            this.btnDeleteMail.Click += new System.EventHandler(this.btnDeleteMail_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.SlateGray;
            this.btnRestore.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRestore.Location = new System.Drawing.Point(565, 665);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(163, 39);
            this.btnRestore.TabIndex = 6;
            this.btnRestore.Text = "Restore Mail";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // ColTo
            // 
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            this.ColTo.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColTo.HeaderText = "To";
            this.ColTo.Name = "ColTo";
            this.ColTo.Visible = false;
            this.ColTo.Width = 250;
            // 
            // ColFileName
            // 
            this.ColFileName.HeaderText = "file";
            this.ColFileName.Name = "ColFileName";
            this.ColFileName.Visible = false;
            // 
            // ColMsg
            // 
            this.ColMsg.HeaderText = "Message";
            this.ColMsg.Name = "ColMsg";
            this.ColMsg.Visible = false;
            // 
            // ColFrom
            // 
            this.ColFrom.HeaderText = "From";
            this.ColFrom.Name = "ColFrom";
            this.ColFrom.Width = 250;
            // 
            // ColSubject
            // 
            this.ColSubject.HeaderText = "Subject";
            this.ColSubject.Name = "ColSubject";
            this.ColSubject.Width = 590;
            // 
            // ColMsgId
            // 
            this.ColMsgId.HeaderText = "msgid";
            this.ColMsgId.Name = "ColMsgId";
            this.ColMsgId.Visible = false;
            // 
            // Mailbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SpeechToText.Properties.Resources.circles_highlights_background_form_size_47438_3840x24001;
            this.ClientSize = new System.Drawing.Size(1182, 714);
            this.Controls.Add(this.btnDeleteMail);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.lbSignout);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnCompose);
            this.Controls.Add(this.groupBoxMessages);
            this.Controls.Add(this.groupBoxActions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Mailbox";
            this.Text = "Mailbox";
            this.Load += new System.EventHandler(this.Mailbox_Load);
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxMessages.ResumeLayout(false);
            this.groupBoxMessages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewMails)).EndInit();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.GroupBox groupBoxMessages;
        private System.Windows.Forms.Button btnCompose;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRecycle;
        private System.Windows.Forms.Button btnDraft;
        private System.Windows.Forms.Button btnSent;
        public System.Windows.Forms.Button btnInbox;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAccountSettings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lbSignout;
        private System.Windows.Forms.Label NoMailLabel;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnDeleteMail;
        private System.Windows.Forms.DataGridView gridviewMails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMsgId;
    }
}