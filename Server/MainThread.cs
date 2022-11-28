using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Server
{
    public static class MainThread
    {
        private static ConcurrentQueue<Action> Tasks = new();
        private static Thread _thread;
		
        static MainThread()
        {
            _thread = Thread.CurrentThread;
        }

        public static void Run(Action task)
        {
            Tasks.Enqueue(task);
        }

        public static void Pulse()
        {
            while (true)
            {
                var result = Tasks.TryDequeue(out var action);
                if (!result) break;
                action?.Invoke();
            }
        }
		
        private static readonly int _mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        public static bool IsMainThread => System.Threading.Thread.CurrentThread.ManagedThreadId == _mainThreadId;

        public static void Assert()
        {
            Debug.Assert(IsMainThread, "Not in main thread");
        }
		
        public static ThreadAwaiter SwitchToMainThread()
        {
            return new ThreadAwaiter(_thread);
        }

        public static ThreadAwaiter SwitchToThreadPool()
        {
            return new ThreadAwaiter(null);
        }
    }
	
    public struct ThreadAwaiter : INotifyCompletion
    {
        private Thread _thread;

        public ThreadAwaiter(Thread thread)
        {
            _thread = thread;
        }

        public bool IsCompleted => _thread != null ? Thread.CurrentThread == _thread : Thread.CurrentThread.IsThreadPoolThread;
        public void OnCompleted(Action continuation)
        {
            if (_thread == null)
            {
                ThreadPool.QueueUserWorkItem(_ => continuation?.Invoke());
            }
            else
            {
                MainThread.Run(continuation);
            }
        }

        public void GetResult() { }
    }

    public static class ThreadAwaiterExtensions
    {
        public static ThreadAwaiter GetAwaiter(this ThreadAwaiter awaiter) => awaiter;
    }
}