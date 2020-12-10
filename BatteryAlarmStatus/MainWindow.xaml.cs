using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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

namespace BatteryAlarmStatus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {

            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Battery");

            foreach (ManagementObject mo in mos.Get())
            {
                //MessageBox.Show($"Battery Name\t:{mo["Name"].ToString()}");
                
                if (int.Parse(mo["EstimatedChargeRemaining"].ToString()) <= int.Parse(txt_percent.Text))
                {
                    System.Media.SystemSound s = System.Media.SystemSounds.Hand;
                    s.Play();
                }
            }

        }
        bool Is_Started = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Is_Started == false)
            {
                btn_Start.Content = "Tap to Stop Timer ...";
                timer.Start();
                Is_Started = true;
            }
            else
            {
                btn_Start.Content = "Start Timing";
                timer.Stop();
                Is_Started = false;
            }
        }
    }
}
