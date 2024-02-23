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
        Assert.Equal(expected, sampleData.CsvRows);
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
        Assert.Equal(expected, sampleData.CsvRows);
    }
    #endregion

    #region Requirements 1-7 Tests
    [Fact]
    public void CsvRows_Get_Success()
    {
        // Arrange
        SampleData sampleData = new("People.csv");
        string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.
            Parent?.Parent?.Parent?.FullName!, "Assignment", "People.csv");
        IEnumerable<string> data = File.ReadAllLines(filePath).Skip(1);

        // Act

        // Assert
        Assert.Equal(data, sampleData.CsvRows);
    }

    //TODO: 1. test uniqueness for req2; 2. test sortedness for req2

    [Fact]
    public void GetUniqueSortedListOfStatesGivenCsvRows_NormalCondition_SuccessfullyContainsCorrectItems()
    {
        // Arrange
        SampleData sampleData = new("TestingCsv.csv");

        // Act
        //IEnumerable<string> data = new string[] {
        //    "AL", "AK", "AZ", "AR",
        //    "CA","CO","CT","DE","DC","FL","GA","HI",
        //    "ID","IL","IN","IA","KS","KY","LA","ME", "MD",
        //    "MA","MI","MN","MS","MO","MT","NE","NV",
        //    "NH","NJ","NM","NY","NC","ND","OH","OK",
        //    "OR","PA","RI","SC","SD","TN","TX","UT",
        //    "VT","VA","WA","WV","WI","WY"
        //};
        IEnumerable<string> data = new string[] {
            "CA", "FL", "GA", "MT"
        };


        // Assert
        IEnumerable<string> uniqueSortedListOfStates = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();
        Assert.True(uniqueSortedListOfStates.All(item => data.Contains(item)));
    }

    [Fact]
    public void GetUniqueSortedListOfStatesGivenCsvRows_NormalCondition_SuccessfullySorts()
    {

    }

    [Fact]
    public void GetUniqueSortedListOfStatesGivenCsvRows_NormalCondition_ItemsAreUnique()
    {
        // Arrange
        SampleData sampleData = new("TestingCsv.csv");

        // Act
        IEnumerable<string> data = new string[] {
            "CA", "FL", "GA", "MT"
        };


        // Assert
        Assert.True(sampleData.GetUniqueSortedListOfStatesGivenCsvRows().SequenceEqual(data));
    }
    #endregion
}
