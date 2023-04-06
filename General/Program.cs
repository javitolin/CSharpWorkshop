using FileSystem;
using System.Diagnostics;
using System;

namespace General
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FinalExercise fe = new FinalExercise();

            Stopwatch stopwatch = Stopwatch.StartNew();

            var found = fe.GetMatches(@"C:\projects\CSharpWorkshop", "^[a-z]+$");
            foreach(var match in found)
            {
                Console.WriteLine(match);
            }
            
            stopwatch.Stop();
            Process process = Process.GetCurrentProcess();
            Console.WriteLine("Memory usage: {0} MB", process.PrivateMemorySize64 / 1024 / 1024);
            Console.WriteLine("Time usage: {0} ms", stopwatch.ElapsedMilliseconds);
            
            stopwatch = Stopwatch.StartNew();

            found = fe.GetMatchesRecursive(@"C:\projects\CSharpWorkshop", "^[a-z]+$");
            foreach (var match in found)
            {
                Console.WriteLine(match);
            }

            stopwatch.Stop();
            process = Process.GetCurrentProcess();
            Console.WriteLine("Memory usage: {0} MB", process.PrivateMemorySize64 / 1024 / 1024);
            Console.WriteLine("Time usage: {0} ms", stopwatch.ElapsedMilliseconds);
        }
    }
}