using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResourceManager:MonoBehaviour {

	public static ResourceManager Instance;

    public void Awake()
    {
        Instance = new ResourceManager();
        Instance.Init();
    }

    public void Init()
	{

	}

	public T LoadAsset<T>(string path) where T : UnityEngine.Object
	{
        T obj = null;
        LoadAssetByResourcesLoad<T>(path, (rPath, objs) => {
            if (objs.Length > 0)
            {
                obj = objs[0] as T;
            }
        });
        return obj;
	}

    /// <summary>
    /// 使用Resources.Load加载资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onCompleted"></param>
    /// <param name="noErrlog">有些时候从这个函数加载不到资源不一定是error</param>
    protected void LoadAssetByResourcesLoad<T>(string path, System.Action<string, UnityEngine.Object[]> onCompleted, bool noErrlog = false, bool isNeedLoadAllRes = false)
        where T : UnityEngine.Object
    {
        // 先处理后缀
        int postFixIndex = path.LastIndexOf(".");
        var rPath = postFixIndex == -1 ? path : path.Substring(0, postFixIndex);
        int preFixIndex = rPath.IndexOf("Resources/");
        rPath = preFixIndex == -1 ? rPath : rPath.Substring(preFixIndex + "Resources/".Length);

        UnityEngine.Object[] allAsset;

        // 只有fbx使用 loadall
        if (isNeedLoadAllRes)
        {
            allAsset = Resources.LoadAll(rPath);
        }
        else
        {
            allAsset = new UnityEngine.Object[1];
            allAsset[0] = Resources.Load<T>(rPath);
        }

        if (allAsset == null || allAsset.Length == 0)
        {
            if (!noErrlog)
            {
                Debug.LogError(string.Format("LoadAssetByResourcesLoad fail {0}", rPath));
            }
            else
            {
                Debug.Log(string.Format("LoadAssetByResourcesLoad fail {0}", rPath));
            }
        }
        else
        {
            Debug.Log(string.Format("LoadAssetByResourcesLoad ok {0}", rPath));
        }
        onCompleted(rPath, allAsset);
    }
}
