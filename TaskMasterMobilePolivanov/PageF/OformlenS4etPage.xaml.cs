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
    /// Логика взаимодействия для OformlenS4etPage.xaml
    /// </summary>
    public partial class OformlenS4etPage : Page
    {
        public OformlenS4etPage()
        {
            InitializeComponent();

            OrderDataGrid.ItemsSource = ClassF.databaseClass.DBCl.OrderComplate.ToList();

            var buhgalter = ClassF.databaseClass.DBCl.UserLab.FirstOrDefault(x => x.Id == LaborantP.LaborantID);
            TbIspolnitel.Text = $"{buhgalter.Name} {buhgalter.LastName} {buhgalter.MiddleName}";

            CmbCompany.SelectedValuePath = "Id";
            CmbCompany.DisplayMemberPath = "Name";
            CmbCompany.ItemsSource = ClassF.databaseClass.DBCl.InsuranceCompany.ToList();
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            var row = (DataGridRow)sender;
            var context = row.DataContext as DataBaseF.OrderComplate;
            TbC4et.Text = context.FullPrice;
            TbC4et.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            TbOrder.IsReadOnly = true;
            TbOrder.Text = $"{context.OrderInfo.Barcode} / {context.pacientFullName}";
            TbOrder.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
        }

        private void CmbCompany_DropDownClosed(object sender, EventArgs e)
        {
            var bc = new BrushConverter();
            TbCompany.Text = CmbCompany.Text;
            TbCompany.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
        }

        private void TbOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbOrder.IsReadOnly == false)
            { 
                OrderDataGrid.ItemsSource = ClassF.databaseClass.DBCl.OrderComplate.Where(x => x.OrderInfo.Pacient.LastName.Contains(TbOrder.Text) || x.OrderInfo.Pacient.Name.Contains(TbOrder.Text) || x.OrderInfo.Barcode.ToString().Contains(TbOrder.Text)).ToList();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbOrder.IsReadOnly = false;
            TbOrder.BorderBrush = Brushes.Red;
            TbOrder.Text = "";
        }
    }
}
