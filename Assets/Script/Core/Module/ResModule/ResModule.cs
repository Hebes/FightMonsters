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
    private IResourcesLoadApi loader { get; set; }

    public void InitModule()
    {
        loader = new ResLoader();
        ModuleManager.Instance.resModule = Instance;
        ModuleManager.Instance.resModule.name = "资源加载模块";
    }

    public T Load<T>(string name, UnityAction<T> callback = null) where T : Object
    {
        return loader.Load(name, callback);
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        return loader.LoadAll<T>(path);
    }

    public void LoadAsync<T>(string name, UnityAction<T> callback = null) where T : Object
    {
        loader.LoadAsync(name, callback);
    }
}
