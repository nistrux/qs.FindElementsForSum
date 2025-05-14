namespace qs.FindElementsForSum;

public class SumFinder
{
    /// <summary>
    /// <see href="https://www.geeksforgeeks.org/window-sliding-technique/">Sliding Window Technique</see>
    /// </summary>
    /// <param name="list">input list</param>
    /// <param name="sum">target sum</param>
    /// <param name="start">start of range (inclusive)</param>
    /// <param name="end">end of range (exclusive)</param>
    public void FindElementsForSum(List<uint> list, ulong sum, out int start, out int end)
    {
        start = 0;
        end = 0;

        if (list.Count == 0 || sum == 0)
        {
            return;
        }

        ulong currentSum = 0;
        int windowStart = 0;

        // Expand window to the right using windowEnd
        for (int windowEnd = 0; windowEnd < list.Count; windowEnd++)
        {
            currentSum += list[windowEnd]; //add current element

            // If sum is too large, shrink window from the left.
            while (currentSum > sum && windowStart <= windowEnd)
            {
                currentSum -= list[windowStart]; //remove first element from sum
                windowStart++; //shink window
            }

            //keep sliding if not found
            if (currentSum != sum) continue;
            
            //answer found
            start = windowStart;
            end = windowEnd + 1; // exclusive range
            return;
        }
    }
}