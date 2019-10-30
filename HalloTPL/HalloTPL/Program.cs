using System;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello TPL!");

            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);
            //Parallel.For(0, 1000000000, i =>
            //{
            //    if (i % 10000 == 0)
            //        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            //});

            Task t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(800);
                Console.WriteLine("T1 fertig");
            });

            Task<long> t2 = new Task<long>(() =>
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(600);
                Console.WriteLine("T2 fertig");
                throw new ExecutionEngineException();
                return 23456734567845678;
            });
            //immer
            var tc3 = t2.ContinueWith(tx =>
            {
                Console.WriteLine($"T2 fertig");
            }, TaskContinuationOptions.None);

            //nur wenn OK
            var tc = t2.ContinueWith(tx =>
            {
                Console.WriteLine($"Result ist  {tx.Result}");
            },TaskContinuationOptions.OnlyOnRanToCompletion);

            //nur bei exception
            var tc2 = t2.ContinueWith(tx =>
            {
                Console.WriteLine($"Fehler: {tx.Exception.Message}");
            }, TaskContinuationOptions.OnlyOnFaulted);



            t1.Start();
            t2.Start();



            //Console.WriteLine($"Result of T2: {t2.Result}");

            Task.WaitAll(t1, tc2);

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
