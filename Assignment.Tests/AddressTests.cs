
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Assignment.Tests;
public class AddressTests
{
    //These test are not really needed but to check another test in SampleDataTests.csv
    [Fact]
    public void CheckAddressConstructor_CSVAddress_Is_The_Same()
    {
        var address = new Address("94148 Kings Terrace", "Long Beach", "CA", "59721");
        SampleData data = new("TestingCsv.csv");

        List<string> dataList = data.CsvRows.ToList();
        string element = dataList[2].Split(',')[4];

        Assert.Equal(element, address.StreetAddress);
    }
}
