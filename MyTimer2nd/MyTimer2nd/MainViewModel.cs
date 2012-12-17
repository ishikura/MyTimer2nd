using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfApplication1
{
    class MainViewModel : INotifyPropertyChanged 
    {
        private TimeSpan _remainTime;

        public string RemainTimeStr
        {
            get { return _remainTime.ToString("c"); }
        }

        public TimeSpan RemainTime
        {
            get { return _remainTime; }
            set
            {
                if (_remainTime != value)
                {
                    _remainTime = value;
                    RaisePropertyChanged("RemainTime");
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
