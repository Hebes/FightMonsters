using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换模块
/// 知识点
/// 1.场景异步加载
/// 2.协程
/// 3.委托
/// </summary>
public class ScenesModule : SingletonAutoMono<ScenesModule>,IModule
{
    public void InitModule()
    {
        ModuleManager.Instance.scenesModule = Instance; 
        ModuleManager.Instance.scenesModule.name = "场景加载模块";
    }

    /// <summary>
    /// 切换场景 同步
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name, UnityAction callback = null)
    {
        //场景同步加载
        SceneManager.LoadScene(name);
        PoolModule.Instance.Clear();
        //加载完成过后 才会去执行fun
        callback?.Invoke();
    }

    /// <summary>
    /// 提供给外部的 异步加载的接口方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public void LoadSceneAsyn(string name, UnityAction callback = null)
    {
        //MonoModule.Instance.StartCoroutine(ReallyLoadSceneAsyn(name, callback));
        StartCoroutine(ReallyLoadSceneAsyn(name, callback));
    }

    /// <summary>
    /// 协程异步加载场景
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction callback)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);

        //可以得到场景加载的一个进度
        while (!ao.isDone)
        {
            //事件中心 向外分发 进度情况  外面想用就用
            EventModule.Instance.EventTrigger("进度条更新", ao.progress);
            //这里面去更新进度条
            yield return ao;
        }
        PoolModule.Instance.Clear();
        //加载完成过后 才会去执行fun
        callback?.Invoke();
    }
}
