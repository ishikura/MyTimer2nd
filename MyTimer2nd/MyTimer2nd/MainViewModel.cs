using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfApplication1
{
    /// <summary>
    /// メイン画面用Viewモデル（残時間…）
    /// </summary>
    class MainViewModel : INotifyPropertyChanged 
    {
        private TimeSpan _remainTime;
        private TimerState _timerState = TimerState.Init;
        private string _startOrPauseBtnStr = "START";

        //タイマー状態
        public TimerState TimerStatus
        {
            get { return _timerState; }
            set
            {
                _timerState = value;
                if (value == TimerState.CountDown)
                {
                    _startOrPauseBtnStr = "PAUSE";
                }
                else
                {
                    _startOrPauseBtnStr = "START";
                }
                RaisePropertyChanged("StartOrPauseBtnStr");
            }
        }
        
        //STARTorPAUSEボタン表記
        public string StartOrPauseBtnStr
        {
            get { return _startOrPauseBtnStr; }
        }

        //残時間
        public string RemainTimeStr
        {
            get { return _remainTime.ToString("hh\\:mm\\:ss\\.fff"); }
        }
        public TimeSpan RemainTime
        {
            get { return _remainTime; }
            set
            {
                if (_remainTime != value)
                {
                    _remainTime = value;
                    //RaisePropertyChanged("RemainTime");
                    RaisePropertyChanged("RemainTimeStr");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if (d != null)
                d(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
