using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WpfApplication1.Properties;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        EasyTimer easyTimer;
        TimerState timerStatus = TimerState.Init;

        MainViewModel mainViewModel = new MainViewModel(); 

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = mainViewModel;

            easyTimer = new EasyTimer();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {      
            easyTimer.SetTimerValue(new TimeSpan(0,0,8));
            easyTimer.setCallback((t) => { mainViewModel.RemainTime = t;}); 
            easyTimer.Start();
            MessageBox.Show("START");
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            timerStatus = TimerState.Init;
            easyTimer.Reset();
            mainViewModel.TimerStatus = timerStatus;
        }

        private void StartPauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (timerStatus != TimerState.CountDown)
            {
                timerStatus = TimerState.CountDown;
                easyTimer.SetTimerValue(new TimeSpan(0, 0, 8));
                easyTimer.setCallback((t) => { mainViewModel.RemainTime = t; });
                easyTimer.Start();
            }
            else
            {
                timerStatus = TimerState.Pause;
                easyTimer.Pause();
            }
            mainViewModel.TimerStatus = timerStatus;
        }

    }
}
