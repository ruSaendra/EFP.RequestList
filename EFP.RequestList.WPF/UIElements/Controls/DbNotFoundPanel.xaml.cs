using Microsoft.Win32;
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
using EFP.RequestList.Libraries.Settings;
using EFP.RequestList.Libraries.HelperClasses;
using EFP.RequestList.Libraries;
using System.IO;
using EFP.RequestList.WPF.UIElements.Windows;

namespace EFP.RequestList.WPF.UIElements.Controls
{
    /// <summary>
    /// Interaction logic for DbNotFoundPanel.xaml
    /// </summary>
    public partial class DbNotFoundPanel : UserControl
    {
        public delegate void DbOpened();
        public event DbOpened OnDbOpened;

        public DbNotFoundPanel()
        {
            InitializeComponent();
        }

        private void createDbBtn_Click(object sender, RoutedEventArgs e)
        {
            var path = SettingsManager.DataBaseSettings.Path;
            var dirPath = System.IO.Path.GetDirectoryName(path);
            var fileName = System.IO.Path.GetFileName(path);

            var dlg = new SaveFileDialog()
            {
                Filter = "Database files | *.db",
                DefaultExt = "db",
                FileName = fileName ?? "database.db",
            };

            if (!dirPath.IsNullOrEmpty())
                dlg.DefaultDirectory = dirPath;
            else
                dlg.DefaultDirectory = Environment.GetEnvironmentVariable("APPDATA");

            if (dlg.ShowDialog() == true)
            {
                path = dlg.FileName;

                try
                {
                    DataBaseManager.CreateDB(path);
                    CheckForCurrencies();

                    SettingsManager.DataBaseSettings.Path = path;
                    SettingsManager.SaveSettings();
                    OnDbOpened?.Invoke();
                }
                catch
                {
                    DataBaseManager.CloseDB();
                }
            }
        }

        private void openDbBtn_Click(object sender, RoutedEventArgs e)
        {
            var path = SettingsManager.DataBaseSettings.Path;
            var dirPath = System.IO.Path.GetDirectoryName(path);
            var fileName = System.IO.Path.GetFileName(path);

            var dlg = new OpenFileDialog()
            {
                Filter = "Database files | *.db",
                DefaultExt = "db",
            };

            if(!dirPath.IsNullOrEmpty())
                dlg.DefaultDirectory = dirPath;
            else
                dlg.DefaultDirectory = Environment.GetEnvironmentVariable("APPDATA");

            if (dlg.ShowDialog() == true)
            {
                path = dlg.FileName;

                try
                {
                    if (!DataBaseManager.CheckIfDbExists(path))
                        throw new FileNotFoundException(path);

                    DataBaseManager.OpenDB(path);
                    CheckForCurrencies();

                    SettingsManager.DataBaseSettings.Path = path;
                    SettingsManager.SaveSettings();
                    OnDbOpened?.Invoke();
                }
                catch
                {
                    DataBaseManager.CloseDB();
                }
            }
        }

        private bool CheckForCurrencies()
        {
            var currList = DataBaseManager.CurrencyList;

            if (currList == null)
                throw new Exception("Wrong DB configuration.");

            if (currList != null && currList.Count() > 0)
                return true;

            MessageBox.Show("Требуется добавить хотя бы одну валюту.", "Внимание!", MessageBoxButton.OK);

            List<object> newCurrList = [];

            while (true)
            {
                var dlg = new AddCurrencyWindow();

                if (dlg.ShowDialog() == false)
                    break;

                var data = dlg.Data;
                if(data != null)
                    newCurrList.Add(data);

                if (MessageBox.Show("Добавить ещё одну валюту?", "Внимание!", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                    break;
            }

            if (newCurrList.Count() == 0)
                throw new Exception("Не обнаружено добавленных валют.");

            foreach (var item in newCurrList)
            {
                try
                {
                    var currData = ((string CurrName, double CurrRate))item;
                    DataBaseManager.AddCurrency(currData.CurrName, currData.CurrRate);
                }
                catch { }
            }

            if (DataBaseManager.CurrencyList.Count() == 0)
                throw new Exception("Не обнаружено добавленных валют.");

            return true;
        }
    }
}
