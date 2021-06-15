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
    /// Логика взаимодействия для LaborantP.xaml
    /// </summary>
    public partial class LaborantP : Page
    {
        public static int LaborantID { get; set; }
        public LaborantP(int ID)
        {
            InitializeComponent();

            ClassF.FrmPageClass.frmLobarant = FrmLaborant;

            var UserInSys = ClassF.databaseClass.DBCl.UserLab.FirstOrDefault(x => x.Id == ID);
            TbLastName.Text = UserInSys.LastName;
            TbName.Text = UserInSys.Name;
            TbRole.Text = UserInSys.Role.Name;
            var now = DateTime.Now;
            decimal age = Convert.ToDecimal(now.Year - UserInSys.Birthdate.Value.Year - 1 + ((now.Month > UserInSys.Birthdate.Value.Month || now.Month == UserInSys.Birthdate.Value.Month && now.Day >= UserInSys.Birthdate.Value.Day) ? 1 : 0));
            
            if (age >= 5 && age <= 20) { TbAge.Text = $"{age} лет"; }
            else if ((age % 10) == 1) { TbAge.Text = $"{age} год"; }
            else if ((age % 10) >= 2 && (age % 10) <= 4) { TbAge.Text = $"{age} года"; }
            else if ((age % 10) >= 5) { TbAge.Text = $"{age} лет"; }

            var image1 = new Uri("/ResF/laborant_1.jpeg", UriKind.Relative);
            var image2 = new Uri("/ResF/laborant_2.png", UriKind.Relative);
            var image3 = new Uri("/ResF/laborant_is.jpeg", UriKind.Relative);
            
            if (UserInSys.IdRole == 1) { Img.Source = new BitmapImage(image1); BtnC4et.Visibility = Visibility.Collapsed; BtnSeeOt4et.Visibility = Visibility.Collapsed; BtnWorkWith.Visibility = Visibility.Collapsed; }
            else if (UserInSys.IdRole == 2) { Img.Source = new BitmapImage(image2); BtnC4et.Visibility = Visibility.Collapsed; BtnSeeOt4et.Visibility = Visibility.Collapsed; }
            else if (UserInSys.IdRole == 3) { Img.Source = new BitmapImage(image3); BtnBiomaterial.Visibility = Visibility.Collapsed; BtnSformirovatOt4et.Visibility = Visibility.Collapsed; BtnWorkWith.Visibility = Visibility.Collapsed; }

            LaborantID = ID;
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassF.FrmPageClass.frm.Navigate(new LoadPage(new LoginP()));
        }

        private void BtnBiomaterial_Click(object sender, RoutedEventArgs e)
        {
            ClassF.FrmPageClass.frmLobarant.Navigate(new BiomaterialPage());
        }

        private void BtnSformirovatOt4et_Click(object sender, RoutedEventArgs e)
        {
            ClassF.FrmPageClass.frmLobarant.Navigate(new CreateOrder());
        }

        private void BtnSeeOt4et_Click(object sender, RoutedEventArgs e)
        {
            ClassF.FrmPageClass.frmLobarant.Navigate(new SeeOrderComplate());
        }
    }
}
