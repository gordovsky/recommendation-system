using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace recommendation_system
{
    class Program
    {
        static void Main(string[] args)
        {


            //var reddit = new Reddit(@"C:\Users\col403\Desktop\reddit.csv");
            //reddit.ReadData();
            //reddit.RankNews();
            //reddit.Print();

            Task3 task = new Task3(@"C:\Users\Admin\Desktop\ml-latest-small\movies.csv",
                                   @"C:\Users\Admin\Desktop\ml-latest-small\ratings.csv");

            //var dm = task.DampedMeanCollection();
            //var dampedTop10 = dm.OrderByDescending(x => x.Value).Take(10);
            //foreach (var d in dampedTop10)
            //{
            //    string similar = task.GetMovieName(task.GetMostSimilar(d.Key));
            //    Console.WriteLine("{0} damped mean: {1}. Similar to {2}", task.GetMovieName(d.Key), d.Value, similar);
            //}

            //var fdsx = task.SimilarityValue(4324, 4234);

            //string similarM = task.GetMovieName(task.GetMostSimilar(1732));
            ConsoleKeyInfo input;
            do
            {
                Console.WriteLine("For what movie do you need recommendation, my master?");
                Int64 id = Int64.Parse(Console.ReadLine());
                try
                {
                    string similarM = task.GetMovieName(task.GetMostSimilar(id));
                    Console.WriteLine("Try \"{0}\"", similarM);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error...");
                }
                input = Console.ReadKey();
            } while (input.Key != ConsoleKey.Escape);
            
            Console.ReadKey();
        }
    }
}
