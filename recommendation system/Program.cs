using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Data;

namespace recommendation_system
{
    class Program
    {
        static void Main(string[] args)
        {

            SlopeOne slope = new SlopeOne();

            slope.ReadData(@"C:\Users\Admin\Desktop\Assignment7.csv");
            //slope.Run();

            var rate = slope.PredictFor(1,1);

            //NBC nbc = new NBC();
            //nbc.Verdict();

            Console.WriteLine("Rate: {0}", rate);

            //Console.WriteLine
            //var x = nbc.Probability(true);
            //var y = nbc.Probability(false);

            Console.ReadKey();

            //var reddit = new Reddit(@"C:\Users\col403\Desktop\reddit.csv");
            //reddit.ReadData();
            //reddit.RankNews();
            //reddit.Print();

            //Task3 task = new Task3(@"C:\Users\col403\Desktop\ml-latest-small\movies.csv",
            //                       @"C:\Users\col403\Desktop\ml-latest-small\ratings.csv");

            //var dm = task.DampedMeanCollection();
            //var dampedTop10 = dm.OrderByDescending(x => x.Value).Take(10);
            //foreach(var d in dampedTop10)
            //{
            //    string similar = task.GetMovieName(task.GetMostSimilar(d.Key));
            //    Console.WriteLine("{0} damped mean: {1}. Similar to {2}",task.GetMovieName(d.Key), d.Value, similar);
            //}

            //var fdsx = task.SimilarityValue(4324, 4234);

            //string similar = task.GetMovieName(task.GetMostSimilar(8));
            //Console.ReadKey();
        }
    }
}
