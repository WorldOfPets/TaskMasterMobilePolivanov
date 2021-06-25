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

        private async void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text != "" && (PbPassword.Password != "" || TbPasswor.Text != ""))
            {
                var userLogin = ClassF.databaseClass.DBCl.UserLab.FirstOrDefault(x => x.Login == TbLogin.Text);
                if (userLogin != null)
                {
                    if (userLogin.Password == PbPassword.Password || userLogin.Password == TbPasswor.Text)
                    {
                        userLogin.Attempt = true;
                        userLogin.DataEnter = DateTime.Now;
                        ClassF.databaseClass.DBCl.SaveChanges();
                        ClassF.FrmPageClass.frm.Navigate(new PageF.LoadPage(new PageF.LaborantP(userLogin.Id)));
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль", "..::Error::..");
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
            else
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = "Не все поля заполнены!!!";
                toolTip.IsOpen = true;
                await Task.Delay(3000);
                toolTip.IsOpen = false;
            }
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            ClassF.FrmPageClass.frm.Navigate(new PageF.LoadPage(new PageF.InfoUser()));
        }

        private void BtnPassShow_Click(object sender, RoutedEventArgs e)
        {
            if (TbPasswor.Visibility == Visibility.Collapsed)
            {
                BtnPassShow.ToolTip = "Скрыть пароль";
                TbPasswor.Text = PbPassword.Password;
                TbPasswor.Visibility = Visibility.Visible;
                PbPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnPassShow.ToolTip = "Показать пароль";
                PbPassword.Password = TbPasswor.Text;
                TbPasswor.Visibility = Visibility.Collapsed;
                PbPassword.Visibility = Visibility.Visible;
            }
        }
    }
}
