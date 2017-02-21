using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommendation_system
{
    //1.fading average
    //2.The similarity of the two films
    class Task3
    {
        private string _moviesPath;
        private string _ratingsPath;
        private Int64 _usersCount;
        private Dictionary<Int64,string> _movies;
        private List<string[]> _ratings;
        public string GetMovieName(Int64 i)
        {
            return _movies[i];
        }
        public Task3(string moviePath, string ratingsPath)
        {
            _moviesPath = moviePath;
            _ratingsPath = ratingsPath;

            _movies = new Dictionary<long, string>();
            _ratings = new List<string[]>();

            ReadData(_moviesPath, ref _movies);
            ReadData(_ratingsPath, ref _ratings);

            _usersCount = _ratings.GroupBy(x => x[0]).Count();
        }
        private void ReadData(string path, ref List<string[]> collection)
        {
            using (TextFieldParser reader = new TextFieldParser(path))
            {
                reader.TextFieldType = FieldType.Delimited;
                reader.SetDelimiters(",");
                reader.HasFieldsEnclosedInQuotes = true;
                string[] currentLine;
                reader.ReadLine();
                while (!reader.EndOfData)
                {
                    currentLine = reader.ReadFields();
                    collection.Add(currentLine);
                }
            }
        }
        private void ReadData(string path, ref Dictionary<Int64, string> collection)
        {
            using (TextFieldParser reader = new TextFieldParser(path))
            {
                reader.TextFieldType = FieldType.Delimited;
                reader.SetDelimiters(",");
                reader.HasFieldsEnclosedInQuotes = true;
                string[] currentLine;
                reader.ReadLine();
                while (!reader.EndOfData)
                {
                    currentLine = reader.ReadFields();
                    collection.Add(Int64.Parse(currentLine[0]),currentLine[1]);
                }
            }
        }

        public Dictionary<Int64, double> DampedMeanCollection()
        {
            Dictionary<Int64, double> result = new Dictionary<Int64, double>();
            int k = 5;
            int my = 3;
            var movies = _ratings.GroupBy(r => r[1])
                                .Select(group => new { id = group.Key, ratings = group.ToList() });
            //var movies = from m in _movies
            //             join r in _ratings on m[0] equals r[1] into ratedM
            //             from sub in ratedM.DefaultIfEmpty()
            //             select new {id = m[0], ratings = sub} 
            //Dictionary<Int64, double> outerMovies = _movies.Where(x => !movies.Any(y => y.id == x[0]))
            //                                                .Select(x => new KeyValuePair<Int64, double>(Int64.Parse(x[0]), 3))
            //                                                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var m in movies)
            {
                double sumRate = 0;
                foreach(var rate in m.ratings)
                {
                    sumRate += double.Parse(rate[2]);
                }
                double mean = (sumRate + k * my) / (m.ratings.Count + k);
                result.Add(Int64.Parse(m.id), mean);
            }
            return result;
        }
        public Int64 GetMostSimilar(Int64 id)
        {
            Dictionary<Int64, List<Int64>> movies = _ratings.GroupBy(x => x[1]).ToDictionary(group => Int64.Parse(group.Key), group => group.Select(x => Int64.Parse(x[0])).ToList());
            Int64 mostSimilarId = -1;
            double value = double.MinValue;
            foreach(var movie in movies)
            {
                if (id != movie.Key)
                {
                    var currentValue = SimilarityValue(id, movie.Key, ref movies);
                    if (currentValue > value)
                    {
                        value = currentValue;
                        mostSimilarId = movie.Key;
                    }
                }

            }
            return mostSimilarId;
        }
        public double SimilarityValue(Int64 xId,Int64 yId, ref Dictionary<Int64,  List<Int64>> movies)
        {
            double XAndY = movies[xId].Intersect(movies[yId]).Count();
            double X = movies[xId].Count();
            double NotXAndY = movies[yId].Except(movies[xId]).Count();
            double NotX = _usersCount - X;
            return ((XAndY)/(1+X))/(1 + ((NotXAndY)/(1+NotX)));
        }

    }
}
