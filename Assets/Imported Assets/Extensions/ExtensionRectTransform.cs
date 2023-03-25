using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ExtensionRectTransform
{
#if UNITY_EDITOR
    [MenuItem("CONTEXT/RectTransform/" + (nameof(SetAnchorsToCorners)))]
    public static void ContextMenuSetAnchorsToCorners(MenuCommand menuCommand)
    {
        RectTransform rectTransform = menuCommand.context as RectTransform;
        if (rectTransform != null)
        {
            Undo.RecordObject(rectTransform, nameof(RectTransform) + " " + ObjectNames.NicifyVariableName(nameof(SetAnchorsToCorners)));
            SetAnchorsToCorners(rectTransform);
            EditorUtility.SetDirty(rectTransform);
        }
    }
#endif
    /// <summary>
    /// Sets RectTransform anchors to its corners.
    /// Not prepared for use with scaled objects!
    /// </summary>
    /// <param name="rectTransform"></param>
    public static void SetAnchorsToCorners(this RectTransform rectTransform)    //TODO support scaled objects
    {
        RectTransform parentRT = rectTransform.parent as RectTransform;
        Rect thisRect = rectTransform.rect;
        float width = thisRect.width;
        float height = thisRect.height;
        Rect parentRect = parentRT.rect;
        Vector2 relativePosition = rectTransform.localPosition;
        Vector2 expecedAnchorMin = new Vector2(
            (float)System.Math.Round(((thisRect.x + relativePosition.x) - parentRect.x) / parentRect.width, 4)
            , (float)System.Math.Round(((thisRect.y + relativePosition.y) - parentRect.y) / parentRect.height, 4)
            );
        Vector2 expecedAnchorMax = expecedAnchorMin + new Vector2(
            (float)System.Math.Round(width / parentRect.width, 4)
            , (float)System.Math.Round(height / parentRect.height, 4)
            );
        //Debug.Log(
        //    "Parent Rect: " + parentRect.ToString()
        //    + "\nThis Rect: " + thisRect.ToString()
        //    + "\nThis LocalPosition: " + relativePosition.ToString("0.00")
        //    + "\nThis pivot: " + rectTransform.pivot.ToString()
        //    + "\nThis anchors min: " + rectTransform.anchorMin.ToString()
        //    + "\nThis anchors max: " + rectTransform.anchorMax.ToString()
        //    + "\nExpected anchors min: " + expecedAnchorMin.ToString()
        //    + "\nExpected anchors max: " + expecedAnchorMax.ToString()
        //    );
        rectTransform.anchorMin = expecedAnchorMin;
        rectTransform.anchorMax = expecedAnchorMax;

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}
