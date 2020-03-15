using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2003._1CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Is: 3533027414857 a Prime Number: "+IsPrime(3533027414857));
            Console.WriteLine();
            Console.WriteLine("TIME WITHOUT MEMOIZED FUNCTION");
            Timer(() => IsPrime(3533027414857));
            Timer(() => IsPrime(3533027414857));
            Timer(() => IsPrime(3533027414857));
            // Use Memoized function
            Console.WriteLine();
            Console.WriteLine("TIME USING MEMOIZED FUNCTION");
            var memoizedIsPrime = MemoizeClass.Memoize<long,bool>(IsPrime);
            Timer(()=>memoizedIsPrime(3533027414857));
            Timer(() => memoizedIsPrime(3533027414857));
            Timer(() => memoizedIsPrime(3533027414857));

            // MergeSort
            List<int> UnsortedCollection = new List<int> { 34, 5, 4, 12, 56, 78, 90, 11 };
            //var sortedArray=MergeSort.DivideMergeSort(UnsortedCollection);
            Console.WriteLine("Binary Search: " + BinarySearch(UnsortedCollection, 4));
        }

        public static bool IsPrime(long n)
        {
            bool isPrime = true; 
            if (n == 2)
            {
                return isPrime;
            }
            else
            {
                for(int i=2; i <= Math.Sqrt(n); i++)
                {
                    if (n % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                return isPrime;
            }

        }

        static void Timer(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            Console.WriteLine("Time taken: "+sw.Elapsed);
        }

        // Binary Search
        public static int BinarySearch(List<int>UnsortedCollection, int value)
        {
            int mid = 0, first = 0, last = UnsortedCollection.Count - 1;
            int foundValue = -1;
            // Need to sort the collection first. Binary search works with sorted collection
            var sortedCollection= MergeSort.DivideMergeSort(UnsortedCollection);
            // If the search value is less than the smallest in the collection
            if (value< sortedCollection[0])
            {
                return foundValue;
            }

            while (first <= last)
            {
                mid = (first + last) / 2;
                if (value == sortedCollection[mid])
                {
                    foundValue = sortedCollection[mid];
                    break;
                }
                else
                {
                    if (value < sortedCollection[mid])
                    {
                        last = mid - 1;
                    }
                    if (value > sortedCollection[mid])
                    {
                        first = mid + 1;
                    }
                }
            }
            return foundValue;
        }
    }

    public static class MemoizeClass
    {
        public static Func<T,TResult> Memoize<T,TResult>(this Func<T, TResult> functionParameter)
        {
            var dictionaryCache = new ConcurrentDictionary<T, TResult>();
            return result => dictionaryCache.GetOrAdd(result,functionParameter);
        }
    }

    public static class MergeSort
    {
        public static List<int> DivideMergeSort(List<int> UnsortedCollection)
        {
            int Size = UnsortedCollection.Count;
            int Mid = Size / 2;

            List<int> Left = new List<int>();
            List<int> Right = new List<int>();

            if (Size > 1)
            {
                for(int i = 0; i < Mid; i++)
                {
                    Left.Add(UnsortedCollection[i]);
                }

                for (int j = Mid; j < Size; j++)
                {
                    Right.Add(UnsortedCollection[j]);
                }

                Left = DivideMergeSort(Left);
                Right = DivideMergeSort(Right);
                return MergeAndSort(Left,Right);
            }
            else
            {
                return UnsortedCollection;
            }
        }

        static List<int> MergeAndSort(List<int>Left, List<int> Right)
        {
            List<int> Result = new List<int>(); 
            while(Left.Count>0 || Right.Count>0)
            {
                if(Left.Count >0 && Right.Count > 0)
                {
                    if (Left.First() < Right.First())
                    {
                        Result.Add(Left.First());
                        Left.Remove(Left.First());
                    }
                    else
                    {
                        Result.Add(Right.First());
                        Right.Remove(Right.First());
                    }
                }else if (Left.Count > 0)
                {
                    Result.Add(Left.First());
                    Left.Remove(Left.First());
                }else if (Right.Count>0)
                {
                    Result.Add(Right.First());
                    Right.Remove(Right.First());
                }
            }
            return Result;
        }
    }
}
  
