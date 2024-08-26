﻿using EFP.RequestList.Libraries.Settings;
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

namespace EFP.RequestList.WPF.UIElements.Controls
{
    /// <summary>
    /// Interaction logic for SettingsTab.xaml
    /// </summary>
    public partial class SettingsTab : UserControl
    {
        public SettingsTab()
        {
            InitializeComponent();

            dbPathTbl.DataContext = SettingsManager.DataBaseSettings;
        }

        private void createDbBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void openDbBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
