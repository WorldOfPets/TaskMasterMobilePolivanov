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
        #region ПЕРЕМЕННЫЕ
        public static int IdPacientP { get; set; } = 0;
        #endregion
        public BiomaterialPage()
        {
            InitializeComponent();
            try
            {
                DataGridUser.ItemsSource = ClassF.databaseClass.DBCl.Pacient.ToList();

                BtnAddUser.ToolTip = "Добавить нового пациента";
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ИНИЦИАЛИЗАЦИЯ датагрид и тултип
        private async void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ВЫБОР СТРОКИ ДАТАГРИДА и заполнение данных
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataGridUser.ItemsSource = ClassF.databaseClass.DBCl.Pacient.Where(x => x.Name.Contains(TbxSearch.Text) || x.LastName.Contains(TbxSearch.Text)).ToList();
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ПОИСК в датагриде
        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClassF.FrmPageClass.frmLobarant.Navigate(new AddPacient());
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ПЕРЕХОД на страницу добавления пациента
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
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ДОБАВЛЕНИЕ биоматериала в базу данных
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
                                    ClassF.ErrorClass.ToolTipMessage("Успех! Код сгенерирована!");
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
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //БАРКОД
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
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        }//МЕТОД для проверки на цифры
    }
}
