using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskMasterMobilePolivanov.PageF
{
    /// <summary>
    /// Логика взаимодействия для LoginP.xaml
    /// </summary>
    public partial class LoginP : Page
    {
        public LoginP()
        {
            InitializeComponent();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            var userLogin = ClassF.databaseClass.DBCl.UserLab.FirstOrDefault(x => x.Login == TbLogin.Text);
            if (userLogin != null)
            {
                if (userLogin.Password == PbPassword.Password)
                {
                    userLogin.Attempt = true;
                    userLogin.DataEnter = DateTime.Now;
                    ClassF.databaseClass.DBCl.SaveChanges();
                    ClassF.FrmPageClass.frm.Navigate(new PageF.LoadPage(new PageF.LaborantP(userLogin.Id)));
                }
                else 
                {
                    MessageBox.Show("Неверный логин или пароль","..::Error::..");
                    userLogin.Attempt = false;
                    userLogin.DataEnter = DateTime.Now;
                    ClassF.databaseClass.DBCl.SaveChanges();
                }
            }
            else 
            {
                MessageBox.Show("Пользователь не найден.", "..::Error::..");
            }
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            ClassF.FrmPageClass.frm.Navigate(new PageF.LoadPage(new PageF.InfoUser()));
        }
    }
}
