using System;

public static class CustomUtils
{
    public static bool CompareIDs(string a, string b)
    {
        return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }
}
