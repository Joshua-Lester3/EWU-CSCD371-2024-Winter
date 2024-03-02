using System.IO;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System;

namespace Assignment.Tests;

public class SampleDataTests
{
    #region Constructor + Properties Tests
    [Fact]
    public void SampleData_InitializedFilePath_Success()
    {
        // Arrange
        SampleData sampleData = new("People.csv");
        // Many null conditional operators and a null forgiving operator because as long as the
        // file structure stays the same, and People.csv stays in the same place, this will be
        // non-null.
        string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.
            Parent?.Parent?.Parent?.FullName!, "Assignment", "People.csv");
        IEnumerable<string> expected = File.ReadLines(filePath).Skip(1);

        // Act

        // Assert
        Assert.Equal(expected.ToArray(), sampleData.CsvRows.ToArray());
    }

    [Fact]
    public void SampleData_DefaultFilePath_Success()
    {
        // Arrange
        SampleData sampleData = new();
        string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.
            Parent?.Parent?.Parent?.FullName!, "Assignment", "People.csv");
        IEnumerable<string> expected = File.ReadLines(filePath).Skip(1);

        // Act

        // Assert
        Assert.Equal(expected.ToArray(), sampleData.CsvRows.ToArray());
    }
    #endregion

    #region Requirements 1-7 Tests
    [Fact]
    public void CsvRows_Get_Success()
    {
        // Arrange
        string filePath = "TestingCsv.csv";
        SampleData sampleData = new(filePath);
        IEnumerable<string> data = File.ReadAllLines(filePath).Skip(1);

        // Act

        // Assert
        Assert.Equal(data, sampleData.CsvRows);
    }

    [Fact]
    public void GetUniqueSortedListOfStatesGivenCsvRows_NormalCondition_SuccessfullyContainsCorrectItems()
    {
        // Arrange
        SampleData sampleData = new("TestingCsv.csv");

        // Act
        IEnumerable<string> data = new string[] {
            "CA", "FL", "GA", "MT"
        };


        // Assert
        IEnumerable<string> uniqueSortedListOfStates = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();
        Assert.Equal(uniqueSortedListOfStates, data);
    }

    [Fact]
    public void GetUniqueSortedListOfStatesGivenCsvRows_NormalCondition_SuccessfullySorts()
    {
        // Arrange
        SampleData sampleData = new();

        // Act
        string? previousItem = null;
        bool isOrdered = true;
        IEnumerable<string> data = sampleData.GetUniqueSortedListOfStatesGivenCsvRows()
            .Select(item =>
            {
                if (!ComparePreviousItem(previousItem, item))
                {
                    isOrdered = false;
                }
                previousItem = item;
                return item;
            })
            .ToArray();

        Assert.True(isOrdered);
    }

    private static bool ComparePreviousItem(string? previousItem, string currentItem)
    {
        if (previousItem == null)
        {
            return true;
        }
        return previousItem.CompareTo(currentItem) <= 0;
    }

    [Fact]
    public void GetUniqueSortedListOfStatesGivenCsvRows_NormalCondition_ItemsAreUnique()
    {
        // Arrange
        SampleData sampleData = new(); // Default .csv has duplicate states

        // Act
        bool isUnique = true;
        IEnumerable<string> data = sampleData.GetUniqueSortedListOfStatesGivenCsvRows().ToArray();
        foreach (string outerState in data)
        {
            int counter = 0;
            foreach (string innerState in data)
            {
                if (outerState.Equals(innerState))
                {
                    counter++;
                }
            }
            if (counter > 1)
            {
                isUnique = false;
            }
        }

        // Assert
        Assert.True(isUnique);
    }

    [Fact]
    public void GetUniqueSortedListOfStatesGivenCsvRows()
    {
        // Arrange
        SampleData sampleData = new("TestingCsv.csv");

        // Act
        string states = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();
        string expected = "CA, FL, GA, MT";

        // Assert
        Assert.Equal(expected, states);
    }

    //for number 4
    [Fact]
    public void Person_Initialize_FilePath()
    {
        SampleData sampleData = new("TestingCsv.csv");

        Assert.NotEmpty(sampleData.People);
    }

    [Fact]
    public void Person_People_Exists()
    {
        SampleData sampleData = new("TestingCsv.csv");
        //public Address(string streetAddress, string city, string state, string zip)
        Address firstAddress = new Address("16958 Forster Crossing", "Atlanta", "GA", "10687");
        Person aPerson = new("Jimbob", "Pallaske", firstAddress, "fpallaske3@umich.edu");
        Person bPerson = new("Chadd", "Stennine", new Address("94148 Kings Terrace", "Long Beach", "CA", "59721"), "cstennine2@wired.com");
        Person cPerson = new("Karin", "Joder", new Address("03594 Florence Park", "Tampa", "FL", "71961"), "kjoder1@quantcast.com");
        Person dPerson = new("Fremont", "Pallaske", new Address("16958 Forster Crossing", "Atlanta", "GA", "10687"), "fpallaske3@umich.edu");
        Person ePerson = new("Priscilla", "Jenyns", new Address("7884 Corry Way", "Helena", "MT", "70577"), "pjenyns0@state.gov");

        IEnumerable<IPerson> expected = new List<IPerson>
        {

         bPerson,
         cPerson,
         dPerson,
         aPerson,
         ePerson


        };

        List<IPerson> actual = sampleData.People.ToList();

        IEnumerator<IPerson> expectedEnumerator = expected.GetEnumerator();
        IEnumerator<IPerson> actualEnumerator = actual.GetEnumerator();

        while (expectedEnumerator.MoveNext() && actualEnumerator.MoveNext())
        {
            IPerson expectedPerson = expectedEnumerator.Current;
            IPerson actualPerson = actualEnumerator.Current;
            Assert.True(PersonEquals(expectedPerson, actualPerson));
        };
    }

    private bool PersonEquals(IPerson person1, IPerson person2)
    {
        bool firstNameEquals = person1.FirstName.Equals(person2.FirstName);
        bool lastNameEquals = person1.LastName.Equals(person2.LastName);
        bool emailAddressEquals = person1.EmailAddress.Equals(person2.EmailAddress);
        IAddress address1 = person1.Address;
        IAddress address2 = person2.Address;
        bool cityEquals = address1.City.Equals(address2.City);
        bool stateEquals = address1.State.Equals(address2.State);
        bool zipEquals = address1.Zip.Equals(address2.Zip);
        bool streetAddressEquals = address1.StreetAddress.Equals(address2.StreetAddress);
        bool addressEquals = cityEquals && stateEquals && zipEquals && streetAddressEquals;
        return firstNameEquals && lastNameEquals && emailAddressEquals && addressEquals;
    }

    //for number 5
    [Fact]
    public void FilterByEmailAddress_FirstAndLastName_Match()
    {
        SampleData sampleData = new("TestingCsv.csv");


        Predicate<string> search = i => i.EndsWith("pjenyns0@state.gov");

        var data = new List<(string firstName, string lastName)>
        {
         ("Priscilla", "Jenyns"),
         //("Karin", "Joder"),
         //("Chadd", "Stennine"),
         //("Fremont", "Pallaske"),
         //("Jimbob","Pallaske")
         };

        var emailFilter = sampleData.FilterByEmailAddress(search);


        Assert.Equal(data, emailFilter.ToArray());

    }


    // req 6
    [Fact]
    public void GetAggregateListOfStatesGivenPeopleCollection_TestCsv_Success()
    {
        // Arrange
        SampleData sampleData = new("TestingCsv.csv");

        // Act
        IEnumerable<IPerson> persons = sampleData.People;
        string aggregateList = sampleData.GetAggregateListOfStatesGivenPeopleCollection(persons);

        // Assert
        string expected = sampleData.GetUniqueSortedListOfStatesGivenCsvRows().
            Aggregate((workingList, state) => $"{workingList}, {state}");
        Assert.Equal(expected, aggregateList);
    }
    #endregion
}
