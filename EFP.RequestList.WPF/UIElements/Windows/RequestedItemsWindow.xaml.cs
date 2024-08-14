using EFP.RequestList.Libraries;
using EFP.RequestList.Libraries.DataStructures.Local;
using EFP.RequestList.Libraries.Enums;
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
    /// Interaction logic for RequestedItemsWindow.xaml
    /// </summary>
    public partial class RequestedItemsWindow : Window
    {
        private RequestedContentType? _type;
        private List<RequestedItem> _items = [];


        public RequestedContentType? Type
        {
            get => _type;
            set
            {
                _type = value;
                SelectItems(value);
            }
        }

        public RequestedItemsWindow()
        {
            InitializeComponent();
            DataBaseManager.RequestSetChanged += RefreshItems;
        }

        private void RefreshItems()
        {
            _items = DataBaseManager.RequestedItemList;
            SelectItems(_type);
        }

        private void SelectItems(RequestedContentType? type = null)
            => reqItemsLv.DataContext = (type switch
            {
                null or RequestedContentType.Unknown => _items,
                _ => _items.Where(i => i.Type == type),
            })
            .OrderByDescending(i => i.Value)
            .Take(5);


    }
}
