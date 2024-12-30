using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVGSUMVECT
{
    public class ManageVector
    {
        private List<int> vector = new List<int>();


        public void Add_Element_To_Vector(int value)
        {
            vector.Add(value);
        }


        public int ParallelSum(int numThreads)
        {
            if (vector.Count == 0) return 0;

            int sum = 0;
            int partSize = vector.Count / numThreads;

            Task<int>[] tasks = new Task<int>[numThreads];


            for (int t = 0; t < numThreads; t++)
            {
                int start = t * partSize;
                int end = (t == numThreads - 1) ? vector.Count : start + partSize;


                tasks[t] = Task.Run(() =>
                {
                    int localSum = 0;
                    for (int i = start; i < end; i++)
                    {
                        localSum += vector[i];
                    }
                    return localSum;
                });
            }


            Task.WaitAll(tasks);

            foreach (var task in tasks)
            {
                sum += task.Result;
            }
            return sum / vector.Count;
        }
    }
}
