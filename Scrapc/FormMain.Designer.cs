namespace Scrapc
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            comboBoxLangage = new ComboBox();
            ButtonChoice = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 0;
            label1.Text = "Language :";
            // 
            // comboBoxLangage
            // 
            comboBoxLangage.FormattingEnabled = true;
            comboBoxLangage.Items.AddRange(new object[] { "HTML" });
            comboBoxLangage.Location = new Point(12, 27);
            comboBoxLangage.Name = "comboBoxLangage";
            comboBoxLangage.Size = new Size(224, 23);
            comboBoxLangage.TabIndex = 1;
            // 
            // ButtonChoice
            // 
            ButtonChoice.Location = new Point(89, 56);
            ButtonChoice.Name = "ButtonChoice";
            ButtonChoice.Size = new Size(75, 23);
            ButtonChoice.TabIndex = 2;
            ButtonChoice.Text = "OK";
            ButtonChoice.UseVisualStyleBackColor = true;
            ButtonChoice.Click += ButtonChoice_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(251, 91);
            Controls.Add(ButtonChoice);
            Controls.Add(comboBoxLangage);
            Controls.Add(label1);
            Cursor = Cursors.Cross;
            MinimizeBox = false;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Scrapc";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBoxLangage;
        private Button ButtonChoice;
    }
}
