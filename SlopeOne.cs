using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

public class Class1
{
    private List<string> _movies;
    private List<string[]> _ratings;

    public Class1()
	{
        _movies = new List<string>();
        _ratings = new List<string[]>();
    }


    private void  ReadData(string ratingsPath)
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
}
