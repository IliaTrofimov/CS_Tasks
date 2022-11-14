using System;
using System.Diagnostics;
using System.Threading;

namespace task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //FailProcess_StackOverflow();
                //FailProcess_ThreadAbort();
                //FailProcess_ProcessKill();
                //FailProcess_Normal();
            }
            catch { }

            Console.WriteLine("Failed to fail process!");
            Console.ReadKey();
        }


        /// <summary>
        /// Stack Overflow не будет перехвачен, т.к. у приложения не хватит места на стеке для его обработки, ведь всё место мы уже истратили.
        /// Программа будет завершена преждевременно.
        /// </summary>
        static void FailProcess_StackOverflow(long count = 0)
        {
            FailProcess_StackOverflow(count + 1);
            Console.WriteLine($"Done {count}");
        }


        /// <summary>
        /// Thread.Abort() выбросит исключение ThreadAbortException, которое будет отловлено в Main,
        /// однако оно будет автоматически выброшено повторно системой в конце блока catch, из-за чего процесс всё равно завершится преждевременно. 
        /// </summary>
        /// <remarks>Не работает в новых версиях С#, но в .Net Framework 4.8 работает</remarks>
        static void FailProcess_ThreadAbort()
        {
            Console.WriteLine($"FailProcess() was called");
            Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// Завершаем текущий процесс
        /// </summary>
        static void FailProcess_ProcessKill()
        {
            Console.WriteLine($"FailProcess_ProcessKill() was called");
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Все другие исключения будут обработаны в блоке try-catch в Main
        /// </summary>
        static void FailProcess_Normal()
        {
            int x = 0;
            int y = 1;
            int z = y / x;
        }
    }
}
