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
        public IEnumerable<IPerson> People => (from id in CsvRows
                                               let elements = id.Split(",")
                                               let street = elements[4]
                                               let city = elements[5]
                                               let state = elements[6]
                                               let zip = elements[7]
                                               let address = new Address(street, city, state, zip)
                                               orderby state, city, zip
                                               select new Person(elements[1], elements[2], address, elements[3]));

        // 5.
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(
            Predicate<string> filter)
        {
            return People.Where(person => filter(person.EmailAddress)).Select(person => (person.FirstName, person.LastName));
            /*return (IEnumerable<(string FirstName, string LastName)>)(from people in People
                   where filter(people.EmailAddress)
                   select new {people.FirstName, people.LastName});*/
        }

        // 6.
        public string GetAggregateListOfStatesGivenPeopleCollection(
            IEnumerable<IPerson> people) => throw new NotImplementedException();
    }
}
