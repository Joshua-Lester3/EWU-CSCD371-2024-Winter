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
    // Requirement 1
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

    // Requirement 2
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
        return string.Compare(previousItem, currentItem, StringComparison.CurrentCulture) <= 0;
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
                if (outerState.Equals(innerState, StringComparison.Ordinal))
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

    // Requirement 3
    [Fact]
    public void GetAggregateSortedListOfStatesUsingCsvRows()
    {
        // Arrange
        SampleData sampleData = new("TestingCsv.csv");

        // Act
        string states = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();
        string expected = "CA, FL, GA, MT";

        // Assert
        Assert.Equal(expected, states);
    }

    // Requirement 4
    [Fact]
    public void Person_Initialize_FilePath()
    {
        SampleData sampleData = new("TestingCsv.csv");

        Assert.NotEmpty(sampleData.People);
    }

    [Fact]
    public void PeopleProperty_CheckExpected_Orderby()
    {
        SampleData sampleData = new("TestingCsv.csv");

        List<List<string>> expectedList = sampleData.CsvRows.
            Select(row => row.Split(',').ToList()).
            OrderBy(row => row[6]).ThenBy(row => row[5]).
            ThenBy(row => row[4]).ToList();
        List<IPerson> actualList = sampleData.People.ToList();
        
     
        for(int j = 0; j < actualList.Count && j < expectedList.Count; j++) 
        {
            IPerson person = actualList[j];
            List<string> orderPerson = expectedList[j];
            Assert.True(PersonComparer(person, orderPerson));
        }
    }
    private static bool PersonComparer(IPerson person, List<string> listPeople) 
    {
        bool firstNameEq = person.FirstName.Equals(listPeople[1], StringComparison.Ordinal);
        bool lastNameEq = person.LastName.Equals(listPeople[2], StringComparison.Ordinal);
        bool emailAddressEq = person.EmailAddress.Equals(listPeople[3], StringComparison.Ordinal);
        IAddress address = person.Address;
        bool streetEq = address.StreetAddress.Equals(listPeople[4], StringComparison.Ordinal);
        bool cityEq = address.City.Equals(listPeople[5], StringComparison.Ordinal);
        bool stateEq = address.State.Equals(listPeople[6], StringComparison.Ordinal);
        bool zipEq = address.Zip.Equals(listPeople[7], StringComparison.Ordinal);
        bool equality = firstNameEq && lastNameEq && emailAddressEq && streetEq && cityEq && zipEq && stateEq;
        return equality;
    }

    // Requirement 5
    [Fact]
    public void FilterByEmailAddress_FirstAndLastName_Match()
    {
        SampleData sampleData = new("TestingCsv.csv");


        Predicate<string> search = i => i.Equals("pjenyns0@state.gov", StringComparison.Ordinal);

        List<(string firstName, string lastName)> data = new List<(string firstName, string lastName)>
        {
            ("Priscilla", "Jenyns"),
        };

        var emailFilter = sampleData.FilterByEmailAddress(search);


        Assert.Equal(data, emailFilter);

    }


    // Requirment 6
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
