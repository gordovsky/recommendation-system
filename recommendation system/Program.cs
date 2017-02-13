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
            

            var reddit = new RedditAlg(@"C:\Users\col403\Desktop\reddit.csv");
            reddit.ReadData();
            reddit.RankNews();
            reddit.Print();
            
            Console.ReadKey();
        }
    }


    public class RedditAlg
    {
        private DateTime _now;
        private string _path;
        private List<string[]> _data = new List<string[]>();
        private SortedDictionary<double, string> _news = new SortedDictionary<double, string>();
        public RedditAlg(string path)
        {
            _now = DateTime.Now;
            _path = path;
        }

        public void ReadData()
        {
            using (TextFieldParser reader = new TextFieldParser(_path))
            {
                reader.TextFieldType = FieldType.Delimited;
                reader.SetDelimiters(",");
                reader.HasFieldsEnclosedInQuotes = true;
                string[] currentLine;
                reader.ReadLine();
                while (!reader.EndOfData)
                {
                    currentLine = reader.ReadFields();
                    _data.Add(currentLine);
                }
            }
        }

        public double GetRate(string[] post)
        {
            double Ts = (_now - DateTime.Parse(post[1])).TotalSeconds;
            Int64 x = Int64.Parse(post[2]) - Int64.Parse(post[3]);
            int y = 0;
            if (x > 0)
                y = 1;
            if (x == 0)
                y = 0;
            if (x < 0)
                y = -1;
            Int64 z = (Math.Abs(x) >= 0) ? Math.Abs(x) : 1;
            return Math.Log10(z) + y * Ts / 45000;
        }

        public void RankNews()
        {
            foreach (var row in _data)
            {
                _news.Add(GetRate(row), row[0]);
            }
        }

        public void Print()
        {
            foreach (var el in _news)
            {
                Console.WriteLine("{0} Rate is : {1}", el.Value, el.Key);
            }
        }
        //public int[,] GetMatrix()
        //{
        //    int[,] matrix = new int[20, 20];
        //    string line = string.Empty;
        //    StreamReader sr = new StreamReader(_path);

        //    while ((line = sr.ReadLine()) != null)
        //    {
        //        String[] parts_of_line = line.Replace("\"","").Split(',');
        //        //for (int i = ; i < parts_of_line.Length; i++)
        //        //    parts_of_line[i] = parts_of_line[i].Trim();

        //        // do with the parts of the line whatever you like

        //    }

        //    return matrix;
        //}

        //public string[] GetFilms()
        //{

        //}

        //public string[] GetUsers()
        //{

        //}
    }
}
