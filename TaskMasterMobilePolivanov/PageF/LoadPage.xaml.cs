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

namespace TaskMasterMobilePolivanov.PageF
{
    /// <summary>
    /// Логика взаимодействия для LoadPage.xaml
    /// </summary>
    public partial class LoadPage : Page
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public int timeS { get; set; }
        public Page page1;
        public LoadPage(Page page)
        {
            InitializeComponent();
            page1 = page;
            timeS = 0;
            TimerLoad();
        }

        public void TimerLoad()
        {
            try
            {
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_tick;
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"..::Error::..");
                throw;
            }
        }

        private void timer_tick(object sender, EventArgs e)
        {
            try
            {
                timeS += 1;
                if (timeS >= 2)
                {
                    timer.Stop();
                    timer = new DispatcherTimer();
                    ClassF.FrmPageClass.frm.Navigate(page1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "..::Error::..");
                throw;
            }
        }
    }
}
