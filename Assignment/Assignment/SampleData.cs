using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Assignment
{
    public class SampleData : ISampleData
    {
        public string FilePath { get; set; }
        public SampleData(string filePath)
        {
            FilePath = filePath;
        }
        public SampleData() : this("People.csv") { }

        // 1.
        public IEnumerable<string> CsvRows => File.ReadLines(FilePath).Skip(1);

        // 2.
        public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
        {
            return (from line in CsvRows
                   let elements = line.Split(',')
                   let state = elements[elements.Length - 2]
                   orderby state
                   select state).Distinct();
        }

        // 3.
        public string GetAggregateSortedListOfStatesUsingCsvRows()
            => throw new NotImplementedException();

        // 4.
        public IEnumerable<IPerson> People => throw new NotImplementedException();

        // 5.
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(
            Predicate<string> filter) => throw new NotImplementedException();

        // 6.
        public string GetAggregateListOfStatesGivenPeopleCollection(
            IEnumerable<IPerson> people) => throw new NotImplementedException();
    }
}
