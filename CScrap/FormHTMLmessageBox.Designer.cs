namespace CScrap
{
    partial class FormHTMLmessageBox
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
            label1 = new Label();
            richTextBoxListURL = new RichTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(124, 15);
            label1.TabIndex = 0;
            label1.Text = "List of gathered URLs :";
            // 
            // richTextBoxListURL
            // 
            richTextBoxListURL.Location = new Point(12, 27);
            richTextBoxListURL.Name = "richTextBoxListURL";
            richTextBoxListURL.ReadOnly = true;
            richTextBoxListURL.Size = new Size(401, 364);
            richTextBoxListURL.TabIndex = 1;
            richTextBoxListURL.Text = "";
            // 
            // FormHTMLmessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(425, 403);
            Controls.Add(richTextBoxListURL);
            Controls.Add(label1);
            Cursor = Cursors.Cross;
            Name = "FormHTMLmessageBox";
            Text = "Scrapy : HTML - URLs";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private RichTextBox richTextBoxListURL;
    }
}