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

namespace aleks_Tih
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Comp company = new Comp(10);

        private void AddOffice_button_Click(object sender, RoutedEventArgs e)
        {
            if (!company.Add(Convert.ToInt32(AdressOffice_textBox.Text)))
                MessageBox.Show($"Офис а с адресом *{AdressOffice_textBox.Text}* не удалось добавить");
            DataOffice.ItemsSource = company.GetOffices();
        }

        private void DeleteOffice_button_Click(object sender, RoutedEventArgs e)
        {
            if (company.Count > 1)
            {
                company.Delete();
                DataOffice.ItemsSource = company.GetOffices();
            }
            else
            {
                company.Delete();
                DataOffice.ItemsSource = null;
            }

        }

        private void DataOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Office office = DataOffice.SelectedItem as Office;
            if (office != null)
            {
                office = company.Company1.Where(a => a.Adress == office.Adress).FirstOrDefault();
                DataWorker.ItemsSource = office.workers.GetWorkers();
            }
        }

        private void AddWorker_button_Click(object sender, RoutedEventArgs e)
        {
            Office office = DataOffice.SelectedItem as Office;
            office = company.Company1.Where(a => a.Adress == office.Adress).FirstOrDefault();
            office.workers.Add(Name_textBox.Text, Position_textBox.Text, Convert.ToInt32(Salary_textBox.Text));
            DataWorker.ItemsSource = office.workers.GetWorkers();
            DataOffice.ItemsSource = company.GetOffices();
        }

        private void DeleteWorker_button_Click(object sender, RoutedEventArgs e)
        {
            Office office = DataOffice.SelectedItem as Office;
            office = company.Company1.Where(a => a.Adress == office.Adress).FirstOrDefault();
            Worker worker = (Worker)DataWorker.SelectedItem;
            office.workers.Delete(worker.Famil);
            DataWorker.ItemsSource = office.workers.GetWorkers();
            DataOffice.ItemsSource = company.GetOffices();
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            company.Save();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            company.Load();
            DataOffice.ItemsSource = company.GetOffices();
        }

        private void AdressOffice_textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void AdressOffice_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void Name_textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Name_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsLetter(e.Text, 0));
        }

        private void Position_textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Position_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsLetter(e.Text, 0));
        }

        private void Salary_textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Salary_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
