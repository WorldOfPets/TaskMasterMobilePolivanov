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
using System.Windows.Threading;

namespace TaskMasterMobilePolivanov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public int timeS { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.Width = 800;
            Application.Current.MainWindow.Height = 450;
            
            ClassF.FrmPageClass.frm = FrmMain;
            ClassF.databaseClass.DBCl = new DataBaseF.Session1_TaskMaster_PolivanovEntities();
            TimerLoad();
            //ClassF.FrmPageClass.frm.Navigate(new PageF.LoadPage(new PageF.LoginP()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
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
                MessageBox.Show(ex.Message, "..::Error::..");
                throw;
            }
        }

        private void timer_tick(object sender, EventArgs e)
        {
            try
            {
                timeS += 1;
                BorderMain.Opacity += 0.003;
                if (timeS >= 350)
                {
                    timer.Stop();
                    timer = new DispatcherTimer();
                    StackMain.Visibility = Visibility.Visible;
                    BorderMain.Visibility = Visibility.Collapsed;
                    Application.Current.MainWindow.Background = Brushes.White;
                    ClassF.FrmPageClass.frm.Navigate(new PageF.LoadPage(new PageF.LoginP()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "..::Error::..");
                throw;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this.Width <= 1919)
            {
                this.WindowState = WindowState.Maximized;
            }
            else 
            {
                Application.Current.MainWindow.Width = 800;
                Application.Current.MainWindow.Height = 450;
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
