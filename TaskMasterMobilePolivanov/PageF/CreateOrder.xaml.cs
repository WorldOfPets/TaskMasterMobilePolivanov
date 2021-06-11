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
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Page
    {
        public bool stResult { get; set; }
        public int idPacient { get; set; } = 0;
        public int Code { get; set; }
        public int orderId { get; set; }
        public CreateOrder()
        {
            InitializeComponent();

            var Laborant = ClassF.databaseClass.DBCl.UserLab.FirstOrDefault(x => x.Id == LaborantP.LaborantID);
            TbIspolnitel.Text = $"{Laborant.LastName} {Laborant.Name}";

            DataGridOrder.ItemsSource = ClassF.databaseClass.DBCl.OrderInfo.ToList();

            TbData.Text = DateTime.Today.ToShortDateString();
            DpDateComplate.SelectedDate = DateTime.Now.Date;

            CmbAnalizator.SelectedValuePath = "Id";
            CmbAnalizator.DisplayMemberPath = "Name";
            CmbAnalizator.ItemsSource = ClassF.databaseClass.DBCl.Analizator.ToList();

            CmbStaus.SelectedValuePath = "Id";
            CmbStaus.DisplayMemberPath = "Name";
            CmbStaus.ItemsSource = ClassF.databaseClass.DBCl.Status.ToList();

            CmbUsluga.SelectedValuePath = "Id";
            CmbUsluga.DisplayMemberPath = "Name";
            CmbUsluga.ItemsSource = ClassF.databaseClass.DBCl.LabServices.ToList();

            CmbStResult.Items.Add("ИСТИНА");
            CmbStResult.Items.Add("ЛОЖЬ");
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = (DataGridRow)sender;
            var context = row.DataContext as DataBaseF.OrderInfo;
            TbPacient.Text = $"{context.Pacient.LastName} {context.Pacient.Name} Код крови:{context.Barcode}";
            idPacient = context.Pacient.Id;
            orderId = context.Id;
        }

        private void TbResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tB = (TextBox)sender;
            tB.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
            tB.BorderBrush = Brushes.Red;
            switch (tB.Text.Length)
            {
                case 1:
                    tB.Text += ".";
                    tB.Focus();
                    tB.CaretIndex = tB.Text.Length;
                    break;
                case 8:
                    var bc = new BrushConverter();
                    tB.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
                    break;
            }
        }
        public void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void DpDateComplate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            TbData.Text = DpDateComplate.SelectedDate.Value.ToShortDateString();
            if (DpDateComplate.SelectedDate <= DateTime.Today)
            {
                var bc = new BrushConverter();
                TbData.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            }
            else
            {
                TbData.BorderBrush = Brushes.Red;
            }
        }

        private void CmbStResult_DropDownClosed(object sender, EventArgs e)
        {
            TbStResult.Text = CmbStResult.Text;
            if (CmbStResult.Text == "ИСТИНА")
            {
                stResult = true;
            }
            else if(CmbStResult.Text == "ЛОЖЬ") 
            {
                stResult = false;
            }
            if (CmbStResult.Text != "")
            {
                var bc = new BrushConverter();
                TbStResult.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            }
            else
            {
                TbStResult.BorderBrush = Brushes.Red;
            }
        }

        private void CmbAnalizator_DropDownClosed(object sender, EventArgs e)
        {
            TbAnalizator.Text = CmbAnalizator.Text;
            if (CmbAnalizator.Text != "")
            {
                var bc = new BrushConverter();
                TbAnalizator.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            }
            else
            {
                TbAnalizator.BorderBrush = Brushes.Red;
            }
        }

        private void CmbStatus_DropDownClosed(object sender, EventArgs e)
        {
            TbStatus.Text = CmbStaus.Text;
            if (CmbStaus.Text != "")
            {
                var bc = new BrushConverter();
                TbStatus.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            }
            else
            {
                TbStatus.BorderBrush = Brushes.Red;
            }
        }

        private void CmbUsluga_DropDownClosed(object sender, EventArgs e)
        {
            TbUsluga.Text = CmbUsluga.Text;
            if (CmbUsluga.Text != "")
            {
                var bc = new BrushConverter();
                TbUsluga.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            }
            else
            {
                TbUsluga.BorderBrush = Brushes.Red;
            }
            Code = ClassF.databaseClass.DBCl.LabServices.FirstOrDefault(x => x.Id == (int)CmbUsluga.SelectedValue).Code;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CmbUsluga.Text != "" && idPacient != 0)
            {
                var codeLab = ClassF.databaseClass.DBCl.LabServices.FirstOrDefault(x => x.Id == (int)CmbUsluga.SelectedValue).Code;
                var proverka = ClassF.databaseClass.DBCl.PacientLabService.FirstOrDefault(x => x.IdPacient == idPacient && x.IdLabServices == codeLab);
                if (proverka == null)
                {
                    DataBaseF.PacientLabService pacientLabServieces = new DataBaseF.PacientLabService()
                    {
                        IdPacient = idPacient,
                        IdLabServices = codeLab
                    };
                    ClassF.databaseClass.DBCl.PacientLabService.Add(pacientLabServieces);
                    ClassF.databaseClass.DBCl.SaveChanges();
                    MessageBox.Show("Услуга добавлена", "Успех");
                }
                else { MessageBox.Show("Услуга не может быть повторна добавлена", "Потвор данных"); }
            }
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            DataBaseF.OrderComplate orderComplate = new DataBaseF.OrderComplate()
            {
                IdOreder = orderId,
                IdUser = LaborantP.LaborantID,
                DateComplate = DpDateComplate.SelectedDate,
                IdAnalizator = (int)CmbAnalizator.SelectedValue,
                IdStatus = (int)CmbStaus.SelectedValue,
                Accepted = stResult,
                Result = Convert.ToDecimal(TbResult.Text)
            };
            ClassF.databaseClass.DBCl.OrderComplate.Add(orderComplate);
            ClassF.databaseClass.DBCl.SaveChanges();
            MessageBox.Show("Заказ успшно оформлен","");
        }
    }
}
