using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
