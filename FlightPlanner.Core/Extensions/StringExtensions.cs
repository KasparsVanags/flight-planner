namespace FlightPlanner.Core.Extensions;

public static class StringExtensions
{
    public static string Capitalize(this string str)
    {
        return string.Join(" ", str.Split(" ").Select(x => char.ToUpper(x[0]) + x[1..]));
    }
}