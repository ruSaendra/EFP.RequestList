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
using System.Windows.Shapes;

namespace EFP.RequestList.WPF.UIElements.Windows
{
    /// <summary>
    /// Interaction logic for AddCurrencyWindow.xaml
    /// </summary>
    public partial class AddCurrencyWindow : Window
    {
        public object? Data;

        public AddCurrencyWindow()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            (string CurrName, double CurrRate) data = new();

            data.CurrName = currNameTbx.Text;

            if (!double.TryParse(currRateTbx.Text, out data.CurrRate))
            {
                MessageBox.Show("Неверное значение курса валюты!", "Ошибка", MessageBoxButton.OK);
                return;
            }
            
            Data = data;
            DialogResult = true;
            Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
