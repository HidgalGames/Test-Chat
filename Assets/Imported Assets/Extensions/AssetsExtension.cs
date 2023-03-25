#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace AssetsOperations
{
    public static class AssetsExtension
    {
        public class UnityObjectShell
        {
            public UnityEngine.Object TargetObject { get; private set; }

            public UnityObjectShell(UnityEngine.Object obj)
            {
                TargetObject = obj;
            }
        }

        #region Path Operations
        public static string GetSceneFolderPath(this Scene scene)
        {
            return (System.IO.Path.GetDirectoryName(scene.path) + "\\").Replace("\\", "/");
        }

        public static string GetPathToFolder(UnityEngine.Object obj)
        {
            return (System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(obj)) + "\\").Replace("\\", "/");
        }
        public static string GetPathToFolder(this UnityObjectShell obj)
        {
            return (System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(obj.TargetObject)) + "\\").Replace("\\", "/");
        }

        public static string GetPathToFolder(string pathToAsset)
        {
            return (System.IO.Path.GetDirectoryName(pathToAsset) + "\\").Replace("\\", "/");
        }

        public static string GetPathToAsset(this UnityObjectShell obj)
        {
            return AssetDatabase.GetAssetPath(obj.TargetObject) + "/";
        }

        public static string[] GetAssetPathsFromCurrentFolder(this UnityObjectShell obj, string filter)
        {
            var path = GetPathToFolder(obj);
            var guids = AssetDatabase.FindAssets(filter, new string[] { path });

            return guids.Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();
        }

        public static string[] GetAssetPathsFrom(string path, string filter)
        {
            var guids = AssetDatabase.FindAssets(filter, new string[] { path });

            return guids.Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();
        }
        public static string[] GetAssetPaths(string filter)
        {
            var guids = AssetDatabase.FindAssets(filter);

            return guids.Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();
        }
        #endregion

        #region Assets Operations

        public static IEnumerable<T> GetAssetsOfType<T>() where T : UnityEngine.Object
        {
            var assetPaths = GetAssetPaths($"t:{typeof(T).Name}");

            return GetListOfAssets<T>(assetPaths);
        }
        public static IEnumerable<T> GetAssetsOfType<T>(this DefaultAsset target) where T : UnityEngine.Object
        {
            var folderPath = target.AsAsset().GetPathToFolder();
            var assetPaths = GetAssetPathsFrom(folderPath, $"t:{typeof(T).Name}");

            return GetListOfAssets<T>(assetPaths);
        }


        public static IEnumerable<T> GetAssetsOfTypeFrom<T>(string path) where T : UnityEngine.Object
        {
            var assetPaths = GetAssetPathsFrom(path, $"t:{typeof(T).Name}");

            return GetListOfAssets<T>(assetPaths);
        }
        public static IEnumerable<T> GetAssetsOfTypeFromCurrentFolder<T>(this UnityObjectShell obj) where T : UnityEngine.Object
        {
            var assetPaths = obj.GetAssetPathsFromCurrentFolder($"t:{typeof(T).Name}");

            return GetListOfAssets<T>(assetPaths);
        }


        public static IEnumerable<T> GetPrefabsWithComponentFrom<T>(string path) where T : UnityEngine.Component
        {
            var assetPaths = GetAssetPathsFrom(path, $"t:Prefab");

            return GetListOfAssets<T>(assetPaths);
        }
        public static IEnumerable<T> GetPrefabsWithComponentFromCurrentFolder<T>(this UnityObjectShell obj) where T : UnityEngine.Component
        {
            var assetPaths = obj.GetAssetPathsFromCurrentFolder($"t:Prefab");

            return GetListOfAssets<T>(assetPaths);
        }
        #endregion

        #region Misc
        public static UnityObjectShell AsAsset(this UnityEngine.Object obj)
        {
            return new(obj);
        }

        private static IEnumerable<T> GetListOfAssets<T>(string[] paths) where T : UnityEngine.Object
        {
            var result = new List<T>();

            foreach (var assetPath in paths)
            {
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset)
                {
                    result.Add(asset);
                }
            }

            return result;
        }
        #endregion
    }
}
#endif
