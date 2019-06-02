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
        readonly SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-FLO2K4S\SQLEXPRESS;Initial Catalog=analyse;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void B_submit_Click(object sender, RoutedEventArgs e)
        {
            // Verbindung öffnen
            conn.Open();

            // Erzeuge Zufalls für die 'id'
            Random r = new Random();
            int z = r.Next(1, 100);

            int id = z;
            string sex = "";
            int err_count = 0;

            if (rb_female.IsChecked == true) { sex = "f"; }

            else if (rb_male.IsChecked == true) { sex = "m"; }

            else
            {
                err_count = 1;
                MessageBox.Show("Eingabefehler !", "Fehlermeldung", MessageBoxButton.OK, MessageBoxImage.Error);

            }


            decimal d;
            float f;

            if (decimal.TryParse(tb_age.Text, out d) == false | decimal.TryParse(tb_height.Text, out d) == false | float.TryParse(tb_weight.Text, out f) == false |
                rb_female.IsChecked == false & rb_male.IsChecked == false)
            {
                err_count = 1;
                MessageBox.Show("Eingabefehler in einem der Angaben !", "Fehlermeldung", MessageBoxButton.OK, MessageBoxImage.Error);


            }

            // Waren alle Eingaben richtig ?
            if (err_count == 0) // Ja
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO a_gewicht(id, age, height, weight, sex) VALUES('" + id + " ',' " + tb_age.Text + "','" + tb_height.Text + "','" + tb_weight.Text + "','" + sex + "')";
                    cmd.ExecuteNonQuery();

                    // MessageBox.Show("Eintrag in die Datenbank war erfolgreich !", "Datenbankeintrag", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tb_age.Clear();
                    tb_height.Clear();
                    tb_weight.Clear();
                    rb_female.IsChecked = false;
                    rb_male.IsChecked = false;
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
