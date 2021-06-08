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
    /// Логика взаимодействия для InfoUser.xaml
    /// </summary>
    public partial class InfoUser : Page
    {
        public InfoUser()
        {
            InitializeComponent();

            DataUser.ItemsSource = ClassF.databaseClass.DBCl.UserLab.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //var userLabs = ClassF.databaseClass.DBCl.UserLab;
            //foreach (var userLab in userLabs)
            //{
            //    userLab.Attempt = true;
            //}
            //ClassF.databaseClass.DBCl.SaveChanges();
            ClassF.FrmPageClass.frm.Navigate(new LoadPage(new LoginP()));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataUser.ItemsSource = ClassF.databaseClass.DBCl.UserLab.Where(x => x.Login.Contains(TbSearch.Text)).ToList();
        }
    }
}
