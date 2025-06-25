using FLP.AzureFunctions.Extensions;
using FLP.Core.Context.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLP.AzureFunction.UnitTest.Tests.Extensions;

public class HttpRequestExtensionsTest
{
    [Theory]
    [InlineData("asc", "", SortOrder.Ascending)]
    [InlineData("desc", "", SortOrder.Descending)]
    [InlineData("Ascending", "", SortOrder.Ascending)]
    [InlineData("Descending", "", SortOrder.Descending)]
    [InlineData("0", "", SortOrder.Ascending)]
    [InlineData("1", "", SortOrder.Descending)]
    [InlineData("price_asc", "price", SortOrder.Ascending)]
    [InlineData("price_desc", "price", SortOrder.Descending)]
    [InlineData("price_Ascending", "price", SortOrder.Ascending)]
    [InlineData("price_Descending", "price", SortOrder.Descending)]
    [InlineData("price_0", "price", SortOrder.Ascending)]
    [InlineData("price_1", "price", SortOrder.Descending)]
    [InlineData("price", "price", SortOrder.Ascending)]
    [InlineData(null, "", SortOrder.Ascending)]
    public void Run_ParseSort(string? value, string? expectedSortBy, SortOrder expectedSortOrder)
    {
        var (sortBy, sortOrder) = HttpRequestExtensions.ParseSort(value);

        Assert.Equal(expectedSortBy, sortBy);
        Assert.Equal(expectedSortOrder, sortOrder);
    }

    [Theory]
    [InlineData("open", BugStatus.Open)]
    [InlineData("inprogress", BugStatus.InProgress)]
    [InlineData("resolved", BugStatus.Resolved)]
    [InlineData("closed", BugStatus.Closed)]

    [InlineData("Open", BugStatus.Open)]
    [InlineData("In Progress", BugStatus.InProgress)]
    [InlineData("Resolved", BugStatus.Resolved)]
    [InlineData("Closed", BugStatus.Closed)]

    [InlineData("0", BugStatus.Open)]
    [InlineData("1", BugStatus.InProgress)]
    [InlineData("2", BugStatus.Resolved)]
    [InlineData("3", BugStatus.Closed)]

    [InlineData(null, null)]
    public void Run_ParseStatus(string? value, BugStatus? expectedBugStatus)
    {
        var bugStatus = HttpRequestExtensions.ParseStatus(value);

        Assert.Equal(expectedBugStatus, bugStatus);
    }
}
