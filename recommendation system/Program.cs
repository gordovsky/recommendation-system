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


            //var reddit = new Reddit(@"C:\Users\col403\Desktop\reddit.csv");
            //reddit.ReadData();
            //reddit.RankNews();
            //reddit.Print();

            Task3 task = new Task3(@"C:\Users\col403\Desktop\ml-latest-small\movies.csv",
                                   @"C:\Users\col403\Desktop\ml-latest-small\ratings.csv");

            var dm = task.DampedMeanCollection();
            var dampedTop10 = dm.OrderByDescending(x => x.Value).Take(10);
            foreach(var d in dampedTop10)
            {
                Console.WriteLine("{0} damped mean: {1}. Similar to ",task.GetMovieName(d.Key), d.Value);
            }

            //var fdsx = task.SimilarityValue(4324, 4234);
            
            string similar = task.GetMovieName(task.GetMostSimilar(8));
            Console.ReadKey();
        }
    }
}
