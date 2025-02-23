using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVLibrary;

public static class CsvReader
{
    public static IEnumerable<string[]> Read(string filepath,
        bool hasHeader = false)
    {
        ConcurrentBag<string[]> Data = new();
        IEnumerable<string> DataRows;

        if (hasHeader)
        {
            DataRows = File.ReadLines(filepath).Skip(1);
        }
        else
        {
            DataRows = File.ReadLines(filepath);
        }

        Parallel.ForEach(DataRows, row =>
        {
            try
            {
                Data.Add(row.Split(",", StringSplitOptions.TrimEntries));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing line; {row}. Error {ex.Message}");
            }
        });
        return Data;
    }
}
