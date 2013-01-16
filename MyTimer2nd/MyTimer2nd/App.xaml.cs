using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data;
using System.Globalization;

namespace WpfApplication1
{
    /// <summary>  
    /// 実行する処理と、実行可能かどうかの判断を  
    /// delegateで指定可能なコマンドクラス。  
    /// </summary>  
    public class DelegateCommand : ICommand
    {
        private Action<object> _executeAction;
        private Func<object, bool> _canExecuteAction;

        public DelegateCommand(Action<object> executeAction)
            : this(executeAction, o => true)
        { }

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        #region ICommand メンバ

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction(parameter);
        }

        // CommandManagerからイベント発行してもらうようにする  
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        #endregion
    }
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<TimeSpan> timeSpanList = (List<TimeSpan>)value;
            List<string> timeSpanListStr = new List<string>();
            foreach(var ts in timeSpanList)
            {
                timeSpanListStr.Add(ts.ToString("hh\\:mm\\:ss"));
            }
            return timeSpanListStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
  
    public enum TimerState
    {
        //初期状態 カウントダウン中  一時停止   タイムアップ
        Init,       CountDown,     Pause,     TimeUp,
    }

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

    }
}
