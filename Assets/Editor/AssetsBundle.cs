using UnityEngine;
using UnityEditor;

public class AssetsBundle : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string path = "Assets/StreamingAssets/Bundle";
        if(!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        try
        {
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}