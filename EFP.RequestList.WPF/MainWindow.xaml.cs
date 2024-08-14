using EFP.RequestList.Libraries;
using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.Enums;
using EFP.RequestList.Libraries.Settings;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EFP.RequestList.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RequestListContext db; // = new RequestListContext();

        public MainWindow()
        {
            InitializeComponent();

            SettingsManager.LoadSettings();
            DataBaseManager.
        }

        private void Test()
        {
            if (Directory.Exists(@"C:\Users\saend\AppData\Roaming\EFP.RequestList.WPF"))
                Directory.Delete(@"C:\Users\saend\AppData\Roaming\EFP.RequestList.WPF", true);

            db = new RequestListContext();

            db.Add(new Currency
            {
                Name = "Балл реквеста",
                Rate = 1,
            });
            db.Add(new Currency
            {
                Name = "Рубль",
                Rate = 3,
            });
            db.Add(new Currency
            {
                Name = "Юникрон",
                Rate = 0.2,
            });
            db.SaveChanges();

            var currencies = db.CurrencySet.OrderBy(c => c.Id).ToList();

            var reqPoint = db.CurrencySet.OrderBy(c => c.Id).ToList()[0];
            var rub = db.CurrencySet.OrderBy(c => c.Id).ToList()[1];
            var unicron = db.CurrencySet.OrderBy(c => c.Id).ToList()[2];

            db.Add(new RequestedContent()
            {
                Type = RequestedContentType.Game,
                Name = "Witcher 3",
                Description = ""
            });
            db.Add(new RequestedContent()
            {
                Type = RequestedContentType.Game,
                Name = "S.T.A.L.K.E.R. 2",
                Description = ""
            });
            db.Add(new RequestedContent()
            {
                Type = RequestedContentType.Game,
                Name = "Tomb Raider",
                Description = ""
            });
            db.SaveChanges();

            var witcher = db.RequestedContentSet.OrderBy(rc => rc.Id).ToList()[0];
            var stalker = db.RequestedContentSet.OrderBy(rc => rc.Id).ToList()[1];
            var traider = db.RequestedContentSet.OrderBy(rc => rc.Id).ToList()[2];

            witcher.Requests.Add(new Request()
            {
                Currency = rub,
                Value = 15.7
            });
            witcher.Requests.Add(new Request()
            {
                Currency = reqPoint,
                Value = 35
            });
            witcher.Requests.Add(new Request()
            {
                Currency = unicron,
                Value = 100
            });

            stalker.Requests.Add(new Request()
            {
                Currency = rub,
                Value = 10
            });
            stalker.Requests.Add(new Request()
            {
                Currency = rub,
                Value = 1
            });
            stalker.Requests.Add(new Request()
            {
                Currency = unicron,
                Value = 10
            });

            traider.Requests.Add(new Request()
            {
                Currency = unicron,
                Value = 10
            });
            traider.Requests.Add(new Request()
            {
                Currency = unicron,
                Value = 15
            });
            traider.Requests.Add(new Request()
            {
                Currency = unicron,
                Value = 200
            });
            db.SaveChanges();

            var list = db.RequestSet
                .AsEnumerable()
                .GroupBy(req => req.RequestedContent)
                .Select(grp => new { grp.Key.Name, Value = grp.Select(req => req.InternalValue).Sum() })
                .OrderByDescending(req => req.Value)
                .ToList();

            list.ForEach(item => Trace.WriteLine($"{item.Name} - {item.Value}"));
        }
    }
}