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
        public static int IdPacient{ get; set; }
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
            IdPacient = context.Id;
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

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBiomaterial_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
