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
    [Fact]
    public void CsvRows_Get_Success()
    {
        // Arrange
        SampleData sampleData = new();
        // Many null conditional operators and a null forgiving operator because as long as the
        // file structure stays the same, and People.csv stays in the same place, this will be
        // non-null.
        string directory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.
            Parent?.Parent?.Parent?.FullName!, "Assignment", "People.csv");

        // Act
        string[] data = File.ReadAllLines(directory).
            Skip(1).ToArray();

        // Assert
        Assert.Equal(data, sampleData.CsvRows);
    }
}
