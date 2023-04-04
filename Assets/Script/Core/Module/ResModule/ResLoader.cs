using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 面向接口编程
/// </summary>
public class ResLoader : IResourcesLoadApi
{
    /// <summary>
    /// 加载单个
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public T Load<T>(string name, UnityAction<T> callback = null) where T : UnityEngine.Object
    {
        T res = Resources.Load<T>(name);
        callback?.Invoke(res);
        return res;
    }

    /// <summary>
    /// 加载所有
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T[] LoadAll<T>(string path) where T : UnityEngine.Object
    {
        return Resources.LoadAll<T>(path);
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    public void LoadAsync<T>(string name, UnityAction<T> callback = null) where T : UnityEngine.Object
    {
        ModuleManager.Instance.monoModule.StartCoroutine(ReallyLoadAsync(name, callback));
    }

    /// <summary>
    /// 真正的协同程序函数  用于 开启异步加载对应的资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback = null) where T : UnityEngine.Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;
        //if (r.asset is GameObject)
        //    callback?.Invoke(GameObject.Instantiate(r.asset) as T);
        //else
        callback?.Invoke(r.asset as T);
    }
}
