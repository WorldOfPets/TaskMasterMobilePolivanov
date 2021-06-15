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
        public bool search { get; set; } = true;
        public CreateOrder()
        {
            InitializeComponent();

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                search = false;
                var row = (DataGridRow)sender;
                var context = row.DataContext as DataBaseF.OrderInfo;
                TbPacient.Text = $"{context.Pacient.LastName} {context.Pacient.Name} Код крови:{context.Barcode}";
                idPacient = context.Pacient.Id;
                TbCountLabServ.Text = ClassF.databaseClass.DBCl.PacientLabService.Where(x => x.IdPacient == idPacient).Count().ToString();
                orderId = context.Id;
                TbPacient.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void TbResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var tB = (TextBox)sender;
                tB.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
                tB.BorderBrush = Brushes.Red;
                switch (tB.Text.Length)
                {
                    case 1:
                        tB.Text += ",";
                        tB.Focus();
                        tB.CaretIndex = tB.Text.Length;
                        break;
                    case 8:
                        var bc = new BrushConverter();
                        tB.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void DpDateComplate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CmbStResult_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                TbStResult.Text = CmbStResult.Text;
                if (CmbStResult.Text == "ИСТИНА")
                {
                    stResult = true;
                    TbCountLabServ.Text = stResult.ToString();
                }
                else if(CmbStResult.Text == "ЛОЖЬ") 
                {
                    stResult = false;
                    TbCountLabServ.Text = stResult.ToString();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CmbAnalizator_DropDownClosed(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CmbStatus_DropDownClosed(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CmbUsluga_DropDownClosed(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
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
                        TbCountLabServ.Text = ClassF.databaseClass.DBCl.PacientLabService.Where(x => x.IdPacient == idPacient).Count().ToString();
                        MessageBox.Show("Услуга добавлена", "Успех");
                    }
                    else { MessageBox.Show("Услуга не может быть повторна добавлена", "Потвор данных"); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TbPacient.Text != "" &&
                    TbResult.BorderBrush != Brushes.Red &&
                    TbStResult.BorderBrush != Brushes.Red &&
                    TbAnalizator.BorderBrush != Brushes.Red &&
                    TbStatus.BorderBrush != Brushes.Red)
                {
                    var proverka = ClassF.databaseClass.DBCl.OrderComplate.FirstOrDefault(x => x.IdOreder == orderId);
                    if (proverka == null)
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
                        MessageBox.Show("Заказ успшно оформлен", "");
                    }
                    else { MessageBox.Show("Такой заказ уже сформирован. Но вы можете добавить услугу.",""); }
                }
                else { MessageBox.Show("Не все поля заполнены.","Ошибка ввода."); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void TbPacient_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (search == true)
                { 
                    DataGridOrder.ItemsSource = ClassF.databaseClass.DBCl.OrderInfo.Where(x => x.Pacient.Name.Contains(TbPacient.Text) || x.Pacient.LastName.Contains(TbPacient.Text)).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                TbPacient.Text = "";
                search = true;
                TbPacient.IsReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
