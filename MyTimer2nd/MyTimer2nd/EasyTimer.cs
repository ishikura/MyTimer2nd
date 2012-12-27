using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows;

namespace WpfApplication1
{
    class EasyTimer
    {
        private static int _intervalMsec= 100;
        private static TimeSpan initialTimerValue = new TimeSpan(0, 3, 0);
        private static TimeSpan timerRemainingValue = initialTimerValue;
        private static DateTime endTime;

        private static bool isPause = false;

        private static Task countdownTask;

        //呼び出し元に残り時間を返す
        public static Action<TimeSpan> CountdownCallback;

        //public static Action<TimeSpan> CountdownCallback;
 
        public void setCallback(Action<TimeSpan> act)
        {
            CountdownCallback = act;
        }
            

        // 10ミリ秒～60秒の任意の刻み
        public int IntervalMsec
        {
            get { return _intervalMsec; }
        }
        public bool SetInterval(int intervalMsec)
        {
            if (intervalMsec >= 10 && intervalMsec <= (60 * 1000))
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
                timerRemainingValue = timerValue;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// カウントダウンSTARTorRESTART
        /// </summary>
        public void Start()
        {
            if (isPause)
            {
                endTime = DateTime.Now + timerRemainingValue;
                isPause = false;
            }
            else
            {
                if (CountdownCallback == null)
                {
                    // TODO: コールバック未設定の場合の挙動
                    return;
                }
                //終了時刻を確定(RESTART時はPAUSE時の残り時間を使用)
                endTime = DateTime.Now + timerRemainingValue;
                
                isPause = false;

                if (countdownTask == null)
                {
                    countdownTask = new Task(countdownTaskImp);
                    countdownTask.Start();
                }
                else if (countdownTask.IsCompleted)
                {
                    countdownTask.Dispose();
                    countdownTask = new Task(countdownTaskImp);
                    countdownTask.Start();
                }
            }
        }
        /// <summary>
        /// 一時停止（残り時間そのまま）
        /// </summary>
        public void Pause()
        {
            isPause = true;

            //現在の残り時間を保持
            timerRemainingValue = endTime - DateTime.Now;
            
            //countdownTask.;
            //tokenを使用したキャンセル処理は例外を使用する必要があるので、別の宿題とする。
            //フラグだらけとどっちがいいのか…？
        }

        /// <summary>
        /// RESET（終了時刻を変更してカウントダウンは継続）
        /// </summary>
        public void Reset()
        {
            endTime = DateTime.Now + initialTimerValue;
        }

        //カウントダウン
        private static void countdownTaskImp()
        {
            while (true)
            {
                if (!isPause)
                {
                    if ((endTime - DateTime.Now) > TimeSpan.Zero)
                    {
                        //残り時間を返す
                        CountdownCallback(endTime - DateTime.Now);
                    }
                    else
                    {
                        //TIMEUP!（…のはず）
                        CountdownCallback(TimeSpan.Zero);
                        break;
                    }
                }
                Thread.Sleep(_intervalMsec);
            }
        }
    }
}
