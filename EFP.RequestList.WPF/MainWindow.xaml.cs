using EFP.RequestList.Libraries;
using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.Enums;
using EFP.RequestList.Libraries.HelperClasses;
using EFP.RequestList.Libraries.Settings;
using EFP.RequestList.WPF.Helpers;
using EFP.RequestList.WPF.UIElements.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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
        RequestListContext db;

        public MainWindow()
        {
            InitializeComponent();

            SettingsManager.LoadSettings();

            if (!DataBaseManager.CheckIfDbExists(SettingsManager.DataBaseSettings.Path))
            {
                mainGrd.Children.Clear();
                var dbNotFoundPanel = new DbNotFoundPanel();
                dbNotFoundPanel.OnDbOpened += DbOpened;

                mainGrd.Children.Add(dbNotFoundPanel);
            }
            else
            {
                DataBaseManager.OpenDB(SettingsManager.DataBaseSettings.Path);
                DbOpened();
            }
        }

        private void DbOpened()
        {
            UIResources.InvokeByMain(() =>
            {
                mainGrd.Children.Clear();
                mainGrd.Children.Add(new SessionPanel());
            });
        }

        private void Test()
        {
            if (Directory.Exists(@"C:\Users\saend\AppData\Roaming\EFP.RequestList.WPF"))
                Directory.Delete(@"C:\Users\saend\AppData\Roaming\EFP.RequestList.WPF", true);

            db = new RequestListContext();

            db.Add(new Currency
            {
                Name = "Балл реквеста",
                CurrencyRates = new()
                {
                    new CurrencyRate
                    {
                        DateTimeStart = DateTime.MinValue,
                        DateTimeEnd = DateTime.MaxValue,
                        Rate = 1,
                    }
                }
            });
            db.Add(new Currency
            {
                Name = "Рубль",
                CurrencyRates = new()
                {
                    new CurrencyRate
                    {
                        DateTimeStart = DateTime.MinValue,
                        DateTimeEnd = DateTime.MaxValue,
                        Rate = 3,
                    }
                }
            });
            db.Add(new Currency
            {
                Name = "Юникрон",
                CurrencyRates = new() 
                {
                    new CurrencyRate
                    {
                        DateTimeStart = DateTime.MinValue,
                        DateTimeEnd = DateTime.MaxValue,
                        Rate = .2
                    }
                },
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

            witcher.Requests.Add(new Request(15.7, rub));
            witcher.Requests.Add(new Request(35, reqPoint));
            witcher.Requests.Add(new Request(100, unicron));

            stalker.Requests.Add(new Request(10, rub)   );
            stalker.Requests.Add(new Request(1, rub));
            stalker.Requests.Add(new Request(10, unicron));

            traider.Requests.Add(new Request(10, unicron));
            traider.Requests.Add(new Request(15, unicron));
            traider.Requests.Add(new Request(200, unicron));
            db.SaveChanges();

            var list = db.RequestSet
                .GroupBy(req => req.RequestedContent)
                .Select(grp => new 
                    {
                        grp.Key.Name, 
                        Value = grp
                            .Select(req => req.ValueBase)
                            .Sum() 
                    })
                .OrderByDescending(req => req.Value)
                .ToList();

            list.ForEach(item => Trace.WriteLine($"{item.Name} - {item.Value}"));
        }
    }
}