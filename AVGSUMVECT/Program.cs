namespace AVGSUMVECT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ManageVector vector = new ManageVector();

            for (int i = 0; i < 100000000; i++)
            {
                vector.Add_Element_To_Vector(i);
            }


            int numThreads = 10;
            Console.WriteLine($"Parallel Sum with {numThreads} threads: {vector.ParallelSum(numThreads)}");


            var watchSingle = System.Diagnostics.Stopwatch.StartNew();
            int singleThreadSum = vector.ParallelSum(1);
            watchSingle.Stop();
            Console.WriteLine($"Single Thread Sum: {singleThreadSum}, Time: {watchSingle.ElapsedMilliseconds} ms");

            var watchMulti = System.Diagnostics.Stopwatch.StartNew();
            int multiThreadSum = vector.ParallelSum(numThreads);
            watchMulti.Stop();
            Console.WriteLine($"Multi Thread Sum: {multiThreadSum}, Time: {watchMulti.ElapsedMilliseconds} ms");

            double speedup = (double)watchSingle.ElapsedMilliseconds / (double)watchMulti.ElapsedMilliseconds;
            Console.WriteLine($"Speedup: {speedup}");
        }
    }
}
