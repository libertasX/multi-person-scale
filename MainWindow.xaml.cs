using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;



namespace multi_person_scale
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly SqlConnection conn = new SqlConnection(@"Data Source=your_computer_name\SQLEXPRESS;Initial Catalog=analyse;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            // Verbindung öffnen
            conn.Open();

            // Erzeuge Zufalls für die 'id'
            Random r = new Random();
            int z = r.Next(1, 100);

            int id = z;
            string sex = "";
            int errCount = 0;

            if (radioButtonFemale.IsChecked == true) { sex = "f"; }

            else if (radioButtonMale.IsChecked == true) { sex = "m"; }

            else
            {
                errCount = 1;
                MessageBox.Show("Eingabefehler !", "Fehlermeldung", MessageBoxButton.OK, MessageBoxImage.Error);

            }


            decimal d;
            float f;

            if (decimal.TryParse(textBoxAge.Text, out d) == false | decimal.TryParse(textBoxHeight.Text, out d) == false | float.TryParse(textBoxWeight.Text, out f) == false |
                radioButtonFemale.IsChecked == false & radioButtonMale.IsChecked == false)
            {
                errCount = 1;
                MessageBox.Show("Eingabefehler in einem der Angaben !", "Fehlermeldung", MessageBoxButton.OK, MessageBoxImage.Error);


            }

            // Waren alle Eingaben richtig ?
            if (errCount == 0) // Ja
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO a_gewicht(id, age, height, weight, sex) VALUES('" + id + " ',' " + textBoxAge.Text + "','" + textBoxHeight.Text + "','" + textBoxWeight.Text + "','" + sex + "')";
                    cmd.ExecuteNonQuery();

                    // MessageBox.Show("Eintrag in die Datenbank war erfolgreich !", "Datenbankeintrag", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxAge.Clear();
                    textBoxHeight.Clear();
                    textBoxWeight.Clear();
                    radioButtonFemale.IsChecked = false;
                    radioButtonMale.IsChecked = false;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Fehlermeldung", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            conn.Close();



        }
    }
}
