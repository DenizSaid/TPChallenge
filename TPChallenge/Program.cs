using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace TPChallenge
{
    class Program
    {
        static IEnumerable<string> ReadData()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "TPChallenge.wordlist";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
        
        static void Main(string[] args)
        {
            var len = ReadData().Count();
            var a = 3;
        }
    }
}
