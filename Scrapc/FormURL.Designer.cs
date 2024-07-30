using System.Windows.Forms;

namespace Scrapc
{
    partial class FormURL
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
            ButtonBack = new Button();
            ButtonScrap = new Button();
            label1 = new Label();
            textBoxURL = new TextBox();
            ButtonCrawl = new Button();
            folderBrowserDialogTEXT = new FolderBrowserDialog();
            label2 = new Label();
            textBoxNumberCrawl = new TextBox();
            ButtonShowUrlsGathered = new Button();
            SuspendLayout();
            // 
            // ButtonBack
            // 
            ButtonBack.Location = new Point(195, 123);
            ButtonBack.Name = "ButtonBack";
            ButtonBack.Size = new Size(75, 23);
            ButtonBack.TabIndex = 0;
            ButtonBack.Text = "Back";
            ButtonBack.UseVisualStyleBackColor = true;
            ButtonBack.Click += ButtonBack_Click;
            // 
            // ButtonScrap
            // 
            ButtonScrap.Location = new Point(104, 123);
            ButtonScrap.Name = "ButtonScrap";
            ButtonScrap.Size = new Size(75, 23);
            ButtonScrap.TabIndex = 1;
            ButtonScrap.Text = "Scrap !";
            ButtonScrap.UseVisualStyleBackColor = true;
            ButtonScrap.Click += ButtonScrap_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 2;
            label1.Text = "URL :";
            // 
            // textBoxURL
            // 
            textBoxURL.Location = new Point(12, 31);
            textBoxURL.Name = "textBoxURL";
            textBoxURL.Size = new Size(262, 23);
            textBoxURL.TabIndex = 3;
            // 
            // ButtonCrawl
            // 
            ButtonCrawl.Location = new Point(195, 82);
            ButtonCrawl.Name = "ButtonCrawl";
            ButtonCrawl.Size = new Size(75, 23);
            ButtonCrawl.TabIndex = 4;
            ButtonCrawl.Text = "Crawl";
            ButtonCrawl.UseVisualStyleBackColor = true;
            ButtonCrawl.Click += ButtonCrawl_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 64);
            label2.Name = "label2";
            label2.Size = new Size(110, 15);
            label2.TabIndex = 5;
            label2.Text = "Max URLs to crawl :";
            // 
            // textBoxNumberCrawl
            // 
            textBoxNumberCrawl.Location = new Point(12, 82);
            textBoxNumberCrawl.Name = "textBoxNumberCrawl";
            textBoxNumberCrawl.Size = new Size(167, 23);
            textBoxNumberCrawl.TabIndex = 6;
            // 
            // ButtonShowUrlsGathered
            // 
            ButtonShowUrlsGathered.Location = new Point(12, 123);
            ButtonShowUrlsGathered.Name = "ButtonShowUrlsGathered";
            ButtonShowUrlsGathered.Size = new Size(75, 23);
            ButtonShowUrlsGathered.TabIndex = 7;
            ButtonShowUrlsGathered.Text = "URLs ?";
            ButtonShowUrlsGathered.UseVisualStyleBackColor = true;
            ButtonShowUrlsGathered.Click += ButtonShowUrlsGathered_Click;
            // 
            // FormURL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(284, 161);
            ControlBox = false;
            Controls.Add(ButtonShowUrlsGathered);
            Controls.Add(textBoxNumberCrawl);
            Controls.Add(label2);
            Controls.Add(ButtonCrawl);
            Controls.Add(textBoxURL);
            Controls.Add(label1);
            Controls.Add(ButtonScrap);
            Controls.Add(ButtonBack);
            Cursor = Cursors.Cross;
            MaximizeBox = false;
            Name = "FormURL";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Scrapc : URL";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonBack;
        private Button ButtonScrap;
        private Label label1;
        private TextBox textBoxURL;
        private Button ButtonCrawl;
        private FolderBrowserDialog folderBrowserDialogTEXT;
        private Label label2;
        private TextBox textBoxNumberCrawl;
        private Button ButtonShowUrlsGathered;
    }
}