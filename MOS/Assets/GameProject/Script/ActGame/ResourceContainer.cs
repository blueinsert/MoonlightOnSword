using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceContainer : MonoBehaviour {

    [Serializable]
    public class AssetCacheItem
    {
        public string Name;
        public UnityEngine.Object Asset;

        [Header("[不要手动设置!]")]
        public string AssetPath;
        [Header("[不要手动设置!]")]
        public UnityEngine.Object RuntimeAssetCache;    // 运行期的资源缓存
    }

    public UnityEngine.Object GetAsset(string name)
    {
        foreach(var item in AssetList)
        {
            if(item.Name == name)
            {
                return item.RuntimeAssetCache;
            }
        }
        return null;
    }

    public void LoadAllResources()
    {
        foreach(var item in AssetList)
        {
            var go = ResourceManager.Instance.LoadAsset<GameObject>(item.AssetPath);
            item.RuntimeAssetCache = go;
        }
    }

    /// <summary>
    /// 资源列表
    /// </summary>
    public List<AssetCacheItem> AssetList = new List<AssetCacheItem>();
}
