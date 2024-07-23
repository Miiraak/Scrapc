namespace CScrap
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
                    FormHTML formHTML = new();
                    formHTML.Show();
                    FormHTML.AllUrls.Clear();
                    break;
                default:
                    MessageBox.Show("Veuillez choisir un langage.");
                    break;
            }
        }
    }
}

