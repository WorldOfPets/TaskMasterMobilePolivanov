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
    /// Логика взаимодействия для BiomaterialPage.xaml
    /// </summary>
    /// 

    public partial class BiomaterialPage : Page
    {
        public static int IdPacientP { get; set; } = 0;
        public BiomaterialPage()
        {
            InitializeComponent();

            DataGridUser.ItemsSource = ClassF.databaseClass.DBCl.Pacient.ToList();

            BtnAddUser.ToolTip = "Добавить нового пациента";
        }

        private async void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = (DataGridRow)sender;
            var context = row.DataContext as DataBaseF.Pacient;
            IdPacientP = context.Id;
            await Task.Delay(20);
            TbLastName.Text = context.LastName;
            await Task.Delay(20);
            TbName.Text = context.Name;
            await Task.Delay(20);
            TbOt4.Text = context.MiddleName;
            await Task.Delay(20);
            TbDate.Text = context.Birthdate.Value.ToShortDateString();
            await Task.Delay(20);
            TbEmail.Text = context.Email;
            await Task.Delay(20);
            TbIEN.Text = context.EIN;
            await Task.Delay(20);
            TbNumberPhone.Text = context.PhoneNumber;
            await Task.Delay(20);
            TbPassportSeria.Text = context.PassportS.ToString();
            await Task.Delay(20);
            TbPassportNumber.Text = context.PassportN.ToString();
            await Task.Delay(20);
            TbType.Text = context.InsurancePolicy.InsurancePoliceType.Name;
            await Task.Delay(20);
            TbPoliceNumber.Text = context.InsurancePolicy.Number.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridUser.ItemsSource = ClassF.databaseClass.DBCl.Pacient.Where(x => x.Name.Contains(TbxSearch.Text) || x.LastName.Contains(TbxSearch.Text)).ToList();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            ClassF.FrmPageClass.frmLobarant.Navigate(new AddPacient());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBiomaterial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TbBarcode.Background == Brushes.Transparent && DpDate.SelectedDate != null && IdPacientP != 0)
                {
                    DataBaseF.OrderInfo orderInfo = new DataBaseF.OrderInfo()
                    {
                        Barcode = Convert.ToInt32(TbBarcode.Text),
                        IdPacient = IdPacientP,
                        Date = DpDate.SelectedDate
                    };
                    ClassF.databaseClass.DBCl.OrderInfo.Add(orderInfo);
                    ClassF.databaseClass.DBCl.SaveChanges();
                    MessageBox.Show("Биоматериал успешно принят","Успех");
                    TbBarcode.Text = "";
                }
                else { MessageBox.Show("Не все поля заполнены или не выбран пациент!", "Данные"); }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка", "..::Error::..");
                throw;
            }
        }

        private void TbBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
                TbBarcode.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
                if (TbBarcode.Text.Length == 7 && !TbBarcode.Text.Contains(" "))
                {
                    TbBarcode.Background = Brushes.Transparent;
                    TbBarcode.BorderBrush = Brushes.Transparent;
                    var barcode = ClassF.databaseClass.DBCl.OrderInfo.FirstOrDefault(x => x.Barcode.ToString() == TbBarcode.Text);
                    if (barcode != null)
                    {
                        MessageBoxResult message = MessageBox.Show("Такой код уже существует. Сгенерировать рандомный?", "Повторение данных", MessageBoxButton.YesNo);
                        if (message == MessageBoxResult.Yes)
                        {
                            Random random = new Random();
                            for (int i = 0; i <= 1;)
                            {
                                TbBarcode.Text = random.Next(1000000, 9999999).ToString();
                                var proverka = ClassF.databaseClass.DBCl.OrderInfo.FirstOrDefault(x => x.Barcode.ToString() == TbBarcode.Text);
                                if (proverka == null)
                                {
                                    MessageBox.Show("Код сгенерирована!", "Успех");
                                    i = 2;
                                }
                            }
                        }
                        else { TbBarcode.Background = Brushes.Red; }
                    }
                }
                else
                {
                    TbBarcode.Background = Brushes.White;
                    TbBarcode.BorderBrush = Brushes.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "..::Error::..");
                throw;
            }
            
        }

        public void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            } 
        }
    }
}
