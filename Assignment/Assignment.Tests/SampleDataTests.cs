using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System;
using System.Reflection.PortableExecutable;

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
        Assert.Equal(data.ToArray(), sampleData.CsvRows.ToArray());
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
        foreach (string outerState in data) {
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
    #endregion
    //for number 4
    [Fact]
    public void Person_Initialize_FilePath()
    {
        SampleData sampleData = new("TestingCsv.csv");

        //Address address = new("7884 Corry Way", "Helena", "MT", "70577");

        //Person person = new("Priscilla", "Jenyns", address, "pjenyns0@state.gov");


        Assert.NotEmpty(sampleData.People);
    }
    //for number 5
    //[Fact] // commented to make code compile
    //public void FilterByEmailAddress_FirstAndLastName_Match()
    //{
    //    SampleData sampleData = new("TestingCsv.csv");
    //    IEnumerable<string> data = new string[] {
    //        "Priscilla Jenyns", "Karin Joder", "Chadd Stennine", "Fremont Pallaske", "Jimbob,Pallaske"
    //    };
        
    //    Predicate<string> filterEmail = sampleData.FilterByEmailAddress();
        
    //}
}
