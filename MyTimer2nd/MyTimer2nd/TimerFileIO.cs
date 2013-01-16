using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
    class TimerFileIo
    {
        const string inputValueFileName  = "c:\\TimerValue.txt";
        const string outputValueFileName = "c:\\TimerValue.txt";// "c:\\OutTimerValue.txt";

        public static IEnumerable<TimeSpan> ReadTimerValue()
        {
            if (File.Exists(inputValueFileName))
            {
                try
                {
                    return File.ReadLines(inputValueFileName).Select(c => TimeSpan.Parse(c));
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                    // Formatエラーくらいしか想定してないが…。
                    return new List<TimeSpan>() { new TimeSpan(0, 3, 0) };
                }
            }
            else
            {
                // ファイルが無いときは1分だけ設定。
                return new List<TimeSpan>() { new TimeSpan(0, 0, 10) };
            }
        }

        public static void SaveTimerValue(List<TimeSpan> items)
        {
            if( items.Count == 0 )
            {
                return;
            }

            File.WriteAllLines(outputValueFileName, items.Select(c => c.ToString(@"hh\:mm\:ss")));
        }
    }
}
