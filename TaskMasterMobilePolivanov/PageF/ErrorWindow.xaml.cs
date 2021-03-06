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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TaskMasterMobilePolivanov.PageF
{
    /// <summary>
    /// Логика взаимодействия для ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        #region ПЕРЕМЕННЫЕ
        public DispatcherTimer timer = new DispatcherTimer();
        public int timeS { get; set; } = 0;
        #endregion
        public ErrorWindow(string messegeex)
        {
            InitializeComponent();
            try
            {
                TblErrorMessage.Text = messegeex;
                TimerLoad();
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        }//ИНИЦИАЛИЗАЦИЯ 
        public void TimerLoad()
        {
            try
            {
                timer.Interval = TimeSpan.FromMilliseconds(1);
                timer.Tick += timer_tick;
                timer.Start();
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //МЕТОД ВРЕМЕНИТ тикает каждую милисекунду
        private async void timer_tick(object sender, EventArgs e)
        {
            try
            {
                timeS += 1;
                if (Smettt.Opacity < 0.05) { Smettt.Opacity += 0.0001; }
                
                if (timeS <= 35)
                {
                    TbError111.Opacity -= 0.03;
                    TblErrorMessage.Opacity -= 0.03;
                }
                else
                {
                    for (timeS = 35; timeS >= 1; timeS--)
                    {
                        await Task.Delay(100);
                        TbError111.Opacity += 0.03;
                        TblErrorMessage.Opacity += 0.03;
                    }
                }
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //МЕТОД ВРЕМЕНИ который срабатывает каждую миллесекунду
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                timer.Stop();
                timer = new DispatcherTimer();
                this.Close();
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //КНОПКА ЗАКРЫТИЕ ОКНА
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception ex)
            {
                ClassF.ErrorClass.MessageForUser(ex.Message);
            }
        } //ПЕРМЕЩЕНИЕ ОКНА
    }
}
