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
                    FormHTML formHTML = new FormHTML();
                    formHTML.Show();
                    FormHTML.AllUrls.Clear();
                    break;
                case "Text":
                    FormTEXT formText = new();
                    formText.Show();
                    FormTEXT.AllUrls.Clear();
                    break;
                case "URLs":
                    // Add for URLs 
                    break;
                case "Images":
                    // Add for Images
                    break;
                default:
                    MessageBox.Show("Veuillez choisir un langage.");
                    break;
            }
        }
    }
}

