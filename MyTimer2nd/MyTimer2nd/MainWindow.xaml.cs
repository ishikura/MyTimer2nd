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
        MainViewModel mainViewModel = new MainViewModel(); 

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = mainViewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {      
            //easyTimer.SetTimerValue(new TimeSpan(0,0,8));
            //easyTimer.setCallback((t) => { mainViewModel.RemainTime = t;}); 
            //easyTimer.Start();
            //MessageBox.Show("START");
        }
    }
}
