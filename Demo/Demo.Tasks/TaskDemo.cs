using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tasks
{
    class TaskDemo
    {
        static void Slow()
        {
            var matrix = new int[10000, 10000];
            var start = System.Environment.TickCount;
            for (int i = 0; i != 10000; ++i)
            {
                for (int j = 0; j != 10000; ++j)
                {
                    //do stuff
                }
            }
            var end = System.Environment.TickCount;
            Console.WriteLine("Slow - Time elapsed: {0}ms", end - start);
        }
        static void Fast()
        {
            var matrix = new int[10000, 10000];
            var start = System.Environment.TickCount;
            Parallel.For(0, 10000, i => {
                for (int j = 0; j != 10000; ++j)
               {
                   //do stuff
               }
            });
            var end = System.Environment.TickCount;
            Console.WriteLine("Fast - Time elapsed: {0}ms", end - start);
        }

        static void Meow()
        {
            var r = new Random();
            var randomInts1 = Enumerable.Repeat(0, 10000000).Select(x => r.Next(0, 9999)).ToArray();
            var randomInts2 = Enumerable.Repeat(0, 10000000).Select(x => r.Next(0, 9999)).ToArray();
            var start = System.Environment.TickCount;
            for (int i = 0; i != 10000000; ++i)
            {
                if ((randomInts1[i] & 1) == 1)
                {
                    ++randomInts1[i];
                }
            }
            for (int i = 0; i != 10000000; ++i)
            {
                if ((randomInts2[i] & 1) == 0)
                {
                    ++randomInts2[i];
                }
            }
            var end = System.Environment.TickCount;
            Console.WriteLine("Meow - Time elapsed: {0}ms", end - start);
        }

        static void Foo()
        {
            var r = new Random();
            var randomInts1 = Enumerable.Repeat(0, 10000000).Select(x => r.Next(0, 9999)).ToArray();
            var randomInts2 = Enumerable.Repeat(0, 10000000).Select(x => r.Next(0, 9999)).ToArray();
            var start = System.Environment.TickCount;
            var t1 = Task.Run(() => {
                for (int i = 0; i != 10000000; ++i)
                {
                    if ((randomInts1[i] & 1) == 1)
                    {
                        ++randomInts1[i];
                    }
                }
            });
            var t2 = Task.Run(() =>
            {
                for (int i = 0; i != 10000000; ++i)
                {
                    if ((randomInts2[i] & 1) == 1)
                    {
                        ++randomInts2[i];
                    }
                }
            });
            t1.Wait();
            t2.Wait();
            var end = System.Environment.TickCount;
            Console.WriteLine("Foo - Time elapsed: {0}ms", end - start);
        }

        static Task<int> Boo()
        {
            var r = new Random();
            var randomInts = Enumerable.Repeat(0, 10000000).Select(x => r.Next(0, 10)).ToArray();
            return Task.Run(() =>
            {
                return randomInts.Aggregate((x, y) => { return x + y; });
            });
        }

        static async void Woof()
        {
            var sum = await Boo();
            Console.WriteLine("Woof - {0}",sum / 10000);
        }

        static void Main(string[] args)
        {
            #region ParallelFor
            Slow();
            Fast();
            #endregion

            #region Tasks
            Meow();
            Foo();
            var task = Boo();
            task = task.ContinueWith(t => { return t.Result / 10000; });                       
            Console.WriteLine("Result is {0}.", task.Result);
            Woof();
            #endregion
            Console.ReadKey();
        }
    }
}
