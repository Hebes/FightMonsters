using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载接口
/// </summary>
public interface IResourcesLoadApi
{
    /// <summary>
    /// 同步加载单个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public T Load<T>(string name, UnityAction<T> callback = null) where T : Object;

    /// <summary>
    /// 异步加载单个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    public void LoadAsync<T>(string name, UnityAction<T> callback = null) where T : Object;

    /// <summary>
    /// 加载同步所有资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T[] LoadAll<T>(string path) where T : Object;
}
