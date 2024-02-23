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
    public SampleData SampleData { get; init; }
    public IEnumerable<string> TestCsvRows { get; init; }
    public SampleDataTests()
    {
        SampleData = new();
        // Many null conditional operators and a null forgiving operator because as long as the
        // file structure stays the same, and People.csv stays in the same place, this will be
        // non-null.
        string directory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.
            Parent?.Parent?.Parent?.FullName!, "Assignment", "People.csv");
        TestCsvRows = File.ReadAllLines(directory).
            Skip(1);

    }
    [Fact]
    public void CsvRows_Get_Success()
    {
        // Arrange

        // Act

        // Assert
        Assert.Equal(TestCsvRows, SampleData.CsvRows);
    }

    [Fact]
    public void GetUniqueSortedListOfStatesGivenCsvRows_NormalCondition_Success()
    {
        // Arrange

        // Act
        IEnumerable<string> data = TestCsvRows;

        // Assert
        Assert.Equal(data, SampleData.GetUniqueSortedListOfStatesGivenCsvRows());
    }
}
