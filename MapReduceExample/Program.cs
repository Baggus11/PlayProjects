using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace MapReduceExample
{
    class Program
    {
        /// <summary>
        /// The folowing code is from:
        /// https://stackoverflow.com/questions/17188357/read-large-txt-file-multithreaded
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string catalogPath = args[0];
            var inputLines = new BlockingCollection<string>();
            ConcurrentDictionary<int, int> catalog = new ConcurrentDictionary<int, int>();

            var readLines = Task.Factory.StartNew(() =>
            {
                foreach (var line in File.ReadLines(catalogPath))
                    inputLines.Add(line);

                inputLines.CompleteAdding();
            });

            var processLines = Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(inputLines.GetConsumingEnumerable(), line =>
                {
                    string[] lineFields = line.Split('\t');
                    int genomicId = int.Parse(lineFields[3]); //custom logic from example.
                    int taxId = int.Parse(lineFields[0]);
                    catalog.TryAdd(genomicId, taxId);
                });
            });

            Task.WaitAll(readLines, processLines);
        }
    }
}
