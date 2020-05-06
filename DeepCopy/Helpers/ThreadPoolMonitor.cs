using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace _01_DeepCopy.Helpers
{
    class ThreadPoolMonitor : NotifyPropertyChanged
    {
        #region Private Definitions
        int availableWorkerThreads;
        int availableCpThreads;
        int maxWorkerThreads;
        int maxCpThreads;
        #endregion

        public ThreadPoolMonitor()
        {
            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCpThreads);
            MaxWorkerThreads = maxWorkerThreads;
            MaxCpThreads = maxCpThreads;
        }

        public void StartMonitor()
        {
            var task = new Task(() =>
            {
                while (true)
                {
                    ThreadPool.GetAvailableThreads(out int avWorkerThreads, out int avCpThreads);

                    if (avWorkerThreads != AvailableWorkerThreads)
                        AvailableWorkerThreads = avWorkerThreads;

                    if (avCpThreads != AvailableCpThreads)
                        AvailableCpThreads = avCpThreads;
                }
            }, TaskCreationOptions.LongRunning);

            task.Start();
        }

        public int AvailableWorkerThreads
        {
            get => availableWorkerThreads;
            set
            {
                availableWorkerThreads = value;
                OnPropertyChanged();
            }
        }
        public int AvailableCpThreads
        {
            get => availableCpThreads;
            set
            {
                availableCpThreads = value;
                OnPropertyChanged();
            }
        }

        public int MaxWorkerThreads
        {
            get => maxWorkerThreads;
            set
            {
                maxWorkerThreads = value;
                OnPropertyChanged();
            }
        }
        public int MaxCpThreads
        {
            get => maxCpThreads;
            set
            {
                maxCpThreads = value;
                OnPropertyChanged();
            }
        }
    }
}
