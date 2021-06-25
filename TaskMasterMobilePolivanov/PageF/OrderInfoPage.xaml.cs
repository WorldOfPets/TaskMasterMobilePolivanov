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
    /// Логика взаимодействия для OrderInfoPage.xaml
    /// </summary>
    public partial class OrderInfoPage : Page
    {
        public OrderInfoPage(int OrderIDinfo)
        {
            InitializeComponent();

            var orderINFO = ClassF.databaseClass.DBCl.OrderComplate.FirstOrDefault(x => x.Id == OrderIDinfo);
            UslugaDataGrid.ItemsSource = ClassF.databaseClass.DBCl.PacientLabService.Where(x => x.IdPacient == orderINFO.OrderInfo.IdPacient).ToList();

            TbBarcod.Text = orderINFO.OrderInfo.Barcode.ToString();
            TbDataNa4.Text = orderINFO.OrderInfo.Date.Value.ToShortDateString();
            TbDataKon.Text = orderINFO.DateComplate.Value.ToShortDateString();
            TbAnalizator.Text = orderINFO.Analizator.Name;
            TbStatus.Text = orderINFO.Status.Name;
            TbResult.Text = orderINFO.Result.ToString();
            TbAllPrice.Text = orderINFO.FullPrice;

        }
    }
}
