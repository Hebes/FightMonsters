using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载模块
/// </summary>
public class ResModule : SingletonAutoMono<ResModule>, IModule, IResourcesLoadApi
{
    public void InitModule()
    {
        Module.I.resModule = I;
        Module.I.resModule.name = "资源加载模块";
    }

    public T Load<T>(string name, UnityAction<T> callback = null) where T : Object
    {
        T res = Resources.Load<T>(name);
        callback?.Invoke(res);
        //如果对象是一个GameObject类型的 我把他实例化后 再返回出去 外部 直接使用即可
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else//TextAsset AudioClip
            return res;
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }

    public void LoadAsync<T>(string name, UnityAction<T> callback = null) where T : Object
    {
        StartCoroutine(ReallyLoadAsync(name, callback));
    }


    //真正的协同程序函数  用于 开启异步加载对应的资源
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;

        if (r.asset is GameObject)
            callback?.Invoke(GameObject.Instantiate(r.asset) as T);
        else
            callback?.Invoke(r.asset as T);
    }
}
