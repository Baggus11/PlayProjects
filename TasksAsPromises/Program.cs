using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TasksAsPromises
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running...");

            var r1 = 1.Map(H)
                .Map(G)
                .Map(F).Result;

            //var r2 = Task.Factory.StartNew(() => CreateExcel())
            //.Map(_ => InjectCells())
            //.Map(_ => RefreshExcel())
            //.Map(_ => ExtractTables());

            Console.WriteLine(r1);

            Console.WriteLine("Done.");
            Console.ReadLine();
        }


        public static async Task<int> F(int x) => await Task.Run(() =>
        {
            Thread.Sleep(50);
            Debug.WriteLine($"F() done...");
            return Task.FromResult(x + 1);
        });
        public static async Task<int> G(int x) => await Task.Run(() =>
        {
            Thread.Sleep(1000);
            Debug.WriteLine($"G() done...");
            return Task.FromResult(x * 2);
        });
        public static async Task<int> H(int x) => await Task.Run(() =>
        {
            Thread.Sleep(100);
            Debug.WriteLine($"H() done...");

            return Task.FromResult(x + 3);
        });

        private static async Task CreateExcel()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(50);
                Debug.WriteLine("excel created...");
            });
        }

        private static async Task InjectCells()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(250);
                Debug.WriteLine("cells injected...");
            });


        }

        private static async Task RefreshExcel()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
                Debug.WriteLine("workbook refreshed...");
            });
        }

        private static async Task ExtractTables()
        {
            //await Task.Run(() =>
            //{
            //    Thread.Sleep(250);
            //    Debug.WriteLine("tables extracted...");
            //});

            await Task.Run(() =>
            {
                Thread.Sleep(100);
                Debug.WriteLine("tables extracted...");
            });
        }
    }

    //Source: https://davefancher.com/2015/12/11/functional-c-chaining-async-methods/
    public static class TaskChainingExtensions
    {
        public static async Task<R> Map<S, R>(this Task<S> task, Func<S, Task<R>> func) => await func(await task);
        public static async Task<R> Map<S, R>(this Task<S> task, Func<S, R> func) => func(await task);
        public static async Task<R> Map<S, R>(this S source, Func<S, Task<R>> func) => await func(source);
    }
}
