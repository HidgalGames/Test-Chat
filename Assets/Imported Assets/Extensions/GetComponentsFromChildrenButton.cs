using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityEditorExtensions
{
    public static class GetComponentsFromChildrenButton
    {
#if UNITY_EDITOR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttonName">Button name override</param>
        /// <returns>True if button was pressed in current frame</returns>
        public static bool DrawGetComponentsInChildrenButton(this Editor editor, string buttonName = "Get Components From Children")
        {
            var targetComponent = editor.target;
            var serializedObject = editor.serializedObject;

            if (GUILayout.Button(buttonName))
            {
                var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                var fields = targetComponent.GetType().GetProperties(bindingFlags);
                var gameObject = (targetComponent as MonoBehaviour).gameObject;

                foreach (var field in fields)
                {
                    if (field.PropertyType.IsSubclassOf(typeof(Object)))
                    {
                        if (field.CanWrite && field.GetSetMethod(true) != null)
                        {
                            var valueAsObject = (Object)field.GetValue(targetComponent);
                            if (!valueAsObject)
                            {
                                var component = gameObject.GetComponentInChildren(field.PropertyType, true);

                                if (component)
                                {
                                    field.SetValue(targetComponent, component);
                                }
                            }
                        }
                    }
                }

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(targetComponent);

                return true;
            }

            return false;
        }
#endif
    }
}
