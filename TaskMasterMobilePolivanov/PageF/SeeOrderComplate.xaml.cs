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

namespace TaskMasterMobilePolivanov.PageF
{
    /// <summary>
    /// Логика взаимодействия для SeeOrderComplate.xaml
    /// </summary>
    public partial class SeeOrderComplate : Page
    {
        public SeeOrderComplate()
        {
            InitializeComponent();

            DataGridOrderComplate.ItemsSource = ClassF.databaseClass.DBCl.OrderComplate.ToList();
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
