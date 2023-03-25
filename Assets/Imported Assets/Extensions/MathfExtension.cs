using UnityEngine;

public static class MathfExtension
{
    /// <summary>
    /// Returns the sign of value
    /// </summary>
    /// <param name="value"></param>
    /// <returns>-1, 1 or 0</returns>
    public static float Sign(this float value)
    {
        return Mathf.Approximately(value, 0) ? 0 : (value < 0 ? -1 : 1);
    }
}
