using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WpfApplication1
{
    public enum TimerState
    {
        //初期状態 カウントダウン中  一時停止   カウントアップ
        Init,       CountDown,     Pause,     CountUp,
    }

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

    }
}
