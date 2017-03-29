using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

public class SlopeOne
{
    private List<string> _movies;
    private int[,] _ratings;

    public SlopeOne()
    {
        _movies = new List<string>();
        //_ratings = new List<int[]>();
        //_slopedRating = new List<int[]>();
    }


    public void ReadData(string path)
    {
        var linesCount = File.ReadAllLines(path).Count() - 1;


        using (TextFieldParser reader = new TextFieldParser(path))
        {
            reader.TextFieldType = FieldType.Delimited;
            reader.SetDelimiters(";");
            reader.HasFieldsEnclosedInQuotes = true;
            string[] currentLine;
            var columnsCount = reader.ReadFields().Count() - 1;

            _ratings = new int[linesCount, columnsCount];
            int lineNumber = 0;
            while (!reader.EndOfData)
            {
                currentLine = reader.ReadFields();
                _movies.Add(currentLine[0]);
                for(int i = 1; i < currentLine.Length; i++)
                {
                    _ratings[lineNumber, i-1] = !string.IsNullOrEmpty(currentLine[i]) ? int.Parse(currentLine[i]) : -1;
                }
                lineNumber++;
                //var ratingsLine = new int[currentLine.Length - 1];
                //for (int i = 1; i < currentLine.Length; i++)
                //{
                //    ratingsLine[i - 1] = !string.IsNullOrEmpty(currentLine[i]) ? int.Parse(currentLine[i]) : -1;
                //}
                //_ratings.Add(ratingsLine);
            }
        }
    }
    public void Run()
    {
        //for(int row = 0; row < _ratings.Count; row ++)
        //{
        //    for (int i = 0; i < _ratings[row].Length; i++)
        //    {
        //        if (_ratings[row].ElementAt(i) == -1) 
        //            Console.WriteLine("Predicted rating for {0} is: {1}" , "movie", PredictFor( row, i));
        //    }
        //}
    }
    public double PredictFor(int i, int j)
    {
        double numerator = 0;
        double denumerator = 0;
        for (int column = 0; column < _ratings.GetLength(1); column++)
        {
            double sumRate = 0;
            double linesWithAnswersCount = 0;
            double averageDifference = 0;
            if (column != j)
            {
                for (int row = 0; row < _ratings.GetLength(0); row++)
                {
                    if ((_ratings[row, column] != -1) && (_ratings[row, j] != -1))
                    {
                        linesWithAnswersCount++;
                        if (column > j)
                        {
                            sumRate += _ratings[row, j] - _ratings[row, column];
                        }
                        if (column < j)
                        {
                            sumRate += _ratings[row, column] - _ratings[row, j];
                        }
                    }
                }
                averageDifference = sumRate / linesWithAnswersCount;
                numerator += linesWithAnswersCount * (averageDifference + _ratings[i, column]);
                denumerator += linesWithAnswersCount;
            }

            
        }
        var result = numerator / denumerator;
        return result;
    }
}
