using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace TPChallenge
{
    static class Program
    {
        static void Main(string[] args)
        {
            var sp = new Stopwatch();
            sp.Start();
            var anagram = "poultry outwits ants";
            var anaArr = anagram.ToLower().ToCharArray();
            var words = File.ReadLines("wordlist").Where(item => item.ToLower().ToCharArray().Except(anagram).Count() == 0).ToArray();
            var wl = words.Length;
            var md5 = "4624d200580677270a54ccff86b9610e";
            var al = anagram.Length;          

            string result = "";

            Parallel.For(0, wl, i =>
            {
                MD5 md5Hash = MD5.Create();
                for (int j = 0; j < wl; j++)
                {
                    for (int k = 0; k < wl; k++)
                    {
                        if (i != j && j != k && k != i)
                        {
                            if (words[i].Length + words[j].Length + words[k].Length + 2 == al)
                            {
                                var tmpstr = (words[i] + " " + words[j] + " " + words[k]);
                                ushort oo, uu, ss, tttt;
                                oo = uu = ss = tttt = 0;
                                foreach (var charItem in tmpstr)
                                {
                                    switch (charItem)
                                    {
                                        case 'o':
                                            oo++;
                                            break;
                                        case 'u':
                                            uu++;
                                            break;
                                        case 's':
                                            ss++;
                                            break;
                                        case 't':
                                            tttt++;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (oo == 2 && uu == 2 && ss == 2 && tttt == 4)
                                {
                                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(tmpstr));
                                    StringBuilder sBuilder = new StringBuilder();

                                    for (int l = 0; l < data.Length; l++)
                                    {
                                        sBuilder.Append(data[l].ToString("x2"));
                                    }

                                    var strHash = sBuilder.ToString();

                                    StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                                    if (0 == comparer.Compare(strHash, md5))
                                    {
                                        result = tmpstr;
                                    }
                                }
                            }
                        }
                    }
                }

            });
            sp.Stop();
            Console.WriteLine(sp.Elapsed.ToString());
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
