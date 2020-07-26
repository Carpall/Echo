namespace EchoGraphics
{
    partial class Main
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.MainRound = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.MainDrag = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.MainBar = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.MessageBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.ToolTip = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.Send = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.AllMessagesBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.MainBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainRound
            // 
            this.MainRound.BorderRadius = 8;
            this.MainRound.TargetControl = this;
            // 
            // MainDrag
            // 
            this.MainDrag.DragEndTransparencyValue = 0.99D;
            this.MainDrag.TargetControl = this.MainBar;
            // 
            // MainBar
            // 
            this.MainBar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.MainBar.BorderColor = System.Drawing.Color.White;
            this.MainBar.BorderRadius = 50;
            this.MainBar.Controls.Add(this.guna2ControlBox2);
            this.MainBar.Location = new System.Drawing.Point(13, 2);
            this.MainBar.Name = "MainBar";
            this.MainBar.ShadowDecoration.Parent = this.MainBar;
            this.MainBar.Size = new System.Drawing.Size(636, 15);
            this.MainBar.TabIndex = 3;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.BorderRadius = 10;
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.SystemColors.MenuHighlight;
            this.guna2ControlBox2.HoverState.Parent = this.guna2ControlBox2;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(605, 4);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.ShadowDecoration.Parent = this.guna2ControlBox2;
            this.guna2ControlBox2.Size = new System.Drawing.Size(13, 12);
            this.guna2ControlBox2.TabIndex = 4;
            this.ToolTip.SetToolTip(this.guna2ControlBox2, "Minimize");
            // 
            // MessageBox
            // 
            this.MessageBox.AcceptsTab = true;
            this.MessageBox.Animated = true;
            this.MessageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MessageBox.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.MessageBox.BorderRadius = 5;
            this.MessageBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.MessageBox.DefaultText = "";
            this.MessageBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.MessageBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.MessageBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.MessageBox.DisabledState.Parent = this.MessageBox;
            this.MessageBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.MessageBox.FillColor = System.Drawing.Color.Transparent;
            this.MessageBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.MessageBox.FocusedState.Parent = this.MessageBox;
            this.MessageBox.Font = new System.Drawing.Font("Hack", 9F);
            this.MessageBox.ForeColor = System.Drawing.Color.Transparent;
            this.MessageBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.MessageBox.HoverState.Parent = this.MessageBox;
            this.MessageBox.Location = new System.Drawing.Point(12, 301);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.PasswordChar = '\0';
            this.MessageBox.PlaceholderForeColor = System.Drawing.Color.White;
            this.MessageBox.PlaceholderText = "Write a crypted message";
            this.MessageBox.SelectedText = "";
            this.MessageBox.ShadowDecoration.Parent = this.MessageBox;
            this.MessageBox.Size = new System.Drawing.Size(574, 25);
            this.MessageBox.TabIndex = 0;
            // 
            // ToolTip
            // 
            this.ToolTip.AllowLinksHandling = true;
            this.ToolTip.AutoPopDelay = 5000;
            this.ToolTip.InitialDelay = 100;
            this.ToolTip.MaximumSize = new System.Drawing.Size(0, 0);
            this.ToolTip.ReshowDelay = 100;
            this.ToolTip.TitleFont = new System.Drawing.Font("Hack", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // Send
            // 
            this.Send.Animated = true;
            this.Send.AutoRoundedCorners = true;
            this.Send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Send.BorderRadius = 11;
            this.Send.CheckedState.Parent = this.Send;
            this.Send.CustomImages.Parent = this.Send;
            this.Send.Font = new System.Drawing.Font("Hack", 9F);
            this.Send.ForeColor = System.Drawing.Color.White;
            this.Send.HoverState.Parent = this.Send;
            this.Send.Location = new System.Drawing.Point(592, 301);
            this.Send.Name = "Send";
            this.Send.ShadowDecoration.Parent = this.Send;
            this.Send.Size = new System.Drawing.Size(59, 25);
            this.Send.TabIndex = 1;
            this.Send.Text = "Send";
            this.ToolTip.SetToolTip(this.Send, "Send messages using current encryption method");
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.SystemColors.MenuHighlight;
            this.guna2ControlBox1.HoverState.Parent = this.guna2ControlBox1;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(634, 3);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.ShadowDecoration.Parent = this.guna2ControlBox1;
            this.guna2ControlBox1.Size = new System.Drawing.Size(13, 12);
            this.guna2ControlBox1.TabIndex = 0;
            this.ToolTip.SetToolTip(this.guna2ControlBox1, "Close");
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Black;
            this.guna2PictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.ErrorImage")));
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.Location = new System.Drawing.Point(600, 239);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.ShadowDecoration.Parent = this.guna2PictureBox1;
            this.guna2PictureBox1.Size = new System.Drawing.Size(43, 43);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox1.TabIndex = 5;
            this.guna2PictureBox1.TabStop = false;
            this.ToolTip.SetToolTip(this.guna2PictureBox1, "Settings");
            this.guna2PictureBox1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // AllMessagesBox
            // 
            this.AllMessagesBox.AcceptsTab = true;
            this.AllMessagesBox.Animated = true;
            this.AllMessagesBox.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.AllMessagesBox.BorderRadius = 5;
            this.AllMessagesBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AllMessagesBox.DefaultText = "";
            this.AllMessagesBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.AllMessagesBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.AllMessagesBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AllMessagesBox.DisabledState.Parent = this.AllMessagesBox;
            this.AllMessagesBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AllMessagesBox.FillColor = System.Drawing.Color.Transparent;
            this.AllMessagesBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AllMessagesBox.FocusedState.Parent = this.AllMessagesBox;
            this.AllMessagesBox.Font = new System.Drawing.Font("Hack", 9F);
            this.AllMessagesBox.ForeColor = System.Drawing.Color.Transparent;
            this.AllMessagesBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AllMessagesBox.HoverState.Parent = this.AllMessagesBox;
            this.AllMessagesBox.Location = new System.Drawing.Point(12, 20);
            this.AllMessagesBox.Multiline = true;
            this.AllMessagesBox.Name = "AllMessagesBox";
            this.AllMessagesBox.PasswordChar = '\0';
            this.AllMessagesBox.PlaceholderForeColor = System.Drawing.Color.White;
            this.AllMessagesBox.PlaceholderText = "";
            this.AllMessagesBox.ReadOnly = true;
            this.AllMessagesBox.SelectedText = "";
            this.AllMessagesBox.ShadowDecoration.Parent = this.AllMessagesBox;
            this.AllMessagesBox.Size = new System.Drawing.Size(639, 270);
            this.AllMessagesBox.TabIndex = 2;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(672, 330);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.MainBar);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.AllMessagesBox);
            this.Font = new System.Drawing.Font("Hack", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Opacity = 0.99D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Echo";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MainBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse MainRound;
        private Guna.UI2.WinForms.Guna2DragControl MainDrag;
        private Guna.UI2.WinForms.Guna2TextBox MessageBox;
        private Guna.UI2.WinForms.Guna2HtmlToolTip ToolTip;
        private Guna.UI2.WinForms.Guna2Button Send;
        private Guna.UI2.WinForms.Guna2TextBox AllMessagesBox;
        private Guna.UI2.WinForms.Guna2Panel MainBar;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
    }
}

