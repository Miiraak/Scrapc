namespace Scrapc
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void ButtonChoice_Click(object sender, EventArgs e)
        {
            switch (comboBoxLangage.Text)
            {
                case "HTML":
                    // Instencie FormHTML et l'affiche.
                    FormHTML formHTML = new();
                    formHTML.Show();
                    FormHTML.AllUrls.Clear();
                    break;
                case "Text":
                    // Pour FormText
                    FormTEXT formText = new();
                    formText.Show();
                    FormTEXT.AllUrls.Clear();
                    break;
                case "Images":
                    // Pour FormImage
                    FormImage formImage = new();
                    formImage.Show();
                    FormImage.ImageUrls.Clear();
                    break;
                case "URLs":
                    // Add for URLs 
                    break;
                default:
                    MessageBox.Show("Veuillez choisir une fonction.");
                    break;
            }
        }
    }
}

