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
            var rowSelect = (DataGridRow)sender;
            var contextSelect = rowSelect.DataContext as DataBaseF.OrderComplate;
            TbResultSearch.Text = $"№{contextSelect.Id} Пациент: {contextSelect.OrderInfo.Pacient.LastName} {contextSelect.OrderInfo.Pacient.Name[0]}. Лаборант: {contextSelect.UserLab.LastName} {contextSelect.UserLab.Name[0]}.";
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridOrderComplate.ItemsSource = ClassF.databaseClass.DBCl.OrderComplate.Where(x => x.OrderInfo.Pacient.LastName.Contains(TbSearch.Text) || x.OrderInfo.Pacient.Name.Contains(TbSearch.Text) || x.UserLab.LastName.Contains(TbSearch.Text) || x.UserLab.Name.Contains(TbSearch.Text)).ToList();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
