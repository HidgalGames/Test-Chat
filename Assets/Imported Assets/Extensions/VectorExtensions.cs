using UnityEngine;

public static class VectorExtensions
{
    #region XZ
    public static Vector3 XZ(this Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }

    public static Vector3 ToVector3XZ(this Vector2 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }
    #endregion

    #region Scale
    public static Vector3 ScaleBy(this Vector3 vector, Vector3 other)
    {
        vector.Scale(other);
        return vector;
    }

    public static Vector2 ScaleBy(this Vector2 vector, Vector2 other)
    {
        vector.Scale(other);
        return vector;
    }
    #endregion
}
