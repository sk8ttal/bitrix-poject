using atFrameWork2.BaseFramework.LogTools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace atFrameWork2.BaseFramework
{
    class Waiters
    {
        public static bool WaitForCondition(Func<bool> conditionMethod, int retryInterval_s, int timeout_s, string waitDescription)
        {
            var limitTime = DateTime.Now.AddSeconds(timeout_s);
            LogTools.Log.Info(waitDescription);

            while (true)
            {
                if(DateTime.Now <= limitTime)
                {
                    try
                    {
                        if (conditionMethod.Invoke())
                            return true;
                    }
                    catch (Exception) { }

                    Thread.Sleep(retryInterval_s * 1000);
                }
                else
                {
                    Log.Info("Достигнут таймаут ожидания");
                    break;
                }
            }

            return false;
        }
    }
}
