namespace Scrapc
{
    public partial class Menu : Form
    {
        /// <summary>
        /// Used to store the index of the function chosen by the user, determine which function to use.
        /// </summary>
        public static int FonctionIndex { get; set; }

        public Menu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Used to determine the function to use.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChoice_Click(object sender, EventArgs e)
        {
            switch (comboBoxLangage.SelectedIndex)
            {
                case 0:
                    FonctionIndex = 0;
                    break;
                case 1:
                    FonctionIndex = 1;
                    break;
                case 2:
                    FonctionIndex = 2;
                    break;
                case 3:
                    FonctionIndex = 3;
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("Veuillez choisir une fonction.");
                    break;
            }
            // Instanciation de FormHTML
            MenuInfo formHTML = new();
            formHTML.Show();
            MenuInfo.AllUrls.Clear();
        }
    }
}

