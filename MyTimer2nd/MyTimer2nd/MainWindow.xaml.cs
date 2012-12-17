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

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string labelRemainTime = "YAHHO";

        enum TimerState
        {
            Init, Counting, Pause, Countup,
        }

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
            //easyTimer.setCallback((t) => { MessageBox.Show(t.ToString()); }); 
            easyTimer.setCallback((t) => { mainViewModel.RemainTime = t;}); 
            easyTimer.Start();
            MessageBox.Show("START");
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            easyTimer.Reset();
        }

    }
}
