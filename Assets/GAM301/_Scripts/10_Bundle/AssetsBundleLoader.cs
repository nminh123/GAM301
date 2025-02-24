using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AssetsBundleLoader : MonoBehaviour
{
    [SerializeField] List<BundleInfo> bundleInfos = new List<BundleInfo>();
    IEnumerator Start()
    {
        foreach (var bundleInfo in bundleInfos)
        {
            string path = Application.streamingAssetsPath + "/Bundles/" + bundleInfo.bundleName;
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                yield break;
            }
            GameObject prefab = bundle.LoadAsset<GameObject>(bundleInfo.assetName);
            Instantiate(prefab);
            bundle.Unload(false);
        }
        // string path = Application.streamingAssetsPath + "/Bundles/" + bundleName;
        // AssetBundle bundle = AssetBundle.LoadFromFile(path);
        // if (bundle == null)
        // {
        //     Debug.LogError("Failed to load AssetBundle!");
        //     yield break;
        // }
    }
}

[Serializable]
public class BundleInfo
{
    public string bundleName;
    public string assetName;
}