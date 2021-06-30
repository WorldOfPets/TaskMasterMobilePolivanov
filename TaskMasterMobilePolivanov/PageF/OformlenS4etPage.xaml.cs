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
        #region ПЕРЕМЕННЫЕ
        public int idOrderComp { get; set; } = 0;
        #endregion
        public OformlenS4etPage()
        {
            InitializeComponent();
            try
            {
                OrderDataGrid.ItemsSource = ClassF.databaseClass.DBCl.OrderComplate.ToList();

                var buhgalter = ClassF.databaseClass.DBCl.UserLab.FirstOrDefault(x => x.Id == LaborantP.LaborantID);
                TbIspolnitel.Text = $"{buhgalter.Name} {buhgalter.LastName} {buhgalter.MiddleName}";

                CmbCompany.SelectedValuePath = "Id";
                CmbCompany.DisplayMemberPath = "Name";
                CmbCompany.ItemsSource = ClassF.databaseClass.DBCl.InsuranceCompany.ToList();
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ИНИЦИАЛИЗАЦИЯ комбобокс, датагрид, исполнитель

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                var bc = new BrushConverter();
                var row = (DataGridRow)sender;
                var context = row.DataContext as DataBaseF.OrderComplate;
                TbC4et.Text = context.FullPrice;
                TbC4et.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
                TbOrder.IsReadOnly = true;
                TbOrder.Text = $"{context.OrderInfo.Barcode} / {context.pacientFullName}";
                TbOrder.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
                idOrderComp = context.Id;
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ДАТАГРИД выбор строки

        private void CmbCompany_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                var bc = new BrushConverter();
                TbCompany.Text = CmbCompany.Text;
                TbCompany.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //КОМБОБОКС при закрытии

        private void TbOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (TbOrder.IsReadOnly == false)
                { 
                    OrderDataGrid.ItemsSource = ClassF.databaseClass.DBCl.OrderComplate.Where(x => x.OrderInfo.Pacient.LastName.Contains(TbOrder.Text) || x.OrderInfo.Pacient.Name.Contains(TbOrder.Text) || x.OrderInfo.Barcode.ToString().Contains(TbOrder.Text)).ToList();
                }
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ТЕКСТБОКС для поиска заказа

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TbOrder.Text = "";
                TbOrder.IsReadOnly = false;
                TbOrder.BorderBrush = Brushes.Red;
                TbC4et.Text = "";
                TbC4et.BorderBrush = Brushes.Red;
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //КНОПКА очистить счёт и заказ

        private void BtnC4et_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var bc = new BrushConverter().ConvertFrom("#FF498C51");
                if (TbC4et.BorderBrush != Brushes.Red && TbCompany.BorderBrush != Brushes.Red && TbOrder.BorderBrush != Brushes.Red)
                {
                    var proverka = ClassF.databaseClass.DBCl.Invoicing.FirstOrDefault(x => x.IdOrderComplate == idOrderComp);
                    if (proverka == null)
                    {
                        DataBaseF.Invoicing invoicing = new DataBaseF.Invoicing()
                        {
                            IdInsuranceCompany = (int)CmbCompany.SelectedValue,
                            IdOrderComplate = idOrderComp,
                            IdUser = LaborantP.LaborantID,
                            Price = Convert.ToDecimal(TbC4et.Text.Replace("$", ""))
                        };
                        ClassF.databaseClass.DBCl.Invoicing.Add(invoicing);
                        ClassF.databaseClass.DBCl.SaveChanges();
                        ClassF.ErrorClass.ToolTipMessage("Счёт успешно сформирован!!!");
                    }
                    else
                    {
                        ClassF.ErrorClass.ToolTipMessage("Такой счет уже сформирован!!!");
                    }
                }
                else 
                {
                    ClassF.ErrorClass.ToolTipMessage("Не все поля заполнены!!!");
                }
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //КНОПКА формирования счёта
    }
}
