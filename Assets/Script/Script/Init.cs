using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Init : MonoBehaviour
{
    private List<IModule> modiles;

    private void Awake()
    {
        //加载模块
        modiles = new List<IModule>()
        {
            ActiveLogModule.I,//日志
            UIModule.I,//UI
            EventModule.I,//事件
            AudioModule.I,//声音
            FPSModule.I,//FPS
            MonoModule.I,//协程
            PoolModule.I,//对象池
            PrefabModule.I,//物体加载
            ResModule.I,//加载
            ScenesModule.I,//场景
        };
        modiles.ForEach(mod => { mod.InitModule(); });
        DontDestroyOnLoad(gameObject);
    }
}
