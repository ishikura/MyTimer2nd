using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;


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
        private List<TimeSpan> _timerValueList = new List<TimeSpan>(){new TimeSpan(0, 0, 10)};
        private int _selectedTimeSpanListId = 0;

        EasyTimer easyTimer;

        public MainViewModel()
        {
            easyTimer = new EasyTimer();

            _timerValueList.Add(new TimeSpan(0, 0, 30));
        }

        /// <summary>
        /// タイマーの状態を保持
        /// （同時にボタンの表示およびEnable/Disableも切り替えているが
        /// 　こういうのはDataConverter等を使用するのが良いのだろうか？
        /// 　ICommandのCanExec判定とか？）
        /// </summary>
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
                else if (value == TimerState.Init || value == TimerState.Pause)
                {
                    _startOrPauseBtnStr = "START";
                }
                else if (value == TimerState.TimeUp)
                {
                    _startOrPauseBtnStr = "TIMEUP";
                }

                RaisePropertyChanged("StartOrPauseBtnStr");
                RaisePropertyChanged("isEnableStartPauseBtn");
            }
        }
        public List<TimeSpan> TimerValueList
        {
            get { return _timerValueList; }
            set
            {
                _timerValueList = value;
                RaisePropertyChanged("TimerValueList");
            }
        }
        public int SelectedTimeSpanListId
        {
            get { return _selectedTimeSpanListId; }
            set
            {
                _selectedTimeSpanListId = value;
                RemainTime = _timerValueList[value];
            }
        }

        //ボタン操作可/不可
        public Boolean isEnableStartPauseBtn
        {
            //get { return _timerState != TimerState.Init; }
            get { return _timerState != TimerState.TimeUp; }

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

        /// <summary>
        /// コマンド群
        /// </summary>
        private void StartOrPauseCommandHandler(object param)
        {
            if (_timerState == TimerState.Init || _timerState == TimerState.TimeUp)
            {
                TimerStatus = TimerState.CountDown;
                easyTimer.SetTimerValue(RemainTime);
                easyTimer.setCallback((t) =>
                {
                    RemainTime = t;
                    if (t == TimeSpan.Zero)
                    {
                        TimerStatus = TimerState.TimeUp;
                    }
                });
                easyTimer.Start();
            }
            else if(_timerState == TimerState.CountDown)
            {
                TimerStatus = TimerState.Pause;
                easyTimer.Pause();
            }
            else if (_timerState == TimerState.Pause)
            {
                //残時間再設定
                TimerStatus = TimerState.CountDown;
                easyTimer.SetTimerValue(RemainTime);
                easyTimer.Start();
            }
            RaisePropertyChanged("StartOrPauseBtnStr");
        }

        private ICommand _startOrPauseCommand;
        public ICommand StartOrPauseCommand
        {
            get
            {
                // 作成済みなら、それを返す  
                if (_startOrPauseCommand != null) return _startOrPauseCommand;

                _startOrPauseCommand = new DelegateCommand(
                    this.StartOrPauseCommandHandler,
                    (x)=>(_timerState != TimerState.TimeUp));
                return _startOrPauseCommand;
            }  
        }

        private void ResetCommandHandler(object param)
        {
            if (TimerStatus != TimerState.CountDown)
            {
                TimerStatus = TimerState.Init;
            }
            RemainTime = _timerValueList[_selectedTimeSpanListId];
            easyTimer.Reset();
        }

        private ICommand _resetCommand;
        public ICommand ResetCommand
        {
            get
            {
                // 作成済みなら、それを返す  
                if (_resetCommand != null) return _resetCommand;

                _resetCommand = new DelegateCommand(
                    this.ResetCommandHandler,
                    (x)=>(_timerState != TimerState.Init));
                return _resetCommand;
            }
        }

        /// <summary>
        /// 変更通知
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if (d != null)
                d(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
