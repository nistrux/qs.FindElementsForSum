using System.Collections;

namespace qs.FindElementsForSum.Tests;

[TestSubject(typeof(SumFinder))]
public class SumFinderTest
{
    [Theory]
    [ClassData(typeof(SumFinderBasicTestData))]
    [ClassData(typeof(SumFinderHugeTestData))]
    public void FumFinder_ReturnsExpectedResults(List<uint> inputList, ulong sum, int expectedStart, int expectedEnd)
    {
        SumFinder sut = new SumFinder();

        sut.FindElementsForSum(inputList, sum, out var start, out var end);

        start.ShouldBe(expectedStart);
        end.ShouldBe(expectedEnd);
    }


    private class SumFinderBasicTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //given tests
            yield return [new List<uint> { 0, 1, 2, 3, 4, 5, 6, 7 }, 11, 5, 7];
            yield return [new List<uint> { 4, 5, 6, 7 }, 18, 1, 4];
            yield return [new List<uint> { 0, 1, 2, 3, 4, 5, 6, 7 }, 88, 0, 0];

            //additional tests
            yield return [new List<uint> { 1, 2, 3 }, 0, 0, 0];
            yield return [new List<uint>(), 0, 0, 0];
            yield return [new List<uint> { 1, 2, 3 }, 1, 0, 1];
            yield return [new List<uint> { 1, 2, 3 }, 3, 0, 2];
            yield return [new List<uint> { 1, 2, 3 }, 6, 0, 3];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private class SumFinderHugeTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            Console.WriteLine("Generating huge test case 1...");
            const int listSize = 25_000_000;
            const int expectedStart1 = 17_900_000;
            const int expectedEnd1 = expectedStart1 + 5;
            var hugeList1 = new List<uint>(listSize);
            for (uint i = 0; i < listSize; i++)
            {
                if (i is >= expectedStart1 and < expectedEnd1)
                {
                    hugeList1.Add(i switch
                    {
                        expectedStart1 => 5,
                        expectedStart1 + 1 => 1_100_000_000,
                        expectedStart1 + 2 => 1_200_000_000,
                        expectedStart1 + 3 => 1_300_000_000,
                        expectedStart1 + 4 => 1,
                        _ => throw new ArgumentOutOfRangeException()
                    });
                    continue;
                }

                hugeList1.Add(i + 1);
            }

            const ulong targetSum1 = 3_600_000_006;

            yield return [hugeList1, targetSum1, expectedStart1, expectedEnd1];

            Console.WriteLine(
                $"Huge test case 1 generated. List size: {listSize:N0}, Target sum: {targetSum1:N0}, Expected start: {expectedStart1:N0}, Expected end: {expectedEnd1:N0}");


            Console.WriteLine("Generating huge test case 2...");
            var hugeList2 = new List<uint>(listSize);
            for (uint i = 0; i < listSize; i++)
            {
                hugeList2.Add(i + 1);
            }

            const ulong targetSum2 = 999_999_999_999_999_999;

            const int expectedStart2 = 0;
            const int expectedEnd2 = 0;

            yield return [hugeList2, targetSum2, expectedStart2, expectedEnd2];
            Console.WriteLine(
                $"Huge test case 2 generated. List size: {listSize:N0}, Target sum: {targetSum2:N0}, Expected start: {expectedStart2:N0}, Expected end: {expectedEnd2:N0}");
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}