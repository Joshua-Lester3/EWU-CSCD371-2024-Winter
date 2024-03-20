using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Assignment;

public class SampleDataAsync : IAsyncSampleData
{
    public string FilePathAsync { get; set; }
    public SampleDataAsync(string filePath)
    {
        FilePathAsync = filePath;
    }
    public SampleDataAsync() : this("People.csv") { }
    //1
    public IAsyncEnumerable<string> CsvRows
    {
        get
        {   
            var reader =  File.OpenText("People.csv");
            return (IAsyncEnumerable<string>)reader.ReadLineAsync();
            
            
        }
        
    }
    private async Task<List<IPerson>> GetPeople()
    {
        List<IPerson> people = new List<IPerson>();
        await foreach(var row in CsvRows)
        {
            string[] splitter = row.Split(',');
            string firstName = splitter[1];
            string lastName = splitter[2];
            string email = splitter[3];
            string street = splitter[4];
            string city = splitter[5];
            string state = splitter[6];
            string zip = splitter[7];
            people.Add(new Person(firstName, lastName, new Address(street, city, state, zip), email));
        }
        
        people.Sort((comp1, comp2) => { return string.Compare(comp1.Address.State, comp2.Address.State); });
        people.Sort((comp1, comp2) => { return string.Compare(comp1.Address.City, comp2.Address.City); });
        people.Sort((comp1, comp2) => { return string.Compare(comp1.Address.Zip, comp2.Address.Zip); });
        return people;
    }
    //2
    public IAsyncEnumerable<IPerson> People => (IAsyncEnumerable<IPerson>)GetPeople().Result;
    
    //3
    public async IAsyncEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(Predicate<string> filter)
    {
        
        await foreach(var people in People)
        {
            if(filter(people.EmailAddress))
            {
                yield return (people.FirstName, people.LastName);
            }
        }
        
    }
    //4
    public string GetAggregateListOfStatesGivenPeopleCollection(IAsyncEnumerable<IPerson> people)
    {
        throw new NotImplementedException();
    }
    //5
    public string GetAggregateSortedListOfStatesUsingCsvRows() => AggregateListStates().Result;
    
    private async Task<string> AggregateListStates()
    {
        var stateList = new List<string>();
        await foreach (var person in People)
        {
            stateList.Add(person.Address.State);
        }
        var result = string.Join(", ", stateList);
        
        return result;

    }
    //6
    public async IAsyncEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
    {
        List<string> result = new List<string>();
        await foreach(var id in CsvRows)
        {
            string[] splitter = id.Split(',');
            string state = splitter[splitter.Length -2];
            result.Add(state);
            
        }
        result.Sort();
        yield return result.ToString()!;
    }
}
