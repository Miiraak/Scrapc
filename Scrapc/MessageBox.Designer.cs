namespace Scrapc
{
    partial class MessageBox
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
            checkedListBoxURL = new CheckedListBox();
            buttonOK = new Button();
            button1 = new Button();
            buttonUnselect = new Button();
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
            // checkedListBoxURL
            // 
            checkedListBoxURL.CheckOnClick = true;
            checkedListBoxURL.FormattingEnabled = true;
            checkedListBoxURL.Location = new Point(12, 27);
            checkedListBoxURL.Name = "checkedListBoxURL";
            checkedListBoxURL.Size = new Size(401, 364);
            checkedListBoxURL.TabIndex = 1;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(338, 397);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 2;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 397);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Select all";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonSelect_Click;
            // 
            // buttonUnselect
            // 
            buttonUnselect.Location = new Point(93, 397);
            buttonUnselect.Name = "buttonUnselect";
            buttonUnselect.Size = new Size(75, 23);
            buttonUnselect.TabIndex = 4;
            buttonUnselect.Text = "Unselect all";
            buttonUnselect.UseVisualStyleBackColor = true;
            buttonUnselect.Click += buttonUnselect_Click;
            // 
            // MessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(425, 430);
            Controls.Add(buttonUnselect);
            Controls.Add(button1);
            Controls.Add(buttonOK);
            Controls.Add(checkedListBoxURL);
            Controls.Add(label1);
            Cursor = Cursors.Cross;
            Name = "MessageBox";
            Text = "Scrapy : URLs";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckedListBox checkedListBoxURL;
        private Button buttonOK;
        private Button button1;
        private Button buttonUnselect;
    }
}