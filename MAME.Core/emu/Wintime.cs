using System.Diagnostics;
using System.Threading;

namespace MAME.Core
{
    public class Wintime
    {
        //[DllImport("kernel32.dll ")]
        //public static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);
        //[DllImport("kernel32.dll")]
        //private static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);

        #region 跨平台等效实现
        public static Stopwatch _stopwatch = Stopwatch.StartNew();
        private static long _lastReportedCount = 0;

        public static bool QueryPerformanceCounter(ref long lpPerformanceCount)
        {
            lpPerformanceCount = _stopwatch.ElapsedTicks;
            return true;
        }

        public static bool QueryPerformanceFrequency(ref long PerformanceFrequency)
        {
            PerformanceFrequency = Stopwatch.Frequency;
            return true;
        }
        #endregion


        public static long ticks_per_second;
        public static void wintime_init()
        {
            long b = 0;
            QueryPerformanceFrequency(ref b);
            ticks_per_second = b;
        }
        public static long osd_ticks()
        {
            long a = 0;
            QueryPerformanceCounter(ref a);
            return a;
        }
        public static void osd_sleep(long duration)
        {
            int msec;
            msec = (int)(duration * 1000 / ticks_per_second);
            if (msec >= 2)
            {
                msec -= 2;
                Thread.Sleep(msec);
            }
        }
    }
}