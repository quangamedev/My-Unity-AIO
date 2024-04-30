public static class MathUtils
{
    /// <summary>
    /// Returns the min or max if the value lies in between, if not, return the value.
    /// </summary>
    public static int InverseClamp(int value, int min, int max)
    {
        // the given value is in the range specified
        if (value > min && value < max)
        {
            int mid = (max - min) / 2 + min;
            return value < mid ? min : max;
        }
        return value;
    }

    /// <summary>
    /// Returns the min or max if the value lies in between, if not, return the value.
    /// </summary>
    public static float InverseClamp(float value, float min, float max)
    {
        // the given value is in the range specified
        if (value > min && value < max)
        {
            float mid = (max - min) / 2 + min;
            return value < mid ? min : max;
        }
        return value;
    }
}
