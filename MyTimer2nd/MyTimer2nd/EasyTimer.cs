using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class EasyTimer
    {
        private int _intervalMsec= 1000;
        private TimeSpan initialTimerValue = new TimeSpan(0, 3, 0);

        public event Action<TimeSpan> CountDownCallback; 

        // 1ミリ秒～60秒の任意の刻み
        public int IntervalMsec
        {
            get { return _intervalMsec; }
        }
        public bool SetInterval(int intervalMsec)
        {
            if (intervalMsec > 0 && intervalMsec <= (60 * 1000))
            {
                _intervalMsec = intervalMsec;
                return true;
            }
            else
            {
                return false;
            }
        }
        // 最大で99:59:59まで（主に画面表示の都合による）（ここでnew使うのあり？）
        public bool SetTimerValue( TimeSpan timerValue )
        {
            if (timerValue > TimeSpan.Zero && timerValue <= new TimeSpan(99, 59, 59))
            {
                initialTimerValue = timerValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Start()
        {
            if(CountDownCallback == null)
            {
                // TODO: コールバック未設定の場合の挙動
                return;
            }
            Task.Factory.StartNew(() => { });
        }
        public void Pause()
        {
        }
        public void Reset()
        {
        }
    }
}
