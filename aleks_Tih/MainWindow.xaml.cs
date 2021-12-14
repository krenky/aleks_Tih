using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        Comp company = new Comp(3);
        /// <summary>
        /// обработчик нажаития добавления офиса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOffice_button_Click(object sender, RoutedEventArgs e)
        {
            if (!company.Add(AdressOffice_textBox.Text))
                MessageBox.Show($"Офис а с адресом *{AdressOffice_textBox.Text}* не удалось добавить");
            DataOffice.ItemsSource = company.GetOffices();
        }
        /// <summary>
        /// обработчик нажаития удаления офиса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Обработчик смены выбранного объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Office office = DataOffice.SelectedItem as Office;
                office = company.Company1.Where(a => a.Adress == office.Adress).FirstOrDefault();
                DataWorker.ItemsSource = office.workers.GetWorkers();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
            
        }
        /// <summary>
        /// обработчик нажаития добавления работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWorker_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Office office = DataOffice.SelectedItem as Office;
                office = company.Company1.Where(a => a.Adress == office.Adress).FirstOrDefault();
                office.workers.Add(Name_textBox.Text, Position_textBox.Text, Convert.ToInt32(Salary_textBox.Text));
                DataWorker.ItemsSource = office.workers.GetWorkers();
                DataOffice.ItemsSource = company.GetOffices();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        /// <summary>
        /// обработчик нажаития удаления работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteWorker_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Office office = DataOffice.SelectedItem as Office;
                office = company.Company1.Where(a => a.Adress == office.Adress).FirstOrDefault();
                Worker worker = (Worker)DataWorker.SelectedItem;
                office.workers.Delete(worker.Famil);
                DataWorker.ItemsSource = office.workers.GetWorkers();
                DataOffice.ItemsSource = company.GetOffices();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
            
        }
        /// <summary>
        /// обработчик нажаития сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if ((bool)saveFileDialog.ShowDialog())
            {
                using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
                    company.Save(fs);
            }
        }
        /// <summary>
        /// обработчик нажаития загрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                using (FileStream fs = (FileStream)openFileDialog.OpenFile())
                    company.Load(fs);
                DataOffice.ItemsSource = company.GetOffices();
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdressOffice_textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        /// <summary>
        /// получение и проверка вводимого символа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdressOffice_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsLetterOrDigit(e.Text, 0));
        }
        /// <summary>
        /// Обработчик нажатия кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Name_textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        /// <summary>
        /// получение и проверка вводимого символа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Name_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsLetter(e.Text, 0));
        }
        /// <summary>
        /// Обработчик нажатия кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Position_textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        /// <summary>
        /// получение и проверка вводимого символа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Position_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsLetter(e.Text, 0));
        }
        /// <summary>
        /// Обработчик нажатия кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Salary_textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        /// <summary>
        /// получение и проверка вводимого символа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Salary_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
